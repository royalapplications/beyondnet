namespace Beyond.NET.Builder.SwiftC;

public static class TargetDouble
{
    public static string Make(
        string targetIdentifier, 
        string platformIdentifier
    )
    {
        string targetDouble = $"{targetIdentifier}-{platformIdentifier}";
        
        return targetDouble;
    }
}