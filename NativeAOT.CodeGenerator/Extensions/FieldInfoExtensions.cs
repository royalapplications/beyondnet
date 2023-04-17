using System.Reflection;

namespace NativeAOT.CodeGenerator.Extensions;

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