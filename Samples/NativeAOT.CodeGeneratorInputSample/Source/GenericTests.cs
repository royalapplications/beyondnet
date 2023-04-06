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
}