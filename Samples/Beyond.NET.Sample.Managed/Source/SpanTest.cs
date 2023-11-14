namespace Beyond.NET.Sample;

public class SpanTest
{
    #region Source Data
    public byte[] Data { get; }
    #endregion Source Data

    #region Constructor
    public SpanTest(byte[] data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }
    
    public SpanTest(Span<byte> dataAsSpan)
    {
        Data = dataAsSpan.ToArray();
    }
    
    public SpanTest(ReadOnlySpan<byte> dataAsReadOnlySpan)
    {
        Data = dataAsReadOnlySpan.ToArray();
    }
    #endregion Constructor

    #region ReadOnlyBytes
    public ReadOnlyBytes DataAsReadOnlyBytes => new(DataAsReadOnlySpan);
    #endregion ReadOnlyBytes

    #region Span<byte>
    public Span<byte> DataAsSpan => new(Data);
    public Span<byte> GetDataAsSpan() => DataAsSpan;
    
    public bool TryGetDataAsSpan(out Span<byte> dataAsSpan)
    {
        dataAsSpan = DataAsSpan;
        
        return true;
    }
    #endregion Span<byte>

    #region ReadOnlySpan<byte>
    public ReadOnlySpan<byte> DataAsReadOnlySpan => new(Data);
    public ReadOnlySpan<byte> GetDataAsReadOnlySpan() => DataAsSpan;
    
    public bool TryGetDataAsReadOnlySpan(out ReadOnlySpan<byte> dataAsReadOnlySpan)
    {
        dataAsReadOnlySpan = DataAsReadOnlySpan;
        
        return true;
    }
    #endregion ReadOnlySpan<byte>
}