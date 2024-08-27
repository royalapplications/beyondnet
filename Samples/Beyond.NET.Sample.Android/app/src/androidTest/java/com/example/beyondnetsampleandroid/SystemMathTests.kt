package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemMathTests {
    @Test
    fun testSystemMath() {
        assertEquals(599, System_Math.abs((-599).toLong()))
        assertEquals(599.995f, System_Math.abs(-599.995f))
        assertEquals((-7.0), System_Math.ceiling((-7.6)), (0).toDouble())
        assertEquals((0.0).toDouble(), System_Math.floor((0.12).toDouble()), (0).toDouble())
        assertEquals(100, System_Math.clamp((500).toLong(), (0).toLong(), (100).toLong()))

        assertEquals(System_Math.max(Byte.MIN_VALUE, Byte.MAX_VALUE), Byte.MAX_VALUE)
        assertEquals(System_Math.max(Short.MIN_VALUE, Short.MAX_VALUE), Short.MAX_VALUE)
        assertEquals(System_Math.max(Int.MIN_VALUE, Int.MAX_VALUE), Int.MAX_VALUE)
        assertEquals(System_Math.max(Long.MIN_VALUE, Long.MAX_VALUE), Long.MAX_VALUE)

        assertEquals(System_Math.max(UByte.MIN_VALUE, UByte.MAX_VALUE), UByte.MAX_VALUE)
        assertEquals(System_Math.max(UShort.MIN_VALUE, UShort.MAX_VALUE), UShort.MAX_VALUE)
        assertEquals(System_Math.max(UInt.MIN_VALUE, UInt.MAX_VALUE), UInt.MAX_VALUE)
        assertEquals(System_Math.max(ULong.MIN_VALUE, ULong.MAX_VALUE), ULong.MAX_VALUE)

        val dnMaxF = Beyond_NET_Sample_PrimitiveTests.floatMax_get()
        val kMaxF = Float.MAX_VALUE
        val maxDeltaF = Math.ulp(dnMaxF).coerceAtLeast(Math.ulp(kMaxF))

        assertEquals(System_Math.max(-kMaxF, kMaxF), kMaxF, maxDeltaF)

        val dnMaxD = Beyond_NET_Sample_PrimitiveTests.doubleMax_get()
        val kMaxD = Double.MAX_VALUE
        val maxDeltaD = Math.ulp(dnMaxD).coerceAtLeast(Math.ulp(kMaxD))

        assertEquals(System_Math.max(-kMaxD, kMaxD), kMaxD, maxDeltaD)
    }
}