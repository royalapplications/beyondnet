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

    // See https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#nondestructive-mutation
    public static bool IsVirtualCloneMethod(this MemberInfo memberInfo)
    {
        if (!(memberInfo is MethodInfo methodInfo)) {
            return false;
        }

        // TODO: Is there a better way to check this?!
        bool isIt = methodInfo.Name == "<Clone>$";

        return isIt;
    }
}