namespace Beyond.NET.Sample;

public static class PrimitiveTests
{
    #region Signed
    public static sbyte SByteMin => sbyte.MinValue;
    public static sbyte SByteMax => sbyte.MaxValue;
    
    public static short ShortMin => short.MinValue;
    public static short ShortMax => short.MaxValue;
    
    public static int IntMin => int.MinValue;
    public static int IntMax => int.MaxValue;
    
    public static long LongMin => long.MinValue;
    public static long LongMax => long.MaxValue;
    
    public static nint NIntMin => nint.MinValue;
    public static nint NIntMax => nint.MaxValue;
    #endregion Signed
    
    #region Unsigned
    public static byte ByteMin => byte.MinValue;
    public static byte ByteMax => byte.MaxValue;
    
    public static ushort UShortMin => ushort.MinValue;
    public static ushort UShortMax => ushort.MaxValue;
    
    public static uint UIntMin => uint.MinValue;
    public static uint UIntMax => uint.MaxValue;
    
    public static ulong ULongMin => ulong.MinValue;
    public static ulong ULongMax => ulong.MaxValue;
    
    public static nuint NUIntMin => nuint.MinValue;
    public static nuint NUIntMax => nuint.MaxValue;
    #endregion Unsigned

    #region Floating Point
    public static float FloatMin => float.MinValue;
    public static float FloatMax => float.MaxValue;
    
    public static double DoubleMin => double.MinValue;
    public static double DoubleMax => double.MaxValue;
    #endregion Floating Point
}