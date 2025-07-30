package com.example.beyondnetsampleandroid

import androidx.test.ext.junit.runners.AndroidJUnit4

import org.junit.Test
import org.junit.runner.RunWith

import org.junit.Assert.*

import com.example.beyondnetsampleandroid.dn.*

@RunWith(AndroidJUnit4::class)
class AnimalTests {
    // NOTE: This was copied from the Swift tests
    @Test
    fun testDog() {
        val dogNameDN = Beyond_NET_Sample_Dog.dogName
        val dogName = dogNameDN.toKString()

        val dog = Beyond_NET_Sample_AnimalFactory.createAnimal(dogNameDN)

        if (dog == null) {
            fail("AnimalFactory.CreateAnimal should not throw and return an instance")

            return
        }

        val retrievedDogName = dog.name.toKString()
        assertEquals(dogName, retrievedDogName)

        val food = "Bone"
        val foodDN = food.toDotNETString()

        val eat = dog.eat(foodDN).toKString()
        val expectedEat = "$dogName is eating $food."
        assertEquals(expectedEat, eat)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testCat() {
        val catNameDN = Beyond_NET_Sample_Cat.catName
        val catName = catNameDN.toKString()

        val cat = Beyond_NET_Sample_AnimalFactory.createAnimal(catNameDN)

        if (cat == null) {
            fail("AnimalFactory.CreateAnimal should not throw and return an instance")

            return
        }

        val retrievedCatName = cat.name.toKString()
        assertEquals(catName, retrievedCatName)

        val food = "Catnip"
        val foodDN = food.toDotNETString()

        val eat = cat.eat(foodDN).toKString()
        val expectedEat = "$catName is eating $food."
        assertEquals(expectedEat, eat)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testCustomAnimalCreator() {
        val creatorFunc: (animalName: System_String) -> Beyond_NET_Sample_IAnimal? = { innerAnimalName ->
            Beyond_NET_Sample_GenericAnimal(innerAnimalName)
        }

        val creatorDelegate = Beyond_NET_Sample_AnimalCreatorDelegate(creatorFunc)

        val animalName = "Horse"
        val animalNameDN = animalName.toDotNETString()

        val horse = Beyond_NET_Sample_AnimalFactory.createAnimal(
            animalNameDN,
            creatorDelegate
        )

        assertNotNull(horse)

        val retrievedAnimalName = horse?.name?.toKString()
        assertEquals(animalName, retrievedAnimalName)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testGettingDefaultAnimalCreator() {
        val defaultCreator = Beyond_NET_Sample_AnimalFactory.dEFAULT_CREATOR

        val dogName = "Dog"
        val dogNameDN = dogName.toDotNETString()

        val dog = defaultCreator.invoke(dogNameDN)
        assertNotNull(dog)

        val dogNameRet = dog?.name?.toKString()
        assertEquals(dogName, dogNameRet)

        val catName = "Cat"
        val catNameDN = catName.toDotNETString()

        val cat = Beyond_NET_Sample_AnimalFactory.createAnimal(
            catNameDN,
            defaultCreator
        )

        assertNotNull(cat)

        val catNameRet = cat?.name?.toKString()
        assertEquals(catName, catNameRet)
    }

    // NOTE: This was copied from the Swift tests
    @Test
    fun testStaticMemberShadowing() {
        val dogNameDN = Beyond_NET_Sample_Dog.staticName
        val dogName = dogNameDN.toKString()

        val labradorNameDN = Beyond_NET_Sample_Labrador.staticName
        val labradorName = labradorNameDN.toKString()

        assertEquals(dogName, "Dog")
        assertEquals(labradorName, "Labrador")

        assertNotEquals(dogName, labradorName)
    }
}
