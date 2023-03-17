namespace NativeAOT.CodeGeneratorInputSample;

public delegate string StringTransformerDelegate(string inputString);

public static class StringTransformer
{
    public static string TransformString(
        string inputString,
        StringTransformerDelegate stringTransformerDelegate
    )
    {
        string outputString = stringTransformerDelegate(inputString);

        return outputString;
    }
}