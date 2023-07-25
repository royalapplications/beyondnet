namespace Beyond.NET.Builder.Apple.XCRun.SwiftC;

public static class TargetDouble
{
    public static string Make(
        string targetIdentifier, 
        string platformIdentifier,
        string platformSuffix
    )
    {
        string targetDouble = $"{targetIdentifier}-{platformIdentifier}{platformSuffix}";
        
        return targetDouble;
    }
}