namespace Beyond.NET.CodeGenerator.Syntax;

public class NamespaceNode
{
    public bool IsTreeRoot { get; }
    public string Name { get; }

    public string FullName
    {
        get {
            string compoundParentNames = CompoundParentNames;

            if (!string.IsNullOrEmpty(compoundParentNames)) {
                return $"{CompoundParentNames}.{Name}";
            } else {
                return Name;
            }
        }
    }

    private List<NamespaceNode> m_children = new();

    public NamespaceNode[] Children
    {
        get {
            return m_children.ToArray();
        }
    }

    internal bool HasParent
    {
        get {
            var parentRef = m_parent;

            if (parentRef is null) {
                return false;
            }

            if (!parentRef.TryGetTarget(out NamespaceNode? parent)) {
                return false;
            }

            if (parent.IsTreeRoot) {
                return false;
            }

            return true;
        }
    }

    // public bool IsRootNode => Parent
    public bool IsDeepestNode => Children.Length <= 0;

    private WeakReference<NamespaceNode>? m_parent;

    public string CompoundParentNames
    {
        get {
            List<string> names = new();

            var target = this;

            while (target.m_parent?.TryGetTarget(out NamespaceNode? parent) ?? false) {
                if (parent.IsTreeRoot) {
                    break;
                }

                names.Add(parent.Name);

                target = parent;
            }

            names.Reverse();

            string joined = string.Join('.', names);

            return joined;
        }
    }

    public NamespaceNode(string name)
    {
        IsTreeRoot = string.IsNullOrEmpty(name);
        Name = name;
    }

    private void AddChild(NamespaceNode childNode)
    {
        childNode.m_parent = new(this);

        m_children.Add(childNode);
    }

    public static NamespaceNode FromTypes(IEnumerable<Type> types)
    {
        var rootNode = new NamespaceNode(string.Empty);

        foreach (var type in types) {
            var namespaceParts = type.Namespace?.Split('.') ?? Array.Empty<string>();
            var currentNode = rootNode;

            foreach (var part in namespaceParts) {
                var existingNode = currentNode.Children.FirstOrDefault(n => n.Name == part);

                if (existingNode is not null) {
                    currentNode = existingNode;
                } else {
                    var newNode = new NamespaceNode(part);

                    currentNode.AddChild(newNode);
                    currentNode = newNode;
                }
            }
        }

        return rootNode;
    }
}