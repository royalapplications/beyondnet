using System.Reflection;

namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTests
{
    public static Type ReturnGenericType<T>()
    {
        return typeof(T);
    }
    
    public static Type[] ReturnGenericTypes<T1, T2>()
    {
        return new [] {
            typeof(T1),
            typeof(T2)
        };
    }

    public static T? ReturnDefaultValueOfGenericType<T>()
    {
        return default;
    }
    
    public static T?[] ReturnArrayOfDefaultValuesOfGenericType<T>(int numberOfElements)
    {
        if (numberOfElements <= 0) {
            return Array.Empty<T>();
        }
        
        List<T?> defaultValues = new();

        for (int i = 0; i < numberOfElements; i++) {
            defaultValues.Add(default);
        }
        
        return defaultValues.ToArray();
    }

    public T[] ReturnArrayOfRepeatedValues<T>(T value, int numberOfElements)
    {
        if (numberOfElements <= 0) {
            return Array.Empty<T>();
        }

        List<T> values = new();

        for (int i = 0; i < numberOfElements; i++) {
            values.Add(value);
        }

        return values.ToArray();
    }
    
    public static Type CallReturnGenericTypeThroughReflection(Type T)
    {
        object? methodTarget = null;
        object[]? parameters = null;
        
        Type targetType = typeof(GenericTests);
        string nameOfMethod = nameof(ReturnGenericType);
        Type[] genericParameterTypes = new[] { T };
        Type[] parameterTypes = Type.EmptyTypes;

        System.Reflection.MethodInfo? method = targetType.GetMethod(
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