package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class PersonTests {
    @Test
    fun testPerson() {
        val johnDoe = Beyond_NET_Sample_Person.makeJohnDoe()

        assertEquals(johnDoe.fullName_get().toKString(), "John Doe")
        assertEquals(johnDoe.age_get(), 50)
        assertEquals(johnDoe.niceLevel_get(), Beyond_NET_Sample_NiceLevels.NICE)
        assertEquals(johnDoe.numberOfChildren_get(), 0)

        johnDoe.increaseAge(2)

        assertEquals(johnDoe.age_get(), 52)

        val address = Beyond_NET_Sample_Address("Homestreet".toDotNETString(), "Hometwon".toDotNETString())
        johnDoe.address_set(address)

        val addressRetRef = ObjectRef<Beyond_NET_Sample_Address?>(null)
        val getAddressSuccess = johnDoe.tryGetAddress(addressRetRef)

        assertTrue(getAddressSuccess)
        assertEquals(addressRetRef.value, address)

        val babyJohn = Beyond_NET_Sample_Person("John".toDotNETString(), "Doe Junior".toDotNETString(), 0)
        johnDoe.addChild(babyJohn)

        assertEquals(johnDoe.numberOfChildren_get(), 1)

        val firstChildRef = ObjectRef<Beyond_NET_Sample_Person?>(null)
        assertNull(firstChildRef.value)
        val getChildSuccess = johnDoe.tryGetChildAt(0, firstChildRef)
        assertTrue(getChildSuccess)
        assertEquals(firstChildRef.value, babyJohn)

        val websiteStr = "https://royalapps.com"
        val websiteUriRef = ObjectRef<System_Uri?>(null)
        val uriCreateSuccess = System_Uri.tryCreate(websiteStr.toDotNETString(), System_UriKind.ABSOLUTE, websiteUriRef)
        assertTrue(uriCreateSuccess)

        val websiteUri = websiteUriRef.value
        assertNotNull(websiteUri)

        johnDoe.website_set(websiteUri)

        assertEquals(johnDoe.website_get(), websiteUri)
    }
}