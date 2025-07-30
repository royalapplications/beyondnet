package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class EventTestTests {
    // NOTE: This was copied from the Swift tests
    @Test
    fun testEventTest() {
        val eventTest = Beyond_NET_Sample_EventTests()

        val expectedNewValue = 5
        var newValuesPassedToValueChangedHandler = mutableListOf<Int>()

        val eventHandler = Beyond_NET_Sample_EventTests_ValueChangedDelegate { sender, newValue ->
            newValuesPassedToValueChangedHandler.add(newValue)
        }

        eventTest.valueChanged_add(eventHandler)

        eventTest.value_set(expectedNewValue)

        eventTest.valueChanged_remove(eventHandler)

        eventTest.value_set(10)

        assertEquals(1, newValuesPassedToValueChangedHandler.count())
        assertEquals(newValuesPassedToValueChangedHandler[0], expectedNewValue)
    }
}
