using System.Runtime.InteropServices;

namespace Beyond.NET.Sample;

public struct ReadOnlyBytes
{
    private readonly byte[] _byteArray;

    public int Length => _byteArray.Length;
    
    internal ReadOnlyBytes(byte[] byteArray)
    {
        _byteArray = byteArray ?? throw new ArgumentNullException(nameof(byteArray));
    }
    
    public ReadOnlyBytes(ReadOnlySpan<byte> readOnlySpanOfBytes)
    {
        _byteArray = readOnlySpanOfBytes.ToArray();
    }

    public void CopyTo(IntPtr destination)
    {
        var byteArray = _byteArray;
        
        Marshal.Copy(
            byteArray,
            0,
            destination,
            byteArray.Length
        );
    }
}