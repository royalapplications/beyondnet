namespace Beyond.NET.Builder.Apple.Nm;

internal class Parser
{
    internal static string[] GetAllRelevantSymbols(string output)
    {
        var lines = output.Split(
            new[] { "\r\n", "\n", "\r" }, 
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
        );

        List<string> symbols = new();

        foreach (var line in lines) {
            var splitLine = line.Split(
                ' ', 
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries
            );

            if (splitLine.Length != 3) {
                // Not relevant
                continue;
            }

            string symbol = splitLine[2];

            if (symbol.StartsWith("___swift") ||
                symbol.StartsWith("__swift")) {
                // Swift internal symbol, not relevant
                continue;
            }
            
            symbols.Add(symbol);
        }

        return symbols.ToArray();
    }
}