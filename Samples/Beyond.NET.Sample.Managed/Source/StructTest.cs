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
    
    public static StructTest? GetNullableStructReturnValueOfRefParameter(ref StructTest? returnValue)
    {
        return returnValue;
    }
}