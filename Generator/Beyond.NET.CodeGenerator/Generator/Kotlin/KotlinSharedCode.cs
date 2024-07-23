namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

internal static class KotlinSharedCode
{
    internal const string SharedCode = """
open class DNObject(handle: Pointer) {
   enum class DestroyMode {
       Normal,
       Skip
   }

   val handle: Pointer
   var destroyMode: DestroyMode = DestroyMode.Normal

   init {
       require(handle !== Pointer.NULL) {
           "Cannot initialize DNObject with a null pointer"
       }

       this.handle = handle
   }

   protected fun finalize() {
       when (destroyMode) {
           DestroyMode.Normal -> destroy()
           DestroyMode.Skip -> {}
       }
   }

   protected open fun destroy() {
       // Override in subclass
   }
}
""";
}