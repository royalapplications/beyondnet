package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class ArrayTests {
    @Test
    fun testArrayOfNullableString() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val array = tests.arrayOfNullableString_get()

        // Check initial state
        verifyArrayOfNullableStringInitialState(array)

        // Modify it
        array[3] = null

        tests.arrayOfNullableString_set(array)
        val newArray = tests.arrayOfNullableString_get()

        // Check modified state
        assertNull(newArray[0])
        assertEquals(array[1]?.toKString(), "a")
        assertEquals(array[2]?.toKString(), "b")
        assertNull(array[3])
    }

    @Test
    fun testArrayOfNullableStringAsReturn() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val array = tests.arrayOfNullableStringAsReturn()

        verifyArrayOfNullableStringInitialState(array)
    }

    @Test
    fun testSetArrayOfNullableStringWithParameter() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val newArray = DNNullableArray<System_String>(3)

        assertEquals(newArray.rank_get(), 1)

        // Set values
        newArray[0] = "z".toDotNETString()
        newArray[1] = "y".toDotNETString()
        newArray[2] = "x".toDotNETString()

        tests.setArrayOfNullableStringWithParameter(newArray)

        val array = tests.arrayOfNullableString_get()
        assertEquals(array.rank_get(), 1)

        // Check state
        assertEquals(array.getValue(0)?.castTo<System_String>()?.toKString(), "z")
        assertEquals(array[0]?.toKString(), "z")

        assertEquals(array.getValue(1)?.castTo<System_String>()?.toKString(), "y")
        assertEquals(array[1]?.toKString(), "y")

        assertEquals(array.getValue(2)?.castTo<System_String>()?.toKString(), "x")
        assertEquals(array[2]?.toKString(), "x")
    }

    @Test
    fun testArrayOfNullableStringAsOut() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val array = DNNullableArray.empty<System_String>()
        val arrayRef = ObjectRef(array)

        tests.arrayOfNullableStringAsOut(arrayRef)

        verifyArrayOfNullableStringInitialState(arrayRef.value)
    }

    @Test
    fun testArrayOfNullableStringAsRef() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val array = DNNullableArray.empty<System_String>()
        val arrayRef = ObjectRef(array)

        tests.arrayOfNullableStringAsRef(arrayRef)

        verifyArrayOfNullableStringInitialState(arrayRef.value)
    }

    @Test
    fun testArrayOfGuids() {
        val tests = Beyond_NET_Sample_ArrayTests()

        val array = tests.arrayOfGuids_get()
        val rank = array.rank_get()

        assertEquals(rank, 1)

        val emptyGuid = System_Guid.empty_get()

        // Check initial state
        assertEquals(array[0], emptyGuid)
        assertNotEquals(array[1], emptyGuid)

        // Modify it
        array[0] = System_Guid.newGuid()
        array[1] = emptyGuid

        tests.arrayOfGuids_set(array)
        val newArray = tests.arrayOfGuids_get()

        // Check modified state
        assertNotEquals(newArray[0], emptyGuid)
        assertEquals(newArray[1], emptyGuid)
    }

    @Test
    fun testArrayOfCharacters() {
        val tests = Beyond_NET_Sample_ArrayTests()
        val array = tests.arrayOfCharacters_get()
        val rank = array.rank_get()

        assertEquals(rank, 1)

        val aChar = 'a'
        val bChar = 'b'
        val cChar = 'c'

        // Check initial state
        assertEquals(array[0].castToChar(), aChar)
        assertEquals(array[1].castToChar(), bChar)
        assertEquals(array[2].castToChar(), cChar)

        // Modify it
        array[0] = cChar.toDotNETObject()
        array[1] = aChar.toDotNETObject()
        array[2] = bChar.toDotNETObject()

        tests.arrayOfCharacters_set(array)
        val newArray = tests.arrayOfCharacters_get()

        // Check modified state
        assertEquals(newArray[0].castToChar(), cChar)
        assertEquals(newArray[1].castToChar(), aChar)
        assertEquals(newArray[2].castToChar(), bChar)
    }

    private fun verifyArrayOfNullableStringInitialState(array: DNNullableArray<System_String>) {
        assertEquals(array.rank_get(), 1)
        assertNull(array[0])
        assertEquals(array[1]?.toKString(), "a")
        assertEquals(array[2]?.toKString(), "b")
        assertEquals(array[3]?.toKString(), "c")
    }
}