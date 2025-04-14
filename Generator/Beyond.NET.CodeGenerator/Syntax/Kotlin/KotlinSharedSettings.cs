namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

static class KotlinSharedSettings
{
    private static readonly List<Type> UnsupportedInterfaceTypes = [
        typeof(System.Net.ICredentialsByHost), // Unsupported because some implementations of this use different nullability
        typeof(System.Net.ICredentials), // Unsupported because some implementations of this use different nullability
        typeof(System.ICloneable), // Unsupported because some implementations of this use different nullability
    ];

    private static readonly List<Type> UnsupportedTypes = [
        typeof(System.Xml.XmlDocument), // Unsupported because some implementations of this use different nullability
        typeof(System.Xml.XmlProcessingInstruction), // Unsupported because some implementations of this use different nullability
        typeof(System.Xml.XmlAttributeCollection), // Unsupported because some implementations of this use different nullability
        typeof(System.Security.Cryptography.HashAlgorithm), // Unsupported because some implementations of this use different nullability
    ];

    internal static bool IsUnsupportedInterface(this Type interfaceType)
    {
        bool isIt = UnsupportedInterfaceTypes.Contains(interfaceType);

        return isIt;
    }

    internal static bool IsUnsupportedTypeOrDerivedByUnsupportedType(this Type type)
    {
        bool isIt = UnsupportedTypes.Contains(type);

        if (isIt) {
            return true;
        }

        var baseType = type.BaseType;

        if (baseType is not null &&
            IsUnsupportedTypeOrDerivedByUnsupportedType(baseType)) {
            return true;
        }

        return false;
    }
}