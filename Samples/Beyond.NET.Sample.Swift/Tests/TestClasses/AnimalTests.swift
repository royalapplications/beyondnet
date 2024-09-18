import XCTest
import BeyondDotNETSampleKit

final class AnimalTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testDog() throws {
		let dogNameDN = Beyond_NET_Sample_Dog.dogName
		let dogName = dogNameDN.string()
		
		guard let dog = try Beyond_NET_Sample_AnimalFactory.createAnimal(dogNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		let retrievedDogName = try dog.name.string()
		XCTAssertEqual(dogName, retrievedDogName)
		
		let food = "Bone"
		let foodDN = food.dotNETString()
		
		let eat = try dog.eat(foodDN).string()
		let expectedEat = "\(dogName) is eating \(food)."
		XCTAssertEqual(expectedEat, eat)
	}
	
	func testCat() throws {
		let catNameDN = Beyond_NET_Sample_Cat.catName
		let catName = catNameDN.string()
		
		guard let cat = try Beyond_NET_Sample_AnimalFactory.createAnimal(catNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
        
        let retrievedCatName = try cat.name.string()
		XCTAssertEqual(catName, retrievedCatName)
		
		let food = "Catnip"
		let foodDN = food.dotNETString()
		
		let eat = try cat.eat(foodDN).string()
		let expectedEat = "\(catName) is eating \(food)."
		XCTAssertEqual(expectedEat, eat)
	}
	
	func testCustomAnimalCreator() throws {
		let creatorFunc: Beyond_NET_Sample_AnimalCreatorDelegate.ClosureType = { innerAnimalName in
			guard let animal = try? Beyond_NET_Sample_GenericAnimal(innerAnimalName) else {
				XCTFail("GenericAnimal ctor should not throw and return an instance")

				return nil
			}
			
			return animal
		}

		let creatorDelegate = Beyond_NET_Sample_AnimalCreatorDelegate(creatorFunc)
		
		let animalName = "Horse"
        let animalNameDN = animalName.dotNETString()
        
        guard let horse = try Beyond_NET_Sample_AnimalFactory.createAnimal(animalNameDN,
                                                                           creatorDelegate) else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        let retrievedAnimalName = try horse.name.string()
		XCTAssertEqual(animalName, retrievedAnimalName)
	}
	
	func testGettingDefaultAnimalCreator() throws {
		let defaultCreator = Beyond_NET_Sample_AnimalFactory.dEFAULT_CREATOR

		let dogName = "Dog"
		let dogNameDN = dogName.dotNETString()

		guard let dog = try defaultCreator.invoke(dogNameDN) else {
			XCTFail("Given the animal name \"Dog\", an instance of a dog should be returned")

			return
		}

		let dogNameRet = try dog.name.string()
		XCTAssertEqual(dogName, dogNameRet)

		let catName = "Cat"
		let catNameDN = catName.dotNETString()

        guard let cat = try Beyond_NET_Sample_AnimalFactory.createAnimal(catNameDN,
                                                                         defaultCreator) else {
			XCTFail("Given the animal name \"Cat\", an instance of a cat should be returned")

			return
		}

		let catNameRet = try cat.name.string()
		XCTAssertEqual(catName, catNameRet)
	}
	
	func testCreatingAnimalThroughGenerics() throws {
		// MARK: Cat
		let catType = Beyond_NET_Sample_Cat.typeOf
		
        let cat = try Beyond_NET_Sample_AnimalFactory.createAnimal(T: catType)
		let catTypeRet = try cat.getType()
		let catTypesAreEqual = catType == catTypeRet
		XCTAssertTrue(catTypesAreEqual)
		
		// MARK: Dog
		let dogType = Beyond_NET_Sample_Dog.typeOf
		
        let dog = try Beyond_NET_Sample_AnimalFactory.createAnimal(T: dogType)
		let dogTypeRet = try dog.getType()
		let dogTypesAreEqual = dogType == dogTypeRet
		XCTAssertTrue(dogTypesAreEqual)
		
		// MARK: Invalid Type
		let stringType = System_String.typeOf
		
		do {
            _ = try Beyond_NET_Sample_AnimalFactory.createAnimal(T: stringType)
			
			XCTFail("Should throw")
		} catch {
			XCTAssertTrue(error.localizedDescription.contains("violates the constraint"))
		}
	}
    
    func testStaticMemberShadowing() {
        let dogNameDN = Beyond_NET_Sample_Dog.staticName
        let dogName = dogNameDN.string()

        let labradorNameDN = Beyond_NET_Sample_Labrador.staticName
        let labradorName = labradorNameDN.string()

        XCTAssertEqual(dogName, "Dog")
        XCTAssertEqual(labradorName, "Labrador")
        
        XCTAssertNotEqual(dogName, labradorName)
    }
}
