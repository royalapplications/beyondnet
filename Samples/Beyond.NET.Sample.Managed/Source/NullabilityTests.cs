namespace Beyond.NET.Sample;

public class NullabilityTests
{
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
    
    public string? MethodWithNullableStringParameter(string? value)
    {
        return value;
    }
}