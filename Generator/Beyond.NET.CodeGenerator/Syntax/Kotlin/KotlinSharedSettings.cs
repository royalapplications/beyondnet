namespace Beyond.NET.CodeGenerator.Syntax.Kotlin;

static class KotlinSharedSettings
{
    private static readonly List<Type> UnsupportedInterfaceTypes = [
        typeof(System.Net.ICredentialsByHost), // Unsupported because some implementations of this use different nullability
        // typeof(System.Net.ICredentials), // Unsupported because some implementations of this use different nullability (eg. `System.Net.NetworkCredential`)
        typeof(System.ICloneable), // Unsupported because some implementations of this use different nullability
    ];

    private static readonly List<Type> UnsupportedTypes = [
        typeof(System.Xml.XmlDocument), // Unsupported because some implementations of this use different nullability
        typeof(System.Xml.XmlProcessingInstruction), // Unsupported because some implementations of this use different nullability
        typeof(System.Xml.XmlAttributeCollection), // Unsupported because some implementations of this use different nullability
        typeof(System.Security.Cryptography.HashAlgorithm), // Unsupported because some implementations of this use different nullability
        typeof(System.Net.NetworkCredential), // Unsupported because it uses different nullability in `GetCredential` than specified by `System.Net.ICredentials`
        typeof(System.Security.Cryptography.X509Certificates.X509Certificate2Collection) // Unsupported because of `GetEnumerator` return type collision
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
