package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*
import java.util.UUID

@RunWith(AndroidJUnit4::class)
class SystemGuidTests {
    @Test
    fun testSystemGuidParse() {
        val inputUUID = UUID.randomUUID()
        val inputUUIDStr = inputUUID.toString()
        val inputUUIDStrDN = inputUUIDStr.toDotNETString()

        val guid = System_Guid.parse(inputUUIDStrDN)
        val guidStrDN = guid.dn_toString()
        val guidStr = guidStrDN.toKString()

        assertEquals(inputUUIDStr.lowercase(), guidStr.lowercase())
    }
}