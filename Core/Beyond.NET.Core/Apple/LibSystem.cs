using System.Runtime.InteropServices;

namespace Beyond.NET.Core;

internal class LibSystem
{
    internal enum RTLD
    {
        RTLD_LAZY 		= 0x1,
        RTLD_NOW		= 0x2,
        RTLD_LOCAL		= 0x4,
        RTLD_GLOBAL		= 0x8,
        RTLD_NOLOAD 	= 0x10,
        RTLD_NODELETE 	= 0x80,
        RTLD_FIRST 		= 0x100
    }
    
    internal static class DL
    {
        internal const string LIBRARY_SYSTEM = "/usr/lib/libSystem.dylib";
        
        [DllImport(LIBRARY_SYSTEM)]
        internal static extern int dlclose(IntPtr handle);

        [DllImport(LIBRARY_SYSTEM)]
        internal static extern IntPtr dlerror();

        [DllImport(LIBRARY_SYSTEM)]
        internal static extern IntPtr dlopen(string path, int mode);

        [DllImport(LIBRARY_SYSTEM)]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport(LIBRARY_SYSTEM)]
        internal static extern bool dlopen_preflight(string path);

        internal static int Close(IntPtr handle)
        {
            return dlclose(handle);
        }

        internal static string Error
        {
            get {
                var err = dlerror();

                if (err != nint.Zero) {
                    return Marshal.PtrToStringAnsi(err) ?? string.Empty;
                }

                return string.Empty;
            }
        }

        internal static nint Open(string path, RTLD mode)
        {
            var sym = dlopen(path, (int)mode);

            if (sym == nint.Zero) {
                throw new Exception($"Failed to open dynamic library at \"{path}\"", new Exception(Error));
            }

            return sym;
        }

        internal static bool OpenPreFlight(string path)
        {
            return dlopen_preflight(path);
        }

        internal static nint Symbol(nint handle, string symbol)
        {
            var sym = dlsym(handle, symbol);

            if (sym == nint.Zero) {
                throw new Exception($"Failed to get symbol \"{symbol}\"", new Exception(Error));
            }

            return sym;
        }
    }
}