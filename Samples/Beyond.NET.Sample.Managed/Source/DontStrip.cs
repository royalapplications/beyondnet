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
}