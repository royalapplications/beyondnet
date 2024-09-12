package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import com.example.beyondnetsampleandroid.dn.*

import com.sun.jna.ptr.*

import org.junit.Assert.assertEquals
import org.junit.Test
import org.junit.runner.RunWith

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

class ObjectRef<T>(var value: T) where T: IDNObject {
    fun makeJNARef(): PointerByReference {
        return PointerByReference(value.getHandleOrNull())
    }
}

class BooleanRef(var value: Boolean) {
    fun makeJNARef(): BooleanByReference {
        return BooleanByReference(value)
    }
}

class ByteRef(var value: Byte) {
    fun makeJNARef(): ByteByReference {
        return ByteByReference(value)
    }
}

class UByteRef(var value: UByte) {
    fun makeJNARef(): UByteByReference {
        return UByteByReference(value)
    }
}

class ShortRef(var value: Short) {
    fun makeJNARef(): ShortByReference {
        return ShortByReference(value)
    }
}

class UShortRef(var value: UShort) {
    fun makeJNARef(): UShortByReference {
        return UShortByReference(value)
    }
}

class IntRef(var value: Int) {
    fun makeJNARef(): IntByReference {
        return IntByReference(value)
    }
}

class UIntRef(var value: UInt) {
    fun makeJNARef(): UIntByReference {
        return UIntByReference(value)
    }
}

class LongRef(var value: Long) {
    fun makeJNARef(): LongByReference {
        return LongByReference(value)
    }
}

class ULongRef(var value: ULong) {
    fun makeJNARef(): ULongByReference {
        return ULongByReference(value)
    }
}

class FloatRef(var value: Float) {
    fun makeJNARef(): FloatByReference {
        return FloatByReference(value)
    }
}

class DoubleRef(var value: Double) {
    fun makeJNARef(): DoubleByReference {
        return DoubleByReference(value)
    }
}

@RunWith(AndroidJUnit4::class)
class ByRefTests {
    @Test
    fun testIntRefCall() {
        val origValue = 5
        val newValue = 2

        val ref = IntRef(origValue)

        modifyIntRef(ref, newValue)

        val result = ref.value

        assertEquals(result, newValue)
    }

    private fun modifyIntRef(returnValue: IntRef, newValue: Int) {
        val returnValueCRef = returnValue.makeJNARef()

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
        val returnValueCRef = returnValue.makeJNARef()

        modifyStringRefC(returnValueCRef, newValue)

        returnValue.value = System_String(returnValueCRef.value)
    }

    private fun modifyStringRefC(returnValue: PointerByReference, newValue: System_String) {
        returnValue.value = newValue.getHandleOrNull()
    }

    private fun returnInt1NonOptional(returnValue: IntRef, newValue: Int) {
        val returnValueCRef = returnValue.makeJNARef()

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