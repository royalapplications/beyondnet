namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTests
{
    public List<string>? ListOfStrings { get; set; } = new() {
        "A",
        "B"
    };

    public Dictionary<string, Exception>? DictionaryOfStringKeysAndExceptionValues { get; set; } = new() {
        { "A", new Exception("A") },
        { "B", new Exception("B") }
    };

    public string JoinListOfStrings(List<string> listOfString, string separator)
    {
        return string.Join(separator, listOfString);
    }
    
    public struct SimpleKeyValuePair
    {
        public object? Key { get; }
        public object? Value { get; }

        public SimpleKeyValuePair(object? key, object? value)
        {
            Key = key;
            Value = value;
        }
    }
    
    public static Type ReturnGenericType<T>()
    {
        return typeof(T);
    }
    
    public static void ReturnGenericTypeAsOutParameter<T>(out Type typeOfT)
    {
        typeOfT = typeof(T);
    }
    
    public static void ReturnGenericTypeAsRefParameter<T>(ref Type typeOfT)
    {
        typeOfT = typeof(T);
    }
    
    public static Type[] ReturnGenericTypes<T1, T2>()
    {
        return new [] {
            typeof(T1),
            typeof(T2)
        };
    }
    
    public static SimpleKeyValuePair ReturnSimpleKeyValuePair<TKey, TValue>(TKey key, TValue value)
    {
        return new SimpleKeyValuePair(key, value);
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

    public static string ReturnStringOfJoinedArray<T>(T[] values, string separator)
    {
        List<string> strings = new();

        foreach (var value in values) {
            if (value is null) {
                continue;
            }
            
            string? stringValue = value.ToString();

            if (stringValue is null) {
                continue;
            }
            
            strings.Add(stringValue);
        }

        string joinedString = string.Join(separator, strings);

        return joinedString;
    }
}