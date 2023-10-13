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
}