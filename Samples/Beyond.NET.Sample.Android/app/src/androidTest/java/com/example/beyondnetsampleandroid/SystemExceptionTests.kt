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
        val retrievedExceptionMessage = createdException.message.toKString()

        assertEquals(exceptionMessage, retrievedExceptionMessage)
    }

    @Test
    fun testCatchingDNExceptions() {
        val exceptionMessage = "I'm a nice exception"
        val createdException = System_Exception(exceptionMessage.toDotNETString())

        var caughtThrowable: Throwable?

        try {
            Beyond_NET_Sample_ExceptionTests.`throw`(createdException)
            caughtThrowable = null
        } catch (e: Throwable) {
            caughtThrowable = e
        }

        assertNotNull(caughtThrowable)
        assertTrue(caughtThrowable is DNException)

        val caughtException = caughtThrowable as DNException
        assertEquals(caughtException.dnException, createdException)

        assertEquals(caughtException.message, exceptionMessage)
        assertNotNull(caughtException.dnStackTrace)
        assertFalse(caughtException.dnStackTrace!!.isEmpty())
    }
}
