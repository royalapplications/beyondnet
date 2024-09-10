package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemUriTests {
    @Test
    fun testUriCreationOptions() {
        val creationOptions = System_UriCreationOptions()
        val type = System_UriCreationOptions.typeOf()
        val typeRet = creationOptions.getType()
        assertTrue(type == typeRet)

        val value = true

        creationOptions.dangerousDisablePathAndQueryCanonicalization_set(value)
        val valueRet = creationOptions.dangerousDisablePathAndQueryCanonicalization_get()
        assertEquals(value, valueRet)
    }

    // TODO: Add testTryCreateUriWithInParameter once by ref parameters are supported
}