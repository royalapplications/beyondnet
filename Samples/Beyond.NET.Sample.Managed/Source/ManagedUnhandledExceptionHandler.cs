namespace Beyond.NET.Sample.Source;

public static class ManagedUnhandledExceptionHandler
{
    public static void Install()
    {
        AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;
    }

    public static void Uninstall()
    {
        AppDomain.CurrentDomain.UnhandledException -= HandleUnhandledException;
    }

    private static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Console.WriteLine("Unhandled .NET Exception:");
        Console.WriteLine(e.ExceptionObject.ToString());
    }
}