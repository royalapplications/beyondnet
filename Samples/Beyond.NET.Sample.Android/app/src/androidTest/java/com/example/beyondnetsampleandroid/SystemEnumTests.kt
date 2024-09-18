package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemEnumTests {
    @Test
    fun testSystemEnum() {
        assertEquals(System_DateTimeKind.UNSPECIFIED.value, 0)
        assertEquals(System_DateTimeKind.UTC.value, 1)
        assertEquals(System_DateTimeKind.LOCAL.value, 2)

        assertEquals(System_DateTimeKind(System_DateTimeKind.UNSPECIFIED.value), System_DateTimeKind.UNSPECIFIED)
        assertEquals(System_DateTimeKind(System_DateTimeKind.UTC.value), System_DateTimeKind.UTC)
        assertEquals(System_DateTimeKind(System_DateTimeKind.LOCAL.value), System_DateTimeKind.LOCAL)
    }
}