package com.example.beyondnetsampleandroid

import android.os.Looper
import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemThreadingTimerTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testTimer() {
        val maximumNumberOfTimesToCall = 30
        var numberOfTimesCalled = 0

        var nullableTimer: System_Threading_Timer? = null

        val closure: (state: System_Object?) -> Unit = { state ->
            val isMainThread = Looper.myLooper() == Looper.getMainLooper()
            assertFalse(isMainThread)

            numberOfTimesCalled += 1

            if (numberOfTimesCalled == maximumNumberOfTimesToCall) {
                nullableTimer?.let {
                    it.dispose()
                } ?: {
                    fail("timer is null")

                    // TODO: return
                }
            }
        }

        val timerCallback = System_Threading_TimerCallback(closure)

        val timer = System_Threading_Timer(
            timerCallback,
            null,
            50,
            10
        )

        nullableTimer = timer

        while (numberOfTimesCalled != maximumNumberOfTimesToCall) {
            Thread.sleep(10)
        }

        assertEquals(numberOfTimesCalled, maximumNumberOfTimesToCall)
    }
}
