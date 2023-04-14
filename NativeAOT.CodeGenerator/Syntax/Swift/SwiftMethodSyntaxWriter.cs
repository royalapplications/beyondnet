using System.Reflection;
using System.Text;

using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Generator;
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
        
        string methodNameSwift = memberKind.SwiftName(memberInfo);
        
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
        
        StringBuilder sb = new();

        string staticOrNot = isStaticMethod
            ? "static "
            : string.Empty;
        
        string funcThrowsPart = mayThrow
            ? " throws"
            : string.Empty;

        string funcPrefix;
        string funcReturn;
        string funcParams = $"({methodSignatureParameters})";

        if (memberKind == MemberKind.Constructor) {
            funcPrefix = string.Empty;
            funcReturn = string.Empty;
        } else if (memberKind == MemberKind.Destructor) {
            funcPrefix = string.Empty;
            funcReturn = string.Empty;
            funcParams = string.Empty;
        } else {
            funcPrefix = $"{staticOrNot}func ";
            
            funcReturn = returnOrSetterOrEventHandlerType.IsVoid()
                ? string.Empty
                : $" -> {swiftReturnOrSetterTypeNameWithComment}";
        }
        
        string funcSignature = $"{funcPrefix}{methodNameSwift}{funcParams}{funcThrowsPart}{funcReturn} {{"; 

        sb.AppendLine(funcSignature);
        #endregion Func Signature

        #region Func Implementation
        StringBuilder sbImpl = new();

        // TODO
        if (memberKind == MemberKind.Constructor) {
            sbImpl.AppendLine("// TODO: Constructor");
        } else if (memberKind == MemberKind.Destructor) {
            sbImpl.AppendLine($"{cMethodName}(self._handle)");
        } else if (memberKind == MemberKind.TypeOf) {
            string returnTypeConversion = returnOrSetterTypeDescriptor.GetTypeConversion(
                CodeLanguage.C, 
                CodeLanguage.Swift
            ) ?? "{0}";

            string invocation = string.Format(returnTypeConversion, $"{cMethodName}()");

            sbImpl.AppendLine($"return {invocation}");
        } else {
            sbImpl.AppendLine("// TODO: Method/Property/Field/Event Handler adder/remover");
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
                true
            );
            
            foreach (var genericArgumentType in genericArguments) {
                string parameterName = genericArgumentType.Name;
            
                string parameterString = $"{nativeSystemTypeTypeName} /* {systemTypeTypeName} */ {parameterName}";
            
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
    
            string parameterString = $"{cSetterOrEventHandlerTypeName} /* {setterOrEventHandlerType.GetFullNameOrName()} */ value";
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

                string parameterString = $"{unmanagedParameterTypeName} /* {parameterType.GetFullNameOrName()} */ {parameter.Name}";
                parameterList.Add(parameterString);
            }
        }

        string parametersString = string.Join(", ", parameterList);

        return parametersString;
    }
}