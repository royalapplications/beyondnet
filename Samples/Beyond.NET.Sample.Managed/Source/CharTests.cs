namespace Beyond.NET.Sample.Source;

public static class CharTests
{
    public const char LowercaseA = 'a';
    public const char UppercaseA = 'A';
    public const char One = '1';
    public const char LowercaseUmlautA = 'ä';

    public static void PassInLowercaseAOrThrow(char value)
    {
        if (value != LowercaseA) {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
    
    public static void PassInUppercaseAOrThrow(char value)
    {
        if (value != UppercaseA) {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
    
    public static void PassInOneOrThrow(char value)
    {
        if (value != One) {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
    
    public static void PassInLowercaseUmlautAOrThrow(char value)
    {
        if (value != LowercaseUmlautA) {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}