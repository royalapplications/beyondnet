using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.Swift;

public class SwiftMethodSyntaxWriter: ISwiftSyntaxWriter, IMethodSyntaxWriter
{
    public string Write(object @object, State state)
    {
        return Write((MethodInfo)@object, state);
    }

    public string Write(MethodInfo method, State state)
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
            true,
            typeDescriptorRegistry,
            state,
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
        bool addToState,
        TypeDescriptorRegistry typeDescriptorRegistry,
        State state,
        out string generatedName
    )
    {
        if (memberInfo == null &&
            memberKind != MemberKind.Destructor &&
            memberKind != MemberKind.TypeOf) {
            throw new Exception("memberInfo may only be null when memberKind is Destructor");
        }
        
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
                bool isOutOrByRef = parameter.IsOut || parameter.ParameterType.IsByRef;

                if (isOutOrByRef) {
                    Type nonByRefParameterType = parameter.ParameterType.GetNonByRefType();

                    if (nonByRefParameterType.IsArray) {
                        generatedName = string.Empty;
                        
                        return "// TODO: Generic Methods with out/ref parameters that are arrays are not supported";    
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
            bool isActuallyOverridden = methodInfo.IsOverridden();

            if (isActuallyOverridden) {
                treatAsOverridden = true;
            } else {
                bool isShadowed = methodInfo.IsShadowed();

                if (isShadowed) {
                    treatAsOverridden = true;
                }
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
        
        string swiftReturnOrSetterTypeName = returnOrSetterTypeDescriptor.GetTypeName(
            CodeLanguage.Swift,
            true,
            true,
            false,
            returnOrSetterOrEventHandlerTypeIsByRef
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
        
        StringBuilder sb = new();

        #region Func Signature
        string methodSignatureParameters = WriteParameters(
            memberKind,
            setterType,
            isStaticMethod,
            declaringType,
            parameters,
            isGeneric,
            combinedGenericArguments,
            typeDescriptorRegistry
        );

        string funcSignature;

        if (memberKind == MemberKind.Constructor) {
            SwiftInitDeclaration decl = new(
                true,
                true,
                SwiftVisibilities.Public,
                methodSignatureParameters,
                mayThrow
            );

            funcSignature = decl.ToString();
        } else if (memberKind == MemberKind.Destructor) {
            SwiftFuncDeclaration decl = new(
                methodNameSwift,
                SwiftVisibilities.Internal,
                SwiftTypeAttachmentKinds.Instance,
                true,
                methodSignatureParameters,
                mayThrow,
                null
            );

            funcSignature = decl.ToString();
        } else if (memberKind == MemberKind.TypeOf) {
            SwiftFuncDeclaration decl = new(
                methodNameSwift,
                SwiftVisibilities.Public,
                SwiftTypeAttachmentKinds.Class,
                true,
                methodSignatureParameters,
                mayThrow,
                !returnOrSetterOrEventHandlerType.IsVoid()
                    ? swiftReturnOrSetterTypeNameWithComment
                    : null
            );

            funcSignature = decl.ToString();
        } else {
            SwiftFuncDeclaration decl = new(
                methodNameSwift,
                SwiftVisibilities.Public,
                isStaticMethod 
                    ? SwiftTypeAttachmentKinds.Class
                    : SwiftTypeAttachmentKinds.Instance,
                treatAsOverridden,
                methodSignatureParameters,
                mayThrow,
                !returnOrSetterOrEventHandlerType.IsVoid()
                    ? swiftReturnOrSetterTypeNameWithComment
                    : null
            );

            funcSignature = decl.ToString();
        }

        funcSignature += " {";

        sb.AppendLine(funcSignature);
        #endregion Func Signature

        #region Func Implementation
        StringBuilder sbImpl = new();

        bool needsRegularImpl = true;

        if (memberKind == MemberKind.Destructor) {
            needsRegularImpl = false;
            
            sbImpl.AppendLine($"{cMethodName}(self.__handle)");
        } else if (memberKind == MemberKind.TypeOf) {
            needsRegularImpl = false;
            
            string returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.C, 
                CodeLanguage.Swift
            ) ?? "{0}";

            string invocation = string.Format(returnTypeConversion, $"{cMethodName}()");

            sbImpl.AppendLine($"return {invocation}");
        }

        if (needsRegularImpl) {
            string parameterConversions = WriteParameterConversions(
                CodeLanguage.Swift,
                CodeLanguage.C,
                parameters,
                isGeneric,
                genericTypeArguments,
                genericMethodArguments,
                typeDescriptorRegistry,
                out List<string> convertedParameterNames,
                out List<string> convertedGenericTypeArgumentNames,
                out List<string> convertedGenericMethodArgumentNames
            );

            sbImpl.AppendLine(parameterConversions);
            sbImpl.AppendLine();

            if (mayThrow) {
                string cExceptionVarName = "__exceptionC";
                
                convertedParameterNames.Add($"&{cExceptionVarName}");
                
                sbImpl.AppendLine($"var {cExceptionVarName}: System_Exception_t?");
                sbImpl.AppendLine();
            }

            bool isReturning =
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

            string returnCode = string.Empty;

            if (isReturning) {
                if (memberKind == MemberKind.Constructor) {
                    returnCode = $$"""
guard let {{returnValueName}} else { return nil }

self.init(handle: {{returnValueName}})
""";
                } else {
                    string? returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                        CodeLanguage.C,
                        CodeLanguage.Swift
                    );
    
                    if (!string.IsNullOrEmpty(returnTypeConversion)) {
                        string newReturnValueName = "__returnValue";
                        
                        string fullReturnTypeConversion = $"let {newReturnValueName} = {string.Format(returnTypeConversion, returnValueName)}";
    
                        sbImpl.AppendLine(fullReturnTypeConversion);
                        sbImpl.AppendLine();
                        
                        returnValueName = newReturnValueName;
                    }
    
                    returnCode = $"return {returnValueName}";
                }
            }

            if (mayThrow) {
                // TODO: Exception handling
                sbImpl.AppendLine("// TODO: Exception Handling");
            }

            if (isReturning) {
                sbImpl.AppendLine();
                sbImpl.AppendLine(returnCode);
            }
        }

        string funcImpl = sbImpl
            .ToString()
            .IndentAllLines(1);

        sb.AppendLine(funcImpl);
        #endregion Func Implementation

        #region Func End
        sb.AppendLine("}");
        #endregion Func End
        
        generatedName = methodNameSwift;
        
        return sb.ToString();
    }
    
    internal static string WriteParameters(
        MemberKind memberKind,
        Type? setterOrEventHandlerType,
        bool isStatic,
        Type declaringType,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericArguments,
        TypeDescriptorRegistry typeDescriptorRegistry
    )
    {
        List<string> parameterList = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string nativeSystemTypeTypeName = systemTypeTypeDescriptor.GetTypeName(
                CodeLanguage.Swift, 
                true,
                false
            );
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name;
            
                string parameterString = $"{parameterName}: {nativeSystemTypeTypeName} /* {systemTypeTypeName} */";
            
                parameterList.Add(parameterString);
            }
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
                true
            );
    
            string parameterString = $"value: {cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */";
            parameterList.Add(parameterString);
        } else {
            foreach (var parameter in parameters) {
                bool isOutParameter = parameter.IsOut;
                
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
                
                TypeDescriptor parameterTypeDescriptor = parameterType.GetTypeDescriptor(typeDescriptorRegistry);
                
                string unmanagedParameterTypeName = parameterTypeDescriptor.GetTypeName(
                    CodeLanguage.Swift,
                    true,
                    true,
                    isOutParameter,
                    isByRefParameter
                );

                string parameterString = $"{parameter.Name}: {unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */";
                parameterList.Add(parameterString);
            }
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }
    
    internal static string WriteParameterConversions(
        CodeLanguage sourceLanguage,
        CodeLanguage targetLanguage,
        IEnumerable<ParameterInfo> parameters,
        bool isGeneric,
        IEnumerable<Type> genericTypeArguments,
        IEnumerable<Type> genericMethodArguments,
        TypeDescriptorRegistry typeDescriptorRegistry,
        out List<string> convertedParameterNames,
        out List<string> convertedGenericTypeArgumentNames,
        out List<string> convertedGenericMethodArgumentNames
    )
    {
        StringBuilder sb = new();
        
        convertedParameterNames = new();
        convertedGenericTypeArgumentNames = new();
        convertedGenericMethodArgumentNames = new();

        if (isGeneric) {
            Type typeOfSystemType = typeof(Type);
            TypeDescriptor systemTypeTypeDescriptor = typeOfSystemType.GetTypeDescriptor(typeDescriptorRegistry);
            string systemTypeTypeName = typeOfSystemType.GetFullNameOrName();
            
            string systemTypeTypeConversion = systemTypeTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage
            )!;
    
            foreach (var genericArgumentType in genericTypeArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}C";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);
                string typeConversionCode = $"let {convertedGenericArgumentName} = {fullTypeConversion}";
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericTypeArgumentNames.Add(convertedGenericArgumentName);
            }
            
            foreach (var genericArgumentType in genericMethodArguments) {
                string name = genericArgumentType.Name;
                
                string convertedGenericArgumentName = $"{name}C";
                    
                string fullTypeConversion = string.Format(systemTypeTypeConversion, name);
                string typeConversionCode = $"let {convertedGenericArgumentName} = {fullTypeConversion}";
    
                sb.AppendLine(typeConversionCode);
                
                convertedGenericMethodArgumentNames.Add(convertedGenericArgumentName);
            }
        }

        foreach (var parameter in parameters) {
            string parameterName = parameter.Name ?? throw new Exception("Parameter has no name");
            
            Type parameterType = parameter.ParameterType;
            bool isOutParameter = parameter.IsOut;
            bool isByRefParameter = parameterType.IsByRef;
            bool isArrayType = parameterType.IsArray;
            bool isInOut = isOutParameter || isByRefParameter;

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

            string? typeConversion = parameterTypeDescriptor.GetTypeConversion(
                sourceLanguage,
                targetLanguage
            );

            string convertedParameterName;

            if (typeConversion != null) {
                convertedParameterName = $"{parameterName}C";
                
                string fullTypeConversion = string.Format(typeConversion, $"{parameterName}?");

                string varOrLet = isInOut 
                    ? "var"
                    : "let";

                string typeConversionCode = $"{varOrLet} {convertedParameterName} = {fullTypeConversion}";
                
                sb.AppendLine(typeConversionCode);
                
                if (isInOut) {
                    convertedParameterName = $"&{convertedParameterName}";
                }
            } else {
                if (!isGeneric &&
                    isOutParameter) {
                    convertedParameterName = $"&{parameterName}";
                } else if (!isGeneric &&
                           isByRefParameter) {
                    convertedParameterName = $"&{parameterName}";
                } else {
                    convertedParameterName = parameterName;
                }
            }
            
            convertedParameterNames.Add(convertedParameterName);
        }

        return sb.ToString();
    }
}