package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.sun.jna.*
import com.sun.jna.ptr.*
import java.util.UUID

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemTypeTests {
    @Test
    fun testSystemTypeComparison() {
        val staticSystemGuidType = System_Guid.typeOf()
        val systemGuidType = System_Guid.empty_get().getType()
        val staticSystemStringType = System_String.typeOf()

        assertTrue(System_Object.equals(staticSystemGuidType, systemGuidType))
        assertFalse(System_Object.equals(staticSystemGuidType, staticSystemStringType))
    }
}