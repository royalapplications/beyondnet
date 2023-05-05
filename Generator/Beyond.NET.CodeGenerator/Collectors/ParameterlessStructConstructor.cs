using System.Globalization;
using System.Reflection;

using Beyond.NET.CodeGenerator.Extensions;

namespace Beyond.NET.CodeGenerator.Collectors;

public class ParameterlessStructConstructorInfo: ConstructorInfo
{
    public ParameterlessStructConstructorInfo(Type declaringType)
    {
        if (!declaringType.IsStruct()) {
            throw new Exception("Declaring Type must be a struct");
        }
        
        DeclaringType = declaringType;
    }
    
    public override Type DeclaringType { get; }
    public override string Name => ".ctor";
    public override ParameterInfo[] GetParameters() => Array.Empty<ParameterInfo>();
    
    public override MethodAttributes Attributes => MethodAttributes.FamANDAssem | 
                                                   MethodAttributes.Family |
                                                   MethodAttributes.Public |
                                                   MethodAttributes.HideBySig |
                                                   MethodAttributes.SpecialName |
                                                   MethodAttributes.RTSpecialName;
    
    public override object[] GetCustomAttributes(bool inherit) => Array.Empty<object>();
    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => Array.Empty<object>();
    public override bool IsDefined(Type attributeType, bool inherit) => false;
    
    public override Type ReflectedType => throw new NotImplementedException();
    public override MethodImplAttributes GetMethodImplementationFlags() => throw new NotImplementedException();
    public override RuntimeMethodHandle MethodHandle => throw new NotImplementedException();
    public override object? Invoke(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? parameters, CultureInfo? culture) => throw new NotImplementedException();
    public override object Invoke(BindingFlags invokeAttr, Binder? binder, object?[]? parameters, CultureInfo? culture) => throw new NotImplementedException();
}