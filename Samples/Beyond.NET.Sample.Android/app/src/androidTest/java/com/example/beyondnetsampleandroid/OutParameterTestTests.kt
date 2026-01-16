package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class OutParameterTestTests {
    // MARK: - Primitives
    // NOTE: This was transpiled from Swift
    @Test
    fun testNonOptionalPrimitives() {
        val inst = makeInstance()

        val returnValue = IntRef(0)
        inst.return_Int_1_NonOptional(returnValue)
        assertEquals(returnValue.value, 1)
    }

    // NOTE: Beyond.NET does not currently support nullable primitives. That's why the other test cases are not implemented.
//    func testOptionalPrimitives() throws { }
//    func testOptionalNullPrimitives() throws { }

    // MARK: - Enums
    // TODO
//    // NOTE: This was transpiled from Swift
//    @Test
//    fun testNonOptionalEnums() {
//        val inst = makeInstance()
//
//        val returnValue = System_DateTimeKind.UNSPECIFIED
//        inst.return_DateTimeKind_Utc_NonOptional(&returnValue)
//        assertEquals(returnValue, System_DateTimeKind.UTC)
//    }

    // MARK: - Structs
    // NOTE: This was transpiled from Swift
    @Test
    fun testNonOptionalStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef(System_DateTime.minValue)
        inst.return_DateTime_MaxValue_NonOptional(returnValue)
        assertEquals(returnValue.value, System_DateTime.maxValue)

        val returnValueWithPlaceholder = ObjectRef(DNOutParameter.createPlaceholder(::System_DateTime))
        inst.return_DateTime_MaxValue_NonOptional(returnValueWithPlaceholder)
        assertEquals(returnValueWithPlaceholder.value, System_DateTime.maxValue)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_DateTime?>(null)
        inst.return_DateTime_MaxValue_Optional(returnValue)
        assertEquals(returnValue.value, System_DateTime.maxValue)

        val returnValueWithInValue = ObjectRef<System_DateTime?>(System_DateTime.minValue)
        inst.return_DateTime_MaxValue_Optional(returnValueWithInValue)
        assertEquals(returnValueWithInValue.value, System_DateTime.maxValue)

        val returnValueWithPlaceholder = ObjectRef<System_DateTime?>(DNOutParameter.createPlaceholder(::System_DateTime))
        inst.return_DateTime_MaxValue_Optional(returnValueWithPlaceholder)
        assertEquals(returnValueWithPlaceholder.value, System_DateTime.maxValue)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalNullStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_DateTime?>(null)
        inst.return_DateTime_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_DateTime?>(System_DateTime.minValue)
        inst.return_DateTime_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        val returnValueWithPlaceholder = ObjectRef<System_DateTime?>(DNOutParameter.createPlaceholder(::System_DateTime))
        inst.return_DateTime_Null(returnValueWithPlaceholder)
        assertNull(returnValueWithPlaceholder.value)
    }

    // MARK: - Classes
    // NOTE: This was transpiled from Swift
    @Test
    fun testNonOptionalClasses() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef(System_String.empty)
        inst.return_String_Abc_NonOptional(returnValue)
        assertEquals(returnValue.value, abc)

        val returnValueWithPlaceholder = ObjectRef(DNOutParameter.createPlaceholder(::System_String))
        inst.return_String_Abc_NonOptional(returnValueWithPlaceholder)
        assertEquals(returnValueWithPlaceholder.value, abc)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalClasses() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef<System_String?>(null)
        inst.return_String_Abc_Optional(returnValue)
        assertEquals(returnValue.value, abc)

        val returnValueWithInValue = ObjectRef<System_String?>(System_String.empty)
        inst.return_String_Abc_Optional(returnValueWithInValue)
        assertEquals(returnValue.value, abc)

        val returnValueWithPlaceholder = ObjectRef<System_String?>(DNOutParameter.createPlaceholder(::System_String))
        inst.return_String_Abc_Optional(returnValueWithPlaceholder)
        assertEquals(returnValue.value, abc)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalNullClasses() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_String?>(null)
        inst.return_String_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_String?>(System_String.empty)
        inst.return_String_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        val returnValueWithPlaceholder = ObjectRef<System_String?>(DNOutParameter.createPlaceholder(::System_String))
        inst.return_String_Null(returnValueWithPlaceholder)
        assertNull(returnValueWithPlaceholder.value)
    }

    // MARK: - Interfaces
    // NOTE: This was transpiled from Swift
    @Test
    fun testNonOptionalInterfaces() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef<System_Collections_IEnumerable>(System_String.empty)
        inst.return_IEnumerable_String_Abc_NonOptional(returnValue)
        assertEquals(returnValue.value.castTo(System_String.typeOf), abc)

        val returnValueWithPlaceholder = ObjectRef<System_Collections_IEnumerable>(DNOutParameter.createPlaceholder(::System_Collections_IEnumerable_DNInterface))
        inst.return_IEnumerable_String_Abc_NonOptional(returnValueWithPlaceholder)
        assertEquals(returnValueWithPlaceholder.value.castTo(System_String), abc)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalInterfaces() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef<System_Collections_IEnumerable?>(null)
        inst.return_IEnumerable_String_Abc_Optional(returnValue)
        assertEquals(returnValue.value?.castTo(System_String.typeOf), abc)

        val returnValueWithInValue = ObjectRef<System_Collections_IEnumerable?>(System_String.empty)
        inst.return_IEnumerable_String_Abc_Optional(returnValueWithInValue)
        assertEquals(returnValueWithInValue.value?.castTo(System_String.typeOf), abc)

        val returnValueWithPlaceholder = ObjectRef<System_Collections_IEnumerable?>(DNOutParameter.createPlaceholder(::System_String))
        inst.return_IEnumerable_String_Abc_Optional(returnValueWithPlaceholder)
        assertEquals(returnValueWithPlaceholder.value?.castTo(System_String), abc)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalNullInterfaces() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_Collections_IEnumerable?>(null)
        inst.return_IEnumerable_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_Collections_IEnumerable?>(System_String.empty)
        inst.return_IEnumerable_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        val returnValueWithPlaceholder = ObjectRef<System_Collections_IEnumerable?>(DNOutParameter.createPlaceholder(::System_Collections_IEnumerable_DNInterface))
        inst.return_IEnumerable_Null(returnValueWithPlaceholder)
        assertNull(returnValueWithPlaceholder.value)
    }

    // MARK: - Arrays
    // NOTE: This was transpiled from Swift
    @Test
    fun testNonOptionalArrays() {
        val inst = makeInstance()

        val returnValue = ObjectRef(DNArray.empty(System_String))
        inst.return_StringArray_NonOptional(returnValue)
        assertEquals(returnValue.value.length, 0)

        val returnValueWithPlaceholder = ObjectRef(DNOutParameter.createPlaceholder { ptr ->
            DNArray(System_String, ptr)
        })

        inst.return_StringArray_NonOptional(returnValueWithPlaceholder)
        assertTrue(returnValueWithPlaceholder.value.length == 0)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalArrays() {
        val inst = makeInstance()

        val returnValue = ObjectRef<DNArray<System_String>?>(null)
        inst.return_StringArray_Optional(returnValue)
        assertEquals(returnValue.value?.length, 0)

        val returnValueWithInValue = ObjectRef<DNArray<System_String>?>(DNArray.empty(System_String))
        inst.return_StringArray_Optional(returnValueWithInValue)
        assertNotNull(returnValue.value)
        assertEquals(returnValue.value?.length, 0)

        val returnValueWithPlaceholder = ObjectRef(DNOutParameter.createPlaceholder { ptr ->
            DNArray(System_String, ptr)
        } as DNArray<System_String>?)

        inst.return_StringArray_Optional(returnValueWithPlaceholder)
        assertNotNull(returnValueWithPlaceholder.value)
        assertTrue(returnValueWithPlaceholder.value?.length == 0)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testOptionalNullArrays() {
        val inst = makeInstance()

        val returnValue = ObjectRef<DNArray<System_String>?>(null)
        inst.return_StringArray_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<DNArray<System_String>?>(DNArray.empty(System_String))
        inst.return_StringArray_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        val returnValueWithPlaceholder = ObjectRef(DNOutParameter.createPlaceholder { ptr ->
            DNArray(System_String, ptr)
        } as DNArray<System_String>?)

        inst.return_StringArray_Null(returnValueWithPlaceholder)
        assertNull(returnValueWithPlaceholder.value)
    }

    // Helpers
    private fun makeInstance(): Beyond_NET_Sample_Source_OutParameterTests {
        return Beyond_NET_Sample_Source_OutParameterTests()
    }
}
