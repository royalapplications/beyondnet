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
        assertEquals(System_DateTimeKind.Unspecified.value, 0)
        assertEquals(System_DateTimeKind.Utc.value, 1)
        assertEquals(System_DateTimeKind.Local.value, 2)

        assertEquals(System_DateTimeKind(System_DateTimeKind.Unspecified.value), System_DateTimeKind.Unspecified)
        assertEquals(System_DateTimeKind(System_DateTimeKind.Utc.value), System_DateTimeKind.Utc)
        assertEquals(System_DateTimeKind(System_DateTimeKind.Local.value), System_DateTimeKind.Local)
    }
}