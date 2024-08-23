package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class TestSystemRandom {
    @Test
    fun testRandom() {
        val random = System_Random()

        val minValue = 5
        val maxValue = 15

        for (i in 0..<200) {
            val value = random.next(minValue, maxValue)

            assertTrue(value >= minValue)
            assertTrue(value < maxValue)
        }
    }
}