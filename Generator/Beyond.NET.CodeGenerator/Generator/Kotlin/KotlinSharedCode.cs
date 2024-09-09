namespace Beyond.NET.CodeGenerator.Generator.Kotlin;

internal static class KotlinSharedCode
{
    internal const string SharedCode = /*lang=Kt*/"""
interface IDNObject {
    val __handle: Pointer

    fun destroy()
}

open class DNObject(handle: Pointer): IDNObject {
    enum class DestroyMode {
        Normal,
        Skip
    }
    
    override val __handle: Pointer
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
    
    override fun destroy() {
        // Override in subclass
    }
}

public class DNException(public val dnException: System_Exception) : Exception(try { dnException.message_get().toKString() } catch (e: Exception) { "Error while getting System.Exception.Message" }) {
    public val dnStackTrace: String?
        get() {
            return try {
                dnException.stackTrace_get()?.toKString()
            } catch (e: Exception) {
                null
            }
        }

    private val exceptionToString: String? = try {
        dnException.dnToString().toKString()
    } catch (e: Exception) {
        null
    }

    override fun toString(): String {
        exceptionToString?.let {
            return exceptionToString
        }

        return super.toString()
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
fun IDNObject?.getHandleOrNullPointer(): Pointer {
    if (this === null) {
        return Pointer.NULL
    }

    return this.__handle
}

@JvmName("getHandleOrNullPointer_non_optional")
fun IDNObject.getHandleOrNullPointer(): Pointer {
    return this.__handle
}

fun Pointer.toUPointer(): UPointer {
    return UPointer(Pointer.nativeValue(this))
}
""";

