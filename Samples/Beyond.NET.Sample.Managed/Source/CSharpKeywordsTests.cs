namespace Beyond.NET.Sample.Source;

public class CSharpKeywordsTests
{
    public string? @as = "test";

    public int @operator { get; }

    public byte @this { get; set; }

    public void @try()
    {}

    public long this[int @struct] => 4242L;

    public static int @abstract(string? @class) => 42;
}
