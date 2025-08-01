package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class PersonTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testPerson() {
        val johnDoe = Beyond_NET_Sample_Person.makeJohnDoe()

        assertEquals(johnDoe.fullName.toKString(), "John Doe")
        assertEquals(johnDoe.age, 50)
        assertEquals(johnDoe.niceLevel, Beyond_NET_Sample_NiceLevels.NICE)
        assertEquals(johnDoe.numberOfChildren, 0)

        johnDoe.increaseAge(2)

        assertEquals(johnDoe.age, 52)

        val address = Beyond_NET_Sample_Address("Homestreet".toDotNETString(), "Hometwon".toDotNETString())
        johnDoe.address_set(address)

        val addressRetRef = ObjectRef<Beyond_NET_Sample_Address?>(null)
        val getAddressSuccess = johnDoe.tryGetAddress(addressRetRef)

        assertTrue(getAddressSuccess)
        assertEquals(addressRetRef.value, address)

        val babyJohn = Beyond_NET_Sample_Person("John".toDotNETString(), "Doe Junior".toDotNETString(), 0)
        johnDoe.addChild(babyJohn)

        assertEquals(johnDoe.numberOfChildren, 1)

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

        assertEquals(johnDoe.website, websiteUri)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testPersonChildren() {
        val motherFirstNameDN = "Johanna".toDotNETString()
        val sonFirstNameDN = "Max".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val mother = Beyond_NET_Sample_Person(motherFirstNameDN, lastNameDN, 40)
        val son = Beyond_NET_Sample_Person(sonFirstNameDN, lastNameDN, 4)

        mother.addChild(son)

        val numberOfChildren = mother.numberOfChildren
        assertEquals(1, numberOfChildren)

        val firstChild = try {
            mother.childAt(0)
        } catch (_: Throwable) {
            fail("Person.childAt should not throw and should return an instance")
            return
        }

        val firstChildEqualsSon = firstChild == son
        assertTrue(firstChildEqualsSon)

        mother.removeChild(son)

        try {
            mother.removeChildAt(0)
            fail("Person.removeChildAt should throw because the sole child has already been removed")
        } catch (e: Exception) {
            val errorMessage = e.message ?: ""
            assertTrue(errorMessage.contains("Index was out of range"))
        }
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testPersonChildrenArray() {
        val motherFirstNameDN = "Johanna".toDotNETString()
        val sonFirstNameDN = "Max".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val mother = Beyond_NET_Sample_Person(motherFirstNameDN, lastNameDN, 40)
        val son = Beyond_NET_Sample_Person(sonFirstNameDN, lastNameDN, 4)

        mother.addChild(son)

        val numberOfChildren = mother.numberOfChildren
        assertEquals(1, numberOfChildren)

        val children = mother.children
        val childrenLength = children.length
        assertEquals(numberOfChildren, childrenLength)

        val firstChild = try {
            children.getValue(0)
        } catch (_: Exception) {
            fail("System.Array.getValue should not throw and should return an instance")
            return
        }

        val firstChildEqualsSon = firstChild == son
        assertTrue(firstChildEqualsSon)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testPersonExtensionMethods() {
        val initialAge = 0
        val firstNameDN = "Johanna".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val baby = Beyond_NET_Sample_Person(firstNameDN, lastNameDN, initialAge)

        val increaseAgeByYears = 4
        baby.increaseAge(increaseAgeByYears)

        val expectedAge = initialAge + increaseAgeByYears
        val age = baby.age
        assertEquals(expectedAge, age)

        val nilAddressRet = ObjectRef<Beyond_NET_Sample_Address?>(null)
        val nilAddressSuccess = baby.tryGetAddress(nilAddressRet)

        assertFalse(nilAddressSuccess)
        assertNull(nilAddressRet.value)

        val address = Beyond_NET_Sample_Address(
            "Street Name".toDotNETString(),
            "City Name".toDotNETString()
        )

        baby.address_set(address)

        val addressRet = ObjectRef<Beyond_NET_Sample_Address?>(null)
        val addressSuccess = baby.tryGetAddress(addressRet)

        assertTrue(addressSuccess)
        assertNotNull(addressRet.value)
    }

    // NOTE: This was transpiled from Swift
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

        val numberOfChildren = mother.numberOfChildren

        val expectedNumberOfChildren = 2

        assertEquals(expectedNumberOfChildren, numberOfChildren)
        assertEquals(expectedNumberOfChildren, numberOfTimesNumberOfChildrenChangedWasCalled)

        mother.removeChild(daughter)

        assertEquals(3, numberOfTimesNumberOfChildrenChangedWasCalled)

        mother.numberOfChildrenChanged_remove(numberOfChildrenChangedDelegate)

        mother.removeChildAt(0)
        assertEquals(3, numberOfTimesNumberOfChildrenChangedWasCalled)

        val numberOfChildrenAfterRemoval = mother.numberOfChildren
        val expectedNumberOfChildrenAfterRemoval = 0
        assertEquals(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
    }

    @Test
    fun testPersonAddress() {
        val firstNameDN = "Johanna".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val person = Beyond_NET_Sample_Person(firstNameDN, lastNameDN, 15)

        val street = "Stephansplatz"
        val streetDN = street.toDotNETString()

        val city = "Vienna"
        val cityDN = city.toDotNETString()

        val address = Beyond_NET_Sample_Address(streetDN, cityDN)
        person.address_set(address)

        val retrievedAddress = try {
            person.address
        } catch (_: Exception) {
            fail("Person.address getter should return an instance and not throw")
            return
        }

        val retrievedStreet = retrievedAddress?.street?.toKString()
        assertEquals(street, retrievedStreet)

        val retrievedCity = retrievedAddress?.city?.toKString()
        assertEquals(city, retrievedCity)
    }

    // NOTE: This was transpiled from Swift
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

        val ageAfterCreation = person.age
        assertEquals(initialAge, ageAfterCreation)

        val newAgeProviderDelegate = Beyond_NET_Sample_Person_NewAgeProviderDelegate {
            10
        }

        person.changeAge(newAgeProviderDelegate)

        val age = person.age
        assertEquals(10, age)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testPersonChildrenArrayChange() {
        val motherFirstNameDN = "Johanna".toDotNETString()
        val sonFirstNameDN = "Max".toDotNETString()
        val daughterFirstNameDN = "Marie".toDotNETString()
        val lastNameDN = "Doe".toDotNETString()

        val mother = Beyond_NET_Sample_Person(motherFirstNameDN, lastNameDN, 40)
        val son = Beyond_NET_Sample_Person(sonFirstNameDN, lastNameDN, 4)

        mother.addChild(son)

        val originalChildren = mother.children
        val numberOfChildrenBeforeDaughter = originalChildren.length
        assertEquals(1, numberOfChildrenBeforeDaughter)

        val newChildren = DNArray(Beyond_NET_Sample_Person, 2)

        System_Array.copy(originalChildren, newChildren, 1)

        val daughter = Beyond_NET_Sample_Person(daughterFirstNameDN, lastNameDN, 10)
        newChildren.setValue(daughter, 1)

        mother.children_set(newChildren)

        val numberOfChildrenAfterDaughter = mother.numberOfChildren
        assertEquals(2, numberOfChildrenAfterDaughter)
    }
}
