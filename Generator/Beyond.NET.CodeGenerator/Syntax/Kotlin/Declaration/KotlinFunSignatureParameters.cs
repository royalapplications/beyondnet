namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

public struct KotlinFunSignatureParameters
{
    public IEnumerable<KotlinFunSignatureParameter> Parameters { get; }

    public KotlinFunSignatureParameters()
    {
        Parameters = Array.Empty<KotlinFunSignatureParameter>();
    }
    
    public KotlinFunSignatureParameters(IEnumerable<KotlinFunSignatureParameter> parameters)
    {
        Parameters = parameters;
    }

    public override string ToString()
    {
        List<string> parameterStrings = new();

        foreach (var parameter in Parameters) {
            parameterStrings.Add(parameter.ToString());
        }
        
        string parametersString = string.Join(", ", parameterStrings);

        return parametersString;
    }
}