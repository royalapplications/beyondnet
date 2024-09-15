package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.assertEquals
import org.junit.Test
import org.junit.runner.RunWith

@RunWith(AndroidJUnit4::class)
class ByRefTests {
    @Test
    fun testOutParameterPlaceholder() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue = DNOutParameter.createPlaceholder(::System_DateTime)
        val expectedValue = System_DateTime.maxValue_get()
        val valueRef = ObjectRef(origValue)

        inst.return_DateTime_MaxValue_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testInt() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue = 0
        val expectedValue = 1
        val valueRef = origValue.toRef()

        inst.return_Int_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testByte() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: Byte = 0
        val expectedValue: Byte = 1
        val valueRef = origValue.toRef()

        inst.return_SByte_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testUByte() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: UByte = 0u
        val expectedValue: UByte = 1u
        val valueRef = origValue.toRef()

        inst.return_Byte_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testUShort() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: UShort = 0u
        val expectedValue: UShort = 1u
        val valueRef = origValue.toRef()

        inst.return_UShort_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testUInt() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: UInt = 0u
        val expectedValue: UInt = 1u
        val valueRef = origValue.toRef()

        inst.return_UInt_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testULong() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: ULong = 0u
        val expectedValue: ULong = 1u
        val valueRef = origValue.toRef()

        inst.return_ULong_1_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testBool() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue = false
        val expectedValue = true
        val valueRef = origValue.toRef()

        inst.return_Bool_true_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testNonOptionalStruct() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue = System_DateTime.minValue_get()
        val expectedValue = System_DateTime.maxValue_get()
        val valueRef = origValue.toRef()

        inst.return_DateTime_MaxValue_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testOptionalStruct() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue = System_DateTime.maxValue_get()
        val valueRef = ObjectRef<System_DateTime?>(null)

        inst.return_DateTime_MaxValue_Optional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testOptionalStructReturningNull() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue: System_DateTime? = null
        val valueRef = ObjectRef<System_DateTime?>(null)

        inst.return_DateTime_Null(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testNonOptionalClass() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue = System_String.empty_get()
        val expectedValue = "Abc".toDotNETString()
        val valueRef = origValue.toRef()

        inst.return_String_Abc_NonOptional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testOptionalClass() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue = "Abc".toDotNETString()
        val valueRef = ObjectRef<System_String?>(null)

        inst.return_String_Abc_Optional(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testOptionalClassReturningNull() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue: System_String? = null
        val valueRef = ObjectRef<System_String?>(null)

        inst.return_String_Null(valueRef)

        assertEquals(valueRef.value, expectedValue)
    }

    @Test
    fun testNonOptionalInterface() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val origValue: System_Collections_IEnumerable = System_String.empty_get()
        val expectedValue = "Abc".toDotNETString()
        val valueRef = origValue.toRef()

        inst.return_IEnumerable_String_Abc_NonOptional(valueRef)

        assertEquals(valueRef.value.castTo(System_String.typeOf()), expectedValue)
    }

    @Test
    fun testOptionalInterface() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue = "Abc".toDotNETString()
        val valueRef = ObjectRef<System_Collections_IEnumerable?>(null)

        inst.return_IEnumerable_String_Abc_Optional(valueRef)

        assertEquals(valueRef.value?.castTo(System_String.typeOf()), expectedValue)
    }

    @Test
    fun testOptionalInterfaceReturningNull() {
        val inst = Beyond_NET_Sample_Source_OutParameterTests()

        val expectedValue: System_Collections_IEnumerable? = null
        val valueRef = ObjectRef<System_Collections_IEnumerable?>(null)

        inst.return_IEnumerable_Null(valueRef)

        assertEquals(valueRef.value?.castTo(System_String.typeOf()), expectedValue)
    }
}