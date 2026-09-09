package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class CharTests {
    @Test
    fun testToDNObjectAndBack() {
        val value = 'a'
        assertEquals('a', value)
        assertNotEquals('A', value)

        val objectDN = value.toDotNETObject()
        val valueBack = objectDN.castToChar()

        assertEquals('a', valueBack)
    }

    @Test
    fun testGetLowercaseA() {
        val valueDN = Beyond_NET_Sample_Source_CharTests.lowercaseA

        assertEquals('a', valueDN)
        assertNotEquals('A', valueDN)
    }

    @Test
    fun testPassInLowercaseA() {
        Beyond_NET_Sample_Source_CharTests.passInLowercaseAOrThrow('a')

        assertThrows(DNException::class.java) {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseAOrThrow('A')
        }
    }

    @Test
    fun testGetUppercaseA() {
        val valueDN = Beyond_NET_Sample_Source_CharTests.uppercaseA

        assertEquals('A', valueDN)
        assertNotEquals('a', valueDN)
    }

    @Test
    fun testPassInUppercaseA() {
        Beyond_NET_Sample_Source_CharTests.passInUppercaseAOrThrow('A')

        assertThrows(DNException::class.java) {
            Beyond_NET_Sample_Source_CharTests.passInUppercaseAOrThrow('a')
        }
    }

    @Test
    fun testGetOne() {
        val valueDN = Beyond_NET_Sample_Source_CharTests.one

        assertEquals('1', valueDN)
        assertNotEquals('2', valueDN)
    }

    @Test
    fun testPassInOne() {
        Beyond_NET_Sample_Source_CharTests.passInOneOrThrow('1')

        assertThrows(DNException::class.java) {
            Beyond_NET_Sample_Source_CharTests.passInOneOrThrow('2')
        }
    }

    @Test
    fun testGetLowercaseUmlautA() {
        val valueDN = Beyond_NET_Sample_Source_CharTests.lowercaseUmlautA

        assertEquals('ä', valueDN)
        assertNotEquals('Ä', valueDN)
    }

    @Test
    fun testPassInLowercaseUmlautA() {
        Beyond_NET_Sample_Source_CharTests.passInLowercaseUmlautAOrThrow('ä')

        assertThrows(DNException::class.java) {
            Beyond_NET_Sample_Source_CharTests.passInLowercaseUmlautAOrThrow('Ä')
        }
    }
}
