using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class MemberInfoExtensions
{
    public static bool IsObsoleteWithError(this MemberInfo memberInfo)
    {
        var obsoleteAttribute = memberInfo.GetCustomAttribute<ObsoleteAttribute>();

        if (obsoleteAttribute is null) {
            return false;
        }

        bool isError = obsoleteAttribute.IsError;

        if (isError) {
            return true;
        }
        
        return false;
    }
}