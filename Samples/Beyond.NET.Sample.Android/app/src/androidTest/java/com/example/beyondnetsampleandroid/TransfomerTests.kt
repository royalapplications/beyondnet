package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class TransfomerTests {
    // NOTE: This was copied from the Swift tests
    @Test
    fun testStringTransformer() {
        val uppercaser = createUppercaser()

        val inputString = "Hello"
        val inputStringDN = inputString.toDotNETString()

        val expectedOutputString = inputString.uppercase()

        val outputString = Beyond_NET_Sample_Transformer.transformString(
            inputStringDN,
            uppercaser
        ).toKString()

        assertEquals(expectedOutputString, outputString)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testStringGetterAndTransformer() {
        val fixedStringProvider = createFixedStringProvider()
        val uppercaser = createUppercaser()

        val outputString = Beyond_NET_Sample_Transformer.getAndTransformString(
            fixedStringProvider,
            uppercaser
        ).toKString()

        assertEquals("FIXED STRING", outputString)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testDoublesTransformer() {
        val doublesTransformerDelegate = Beyond_NET_Sample_Transformer_DoublesTransformerDelegate { number1, number2 ->
            number1 * number2
        }

        val inputNumber1 = 2.5
        val inputNumber2 = 3.5

        val expectedResult = inputNumber1 * inputNumber2

        val result = Beyond_NET_Sample_Transformer.transformDoubles(
            inputNumber1,
            inputNumber2,
            doublesTransformerDelegate
        )

        assertEquals(expectedResult, result, 0.0001)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testUppercaserThatActuallyLowercases() {
        val lowercaser = createLowercaser()

        // "Remember" original uppercaser
        val origUppercaser = Beyond_NET_Sample_Transformer_BuiltInTransformers.uppercaseStringTransformer
        Beyond_NET_Sample_Transformer_BuiltInTransformers.uppercaseStringTransformer_set(lowercaser)

        val inputString = "Hello"
        val inputStringDN = inputString.toDotNETString()

        val expectedOutputString = inputString.lowercase()

        val outputString = Beyond_NET_Sample_Transformer.uppercaseString(inputStringDN).toKString()
        assertEquals(expectedOutputString, outputString)

        // Restore original uppercaser
        Beyond_NET_Sample_Transformer_BuiltInTransformers.uppercaseStringTransformer_set(origUppercaser)

        val expectedOutputStringWithOrig = inputString.uppercase()

        val outputStringWithOrig = Beyond_NET_Sample_Transformer.uppercaseString(inputStringDN).toKString()
        assertEquals(expectedOutputStringWithOrig, outputStringWithOrig)
    }

    // NOTE: This was copied from the Swift tests
    private fun createFixedStringProvider(): Beyond_NET_Sample_Transformer_StringGetterDelegate {
        val fixedStringProvider = Beyond_NET_Sample_Transformer_StringGetterDelegate {
            val outputString = "Fixed String"
            val outputStringDN = outputString.toDotNETString()

            outputStringDN
        }

        return fixedStringProvider
    }

    // NOTE: This was copied from the Swift tests
    private fun createUppercaser(): Beyond_NET_Sample_Transformer_StringTransformerDelegate {
        val caser = Beyond_NET_Sample_Transformer_StringTransformerDelegate { inputStringDN ->
            val inputString = inputStringDN.toKString()

            val outputString = inputString.uppercase()
            val outputStringDN = outputString.toDotNETString()

            outputStringDN
        }

        return caser
    }

    // NOTE: This was copied from the Swift tests
    private fun createLowercaser(): Beyond_NET_Sample_Transformer_StringTransformerDelegate {
        val caser = Beyond_NET_Sample_Transformer_StringTransformerDelegate { inputStringDN ->
            val inputString = inputStringDN.toKString()

            val outputString = inputString.lowercase()
            val outputStringDN = outputString.toDotNETString()

            outputStringDN
        }

        return caser
    }
}
