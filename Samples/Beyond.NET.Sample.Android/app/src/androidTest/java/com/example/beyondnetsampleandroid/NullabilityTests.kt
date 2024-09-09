package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*
import com.sun.jna.Pointer

interface IDNObjectTest {
    val __handle: Pointer
}

class DNObjectTest(handle: Pointer): IDNObjectTest {
    override val __handle: Pointer

    init {
        require(handle !== Pointer.NULL) {
            "Cannot initialize DNObject with a null pointer"
        }

        this.__handle = handle
    }
}

@JvmName("getHandleOrNullPointer_optional")
fun IDNObjectTest?.getHandleOrNullPointer(): Pointer {
    if (this == null) {
        val nullPtr = Pointer.NULL

        // TODO: This throws: java.lang.NullPointerException: NULL must not be null
        return nullPtr
    }

    return this.__handle
}

@RunWith(AndroidJUnit4::class)
class NullabilityTests {
    @Test
    fun testMethodPropertyAndFieldNullability() {
        // TODO: This is a playground for Pointer nullability
        val nullObj: DNObjectTest? = null
        val nullPtr = nullObj.getHandleOrNullPointer()



        // These are the real tests
        val test = Beyond_NET_Sample_NullabilityTests()

        val helloString = "Hello".toDotNETString()
        val nullString: System_String? = null

        assertEquals(test.methodWithNonNullableStringParameter(helloString), helloString)

        // TODO: This currently fails because nullability at the JNA Pointer levels seems to be handled incorrectly
        assertEquals(test.methodWithNullableStringParameter(nullString), nullString)

        assertEquals(test.nonNullableStringProperty_get(), helloString)
        assertEquals(test.nonNullableStringField_get(), helloString)
        assertEquals(test.nullableStringProperty_get(), nullString)
        assertEquals(test.nullableStringField_get(), nullString)

        assertEquals(test.methodWithNonNullableStringReturnValue(), helloString)
        assertEquals(test.methodWithNullableStringReturnValue(), nullString)
    }

    @Test
    fun testConstructor() {
        // Should not throw
        val inst1 = Beyond_NET_Sample_NullabilityTests(false)
        assertNotNull(inst1)

        assertThrows(DNException::class.java) {
            // Should throw
            Beyond_NET_Sample_NullabilityTests(true)
        }
    }

    @Test
    fun testStaticMethod() {
        // Should not throw
        val inst1 = Beyond_NET_Sample_NullabilityTests.createInstance(false)
        assertNotNull(inst1)

        assertThrows(DNException::class.java) {
            // Should throw
            Beyond_NET_Sample_NullabilityTests.createInstance(true)
        }
    }
}