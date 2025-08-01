package com.example.beyondnetsampleandroid

import com.example.beyondnetsampleandroid.dn.*

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.sun.jna.ptr.*
import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class SystemActionTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testSystemAction() {
        var numberOfTimesCalled = 0

        val action = System_Action {
            numberOfTimesCalled += 1
        }

        assertEquals(0, numberOfTimesCalled)

        action.invoke()
        assertEquals(1, numberOfTimesCalled)

        action.invoke()
        action.invoke()
        action.invoke()
        assertEquals(4, numberOfTimesCalled)
    }

    @Test
    fun testSystemActionThrowingAnException() {
        var numberOfTimesCalled = 0

        val action = System_Action {
            throw Exception("Oh no!")

            numberOfTimesCalled += 1
        }

//        try {
//
//        } catch (t: Throwable) {
//            error("Blah ${t.toString()}")
//        }

        assertEquals(0, numberOfTimesCalled)

        action.invoke()
        assertEquals(0, numberOfTimesCalled)

        action.invoke()
        action.invoke()
        action.invoke()
        assertEquals(0, numberOfTimesCalled)
    }

    @Test
    fun testSystemAction_C_API() {
        var numberOfTimesCalled = 0

        val action = CAPI.System_Action_Create {
            numberOfTimesCalled += 1
        }

        assertEquals(0, numberOfTimesCalled)

        val exceptionC = PointerByReference()
        val exceptionCHandle = exceptionC.value

        CAPI.System_Action_Invoke(action, exceptionC)
        assertNull(exceptionCHandle)
        assertEquals(1, numberOfTimesCalled)

        CAPI.System_Action_Invoke(action, exceptionC)
        assertNull(exceptionCHandle)
        CAPI.System_Action_Invoke(action, exceptionC)
        assertNull(exceptionCHandle)
        CAPI.System_Action_Invoke(action, exceptionC)
        assertNull(exceptionCHandle)

        assertEquals(4, numberOfTimesCalled)

        CAPI.System_Action_Destroy(action)

        // Comment in for testing delegate memory management
        /*
        fun gcNow() {
            System_GC.collect(System_GC.maxGeneration_get(), System_GCCollectionMode.FORCED, blocking = true, compacting = true)
            System_GC.waitForPendingFinalizers()
        }

        gcNow()
        Thread.sleep(10)
        gcNow()

        // The callback wrapper instance should've been destroyed by now
         */
    }
}
