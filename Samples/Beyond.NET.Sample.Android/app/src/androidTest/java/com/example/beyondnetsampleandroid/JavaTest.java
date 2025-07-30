package com.example.beyondnetsampleandroid;

import androidx.test.ext.junit.runners.AndroidJUnit4;

import com.example.beyondnetsampleandroid.dn.*;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;

@RunWith(AndroidJUnit4.class)
public class JavaTest {
    @Test
    public void testPerson() {
        var johnDoe = Beyond_NET_Sample_Person.Companion.makeJohnDoe();

//        Assert.assertEquals("John Doe", johnDoe.fullName_get().toKString());
        Assert.assertEquals(50, johnDoe.age_get());
        Assert.assertEquals(Beyond_NET_Sample_NiceLevels.NICE, johnDoe.niceLevel_get());
        Assert.assertEquals(0, johnDoe.numberOfChildren_get());

        // TODO: Lots of missing/not working stuff
//        johnDoe.increaseAge(2)
//
//        Assert.assertEquals(johnDoe.age, 52)
//
//        val address = Beyond_NET_Sample_Address("Homestreet".toDotNETString(), "Hometwon".toDotNETString())
//        johnDoe.address_set(address)
//
//        val addressRetRef = ObjectRef<Beyond_NET_Sample_Address?>(null)
//        val getAddressSuccess = johnDoe.tryGetAddress(addressRetRef)
//
//        assertTrue(getAddressSuccess)
//        Assert.assertEquals(addressRetRef.value, address)
//
//        val babyJohn = Beyond_NET_Sample_Person("John".toDotNETString(), "Doe Junior".toDotNETString(), 0)
//        johnDoe.addChild(babyJohn)
//
//        Assert.assertEquals(johnDoe.numberOfChildren, 1)
//
//        val firstChildRef = ObjectRef<Beyond_NET_Sample_Person?>(null)
//        assertNull(firstChildRef.value)
//        val getChildSuccess = johnDoe.tryGetChildAt(0, firstChildRef)
//        assertTrue(getChildSuccess)
//        Assert.assertEquals(firstChildRef.value, babyJohn)
//
//        val websiteStr = "https://royalapps.com"
//        val websiteUriRef = ObjectRef<System_Uri?>(null)
//        val uriCreateSuccess = System_Uri.tryCreate(websiteStr.toDotNETString(), System_UriKind.ABSOLUTE, websiteUriRef)
//        assertTrue(uriCreateSuccess)
//
//        val websiteUri = websiteUriRef.value
//        assertNotNull(websiteUri)
//
//        johnDoe.website_set(websiteUri)
//
//        Assert.assertEquals(johnDoe.website, websiteUri)
    }
}
