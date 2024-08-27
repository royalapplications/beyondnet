using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Kotlin;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;
using Beyond.NET.CodeGenerator.Types;
using Beyond.NET.Core;

using Settings = Beyond.NET.CodeGenerator.Generator.Kotlin.Settings;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

public class KotlinTypeSyntaxWriter: IKotlinSyntaxWriter, ITypeSyntaxWriter
{
    public const string JNA_CLASS_NAME = "CAPI";
    
    public Settings Settings { get; }
    
    private readonly Dictionary<MemberTypes, IKotlinSyntaxWriter> m_syntaxWriters = new() {
        { MemberTypes.Constructor, new KotlinConstructorSyntaxWriter() },
        { MemberTypes.Property, new KotlinPropertySyntaxWriter() },
        { MemberTypes.Method, new KotlinMethodSyntaxWriter() },
        { MemberTypes.Field, new KotlinFieldSyntaxWriter() },
        { MemberTypes.Event, new KotlinEventSyntaxWriter() }
    };
    
    private KotlinDestructorSyntaxWriter m_destructorSyntaxWriter = new();
    private KotlinTypeOfSyntaxWriter m_typeOfSyntaxWriter = new();
    
    public KotlinTypeSyntaxWriter(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((Type)@object, state, configuration);
    }

    public string Write(
        Type type,
        State state,
        ISyntaxWriterConfiguration? configuration
    )
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        var kotlinConfiguration = (configuration as KotlinSyntaxWriterConfiguration)!; 
        var generationPhase = kotlinConfiguration.GenerationPhase;
        
        if (state.CSharpUnmanagedResult is null) {
            throw new Exception("No CSharpUnmanagedResult provided");
        }

        if (state.CResult is null) {
            throw new Exception("No CResult provided");
        }

        string? fullTypeName = type.FullName;

        if (fullTypeName == null) {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It has no full name.").ToString();
        }

        // TODO
        if (type.IsGenericInAnyWay(true)) {
            return Builder.SingleLineComment($"Type \"{type.Name}\" was skipped. Reason: It is generic somehow.").ToString();
        }
        
        KotlinCodeBuilder sb = new();

        string typeCode;
        
        if (generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.KotlinBindings) {
            if (type.IsEnum) {
                typeCode = WriteEnumType(
                    type,
                    typeDescriptorRegistry
                );   
            } else if (ExperimentalFeatureFlags.EnableKotlinTypeGenerator) {
                typeCode = WriteKotlinType(
                    type,
                    state,
                    kotlinConfiguration
                );
            } else {
                typeCode = Builder.SingleLineComment("TODO: ENABLE_EXPERIMENTAL_KOTLIN_TYPE_GENERATOR is false").ToString();
            }
        } else if (generationPhase == KotlinSyntaxWriterConfiguration.GenerationPhases.JNA) {
            typeCode = WriteJnaType(
                type,
                state,
                kotlinConfiguration
            );
        } else {
            throw new Exception($"Unknown generation phase: {generationPhase}");
        }
        
        sb.AppendLine(typeCode);
        
