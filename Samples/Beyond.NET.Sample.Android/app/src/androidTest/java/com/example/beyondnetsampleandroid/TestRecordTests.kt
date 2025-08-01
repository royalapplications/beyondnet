package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class TestRecordTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testRecords() {
        val expectedString = "Hello 👍"

        val aRecord = Beyond_NET_Sample_TestRecord(expectedString.toDotNETString())

        val retString = aRecord.aString.toKString()
        assertEquals(expectedString, retString)

        val deconstructedStringRefDN = ObjectRef(System_String.empty)

        aRecord.deconstruct(deconstructedStringRefDN)

        val deconstructedString = deconstructedStringRefDN.value.toKString()

        assertEquals(expectedString, deconstructedString)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testReadOnlyRecordStruct() {
        val expectedInt = Int.MAX_VALUE

        val aRecord = Beyond_NET_Sample_TestReadOnlyRecordStruct(expectedInt)
        val retInt = aRecord.anInt

        assertEquals(expectedInt, retInt)
    }
}
