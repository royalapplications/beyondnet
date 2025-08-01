package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class TypeConversionTests {
    @Test
    fun testIs() {
        // MARK: - System.Object
        val systemObject = System_Object()

        assertTrue(systemObject.`is`(System_Object))
        assertFalse(systemObject.`is`(System_String))
        assertFalse(systemObject.`is`(System_Exception))
        assertFalse(systemObject.`is`(System_Guid))

        // MARK: - System.String
        val systemString = System_String.empty

        assertTrue(systemString.`is`(System_Object))
        assertTrue(systemString.`is`(System_String))
        assertFalse(systemString.`is`(System_Exception))
        assertFalse(systemString.`is`(System_Guid))

        // MARK: - System.Exception
        val systemException = System_Exception()

        assertTrue(systemException.`is`(System_Object))
        assertFalse(systemException.`is`(System_String))
        assertTrue(systemException.`is`(System_Exception))
        assertFalse(systemException.`is`(System_Guid))

        // MARK: - System.Guid
        val systemGuid = System_Guid.newGuid()

        assertTrue(systemGuid.`is`(System_Object))
        assertFalse(systemGuid.`is`(System_String))
        assertFalse(systemGuid.`is`(System_Exception))
        assertTrue(systemGuid.`is`(System_Guid))
    }

    @Test
    fun testCastAs() {
        val systemObject = System_Object()
        val systemGuid = System_Guid.newGuid()
        val systemException = System_Exception()
        val systemNullReferenceException = System_NullReferenceException()

        val systemObjectCastToSystemObject = systemObject.castAs(System_Object)
        assertNotNull(systemObjectCastToSystemObject)

        val systemObjectCastToSystemGuid = systemObject.castAs(System_Guid)
        assertNull(systemObjectCastToSystemGuid)

        val systemGuidCastToSystemGuid = systemGuid.castAs(System_Guid)
        assertNotNull(systemGuidCastToSystemGuid)

        val systemGuidCastToSystemObject = systemGuid.castAs(System_Object)
        assertNotNull(systemGuidCastToSystemObject)

        val systemExceptionCastToSystemNullReferenceException = systemException.castAs(System_NullReferenceException)
        assertNull(systemExceptionCastToSystemNullReferenceException)

        val systemNullReferenceExceptionCastToSystemException = systemNullReferenceException.castAs(System_Exception)
        assertNotNull(systemNullReferenceExceptionCastToSystemException)
    }

    @Test
    fun testCastTo() {
        val systemObject = System_Object()
        val systemGuid = System_Guid.newGuid()
        val systemException = System_Exception()
        val systemNullReferenceException = System_NullReferenceException()

        assertNotNull(systemObject.castTo(System_Object))
        assertThrows(DNException::class.java) { systemObject.castTo(System_Guid) }
        assertNotNull(systemGuid.castTo(System_Guid))
        assertNotNull(systemGuid.castTo(System_Object))
        assertThrows(DNException::class.java) { systemException.castTo(System_NullReferenceException) }
        assertNotNull(systemNullReferenceException.castTo(System_Exception))
    }
}
