using System.Diagnostics.CodeAnalysis;

namespace Beyond.NET.Sample;

public record TestRecord(string AString)
{
    public void Test()
    {
        var recordStruct = new TestRecordStruct(DateTime.Now) {
            AString = "",
            AStringField = ""
        };
    }
}

public record struct TestRecordStruct(int anInt)
{
    public required string AString { get; init; }
    public required string AStringField;

    public TestRecordStruct(DateTime dateTime) : this(0)
    {
        
    }
}