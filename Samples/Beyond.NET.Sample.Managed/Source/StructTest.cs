namespace Beyond.NET.Sample;

public struct StructTest
{
    public string? Name { get; set; }

    public StructTest(string? name)
    {
        Name = name;
    }

    // private static StructTest? NullInstanceField = null;
    public static StructTest? NullInstanceProperty => null;

    // private static StructTest? NonNullInstanceField = new StructTest("Test");
    public static StructTest? NonNullInstanceProperty => new StructTest("Test");

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