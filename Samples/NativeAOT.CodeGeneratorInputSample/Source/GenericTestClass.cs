namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTestClass<T>
{
    public int AProperty { get; set; } = 0;
    public int AField = 0;

    public Type ReturnGenericClassType()
    {
        return typeof(T);
    }
    
    public static Type[] ReturnGenericClassTypeAndGenericMethodType<TM>()
    {
        return new[] {
            typeof(T),
            typeof(TM)
        };
    }

    public T Extreme<TM>(
        int countIn,
        out int countOut,
        T typeGenericInput,
        out T typeGenericOutput,
        ref TM? methodGenericInputThatIsToBeReplacedWithDefault
    )
    {
        typeGenericOutput = typeGenericInput;
        countOut = countIn;
        methodGenericInputThatIsToBeReplacedWithDefault = default(TM);

        return typeGenericInput;
    }
}

public class GenericTestClass<T1, T2>
{
    public int AProperty { get; set; } = 0;
    public int AField = 0;
    
    public Type[] ReturnGenericClassTypes()
    {
        return new[] {
            typeof(T1),
            typeof(T2)
        };
    }

    public static Type[] ReturnGenericClassTypeAndGenericMethodType<TM>()
    {
        return new[] {
            typeof(T1),
            typeof(T2),
            typeof(TM)
        };
    }
}