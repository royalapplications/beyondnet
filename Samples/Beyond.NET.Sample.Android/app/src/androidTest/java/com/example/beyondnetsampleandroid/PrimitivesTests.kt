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
        // TODO: Not sure if this is correct
        val dnMaxF = Beyond_NET_Sample_PrimitiveTests.floatMax_get()
        val kMaxF = Float.MAX_VALUE
        val maxDeltaF = Math.ulp(dnMaxF).coerceAtLeast(Math.ulp(kMaxF))

        assertEquals(dnMaxF, kMaxF, maxDeltaF)

        // TODO: Currently fails
//        val dnMinF = Beyond_NET_Sample_PrimitiveTests.floatMin_get()
//        val kMinF = Float.MIN_VALUE
//        val minDeltaF = Math.ulp(dnMinF).coerceAtLeast(Math.ulp(kMinF))
//
//        assertEquals(dnMinF, kMinF, minDeltaF)

        // TODO: Not sure if this is correct
        val dnMaxD = Beyond_NET_Sample_PrimitiveTests.doubleMax_get()
        val kMaxD = Double.MAX_VALUE
        val maxDeltaD = Math.ulp(dnMaxD).coerceAtLeast(Math.ulp(kMaxD))

        assertEquals(dnMaxD, kMaxD, maxDeltaD)

        // TODO: Currently fails
//        val dnMinD = Beyond_NET_Sample_PrimitiveTests.doubleMin_get()
//        val kMinD = Double.MIN_VALUE
//        val minDeltaD = Math.ulp(dnMinD).coerceAtLeast(Math.ulp(kMinD))
//
//        assertEquals(dnMinD, kMinD, minDeltaD)
    }
}