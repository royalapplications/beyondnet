namespace Beyond.NET.Builder;

[Flags]
public enum BuildTargets
{
    None =                  0,
        
    MacOSArm64 =            1 << 0,
    MacOSX64 =              1 << 1,
    iOSArm64 =              1 << 2,
    iOSSimulatorArm64 =     1 << 3,
    iOSSimulatorX64 =       1 << 4,
        
    MacOSUniversal = MacOSArm64 | MacOSX64,
    iOSSimulatorUniversal = iOSSimulatorArm64 | iOSSimulatorX64,
    iOSUniversal = iOSArm64 | iOSSimulatorUniversal,
        
    AppleUniversal = MacOSUniversal | iOSUniversal
}

internal static class BuildTargets_Extensions
{
    public static bool ContainsAnyiOSTarget(this BuildTargets buildTargets)
    {
        return buildTargets.HasFlag(BuildTargets.iOSArm64) ||
               buildTargets.HasFlag(BuildTargets.iOSSimulatorArm64) ||
               buildTargets.HasFlag(BuildTargets.iOSSimulatorX64);
    }
    
    public static bool ContainsAnyiOSSimulatorTarget(this BuildTargets buildTargets)
    {
        return buildTargets.HasFlag(BuildTargets.iOSSimulatorArm64) ||
               buildTargets.HasFlag(BuildTargets.iOSSimulatorX64);
    }
    
    public static bool ContainsAnyMacOSTarget(this BuildTargets buildTargets)
    {
        return buildTargets.HasFlag(BuildTargets.MacOSArm64) ||
               buildTargets.HasFlag(BuildTargets.MacOSX64);
    }
}