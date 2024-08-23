namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

internal static class KotlinSharedCode
{
    internal const string SharedCode = /*lang=Kt*/"""
open class DNObject(handle: Pointer) {
    enum class DestroyMode {
        Normal,
        Skip
    }
    
    val __handle: Pointer
    var __destroyMode: DestroyMode = DestroyMode.Normal
    
    init {
        require(handle !== Pointer.NULL) {
            "Cannot initialize DNObject with a null pointer"
        }
    
        this.__handle = handle
    }
    
    protected fun finalize() {
        if (__handle == Pointer.NULL) {
            return
        }
        
        when (__destroyMode) {
            DestroyMode.Normal -> destroy()
            DestroyMode.Skip -> {}
        }
    }
    
    protected open fun destroy() {
        // Override in subclass
    }
}

// TODO: Is this correct? Unlikely...
open class UPointer(peer: Long) : Pointer(peer) {
    fun toPointer(): Pointer {
        return Pointer(peer)
    }
}

val com.sun.jna.Pointer.value: Pointer
    get() = this

// Extensions
@JvmName("getHandleOrNullPointer_optional")
fun DNObject?.getHandleOrNullPointer(): Pointer {
    if (this === null) {
        return Pointer.NULL
    }

    return this.__handle
}

@JvmName("getHandleOrNullPointer_non_optional")
fun DNObject.getHandleOrNullPointer(): Pointer {
    return this.__handle
}

fun Pointer.toUPointer(): UPointer {
    return UPointer(Pointer.nativeValue(this))
}
""";

    internal static string GetExtensions(string dnClassName)
    {
        return $$"""
fun System_String.toKString(): String {
    val cString = {{dnClassName}}.DNStringToC(getHandleOrNullPointer())
    val string = cString.getString(0)

    val ptrVal = Pointer.nativeValue(cString)
    Native.free(ptrVal)

    return string
}

fun String.toDotNETString(): System_String {
    val dnStringHandle = {{dnClassName}}.DNStringFromC(this)
    val dnString = System_String(dnStringHandle)

    return dnString
}

fun System_Exception.toKException(): Exception {
    val exStrDN = dnToString() // TODO: Should be message instead of toString
    val exStr = exStrDN.toKString()
    val ex = Exception(exStr)

    return ex
}
""";
    }
}