package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*
import com.example.beyondnetsampleandroid.dn.Beyond.NET.Sample.Source.CharTests

@RunWith(AndroidJUnit4::class)
class SystemCharTests {
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
        val valueDN = CharTests.lowercaseA

        assertEquals('a', valueDN)
        assertNotEquals('A', valueDN)
    }

    @Test
    fun testPassInLowercaseA() {
        CharTests.passInLowercaseAOrThrow('a')

        assertThrows(DNException::class.java) {
            CharTests.passInLowercaseAOrThrow('A')
        }
    }

    @Test
    fun testGetUppercaseA() {
        val valueDN = CharTests.uppercaseA

        assertEquals('A', valueDN)
        assertNotEquals('a', valueDN)
    }

    @Test
    fun testPassInUppercaseA() {
        CharTests.passInUppercaseAOrThrow('A')

        assertThrows(DNException::class.java) {
            CharTests.passInUppercaseAOrThrow('a')
        }
    }

    @Test
    fun testGetOne() {
        val valueDN = CharTests.one

        assertEquals('1', valueDN)
        assertNotEquals('2', valueDN)
    }

    @Test
    fun testPassInOne() {
        CharTests.passInOneOrThrow('1')

        assertThrows(DNException::class.java) {
            CharTests.passInOneOrThrow('2')
        }
    }

    @Test
    fun testGetLowercaseUmlautA() {
        val valueDN = CharTests.lowercaseUmlautA

        assertEquals('ä', valueDN)
        assertNotEquals('Ä', valueDN)
    }

    @Test
    fun testPassInLowercaseUmlautA() {
        CharTests.passInLowercaseUmlautAOrThrow('ä')

        assertThrows(DNException::class.java) {
            CharTests.passInLowercaseUmlautAOrThrow('Ä')
        }
    }
}
