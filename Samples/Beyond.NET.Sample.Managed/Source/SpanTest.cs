namespace Beyond.NET.Sample;

public class SpanTest
{
    #region Delegates
    public delegate ReadOnlySpan<byte> ByteArrayToSpanDelegate(byte[] bytes);
    public delegate byte[] SpanToByteArrayDelegate(ReadOnlySpan<byte> span);
    #endregion Delegates

    #region Source Data
    public byte[] Data { get; private set; }
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

    public ReadOnlySpan<byte> DataAsReadOnlySpan
    {
        get {
            return new(Data);
        }
        set {
            Data = value.ToArray();
        }
    }

    public ReadOnlySpan<byte> GetDataAsReadOnlySpan() => DataAsSpan;
    public void SetDataAsReadOnlySpan(ReadOnlySpan<byte> span) => Data = span.ToArray();

    public bool TryGetDataAsReadOnlySpan(out ReadOnlySpan<byte> dataAsReadOnlySpan)
    {
        dataAsReadOnlySpan = DataAsReadOnlySpan;

        return true;
    }

    public ReadOnlySpan<byte> ConvertByteArrayToSpan(
        byte[] bytes,
        ByteArrayToSpanDelegate converter
    )
    {
        return converter(bytes);
    }

    public byte[] ConvertSpanToByteArray(
        ReadOnlySpan<byte> span,
        SpanToByteArrayDelegate converter
    )
    {
        return converter(span);
    }
    #endregion ReadOnlySpan<byte>
}