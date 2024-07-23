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

    public static bool IsStatic(this MemberInfo memberInfo)
    {
        if (memberInfo is MethodBase methodBase) {
            return methodBase.IsStatic;
        } else if (memberInfo is ConstructorInfo constructorInfo) {
            return constructorInfo.IsStatic;
        } else if (memberInfo is EventInfo eventInfo) {
            return (eventInfo.AddMethod?.IsStatic ?? false) || (eventInfo.RemoveMethod?.IsStatic ?? false);
        } else if (memberInfo is PropertyInfo propertyInfo) {
            return (propertyInfo.GetMethod?.IsStatic ?? false) || (propertyInfo.SetMethod?.IsStatic ?? false);
        } else if (memberInfo is FieldInfo fieldInfo) {
            return fieldInfo.IsStatic;
        }

        return false;
    }
}