package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*
import java.util.Base64

@RunWith(AndroidJUnit4::class)
class SystemConvertTests {
    @Test
    fun testBooleanConversion() {
        val trueStringDN = "true".toDotNETString()
        val falseStringDN = "false".toDotNETString()
        val nonsenseStringDN = "nonsense".toDotNETString()

        val result1 = System_Convert.toBoolean(trueStringDN)
        assertTrue(result1)

        val result2 = System_Convert.toBoolean(falseStringDN)
        assertFalse(result2)

        assertThrows(DNException::class.java) { System_Convert.toBoolean(nonsenseStringDN) }
    }

    @Test
    fun testIntegerConversion() {
        val number1: Int = 123456789
        val number1StringDN = "$number1".toDotNETString()

        assertEquals(number1, System_Convert.toInt32(number1StringDN))

        val number2: Long = -123456789
        val number2StringDN = "$number2".toDotNETString()

        assertEquals(number2, System_Convert.toInt64(number2StringDN))

        val number3StringDN = "nonsense".toDotNETString()
        assertThrows(DNException::class.java) { System_Convert.toInt64(number3StringDN) }

        val number4StringDN = "nonsense".toDotNETString()
        assertThrows(DNException::class.java) { System_Convert.toUInt64(number4StringDN) }
    }

    @Test
    fun testBase64Conversion() {
        val text = "Hello World!"
        val textDN = text.toDotNETString()

        val utf8Encoding = System_Text_Encoding.uTF8_get()
        val textBytes = utf8Encoding.getBytes(textDN)
        val textAsBase64StringDN = System_Convert.toBase64String(textBytes)
        val textAsBase64String = textAsBase64StringDN.toKString()

        val textAsBase64ByteArray = Base64.getDecoder().decode(textAsBase64String)
        assertNotNull(textAsBase64ByteArray)

        val decodedText = String(textAsBase64ByteArray)
        assertNotNull(decodedText)

        assertEquals(text, decodedText)

        val textBytesRet = System_Convert.fromBase64String(textAsBase64StringDN)
        val textRet = utf8Encoding.getString(textBytesRet).toKString()
        assertEquals(text, textRet)
    }
}