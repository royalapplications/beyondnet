package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

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
}