using System.Reflection;

namespace NativeAOT.CodeGenerator.Extensions;

public static class MethodInfoExtensions
{
    // TODO: Works only for methods (not! fields)
    public static bool IsOverridden(this MethodInfo methodInfo)
    {
        Type? declaringType = methodInfo.DeclaringType;

        if (declaringType is null) {
            return false;
        }
        
        MethodInfo baseMethodInfo = methodInfo.GetBaseDefinition();
        Type? baseMethodDeclaringType = baseMethodInfo.DeclaringType;

        bool isOverridden = methodInfo != baseMethodInfo || 
                            declaringType != baseMethodDeclaringType;

        return isOverridden;
    }

    // TODO: Works only for methods (not! fields)
    public static bool IsShadowed(this MethodInfo methodInfo)
    {
        Type? declaringType = methodInfo.DeclaringType;

        if (declaringType is null) {
            return false;
        }
        
        Type? baseType = declaringType.BaseType;

        if (baseType is not null) {
            MemberInfo[] baseMembers = baseType.GetMember(methodInfo.Name);

            foreach (var baseMember in baseMembers) {
                if (baseMember is not MethodInfo baseBaseMethodInfo) {
                    continue;
                }
                
                if (baseBaseMethodInfo.DeclaringType == declaringType) {
                    continue;
                }

                var baseParameters = baseBaseMethodInfo.GetParameters();
                // var baseReturnType = baseBaseMethodInfo.ReturnType;

                if (methodInfo.GetParameters() == baseParameters /*&&
                    methodInfo.ReturnType == baseReturnType*/) {
                    return true;
                }
            }
        }

        return false;
    }
}