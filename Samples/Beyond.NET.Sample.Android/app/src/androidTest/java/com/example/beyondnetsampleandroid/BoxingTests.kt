package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class BoxingTests {
    @Test
    fun testBool() {
        val trueB = true
        val trueSystemB = trueB.toDotNETObject()
        val trueBBack = trueSystemB.castToBool()

        assertEquals(trueB, trueBBack)

        val falseB = false
        val falseSystemB = falseB.toDotNETObject()
        val falseBBack = falseSystemB.castToBool()

        assertEquals(falseB, falseBBack)
    }

    @Test
    fun testChar() {
        val origVal = 'Ä'
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToChar()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testFloat() {
        val origVal = Float.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToFloat()

        assertEquals(origVal, backVal, 0f)
    }

    @Test
    fun testDouble() {
        val origVal = Double.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToDouble()

        assertEquals(origVal, backVal, 0.0)
    }

    @Test
    fun testByte() {
        val origVal = Byte.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToByte()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testUByte() {
        val origVal = UByte.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToUByte()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testShort() {
        val origVal = Short.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToShort()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testUShort() {
        val origVal = UShort.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToUShort()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testInt() {
        val origVal = Int.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToInt()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testUInt() {
        val origVal = UInt.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToUInt()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testLong() {
        val origVal = Long.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToLong()

        assertEquals(origVal, backVal)
    }

    @Test
    fun testULong() {
        val origVal = ULong.MAX_VALUE
        val systemVal = origVal.toDotNETObject()
        val backVal = systemVal.castToULong()

        assertEquals(origVal, backVal)
    }
}