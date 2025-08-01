package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class AddressTests {
    // NOTE: This was transpiled from Swift
    @Test
    fun testAddress() {
        val street = "Schwedenplatz"
        val streetDN = street.toDotNETString()

        val city = "Vienna"
        val cityDN = city.toDotNETString()

        val address = Beyond_NET_Sample_Address(
            streetDN,
            cityDN
        )

        val retrievedStreet = address.street.toKString()
        assertEquals(street, retrievedStreet)

        val addressType = address.getType()
        val expectedAddressTypeFullName = "Beyond.NET.Sample.Address"
        val actualAddressFullTypeName = addressType.fullName?.toKString()
        assertEquals(expectedAddressTypeFullName, actualAddressFullTypeName)
    }

    // NOTE: This was transpiled from Swift
    @Test
    fun testAddressMover() {
        val originalStreet = "Schwedenplatz"
        val originalStreetDN = originalStreet.toDotNETString()

        val newStreet = "Stephansplatz"
        val newStreetDN = newStreet.toDotNETString()

        val originalCity = "Vienna"
        val originalCityDN = originalCity.toDotNETString()

        val newCity = "Wien"
        val newCityDN = newCity.toDotNETString()

        val originalAddress = Beyond_NET_Sample_Address(
            originalStreetDN,
            originalCityDN
        )

        val moverFunc: (newStreet: System_String, newCity: System_String) -> Beyond_NET_Sample_Address? = { newStreetInnerDN, newCityInnerDN ->
            val newAddress = Beyond_NET_Sample_Address(
                newStreetInnerDN,
                newCityInnerDN
            )

            newAddress
        }

        val newAddress = originalAddress.move(Beyond_NET_Sample_MoveDelegate(moverFunc),
            newStreetDN,
            newCityDN
        )

        val retrievedNewStreet = newAddress.street.toKString()
        assertEquals(newStreet, retrievedNewStreet)

        val retrievedNewCity = newAddress.city.toKString()
        assertEquals(newCity, retrievedNewCity)
    }
}
