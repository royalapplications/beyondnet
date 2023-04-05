using System.Reflection;

namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTests
{
    public static Type ReturnGenericType<T>()
    {
        return typeof(T);
    }
    
    public static Type CallReturnGenericTypeThroughReflection(Type T)
    {
        object? methodTarget = null;
        object[]? parameters = null;
        
        Type targetType = typeof(GenericTests);
        string nameOfMethod = nameof(ReturnGenericType);
        Type[] genericParameterTypes = new[] { T };
        Type[] parameterTypes = Array.Empty<Type>();

        MethodInfo? method = targetType.GetMethod(
            nameOfMethod,
            genericParameterTypes.Length,
            parameterTypes
        );

        if (method == null) {
            throw new Exception($"Method \"{nameOfMethod}\" not found");
        }

        MethodInfo genericMethod = method.MakeGenericMethod(genericParameterTypes);

        object? untypedReturnValue = genericMethod.Invoke(
            methodTarget,
            parameters
        );

        Type returnValue = (untypedReturnValue as Type)!;
        
        return returnValue;
    }
}

public class GenericTestClass<T>
{
    public Type ReturnGenericClassType()
    {
        return typeof(T);
    }
    
    public Type[] ReturnGenericClassTypeAndGenericMethodType<TM>()
    {
        return new[] {
            typeof(T),
            typeof(TM)
        };
    }
}