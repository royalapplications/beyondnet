using System.Runtime.InteropServices;

namespace Beyond.NET.Core;

public class NSString: NSObject
{
    private static nint CLASS => ObjCInterop.objc_getClass("NSString");
    
    private static nint SELECTOR_INITWITHUTF8STRING => ObjCInterop.sel_registerName("initWithUTF8String:");
    private static nint SELECTOR_UTF8STRING => ObjCInterop.sel_registerName("UTF8String");
    private static nint SELECTOR_STRINGBYRESOLVINGSYMLINKSINPATH = ObjCInterop.sel_registerName("stringByResolvingSymlinksInPath");

    internal NSString(nint instance) : base(instance)
    {
    }
    
    public NSString(string str)
    {
        var allocedNSString = Alloc(CLASS);

        var cString = nint.Zero;
        
        try {
            cString = Marshal.StringToHGlobalAuto(str);

            if (cString == nint.Zero) {
                throw new Exception("Failed to convert System.String to C String");
            }
            
            var initedNSString = ObjCInterop.objc_msgSend_RetPtr_1ArgPtr(
                allocedNSString,
                SELECTOR_INITWITHUTF8STRING,
                cString
            );

            if (initedNSString == nint.Zero) {
                ObjCInterop.objc_msgSend_RetNone(
                    allocedNSString,
                    SELECTOR_RELEASE
                );

                allocedNSString = nint.Zero;

                throw new Exception($"Failed to initialize {nameof(NSString)} with C String");
            }

            m_instance = initedNSString;
        } finally {
            if (cString != nint.Zero) {
                Marshal.FreeHGlobal(cString); 
                cString = nint.Zero;
            }
        }
    }

    public NSString? StringByResolvingSymlinksInPath
    {
        get {
            if (m_instance == nint.Zero) {
                throw new Exception($"{nameof(NSString)} is null");
            }

            var resolvedNSStringInstance = ObjCInterop.objc_msgSend_RetPtr(
                m_instance,
                SELECTOR_STRINGBYRESOLVINGSYMLINKSINPATH
            );

            if (resolvedNSStringInstance == nint.Zero) {
                return null;
            }

            var retainedResolvedNSString = ObjCInterop.objc_msgSend_RetPtr(
                resolvedNSStringInstance,
                SELECTOR_RETAIN
            );
            
            if (retainedResolvedNSString == nint.Zero) {
                return null;
            }

            var resolvedNSString = new NSString(retainedResolvedNSString);

            return resolvedNSString;
        }
    }

    public string? UTF8String
    {
        get {
            if (m_instance == nint.Zero) {
                throw new Exception($"{nameof(NSString)} is null");
            }

            nint utf8String = ObjCInterop.objc_msgSend_RetPtr(
                m_instance,
                SELECTOR_UTF8STRING
            );

            if (utf8String == nint.Zero) {
                return null;
            }

            string? csString = Marshal.PtrToStringAuto(utf8String);

            return csString;
        }
    }
}