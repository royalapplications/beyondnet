package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class AnimalTests {
    @Test
    fun testDog() {
        val dogNameDN = Beyond_NET_Sample_Dog.dogName_get()
        val dogName = dogNameDN.toKString()

        val dog = Beyond_NET_Sample_AnimalFactory.createAnimal(dogNameDN)

        if (dog == null) {
            fail("AnimalFactory.CreateAnimal should not throw and return an instance")

            return
        }

        val retrievedDogName = dog.name_get().toKString()
        assertEquals(dogName, retrievedDogName)

        val food = "Bone"
        val foodDN = food.toDotNETString()

        val eat = dog.eat(foodDN).toKString()
        val expectedEat = "$dogName is eating $food."
        assertEquals(expectedEat, eat)
    }

    @Test
    fun testCat() {
        val catNameDN = Beyond_NET_Sample_Cat.catName_get()
        val catName = catNameDN.toKString()

        val cat = Beyond_NET_Sample_AnimalFactory.createAnimal(catNameDN)

        if (cat == null) {
            fail("AnimalFactory.CreateAnimal should not throw and return an instance")

            return
        }

        val retrievedCatName = cat.name_get().toKString()
        assertEquals(catName, retrievedCatName)

        val food = "Catnip"
        val foodDN = food.toDotNETString()

        val eat = cat.eat(foodDN).toKString()
        val expectedEat = "$catName is eating $food."
        assertEquals(expectedEat, eat)
    }

    @Test
    fun testStaticMemberShadowing() {
        val dogNameDN = Beyond_NET_Sample_Dog.staticName_get()
        val dogName = dogNameDN.toKString()

        val labradorNameDN = Beyond_NET_Sample_Labrador.staticName_get()
        val labradorName = labradorNameDN.toKString()

        assertEquals(dogName, "Dog")
        assertEquals(labradorName, "Labrador")

        assertNotEquals(dogName, labradorName)
    }
}