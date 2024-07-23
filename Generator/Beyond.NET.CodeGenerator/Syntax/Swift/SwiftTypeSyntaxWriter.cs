using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Swift;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

using Settings = Beyond.NET.CodeGenerator.Generator.Swift.Settings;

namespace Beyond.NET.CodeGenerator.Syntax.Swift;

public partial class SwiftTypeSyntaxWriter: ISwiftSyntaxWriter, ITypeSyntaxWriter
{
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, ISwiftSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new SwiftConstructorSyntaxWriter() },
        { MemberTypes.Property, new SwiftPropertySyntaxWriter() },
        { MemberTypes.Method, new SwiftMethodSyntaxWriter() },
        { MemberTypes.Field, new SwiftFieldSyntaxWriter() },
        { MemberTypes.Event, new SwiftEventSyntaxWriter() }
    };
    
    private SwiftDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private SwiftTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public SwiftTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(Type type, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No CSharpUnmanagedResult provided");
        }

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsArray) {
            // No need to generate Swift code for those kinds of types

            return string.Empty;
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It has no full name.").ToString();
        }
        
        SwiftCodeBuilder sb = new();

        bool writeMembers = true;
        bool writeTypeDefinition = true;
        bool writeTypeExtension = false;
        
        if (type.IsEnum) {
            writeTypeDefinition = false;
            writeTypeExtension = true;
            
            string enumdefCode = WriteEnumDef(
                type,
                typeDescriptorRegistry
            );

            sb.AppendLine(enumdefCode);
        } else if (type.IsDelegate()) {
            writeTypeDefinition = false;
            writeMembers = false;
            
            string delegateTypedefCode = WriteDelegateTypeDefs(
                configuration,
                type,
                state
            );
    
            sb.AppendLine(delegateTypedefCode);
        }

        if (writeTypeExtension) {
            string swiftTypeName = type.GetTypeDescriptor(typeDescriptorRegistry)
                .GetTypeName(CodeLanguage.Swift, false);

            string extensionDecl = Builder.Extension(swiftTypeName)
                .ToString();

            sb.AppendLine($"{extensionDecl} {{");
        }

        if (writeMembers) {
            string membersCode = WriteMembers(
                configuration,
                type,
                state,
                writeTypeDefinition
            );

            if (writeTypeExtension) {
                membersCode = membersCode.IndentAllLines(1);
            }
                
            sb.AppendLine(membersCode);
        }
        
        if (writeTypeExtension) {
            sb.AppendLine("}");
        }
        
        return sb.ToString();
    }

    #region Enum
    private string WriteEnumDef(
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        SwiftCodeBuilder sb = new();

        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string rawSwiftTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        string cEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        string swiftEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        
        bool isFlagsEnum = type.IsDefined(typeof(FlagsAttribute), false);
        
        var typeDocumentationComment = type.GetDocumentation()
            ?.GetFormattedDocumentationComment();

        if (isFlagsEnum) {
            string structDecl = Builder.Struct(swiftEnumTypeName)
                .ProtocolConformance("OptionSet")
                .Public()
                .ToString();
            
            if (!string.IsNullOrEmpty(typeDocumentationComment)) {
                sb.AppendLine(typeDocumentationComment);
            }
            
            sb.AppendLine($"{structDecl} {{");

            const string rawValueTypeAliasVarName = "RawValue";

            string rawValueTypeAliasDecl = Builder.TypeAlias(rawValueTypeAliasVarName, rawSwiftTypeName)
                .Public()
                .ToIndentedString(1);
            
            sb.AppendLine(rawValueTypeAliasDecl);
            
            sb.AppendLine(Builder.Let("rawValue")
                .Public()
                .TypeName(rawValueTypeAliasVarName)
                .ToIndentedString(1));
            
            sb.AppendLine();

            string rawValueParam = Builder.FuncSignatureParameter("rawValue", rawValueTypeAliasVarName)
                .ToString();

            string initRawValueDecl = Builder.Initializer()
                .Public()
                .Parameters(rawValueParam)
                .Implementation("self.rawValue = rawValue")
                .ToIndentedString(1);
            
            sb.AppendLine(initRawValueDecl);
            sb.AppendLine();
        } else {
            string enumDecl = Builder.Enum(swiftEnumTypeName)
                .Public()
                .RawTypeName(rawSwiftTypeName)
                .ToString();
            
            if (!string.IsNullOrEmpty(typeDocumentationComment)) {
                sb.AppendLine(typeDocumentationComment);
            }
            
            sb.AppendLine($"{enumDecl} {{");
        }

        string initUnwrap = isFlagsEnum 
            ? string.Empty
            : "!";

        string cValueParam = Builder.FuncSignatureParameter("cValue", cEnumTypeName)
            .ToString();

        string initDecl = Builder.Initializer()
            .Parameters(cValueParam)
            .Implementation($"self.init(rawValue: cValue.rawValue){initUnwrap}")
            .ToIndentedString(1);

        sb.AppendLine(initDecl);
        sb.AppendLine();

        string cValuePropDecl = Builder.GetOnlyProperty("cValue", cEnumTypeName)
            .Implementation($"{cEnumTypeName}(rawValue: rawValue){initUnwrap}")
            .ToIndentedString(1);

        sb.AppendLine(cValuePropDecl);
        sb.AppendLine();
        
        var caseNames = type.GetEnumNames();
        var values = type.GetEnumValuesAsUnderlyingType() ?? throw new Exception("No enum values");

        if (caseNames.Length != values.Length) {
            throw new Exception("The number of case names in an enum must match the number of values");
        }

        Dictionary<object, string> valueToNameMapping = new();

        for (int i = 0; i < values.Length; i++) {
            object value = values.GetValue(i) ?? throw new ArgumentNullException(nameof(value));
            string name = caseNames[i];

            if (valueToNameMapping.ContainsKey(value)) {
                continue;
            }

            valueToNameMapping[value] = name;
        }

        Dictionary<string, string> duplicateCases = new();
        List<object> uniqueValues = new();

        for (int i = 0; i < values.Length; i++) {
            object value = values.GetValue(i) ?? throw new ArgumentNullException(nameof(value));
            string name = caseNames[i];

            if (uniqueValues.Contains(value)) {
                string originalName = valueToNameMapping[value];

                duplicateCases[name] = originalName;
            } else {
                uniqueValues.Add(value);
            }
        }

        List<string> enumCases = new();

        for (int i = 0; i < caseNames.Length; i++) {
            string caseName = caseNames[i];
            string swiftCaseName = caseName.ToSwiftEnumCaseName();

            var value = values.GetValue(i) ?? throw new Exception("No enum value for case");

            string caseCode;

            if (isFlagsEnum) {
                string caseCodeDecl = Builder.Let(swiftCaseName)
                    .Public()
                    .Static()
                    .Value(swiftEnumTypeName)
                    .ToString();
                
                if (value.Equals(0) ||
                    value.Equals((uint)0) ||
                    value.Equals((byte)0) ||
                    value.Equals((short)0) ||
                    value.Equals((ushort)0) ||
                    value.Equals((long)0) ||
                    value.Equals((ulong)0)) {
                    caseCode = $"{caseCodeDecl}([])";
                } else {
                    caseCode = $"{caseCodeDecl}(rawValue: {value})";
                }
            } else {
                if (duplicateCases.TryGetValue(caseName, out string? caseNameWithEquivalentValue)) {
                    string swiftCaseNameWithEquivalentValue = caseNameWithEquivalentValue.ToSwiftEnumCaseName();
                    
                    string caseCodeDecl = Builder.Let(swiftCaseName)
                        .Public()
                        .Static()
                        .Value($"{swiftEnumTypeName}.{swiftCaseNameWithEquivalentValue}")
                        .ToString();
                    
                    caseCode = caseCodeDecl;
                } else {
                    caseCode = $"case {swiftCaseName} = {value}";
                }
            }
            
            enumCases.Add($"\t{caseCode}");
        }

        string enumCasesString = string.Join('\n', enumCases);

        sb.AppendLine(enumCasesString);
        sb.AppendLine("}");
        
        return sb.ToString();
    }
    #endregion Enum

    #region Members
    public string WriteMembers(
        ISyntaxWriterConfiguration? configuration,
        Type type,
        State state,
        bool writeTypeDefinition
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");

        if (type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate Swift code for those kinds of types

            return string.Empty;
        }
        
        var interfaceGenerationPhase = (configuration as SwiftSyntaxWriterConfiguration)?.InterfaceGenerationPhase ?? SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.NoInterface;

        // bool isAbstract = type.IsAbstract;
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        // var cMembers = cResult.GeneratedTypes[type];

        bool isInterface = type.IsInterface;
        bool isPrimitive = type.IsPrimitive;
        bool isArray = type.IsArray;
        
        SwiftCodeBuilder sb = new();

        string typeName = type.Name;
        string fullTypeName = type.GetFullNameOrName();

        string swiftTypeName;
        
        if (isPrimitive) {
            swiftTypeName = type.CTypeName();            
        } else {
            TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
            swiftTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
        }

        string? arrayMutableCollectionExtension = null;

        if (writeTypeDefinition) {
            Type? baseType = type.BaseType;
            TypeDescriptor? baseTypeDescriptor = baseType?.GetTypeDescriptor(typeDescriptorRegistry);

            string swiftBaseTypeName = baseTypeDescriptor?.GetTypeName(CodeLanguage.Swift, false)
                                       ?? "DNObject";

            List<Type> interfaceTypes = new();

            if (isInterface &&
                interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
                interfaceTypes.Add(type);
            }
            
            interfaceTypes.AddRange(type.GetInterfaces());
            
            List<string> swiftProtocolTypeNames = new();

            foreach (var interfaceType in interfaceTypes) {
                if (!cSharpUnmanagedResult.GeneratedTypes.ContainsKey(interfaceType) ||
                    !cResult.GeneratedTypes.ContainsKey(interfaceType)) {
                    continue;
                }

                if (!type.IsInterface) {
                    if (type.DoesAnyBaseTypeImplementInterface(interfaceType)) {
                        continue;
                    }
                }
                
                TypeDescriptor interfaceTypeDescriptor = interfaceType.GetTypeDescriptor(typeDescriptorRegistry);

                string swiftProtocolTypeName = interfaceTypeDescriptor.GetTypeName(
                    CodeLanguage.Swift, 
                    false
                );
                
                swiftProtocolTypeNames.Add(swiftProtocolTypeName);
            }

            string protocolConformancesString = string.Join(", ", swiftProtocolTypeNames);

            string typeDecl;

            if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol) {
                string protocolName = $"{swiftTypeName}";
                string fullProtocolName = $"{protocolName} /* {fullTypeName} */";

                typeDecl = Builder.Protocol(fullProtocolName)
                    .BaseTypeName("DNObject")
                    .ProtocolConformance(protocolConformancesString)
                    .Public()
                    .ToString();
            } else if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
                string fullSwiftTypeName = $"{swiftTypeName}{TypeDescriptor.SwiftDotNETInterfaceImplementationSuffix} /* {fullTypeName} */";
                
                typeDecl = Builder.Class(fullSwiftTypeName)
                    .BaseTypeName(swiftBaseTypeName)
                    .ProtocolConformance(protocolConformancesString)
                    .Public()
                    .ToString();
            } else if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) {
                string fullSwiftTypeName = $"{swiftTypeName} /* {fullTypeName} */";

                typeDecl = Builder.Extension(fullSwiftTypeName)
                    .ToString();
            } else {
                string fullSwiftTypeName = $"{swiftTypeName} /* {fullTypeName} */";
                
                typeDecl = Builder.Class(fullSwiftTypeName)
                    .BaseTypeName(swiftBaseTypeName)
                    .ProtocolConformance(protocolConformancesString)
                    .Public()
                    .ToString();
            }
            
            var typeDocumentationComment = type.GetDocumentation()
                ?.GetFormattedDocumentationComment();
            
            if (!string.IsNullOrEmpty(typeDocumentationComment)) {
                sb.AppendLine(typeDocumentationComment);
            }

            sb.AppendLine($"{typeDecl} {{");

            if (interfaceGenerationPhase != SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol &&
                interfaceGenerationPhase != SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) {
                string typeNameDecl = Builder.GetOnlyProperty("typeName", "String")
                    .Public()
                    .Class()
                    .Override()
                    .Implementation($"\"{typeName}\"")
                    .ToIndentedString(1);
    
                string fullTypeNameDecl = Builder.GetOnlyProperty("fullTypeName", "String")
                    .Public()
                    .Class()
                    .Override()
                    .Implementation($"\"{fullTypeName}\"")
                    .ToIndentedString(1);
                
                sb.AppendLine(typeNameDecl);
                sb.AppendLine();
                
                sb.AppendLine(fullTypeNameDecl);
                sb.AppendLine();
                
                if (isArray) {
                    var elementType = type.GetElementType();

                    if (elementType is not null) {
                        string swiftElementTypeName;
        
                        if (elementType.IsPrimitive) {
                            swiftElementTypeName = elementType.CTypeName();            
                        } else {
                            TypeDescriptor typeDescriptor = elementType.GetTypeDescriptor(typeDescriptorRegistry);
                            swiftElementTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
                        }
                        
                        string elementTypeDecl = Builder.GetOnlyProperty("elementType", "System_Type")
                            .Public()
                            .Class()
                            .Implementation($"{swiftElementTypeName}.typeOf")
                            .ToIndentedString(1);
                        
                        sb.AppendLine("/// The element type of the System.Array".IndentAllLines(1));
                        sb.AppendLine(elementTypeDecl);
                        sb.AppendLine();
                        
                        string emptyArrayInitializerImpl = $$"""
let elementType = {{swiftTypeName}}.elementType
let elementTypeC = elementType.__handle

var __exceptionC: System_Exception_t?

let newArrayC = System_Array_CreateInstance(elementTypeC, 0, &__exceptionC)

if let __exceptionC {
    let __exception = System_Exception(handle: __exceptionC)
    let __error = __exception.error
    
    throw __error
}

self.init(handle: newArrayC)
""";
                        
                        string emptyArrayInitializer = Builder.Initializer()
                            .Public()
                            .Convenience()
                            .Throws()
                            .Implementation(emptyArrayInitializerImpl)
                            .ToIndentedString(1);

                        sb.AppendLine($"/// Creates an empty {type.GetFullNameOrName()}".IndentAllLines(1));
                        sb.AppendLine(emptyArrayInitializer);
                        sb.AppendLine();
                        
                        string arrayInitializerImpl = $$"""
 let elementType = {{swiftTypeName}}.elementType
 let elementTypeC = elementType.__handle

 var __exceptionC: System_Exception_t?
 
 let newArrayC = System_Array_CreateInstance(elementTypeC, length, &__exceptionC)
 
 if let __exceptionC {
     let __exception = System_Exception(handle: __exceptionC)
     let __error = __exception.error
     
     throw __error
 }

 self.init(handle: newArrayC)
 """;
                        
                        string arrayInitializer = Builder.Initializer()
                            .Public()
                            .Convenience()
                            .Throws()
                            .Parameters("length: Int32")
                            .Implementation(arrayInitializerImpl)
                            .ToIndentedString(1);

                        sb.AppendLine($"/// Creates an {type.GetFullNameOrName()} with the specified length".IndentAllLines(1));
                        sb.AppendLine(arrayInitializer);
                        sb.AppendLine();
                        
                        string arrayMutableCollectionConformanceImpl = $$"""
public typealias Element = {{swiftElementTypeName}}?

public subscript(position: Index) -> Element {
    get {
        assert(position >= startIndex && position < endIndex, "Out of bounds")
        
        do {
            guard let element = try self.getValue(position) else {
                return nil
            }
            
            return try element.castTo()
        } catch {
            fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
        }
    }
    set {
        assert(position >= startIndex && position < endIndex, "Out of bounds")

        do {
            try self.setValue(newValue, position)
        } catch {
            fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
        }
    }
}
""";

                        arrayMutableCollectionExtension = Builder.Extension(swiftTypeName)
                            .ProtocolConformance("MutableCollection")
                            .Implementation(arrayMutableCollectionConformanceImpl)
                            .ToString();
                    }
                }
            }
        }

        HashSet<MemberInfo> generatedMembers = new();

        SwiftCodeBuilder sbMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            ISwiftSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sbMembers.AppendLine(Builder.SingleLineComment($"TODO: Unsupported Member Type \"{memberType}\"").ToString());
                }
                    
                continue;
            }

            object? target;

            if (syntaxWriter is IDestructorSyntaxWriter) {
                target = type;
            } else if (syntaxWriter is ITypeOfSyntaxWriter) {
                target = type;
            } else {
                target = member;
            }

            if (target == null) {
                throw new Exception("No target");
            }

            if ((interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) &&
                (syntaxWriter is IDestructorSyntaxWriter || syntaxWriter is ITypeOfSyntaxWriter)) {
                continue;
            }

            if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass &&
                syntaxWriter is not IDestructorSyntaxWriter &&
                syntaxWriter is not ITypeOfSyntaxWriter) {
                continue;
            }

            string memberCode = syntaxWriter.Write(
                target,
                state,
                configuration
            );

            sbMembers.AppendLine(memberCode);
            sbMembers.AppendLine();

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        string membersCode = sbMembers.ToString();

        if (writeTypeDefinition) {
            membersCode = membersCode.IndentAllLines(1);
        }
        
        sb.AppendLine(membersCode);

        if (writeTypeDefinition) {
            sb.AppendLine("}");
        }

        if (!string.IsNullOrEmpty(arrayMutableCollectionExtension)) {
            sb.AppendLine();
            sb.AppendLine(arrayMutableCollectionExtension);
        }

        return sb.ToString();
    }
    #endregion Members

    #region Type Extensions
    public string WriteTypeExtensionMethods(
        Type extendedType,
        List<GeneratedMember> generatedMembers
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;

        string? codeForOptional;
        
        if (!extendedType.IsEnum &&
            !extendedType.IsStruct()) {
            codeForOptional = GetTypeExtensionsCode(
                extendedType,
                true,
                generatedMembers,
                typeDescriptorRegistry
            );
        } else {
            codeForOptional = null;
        }
            
        string codeForNonOptional = GetTypeExtensionsCode(
            extendedType,
            false,
            generatedMembers,
            typeDescriptorRegistry
        );

        SwiftCodeBuilder sb = new();

        if (codeForOptional is not null) {
            sb.AppendLine(codeForOptional);
            sb.AppendLine();
        }
        
        sb.AppendLine(codeForNonOptional);

        string code = sb.ToString();

        return code;
    }
    
    private string GetTypeExtensionsCode(
        Type extendedType,
        bool isExtendedTypeOptional,
        List<GeneratedMember> generatedMembers,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        if (generatedMembers.Count <= 0) {
            return string.Empty;
        }
        
        TypeDescriptor extendedTypeDescriptor = extendedType.GetTypeDescriptor(typeDescriptorRegistry);
        string extendedTypeSwiftName = extendedTypeDescriptor.GetTypeName(CodeLanguage.Swift, false);

        string extendedTypeOptionality = isExtendedTypeOptional
            ? "?"
            : string.Empty;

        SwiftCodeBuilder sbMembers = new();
        
        foreach (GeneratedMember swiftGeneratedMember in generatedMembers) {
            string extensionMethod = SwiftMethodSyntaxWriter.WriteExtensionMethod(
                swiftGeneratedMember,
                isExtendedTypeOptional,
                typeDescriptorRegistry
            );

            sbMembers.AppendLine(extensionMethod);
            sbMembers.AppendLine();
        }
        
        string code = Builder.Extension($"{extendedTypeSwiftName}{extendedTypeOptionality}")
            .Implementation(sbMembers.ToString())
            .ToString();
        
        return code;
    }
    #endregion Type Extensions

    #region Syntax Writers
    private ISwiftSyntaxWriter? GetSyntaxWriter(
        MemberKind memberKind,
        MemberTypes memberType
    )
    {
        if (memberKind == MemberKind.Destructor) {
            return m_destructorSyntaxWriter;
        } else if (memberKind == MemberKind.TypeOf) {
            return m_typeOfSyntaxWriter;
        }

        m_syntaxWriters.TryGetValue(
            memberType,
            out ISwiftSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
    #endregion Syntax Writers
}