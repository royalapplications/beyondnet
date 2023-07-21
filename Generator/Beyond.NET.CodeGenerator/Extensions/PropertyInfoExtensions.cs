using System.Reflection;

namespace Beyond.NET.CodeGenerator.Extensions;

public static class PropertyInfoExtensions
{
    public static MethodInfo? GetPublicAndNonInitSetMethod(this PropertyInfo propertyInfo)
    {
        MethodInfo? setterMethod = propertyInfo.GetSetMethod(false);

        if (setterMethod is not null) {
            var isExternalInitType = typeof(System.Runtime.CompilerServices.IsExternalInit);
            var requiredCustomModifiers = setterMethod.ReturnParameter.GetRequiredCustomModifiers();
            bool isInitOnly = requiredCustomModifiers.Contains(isExternalInitType);

            if (isInitOnly) {
                return null;
            }
        }
        
        return setterMethod;
    }
}