package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class PrimitivesTests {
    @Test
    fun testPrimitives() {
        // Signed
        assertEquals(Beyond_NET_Sample_PrimitiveTests.sByteMin_get(), Byte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.sByteMax_get(), Byte.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.shortMin_get(), Short.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.shortMax_get(), Short.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.intMin_get(), Int.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.intMax_get(), Int.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.longMin_get(), Long.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.longMax_get(), Long.MAX_VALUE)

        // Unsigned
        assertEquals(Beyond_NET_Sample_PrimitiveTests.byteMin_get(), UByte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.byteMax_get(), UByte.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.uShortMin_get(), UShort.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uShortMax_get(), UShort.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.uIntMin_get(), UInt.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uIntMax_get(), UInt.MAX_VALUE)

        assertEquals(Beyond_NET_Sample_PrimitiveTests.uLongMin_get(), ULong.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uLongMax_get(), ULong.MAX_VALUE)

        // Floating Point
        // TODO: Currently fails
//        assertEquals(Beyond_NET_Sample_PrimitiveTests.floatMin_get(), Float.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.floatMax_get(), Float.MAX_VALUE)

        // TODO: Currently fails
//        assertEquals(Beyond_NET_Sample_PrimitiveTests.doubleMin_get(), Double.MIN_VALUE, (0).toDouble())
        assertEquals(Beyond_NET_Sample_PrimitiveTests.doubleMax_get(), Double.MAX_VALUE, (0).toDouble())
    }
}