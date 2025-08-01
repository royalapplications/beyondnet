package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class StructTestTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testStructTest() {
        val nameOrig = "Joe"
        val nameNew = "John"

        val structTest = Beyond_NET_Sample_StructTest(nameOrig.toDotNETString())

        val nameOrigRet = try {
            structTest.name?.toKString()
        } catch (e: Exception) {
            fail("StructTest.name getter should not throw")
            return
        }
        assertEquals(nameOrig, nameOrigRet)

        structTest.name_set(nameNew.toDotNETString())

        val nameNewRet = try {
            structTest.name?.toKString()
        } catch (e: Exception) {
            fail("StructTest.name getter should not throw")
            return
        }
        assertEquals(nameNew, nameNewRet)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testNullableValueTypes() {
        val nullRetVal = Beyond_NET_Sample_StructTest.nullInstanceProperty
        assertNull(nullRetVal)

        val structRetVal = Beyond_NET_Sample_StructTest.nonNullInstanceProperty
        assertEquals("Test", structRetVal?.name?.toKString())

        val newName = "NotTest"
        structRetVal?.name_set(newName.toDotNETString())
        assertEquals(newName, structRetVal?.name?.toKString())

        val nullRetVal2 = Beyond_NET_Sample_StructTest.getNullableStructReturnValue(true)
        assertNull(nullRetVal2)

        val structRetVal2 = Beyond_NET_Sample_StructTest.getNullableStructReturnValue(false)
        assertEquals("Test", structRetVal2?.name?.toKString())

        val newName2 = "NotTest"
        structRetVal2?.name_set(newName2.toDotNETString())
        assertEquals(newName2, structRetVal2?.name?.toKString())

        val nullOutRetVal = ObjectRef<Beyond_NET_Sample_StructTest?>(null)
        Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(true, nullOutRetVal)
        assertNull(nullOutRetVal.value)

        val structRetVal3 = ObjectRef<Beyond_NET_Sample_StructTest?>(null)
        Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(false, structRetVal3)

        val sr3 = structRetVal3.value
        assertNotNull(sr3)
        assertEquals("Test", sr3?.name?.toKString())

        val newName3 = "NotTest"
        sr3?.name_set(newName3.toDotNETString())
        assertEquals(newName3, sr3?.name?.toKString())

        val nullRef = ObjectRef<Beyond_NET_Sample_StructTest?>(null)
        val ret = Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(nullRef)
        assertNull(ret)
        assertTrue(nullRef.value == ret)

        // TODO
//        val retNonRef = Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfParameter(nullRef.value)
//        assertNull(retNonRef)
//        assertTrue(nullRef.value == retNonRef)

        val origName = "test"
        val structRef = ObjectRef<Beyond_NET_Sample_StructTest?>(Beyond_NET_Sample_StructTest(origName.toDotNETString()))
        val ret2 = Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(structRef)

        assertNotNull(ret2)
        assertTrue(structRef.value == ret2)
        assertEquals(origName, ret2?.name?.toKString())

        // TODO
//        val retNonRef2 = Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfParameter(structRef.value)
//        assertNotNull(retNonRef2)
//        assertTrue(structRef.value == retNonRef2)
//        assertEquals(origName, ret2?.name?.toKString())

        val newName4 = "NotTest"
        ret2?.name_set(newName4.toDotNETString())
        assertEquals(newName4, ret2?.name?.toKString())
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testNullableValueTypes_readOnlyNullInstanceField() {
        val testClass = Beyond_NET_Sample_StructTestClass()
        val value = testClass.readOnlyNullInstanceField
        assertNull(value)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testNullableValueTypes_nonNullInstanceField() {
        val origName = "Test"
        val newName = "New Test"

        val testClass = Beyond_NET_Sample_StructTestClass()
        val origValue = testClass.nonNullInstanceField

        assertNotNull(origValue)
        assertEquals(origName, origValue?.name?.toKString())

        val newValue = Beyond_NET_Sample_StructTest(newName.toDotNETString())
        testClass.nonNullInstanceField_set(newValue)

        val newValueRet = testClass.nonNullInstanceField
        assertNotNull(newValueRet)
        assertEquals(newName, newValueRet?.name?.toKString())

        testClass.nonNullInstanceField_set(null)

        val nilValueRet = testClass.nonNullInstanceField
        assertNull(nilValueRet)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testNullableValueTypes_nullableStructPropertyWithGetterAndSetter() {
        val testClass = Beyond_NET_Sample_StructTestClass()

        val origValue = testClass.nullableStructPropertyWithGetterAndSetter
        assertNull(origValue)

        val newName = "Test"
        val newValue = Beyond_NET_Sample_StructTest(newName.toDotNETString())

        testClass.nullableStructPropertyWithGetterAndSetter_set(newValue)

        val newValueRet = testClass.nullableStructPropertyWithGetterAndSetter
        assertNotNull(newValueRet)
        assertEquals(newValue, newValueRet)
        assertEquals(newName, newValueRet?.name?.toKString())

        testClass.nullableStructPropertyWithGetterAndSetter_set(null)
        val nilValueRet = testClass.nullableStructPropertyWithGetterAndSetter
        assertNull(nilValueRet)
    }
}
