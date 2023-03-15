using System.Reflection;

namespace NativeAOT.CodeGenerator.CLI;

internal class AssemblyLoader: IDisposable
{
    internal IEnumerable<string> SearchPaths { get; }

    internal AssemblyLoader() : this(Array.Empty<string>()) { }
    
    internal AssemblyLoader(IEnumerable<string> searchPaths)
    {
        SearchPaths = searchPaths;
        
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
            var searchPaths = GetAssemblySearchPaths();

            foreach (var searchPath in searchPaths) {
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
    
    private IEnumerable<string> GetAssemblySearchPaths()
    {
        List<string> searchPaths = SearchPaths.ToList();

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