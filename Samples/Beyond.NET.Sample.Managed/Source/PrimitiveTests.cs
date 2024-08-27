namespace Beyond.NET.Sample;

public static class PrimitiveTests
{
    #region Signed
    public static sbyte SByteMin => sbyte.MinValue;
    public static sbyte SByteMax => sbyte.MaxValue;
    public static sbyte ReturnSByte(sbyte input) => input;
    
    public static short ShortMin => short.MinValue;
    public static short ShortMax => short.MaxValue;
    public static short ReturnShort(short input) => input;
    
    public static int IntMin => int.MinValue;
    public static int IntMax => int.MaxValue;
    public static int ReturnInt(int input) => input;
    
    public static long LongMin => long.MinValue;
    public static long LongMax => long.MaxValue;
    public static long ReturnLong(long input) => input;
    
    public static nint NIntMin => nint.MinValue;
    public static nint NIntMax => nint.MaxValue;
    public static nint ReturnNInt(nint input) => input;
    #endregion Signed
    
    #region Unsigned
    public static byte ByteMin => byte.MinValue;
    public static byte ByteMax => byte.MaxValue;
    public static byte ReturnByte(byte input) => input;
    
    public static ushort UShortMin => ushort.MinValue;
    public static ushort UShortMax => ushort.MaxValue;
    public static ushort ReturnUShort(ushort input) => input;
    
    public static uint UIntMin => uint.MinValue;
    public static uint UIntMax => uint.MaxValue;
    public static uint ReturnUInt(uint input) => input;
    
    public static ulong ULongMin => ulong.MinValue;
    public static ulong ULongMax => ulong.MaxValue;
    public static ulong ReturnULong(ulong input) => input;
    
    public static nuint NUIntMin => nuint.MinValue;
    public static nuint NUIntMax => nuint.MaxValue;
    public static nuint ReturnNUInt(nuint input) => input;
    #endregion Unsigned

    #region Floating Point
    public static float FloatMin => float.MinValue;
    public static float FloatMax => float.MaxValue;
    public static float ReturnFloat(float input) => input;
    
    public static double DoubleMin => double.MinValue;
    public static double DoubleMax => double.MaxValue;
    public static double ReturnDouble(double input) => input;
    #endregion Floating Point
}