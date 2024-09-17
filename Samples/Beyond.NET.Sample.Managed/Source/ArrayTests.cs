namespace Beyond.NET.Sample;

public class ArrayTests
{
    #region TwoDimensionalArrayOfBool
    public bool[,] TwoDimensionalArrayOfBool { get; set; } = {
        { false, true },
        { true, false }
    };

    public bool[,] TwoDimensionalArrayOfBoolAsReturn() 
        => TwoDimensionalArrayOfBool;
    
    public void SetTwoDimensionalArrayOfBoolWithParameter(bool[,] twoDimensionalArrayOfBool)
        => TwoDimensionalArrayOfBool = twoDimensionalArrayOfBool;
    
    public void TwoDimensionalArrayOfBoolAsOut(out bool[,] twoDimensionalArrayOfBool)
        => twoDimensionalArrayOfBool = TwoDimensionalArrayOfBool;
    
    public void TwoDimensionalArrayOfBoolAsRef(ref bool[,] twoDimensionalArrayOfBool)
        => twoDimensionalArrayOfBool = TwoDimensionalArrayOfBool;
    #endregion TwoDimensionalArrayOfBool

    #region ThreeDimensionalArrayOfInt32
    public int[,,] ThreeDimensionalArrayOfInt32 { get; set; } = {
        { { 1, 2, 3}, {4, 5, 6} },
        { { 7, 8, 9}, {10, 11, 12} }
    };
    #endregion ThreeDimensionalArrayOfInt32

    #region ArrayOfNullableString
    public string?[] ArrayOfNullableString { get; set; } = {
        null, "a", "b", "c"
    };
    
    public string?[] ArrayOfNullableStringAsReturn() 
        => ArrayOfNullableString;
    
    public void SetArrayOfNullableStringWithParameter(string?[] arrayOfNullableString)
        => ArrayOfNullableString = arrayOfNullableString;
    
    public void ArrayOfNullableStringAsOut(out string?[] arrayOfNullableString)
        => arrayOfNullableString = ArrayOfNullableString;
    
    public void ArrayOfNullableStringAsRef(ref string?[] arrayOfNullableString)
        => arrayOfNullableString = ArrayOfNullableString;
    #endregion ArrayOfNullableString

    #region ArrayOfNullableInt32
    // TODO: Unsupported at the moment
    public int?[] ArrayOfNullableInt32 { get; set; } = {
        null, 1, 2, 3
    };
    #endregion ArrayOfNullableInt32

    #region ArrayOfNullableGuids
    // TODO: Unsupported at the moment
    public Guid?[] ArrayOfNullableGuids { get; set; } = {
        null, Guid.Empty, Guid.NewGuid()
    };
    #endregion ArrayOfNullableGuids

    #region ArrayOfGuids
    public Guid[] ArrayOfGuids { get; set; } = {
        Guid.Empty, Guid.NewGuid()
    };
    #endregion ArrayOfGuids

    #region ArrayOfCharacters
    public char[] ArrayOfCharacters { get; set; } = [
        'a', 'b', 'c'
    ];
    #endregion ArrayOfCharacters

    #region ArrayOfBytes
    public byte[] ArrayOfBytes { get; set; } = [
        byte.MinValue, 1, 2, byte.MaxValue
    ];
    
    public sbyte[] ArrayOfSBytes { get; set; } = [
        SByte.MinValue, 1, 2, sbyte.MaxValue
    ];
    #endregion ArrayOfBytes
}