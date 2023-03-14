using System.Reflection;
using System.Text;
using NativeAOT.CodeGenerator.Extensions;
using NativeAOT.CodeGenerator.Types;

namespace NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

public class CSharpUnmanagedPropertySyntaxWriter: CSharpUnmanagedMethodSyntaxWriter, IPropertySyntaxWriter
{
    public new string Write(object @object, State state)
    {
        return Write((PropertyInfo)@object, state);
    }
    
    public string Write(PropertyInfo property, State state)
    {
        TypeDescriptorRegistry typeDescriptorRegistry = TypeDescriptorRegistry.Shared;
        
        const bool mayThrow = true;

        string propertyName = property.Name;

        Type declaringType = property.DeclaringType ?? throw new Exception("No declaring type");;

        if (declaringType.IsAbstract) {
            return string.Empty;
        }
        
        IEnumerable<ParameterInfo> parameters = Array.Empty<ParameterInfo>();

        var accessors = property.GetAccessors();

        StringBuilder sb = new();

        Type setterType = declaringType;

        foreach (var accessor in accessors) {
            bool isSetter = !accessor.ReturnType.IsVoid();
            bool isGetter = !isSetter;
            bool isStaticMethod = accessor.IsStatic;

            MethodKind methodKind = isGetter 
                ? MethodKind.PropertyGetter
                : MethodKind.PropertySetter;

            // string accessorMethodName = accessor.Name;
            
            string accessorCode = WriteMethod(
                property,
                methodKind,
                propertyName,
                isStaticMethod,
                mayThrow,
                declaringType,
                setterType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(accessorCode);
        }

        return sb.ToString();
    }
}