package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class PersonTests {
    // NOTE: This was copied from the Swift tests
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

    // NOTE: This was copied from the Swift tests
    @Test
    fun testPersonEvents() {
        val initialAge = 0

        val firstNameDN = "Johanna".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val person = Beyond_NET_Sample_Person(
            firstNameDN,
            lastNameDN,
            initialAge
        )

        val ageAfterCreation = person.age_get()
        assertEquals(initialAge, ageAfterCreation)

        val newAgeProviderDelegate = Beyond_NET_Sample_Person_NewAgeProviderDelegate {
            10
        }

        person.changeAge(newAgeProviderDelegate)

        val age = person.age_get()
        assertEquals(10, age)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testPersonChangeAge() {
        var numberOfTimesNumberOfChildrenChangedWasCalled = 0

        val motherFirstNameDN = "Johanna".toDotNETString()
        val sonFirstNameDN = "Max".toDotNETString()
        val daugtherFirstNameDN = "Marie".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val mother = Beyond_NET_Sample_Person(
            motherFirstNameDN,
            lastNameDN,
            40
        )

        val numberOfChildrenChangedDelegate = Beyond_NET_Sample_Person_NumberOfChildrenChangedDelegate {
            numberOfTimesNumberOfChildrenChangedWasCalled += 1
        }

        mother.numberOfChildrenChanged_add(numberOfChildrenChangedDelegate)

        val son = Beyond_NET_Sample_Person(
            sonFirstNameDN,
            lastNameDN,
            4
        )

        mother.addChild(son)

        val daughter = Beyond_NET_Sample_Person(
            daugtherFirstNameDN,
            lastNameDN,
            10
        )

        mother.addChild(daughter)

        val numberOfChildren = mother.numberOfChildren_get()

        val expectedNumberOfChildren = 2

        assertEquals(expectedNumberOfChildren, numberOfChildren)
        assertEquals(expectedNumberOfChildren, numberOfTimesNumberOfChildrenChangedWasCalled)

        mother.removeChild(daughter)

        assertEquals(3, numberOfTimesNumberOfChildrenChangedWasCalled)

        mother.numberOfChildrenChanged_remove(numberOfChildrenChangedDelegate)

        mother.removeChildAt(0)
        assertEquals(3, numberOfTimesNumberOfChildrenChangedWasCalled)

        val numberOfChildrenAfterRemoval = mother.numberOfChildren_get()
        val expectedNumberOfChildrenAfterRemoval = 0
        assertEquals(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
    }
}
