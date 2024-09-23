namespace Beyond.NET.Sample.Source;

public static class DontStrip
{
    #region System.IO
    public static System.IO.MemoryStream MemoryStream => null!;
    public static System.IO.StreamWriter StreamWriter => null!;
    public static System.IO.StreamReader StreamReader => null!;
    #endregion System.IO

    #region System.Security.Cryptography
    public static System.Security.Cryptography.CryptoStream CryptoStream => null!;
    public static System.Security.Cryptography.Aes AES => null!;
    public static System.Security.Cryptography.DSA DSA => null!;
    public static System.Security.Cryptography.ECDsa ECDsa => null!;
    public static System.Security.Cryptography.ECDiffieHellman ECDiffieHellman => null!;
    #endregion System.Security.Cryptography
    
    #region System.Net
    public static System.Net.NetworkCredential NetworkCredential => null!;
    #endregion System.Net

    #region System.XML
    // TODO: This causes Kotlin override errors
    // Problem is: System.Xml.XmlNode innerText_set has value System.String but the impl in System.Xml.XmlDocument and System.Xml.XmlProcessingInstruction have nullable strings
    public static System.Xml.Serialization.XmlSerializer XmlSerializer => null!;
    #endregion System.XML
}