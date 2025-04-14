namespace Beyond.NET.Sample;

public static class Transformer
{
    public delegate string StringGetterDelegate();
    public delegate string StringTransformerDelegate(string inputString);
    public delegate double DoublesTransformerDelegate(double number1, double number2);

    public static class BuiltInTransformers
    {
        public static StringTransformerDelegate UppercaseStringTransformer { get; set; } =
            inputString => inputString.ToUpper();
    }

    public static string TransformString(
        string inputString,
        StringTransformerDelegate stringTransformer
    )
    {
        string outputString = stringTransformer(inputString);

        return outputString;
    }

    public static double TransformDoubles(
        double number1,
        double number2,
        DoublesTransformerDelegate doublesTransformer
    )
    {
        double outputNumber = doublesTransformer(number1, number2);

        return outputNumber;
    }

    public static string GetAndTransformString(
        StringGetterDelegate stringGetter,
        StringTransformerDelegate stringTransformer
    )
    {
        string originalString = stringGetter();
        string transformedString = stringTransformer(originalString);

        return transformedString;
    }

    public static string UppercaseString(string inputString)
    {
        return BuiltInTransformers.UppercaseStringTransformer(inputString);
    }
}