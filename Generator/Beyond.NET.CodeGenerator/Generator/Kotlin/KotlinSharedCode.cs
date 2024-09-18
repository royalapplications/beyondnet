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
        require(handle != null) {
            "Cannot initialize DNObject with a null pointer"
        }
    
        this.__handle = handle
    }
    
    protected fun finalize() {
        if (__handle == null) {
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

public class DNOutParameter {
    companion object {
        fun <T> createPlaceholder(factory: (Pointer) -> T): T where T: DNObject {
            // TODO: Not sure why SIZE is not available: https://java-native-access.github.io/jna/4.2.1/com/sun/jna/Pointer.html#SIZE
//            val sizeOfPointer = com.sun.jna.Pointer.SIZE
            val sizeOfPointer: Long = 8

            val mem = Memory(sizeOfPointer)
            val ptr = mem.value

            val obj = factory(ptr)
            obj.__destroyMode = DNObject.DestroyMode.Skip

            return obj
        }
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
@JvmName("getHandleOrNull_optional")
fun IDNObject?.getHandleOrNull(): Pointer? {
    this?.let {
        return this.__handle
    }
    
    return null
}

@JvmName("getHandleOrNull_non_optional")
fun IDNObject.getHandleOrNull(): Pointer {
    return this.__handle
}

fun Pointer.toUPointer(): UPointer {
    return UPointer(Pointer.nativeValue(this))
}

// TODO: Is this correct? Unlikely
public class UPointerByReference : PointerByReference {
    constructor(value: UPointer) : super(value.toPointer()) {
        
    }

    constructor() : super() {
        
    }
}

public class BooleanByReference : ByReference {
    constructor(value: Boolean) : super(1) {
        this.value = value
    }

    constructor() : super(1) {
        this.value = false
    }

    public var value: Boolean
        get() {
            val valueByte = pointer.getByte(0)

            val byteOne: Byte = 1
            val valueBoolean: Boolean = valueByte == byteOne

            return valueBoolean
        }
        set(value) {
            val valueByte: Byte = if (value) 1 else 0

            pointer.setByte(0, valueByte)
        }
}

public class UByteByReference : ByReference {
    constructor(value: UByte) : super(UByte.SIZE_BYTES) {
        this.value = value
    }

    constructor() : super(UByte.SIZE_BYTES) {
        this.value = 0u
    }

    // TODO: Is this correct?
    public var value: UByte
        get() {
            val valueByte = pointer.getByte(0)
            val valueUByte = valueByte.toUByte()

            return valueUByte
        }
        set(value) {
            val valueByte = value.toByte()

            pointer.setByte(0, valueByte)
        }
}

public class UShortByReference : ByReference {
    constructor(value: UShort) : super(UShort.SIZE_BYTES) {
        this.value = value
    }

    constructor() : super(UShort.SIZE_BYTES) {
        this.value = 0u
    }

    // TODO: Is this correct?
    public var value: UShort
        get() {
            val valueShort = pointer.getShort(0)
            val valueUShort = valueShort.toUShort()

            return valueUShort
        }
        set(value) {
            val valueShort = value.toShort()

            pointer.setShort(0, valueShort)
        }
}

public class UIntByReference : ByReference {
    constructor(value: UInt) : super(UInt.SIZE_BYTES) {
        this.value = value
    }

    constructor() : super(UInt.SIZE_BYTES) {
        this.value = 0u
    }

    // TODO: Is this correct?
    public var value: UInt
        get() {
            val valueInt = pointer.getInt(0)
            val valueUInt = valueInt.toUInt()

            return valueUInt
        }
        set(value) {
            val valueInt = value.toInt()

            pointer.setInt(0, valueInt)
        }
}

public class ULongByReference : ByReference {
    constructor(value: ULong) : super(ULong.SIZE_BYTES) {
        this.value = value
    }

    constructor() : super(ULong.SIZE_BYTES) {
        this.value = 0u
    }

    // TODO: Is this correct?
    public var value: ULong
        get() {
            val valueLong = pointer.getLong(0)
            val valueULong = valueLong.toULong()

            return valueULong
        }
        set(value) {
            val valueLong = value.toLong()

            pointer.setLong(0, valueLong)
        }
}

public interface IRef {
    fun toJNARef(): ByReference
}

public class ObjectRef<T>(var value: T): IRef where T: IDNObject? {
    override fun toJNARef(): PointerByReference {
        return PointerByReference(value.getHandleOrNull())
    }
}

inline fun <reified T> T.toRef(): ObjectRef<T> where T: IDNObject? {
    return ObjectRef(this)
}

inline fun <reified T> T?.toOptionalRef(): ObjectRef<T?> where T: IDNObject? {
    return ObjectRef(this)
}

public class BooleanRef(var value: Boolean): IRef {
    override fun toJNARef(): BooleanByReference {
        return BooleanByReference(value)
    }
}

fun Boolean.toRef(): BooleanRef {
    return BooleanRef(this)
}

public class ByteRef(var value: Byte): IRef {
    override fun toJNARef(): ByteByReference {
        return ByteByReference(value)
    }
}

fun Byte.toRef(): ByteRef {
    return ByteRef(this)
}

public class UByteRef(var value: UByte): IRef {
    override fun toJNARef(): UByteByReference {
        return UByteByReference(value)
    }
}

fun UByte.toRef(): UByteRef {
    return UByteRef(this)
}

public class ShortRef(var value: Short): IRef {
    override fun toJNARef(): ShortByReference {
        return ShortByReference(value)
    }
}

fun Short.toRef(): ShortRef {
    return ShortRef(this)
}

public class UShortRef(var value: UShort): IRef {
    override fun toJNARef(): UShortByReference {
        return UShortByReference(value)
    }
}

fun UShort.toRef(): UShortRef {
    return UShortRef(this)
}

public class IntRef(var value: Int): IRef {
    override fun toJNARef(): IntByReference {
        return IntByReference(value)
    }
}

fun Int.toRef(): IntRef {
    return IntRef(this)
}

public class UIntRef(var value: UInt): IRef {
    override fun toJNARef(): UIntByReference {
        return UIntByReference(value)
    }
}

fun UInt.toRef(): UIntRef {
    return UIntRef(this)
}

public class LongRef(var value: Long): IRef {
    override fun toJNARef(): LongByReference {
        return LongByReference(value)
    }
}

fun Long.toRef(): LongRef {
    return LongRef(this)
}

public class ULongRef(var value: ULong): IRef {
    override fun toJNARef(): ULongByReference {
        return ULongByReference(value)
    }
}

fun ULong.toRef(): ULongRef {
    return ULongRef(this)
}

public class FloatRef(var value: Float): IRef {
    override fun toJNARef(): FloatByReference {
        return FloatByReference(value)
    }
}

fun Float.toRef(): FloatRef {
    return FloatRef(this)
}

public class DoubleRef(var value: Double): IRef {
    override fun toJNARef(): DoubleByReference {
        return DoubleByReference(value)
    }
}

fun Double.toRef(): DoubleRef {
    return DoubleRef(this)
}

// TODO: This uses reflection - no good
open class DNArray<T: System_Object>(handle: Pointer, klassOfT: Class<T>)
    : System_Array(handle), Iterable<T> {
    //    private val systemTypeOfT = klassOfT.getMethod("typeOf").invoke(null) as System_Type
    private val constructorOfT = klassOfT.getDeclaredConstructor(Pointer::class.java)

    companion object {
        // Constructor that creates an empty array
        public inline fun <reified T: System_Object> empty(): DNArray<T> {
            return invoke<T>(0)
        }

        // Constructor with optional length parameter
        public inline operator fun <reified T: System_Object> invoke(length: Int = 0): DNArray<T> {
            val klassOfT = T::class.java
            val systemTypeOfT = klassOfT.getMethod("typeOf").invoke(null) as System_Type

            val __exceptionC = PointerByReference()

            val systemArrayHandle = CAPI.System_Array_CreateInstance(systemTypeOfT.__handle, length, __exceptionC)

            val __exceptionCHandle = __exceptionC.value

            if (__exceptionCHandle != null) {
                throw System_Exception(__exceptionCHandle).toKException()
            }

            val dnArray = DNArray(systemArrayHandle, klassOfT)

            return dnArray
        }
    }

    // Subscript getter
    public operator fun get(index: Int): T {
        val __exceptionC = PointerByReference()

        val valueHandle = CAPI.System_Array_GetValue_1(__handle, index, __exceptionC)
            ?: throw NullPointerException()

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val castedValue = constructorOfT.newInstance(valueHandle)

        return castedValue
    }

    // Subscript setter
    public operator fun set(index: Int, value: T) {
        setValue(value, index)
    }

    // Iterator/foreach loop support
    override fun iterator() = object: Iterator<T> {
        private val count = this@DNArray.count_get()
        private var currentIndex: Int = -1

        override fun hasNext(): Boolean {
            if (count <= 0) {
                return false
            }

            val idx = currentIndex
            val hasIt = idx < count - 1

            return hasIt
        }

        override fun next(): T {
            currentIndex += 1

            return this@DNArray[currentIndex]
        }
    }
}

// TODO: This uses reflection - no good
open class DNNullableArray<T: System_Object?>(handle: Pointer, klassOfT: Class<T>)
    : System_Array(handle), Iterable<T?> {
//    private val systemTypeOfT = klassOfT.getMethod("typeOf").invoke(null) as System_Type
    private val constructorOfT = klassOfT.getDeclaredConstructor(Pointer::class.java)

    companion object {
        // Constructor that creates an empty nullable array
        public inline fun <reified T: System_Object?> empty(): DNNullableArray<T> {
            return invoke<T>(0)
        }

        // Constructor with optional length parameter
        public inline operator fun <reified T: System_Object?> invoke(length: Int = 0): DNNullableArray<T> {
            val klassOfT = T::class.java
            val systemTypeOfT = klassOfT.getMethod("typeOf").invoke(null) as System_Type

            val __exceptionC = PointerByReference()

            val systemArrayHandle = CAPI.System_Array_CreateInstance(systemTypeOfT.__handle, length, __exceptionC)

            val __exceptionCHandle = __exceptionC.value

            if (__exceptionCHandle != null) {
                throw System_Exception(__exceptionCHandle).toKException()
            }

            val dnNullableArray = DNNullableArray(systemArrayHandle, klassOfT)

            return dnNullableArray
        }
    }

    // Subscript getter, returns nullable type
    public operator fun get(index: Int): T? {
        val __exceptionC = PointerByReference()

        val valueHandle = CAPI.System_Array_GetValue_1(__handle, index, __exceptionC)

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        // If valueHandle is null, return null
        if (valueHandle == null) {
            return null
        }

        // Otherwise, construct the casted object
        val castedValue = constructorOfT.newInstance(valueHandle)

        return castedValue
    }

    // Subscript setter, can set nullable value
    public operator fun set(index: Int, value: T?) {
        setValue(value, index)
    }

    // Iterator/foreach loop support, works with nullable types
    override fun iterator() = object : Iterator<T?> {
        private val count = this@DNNullableArray.count_get()
        private var currentIndex: Int = -1

        override fun hasNext(): Boolean {
            if (count <= 0) {
                return false
            }

            val idx = currentIndex
            val hasIt = idx < count - 1

            return hasIt
        }

        override fun next(): T? {
            currentIndex += 1

            // Handle nullable types properly
            return this@DNNullableArray[currentIndex]
        }
    }
}
""";

    internal static string GetExtensions(string jnaClassName)
    {
        return /*lang=Kt*/$$"""
fun IDNObject.castTo(type: System_Type): System_Object? {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = {{jnaClassName}}.DNObjectCastTo(this.__handle, type.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle != null) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    if (__returnValueC == null) {
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

    if (__exceptionCHandle != null) {
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

    if (__returnValueC == null) {
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
    
    if (__exceptionCHandle != null) {
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

/// Cast the targeted .NET object to a Char.
/// - Returns: A Char value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToChar(): Char {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToChar(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle != null) {
        throw System_Exception(__exceptionCHandle).toKException()
    }
    
    return __returnValueC
}

/// Boxes the specified Char value in an .NET object.
/// - Returns: An .NET object containing the boxed value.
fun Char.toDotNETObject(): System_Char {
    val castedObjectC = CAPI.DNObjectFromChar(this)
    
    return System_Char(castedObjectC)
}

/// Cast the targeted .NET object to a Float.
/// - Returns: A Float value if the cast succeeded.
/// - Throws: If the cast fails, an exception is thrown.
fun IDNObject.castToFloat(): Float {
    val __exceptionC = PointerByReference()
    
    val __returnValueC = CAPI.DNObjectCastToFloat(this.__handle, __exceptionC)
    
    val __exceptionCHandle = __exceptionC.value
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    
    if (__exceptionCHandle != null) {
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
    val cString = {{jnaClassName}}.DNStringToC(getHandleOrNull())
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

// TODO: This can be optimized (see Swift impl)
fun System_Guid.toUUID(): UUID {
    val guidStrDN = this.dnToString()
    val guidStr = guidStrDN.toKString()

    val uuid = UUID.fromString(guidStr)

    return uuid
}

// TODO: This can be optimized (see Swift impl)
fun UUID.toDotNETGuid(): System_Guid {
    val uuidStr = this.toString()
    val uuidStrDN = uuidStr.toDotNETString()

    val guid = System_Guid(uuidStrDN)

    return guid
}

fun System_DateTime.toKDate(): Date {
    val dateTimeKind = kind_get()

    if (dateTimeKind == System_DateTimeKind.UNSPECIFIED) {
        throw Exception("DateTimeKind.Unspecified cannot be safely converted")
    }

    val universalDateTime = toUniversalTime()
    val offset = System_DateTimeOffset(universalDateTime)
    val unixTime = offset.toUnixTimeMilliseconds()

    val date = Date(unixTime)

    return date
}

fun Date.toDotNETDateTime(): System_DateTime {
    val unixTime = this.time
    val offset = System_DateTimeOffset.fromUnixTimeMilliseconds(unixTime)
    val dateTime = offset.utcDateTime_get()

    return dateTime
}

@OptIn(ExperimentalUnsignedTypes::class)
fun DNArray<System_Byte>.toKUByteArray(): UByteArray {
    val len = this.length_get()

    if (len <= 0) {
        return UByteArray(0)
    }

    val mem = Memory(len.toLong())

    try {
        val __exceptionC = PointerByReference()

        CAPI.System_Runtime_InteropServices_Marshal_Copy_6(
            this.__handle,
            0,
            mem,
            len,
            __exceptionC
        )

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val byteArray = mem.getByteArray(0, len)
        val uByteArray = byteArray.toUByteArray()

        return uByteArray
    } finally {
        mem.close()
    }
}

fun DNArray<System_SByte>.toKByteArray(): ByteArray {
    val len = this.length_get()

    if (len <= 0) {
        return ByteArray(0)
    }

    val mem = Memory(len.toLong())

    try {
        val __exceptionC = PointerByReference()

        CAPI.System_Runtime_InteropServices_Marshal_Copy_6(
            this.__handle,
            0,
            mem,
            len,
            __exceptionC
        )

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val byteArray = mem.getByteArray(0, len)

        return byteArray
    } finally {
        mem.close()
    }
}

@OptIn(ExperimentalUnsignedTypes::class)
fun UByteArray.toDotNETByteArray(): DNArray<System_Byte> {
    val len = this.count()
    val mem = Memory(len.toLong())

    try {
        mem.write(0, this.toByteArray(), 0, len)

        val byteArray = DNArray<System_Byte>(len)

        val __exceptionC = PointerByReference()

        CAPI.System_Runtime_InteropServices_Marshal_Copy_14(
            mem,
            byteArray.__handle,
            0,
            len,
            __exceptionC
        )

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        return byteArray
    } finally {
        mem.close()
    }
}

fun ByteArray.toDotNETSByteArray(): DNArray<System_SByte> {
    val len = this.count()
    val mem = Memory(len.toLong())

    try {
        mem.write(0, this, 0, len)

        val sByteArray = DNArray<System_SByte>(len)

        val __exceptionC = PointerByReference()

        CAPI.System_Runtime_InteropServices_Marshal_Copy_14(
            mem,
            sByteArray.__handle,
            0,
            len,
            __exceptionC
        )

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        return sByteArray
    } finally {
        mem.close()
    }
}
""";
    }
}