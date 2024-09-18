namespace Beyond.NET.Sample.Source;

public class EnumTests
{
    public string GetEnumName(EnumWithUnfavorableNames value)
    {
        return value.ToString();
    }
}

// TODO: Cases that are named the same if the first letter is lowercased present a problem in Swift
public enum EnumWithUnfavorableNames
{
    Int,
    // @int,
    UInt,
    // @uint,
    String,
    // @string,
    System,
    // system
}