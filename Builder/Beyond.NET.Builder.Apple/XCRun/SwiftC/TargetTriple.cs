namespace Beyond.NET.Builder.Apple.XCRun.SwiftC;

public static class TargetTriple
{
    public static string Make(
        string targetIdentifier,
        string platformIdentifier,
        string deploymentTarget,
        string platformSuffix
    )
    {
        string targetDouble = TargetDouble.Make(
            targetIdentifier,
            platformIdentifier,
            string.Empty
        );
        
        string targetTriple = $"{targetDouble}{deploymentTarget}{platformSuffix}";

        return targetTriple;
    }
}