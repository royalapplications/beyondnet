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
            bool containsIsExternalInitType = requiredCustomModifiers.Contains(isExternalInitType);

            if (containsIsExternalInitType) {
                return null;
            }

            var isExternalInitTypeFullName = isExternalInitType.FullName;

            foreach (var requiredCustomModifier in requiredCustomModifiers) {
                var requiredCustomModifierTypeFullName = requiredCustomModifier.FullName;

                if (requiredCustomModifierTypeFullName == isExternalInitTypeFullName) {
                    return null;
                }
            }
        }

        return setterMethod;
    }
}