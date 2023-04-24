import XCTest
import NativeAOTCodeGeneratorOutputSample

final class AnimalTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testDog() {
		guard let dogNameDN = NativeAOT_CodeGeneratorInputSample_Dog.dogName else {
			XCTFail("Dog.DogName should return an instance")
			
			return
		}
		
		let dogName = dogNameDN.string()
		
		guard let dog = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(dogNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		guard let retrievedDogName = try? dog.name?.string() else {
			XCTFail("Dog.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(dogName, retrievedDogName)
		
		let food = "Bone"
		let foodDN = food.dotNETString()
		
		guard let eat = try? dog.eat(foodDN)?.string() else {
			XCTFail("IAnimal.Eat should not throw and return an instance")
			
			return
		}
		
		let expectedEat = "\(dogName) is eating \(food)."
		
		XCTAssertEqual(expectedEat, eat)
	}
	
	func testCat() {
		guard let catNameDN = NativeAOT_CodeGeneratorInputSample_Cat.catName else {
			XCTFail("Cat.CatName should return an instance of a string")
			
			return
		}
		
		let catName = catNameDN.string()
		
		guard let cat = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(catNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		guard let retrievedCatName = try? cat.name?.string() else {
			XCTFail("Cat.Name getter should not throw and return an instance of a string")
			
			return
		}
		
		XCTAssertEqual(catName, retrievedCatName)
		
		let food = "Catnip"
		let foodDN = food.dotNETString()
		
		guard let eat = try? cat.eat(foodDN)?.string() else {
			XCTFail("IAnimal.Eat should not throw and return an instance of a string")
			
			return
		}
		
		let expectedEat = "\(catName) is eating \(food)."
		
		XCTAssertEqual(expectedEat, eat)
	}
	
	func testCustomAnimalCreator() {
		let creatorFunc: NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate.ClosureType = { innerAnimalName in
			guard let innerAnimalName else {
				// Can't create an animal without a name

				return nil
			}

			guard let animal = try? NativeAOT_CodeGeneratorInputSample_GenericAnimal(innerAnimalName),
				  let animalAsIAnimal = try? animal.castTo(NativeAOT_CodeGeneratorInputSample_IAnimal.self) else {
				XCTFail("GenericAnimal ctor should not throw and return an instance")

				return nil
			}
			
			return animalAsIAnimal
		}

		guard let creatorDelegate = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate(creatorFunc) else {
			XCTFail("AnimalCreatorDelegate ctor should return an instance")

			return
		}
		
		let animalName = "Horse"
		let animalNameDN = animalName.dotNETString()

		guard let horse = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(animalNameDN,
																							 creatorDelegate) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")

			return
		}

		guard let retrievedAnimalName = try? horse.name?.string() else {
			XCTFail()

			return
		}

		XCTAssertEqual(animalName, retrievedAnimalName)
	}
	
	func testGettingDefaultAnimalCreator() {
		guard let defaultCreator = NativeAOT_CodeGeneratorInputSample_AnimalFactory.dEFAULT_CREATOR else {
			XCTFail("AnimalFactory.DEFAULT_CREATOR should not return nil")

			return
		}

		let dogName = "Dog"
		let dogNameDN = dogName.dotNETString()

		guard let dog = try? defaultCreator.invoke(dogNameDN) else {
			XCTFail("Given the animal name \"Dog\", an instance of a dog should be returned")

			return
		}

		guard let dogNameRet = try? dog.name?.string() else {
			XCTFail("IAnimal.Name should not throw and return an instance")

			return
		}

		XCTAssertEqual(dogName, dogNameRet)

		let catName = "Cat"
		let catNameDN = catName.dotNETString()

		guard let cat = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(catNameDN,
																						   defaultCreator) else {
			XCTFail("Given the animal name \"Cat\", an instance of a cat should be returned")

			return
		}

		guard let catNameRet = try? cat.name?.string() else {
			XCTFail("IAnimal.Name should not throw and return an instance")

			return
		}

		XCTAssertEqual(catName, catNameRet)
	}
	
	func testCreatingAnimalThroughGenerics() {
		// MARK: Cat
		let catType = NativeAOT_CodeGeneratorInputSample_Cat.typeOf
		
		guard let cat = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(catType) else {
			XCTFail("CreateAnimal<Cat> should not throw and return an instance")
			
			return
		}
		
		guard let catTypeRet = try? cat.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		let catTypesAreEqual = catType == catTypeRet
		XCTAssertTrue(catTypesAreEqual)
		
		// MARK: Dog
		let dogType = NativeAOT_CodeGeneratorInputSample_Dog.typeOf
		
		guard let dog = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(dogType) else {
			XCTFail("CreateAnimal<Dog> should not throw and return an instance")
			
			return
		}
		
		guard let dogTypeRet = try? dog.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		let dogTypesAreEqual = dogType == dogTypeRet
		XCTAssertTrue(dogTypesAreEqual)
		
		// MARK: Invalid Type
		let stringType = System_String.typeOf
		
		do {
			_ = try NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(stringType)
			
			XCTFail("Should throw")
		} catch {
			XCTAssertTrue(error.localizedDescription.contains("violates the constraint"))
		}
	}
}
