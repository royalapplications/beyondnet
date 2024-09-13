using System.Reflection;

using Beyond.NET.CodeGenerator.Collectors;
using Beyond.NET.CodeGenerator.Extensions;
using Beyond.NET.CodeGenerator.Generator;
using Beyond.NET.CodeGenerator.Generator.Swift;
using Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;
using Beyond.NET.CodeGenerator.Types;

namespace Beyond.NET.CodeGenerator.Syntax.Swift;

public class SwiftMethodSyntaxWriter: ISwiftSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state, ISyntaxWriterConfiguration? configuration)
    {
        return Write((MethodInfo)@object, state, configuration);
    }

    public string Write(MethodInfo method, State state, ISyntaxWriterConfiguration? configuration)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        Result cSharpUnmanagedResult = state.CSharpUnmanagedResult ?? throw new Exception("No CSharpUnmanagedResult provided");
        Result cResult = state.CResult ?? throw new Exception("No CResult provided");
        
        GeneratedMember cSharpGeneratedMember = cSharpUnmanagedResult.GetGeneratedMember(method) ?? throw new Exception("No C# generated member");
        GeneratedMember cGeneratedMember = cResult.GetGeneratedMember(method) ?? throw new Exception("No C generated member");

        bool mayThrow = cSharpGeneratedMember.MayThrow;
        const MemberKind methodKind = MemberKind.Method;

        bool isStaticMethod = method.IsStatic;

        Type declaringType = method.DeclaringType ?? throw new Exception("No declaring type");
        Type returnType = method.ReturnType;
        IEnumerable<ParameterInfo> parameters = method.GetParameters();

        string methodCode = WriteMethod(
            cSharpGeneratedMember,
            cGeneratedMember,
            method,
            methodKind,
            isStaticMethod,
            mayThrow,
            declaringType,
            returnType,
            parameters,
            configuration,
            true,
            typeDescriptorRegistry,
            state,
            method,
            out _
        );

        return methodCode;
    }

    protected string WriteMethod(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        IEnumerable<ParameterInfo> parameters,
        ISyntaxWriterConfiguration? syntaxWriterConfiguration,
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        MemberInfo? originatingMemberInfo,
        out string generatedName
    )
    {
        #region Preparation
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
        }

        #region TODO: Unsupported Stuff
        if (returnOrSetterOrEventHandlerType.IsByRef) {
            generatedName = string.Empty;
            
            return $"// TODO: Method with by ref return or setter or event handler type ({cMember.GetGeneratedName(CodeLanguage.C)})";
        }
        #endregion TODO: Unsupported Stuff

        var interfaceGenerationPhase = (syntaxWriterConfiguration as SwiftSyntaxWriterConfiguration)?.InterfaceGenerationPhase ?? SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.NoInterface;
        
        MethodBase? methodBase = memberInfo as MethodBase;
        MethodInfo? methodInfo = methodBase as MethodInfo;

        bool isGenericType = declaringType.IsGenericType ||
                             declaringType.IsGenericTypeDefinition;
        
        Type[] genericTypeArguments = Array.Empty<Type>();
        int numberOfGenericTypeArguments = 0;

        if (isGenericType) {
            genericTypeArguments = declaringType.GetGenericArguments();
            numberOfGenericTypeArguments = genericTypeArguments.Length;
        }
        
        bool isGeneric = false;
        Type[] genericMethodArguments = Array.Empty<Type>();
        int numberOfGenericMethodArguments = 0;

        if (methodBase is not null) {
            isGeneric = isGenericType ||
                        methodBase.IsGenericMethod ||
                        methodBase.IsConstructedGenericMethod ||
                        methodBase.IsGenericMethodDefinition ||
                        methodBase.ContainsGenericParameters;

            if (isGeneric) {
                try {
                    genericMethodArguments = methodBase.GetGenericArguments();
                } catch {
                    genericMethodArguments = Array.Empty<Type>();
                }
                
                numberOfGenericMethodArguments = genericMethodArguments.Length;
            }
        } else if (isGenericType &&
                   memberKind != MemberKind.Destructor &&
                   memberKind != MemberKind.TypeOf) {
            isGeneric = true;
        }

        if (isGenericType &&
            numberOfGenericTypeArguments <= 0) {
            throw new Exception("Generic Type without generic arguments");
        }

        if (isGeneric &&
            !isGenericType &&
            numberOfGenericMethodArguments <= 0) {
            throw new Exception("Generic Method without generic arguments");
        }

        List<Type> tempCombinedGenericArguments = new();
        
        tempCombinedGenericArguments.AddRange(genericTypeArguments);
        tempCombinedGenericArguments.AddRange(genericMethodArguments);

        Type[] combinedGenericArguments = tempCombinedGenericArguments.ToArray();

        if (isGeneric) {
            foreach (var parameter in parameters) {
                bool isOutOrInOrByRef = parameter.IsOut ||
                                        parameter.IsIn ||
                                        parameter.ParameterType.IsByRef;

                if (isOutOrInOrByRef) {
                    Type nonByRefParameterType = parameter.ParameterType.GetNonByRefType();

                    if (nonByRefParameterType.IsArray) {
                        generatedName = string.Empty;
                        
                        return "// TODO: Generic Methods with out/in/ref parameters that are arrays are not supported";    
                    }
                }
            }
        }
        
        string cMethodName = cSharpGeneratedMember.GetGeneratedName(CodeLanguage.CSharpUnmanaged) ?? throw new Exception("No native name");
        
        // string methodNameSwift = state.UniqueGeneratedName(
        //     memberKind.SwiftName(memberInfo),
        //     CodeLanguage.Swift
        // );

        string methodNameSwift = memberKind.SwiftName(memberInfo);

        bool treatAsOverridden = false;

        if (methodInfo is not null) {
            bool isActuallyOverridden = methodInfo.IsOverridden(out bool overrideNullabilityIsCompatible);

            if (isActuallyOverridden) {
                if (overrideNullabilityIsCompatible) {
                    treatAsOverridden = true;
                }
            } else {
                bool isShadowed = methodInfo.IsShadowed(
                    CodeLanguage.Swift,
                    out bool shadowNullabilityIsCompatible
                );

                if (isShadowed) {
                    if (shadowNullabilityIsCompatible) {
                        treatAsOverridden = true;
                    }
                }
            }
        } else if (memberInfo is FieldInfo fieldInfo) {
            bool isShadowed = fieldInfo.IsShadowed();

            if (isShadowed) {
                treatAsOverridden = true;
            }
        }

        if (addToState) {
            state.AddGeneratedMember(
                memberKind,
                memberInfo,
                mayThrow,
                methodNameSwift,
                CodeLanguage.Swift
            );
        }
        
        bool isGenericReturnType = returnOrSetterOrEventHandlerType.IsGenericParameter ||
                                   returnOrSetterOrEventHandlerType.IsGenericMethodParameter ||
                                   returnOrSetterOrEventHandlerType.IsGenericType ||
                                   returnOrSetterOrEventHandlerType.IsGenericTypeDefinition ||
                                   returnOrSetterOrEventHandlerType.IsGenericTypeParameter ||
                                   returnOrSetterOrEventHandlerType.IsConstructedGenericType;

        bool isConstructedGenericReturnType = returnOrSetterOrEventHandlerType.IsConstructedGenericType;
        
        bool isGenericArrayReturnType = false;

        if (!isGenericReturnType &&
            returnOrSetterOrEventHandlerType.IsArray) {
            Type? arrayElementType = returnOrSetterOrEventHandlerType.GetElementType();

            if (arrayElementType is not null) {
                bool isGenericArray = arrayElementType.IsGenericParameter ||
                                      arrayElementType.IsGenericMethodParameter ||
                                      arrayElementType.IsGenericType ||
                                      arrayElementType.IsGenericTypeDefinition ||
                                      arrayElementType.IsGenericTypeParameter ||
                                      arrayElementType.IsConstructedGenericType;

                isGenericReturnType = isGenericArray;
                isGenericArrayReturnType = isGenericArray;
            }
        }

        if (isGenericReturnType) {
            if (isGenericArrayReturnType) {
                returnOrSetterOrEventHandlerType = typeof(Array);
            } else if (!isConstructedGenericReturnType) {
                returnOrSetterOrEventHandlerType = typeof(object);
            }
        }

        bool returnOrSetterOrEventHandlerTypeIsByRef = returnOrSetterOrEventHandlerType.IsByRef;

        if (returnOrSetterOrEventHandlerTypeIsByRef) {
            returnOrSetterOrEventHandlerType = returnOrSetterOrEventHandlerType.GetNonByRefType();
        }
        
        TypeDescriptor returnOrSetterTypeDescriptor = returnOrSetterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
        
        var nullabilityInfoContext = new NullabilityInfoContext();
        var returnOrSetterTypeNullability = Nullability.NotSpecified;
        var returnOrSetterTypeArrayElementNullability = Nullability.NotSpecified; 
        
        if (memberKind == MemberKind.TypeOf) {
            returnOrSetterTypeNullability = Nullability.NonNullable;
        } else if (returnOrSetterOrEventHandlerType.IsNullableValueType(out _)) {
            returnOrSetterTypeNullability = Nullability.Nullable;
        } else if (returnOrSetterOrEventHandlerType.IsReferenceType() &&
                   !returnOrSetterOrEventHandlerType.IsByRefValueType(out bool nonByRefTypeIsStruct) &&
                   !nonByRefTypeIsStruct) {
            if (methodInfo is not null) {
                ParameterInfo returnOrSetterValueParameter;

                if (memberKind == MemberKind.PropertySetter ||
                    memberKind == MemberKind.EventHandlerAdder ||
                    memberKind == MemberKind.EventHandlerRemover) {
                    returnOrSetterValueParameter = methodInfo.GetParameters().LastOrDefault() ?? methodInfo.ReturnParameter;
                } else {
                    returnOrSetterValueParameter = methodInfo.ReturnParameter;
                }
                
                var nullabilityInfo = nullabilityInfoContext.Create(returnOrSetterValueParameter);

                if (memberKind == MemberKind.PropertyGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.PropertySetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.EventHandlerAdder ||
                           memberKind == MemberKind.EventHandlerRemover) {
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                } else { // Method
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }

                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability = elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            } else if (memberInfo is ConstructorInfo) {
                // Constructors in C# are never expected to return null
                returnOrSetterTypeNullability = Nullability.NonNullable;
            } else if (memberInfo is FieldInfo fieldInfo) {
                var nullabilityInfo = nullabilityInfoContext.Create(fieldInfo);

                if (memberKind == MemberKind.FieldGetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.FieldSetter) {
                    returnOrSetterTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnOrSetterTypeArrayElementNullability = nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else { // Hmm, not sure this can ever happen
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnOrSetterTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                    
                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnOrSetterTypeArrayElementNullability = elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            }
        }
        
        // TODO: This generates inout TypeName if the return type is by ref
        string swiftReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(
            CodeLanguage.Swift,
            true,
            returnOrSetterTypeNullability,
            returnOrSetterTypeArrayElementNullability,
            false,
            returnOrSetterOrEventHandlerTypeIsByRef,
            false
        );
        
        string swiftReturnOrSetterTypeNameWithComment;
        Type? setterType;
        
        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            swiftReturnOrSetterTypeNameWithComment = string.Empty;
            setterType = returnOrSetterOrEventHandlerType;
        } else {
            swiftReturnOrSetterTypeNameWithComment = $"{swiftReturnOrSetterTypeName} /* {returnOrSetterOrEventHandlerType.GetFullNameOrName()} */";
            setterType = null;
        }
        
        generatedName = methodNameSwift;
        #endregion Preparation

        #region Func Declaration
        string? memberImpl;

        bool needImpl;

        if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol ||
            interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ImplementationClass) {
            needImpl = memberKind == MemberKind.Destructor ||
                       memberKind == MemberKind.TypeOf;
        } else {
            needImpl = true;
        }
        
        if (!needImpl) {
            memberImpl = null;
        } else {
            memberImpl = WriteMethodImplementation(
                cSharpGeneratedMember,
                cMember,
                memberInfo,
                memberKind,
                cMethodName,
                isGeneric,
                isStaticMethod,
                mayThrow,
                declaringType,
                returnOrSetterOrEventHandlerType,
                returnOrSetterTypeNullability,
                returnOrSetterTypeArrayElementNullability,
                returnOrSetterTypeDescriptor,
                parameters,
                genericTypeArguments,
                genericMethodArguments,
                syntaxWriterConfiguration,
                typeDescriptorRegistry
            );   
        }
        
        string methodSignatureParameters = WriteParameters(
            memberKind,
            setterType,
            returnOrSetterTypeNullability,
            returnOrSetterTypeArrayElementNullability,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            false,
            false,
            typeDescriptorRegistry
        );

        string declaration;

        SwiftVisibilities memberVisibility;

        if (interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol) {
            memberVisibility = SwiftVisibilities.None;
        } else {
            if (memberKind == MemberKind.Destructor) {
                memberVisibility = SwiftVisibilities.Internal;
            } else {
                memberVisibility = SwiftVisibilities.Public;
            }
        }

        if (memberKind == MemberKind.Constructor) {
            declaration = Builder.Initializer()
                .Convenience()
                .Visibility(memberVisibility)
                .Parameters(methodSignatureParameters)
                .Throws(mayThrow)
                .Implementation(memberImpl)
                .ToString();
        } else if (memberKind == MemberKind.Destructor) {
            declaration = Builder.Func(methodNameSwift)
                .Visibility(memberVisibility)
                .Override()
                .Parameters(methodSignatureParameters)
                .Throws(mayThrow)
                .Implementation(memberImpl)
                .ToString();
        } else if (memberKind == MemberKind.TypeOf) {
            bool isEnum = declaringType.IsEnum;
            
            string propTypeName = !returnOrSetterOrEventHandlerType.IsVoid()
                ? swiftReturnOrSetterTypeNameWithComment
                : throw new Exception("A property must have a return type");

            declaration = Builder.GetOnlyProperty(methodNameSwift, propTypeName)
                .Visibility(memberVisibility)
                .TypeAttachmentKind(isEnum 
                    ? SwiftTypeAttachmentKinds.Static
                    : SwiftTypeAttachmentKinds.Class)
                .Override(!isEnum)
                .Throws(mayThrow)
                .Implementation(memberImpl)
                .ToString();
        } else if (CanBeGeneratedAsGetOnlyProperty(memberKind, isGeneric, parameters.Any())) {
            string propTypeName = !returnOrSetterOrEventHandlerType.IsVoid()
                ? swiftReturnOrSetterTypeNameWithComment
                : throw new Exception("A property must have a return type");

            declaration = Builder.GetOnlyProperty(methodNameSwift, propTypeName)
                .Visibility(memberVisibility)
                .TypeAttachmentKind(isStaticMethod
                    ? interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations ? SwiftTypeAttachmentKinds.Static : SwiftTypeAttachmentKinds.Class
                    : SwiftTypeAttachmentKinds.Instance)
                .Override(treatAsOverridden)
                .Throws(mayThrow)
                .Implementation(memberImpl)
                .ToString();
        } else {
            declaration = Builder.Func(methodNameSwift)
                .Visibility(memberVisibility)
                .TypeAttachmentKind(isStaticMethod 
                    ? interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.Protocol || interfaceGenerationPhase == SwiftSyntaxWriterConfiguration.InterfaceGenerationPhases.ProtocolExtensionForDefaultImplementations ? SwiftTypeAttachmentKinds.Static : SwiftTypeAttachmentKinds.Class
                    : SwiftTypeAttachmentKinds.Instance)
                .Override(treatAsOverridden)
                .Parameters(methodSignatureParameters)
                .Throws(mayThrow)
                .ReturnTypeName(!returnOrSetterOrEventHandlerType.IsVoid()
                    ? swiftReturnOrSetterTypeNameWithComment
                    : null)
                .Implementation(memberImpl)
                .ToString();
        }
        #endregion Func Declaration

        XmlDocumentationMember? xmlDocumentationContent;
        
        if (originatingMemberInfo is FieldInfo originatingFieldInfo) {
            xmlDocumentationContent = originatingFieldInfo.GetDocumentation();
        } else if (originatingMemberInfo is PropertyInfo originatingPropertyInfo) {
            xmlDocumentationContent = originatingPropertyInfo.GetDocumentation();
        } else if (originatingMemberInfo is EventInfo originatingEventInfo) {
            xmlDocumentationContent = originatingEventInfo.GetDocumentation();
        } else if (originatingMemberInfo is ParameterlessStructConstructorInfo) {
            xmlDocumentationContent = null;
        } else if (originatingMemberInfo is ConstructorInfo originatingConstructorInfo) {
            xmlDocumentationContent = originatingConstructorInfo.GetDocumentation();
        } else if (originatingMemberInfo is MethodInfo originatingMethodInfo) {
            xmlDocumentationContent = originatingMethodInfo.GetDocumentation();
        } else {
            xmlDocumentationContent = null;
        }
        
        var declarationComment = xmlDocumentationContent
            ?.GetFormattedDocumentationComment();

        if (declarationComment is null &&
            originatingMemberInfo is ParameterlessStructConstructorInfo) {
            declarationComment = $"/// Initializes a new instance of the {declaringType.GetFullNameOrName()} struct.";
        }

        string declarationWithComment;

        if (!string.IsNullOrEmpty(declarationComment)) {
            declarationWithComment = declarationComment + "\n" + declaration;
        } else {
            declarationWithComment = declaration;
        }
        
        return declarationWithComment;
    }

    private static bool CanBeGeneratedAsGetOnlyProperty(
        MemberKind memberKind,
        bool isGeneric,
        bool hasParameters
    )
    {
        return
            (memberKind == MemberKind.PropertyGetter || memberKind == MemberKind.FieldGetter) &&
            !isGeneric &&
            !hasParameters;
    }
    
    private static string WriteMethodImplementation(
        GeneratedMember cSharpGeneratedMember,
        GeneratedMember cMember,
        MemberInfo? memberInfo,
        MemberKind memberKind,
        string cMethodName,
        bool isGeneric,
        bool isStaticMethod,
        bool mayThrow,
        Type declaringType,
        Type returnOrSetterOrEventHandlerType,
        Nullability returnOrSetterOrEventHandlerNullability,
        Nullability returnOrSetterOrEventHandlerArrayElementNullability,
        TypeDescriptor returnOrSetterTypeDescriptor,
        IEnumerable<ParameterInfo> parameters,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        ISyntaxWriterConfiguration? syntaxWriterConfiguration,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        SwiftCodeBuilder sbImpl = new();

        bool needsRegularImpl = true;

        if (memberKind == MemberKind.Destructor) {
            needsRegularImpl = false;
            
            sbImpl.AppendLine($"{cMethodName}(self.__handle)");
        } else if (memberKind == MemberKind.TypeOf) {
            needsRegularImpl = false;
            
            string returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.C, 
                CodeLanguage.Swift,
                returnOrSetterOrEventHandlerArrayElementNullability
            ) ?? "{0}";

            string invocation = string.Format(returnTypeConversion, $"{cMethodName}()");

            sbImpl.AppendLine($"return {invocation}");
        }

        if (needsRegularImpl) {
            string parameterConversions = WriteParameterConversions(
                CodeLanguage.Swift,
                CodeLanguage.C,
                memberKind,
                returnOrSetterOrEventHandlerType,
                returnOrSetterOrEventHandlerNullability,
                returnOrSetterOrEventHandlerArrayElementNullability,
                parameters,
                isGeneric,
                genericTypeArguments,
                genericMethodArguments,
                typeDescriptorRegistry,
                out List<string> convertedParameterNames,
                out List<string> convertedGenericTypeArgumentNames,
                out List<string> convertedGenericMethodArgumentNames,
                out _
            );

            sbImpl.AppendLine(parameterConversions);
            sbImpl.AppendLine();

            if (mayThrow) {
                string cExceptionVarName = "__exceptionC";
                
                convertedParameterNames.Add($"&{cExceptionVarName}");
                
                sbImpl.AppendLine(Builder.Var(cExceptionVarName)
                    .TypeName("System_Exception_t?")
                    .ToString());
                
                sbImpl.AppendLine();
            }

            bool isReturning =
                memberKind != MemberKind.EventHandlerAdder &&
                memberKind != MemberKind.EventHandlerRemover &&
                memberKind != MemberKind.FieldSetter &&
                memberKind != MemberKind.PropertySetter &&
                !returnOrSetterTypeDescriptor.IsVoid;

            string returnValueName = "__returnValueC";
            
            string returnValueCStorage = isReturning
                ? $"let {returnValueName} = "
                : string.Empty;

            List<string> allParameterNames = new();

            if (!isStaticMethod) {
                allParameterNames.Add("self.__handle");
            }
            
            allParameterNames.AddRange(convertedGenericTypeArgumentNames);
            allParameterNames.AddRange(convertedGenericMethodArgumentNames);
            allParameterNames.AddRange(convertedParameterNames);
            
            string allParameterNamesString = string.Join(", ", allParameterNames);
            
            string invocation = $"{returnValueCStorage}{cMethodName}({allParameterNamesString})";
            
            sbImpl.AppendLine(invocation);
            sbImpl.AppendLine();

            string returnCode = string.Empty;

            if (isReturning) {
                if (memberKind == MemberKind.Constructor) {
                    returnCode = $"self.init(handle: {returnValueName})";
                } else {
                    string? returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                        CodeLanguage.C,
                        CodeLanguage.Swift,
                        returnOrSetterOrEventHandlerArrayElementNullability
                    );
    
                    if (!string.IsNullOrEmpty(returnTypeConversion)) {
                        string newReturnValueName = "__returnValue";

                        string fullReturnTypeConversion = Builder.Let(newReturnValueName)
                            .Value(string.Format(returnTypeConversion, returnValueName))
                            .ToString();
    
                        sbImpl.AppendLine(fullReturnTypeConversion);
                        sbImpl.AppendLine();
                        
                        returnValueName = newReturnValueName;
                    }
    
                    returnCode = $"return {returnValueName}";
                }
            }

            SwiftCodeBuilder sbByRefParameters = new();

            foreach (var parameter in parameters) {
                Type parameterType = parameter.ParameterType;

                bool isOutParameter = parameter.IsOut;
                bool isInParameter = parameter.IsIn;
                bool isByRefParameter = parameterType.IsByRef;
                
                if (!isOutParameter &&
                    !isInParameter &&
                    !isByRefParameter) {
                    continue;
                }

                parameterType = parameterType.GetNonByRefType();

                if (parameterType.IsGenericParameter ||
                    parameterType.IsGenericMethodParameter) {
                    parameterType = typeof(object);
                }
                
                string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
                string convertedParameterName = $"{parameterName}C";

                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

                var nullabilityInfoContext = new NullabilityInfoContext();
                var parameterNullability = nullabilityInfoContext.Create(parameter);
                var parameterArrayElementNullabilityState = parameterNullability.ElementType;
                Nullability parameterArrayElementNullability;

                if (parameterArrayElementNullabilityState is not null &&
                    parameterArrayElementNullabilityState.ReadState == parameterArrayElementNullabilityState.WriteState) {
                    parameterArrayElementNullability = parameterArrayElementNullabilityState.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else {
                    parameterArrayElementNullability = Nullability.NotSpecified;
                }

                string? parameterTypeConversion = parameterTypeDescriptor.GetTypeConversion(
                    CodeLanguage.C,
                    CodeLanguage.Swift,
                    parameterArrayElementNullability
                );

                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    continue;
                }

                if (!convertedParameterNames.Contains($"&{convertedParameterName}")) {
                    convertedParameterName = parameterName;
                }

                if (string.IsNullOrEmpty(parameterTypeConversion)) {
                    parameterTypeConversion = convertedParameterName;
                } else {
                    parameterTypeConversion = string.Format(parameterTypeConversion, convertedParameterName);
                }
                
                sbByRefParameters.AppendLine($"{parameterName} = {parameterTypeConversion}");
                sbByRefParameters.AppendLine();
            }

            string byRefParamtersCode = sbByRefParameters.ToString();

            if (!string.IsNullOrEmpty(byRefParamtersCode)) {
                sbImpl.AppendLine(byRefParamtersCode);
            }

            if (mayThrow) {
                sbImpl.AppendLine("""
if let __exceptionC {
    let __exception = System_Exception(handle: __exceptionC)
    let __error = __exception.error
    
    throw __error
}
""");

                sbImpl.AppendLine();
            }

            if (isReturning) {
                sbImpl.AppendLine(returnCode);
            }
        }

        string funcImpl = sbImpl.ToString();

        return funcImpl;
    }

    internal static string WriteExtensionMethod(
        GeneratedMember swiftGeneratedMember,
        bool isExtendedTypeOptional,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        MethodBase? methodBase = swiftGeneratedMember.Member as MethodBase;

        if (methodBase is null) {
            return string.Empty;
        }

        Type? typeWhereExtensionIsDeclared = methodBase.DeclaringType;

        if (typeWhereExtensionIsDeclared is null) {
            return string.Empty;
        }
        
        MemberKind memberKind = swiftGeneratedMember.MemberKind;

        TypeDescriptor typeDescriptorWhereExtensionIsDeclared = typeWhereExtensionIsDeclared.GetTypeDescriptor(typeDescriptorRegistry);
        string swiftTypeNameWhereExtensionIsDeclared = typeDescriptorWhereExtensionIsDeclared.GetTypeName(CodeLanguage.Swift, false);
        
        string? generatedName = swiftGeneratedMember.GetGeneratedName(CodeLanguage.Swift);

        if (string.IsNullOrEmpty(generatedName)) {
            return string.Empty;
        }
        
        // TODO: This is likely wrong
        bool isGeneric = false;
        IEnumerable<Type> genericParameters = Array.Empty<Type>();
        
        List<ParameterInfo> parameters = methodBase.GetParameters().ToList();
        var extendedTypeParameter = parameters[0];
        
        if (isExtendedTypeOptional) {
            if (!isGeneric &&
                extendedTypeParameter.ParameterType.IsReferenceType()) {
                var nullabilityContext = new NullabilityInfoContext();
                var parameterNullability = nullabilityContext.Create(extendedTypeParameter);

                if (parameterNullability.ReadState == parameterNullability.WriteState) {
                    bool isNotNull = parameterNullability.ReadState == NullabilityState.NotNull;

                    if (isNotNull) {
                        return string.Empty;
                    }
                }
            }
        }
        
        parameters.RemoveAt(0);

        string parametersString = WriteParameters(
            swiftGeneratedMember.MemberKind,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            false,
            typeWhereExtensionIsDeclared,
            parameters,
            isGeneric,
            genericParameters,
            false,
            false,
            typeDescriptorRegistry
        );

        Type returnType = typeof(void);
        
        MethodInfo? methodInfo = methodBase as MethodInfo;

        if (methodInfo is not null) {
            returnType = methodInfo.ReturnType;
        }
        
        bool returnTypeIsByRef = returnType.IsByRef;

        if (returnTypeIsByRef) {
            returnType = returnType.GetNonByRefType();
        }
        
        TypeDescriptor returnTypeDescriptor = returnType.GetTypeDescriptor(typeDescriptorRegistry);
        
        var nullabilityInfoContext = new NullabilityInfoContext();
        var returnTypeNullability = Nullability.NotSpecified;
        var returnTypeArrayElementNullability = Nullability.NotSpecified; 
        
        if (returnType.IsNullableValueType(out _)) {
            returnTypeNullability = Nullability.Nullable;
        } else if (returnType.IsReferenceType() &&
                   !returnType.IsByRefValueType(out bool nonByRefTypeIsStruct) &&
                   !nonByRefTypeIsStruct) {
            if (methodInfo is not null) {
                ParameterInfo returnOrSetterValueParameter;

                if (memberKind == MemberKind.PropertySetter ||
                    memberKind == MemberKind.EventHandlerAdder ||
                    memberKind == MemberKind.EventHandlerRemover) {
                    returnOrSetterValueParameter = methodInfo.GetParameters().LastOrDefault() ?? methodInfo.ReturnParameter;
                } else {
                    returnOrSetterValueParameter = methodInfo.ReturnParameter;
                }
                
                var nullabilityInfo = nullabilityInfoContext.Create(returnOrSetterValueParameter);

                if (memberKind == MemberKind.PropertyGetter) {
                    returnTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;

                    returnTypeArrayElementNullability = nullabilityInfo.ElementType?.ReadState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.PropertySetter) {
                    returnTypeNullability = nullabilityInfo.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                    
                    returnTypeArrayElementNullability = nullabilityInfo.ElementType?.WriteState == NullabilityState.NotNull
                        ? Nullability.NonNullable
                        : Nullability.NotSpecified;
                } else if (memberKind == MemberKind.EventHandlerAdder ||
                           memberKind == MemberKind.EventHandlerRemover) {
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                } else { // Method
                    if (nullabilityInfo.ReadState == nullabilityInfo.WriteState) {
                        returnTypeNullability = nullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }

                    var elementTypeNullabilityInfo = nullabilityInfo.ElementType;

                    if (elementTypeNullabilityInfo is not null &&
                        elementTypeNullabilityInfo.ReadState == elementTypeNullabilityInfo.WriteState) {
                        returnTypeArrayElementNullability = elementTypeNullabilityInfo.ReadState == NullabilityState.NotNull
                            ? Nullability.NonNullable
                            : Nullability.NotSpecified;
                    }
                }
            }
        }
        
        // TODO: This generates inout TypeName if the return type is by ref
        string swiftReturnTypeName = returnTypeDescriptor.GetTypeName(
            CodeLanguage.Swift,
            true,
            returnTypeNullability,
            returnTypeArrayElementNullability,
            false,
            returnTypeIsByRef,
            false
        );

        bool mayThrow = swiftGeneratedMember.MayThrow;

        #region Impl
        string toReturnOrNotToReturn = !returnType.IsVoid()
            ? "return "
            : string.Empty;
        
        string toTryOrNotToTry = mayThrow
            ? "try "
            : string.Empty;

        string invocationParametersString = WriteParameters(
            swiftGeneratedMember.MemberKind,
            null,
            Nullability.NotSpecified,
            Nullability.NotSpecified,
            false,
            typeWhereExtensionIsDeclared,
            parameters,
            isGeneric,
            genericParameters,
            true,
            true,
            typeDescriptorRegistry
        );

        if (string.IsNullOrEmpty(invocationParametersString)) {
            invocationParametersString = "self";
        } else {
            invocationParametersString = "self, " + invocationParametersString;
        }

        string invocation = $"{toReturnOrNotToReturn}{toTryOrNotToTry}{swiftTypeNameWhereExtensionIsDeclared}.{generatedName}({invocationParametersString})";
        #endregion Impl

        string fullDecl;

        if (CanBeGeneratedAsGetOnlyProperty(memberKind, isGeneric, parameters.Any())) {
            string propTypeName = !returnType.IsVoid()
                ? swiftReturnTypeName
                : throw new Exception("A property must have a return type");
            
            fullDecl = Builder.GetOnlyProperty(generatedName, propTypeName)
                .Public()
                .Throws(mayThrow)
                .Implementation(invocation)
                .ToString();
        } else {
            fullDecl = Builder.Func(generatedName)
                .Public()
                .Parameters(parametersString)
                .Throws(mayThrow)
                .ReturnTypeName(!returnType.IsVoid()
                    ? swiftReturnTypeName
                    : null)
                .Implementation(invocation)
                .ToString();   
        }

        return fullDecl;
    }
    
    internal static string WriteParameters(
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        Nullability setterOrEventHandlerTypeArrayElementNullability,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        bool onlyWriteParameterNames,
        bool writeModifiersForInvocation,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        var nullabilityContext = new NullabilityInfoContext();
        
        List<string> parameterList = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(
                CodeLanguage.Swift, 
                true,
                Nullability.NonNullable
            );
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name.EscapedSwiftName();

                string parameterString = onlyWriteParameterNames 
                    ? parameterName 
                    : $"{parameterName}: {nativeSystemTypeTypeName} /* {systemTypeTypeName} */";
            
                parameterList.Add(parameterString);
            }
        }
        
        foreach (var parameter in parameters) {
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
            
            Type parameterType = parameter.ParameterType;
            
            bool isByRefParameter = parameterType.IsByRef;

            if (isByRefParameter) {
                parameterType = parameterType.GetNonByRefType();
            }
            
            bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;
            
            if (isGenericParameterType) {
                parameterType = typeof(object);
            }
            
            bool isGenericArrayParameterType = false;
            Type? arrayType = parameterType.GetElementType();
                    
            if (parameterType.IsArray &&
                arrayType is not null &&
                (arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                isGenericArrayParameterType = true;
            }

            if (isGenericArrayParameterType) {
                parameterType = typeof(Array);
            }
            
            bool isNotNull = false;
            bool isArrayElementNotNull = false;

            if (!isGeneric &&
                !isGenericParameterType &&
                !isGenericArrayParameterType &&
                parameterType.IsReferenceType()) {
                var parameterNullability = nullabilityContext.Create(parameter);

                if (parameterNullability.ReadState == parameterNullability.WriteState) {
                    isNotNull = parameterNullability.ReadState == NullabilityState.NotNull;
                }

                var parameterElementTypeNullability = parameterNullability.ElementType;

                if (parameterElementTypeNullability is not null &&
                    parameterElementTypeNullability.ReadState == parameterElementTypeNullability.WriteState) {
                    isArrayElementNotNull = parameterElementTypeNullability.ReadState == NullabilityState.NotNull;
                }
            }
            
            TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

            string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                CodeLanguage.Swift,
                true,
                isNotNull ? Nullability.NonNullable : Nullability.NotSpecified,
                isArrayElementNotNull ? Nullability.NonNullable : Nullability.NotSpecified,
                isOutParameter,
                isByRefParameter,
                isInParameter
            );

            string? parameterName = parameter.Name
                ?.EscapedSwiftName();

            if (parameterName is null) {
                throw new Exception("Parameter without a name");
            }

            string parameterString;
            
            if (onlyWriteParameterNames) {
                if (writeModifiersForInvocation) {
                    parameterString = $"{(isByRefParameter || isOutParameter ? "&" : string.Empty)}{parameterName}";
                } else {
                    parameterString = parameterName;
                }
            } else {
                parameterString = $"_ {parameterName}: {unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */";    
            }
            
            parameterList.Add(parameterString);
        }

        if (memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            if (setterOrEventHandlerType == null) {
                throw new Exception("Setter or Event Handler Type may not be null");
            }
            
            TypeDescriptor setterOrEventHandlerTypeDescriptor = setterOrEventHandlerType.GetTypeDescriptor(typeDescriptorRegistry);
            
            string cSetterOrEventHandlerTypeName = setterOrEventHandlerTypeDescriptor.GetTypeName(
                CodeLanguage.Swift,
                true,
                setterOrEventHandlerTypeNullability,
                setterOrEventHandlerTypeArrayElementNullability
            );

            const string parameterName = "value";

            string parameterString = onlyWriteParameterNames
                ? parameterName
                : $"_ {parameterName}: {cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */";
            
            parameterList.Add(parameterString);
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }
    
    internal static string WriteParameterConversions(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        Nullability setterOrEventHandlerTypeNullability,
        Nullability setterOrEventHandlerTypeArrayElementNullability,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames,
        out List<string> convertedGenericTypeArgumentNames,
        out List<string> convertedGenericMethodArgumentNames,
        out List<string> parameterTypeBackConversionCodes
    )
    {
        string convertedParameterNameSuffix;

        if (sourceLanguage == CodeLanguage.Swift &&
            targetLanguage == CodeLanguage.C) {
            convertedParameterNameSuffix = "C";
        } else if (sourceLanguage == CodeLanguage.C &&
                   targetLanguage == CodeLanguage.Swift) {
            convertedParameterNameSuffix = "Swift";
        } else {
            throw new Exception("Unknown language pair");
        }
        
        SwiftCodeBuilder sb = new();
        
        convertedParameterNames = new();
        convertedGenericTypeArgumentNames = new();
        convertedGenericMethodArgumentNames = new();
        parameterTypeBackConversionCodes = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string systemTypeTypeConversion = systemTypeTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage,
                Nullability.NotSpecified
            )!;
    
            foreach (var genericArgumentType in genericTypeArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}{convertedParameterNameSuffix}";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);

                string typeConversionCode = Builder.Let(convertedGenericArgumentName)
                    .Value(fullTypeConversion)
                    .ToString();
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericTypeArgumentNames.Add(convertedGenericArgumentName);
            }
            
            foreach (var genericArgumentType in genericMethodArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}{convertedParameterNameSuffix}";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);

                string typeConversionCode = Builder.Let(convertedGenericArgumentName)
                    .Value(fullTypeConversion)
                    .ToString();
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericMethodArgumentNames.Add(convertedGenericArgumentName);
            }
        }

        foreach (var parameter in parameters) {
            string? parameterName = parameter.Name
                ?.EscapedSwiftName();
            
            if (parameterName is null) {
                throw new Exception("Parameter without a name");
            }
            
            Type parameterType = parameter.ParameterType;
            bool isOutParameter = parameter.IsOut;
            bool isInParameter = parameter.IsIn;
            
            WriteParameterConversion(
                sourceLanguage,
                targetLanguage,
                parameter,
                Nullability.NotSpecified,
                parameterName,
                parameterType,
                isOutParameter,
                isInParameter,
                isGeneric,
                typeDescriptorRegistry,
                out string? typeConversionCode,
                out string convertedParameterName,
                out string? typeBackConversionCode
            );

            if (!string.IsNullOrEmpty(typeConversionCode)) {
                sb.AppendLine(typeConversionCode);
            }
            
            convertedParameterNames.Add(convertedParameterName);

            if (!string.IsNullOrEmpty(typeBackConversionCode)) {
                parameterTypeBackConversionCodes.Add(typeBackConversionCode);
            }
        }

        if (memberKind == MemberKind.FieldSetter ||
            memberKind == MemberKind.PropertySetter ||
            memberKind == MemberKind.EventHandlerAdder ||
            memberKind == MemberKind.EventHandlerRemover) {
            string parameterName = "value";
            Type parameterType = setterOrEventHandlerType ?? throw new Exception("No setter or event handler type");
            const bool isOutParameter = false;
            const bool isInParameter = false;
            
            WriteParameterConversion(
                sourceLanguage,
                targetLanguage,
                null, // TODO
                setterOrEventHandlerTypeNullability,
                parameterName,
                parameterType,
                isOutParameter,
                isInParameter,
                isGeneric,
                typeDescriptorRegistry,
                out string? typeConversionCode,
                out string convertedParameterName,
                out string? typeBackConversionCode
            );

            if (!string.IsNullOrEmpty(typeConversionCode)) {
                sb.AppendLine(typeConversionCode);
            }
            
            convertedParameterNames.Add(convertedParameterName);
            
            if (!string.IsNullOrEmpty(typeBackConversionCode)) {
                parameterTypeBackConversionCodes.Add(typeBackConversionCode);
            }
        }

        return sb.ToString();
    }

    private static void WriteParameterConversion(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        ParameterInfo? parameterInfo,
        Nullability parameterNullability,
        string parameterName,
        Type parameterType,
        bool isOutParameter,
        bool isInParameter,
        bool isGeneric,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out string? typeConversionCode,
        out string convertedParameterName,
        out string? typeBackConversionCode
    )
    {
        if (string.IsNullOrEmpty(parameterName)) {
            throw new Exception("Parameter has no name");   
        }
        
        var nullabilityContext = new NullabilityInfoContext();
        
        string convertedParameterNameSuffix;

        if (sourceLanguage == CodeLanguage.Swift &&
            targetLanguage == CodeLanguage.C) {
            convertedParameterNameSuffix = "C";
        } else if (sourceLanguage == CodeLanguage.C &&
                   targetLanguage == CodeLanguage.Swift) {
            convertedParameterNameSuffix = "Swift";
        } else {
            throw new Exception("Unknown language pair");
        }
        
        bool isByRefParameter = parameterType.IsByRef;
        bool isArrayType = parameterType.IsArray;
        bool isInOut = isOutParameter || isInParameter || isByRefParameter;

        if (isByRefParameter) {
            parameterType = parameterType.GetNonByRefType();
        }
        
        bool isGenericParameterType = parameterType.IsGenericParameter || parameterType.IsGenericMethodParameter;

        if (!isByRefParameter &&
            isOutParameter) {
            throw new Exception("Parameter is out but not by ref, that's impossible");
        } else if (isGenericParameterType) {
            parameterType = typeof(object);
        } else if (isArrayType) {
            if (isGeneric) {
                Type? arrayType = parameterType.GetElementType();

                if (arrayType is not null &&
                    (arrayType.IsGenericType || arrayType.IsGenericParameter || arrayType.IsGenericMethodParameter)) {
                    parameterType = typeof(Array);
                }
            }
        }
        
        TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);

        Nullability parameterArrayElementNullability;
        
        if (parameterInfo is not null) {
            var nullabilityInfoContext = new NullabilityInfoContext();
            var parameterNullabilityA = nullabilityInfoContext.Create(parameterInfo);
            var parameterArrayElementNullabilityState = parameterNullabilityA.ElementType;

            if (parameterArrayElementNullabilityState is not null &&
                parameterArrayElementNullabilityState.ReadState == parameterArrayElementNullabilityState.WriteState) {
                parameterArrayElementNullability = parameterArrayElementNullabilityState.ReadState == NullabilityState.NotNull
                    ? Nullability.NonNullable
                    : Nullability.NotSpecified;
            } else {
                parameterArrayElementNullability = Nullability.NotSpecified;
            }
        } else {
            parameterArrayElementNullability = Nullability.NotSpecified;
        }

        string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
            sourceLanguage,
            targetLanguage,
            parameterArrayElementNullability
        );
        
        if (typeConversion != null) {
            string parameterNameForConversion = parameterName;
            
            if (parameterName.StartsWith("`") &&
                parameterName.EndsWith("`")) {
                parameterNameForConversion = parameterName.Trim('`');
            }
            
            convertedParameterName = $"{parameterNameForConversion}{convertedParameterNameSuffix}";

            string optionalString;
            
            if (sourceLanguage == CodeLanguage.Swift &&
                targetLanguage == CodeLanguage.C) {
                if (parameterInfo is not null &&
                    !isGeneric &&
                    !isGenericParameterType &&
                    parameterType.IsReferenceType()) {
                    bool isNotNull = false;
                    
                    var parameterNullabilityInfo = nullabilityContext.Create(parameterInfo);

                    if (parameterNullabilityInfo.ReadState == parameterNullabilityInfo.WriteState) {
                        isNotNull = parameterNullabilityInfo.ReadState == NullabilityState.NotNull;
                    }
                    
                    parameterNullability = isNotNull
                        ? Nullability.NonNullable
                        : parameterTypeDescriptor.Nullability;   
                }

                if (parameterNullability == Nullability.NotSpecified) {
                    parameterNullability = parameterTypeDescriptor.Nullability;
                }
                
                optionalString = parameterNullability.GetSwiftOptionalitySpecifier();
            } else {
                optionalString = string.Empty;
            }

            string fullTypeConversion = string.Format(typeConversion, $"{parameterName}{optionalString}");

            SwiftVariableKinds variableKind = isInOut
                ? SwiftVariableKinds.Variable
                : SwiftVariableKinds.Constant;

            typeConversionCode = Builder.Variable(variableKind, convertedParameterName)
                .Value(fullTypeConversion)
                .ToString();
            
            if (isInOut) {
                convertedParameterName = $"&{convertedParameterName}";
            }

            typeBackConversionCode = null;
        } else {
            if (sourceLanguage == CodeLanguage.C &&
                targetLanguage == CodeLanguage.Swift &&
                isInOut) {
                string swiftParameterName = $"__{parameterName}Swift";
                
                typeConversionCode = $"var {swiftParameterName} = {parameterName}?.pointee ?? .init()";
                typeBackConversionCode = $"{parameterName}?.pointee = {swiftParameterName}";
                
                convertedParameterName = $"&{swiftParameterName}";
            } else {
                typeConversionCode = null;
                typeBackConversionCode = null;
                
                if (isInOut) {
                    convertedParameterName = $"&{parameterName}";
                } else {
                    convertedParameterName = parameterName;
                }
            }
        }
    }
}