using System.Runtime.InteropServices;

namespace Beyond.NET.Core;

public class NSString: IDisposable
{
    private IntPtr m_instance;
    
    private static IntPtr CLASS_NSSTRING => ObjCInterop.objc_getClass("NSString");
    
    private static IntPtr SELECTOR_ALLOC => ObjCInterop.sel_registerName("alloc");
    private static IntPtr SELECTOR_INITWITHUTF8STRING => ObjCInterop.sel_registerName("initWithUTF8String:");
    private static IntPtr SELECTOR_RETAIN => ObjCInterop.sel_registerName("retain");
    private static IntPtr SELECTOR_RELEASE => ObjCInterop.sel_registerName("release");
    private static IntPtr SELECTOR_UTF8STRING => ObjCInterop.sel_registerName("UTF8String");
    private static IntPtr SELECTOR_STRINGBYRESOLVINGSYMLINKSINPATH = ObjCInterop.sel_registerName("stringByResolvingSymlinksInPath");

    private NSString(IntPtr instance)
    {
        if (instance == IntPtr.Zero) {
            throw new Exception("Passed in NSString reference is null");
        }
        
        m_instance = instance;
    }

    public NSString(string str)
    {
        IntPtr allocedNSString = ObjCInterop.objc_msgSend_RetPtr(
            CLASS_NSSTRING,
            SELECTOR_ALLOC
        );

        if (allocedNSString == IntPtr.Zero) {
            throw new Exception("Failed to allocate NSString");
        }

        IntPtr cString = IntPtr.Zero;
        
        try {
            cString = Marshal.StringToHGlobalAuto(str);

            if (cString == IntPtr.Zero) {
                throw new Exception("Failed to convert System.String to C String");
            }
            
            IntPtr initedNSString = ObjCInterop.objc_msgSend_RetPtr_1ArgPtr(
                allocedNSString,
                SELECTOR_INITWITHUTF8STRING,
                cString
            );

            if (initedNSString == IntPtr.Zero) {
                ObjCInterop.objc_msgSend_RetNone(
                    allocedNSString,
                    SELECTOR_RELEASE
                );

                allocedNSString = IntPtr.Zero;

                throw new Exception("Failed to initialize NSString with C String");
            }

            m_instance = initedNSString;
        } finally {
            if (cString != IntPtr.Zero) {
                Marshal.FreeHGlobal(cString); 
                cString = IntPtr.Zero;
            }
        }
    }

    public NSString? StringByResolvingSymlinksInPath
    {
        get {
            if (m_instance == IntPtr.Zero) {
                throw new Exception("NSString is null");
            }

            IntPtr resolvedNSStringInstance = ObjCInterop.objc_msgSend_RetPtr(
                m_instance,
                SELECTOR_STRINGBYRESOLVINGSYMLINKSINPATH
            );

            if (resolvedNSStringInstance == IntPtr.Zero) {
                return null;
            }

            IntPtr retainedResolvedNSString = ObjCInterop.objc_msgSend_RetPtr(
                resolvedNSStringInstance,
                SELECTOR_RETAIN
            );
            
            if (retainedResolvedNSString == IntPtr.Zero) {
                return null;
            }

            NSString resolvedNSString = new(retainedResolvedNSString);

            return resolvedNSString;
        }
    }

    public string? UTF8String
    {
        get {
            if (m_instance == IntPtr.Zero) {
                throw new Exception("NSString is null");
            }

            IntPtr utf8String = ObjCInterop.objc_msgSend_RetPtr(
                m_instance,
                SELECTOR_UTF8STRING
            );

            if (utf8String == IntPtr.Zero) {
                return null;
            }

            string? csString = Marshal.PtrToStringAuto(utf8String);

            return csString;
        }
    }
    
    public void Dispose()
    {
        var instance = m_instance;
        m_instance = IntPtr.Zero;
        
        if (instance == IntPtr.Zero) {
            return;
        }
        
        ObjCInterop.objc_msgSend_RetNone(
            instance,
            SELECTOR_RELEASE
        );
    }
}