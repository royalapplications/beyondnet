package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class X509CertificatesTests {
    @Test
    fun testX509CertificateCollection() {
        val dnCollection = Beyond_NET_Sample_X509CertificatesTests.createX509CertificateCollection()
        assertNotNull(dnCollection)
    }

    @Test
    fun testX509CertificateEnumerator() {
        val dnEnumerator = Beyond_NET_Sample_X509CertificatesTests.createX509CertificateEnumerator()
        assertNotNull(dnEnumerator)
    }

    // X509Certificate2Collection is skipped by generator
//    @Test
//    fun testX509Certificate2Collection() {
//        val dnCollection = Beyond_NET_Sample_X509CertificatesTests.createX509Certificate2Collection()
//        assertNotNull(dnCollection)
//    }

    @Test
    fun testX509Certificate2Enumerator() {
        val dnEnumerator = Beyond_NET_Sample_X509CertificatesTests.createX509Certificate2Enumerator()
        assertNotNull(dnEnumerator)
    }
}
