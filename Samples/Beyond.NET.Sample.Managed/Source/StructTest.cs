namespace Beyond.NET.Sample;

public class StructTestClass
{
    public readonly StructTest? ReadOnlyNullInstanceField = null;
    public StructTest? NonNullInstanceField = new StructTest("Test");
    public StructTest? NullableStructPropertyWithGetterAndSetter { get; set; } = null;
}

public struct StructTest
{
    public string? Name { get; set; }

    public StructTest(string? name)
    {
        Name = name;
    }

    // TODO: Uncommenting this makes Assembly.ExportedTypes throw
    // public static readonly StructTest? ReadOnlyNullInstanceField = null;
    public static StructTest? NullInstanceProperty => null;
    
    // TODO: Uncommenting this makes Assembly.ExportedTypes throw
    // public static StructTest? NonNullInstanceField = new StructTest("Test");
    
    public static StructTest? NonNullInstanceProperty => new StructTest("Test");
    
    // TODO: Uncommenting this makes Assembly.ExportedTypes throw
    // public static StructTest? NullableStructPropertyWithGetterAndSetter { get; set; } = null;

    public static StructTest? GetNullableStructReturnValue(bool returnNull)
    {
        if (returnNull) {
            return null;
        } else {
            return new StructTest("Test");
        }
    }
    
    public static void GetNullableStructReturnValueInOutParameter(
        bool returnNull, 
        out StructTest? returnValue
    )
    {
        if (returnNull) {
            returnValue = null;
        } else {
            returnValue = new StructTest("Test");
        }
    }
    
    public static StructTest? GetNullableStructReturnValueOfParameter(StructTest? returnValue)
    {
        return returnValue;
    }
    
    public static StructTest? GetNullableStructReturnValueOfRefParameter(ref StructTest? returnValue)
    {
        return returnValue;
    }
}