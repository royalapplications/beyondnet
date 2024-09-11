package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.Beyond_NET_Sample_Source_OutParameterTests
import com.example.beyondnetsampleandroid.dn.BooleanByReference
import com.example.beyondnetsampleandroid.dn.CAPI
import com.example.beyondnetsampleandroid.dn.System_Exception
import com.example.beyondnetsampleandroid.dn.System_String
import com.example.beyondnetsampleandroid.dn.getHandleOrNull
import com.example.beyondnetsampleandroid.dn.toKException
import com.example.beyondnetsampleandroid.dn.toKString

import com.sun.jna.ptr.*

import org.junit.Assert.assertEquals
import org.junit.Test
import org.junit.runner.RunWith

class IntRef(var value: Int) {
    fun makeByRef(): IntByReference {
        return IntByReference(value)
    }
}

fun Int.makeRef(): IntRef {
    return IntRef(this)
}

@RunWith(AndroidJUnit4::class)
class ByRefTests {
    @Test
    fun testRefCall() {
        val origValue = 5
        val newValue = 2

        val ref = origValue.makeRef()

        returnInt1NonOptional(ref, newValue)

        val result = ref.value

        assertEquals(result, newValue)
    }

    private fun returnInt1NonOptional(returnValue: IntRef, newValue: Int) {
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