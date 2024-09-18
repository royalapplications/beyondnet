package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemStringTests {
    // NOTE: This was copied from the Swift tests
    @Test
    fun testString() {
        val emptyStringDN = System_String.empty_get()

        val emptyString = emptyStringDN.toKString()
        assertTrue(emptyString.isEmpty())

        val isNullOrEmpty = System_String.isNullOrEmpty(emptyStringDN)
        assertTrue(isNullOrEmpty)

        val isNullOrWhiteSpace = System_String.isNullOrWhiteSpace(emptyStringDN)
        assertTrue(isNullOrWhiteSpace)

        val nonEmptyString = "Hello World!"
        val nonEmptyStringDN = nonEmptyString.toDotNETString()

        val isNonEmptyStringNullOrEmpty = System_String.isNullOrEmpty(nonEmptyStringDN)
        assertFalse(isNonEmptyStringNullOrEmpty)

        val stringForTrimmingDN = " $nonEmptyString ".toDotNETString()

        val trimmedString = stringForTrimmingDN.trim().toKString()
        assertEquals(nonEmptyString, trimmedString)

        val worldDN = "World".toDotNETString()
        val expectedIndexOfWorld = 6

        val indexOfWorld = nonEmptyStringDN.indexOf(worldDN)
        assertEquals(expectedIndexOfWorld, indexOfWorld)

        val splitOptions = System_StringSplitOptions(
            System_StringSplitOptions.REMOVEEMPTYENTRIES.value or System_StringSplitOptions.TRIMENTRIES.value
        )

        val blankDN = " ".toDotNETString()

        val split = nonEmptyStringDN.split(blankDN, splitOptions)

        if (split.length_get() != 2) {
            fail("System.Array.Length getter should not throw and return 2")
        }
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testStringReplace() {
        val hello = "Hello"
        val otherPart = " World 😀"
        val originalString = "$hello$otherPart"
        val emptyString = ""

        val expectedString = originalString.replace(hello, emptyString)

        val originalStringDN = originalString.toDotNETString()
        val helloDN = hello.toDotNETString()
        val emptyStringDN = emptyString.toDotNETString()

        val replacedString = originalStringDN.replace(helloDN, emptyStringDN).toKString()

        assert(expectedString == replacedString) {
            "Expected $expectedString but got $replacedString"
        }
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testStringSubstring() {
        val string = "Hello World 😀"
        val needle = "World"

        val stringDN = string.toDotNETString()
        val needleDN = needle.toDotNETString()

        val expectedIndex: Int = 6

        val index = stringDN.indexOf(needleDN)
        assert(expectedIndex == index) {
            "Expected $expectedIndex but got $index"
        }
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testStringComparison() {
        val hello = "Hello"
        val helloDN = hello.toDotNETString()

        val hello2 = "Hello"
        val hello2DN = hello2.toDotNETString()

        val world = "World"
        val worldDN = world.toDotNETString()

        assert(System_Object.equals(helloDN, hello2DN)) {
            "Expected helloDN and hello2DN to be equal"
        }
        assert(!System_Object.referenceEquals(helloDN, hello2DN)) {
            "Expected helloDN and hello2DN to be different instances"
        }

        assert(!System_Object.equals(helloDN, worldDN)) {
            "Expected helloDN and worldDN to be different"
        }
        assert(!System_Object.referenceEquals(helloDN, worldDN)) {
            "Expected helloDN and worldDN to be different instances"
        }
    }
}