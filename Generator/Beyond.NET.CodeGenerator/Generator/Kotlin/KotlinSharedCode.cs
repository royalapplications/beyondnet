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
      when (__destroyMode) {
          DestroyMode.Normal -> destroy()
          DestroyMode.Skip -> {}
      }
    }
    
    protected open fun destroy() {
      // Override in subclass
    }
}

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

val com.sun.jna.Pointer.value: Pointer
    get() = this
""";
}