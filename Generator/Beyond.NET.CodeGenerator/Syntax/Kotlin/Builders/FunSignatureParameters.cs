using System.Diagnostics.CodeAnalysis;
using Beyond.NET.CodeGenerator.Syntax.Kotlin.Declaration;

namespace Beyond.NET.CodeGenerator.Syntax.Kotlin.Builders;

public struct FunSignatureParameters
{
    private List<FunSignatureParameter> m_parameters = new();

    public FunSignatureParameters() { }

    #region Add Parameter
    public FunSignatureParameters AddParameter(FunSignatureParameter parameter)
    {
        m_parameters.Add(parameter);

        return this;
    }
    
    public FunSignatureParameters AddParameter(
        string name,
        [StringSyntax("Kt")] string typeName
    )
    {
        return AddParameter(new FunSignatureParameter(
            name,
            typeName
        ));
    }
    #endregion Add Parameter
    
    #region Build
    public KotlinFunSignatureParameters Build()
    {
        List<KotlinFunSignatureParameter> parameters = new();

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