namespace Beyond.NET.Sample;

public struct StructTest
{
    public string? Name { get; set; }

    public StructTest(string? name)
    {
        Name = name;
    }

    public static StructTest? GetNullableStructReturnValue(bool returnNull)
    {
        if (returnNull) {
            return null;
        } else {
            return new StructTest("Test");
        }
    }
}