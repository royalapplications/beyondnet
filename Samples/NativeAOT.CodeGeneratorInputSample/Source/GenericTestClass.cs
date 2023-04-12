namespace NativeAOT.CodeGeneratorInputSample;

public class GenericTestClass<T>
{
    public int AProperty { get; set; } = 0;
    
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