namespace Beyond.NET.Sample;

public class AsyncTests
{
    public async Task<int> AddAsync(
        int number1,
        int number2
    )
    {
        var task = Task<int>.Factory.StartNew(() => number1 + number2);
        var result = await task;

        return result;
    }

    public delegate int TransformerDelegate(
        int number1,
        int number2
    );

    public async Task<int> TransformNumbersAsync(
        int number1,
        int number2,
        TransformerDelegate transformerDelegate
    )
    {
        var task = Task<int>.Factory.StartNew(() => transformerDelegate(number1, number2));
        
        var result = await task;

        return result;
    }
}