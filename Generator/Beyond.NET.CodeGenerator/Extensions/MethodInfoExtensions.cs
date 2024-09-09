using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsOverridden(
        this MethodInfo methodInfo, 
        out bool nullabilityIsCompatible
    )
    {
        nullabilityIsCompatible = true;
        
        Type? declaringType = methodInfo.DeclaringType;

        if (declaringType is null) {
            return false;
        }
        
        MethodInfo baseMethodInfo = methodInfo.GetBaseDefinition();
        Type? baseMethodDeclaringType = baseMethodInfo.DeclaringType;

        if (baseMethodDeclaringType is null) {
            return false;
        }

        bool declaringTypeIsGeneric = declaringType.IsGenericType ||
                                      declaringType.IsGenericTypeDefinition;

        bool baseTypeIsGeneric = baseMethodDeclaringType.IsGenericType ||
                                 baseMethodDeclaringType.IsGenericTypeDefinition;

        if (declaringTypeIsGeneric != baseTypeIsGeneric) {
            return false;
        }

        bool isOverridden = methodInfo != baseMethodInfo || 
                            declaringType != baseMethodDeclaringType ||
                            declaringTypeIsGeneric != baseTypeIsGeneric;

        if (!isOverridden) {
            return false;
        }

        if (!methodInfo.ReturnParameter.IsNullabilityInfoCompatible(baseMethodInfo.ReturnParameter)) {
            nullabilityIsCompatible = false;
            
            return isOverridden;
        }

        var methodParameters = methodInfo.GetParameters();
        var methodParametersBase = baseMethodInfo.GetParameters();

        for (int i = 0; i < methodParameters.Length; i++) {
            var parameter = methodParameters[i];
            var parameterBase = methodParametersBase[i];

            if (!parameter.IsNullabilityInfoCompatible(parameterBase)) {
                nullabilityIsCompatible = false;

                return isOverridden;
            }
        }

        return isOverridden;
    }

    public static bool IsShadowed(
        this MethodInfo methodInfo,
        CodeLanguage targetLanguage,
        out bool nullabilityIsCompatible
    )
    {
        nullabilityIsCompatible = true;
        
        Type? declaringType = methodInfo.DeclaringType;

        if (declaringType is null) {
            return false;
        }

        Type? baseType = declaringType.BaseType;

        if (baseType is not null) {
            var isShadowedByBaseType = IsShadowed(
                methodInfo,
                declaringType,
                baseType,
                targetLanguage,
                out nullabilityIsCompatible
            );

            if (isShadowedByBaseType) {
                return true;
            }
        }

        if (targetLanguage == CodeLanguage.Kotlin) {
            var interfaceTypes = declaringType.GetInterfaces();

            foreach (var interfaceType in interfaceTypes) {
                var isShadowedByInterface = IsShadowed(
                    methodInfo,
                    declaringType,
                    interfaceType,
                    targetLanguage,
                    out nullabilityIsCompatible
                );

                if (isShadowedByInterface) {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsShadowed(
        this MethodInfo methodInfo,
        Type declaringType,
        Type targetType,
        CodeLanguage targetLanguage,
        out bool nullabilityIsCompatible
    )
    {
        nullabilityIsCompatible = true;
        
        bool declaringTypeIsGeneric = declaringType.IsGenericType ||
                                      declaringType.IsGenericTypeDefinition;

        bool baseTypeIsGeneric = targetType.IsGenericType ||
                                 targetType.IsGenericTypeDefinition;

        if (declaringTypeIsGeneric != baseTypeIsGeneric) {
            return false;
        }

        string name = methodInfo.Name;
        bool isStatic = methodInfo.IsStatic;
        bool isPublic = methodInfo.IsPublic;

        BindingFlags flags = BindingFlags.FlattenHierarchy;

        if (isStatic) {
            flags |= BindingFlags.Static;
        } else {
            flags |= BindingFlags.Instance;
        }

        if (isPublic) {
            flags |= BindingFlags.Public;
        } else {
            flags |= BindingFlags.NonPublic;
        }

        MemberInfo[] baseMembers = targetType.GetMember(name, flags);

        var returnType = methodInfo.ReturnType;

        foreach (var baseMember in baseMembers) {
            if (baseMember is not MethodInfo baseBaseMethodInfo) {
                continue;
            }

            Type? baseBaseDeclaringType = baseBaseMethodInfo.DeclaringType;

            if (baseBaseDeclaringType == declaringType) {
                continue;
            }

            var baseReturnType = baseBaseMethodInfo.ReturnType;

            if (!returnType.IsAssignableTo(baseReturnType)) {
                continue;
            }

            if ((targetLanguage == CodeLanguage.Swift) &&
                baseReturnType.IsInterface &&
                !returnType.IsInterface) {
                continue;
            }

            var returnParameter = methodInfo.ReturnParameter;
            var baseReturnParameter = baseBaseMethodInfo.ReturnParameter;

            if (!returnParameter.IsNullabilityInfoCompatible(baseReturnParameter)) {
                nullabilityIsCompatible = false;
            }

            var parameters = methodInfo.GetParameters();
            var baseParameters = baseBaseMethodInfo.GetParameters();

            if (parameters.Length != baseParameters.Length) {
                continue;
            }

            bool parameterTypesMatch = true;
            int parameterIdx = 0;

            foreach (var parameter in parameters) {
                var baseParameter = baseParameters[parameterIdx];

                // if (!parameter.ParameterType.IsAssignableTo(baseParameter.ParameterType)) {
                //     parameterTypesMatch = false;
                //     break;
                // }

                if (parameter.ParameterType != baseParameter.ParameterType) {
                    parameterTypesMatch = false;
                    break;
                }

                if (!parameter.IsNullabilityInfoCompatible(baseParameter)) {
                    nullabilityIsCompatible = false;
                }

                parameterIdx++;
            }

            if (parameterTypesMatch) {
                return true;
            }
        }

        return false;
    }
}