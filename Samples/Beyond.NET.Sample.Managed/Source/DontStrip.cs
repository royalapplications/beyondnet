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
    // TODO: This causes override errors in Kotlin
    // public static System.Net.NetworkCredential NetworkCredential => null!;
    //
    // private static void TestNetworkCredential()
    // {
    //     var c = (System.Net.ICredentialsByHost)null!;
    //     c.GetCredential(null, 0, null);
    //     
    //     var nc = (System.Net.NetworkCredential)null!;
    //     nc.GetCredential(null, 0, null);
    // }
    #endregion System.Net
}