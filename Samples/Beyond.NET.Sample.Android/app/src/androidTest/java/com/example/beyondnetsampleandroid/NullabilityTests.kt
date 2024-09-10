package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class NullabilityTests {
    @Test
    fun testMethodPropertyAndFieldNullability() {
        val test = Beyond_NET_Sample_NullabilityTests()

        val helloString = "Hello".toDotNETString()
        val nullString: System_String? = null

        assertEquals(test.methodWithNonNullableStringParameter(helloString), helloString)
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