        return sb.ToString();
    }

    #region Enum
    private string WriteEnumType(
        Type type,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
        
        // string cEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.C, false);
        string kotlinEnumTypeName = typeDescriptor.GetTypeName(CodeLanguage.Kotlin, false);

        Type underlyingType = type.GetEnumUnderlyingType();
        TypeDescriptor underlyingTypeDescriptor = underlyingType.GetTypeDescriptor(typeDescriptorRegistry);

        string underlyingTypeName = underlyingTypeDescriptor.GetTypeName(CodeLanguage.KotlinJNA, false);
        
        // bool isFlagsEnum = type.IsDefined(typeof(FlagsAttribute), false);
        
        var caseNames = type.GetEnumNames();
        var values = type.GetEnumValuesAsUnderlyingType() ?? throw new Exception("No enum values");

        if (caseNames.Length != values.Length) {
            throw new Exception("The number of case names in an enum must match the number of values");
        }

        List<KotlinEnumClassCase> enumCases = new();

        for (int i = 0; i < caseNames.Length; i++) {
            string caseName = caseNames[i];
            var value = values.GetValue(i) ?? throw new Exception("No enum value for case");
            var valueType = value.GetType();

            if (valueType == typeof(int) ||
                valueType == typeof(sbyte) ||
                valueType == typeof(byte) ||
                valueType == typeof(ushort) ||
                valueType == typeof(uint) ||
                valueType == typeof(ulong)) {
                enumCases.Add(new KotlinEnumClassCase(caseName, value.ToString()!));
            // else if (valueType == typeof(byte) ||
            //          valueType == typeof(ushort) ||
            //          valueType == typeof(uint) ||
            //          valueType == typeof(ulong))
            //     enumCases.Add($"{caseName}({value}u)");
            } else if (valueType == typeof(double)) {
                enumCases.Add(new KotlinEnumClassCase(caseName, $"{value}.toDouble()"));
            } else if (valueType == typeof(float)) {
                enumCases.Add(new KotlinEnumClassCase(caseName, $"{value}.toFloat()"));
            } else {
                throw new Exception($"Unknown underlying enum type: {underlyingType}");
            }
        }

        enumCases.Add(new("DN_CUSTOM", $"{underlyingTypeName}.MAX_VALUE"));

        string enumCasesString = KotlinEnumClassCase.CasesToString(enumCases);

        var additionalEnumCode = $$"""
companion object {
    public operator fun invoke(underlyingValue: {{underlyingTypeName}}): {{kotlinEnumTypeName}} {
        val foundCase = {{kotlinEnumTypeName}}.entries.firstOrNull {
            it.rawValue == underlyingValue
        }

        foundCase?.let {
            return foundCase
        }

        return DN_CUSTOM.apply {
            _dnCustomValue = underlyingValue
        }
    }
}

infix fun or(other: {{kotlinEnumTypeName}}): {{kotlinEnumTypeName}} {
    val newRawValue = this.value or other.value
    val customEnumCase = {{kotlinEnumTypeName}}.invoke(newRawValue)

    return customEnumCase
}

private var _dnCustomValue: {{underlyingTypeName}}? = null

public val value: {{underlyingTypeName}}
    get() {
        val dnCustomValue = _dnCustomValue

        dnCustomValue?.let {
            return dnCustomValue
        }

        return rawValue
    }
""";

        var implementation = $"{enumCasesString}\n\n{additionalEnumCode}";

        var enumClassDef = new KotlinEnumClassDeclaration(
            kotlinEnumTypeName,
            KotlinVisibilities.Public,
            underlyingTypeName,
            implementation
        );

        var enumClassDefStr = enumClassDef.ToString();

        return enumClassDefStr;
    }
    #endregion Enum

    #region JNA
    private string WriteJnaType(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration
    )
    {
        return WriteJnaMembers(
            type,
            state,
            configuration
        );
    }

    private string WriteJnaMembers(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration
    )
    {
        // TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        // Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate Kotlin code for those kinds of types

            return string.Empty;
        }
        
        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        // var cMembers = cResult.GeneratedTypes[type];
        
        HashSet<MemberInfo> generatedMembers = new();
        
        KotlinCodeBuilder sbMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            IKotlinSyntaxWriter? syntaxWriter = GetSyntaxWriter(
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

            // if ((interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) &&
            //     (syntaxWriter is IDestructorSyntaxWriter || syntaxWriter is ITypeOfSyntaxWriter)) {
            //     continue;
            // }
            //
            // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass &&
            //     syntaxWriter is not IDestructorSyntaxWriter &&
            //     syntaxWriter is not ITypeOfSyntaxWriter) {
            //     continue;
            // }

            string memberCode = syntaxWriter.Write(
                target,
                state,
                configuration
            );

            sbMembers.AppendLine(memberCode);

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        string membersCode = sbMembers.ToString()
            .IndentAllLines(1);

        return membersCode;
    }
    #endregion JNA

    #region Kotlin
    private string WriteKotlinType(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration
    )
    {
        return WriteKotlinMembers(
            type,
            state,
            configuration,
            true
        );
    }

    private string WriteKotlinMembers(
        Type type,
        State state,
        KotlinSyntaxWriterConfiguration configuration,
        bool writeTypeDefinition
    )
    {
        // TODO: This was copied from the Swift version of the same method and modified
        
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        // Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        if (type.IsPointer ||
            type.IsByRef ||
            type.IsGenericParameter ||
            type.IsGenericMethodParameter ||
            type.IsGenericTypeParameter ||
            type.IsConstructedGenericType) {
            // No need to generate Kotlin code for those kinds of types

            return string.Empty;
        }

        var cSharpMembers = cSharpUnmanagedResult.GeneratedTypes[type];
        // var cMembers = cResult.GeneratedTypes[type];
        
        // bool isInterface = type.IsInterface;
        bool isPrimitive = type.IsPrimitive;
        // bool isArray = type.IsArray;
        
        KotlinCodeBuilder sb = new();

        // string typeName = type.Name;
        string fullTypeName = type.GetFullNameOrName();

        string kotlinTypeName;
        
        if (isPrimitive) {
            kotlinTypeName = type.CTypeName();
        } else {
            TypeDescriptor typeDescriptor = type.GetTypeDescriptor(typeDescriptorRegistry);
            kotlinTypeName = typeDescriptor.GetTypeName(CodeLanguage.Kotlin, false);
        }

        string? arrayMutableCollectionExtension = null;

        if (writeTypeDefinition) {
            Type? baseType = type.BaseType;
            TypeDescriptor? baseTypeDescriptor = baseType?.GetTypeDescriptor(typeDescriptorRegistry);

            string kotlinBaseTypeName = baseTypeDescriptor?.GetTypeName(CodeLanguage.Kotlin, false)
                                       ?? "DNObject";

            // TODO: Interfaces
            // List<Type> interfaceTypes = new();
            // 
            // if (isInterface &&
            //     interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
            //     interfaceTypes.Add(type);
            // }
            //
            // interfaceTypes.AddRange(type.GetInterfaces());
            //
            // List<string> kotlinInterfaceTypeNames = new();
            //
            // foreach (var interfaceType in interfaceTypes) {
            //     if (!cSharpUnmanagedResult.GeneratedTypes.ContainsKey(interfaceType) ||
            //         !cResult.GeneratedTypes.ContainsKey(interfaceType)) {
            //         continue;
            //     }
            //
            //     if (!type.IsInterface) {
            //         if (type.DoesAnyBaseTypeImplementInterface(interfaceType)) {
            //             continue;
            //         }
            //     }
            //     
            //     TypeDescriptor interfaceTypeDescriptor = interfaceType.GetTypeDescriptor(typeDescriptorRegistry);
            //
            //     string swiftProtocolTypeName = interfaceTypeDescriptor.GetTypeName(
            //         CodeLanguage.Swift, 
            //         false
            //     );
            //     
            //     kotlinInterfaceTypeNames.Add(swiftProtocolTypeName);
            // }
            //
            // string protocolConformancesString = string.Join(", ", kotlinInterfaceTypeNames);
            string interfaceConformancesString = string.Empty;

            string typeDecl;

            // TODO: Interfaces
            // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol) {
            //     string protocolName = $"{kotlinTypeName}";
            //     string fullProtocolName = $"{protocolName} /* {fullTypeName} */";
            //
            //     typeDecl = Builder.Protocol(fullProtocolName)
            //         .BaseTypeName("DNObject")
            //         .ProtocolConformance(protocolConformancesString)
            //         .Public()
            //         .ToString();
            // } else if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
            //     string fullSwiftTypeName = $"{kotlinTypeName}{TypeDescriptor.SwiftDotNETInterfaceImplementationSuffix} /* {fullTypeName} */";
            //     
            //     typeDecl = Builder.Class(fullSwiftTypeName)
            //         .BaseTypeName(kotlinBaseTypeName)
            //         .ProtocolConformance(protocolConformancesString)
            //         .Public()
            //         .ToString();
            // } else if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) {
            //     string fullSwiftTypeName = $"{kotlinTypeName} /* {fullTypeName} */";
            //
            //     typeDecl = Builder.Extension(fullSwiftTypeName)
            //         .ToString();
            // } else {
                string fullKotlinTypeName = $"{kotlinTypeName} /* {fullTypeName} */";

                typeDecl = new KotlinClassDeclaration(
                    fullKotlinTypeName,
                    kotlinBaseTypeName,
                    interfaceConformancesString,
                    KotlinVisibilities.Open,
                    new KotlinFunSignatureParameters(new [] {
                        new KotlinFunSignatureParameter("handle", "Pointer")
                    }),
                    new [] {
                        "handle"
                    },
                    null
                ).ToString();
            // }
            
            var typeDocumentationComment = type.GetDocumentation()
                ?.GetFormattedDocumentationComment();
            
            if (!string.IsNullOrEmpty(typeDocumentationComment)) {
                sb.AppendLine(typeDocumentationComment);
            }

            sb.AppendLine($"{typeDecl} {{");

            // TODO: Interfaces
            // if (interfaceGenerationPhase != SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol &&
            //     interfaceGenerationPhase != SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) {
            // TODO: Mucho stuff
//                 string typeNameDecl = Builder.GetOnlyProperty("typeName", "String")
//                     .Public()
//                     .Class()
//                     .Override()
//                     .Implementation($"\"{typeName}\"")
//                     .ToIndentedString(1);
//     
//                 string fullTypeNameDecl = Builder.GetOnlyProperty("fullTypeName", "String")
//                     .Public()
//                     .Class()
//                     .Override()
//                     .Implementation($"\"{fullTypeName}\"")
//                     .ToIndentedString(1);
//                 
//                 sb.AppendLine(typeNameDecl);
//                 sb.AppendLine();
//                 
//                 sb.AppendLine(fullTypeNameDecl);
//                 sb.AppendLine();
//                 
//                 if (isArray) {
//                     var elementType = type.GetElementType();
//
//                     if (elementType is not null) {
//                         string swiftElementTypeName;
//         
//                         if (elementType.IsPrimitive) {
//                             swiftElementTypeName = elementType.CTypeName();            
//                         } else {
//                             TypeDescriptor typeDescriptor = elementType.GetTypeDescriptor(typeDescriptorRegistry);
//                             swiftElementTypeName = typeDescriptor.GetTypeName(CodeLanguage.Swift, false);
//                         }
//                         
//                         string elementTypeDecl = Builder.GetOnlyProperty("elementType", "System_Type")
//                             .Public()
//                             .Class()
//                             .Implementation($"{swiftElementTypeName}.typeOf")
//                             .ToIndentedString(1);
//                         
//                         sb.AppendLine("/// The element type of the System.Array".IndentAllLines(1));
//                         sb.AppendLine(elementTypeDecl);
//                         sb.AppendLine();
//                         
//                         string emptyArrayInitializerImpl = $$"""
// let elementType = {{kotlinTypeName}}.elementType
// let elementTypeC = elementType.__handle
//
// var __exceptionC: System_Exception_t?
//
// let newArrayC = System_Array_CreateInstance(elementTypeC, 0, &__exceptionC)
//
// if let __exceptionC {
//     let __exception = System_Exception(handle: __exceptionC)
//     let __error = __exception.error
//     
//     throw __error
// }
//
// self.init(handle: newArrayC)
// """;
//                         
//                         string emptyArrayInitializer = Builder.Initializer()
//                             .Public()
//                             .Convenience()
//                             .Throws()
//                             .Implementation(emptyArrayInitializerImpl)
//                             .ToIndentedString(1);
//
//                         sb.AppendLine($"/// Creates an empty {type.GetFullNameOrName()}".IndentAllLines(1));
//                         sb.AppendLine(emptyArrayInitializer);
//                         sb.AppendLine();
//                         
//                         string arrayInitializerImpl = $$"""
//  let elementType = {{kotlinTypeName}}.elementType
//  let elementTypeC = elementType.__handle
//
//  var __exceptionC: System_Exception_t?
//  
//  let newArrayC = System_Array_CreateInstance(elementTypeC, length, &__exceptionC)
//  
//  if let __exceptionC {
//      let __exception = System_Exception(handle: __exceptionC)
//      let __error = __exception.error
//      
//      throw __error
//  }
//
//  self.init(handle: newArrayC)
//  """;
//                         
//                         string arrayInitializer = Builder.Initializer()
//                             .Public()
//                             .Convenience()
//                             .Throws()
//                             .Parameters("length: Int32")
//                             .Implementation(arrayInitializerImpl)
//                             .ToIndentedString(1);
//
//                         sb.AppendLine($"/// Creates an {type.GetFullNameOrName()} with the specified length".IndentAllLines(1));
//                         sb.AppendLine(arrayInitializer);
//                         sb.AppendLine();
//                         
//                         string arrayMutableCollectionConformanceImpl = $$"""
// public typealias Element = {{swiftElementTypeName}}?
//
// public subscript(position: Index) -> Element {
//     get {
//         assert(position >= startIndex && position < endIndex, "Out of bounds")
//         
//         do {
//             guard let element = try self.getValue(position) else {
//                 return nil
//             }
//             
//             return try element.castTo()
//         } catch {
//             fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
//         }
//     }
//     set {
//         assert(position >= startIndex && position < endIndex, "Out of bounds")
//
//         do {
//             try self.setValue(newValue, position)
//         } catch {
//             fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
//         }
//     }
// }
// """;
//
//                         arrayMutableCollectionExtension = Builder.Extension(kotlinTypeName)
//                             .ProtocolConformance("MutableCollection")
//                             .Implementation(arrayMutableCollectionConformanceImpl)
//                             .ToString();
//                     }
//                 }
//             }
        // TODO: Interfaces
        }

        HashSet<MemberInfo> generatedMembers = new();

        KotlinCodeBuilder sbInstanceMembers = new();
        KotlinCodeBuilder sbStaticMembers = new();

        foreach (var cSharpMember in cSharpMembers) {
            var member = cSharpMember.Member;

            if (member is not null &&
                generatedMembers.Contains(member)) {
                continue;
            }
            
            var memberKind = cSharpMember.MemberKind;
            var memberType = member?.MemberType;

            IKotlinSyntaxWriter? syntaxWriter = GetSyntaxWriter(
                memberKind,
                memberType ?? MemberTypes.Custom
            );
            
            if (syntaxWriter == null) {
                if (Settings.EmitUnsupported) {
                    sbInstanceMembers.AppendLine(Builder.SingleLineComment($"TODO: Unsupported Member Type \"{memberType}\"").ToString());
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

            // TODO: Interfaces
            // if ((interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations) &&
            //     (syntaxWriter is IDestructorSyntaxWriter || syntaxWriter is ITypeOfSyntaxWriter)) {
            //     continue;
            // }

            // TODO: Interfaces
            // if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass &&
            //     syntaxWriter is not IDestructorSyntaxWriter &&
            //     syntaxWriter is not ITypeOfSyntaxWriter) {
            //     continue;
            // }

            string memberCode = syntaxWriter.Write(
                target,
                state,
                configuration
            );
            
            bool isStatic = cSharpMember.MemberKind == MemberKind.TypeOf || 
                            cSharpMember.MemberKind == MemberKind.Constructor ||
                            (cSharpMember.Member?.IsStatic() ?? false);

            var sbMembers = isStatic 
                ? sbStaticMembers
                : sbInstanceMembers;

            sbMembers.AppendLine(memberCode);

            if (member is not null) {
                generatedMembers.Add(member);
            }
        }

        var instanceMembersCode = sbInstanceMembers.ToString();
        var staticMembersCode = sbStaticMembers.ToString();

        string companionObjectCode;
        
        if (!string.IsNullOrEmpty(staticMembersCode)) {
            companionObjectCode = "companion object {\n" + staticMembersCode.IndentAllLines(1) + "\n}";
        } else {
            companionObjectCode = string.Empty;
        }
        
        string membersCode = companionObjectCode + "\n\n" + instanceMembersCode;

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
    #endregion Kotlin

    #region Syntax Writers
    private IKotlinSyntaxWriter? GetSyntaxWriter(
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
            out IKotlinSyntaxWriter? syntaxWriter
        );

        return syntaxWriter;
    }
    #endregion Syntax Writers
}