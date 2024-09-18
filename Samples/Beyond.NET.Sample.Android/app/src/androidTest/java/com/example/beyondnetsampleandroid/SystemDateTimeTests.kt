package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

import java.time.*
import java.util.*

@RunWith(AndroidJUnit4::class)
class SystemDateTimeTests {
    @Test
    fun testSystemDateTime() {
        val nowK = Date().toInstant()
            .atZone(ZoneId.systemDefault())
            .toLocalDateTime()

        val expectedYear = nowK.year
        val expectedMonth = nowK.month.value
        val expectedDay = nowK.dayOfMonth
        val expectedHour = nowK.hour
        val expectedMinute = nowK.minute
        val expectedSecond = nowK.second

        val nowDN = System_DateTime(expectedYear, expectedMonth, expectedDay, expectedHour, expectedMinute, expectedSecond)

        val year = nowDN.year_get()
        val month = nowDN.month_get()
        val day = nowDN.day_get()
        val hour = nowDN.hour_get()
        val minute = nowDN.minute_get()

        assertEquals(expectedYear, year)
        assertEquals(expectedMonth, month)
        assertEquals(expectedDay, day)

        assertEquals(expectedHour, hour)
        assertEquals(expectedMinute, minute)
    }

    @Test
    fun testSystemDateTimeParse() {
        val nowK = Date().toInstant()
            .atZone(ZoneId.systemDefault())
            .toLocalDateTime()

        val expectedYear = nowK.year
        val expectedMonth = nowK.month.value
        val expectedDay = nowK.dayOfMonth
        val expectedHour = nowK.hour
        val expectedMinute = nowK.minute
        val expectedSecond = nowK.second

        val cultureNameDN = "en-US".toDotNETString()
        val enUSCulture = System_Globalization_CultureInfo(cultureNameDN)

        val dateString = "$expectedMonth/$expectedDay/$expectedYear $expectedHour:$expectedMinute:$expectedSecond"
        val dateStringDN = dateString.toDotNETString()

        val nowDN = System_DateTime.parse(dateStringDN, enUSCulture)

        val year = nowDN.year_get()
        val month = nowDN.month_get()
        val day = nowDN.day_get()
        val hour = nowDN.hour_get()
        val minute = nowDN.minute_get()

        assertEquals(expectedYear, year)
        assertEquals(expectedMonth, month)
        assertEquals(expectedDay, day)

        assertEquals(expectedHour, hour)
        assertEquals(expectedMinute, minute)
    }

    @Test
    fun testDateConversion() {
        val referenceKDate = Date(0)
        val dateTime = System_DateTime(1970, 1, 1, 0, 0, 0, 0, 0, System_DateTimeKind.UTC)

        val dateTimeMinValue = System_DateTime.minValue_get()

        val retDate = dateTime.toKDate()
        assertEquals(retDate, referenceKDate)
        assertNotEquals(retDate, Date())

        val retDateTime = retDate.toDotNETDateTime()
        assertTrue(retDateTime == dateTime)
        assertFalse(retDateTime == dateTimeMinValue)
    }
}