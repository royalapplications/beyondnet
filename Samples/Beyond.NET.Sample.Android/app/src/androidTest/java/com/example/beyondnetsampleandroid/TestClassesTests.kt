package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class TestClassesTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testTestClass() {
        val testClass = Beyond_NET_Sample_TestClass()
        val testClassType = testClass.getType()

        val systemObjectTypeName = "System.Object"
        val systemObjectTypeNameDN = systemObjectTypeName.toDotNETString()

        val systemObjectType = System_Type.getType(systemObjectTypeNameDN, throwOnError = true, ignoreCase = false)
        requireNotNull(systemObjectType) { "System.Type.GetType should not throw and return an instance" }

        val retrievedSystemObjectTypeName = systemObjectType.dnToString().toKString()
        assertEquals(systemObjectTypeName, retrievedSystemObjectTypeName)

        val isTestClassAssignableToSystemObject = testClassType.isAssignableTo(systemObjectType)
        assertTrue(isTestClassAssignableToSystemObject)

        val isSystemObjectAssignableToTestClass = systemObjectType.isAssignableTo(testClassType)
        assertFalse(isSystemObjectAssignableToTestClass)

        testClass.sayHello()

        val john = "John".toDotNETString()
        testClass.sayHello(john)

        val hello = testClass.getHello().toKString()
        assertEquals("Hello", hello)

        val number1 = 85
        val number2 = 262
        val expectedResult = number1 + number2

        val result = testClass.add(number1, number2)
        assertEquals(expectedResult, result)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testEnum() {
        val enumValue = Beyond_NET_Sample_TestEnum.SECONDCASE
        val enumName = Beyond_NET_Sample_TestClass.getTestEnumName(enumValue).toKString()
        assertEquals("SecondCase", enumName)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testInt32ByRef() {
        val testClass = Beyond_NET_Sample_TestClass()

        val originalValue = 5
        val targetValue = 10

        val originalValueRef = IntRef(originalValue)

        val originalValueRet = testClass.modifyByRefValueAndReturnOriginalValue(
            originalValueRef,
            targetValue
        )

        assertEquals(originalValue, originalValueRet)
        assertEquals(targetValue, originalValueRef.value)
    }

    // NOTE: This was transpiled from Swift
    // TODO
//    @Test
//    fun testEnumByRef() {
//
//    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testDelegateReturningChar() {
        val testClass = Beyond_NET_Sample_TestClass()

        val value = Char(5)

        val charReturnerDelegate = Beyond_NET_Sample_CharReturnerDelegate {
            value
        }

        val retVal = testClass.getChar(charReturnerDelegate)
        assertEquals(value, retVal)
    }

    // NOTE: This was transpiled from Swift
    // TODO: Fails. Why?
//    @Test
//    fun testBookByRef() {
//        val testClass = Beyond_NET_Sample_TestClass()
//
//        val originalBook = Beyond_NET_Sample_Book.donQuixote
//        val targetBook = Beyond_NET_Sample_Book.theLordOfTheRings
//
//        val bookToModify = originalBook
//        val bookToModifyRef = ObjectRef(bookToModify)
//        val originalBookRet = ObjectRef(DNOutParameter.createPlaceholder(::Beyond_NET_Sample_Book))
//
//        testClass.modifyByRefBookAndReturnOriginalBookAsOutParameter(
//            bookToModifyRef,
//            targetBook,
//            originalBookRet
//        )
//
//        assertEquals(originalBook, originalBookRet.value)
//        assertEquals(targetBook, bookToModify)
//    }
}
