namespace Beyond.NET.Builder;

public class SwiftBuilderConfiguration
{
    public string ProductName { get; init; }
    
    public string SDKPath { get; init; }
    
    public string DeploymentTargetMacOS { get; init; }
    public string DeploymentTargetiOS { get; init; }
    
    public string SwiftVersion { get; init; }
    
    public string GeneratedHeaderFileName { get; init; }
    public string GeneratedSwiftFileName { get; init; }
}