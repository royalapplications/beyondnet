package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import com.example.beyondnetsampleandroid.dn.*

import com.sun.jna.ptr.*

import org.junit.Assert.assertEquals
import org.junit.Test
import org.junit.runner.RunWith

public interface IRef {
    fun toJNARef(): ByReference
}

public class ObjectRef<T>(var value: T): IRef where T: IDNObject {
    override fun toJNARef(): PointerByReference {
        return PointerByReference(value.getHandleOrNull())
    }
}

fun IDNObject.toRef(): IRef /* TODO: Maybe there's a way to express this using Kotlin generics. If not, we can generate a toRef method for every .NET type to make it type-safe. */ {
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

@RunWith(AndroidJUnit4::class)
class ByRefTests {
    @Test
    fun testIntRefCall() {
        val origValue = 5
        val newValue = 2

        val ref = origValue.toRef()

        modifyIntRef(ref, newValue)

        val result = ref.value

        assertEquals(result, newValue)
    }

    private fun modifyIntRef(returnValue: IntRef, newValue: Int) {
        val returnValueCRef = returnValue.toJNARef()

        modifyIntRefC(returnValueCRef, newValue)

        returnValue.value = returnValueCRef.value
    }

    private fun modifyIntRefC(returnValue: IntByReference, newValue: Int) {
        returnValue.value = newValue
    }

    @Test
    fun testStringRefCall() {
        val origValue = System_String.empty_get()
        val newValue = "Abc".toDotNETString()

        val ref = ObjectRef(origValue)

        modifyStringRef(ref, newValue)

        val result = ref.value

        assertEquals(result, newValue)
    }

    private fun modifyStringRef(returnValue: ObjectRef<System_String>, newValue: System_String) {
        val returnValueCRef = returnValue.toJNARef()

        modifyStringRefC(returnValueCRef, newValue)

        returnValue.value = System_String(returnValueCRef.value)
    }

    private fun modifyStringRefC(returnValue: PointerByReference, newValue: System_String) {
        returnValue.value = newValue.getHandleOrNull()
    }

    private fun returnInt1NonOptional(returnValue: IntRef, newValue: Int) {
        val returnValueCRef = returnValue.toJNARef()

        returnInt1NonOptionalC(returnValueCRef, newValue)

        returnValue.value = returnValueCRef.value
    }

    private fun returnInt1NonOptionalC(returnValue: IntByReference, newValue: Int) {
        returnValue.value = newValue
    }

    @Test
    fun testManualOutParameterBinding_Int() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()
        val instPtr = inst.getHandleOrNull()

        val retOutRef = IntByReference(-1)

        val __exceptionC = PointerByReference()

        CAPI.Beyond_NET_Sample_Source_OutParameterTests_Return_Int_1_NonOptional(instPtr, retOutRef, __exceptionC)

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val retOut = retOutRef.value

        assertEquals(retOut, 1)
    }

    @Test
    fun testManualOutParameterBinding_Bool() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()
        val instPtr = inst.getHandleOrNull()

        val retOutRef = BooleanByReference(false)

        val __exceptionC = PointerByReference()

        CAPI.Beyond_NET_Sample_Source_OutParameterTests_Return_Bool_true_NonOptional(instPtr, retOutRef, __exceptionC)

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val retOut = retOutRef.value

        assertEquals(retOut, true)
    }

    @Test
    fun testManualOutParameterBinding_String() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()
        val instPtr = inst.getHandleOrNull()

        val retOutRef = PointerByReference()

        val __exceptionC = PointerByReference()

        CAPI.Beyond_NET_Sample_Source_OutParameterTests_Return_String_Abc_NonOptional(instPtr, retOutRef, __exceptionC)

        val __exceptionCHandle = __exceptionC.value

        if (__exceptionCHandle != null) {
            throw System_Exception(__exceptionCHandle).toKException()
        }

        val retOutPtr = retOutRef.value
        val retOut = System_String(retOutPtr)

        assertEquals(retOut.toKString(), "Abc")
    }
}