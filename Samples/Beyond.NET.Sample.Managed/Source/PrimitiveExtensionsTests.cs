namespace Beyond.NET.Sample.Source;

public static class PrimitiveExtensionsTests
{
    public static int RoundToInt(this double value)
    {
        return (int)Math.Round(value, 0);
    }

    public static int RoundToInt(this float value)
    {
        return (int)Math.Round(value, 0);
    }
}
