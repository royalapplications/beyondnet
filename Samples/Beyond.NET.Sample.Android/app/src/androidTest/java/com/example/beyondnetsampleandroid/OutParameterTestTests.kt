package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class OutParameterTestTests {
    // MARK: - Primitives
    // NOTE: This was copied from the Swift tests
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
//    // NOTE: This was copied from the Swift tests
//    @Test
//    fun testNonOptionalEnums() {
//        val inst = makeInstance()
//
//        val returnValue = System.DateTimeKind.unspecified
//        inst.return_DateTimeKind_Utc_NonOptional(&returnValue)
//        assertEquals(returnValue, System_DateTimeKind.UTC)
//    }

    // MARK: - Structs
    // NOTE: This was copied from the Swift tests
    @Test
    fun testNonOptionalStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef(System_DateTime.minValue)
        inst.return_DateTime_MaxValue_NonOptional(returnValue)
        assertEquals(returnValue.value, System_DateTime.maxValue)

        // TODO
//        val returnValueWithPlaceholder = System.DateTime.outParameterPlaceholder
//        try inst.return_DateTime_MaxValue_NonOptional(&returnValueWithPlaceholder)
//        XCTAssertEqual(returnValueWithPlaceholder, System.DateTime.maxValue)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testOptionalStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_DateTime?>(null)
        inst.return_DateTime_MaxValue_Optional(returnValue)
        assertEquals(returnValue.value, System_DateTime.maxValue)

        val returnValueWithInValue = ObjectRef<System_DateTime?>(System_DateTime.minValue)
        inst.return_DateTime_MaxValue_Optional(returnValueWithInValue)
        assertEquals(returnValueWithInValue.value, System_DateTime.maxValue)

        // TODO
//        var returnValueWithPlaceholder: System.DateTime? = System.DateTime.outParameterPlaceholder
//        try inst.return_DateTime_MaxValue_Optional(&returnValueWithPlaceholder)
//        XCTAssertEqual(returnValueWithPlaceholder, System.DateTime.maxValue)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testOptionalNullStructs() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_DateTime?>(null)
        inst.return_DateTime_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_DateTime?>(System_DateTime.minValue)
        inst.return_DateTime_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        // TODO
//        var returnValueWithPlaceholder: System.DateTime? = System.DateTime.outParameterPlaceholder
//        try inst.return_DateTime_Null(&returnValueWithPlaceholder)
//        XCTAssertNil(returnValueWithPlaceholder)
    }

    // MARK: - Classes
    // NOTE: This was copied from the Swift tests
    @Test
    fun testNonOptionalClasses() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef(System_String.empty)
        inst.return_String_Abc_NonOptional(returnValue)
        assertEquals(returnValue.value, abc)

        // TODO
//        var returnValueWithPlaceholder = System.String.outParameterPlaceholder
//        try inst.return_String_Abc_NonOptional(&returnValueWithPlaceholder)
//        XCTAssertEqual(returnValueWithPlaceholder, abc)
    }

    // NOTE: This was copied from the Swift tests
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

        // TODO
//        var returnValueWithPlaceholder: System.String? = System.String.outParameterPlaceholder
//        try inst.return_String_Abc_Optional(&returnValueWithPlaceholder)
//        XCTAssertEqual(returnValue, abc)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testOptionalNullClasses() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_String?>(null)
        inst.return_String_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_String?>(System_String.empty)
        inst.return_String_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        // TODO
//        var returnValueWithPlaceholder: System.String? = System.String.outParameterPlaceholder
//        try inst.return_String_Null(&returnValueWithPlaceholder)
//        XCTAssertNil(returnValueWithPlaceholder)
    }

    // MARK: - Interfaces
    // NOTE: This was copied from the Swift tests
    @Test
    fun testNonOptionalInterfaces() {
        val inst = makeInstance()

        val abc = "Abc".toDotNETString()

        val returnValue = ObjectRef<System_Collections_IEnumerable>(System_String.empty)
        inst.return_IEnumerable_String_Abc_NonOptional(returnValue)
        assertEquals(returnValue.value.castTo(System_String.typeOf), abc)

        // TODO
//        var returnValueWithPlaceholder: System.Collections.IEnumerable = System.Collections.IEnumerable_DNInterface.outParameterPlaceholder
//        try inst.return_IEnumerable_String_Abc_NonOptional(&returnValueWithPlaceholder)
//        XCTAssertEqual(try returnValueWithPlaceholder.castTo(System.String.self), abc)
    }

    // NOTE: This was copied from the Swift tests
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

        // TODO
//        var returnValueWithPlaceholder: System.Collections.IEnumerable? = System.String.outParameterPlaceholder
//        try inst.return_IEnumerable_String_Abc_Optional(&returnValueWithPlaceholder)
//        XCTAssertEqual(try returnValueWithPlaceholder?.castTo(System.String.self), abc)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testOptionalNullInterfaces() {
        val inst = makeInstance()

        val returnValue = ObjectRef<System_Collections_IEnumerable?>(null)
        inst.return_IEnumerable_Null(returnValue)
        assertNull(returnValue.value)

        val returnValueWithInValue = ObjectRef<System_Collections_IEnumerable?>(System_String.empty)
        inst.return_IEnumerable_Null(returnValueWithInValue)
        assertNull(returnValueWithInValue.value)

        // TODO
//        var returnValueWithPlaceholder: System.Collections.IEnumerable? = System.Collections.IEnumerable_DNInterface.outParameterPlaceholder
//        try inst.return_IEnumerable_Null(&returnValueWithPlaceholder)
//        XCTAssertNil(returnValueWithPlaceholder)
    }

    private fun makeInstance(): Beyond_NET_Sample_Source_OutParameterTests {
        return Beyond_NET_Sample_Source_OutParameterTests()
    }
}
