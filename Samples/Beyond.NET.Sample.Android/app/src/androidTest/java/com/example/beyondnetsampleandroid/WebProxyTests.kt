package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class WebProxyTests {
    @Test
    fun testWebProxy() {
        val dnProxy = Beyond_NET_Sample_WebProxyTests.createWebProxy()
        val dnCredential = dnProxy.credentials
        assertNull(dnCredential)

        val proxy = System_Net_WebProxy()
        val credential = proxy.credentials
        assertNull(credential)
    }
}
