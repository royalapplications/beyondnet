using System.Runtime.InteropServices;

namespace Beyond.NET.Core;

internal class ObjCInterop
{
	private const string PATH_LIBOBJC = "/usr/lib/libobjc.A.dylib";
	private const string ENTRYPOINT_OBJC_MSGSEND = "objc_msgSend";

	[DllImport(PATH_LIBOBJC)]
	internal static extern IntPtr sel_registerName(string str);

	[DllImport(PATH_LIBOBJC)]
	internal static extern IntPtr objc_getClass(string name);

	[DllImport(PATH_LIBOBJC, EntryPoint = ENTRYPOINT_OBJC_MSGSEND)]
	internal static extern IntPtr objc_msgSend_RetPtr_1ArgPtr(
		IntPtr target,
		IntPtr selector,
		IntPtr arg1
	);

	[DllImport(PATH_LIBOBJC, EntryPoint = ENTRYPOINT_OBJC_MSGSEND)]
	internal static extern byte objc_msgSend_RetBool_3ArgPtr(
		IntPtr target,
		IntPtr selector,
		IntPtr arg1,
		IntPtr arg2,
		IntPtr arg3
	);

	[DllImport(PATH_LIBOBJC, EntryPoint = ENTRYPOINT_OBJC_MSGSEND)]
	internal static extern void objc_msgSend_RetNone(
		IntPtr target,
		IntPtr selector
	);

	[DllImport(PATH_LIBOBJC, EntryPoint = ENTRYPOINT_OBJC_MSGSEND)]
	internal static extern IntPtr objc_msgSend_RetPtr(
		IntPtr target,
		IntPtr selector
	);

	internal static nint LoadFramework(string path)
	{
		var handle = LibSystem.DL.dlopen(path, (int)LibSystem.RTLD.RTLD_LAZY);

		if (handle == nint.Zero) {
			var error = LibSystem.DL.Error;

			throw new Exception($"Failed to load framework at \"{path}\": {error}");
		}

		return handle;
	}

	internal static void CloseFrameworkHandle(nint handle)
	{
		LibSystem.DL.dlclose(handle);
	}

	internal static nint GetSymbol(nint frameworkHandle, string symbolName)
	{
		if (frameworkHandle == nint.Zero) {
			throw new Exception("Framework handle is null");
		}

		var symbol = LibSystem.DL.dlsym(frameworkHandle, symbolName);

		return symbol;
	}
}