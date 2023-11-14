using System.Runtime.InteropServices;

namespace Beyond.NET.Sample;

public struct ReadOnlyBytes
{
    private readonly IReadOnlyCollection<byte> _bytes;

    public int Length => _bytes.Count;
    
    public ReadOnlyBytes(byte[] byteArray)
    {
        _bytes = byteArray.AsReadOnly() ?? throw new ArgumentNullException(nameof(byteArray));
    }
    
    public ReadOnlyBytes(ReadOnlySpan<byte> readOnlySpanOfBytes)
    {
        _bytes = readOnlySpanOfBytes.ToArray() ?? throw new ArgumentNullException(nameof(readOnlySpanOfBytes));
    }
    
    public ReadOnlyBytes(
        IntPtr source,
        int length
    )
    {
        if (source == IntPtr.Zero) {
            throw new ArgumentOutOfRangeException(nameof(source));
        }

        byte[] bytes;

        if (length > 0) {
            bytes = new byte[length];
            
            Marshal.Copy(
                source,
                bytes,
                0,
                length
            );
        } else { // Empty
            bytes = Array.Empty<byte>();
        }

        _bytes = bytes;
    }

    public void CopyTo(IntPtr destination)
    {
        var length = Length;

        if (length <= 0) { // Empty
            return;
        }
        
        var byteArray = ToArray();

        Marshal.Copy(
            byteArray,
            0,
            destination,
            length
        );
    }

    public byte[] ToArray()
    {
        return _bytes.ToArray();
    }
}