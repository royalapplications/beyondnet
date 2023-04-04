namespace NativeAOT.CodeGeneratorInputSample;

public enum TestEnum
{
    FirstCase,
    SecondCase
}

public delegate ref int ByRefReturnValueDelegate();
public delegate void ByRefParametersDelegate(ref int byRefInt);
public delegate void OutParametersDelegate(out int outInt);

public class TestClass
{
    public void SayHello()
    {
        Console.WriteLine(GetHello());
    }
    
    public void SayHello(string name)
    {
        Console.WriteLine(GetHello() + " " + name);
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
    
    public int Divide(int number1, int number2)
    {
        return number1 / number2;
    }

    public static string GetTestEnumName(TestEnum testEnum)
    {
        return testEnum.ToString();
    }

    public int ModifyByRefValueAndReturnOriginalValue(ref int valueToModify, int targetValue)
    {
        int originalValue = valueToModify;
        
        valueToModify = targetValue;

        return originalValue;
    }
}
