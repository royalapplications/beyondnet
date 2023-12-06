using System.Reflection;

using Beyond.NET.Core;

namespace Beyond.NET.CodeGenerator.CLI;

internal class AssemblyResolvedEventArgs: EventArgs
{
    internal Assembly Assembly { get; }
    internal string AssemblyPath { get; }

    internal AssemblyResolvedEventArgs(
        Assembly assembly,
        string assemblyPath
    )
    {
        Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        
        AssemblyPath = !string.IsNullOrEmpty(assemblyPath) 
            ? assemblyPath
            : throw new ArgumentOutOfRangeException(nameof(assemblyPath));
    }
}

internal class AssemblyLoader: IDisposable
{
    private readonly List<string> m_searchPaths = new();

    internal event EventHandler<AssemblyResolvedEventArgs>? AssemblyResolved;

    internal AssemblyLoader() : this(Array.Empty<string>()) { }
    
    internal AssemblyLoader(IEnumerable<string> searchPaths)
    {
        m_searchPaths.AddRange(GetAssemblySearchPaths(searchPaths));
        
        AppDomain.CurrentDomain.AssemblyResolve += AppDomain_OnAssemblyResolve;
    }

    internal Assembly LoadFrom(string assemblyPath)
    {
        string? assemblyDirectoryPath = Path.GetDirectoryName(assemblyPath);

        if (!string.IsNullOrEmpty(assemblyDirectoryPath) &&
            !m_searchPaths.Contains(assemblyDirectoryPath)) {
            m_searchPaths.Add(assemblyDirectoryPath);
        }
        
        Assembly assembly = Assembly.LoadFrom(assemblyPath);
        
        XmlDocumentationStore.Shared.ParseDocumentation(assembly);

        return assembly;
    }
    
    /* internal IEnumerable<Assembly> LoadReferences(Assembly assembly)
    {
        HashSet<Assembly> references = new();
        
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        foreach (var referencedAssemblyNameObject in referencedAssemblies) {
            Assembly referencedAssembly = Assembly.Load(referencedAssemblyNameObject);

            references.Add(referencedAssembly);

            var recursiveReferences = LoadReferences(referencedAssembly);

            foreach (var recursiveReference in recursiveReferences) {
                references.Add(recursiveReference);
            }
        }

        return references;
    } */
    
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
            Assembly assembly = Assembly.LoadFrom(assemblyName);
            
            XmlDocumentationStore.Shared.ParseDocumentation(assembly);

            return assembly;
        } catch {
            foreach (var searchPath in m_searchPaths) {
                string potentialAssemblyPath = Path.Combine(
                    searchPath,
                    assemblyName
                );

                try {
                    Assembly assembly = Assembly.LoadFrom(potentialAssemblyPath);
                    
                    AssemblyResolved?.Invoke(this, new(
                        assembly,
                        potentialAssemblyPath
                    ));

                    return assembly;
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