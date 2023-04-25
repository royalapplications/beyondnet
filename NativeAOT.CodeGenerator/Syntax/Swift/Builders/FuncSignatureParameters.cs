using NativeAOT.CodeGenerator.Syntax.Swift.Declaration;

namespace NativeAOT.CodeGenerator.Syntax.Swift.Builders;

public struct FuncSignatureParameters
{
    private List<FuncSignatureParameter> m_parameters = new();

    public FuncSignatureParameters() { }

    #region Add Parameter
    public FuncSignatureParameters AddParameter(FuncSignatureParameter parameter)
    {
        m_parameters.Add(parameter);

        return this;
    }
    
    public FuncSignatureParameters AddParameter(
        string label,
        string? name,
        string typeName
    )
    {
        return AddParameter(new FuncSignatureParameter(
            label,
            name,
            typeName
        ));
    }
    
    public FuncSignatureParameters AddParameter(
        string label,
        string typeName
    )
    {
        return AddParameter(new FuncSignatureParameter(
            label,
            typeName
        ));
    }
    #endregion Add Parameter
    
    #region Build
    public SwiftFuncSignatureParameters Build()
    {
        List<SwiftFuncSignatureParameter> parameters = new();

        foreach (var parameter in m_parameters) {
            var convertedParameter = parameter.Build();
            
            parameters.Add(convertedParameter);
        }
        
        return new(parameters);
    }

    public override string ToString()
    {
        return Build()
            .ToString();
    }
    #endregion Build
}