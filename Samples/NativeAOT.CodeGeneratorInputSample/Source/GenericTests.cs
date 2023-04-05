namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTests
{
    public static Type ReturnGenericType<T>()
    {
        return typeof(T);
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