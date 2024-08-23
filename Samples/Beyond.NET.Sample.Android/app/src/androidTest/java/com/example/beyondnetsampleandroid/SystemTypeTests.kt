package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemTypeTests {
    @Test
    fun testInvalidType() {
        val invalidTypeName = "! This.Type.Surely.Does.Not.Exist !"

        var invalidType: System_Type? = null

        try {
            invalidType = System_Type.getType(invalidTypeName.toDotNETString(), true)

            fail("System.Type.GetType should throw")
        } catch (e: Exception) {
            assertNull(invalidType)

            val exMessage = e.message

            assertNotNull(exMessage)

            if (exMessage != null) {
                assertTrue(exMessage.contains("The type '$invalidTypeName' cannot be found"))
            }
        }
    }

    @Test
    fun testSystemTypeComparison() {
        val staticSystemGuidType = System_Guid.typeOf()
        val systemGuidType = System_Guid.empty_get().getType()
        val staticSystemStringType = System_String.typeOf()

        assertTrue(System_Object.equals(staticSystemGuidType, systemGuidType))
        assertFalse(System_Object.equals(staticSystemGuidType, staticSystemStringType))
    }
}