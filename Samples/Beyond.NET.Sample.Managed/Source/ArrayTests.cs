namespace Beyond.NET.Sample;

public class ArrayTests
{
    public bool[,] TwoDimensionalArrayOfBool { get; set; } = {
        { false, true },
        { true, false }
    };
    
    public int[,,] ThreeDimensionalArrayOfInt32 { get; set; } = {
        { { 1, 2, 3}, {4, 5, 6} },
        { { 7, 8, 9}, {10, 11, 12} }
    };

    public string?[] ArrayOfNullableString { get; set; } = {
        null, "a", "b", "c"
    };
    
    // TODO: Unsupported at the moment
    public int?[] ArrayOfNullableInt32 { get; set; } = {
        null, 1, 2, 3
    };
    
    // TODO: Unsupported at the moment
    public Guid?[] ArrayOfNullableGuids { get; set; } = {
        null, Guid.Empty, Guid.NewGuid()
    };
    
    public Guid[] ArrayOfGuids { get; set; } = {
        Guid.Empty, Guid.NewGuid()
    };
    
    public char[] ArrayOfCharacters { get; set; } = {
        'a', 'b', 'c'
    };
}