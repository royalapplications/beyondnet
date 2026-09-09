using System.Security.Cryptography.X509Certificates;

namespace Beyond.NET.Sample;

public class X509CertificatesTests
{
    #region X509Certificate
    public static X509CertificateCollection CreateX509CertificateCollection()
    {
        var collection = new X509CertificateCollection();

        return collection;
    }

    public static X509CertificateCollection.X509CertificateEnumerator CreateX509CertificateEnumerator()
    {
        var collection = CreateX509CertificateCollection();
        var enumerator = collection.GetEnumerator();

        return enumerator;
    }
    #endregion X509Certificate

    #region X509Certificate2
    public static X509Certificate2Collection CreateX509Certificate2Collection()
    {
        var collection = new X509Certificate2Collection();

        return collection;
    }

    public static X509Certificate2Enumerator CreateX509Certificate2Enumerator()
    {
        var collection = CreateX509Certificate2Collection();
        var enumerator = collection.GetEnumerator();

        return enumerator;
    }
    #endregion X509Certificate2
}
