using System.Runtime.InteropServices;

namespace NativeAOTLibraryTest;

public static class Main
{
    [UnmanagedCallersOnly(EntryPoint="nativeaotlibrarytest_main_write_to_console")]
    public static void WriteToConsole()
    {
        Console.WriteLine("Hello from .NET 8 Universal NativeAOT library!");
    }
}