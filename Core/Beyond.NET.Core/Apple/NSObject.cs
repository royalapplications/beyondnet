namespace Beyond.NET.Core;

public class NSObject: IDisposable
{
    internal nint m_instance;

    private static nint CLASS => ObjCInterop.objc_getClass("NSObject");

    internal static nint SELECTOR_ALLOC => ObjCInterop.sel_registerName("alloc");
    internal static nint SELECTOR_INIT => ObjCInterop.sel_registerName("init");
    internal static nint SELECTOR_RETAIN => ObjCInterop.sel_registerName("retain");
    internal static nint SELECTOR_RELEASE => ObjCInterop.sel_registerName("release");

    internal NSObject(nint instance)
    {
        if (instance == nint.Zero) {
            throw new Exception($"Passed in {nameof(NSObject)} reference is null");
        }

        m_instance = instance;
    }

    protected NSObject() : this(AllocAndInit())
    {
    }

    private static nint AllocAndInit()
    {
        var alloced = Alloc();
        var inited = Init(alloced);

        return inited;
    }

    private static nint Alloc()
    {
        return Alloc(CLASS);
    }

    protected static nint Alloc(nint klass)
    {
        var allocedObj = ObjCInterop.objc_msgSend_RetPtr(
            klass,
            SELECTOR_ALLOC
        );

        if (allocedObj == nint.Zero) {
            throw new Exception("Failed to allocate");
        }

        return allocedObj;
    }

    protected static nint Init(nint obj)
    {
        var initedObj = ObjCInterop.objc_msgSend_RetPtr(
            obj,
            SELECTOR_INIT
        );

        if (initedObj == nint.Zero) {
            throw new Exception($"Failed to init {nameof(NSObject)}");
        }

        return initedObj;
    }

    protected void Retain()
    {
        if (m_instance == nint.Zero) {
            return;
        }

        ObjCInterop.objc_msgSend_RetPtr(
            m_instance,
            SELECTOR_RETAIN
        );
    }

    public void Dispose()
    {
        var instance = m_instance;
        m_instance = nint.Zero;

        if (instance == nint.Zero) {
            return;
        }

        ObjCInterop.objc_msgSend_RetNone(
            instance,
            SELECTOR_RELEASE
        );
    }
}