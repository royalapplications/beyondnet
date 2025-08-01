package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class DelegateTestTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testTransformInt() {
        val original = 0
        val valueToAdd = 1
        val expectedResult = original + valueToAdd

        val transformer = Beyond_NET_Sample_DelegatesTest_TransformIntDelegate { i ->
            i + valueToAdd
        }

        val result = Beyond_NET_Sample_DelegatesTest.transformInt(original, transformer)

        assertEquals(expectedResult, result)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testTransformStepMode() {
        var original = Beyond_NET_Sample_StepMode.IN
        var result = Beyond_NET_Sample_DelegatesTest.transformStepMode(original)
        assertEquals(original, result)

        original = Beyond_NET_Sample_StepMode.OUT
        result = Beyond_NET_Sample_DelegatesTest.transformStepMode(original)
        assertEquals(original, result)

        original = Beyond_NET_Sample_StepMode.OVER
        result = Beyond_NET_Sample_DelegatesTest.transformStepMode(original)
        assertEquals(original, result)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testTransformPoint() {
        val originalX = 0.0
        val originalY = 0.0

        val valueToAddToX = 0.1
        val valueToAddToY = 0.2

        val original = Beyond_NET_Sample_Point(originalX, originalY)
        val expectedResult = Beyond_NET_Sample_Point(originalX + valueToAddToX, originalY + valueToAddToY)

        val result = Beyond_NET_Sample_DelegatesTest.transformPoint(original, Beyond_NET_Sample_DelegatesTest_PointTransformDelegate { p ->
            val pX = p.x
            val pY = p.y

            val newX = pX + valueToAddToX
            val newY = pY + valueToAddToY

            Beyond_NET_Sample_Point(newX, newY)
        })

        val resultX = result.x
        val resultY = result.y
        val expectedX = expectedResult.x
        val expectedY = expectedResult.y

        assertEquals(resultX, expectedX, 0.001)
        assertEquals(resultY, expectedY, 0.001)
    }
}
