using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class MethodInfoExtensions
{
    public static bool IsOverridden(this MethodInfo methodInfo)
    {
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

        return isOverridden;
    }

    public static bool IsShadowed(this MethodInfo methodInfo)
    {
        Type? declaringType = methodInfo.DeclaringType;

        if (declaringType is null) {
            return false;
        }
        
        Type? baseType = declaringType.BaseType;

        if (baseType is not null) {
            bool declaringTypeIsGeneric = declaringType.IsGenericType ||
                                          declaringType.IsGenericTypeDefinition;
            
            bool baseTypeIsGeneric = baseType.IsGenericType ||
                                     baseType.IsGenericTypeDefinition;

            if (declaringTypeIsGeneric != baseTypeIsGeneric) {
                return false;
            }
            
            MemberInfo[] baseMembers = baseType.GetMember(methodInfo.Name);

            foreach (var baseMember in baseMembers) {
                if (baseMember is not MethodInfo baseBaseMethodInfo) {
                    continue;
                }

                Type? baseBaseDeclaringType = baseBaseMethodInfo.DeclaringType;
                
                if (baseBaseDeclaringType == declaringType) {
                    continue;
                }

                var baseParameters = baseBaseMethodInfo.GetParameters();
                var baseReturnType = baseBaseMethodInfo.ReturnType;

                if (methodInfo.GetParameters() == baseParameters &&
                    methodInfo.ReturnType == baseReturnType) {
                    return true;
                }
            }
        }

        return false;
    }
}