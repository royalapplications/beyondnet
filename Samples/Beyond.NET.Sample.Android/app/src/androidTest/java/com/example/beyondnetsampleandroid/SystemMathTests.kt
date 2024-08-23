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
    }
}