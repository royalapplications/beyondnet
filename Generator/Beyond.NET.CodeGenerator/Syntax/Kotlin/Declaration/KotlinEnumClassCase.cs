namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinEnumClassCase
{
    public string Name { get; }
    public string UnderlyingValue { get; }

    public KotlinEnumClassCase(
        string name,
        string underlyingValue
    )
    {
        Name = !string.IsNullOrEmpty(name)
            ? name 
            : throw new ArgumentOutOfRangeException(nameof(name));
        
        UnderlyingValue = !string.IsNullOrEmpty(underlyingValue)
            ? underlyingValue 
            : throw new ArgumentOutOfRangeException(nameof(underlyingValue));
    }
    
    public override string ToString()
    {
        var str = $"{Name}({UnderlyingValue})";

        return str;
    }

    public static string CasesToString(IEnumerable<KotlinEnumClassCase> cases)
    {
        string str = string.Join(",\n", cases);

        if (!string.IsNullOrEmpty(str)) {
            str += ";";
        }

        return str;
    }
}