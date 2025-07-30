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
        // SByte
        assertEquals(Beyond_NET_Sample_PrimitiveTests.sByteMin, Byte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnSByte(Byte.MIN_VALUE), Byte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.sByteMax, Byte.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnSByte(Byte.MAX_VALUE), Byte.MAX_VALUE)

        // Short
        assertEquals(Beyond_NET_Sample_PrimitiveTests.shortMin, Short.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnShort(Short.MIN_VALUE), Short.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.shortMax, Short.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnShort(Short.MAX_VALUE), Short.MAX_VALUE)

        // Int
        assertEquals(Beyond_NET_Sample_PrimitiveTests.intMin, Int.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnInt(Int.MIN_VALUE), Int.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.intMax, Int.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnInt(Int.MAX_VALUE), Int.MAX_VALUE)

        // Long
        assertEquals(Beyond_NET_Sample_PrimitiveTests.longMin, Long.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnLong(Long.MIN_VALUE), Long.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.longMax, Long.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnLong(Long.MAX_VALUE), Long.MAX_VALUE)

        // Unsigned
        // Byte
        assertEquals(Beyond_NET_Sample_PrimitiveTests.byteMin, UByte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnByte(UByte.MIN_VALUE), UByte.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.byteMax, UByte.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnByte(UByte.MAX_VALUE), UByte.MAX_VALUE)

        // UShort
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uShortMin, UShort.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnUShort(UShort.MIN_VALUE), UShort.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uShortMax, UShort.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnUShort(UShort.MAX_VALUE), UShort.MAX_VALUE)

        // UInt
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uIntMin, UInt.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnUInt(UInt.MIN_VALUE), UInt.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uIntMax, UInt.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnUInt(UInt.MAX_VALUE), UInt.MAX_VALUE)

        // ULong
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uLongMin, ULong.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnULong(ULong.MIN_VALUE), ULong.MIN_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.uLongMax, ULong.MAX_VALUE)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnULong(ULong.MAX_VALUE), ULong.MAX_VALUE)

        // Floating Point
        // Float
        val dnMaxF = Beyond_NET_Sample_PrimitiveTests.floatMax
        val kMaxF = Float.MAX_VALUE

        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnFloat(kMaxF), kMaxF, 0f)
        assertEquals(dnMaxF, kMaxF, 0f)

        val dnMinF = Beyond_NET_Sample_PrimitiveTests.floatMin
        val kMinF = -Float.MAX_VALUE

        assertEquals(dnMinF, kMinF, 0f)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnFloat(kMinF), kMinF, 0f)

        // Double
        val dnMaxD = Beyond_NET_Sample_PrimitiveTests.doubleMax
        val kMaxD = Double.MAX_VALUE

        assertEquals(dnMaxD, kMaxD, 0.0)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnDouble(kMaxD), kMaxD, 0.0)

        val dnMinD = Beyond_NET_Sample_PrimitiveTests.doubleMin
        val kMinD = -Double.MAX_VALUE

        assertEquals(dnMinD, kMinD, 0.0)
        assertEquals(Beyond_NET_Sample_PrimitiveTests.returnDouble(kMinD), kMinD, 0.0)
    }
}
