namespace Beyond.NET.Sample.Source;

public class OperatorTests
{
    public static OperatorTests operator +(OperatorTests a, string b)
    {
        return a;
    }

    public void operator +=(string b)
    {

    }
}