    internal static string GetExtensions(string jnaClassName)
    {
        return /*lang=Kt*/$$"""
fun IDNObject.castTo(type: System_Type): System_Object? {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = {{jnaClassName}}.DNObjectCastTo(this.__handle, type.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    if (__returnValueC == Pointer.NULL) {
        return null
    }
    
    val __returnValue = System_Object(__returnValueC)

    return __returnValue
}

// TODO: This uses reflection - no good
inline fun <reified T> IDNObject.castTo(): T where T : System_Object {
    val tClass = T::class.java
    val tClassTypeOfMethod = tClass.getMethod("typeOf")
    val tClassTypeOf = tClassTypeOfMethod.invoke(null) as System_Type
    val tClassConstructor = tClass.getDeclaredConstructor(Pointer::class.java)

    val __exceptionC = PointerByReference()

    val __returnValueC = CAPI.DNObjectCastTo(this.__handle, tClassTypeOf.__handle, __exceptionC)

    val __exceptionCHandle = __exceptionC.value

    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }

    val __returnValue = tClassConstructor.newInstance(__returnValueC)

    return __returnValue
}

// TODO: This uses reflection - no good
inline fun <reified T> IDNObject.castAs(): T? where T : System_Object {
    val tClass = T::class.java
    val tClassTypeOfMethod = tClass.getMethod("typeOf")
    val tClassTypeOf = tClassTypeOfMethod.invoke(null) as System_Type
    val tClassConstructor = tClass.getDeclaredConstructor(Pointer::class.java)

    val __returnValueC = CAPI.DNObjectCastAs(this.__handle, tClassTypeOf.__handle)

    if (__returnValueC == Pointer.NULL) {
        return null
    }

    val __returnValue = tClassConstructor.newInstance(__returnValueC)

    return __returnValue
}

fun IDNObject.`is`(type: System_Type): Boolean {
    return {{jnaClassName}}.DNObjectIs(this.__handle, type.__handle)
}

/// Cast the targeted .NET object to a Bool.
/// - Returns: A Bool value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToBool(): Boolean {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToBool(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Bool value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Boolean.toDotNETObject(): System_Boolean {
    val castedObjectC = CAPI.DNObjectFromBool(this)
    
    return System_Boolean(castedObjectC)
}

/// Cast the targeted .NET object to a Float.
/// - Returns: A Float value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToFloat(): Float {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToFloat(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Float value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Float.toDotNETObject(): System_Single {
    val castedObjectC = CAPI.DNObjectFromFloat(this)
    
    return System_Single(castedObjectC)
}

/// Cast the targeted .NET object to a Double.
/// - Returns: A Double value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToDouble(): Double {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToDouble(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Double value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Double.toDotNETObject(): System_Double {
    val castedObjectC = CAPI.DNObjectFromDouble(this)
    
    return System_Double(castedObjectC)
}

/// Cast the targeted .NET object to a Byte.
/// - Returns: A Byte value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToByte(): Byte {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToInt8(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Byte value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Byte.toDotNETObject(): System_SByte {
    val castedObjectC = CAPI.DNObjectFromInt8(this)
    
    return System_SByte(castedObjectC)
}

/// Cast the targeted .NET object to a UByte.
/// - Returns: A UByte value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToUByte(): UByte {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToUInt8(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC.toUByte()
}

/// Boxes the specified UByte value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun UByte.toDotNETObject(): System_Byte {
    val castedObjectC = CAPI.DNObjectFromUInt8(this.toByte())
    
    return System_Byte(castedObjectC)
}

/// Cast the targeted .NET object to a Short.
/// - Returns: A Short value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToShort(): Short {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToInt16(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Short value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Short.toDotNETObject(): System_Int16 {
    val castedObjectC = CAPI.DNObjectFromInt16(this)
    
    return System_Int16(castedObjectC)
}

/// Cast the targeted .NET object to a UShort.
/// - Returns: A UShort value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToUShort(): UShort {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToUInt16(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC.toUShort()
}

/// Boxes the specified UShort value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun UShort.toDotNETObject(): System_UInt16 {
    val castedObjectC = CAPI.DNObjectFromUInt16(this.toShort())
    
    return System_UInt16(castedObjectC)
}

/// Cast the targeted .NET object to an Int.
/// - Returns: An Int value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToInt(): Int {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToInt32(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Int value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Int.toDotNETObject(): System_Int32 {
    val castedObjectC = CAPI.DNObjectFromInt32(this)
    
    return System_Int32(castedObjectC)
}

/// Cast the targeted .NET object to an UInt.
/// - Returns: An UInt value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToUInt(): UInt {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToUInt32(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC.toUInt()
}

/// Boxes the specified UInt value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun UInt.toDotNETObject(): System_UInt32 {
    val castedObjectC = CAPI.DNObjectFromUInt32(this.toInt())
    
    return System_UInt32(castedObjectC)
}

/// Cast the targeted .NET object to a Long.
/// - Returns: A Long value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToLong(): Long {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToInt64(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Long value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Long.toDotNETObject(): System_Int64 {
    val castedObjectC = CAPI.DNObjectFromInt64(this)
    
    return System_Int64(castedObjectC)
}

/// Cast the targeted .NET object to a ULong.
/// - Returns: A ULong value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToULong(): ULong {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToUInt64(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle !== Pointer.NULL) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC.toULong()
}

/// Boxes the specified ULong value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun ULong.toDotNETObject(): System_UInt64 {
    val castedObjectC = CAPI.DNObjectFromUInt64(this.toLong())
    
    return System_UInt64(castedObjectC)
}

fun System_String.toKString(): String {
    val cString = {{jnaClassName}}.DNStringToC(getHandleOrNullPointer())
    val string = cString.getString(0)

    val ptrVal = Pointer.nativeValue(cString)
    Native.free(ptrVal)

    return string
}

fun String.toDotNETString(): System_String {
    val dnStringHandle = {{jnaClassName}}.DNStringFromC(this)
    val dnString = System_String(dnStringHandle)

    return dnString
}

fun System_Exception.toKException(): Exception {
    return DNException(this)
}
""";
    }
}