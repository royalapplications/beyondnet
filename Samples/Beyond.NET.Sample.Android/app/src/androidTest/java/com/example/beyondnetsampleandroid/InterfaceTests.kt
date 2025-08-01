package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4
import com.example.beyondnetsampleandroid.dn.*

import org.junit.Assert.*
import org.junit.*
import org.junit.runner.*

@RunWith(AndroidJUnit4::class)
class InterfaceTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testPassingInInterfaces() {
        val typeThatImplementsMultipleInterfaces = Beyond_NET_Sample_TypeThatImplementsMultipleInterfaces()
        val typeThatUsesInterfaces = Beyond_NET_Sample_TypeThatUsesInterfaces()

        val value = 5
        typeThatUsesInterfaces.callMethod1InIInterface1(typeThatImplementsMultipleInterfaces)
        typeThatUsesInterfaces.setPropertyInIInterface2(typeThatImplementsMultipleInterfaces, value)
        val retValue = typeThatUsesInterfaces.getPropertyInIInterface2(typeThatImplementsMultipleInterfaces)
        assertEquals(value, retValue)
        typeThatUsesInterfaces.callMethod1InIInterface3(typeThatImplementsMultipleInterfaces)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testStaticMethodOnInterface() {
        val inst = Beyond_NET_Sample_IInterface1.createDefaultInstance()
        assertTrue(inst.`is`(Beyond_NET_Sample_TypeThatImplementsMultipleInterfaces))

        inst.methodInIInterface1()
    }

    // NOTE: This was transpiled from Swift
    // TODO
//    @Test
//    fun testRetrievingInterfaces() {
//
//    }

    // TODO: How to test async code in Kotlin/Java?
//    // NOTE: This was transpiled from Swift
//    @Test
//    fun testInterfaceAdapter() {
//        val typeThatUsesInterfaces = Beyond_NET_Sample_TypeThatUsesInterfaces()
//
////        val methodInIInterface1CalledExpectation = expectation(description: "IInterface1.MethodInIInterface1 called in Swift")
//
//        // Compiler ensures we provide all interface requirements.
//        // Ideally, we'd have an auto-generated Swift wrapper type that sets up all the delegate -> closure callbacks and allows us to just override the members required to satisfy the interface/protocol requirements.
//        val interface1Adapter = Beyond_NET_Sample_IInterface1_DelegateAdapter(
//            Beyond_NET_Sample_IInterface1_DelegateAdapter_MethodInIInterface1_Delegate {
//            print("IInterface1.MethodInIInterface1 called in Swift")
//
//            methodInIInterface1CalledExpectation.fulfill()
//        })
//
//        try typeThatUsesInterfaces.callMethod1InIInterface1(interface1Adapter)
//
//        wait(for: [ methodInIInterface1CalledExpectation ])
//    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testDelegateThatReceivesInterface() {
        val typeThatUsesInterfaces = Beyond_NET_Sample_TypeThatUsesInterfaces()
        val typeThatImplementsInterface = Beyond_NET_Sample_TypeThatImplementsMultipleInterfaces()

        val del = Beyond_NET_Sample_TypeThatUsesInterfaces_DelegateThatReceivesIInterface1 { interface1 ->
            interface1.methodInIInterface1()
        }

        typeThatUsesInterfaces.delegateThatReceivesInterfaceTest(del, typeThatImplementsInterface)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testDelegateThatReturnsInterface() {
        val typeThatUsesInterfaces = Beyond_NET_Sample_TypeThatUsesInterfaces()

        val del = Beyond_NET_Sample_TypeThatUsesInterfaces_DelegateThatReturnsIInterface1 {
            Beyond_NET_Sample_TypeThatImplementsMultipleInterfaces()
        }

        val returnedInterface = typeThatUsesInterfaces.delegateThatReturnsInterfaceTest(del)

        returnedInterface.methodInIInterface1()
    }
}
