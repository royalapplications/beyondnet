using System.Runtime.InteropServices;

namespace Beyond.NET.Core;

public class NSURL: NSObject
{
    private static nint CLASS => ObjCInterop.objc_getClass("NSURL");
    
    private static nint SELECTOR_FILEURLWITHPATH = ObjCInterop.sel_registerName("fileURLWithPath:");
    private static nint SELECTOR_PATH = ObjCInterop.sel_registerName("path");
    private static nint SELECTOR_GETRESOURCEVALUEFORKEYERROR = ObjCInterop.sel_registerName("getResourceValue:forKey:error:");

    public static NSString NSURLCanonicalPathKey
    {
        get {
            nint foundationFrameworkHandle = nint.Zero;
            
            try {
                foundationFrameworkHandle = ObjCInterop.LoadFramework("/System/Library/Frameworks/Foundation.framework/Foundation");

                if (foundationFrameworkHandle != nint.Zero) {
                    var keySymbol = ObjCInterop.GetSymbol(foundationFrameworkHandle, nameof(NSURLCanonicalPathKey));

                    if (keySymbol != nint.Zero) {
                        nint inst = Marshal.ReadIntPtr(keySymbol);

                        if (inst != nint.Zero) {
                            var key = new NSString(inst);
    
                            return key;
                        }
                    }
                }
            } finally {
                if (foundationFrameworkHandle != nint.Zero) {
                    ObjCInterop.CloseFrameworkHandle(foundationFrameworkHandle);
                }
            }

            throw new Exception($"Failed to get {nameof(NSURLCanonicalPathKey)}");
        }
    }
    
    internal NSURL(nint instance) : base(instance)
    {
    }

    public NSURL(NSString path)
    {
        var urlInst = ObjCInterop.objc_msgSend_RetPtr_1ArgPtr(
            CLASS,
            SELECTOR_FILEURLWITHPATH,
            path.m_instance
        );

        if (urlInst == nint.Zero) {
            throw new Exception($"Failed to initialize {nameof(NSURL)} with {nameof(NSString)}");
        }
        
        m_instance = urlInst;
        
        Retain();
    }

    public NSString? Path
    {
        get {
            if (m_instance == nint.Zero) {
                throw new Exception($"{nameof(NSURL)} is null");
            }
            
            var pathInst = ObjCInterop.objc_msgSend_RetPtr(
                m_instance,
                SELECTOR_PATH
            );

            if (pathInst == nint.Zero) {
                return null;
            }

            var path = new NSString(pathInst);

            return path;
        }
    }

    public NSString? GetResourceValue(NSString urlResourceKey)
    {
        var valuePtr = Marshal.AllocHGlobal(nint.Size);
        
        try {
            var success = ObjCInterop.objc_msgSend_RetBool_3ArgPtr(
                m_instance,
                SELECTOR_GETRESOURCEVALUEFORKEYERROR,
                valuePtr,
                urlResourceKey.m_instance,
                nint.Zero
            ) == 1;

            if (!success ||
                valuePtr == nint.Zero) {
                return null;
            }

            var value = Marshal.ReadIntPtr(valuePtr);

            if (value == nint.Zero) {
                return null;
            }

            var valueNS = new NSString(value);

            return valueNS;
        } finally {
            if (valuePtr != nint.Zero) {
                Marshal.FreeHGlobal(valuePtr);
            }
        }
    }
}