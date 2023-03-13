namespace NativeAOT.CodeGeneratorInputSample;

public class TestClass
{
    public delegate void SimpleDelegate();

    public void CallSimpleDelegate(SimpleDelegate simpleDelegate)
    {
        simpleDelegate();
    }
    
    public void SayHello()
    {
        Console.WriteLine(GetHello());
    }

    public string GetHello()
    {
        return "Hello";
    }

    public DateTime GetDate()
    {
        return DateTime.Now;
    }

    public int Add(int number1, int number2)
    {
        return number1 + number2;
    }
}
