using System.Reflection;
using System.Text;

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

        StringBuilder sb = new();

        Type propertyType = property.PropertyType;

        MethodInfo? getterMethod = property.GetGetMethod(false);
        MethodInfo? setterMethod = property.GetSetMethod(false);

        if (getterMethod != null) {
            bool isStaticMethod = getterMethod.IsStatic;
            
            string getterCode = WriteMethod(
                getterMethod,
                MethodKind.PropertyGetter,
                propertyName,
                isStaticMethod,
                mayThrow,
                declaringType,
                propertyType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(getterCode);
        }
        
        if (setterMethod != null) {
            bool isStaticMethod = setterMethod.IsStatic;
            
            string setterCode = WriteMethod(
                setterMethod,
                MethodKind.PropertySetter,
                propertyName,
                isStaticMethod,
                mayThrow,
                declaringType,
                propertyType,
                parameters,
                typeDescriptorRegistry,
                state
            );

            sb.AppendLine(setterCode);
        }

        return sb.ToString();
    }
}