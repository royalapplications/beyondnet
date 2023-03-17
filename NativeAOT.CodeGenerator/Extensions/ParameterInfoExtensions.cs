using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace NativeAOT.CodeGenerator.Extensions;

public static class ParameterInfoExtensions
{
    internal static void GetDeclaration(this ParameterInfo parameterInfo)
    {
        StringBuilder sb = new();

        var attributes = new List<string>();

        string typeName;
        Type? elementType = parameterInfo.ParameterType.GetElementType();

        if (elementType != null) {
            typeName = parameterInfo.ParameterType.Name.Replace(elementType.Name, elementType.GetFullNameOrName());
        } else {
            typeName = parameterInfo.ParameterType.GetFullNameOrName();
        }

        if (Attribute.IsDefined(parameterInfo, typeof(ParamArrayAttribute))) {
            sb.Append("params ");
        } else if (parameterInfo.Position == 0 && parameterInfo.Member.IsDefined(typeof(ExtensionAttribute))) {
            sb.Append("this ");
        }

        if (parameterInfo.IsIn) {
            attributes.Add("In");
        }

        if (parameterInfo.IsOut) {
            if (typeName.Contains("&")) {
                typeName = typeName.Replace("&", "");
                sb.Append("out ");
            } else {
                attributes.Add("Out");
            }
        } else if (parameterInfo.ParameterType.IsByRef) {
            typeName = typeName.Replace("&", "");
            sb.Append("ref ");
        }

        sb.Append(typeName);

        sb.Append(" ");
        sb.Append(parameterInfo.Name);

        if (parameterInfo.IsOptional) {
            if (parameterInfo.DefaultValue != Missing.Value) {
                sb.Append(" = " + parameterInfo.DefaultValue);
            } else {
                attributes.Add("Optional");
            }
        }


        string attribute = attributes.Count > 0 ? "[" + string.Join(", ", attributes) + "] " : "";
        sb.Insert(0, attribute);
    }
}