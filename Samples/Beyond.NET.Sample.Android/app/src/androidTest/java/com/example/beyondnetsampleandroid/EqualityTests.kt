package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class EqualityTests {
    // NOTE: As per https://kotlinlang.org/docs/operator-overloading.html#equality-and-inequality-operators identity checks (ie. === and !===) cannot be overridden, so we can only use regular equality operators for calling into .NET's System.Object.Equals function but not System.Object.ReferenceEquals.

    @Test
    fun testEquality() {
        val emptyString = System_String.empty_get()
        val emptyStringFromK = "".toDotNETString()
        val helloString = "Hello".toDotNETString()
        val helloCustomString = "Hello World".toDotNETString().replace(" World".toDotNETString(), emptyString)

        assertEquals(emptyString, emptyStringFromK)
        assertNotEquals(emptyString, helloString)
        assertEquals(helloString, helloCustomString)
    }
}