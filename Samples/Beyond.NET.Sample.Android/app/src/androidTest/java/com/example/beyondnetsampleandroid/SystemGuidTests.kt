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
class SystemGuidTests {
    @Test
    fun testSystemGuidParse() {
        val inputUUID = UUID.randomUUID()
        val inputUUIDStr = inputUUID.toString()
        val inputUUIDStrDN = inputUUIDStr.toDotNETString()

        val guid = System_Guid.parse(inputUUIDStrDN)
        val guidStrDN = guid.dnToString()
        val guidStr = guidStrDN.toKString()

        assertEquals(inputUUIDStr.lowercase(), guidStr.lowercase())
    }

    @Test
    fun testComparingSystemGuids() {
        val emptyGuid = System_Guid.empty_get()
        val emptyGuidWithCtor = System_Guid()

        val areEqual = System_Object.equals(emptyGuid, emptyGuidWithCtor)

        assertTrue(areEqual)
    }
}