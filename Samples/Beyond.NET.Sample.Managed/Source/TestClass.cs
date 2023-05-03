namespace Beyond.NET.Sample;

public enum TestEnum
{
    FirstCase,
    SecondCase
}

public delegate ref int ByRefReturnValueDelegate();
public delegate void ByRefParametersDelegate(ref int byRefInt);
public delegate void OutParametersDelegate(out int outInt);
public delegate char CharReturnerDelegate();

public class Book
{
    public string Name { get; }

    public static readonly Book DonQuixote = new("Don Quixote");
    public static readonly Book ATaleOfTwoCities = new("A Tale of Two Cities");
    public static readonly Book TheLordOfTheRings = new("The Lord of the Rings");

    public Book(string name)
    {
        Name = name;
    }
}

public class TestClass
{
    public Book CurrentBook = Book.DonQuixote;
    public int CurrentIntValue;

    public char GetChar(CharReturnerDelegate charReturnerDelegate)
    {
        return charReturnerDelegate();
    }
    
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

    public void ModifyByRefEnum(ref TestEnum enumToModify)
    {
        if (enumToModify == TestEnum.FirstCase) {
            enumToModify = TestEnum.SecondCase;
        } else if (enumToModify == TestEnum.SecondCase) {
            enumToModify = TestEnum.FirstCase;
        } else {
            throw new Exception("Unknown enum case");
        }
    }

    public void ModifyByRefBookAndReturnOriginalBookAsOutParameter(ref Book bookToModify, Book targetBook, out Book originalBook)
    {
        originalBook = bookToModify;
        bookToModify = targetBook;
    }

    public ref Book GetCurrentBookByRef()
    {
        return ref CurrentBook;
    }

    public ref int IncreaseAndGetCurrentIntValueByRef()
    {
        CurrentIntValue += 1;
        
        return ref CurrentIntValue;
    }
}
