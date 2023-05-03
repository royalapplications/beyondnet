using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class FieldInfoExtensions
{
    public static bool IsShadowed(this FieldInfo fieldInfo)
    {
        Type? declaringType = fieldInfo.DeclaringType;

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
            
            MemberInfo[] baseMembers = baseType.GetMember(fieldInfo.Name);

            foreach (var baseMember in baseMembers) {
                if (baseMember.DeclaringType == declaringType) {
                    continue;
                }

                return true;
            }
        }

        return false;
    }
}