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
		guard let dogNameDN = NativeAOT_CodeGeneratorInputSample_Dog.dogName_get() else {
			XCTFail("Dog.DogName should return an instance")
			
			return
		}
		
		let dogName = dogNameDN.string()
		
		guard let dog = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(dogNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		guard let retrievedDogName = try? dog.name_get()?.string() else {
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
		guard let catNameDN = NativeAOT_CodeGeneratorInputSample_Cat.catName_get() else {
			XCTFail("Cat.CatName should return an instance of a string")
			
			return
		}
		
		let catName = catNameDN.string()
		
		guard let cat = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(catNameDN) else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		guard let retrievedCatName = try? cat.name_get()?.string() else {
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
	
	// TODO: Swiftify
//	func testCustomAnimalCreator() {
//		var exception: System_Exception_t?
//
//		guard let creatorDelegate = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate({ animalName in
//			guard let animalName else {
//				// Can't create an animal without a name
//
//				return nil
//			}
//
//			guard let animal = try? NativeAOT_CodeGeneratorInputSample_GenericAnimal(animalNameDN) else {
//				XCTFail("GenericAnimal ctor should not throw and return an instance")
//
//				return nil
//			}
//
//			return animal
//		}) else {
//			XCTFail("AnimalCreatorDelegate ctor should return an instance")
//
//			return
//		}
//
//
//		let animalName = "Horse"
//		let animalNameDN = animalName.dotNETString()
//
//		guard let horse = try? NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(animalNameDN,
//																							 creatorDelegate) else {
//			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
//
//			return
//		}
//
//		guard let retrievedAnimalName = try? horse.name_get()?.string() else {
//			XCTFail()
//
//			return
//		}
//
//		XCTAssertEqual(animalName, retrievedAnimalName)
//	}
	
	// TODO: Swiftify
//	func testGettingDefaultAnimalCreator() {
//		guard let defaultCreator = NativeAOT_CodeGeneratorInputSample_AnimalFactory.dEFAULT_CREATOR_get() else {
//			XCTFail("AnimalFactory.DEFAULT_CREATOR should not return nil")
//
//			return
//		}
//
//		let dogName = "Dog"
//		let dogNameDN = dogName.dotNETString()
//
//		guard let dog = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Invoke(defaultCreator,
//																						dogNameDN,
//																						&exception),
//			  exception == nil else {
//			XCTFail("Given the animal name \"Dog\", an instance of a dog should be returned")
//
//			return
//		}
//
//		defer { NativeAOT_CodeGeneratorInputSample_IAnimal_Destroy(dog) }
//
//		guard let dogNameRet = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(dog,
//																										&exception),
//									  destroyDotNETString: true),
//			  exception == nil else {
//			XCTFail("IAnimal.Name should not throw and return an instance")
//
//			return
//		}
//
//		XCTAssertEqual(dogName, dogNameRet)
//
//		let catName = "Cat"
//		let catNameDN = catName.cDotNETString()
//		defer { System_String_Destroy(catNameDN) }
//
//		guard let cat = NativeAOT_CodeGeneratorInputSample_AnimalFactory_CreateAnimal_1(catNameDN,
//																						defaultCreator,
//																						&exception),
//			  exception == nil else {
//			XCTFail("Given the animal name \"Cat\", an instance of a cat should be returned")
//
//			return
//		}
//
//		defer { NativeAOT_CodeGeneratorInputSample_IAnimal_Destroy(cat) }
//
//		guard let catNameRet = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(cat,
//																										&exception),
//									  destroyDotNETString: true),
//			  exception == nil else {
//			XCTFail("IAnimal.Name should not throw and return an instance")
//
//			return
//		}
//
//		XCTAssertEqual(catName, catNameRet)
//	}
	
	func testCreatingAnimalThroughGenerics() {
		// MARK: Cat
		let catType = NativeAOT_CodeGeneratorInputSample_Cat.typeOf()
		
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
		let dogType = NativeAOT_CodeGeneratorInputSample_Dog.typeOf()
		
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
		let stringType = System_String.typeOf()
		
		do {
			_ = try NativeAOT_CodeGeneratorInputSample_AnimalFactory.createAnimal(stringType)
			
			XCTFail("Should throw")
		} catch {
			XCTAssertTrue(error.localizedDescription.contains("violates the constraint"))
		}
	}
}
