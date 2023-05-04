using System.Reflection;

namespace Beyond.NET.CodeGenerator.CLI;

internal class AssemblyLoader: IDisposable
{
    internal IEnumerable<string> SearchPaths { get; }

    internal AssemblyLoader() : this(Array.Empty<string>()) { }
    
    internal AssemblyLoader(IEnumerable<string> searchPaths)
    {
        SearchPaths = GetAssemblySearchPaths(searchPaths);
        
        AppDomain.CurrentDomain.AssemblyResolve += AppDomain_OnAssemblyResolve;
    }

    internal Assembly LoadFrom(string assemblyPath)
    {
        Assembly assembly = Assembly.LoadFrom(assemblyPath);

        return assembly;
    }
    
    private Assembly? AppDomain_OnAssemblyResolve(object? sender, ResolveEventArgs args)
    {
        string assemblyFullName = args.Name;
        AssemblyName assemblyNameObject = new(assemblyFullName);
        string? assemblyName = assemblyNameObject.Name;

        if (string.IsNullOrEmpty(assemblyName)) {
            return null;
        }

        if (!assemblyName.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase)) {
            assemblyName += ".dll";
        }
        
        try {
            return Assembly.LoadFrom(assemblyName);
        } catch {
            foreach (var searchPath in SearchPaths) {
                string potentialAssemblyPath = Path.Combine(
                    searchPath,
                    assemblyName
                );

                try {
                    return Assembly.LoadFrom(potentialAssemblyPath);
                } catch {
                    // ignored
                }
            }
        }

        return null;
    }
    
    private static IEnumerable<string> GetAssemblySearchPaths(IEnumerable<string> configuredSearchPaths)
    {
        List<string> searchPaths = new();

        foreach (string searchPath in configuredSearchPaths) {
            string expandedSearchPath = searchPath.ExpandTildeAndGetAbsolutePath();
            
            searchPaths.Add(expandedSearchPath);
        }

        searchPaths.Add(Environment.CurrentDirectory);

        string? processPath = Environment.ProcessPath;

        if (!string.IsNullOrEmpty(processPath)) {
            string? processDirectoryPath = Path.GetDirectoryName(processPath);
    
            if (!string.IsNullOrEmpty(processDirectoryPath)) {
                if (!searchPaths.Contains(processDirectoryPath)) {
                    searchPaths.Add(processDirectoryPath);
                }
            }
        }

        return searchPaths;
    }

    public void Dispose()
    {
        AppDomain.CurrentDomain.AssemblyResolve -= AppDomain_OnAssemblyResolve;
    }
}