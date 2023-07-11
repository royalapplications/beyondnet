namespace Beyond.NET.Sample;

public static class OverloadTests
{
    public static void Print(int value) {
        Console.WriteLine(value);
    }
    
    public static void Print(DateTime value) {
        Console.WriteLine(value);
    }
    
    public static void Print(string value) {
        Console.WriteLine(value);
    }
}