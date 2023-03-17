namespace NativeAOT.CodeGeneratorInputSample;

public delegate string StringTransformerDelegate(string inputString);
public delegate double DoublesTransformerDelegate(double number1, double number2);

public static class Transformer
{
    public static string TransformString(
        string inputString,
        StringTransformerDelegate stringTransformerDelegate
    )
    {
        string outputString = stringTransformerDelegate(inputString);

        return outputString;
    }
    
    public static double TransformDoubles(
        double number1,
        double number2,
        DoublesTransformerDelegate doublesTransformerDelegate
    )
    {
        double outputNumber = doublesTransformerDelegate(number1, number2);

        return outputNumber;
    }
}