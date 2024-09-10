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
        val systemObjectTypeDN = System_Object.typeOf()
        val systemStringTypeDN = System_String.typeOf()
        val systemExceptionTypeDN = System_Exception.typeOf()
        val systemGuidTypeDN = System_Guid.typeOf()

        // MARK: - System.Object
        val systemObject = System_Object()

        assertTrue(systemObject.`is`(systemObjectTypeDN))
        assertFalse(systemObject.`is`(systemStringTypeDN))
        assertFalse(systemObject.`is`(systemExceptionTypeDN))
        assertFalse(systemObject.`is`(systemGuidTypeDN))

        // MARK: - System.String
        val systemString = System_String.empty_get()

        assertTrue(systemString.`is`(systemObjectTypeDN))
        assertTrue(systemString.`is`(systemStringTypeDN))
        assertFalse(systemString.`is`(systemExceptionTypeDN))
        assertFalse(systemString.`is`(systemGuidTypeDN))

        // MARK: - System.Exception
        val systemException = System_Exception()

        assertTrue(systemException.`is`(systemObjectTypeDN))
        assertFalse(systemException.`is`(systemStringTypeDN))
        assertTrue(systemException.`is`(systemExceptionTypeDN))
        assertFalse(systemException.`is`(systemGuidTypeDN))

        // MARK: - System.Guid
        val systemGuid = System_Guid.newGuid()

        assertTrue(systemGuid.`is`(systemObjectTypeDN))
        assertFalse(systemGuid.`is`(systemStringTypeDN))
        assertFalse(systemGuid.`is`(systemExceptionTypeDN))
        assertTrue(systemGuid.`is`(systemGuidTypeDN))
    }

    @Test
    fun testCastAs() {
        val systemObject = System_Object()
        val systemGuid = System_Guid.newGuid()
        val systemException = System_Exception()
        val systemNullReferenceException = System_NullReferenceException()

        val systemObjectCastToSystemObject = systemObject.castAs<System_Object>()
        assertNotNull(systemObjectCastToSystemObject)

        val systemObjectCastToSystemGuid = systemObject.castAs<System_Guid>()
        assertNull(systemObjectCastToSystemGuid)

        val systemGuidCastToSystemGuid = systemGuid.castAs<System_Guid>()
        assertNotNull(systemGuidCastToSystemGuid)

        val systemGuidCastToSystemObject = systemGuid.castAs<System_Object>()
        assertNotNull(systemGuidCastToSystemObject)

        val systemExceptionCastToSystemNullReferenceException = systemException.castAs<System_NullReferenceException>()
        assertNull(systemExceptionCastToSystemNullReferenceException)

        val systemNullReferenceExceptionCastToSystemException = systemNullReferenceException.castAs<System_Exception>()
        assertNotNull(systemNullReferenceExceptionCastToSystemException)
    }

    @Test
    fun testCastTo() {
        val systemObject = System_Object()
        val systemGuid = System_Guid.newGuid()
        val systemException = System_Exception()
        val systemNullReferenceException = System_NullReferenceException()

        assertNotNull(systemObject.castTo<System_Object>())
        assertThrows(DNException::class.java) { systemObject.castTo<System_Guid>() }
        assertNotNull(systemGuid.castTo<System_Guid>())
        assertNotNull(systemGuid.castTo<System_Object>())
        assertThrows(DNException::class.java) { systemException.castTo<System_NullReferenceException>() }
        assertNotNull(systemNullReferenceException.castTo<System_Exception>())
    }
}