namespace Beyond.NET.Builder.DotNET;

public class Runtime
{
    public const string RUNTIME_NAME_MICROSOFT_NETCORE_APP = "Microsoft.NETCore.App";
    
    public string Name { get; }
    public string Version { get; }
    public string Path { get; }

    public Runtime(
        string name,
        string version,
        string path
    )
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        Path = path ?? throw new ArgumentNullException(nameof(path));
    }
    
    public static Runtime[] GetRuntimes()
    {
        var result = App.DotNETApp.Launch(new[] {
            "--list-runtimes"
        });

        Exception? failure = result.FailureAsException;

        if (failure is not null) {
            throw failure;
        }

        var stdOut = result.StandardOut
            ?.Replace("\r\n", "\n");

        if (string.IsNullOrEmpty(stdOut)) {
            return Array.Empty<Runtime>();
        }

        var split = stdOut.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        List<Runtime> runtimes = new();
        
        foreach (var runtimeLine in split) {
            var runtime = WithRuntimeLine(runtimeLine);

            if (runtime is null) {
                continue;
            }
            
            runtimes.Add(runtime);
        }

        return runtimes.ToArray();
    }

    private static Runtime? WithRuntimeLine(string runtimeLine)
    {
        runtimeLine = runtimeLine.Trim();

        var split = runtimeLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (split.Length != 3) {
            return null;
        }

        var name = split[0].Trim();
        var version = split[1].Trim();
        var path = split[2].Trim('[', ']');

        if (string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(version) ||
            string.IsNullOrEmpty(path)) {
            return null;
        }

        var runtime = new Runtime(
            name,
            version,
            path
        );
        
        return runtime;
    }
}