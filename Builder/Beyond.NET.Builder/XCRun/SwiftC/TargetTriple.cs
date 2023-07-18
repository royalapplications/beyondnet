namespace Beyond.NET.Builder.XCRun.SwiftC;

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
            platformIdentifier
        );
        
        string targetTriple = $"{targetDouble}{deploymentTarget}{platformSuffix}";

        return targetTriple;
    }
}