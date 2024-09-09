namespace Beyond.NET.Sample;

public class NullabilityTests
{
    public NullabilityTests()
    {
        
    }
    
    public NullabilityTests(bool throwAnException)
    {
        if (throwAnException) {
            throw new Exception("Expected Exception");
        }
    }

    public static NullabilityTests CreateInstance(bool throwAnException)
    {
        return new NullabilityTests(throwAnException);
    }
    
    public string NonNullableStringProperty { get; set; } = "Hello";
    public string NonNullableStringField = "Hello";

    public string? NullableStringProperty { get; set; } = null;
    public string? NullableStringField = null;

    public string MethodWithNonNullableStringReturnValue() => "Hello";
    public string? MethodWithNullableStringReturnValue() => null;

    public string MethodWithNonNullableStringParameter(string value)
    {
        if (value == null) {
            throw new ArgumentNullException(nameof(value));
        }

        return value;
    }
    
    public string MethodWithNonNullableStringParameterThatThrows(string value)
    {
        throw new Exception("Expected Exception");
    }
    
    public string? MethodWithNullableStringParameter(string? value)
    {
        return value;
    }
}