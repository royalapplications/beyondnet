package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class CharTests {
    @Test
    fun testLowercaseA() {
        val value = 'a'
        val valueDN = Beyond_NET_Sample_Source_CharTests.lowercaseA_get()

        assertEquals(valueDN, value)

        try {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseAOrThrow(value)
        } catch (e: Exception) {
            fail()
        }

        try {
            Beyond_NET_Sample_Source_CharTests.passInUppercaseAOrThrow(value)
            fail()
        } catch (_: Exception) { }
    }

    @Test
    fun testUppercaseA() {
        val value = 'A'
        val valueDN = Beyond_NET_Sample_Source_CharTests.uppercaseA_get()

        assertEquals(valueDN, value)

        try {
            Beyond_NET_Sample_Source_CharTests.passInUppercaseAOrThrow(value)
        } catch (e: Exception) {
            fail()
        }

        try {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseAOrThrow(value)
            fail()
        } catch (_: Exception) { }
    }

    @Test
    fun testOne() {
        val value = '1'
        val valueDN = Beyond_NET_Sample_Source_CharTests.one_get()

        assertEquals(valueDN, value)

        try {
            Beyond_NET_Sample_Source_CharTests.passInOneOrThrow(value)
        } catch (e: Exception) {
            fail()
        }

        try {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseUmlautAOrThrow(value)
            fail()
        } catch (_: Exception) { }
    }

    @Test
    fun testLowercaseUmlautA() {
        val value = 'ä'
        val valueDN = Beyond_NET_Sample_Source_CharTests.lowercaseUmlautA_get()

        assertEquals(valueDN, value)

        try {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseUmlautAOrThrow(value)
        } catch (e: Exception) {
            fail()
        }

        try {
            Beyond_NET_Sample_Source_CharTests.passInOneOrThrow(value)
            fail()
        } catch (_: Exception) { }
    }
}