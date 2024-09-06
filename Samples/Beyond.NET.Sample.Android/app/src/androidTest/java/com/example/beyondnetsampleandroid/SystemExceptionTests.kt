package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemExceptionTests {
    @Test
    fun testSystemException() {
        val exceptionMessage = "I'm a nice exception"

        val createdException = System_Exception(exceptionMessage.toDotNETString())
        val retrievedExceptionMessage = createdException.message_get().toKString()

        assertEquals(exceptionMessage, retrievedExceptionMessage)
    }

    // TODO: Add tests for throwing/catching exceptions that occur within .NET
}