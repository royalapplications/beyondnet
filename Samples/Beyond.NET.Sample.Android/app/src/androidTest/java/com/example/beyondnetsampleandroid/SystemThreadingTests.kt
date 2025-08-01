package com.example.beyondnetsampleandroid

import android.os.Looper
import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class SystemThreadingTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testThread() {
        var numberOfTimesCalled = 0

        val closure: () -> Unit = {
            val isMainThread = Looper.myLooper() == Looper.getMainLooper()
            assertFalse(isMainThread)

            numberOfTimesCalled += 1
        }

        val threadStart = System_Threading_ThreadStart(closure)
        val thread = System_Threading_Thread(threadStart)

        thread.start()

        while (numberOfTimesCalled < 1) {
            Thread.sleep(10)
        }

        assertEquals(1, numberOfTimesCalled)
    }
}
