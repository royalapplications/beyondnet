namespace Beyond.NET.CodeGenerator.Syntax.Swift.Declaration;

public struct SwiftFuncSignatureParameters
{
    public IEnumerable<SwiftFuncSignatureParameter> Parameters { get; }

    public SwiftFuncSignatureParameters()
    {
        Parameters = Array.Empty<SwiftFuncSignatureParameter>();
    }

    public SwiftFuncSignatureParameters(IEnumerable<SwiftFuncSignatureParameter> parameters)
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