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
        assertFalse(emptyString === emptyStringFromK) // NOTE: This returns false right now because Kotlin doesn't support overriding the identity check operators. If we happen to find a way to do that in the future, this should return true, just like `System.Object.ReferenceEquals` would.
        assertNotEquals(emptyString, helloString)
        assertEquals(helloString, helloCustomString)

        val emptyGuid = System_Guid.empty_get()
        val newGuid = System_Guid.newGuid()
        val newGuidConverted = System_Guid.parse(newGuid.dnToString())

        assertNotEquals(emptyGuid, newGuid)
        assertEquals(newGuid, newGuidConverted)
        assertFalse(newGuid === newGuidConverted)
    }
}