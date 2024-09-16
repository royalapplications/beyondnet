package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

import com.sun.jna.*
import com.sun.jna.ptr.*

// TODO: This should be integrated into Beyond.NET
// TODO: This uses reflection - no good
open class DNArray<T: System_Object>(handle: Pointer, klassOfT: Class<T>)
    : System_Array(handle), Iterable<T> {
    //    private val systemTypeOfT = klassOfT.getMethod("typeOf").invoke(null) as System_Type
    private val constructorOfT = klassOfT.getDeclaredConstructor(Pointer::class.java)

    companion object {
        // Constructor that creates an empty array
        inline fun <reified T: System_Object> empty(): DNArray<T> {
            return invoke<T>(0)
        }

        // Constructor with optional length parameter
        inline operator fun <reified T: System_Object> invoke(length: Int = 0): DNArray<T> {
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
    operator fun get(index: Int): T {
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
    operator fun set(index: Int, value: T) {
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

@RunWith(AndroidJUnit4::class)
class SystemArrayTests {
    @Test
    fun testSystemArray() {
        val now = System_DateTime.now_get()
        val dateTimeType = now.getType()

        val arrayLength = 1
        val arrayOfDateTime = System_Array.createInstance(dateTimeType, arrayLength)

        val rank = arrayOfDateTime.rank_get()

        assertEquals(rank, 1)

        val index = 0

        arrayOfDateTime.setValue(now, index)

        val retrievedNow = arrayOfDateTime.getValue(index)

        assertTrue(System_Object.equals(now, retrievedNow))
    }

    @Test
    fun testSystemArrayBoxing() {
        val elementType = System_Boolean.typeOf()
        val array = System_Array.createInstance(elementType, 2)

        array.setValue(true.toDotNETObject(), 0)
        array.setValue(false.toDotNETObject(), 1)

        val valueIdx0 = array.getValue(0)

        if (valueIdx0 != null) {
            val unboxedValue = valueIdx0.castToBool()

            assertTrue(unboxedValue)
        } else {
            fail("System.Array.GetValue returned null")
        }

        val valueIdx1 = array.getValue(1)

        if (valueIdx1 != null) {
            val unboxedValue = valueIdx1.castToBool()

            assertFalse(unboxedValue)
        } else {
            fail("System.Array.GetValue returned null")
        }
    }

    @Test
    fun testDNArray() {
        val count = 2
        val arrayOfDateTime = DNArray<System_DateTime>(count)

        val dateTimeMax = System_DateTime.maxValue_get()
        val dateTimeMin = System_DateTime.minValue_get()

        arrayOfDateTime[0] = dateTimeMax
        arrayOfDateTime[1] = dateTimeMin

        assertEquals(arrayOfDateTime[0], dateTimeMax)
        assertEquals(arrayOfDateTime[1], dateTimeMin)

        var index: Int = -1

        for (currentDate in arrayOfDateTime) {
            index += 1

            assertEquals(currentDate, arrayOfDateTime[index])
        }

        assertEquals(index, count - 1)
    }
}