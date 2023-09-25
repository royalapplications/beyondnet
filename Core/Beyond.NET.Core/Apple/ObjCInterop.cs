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
	internal static extern void objc_msgSend_RetNone(
		IntPtr target,
		IntPtr selector
	);
	
	[DllImport(PATH_LIBOBJC, EntryPoint = ENTRYPOINT_OBJC_MSGSEND)]
	internal static extern IntPtr objc_msgSend_RetPtr(
		IntPtr target,
		IntPtr selector
	);
}