using System.Text;

namespace Beyond.NET.Builder.Apple.Clang;

public class ModuleMap
{
    public class Header
    {
        public enum Types
        {
            None,
            Private,
            Umbrella
        }

        public Types Type { get; init; }
        public string Name { get; init; }

        public Header(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            string prefix = Type switch {
                Types.Umbrella => "umbrella ",
                Types.Private => "private ",
                _ => string.Empty
            };

            return $"{prefix}header \"{Name}\"";
        }
    }

    public string Name { get; init; }
    public bool IsFramework { get; init; }
    public Header[]? Headers { get; init; }
    public bool ExportEverything { get; init; }

    public ModuleMap(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        string frameworkPrefix = IsFramework
            ? "framework "
            : string.Empty;

        string moduleDecl = $"{frameworkPrefix}module {Name} {{\n";

        StringBuilder sb = new(moduleDecl);

        if (Headers is not null) {
            foreach (var header in Headers) {
                sb.AppendLine($"\t{header.ToString()}");
            }
        }

        if (ExportEverything) {
            sb.AppendLine("\texport *");
        }

        sb.AppendLine("}");

        string fullDecl = sb.ToString();

        return fullDecl;
    }
}
