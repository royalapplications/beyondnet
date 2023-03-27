namespace NativeAOT.CodeGeneratorInputSample;

public delegate string StringGetterDelegate();
public delegate string StringTransformerDelegate(string inputString);
public delegate double DoublesTransformerDelegate(double number1, double number2);

public static class Transformer
{
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
}