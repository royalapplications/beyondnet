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

    // TODO: Bring back once the generator supports properties and constructors
    /* @Test
    fun testComparingSystemGuids() {
        val emptyGuid = System_Guid.empty
        val emptyGuidWithCtor = System_Guid()

        val exRef = PointerByReference()

        val areEqual = BeyondDotNETSampleNative.System_Object_Equals(
            emptyGuid.__handle,
            emptyGuidWithCtor.__handle,
            exRef
        )

        assertTrue(exRef.value == Pointer.NULL)

        assertTrue(areEqual)
    } */
}