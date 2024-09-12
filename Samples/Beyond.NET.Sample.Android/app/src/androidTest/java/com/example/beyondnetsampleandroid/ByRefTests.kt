package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import com.example.beyondnetsampleandroid.dn.*

import com.sun.jna.ptr.*

import org.junit.Assert.assertEquals
import org.junit.Test
import org.junit.runner.RunWith

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