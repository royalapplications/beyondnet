import XCTest
import NativeAOTCodeGeneratorOutputSample

final class AnimalTests: XCTestCase {
    func testDog() {
        var exception: System_Exception_t?
        
        guard let dogNameC = NativeAOT_CodeGeneratorInputSample_Dog_DogName_Get() else {
            XCTFail("Dog.DogName should return an instance of a string")
            
            return
        }
        
        let dogName = String(cString: dogNameC)
        
        defer { dogNameC.deallocate() }
        
        guard let dog = NativeAOT_CodeGeneratorInputSample_AnimalFactory_CreateAnimal(dogNameC,
                                                                                      &exception),
              exception == nil else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        guard let retrievedDogNameC = NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(dog,
                                                                                          &exception),
              exception == nil else {
            XCTFail("Dog.Name getter should not throw and return an instance of a string")
            
            return
        }
        
        let retrievedDogName = String(cString: retrievedDogNameC)
        
        XCTAssertEqual(dogName, retrievedDogName)
        
        let food = "Bone"
        
        var eatC: CString?
        
        food.withCString { foodC in
            eatC = NativeAOT_CodeGeneratorInputSample_IAnimal_Eat(dog,
                                                                  foodC,
                                                                  &exception)
        }
        
        guard let eatC,
              exception == nil else {
            XCTFail("IAnimal.Eat should not throw and return an instance of a string")
            
            return
        }
        
        defer { eatC.deallocate() }
        
        let eat = String(cString: eatC)
        
        let expectedEat = "\(dogName) is eating \(food)."
        
        XCTAssertEqual(expectedEat, eat)
    }
    
    func testCat() {
        var exception: System_Exception_t?
        
        guard let catNameC = NativeAOT_CodeGeneratorInputSample_Cat_CatName_Get() else {
            XCTFail("Cat.CatName should return an instance of a string")
            
            return
        }
        
        let catName = String(cString: catNameC)
        
        defer { catNameC.deallocate() }
        
        guard let cat = NativeAOT_CodeGeneratorInputSample_AnimalFactory_CreateAnimal(catNameC,
                                                                                      &exception),
              exception == nil else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        guard let retrievedCatNameC = NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(cat,
                                                                                          &exception),
              exception == nil else {
            XCTFail("Cat.Name getter should not throw and return an instance of a string")
            
            return
        }
        
        let retrievedCatName = String(cString: retrievedCatNameC)
        
        XCTAssertEqual(catName, retrievedCatName)
        
        let food = "Catnip"
        
        var eatC: CString?
        
        food.withCString { foodC in
            eatC = NativeAOT_CodeGeneratorInputSample_IAnimal_Eat(cat,
                                                                  foodC,
                                                                  &exception)
        }
        
        guard let eatC,
              exception == nil else {
            XCTFail("IAnimal.Eat should not throw and return an instance of a string")
            
            return
        }
        
        defer { eatC.deallocate() }
        
        let eat = String(cString: eatC)
        
        let expectedEat = "\(catName) is eating \(food)."
        
        XCTAssertEqual(expectedEat, eat)
    }
	
	func testCustomAnimalCreator() {
		var exception: System_Exception_t?
		
		let creator: NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_CFunction_t = { _, animalNameC in
			guard let animalNameC else {
				// Can't create an animal without a name
				
				return nil
			}
			
			// We need to release any reference types that are given to us through the delegate
			defer { animalNameC.deallocate() }
			
			var innerException: System_Exception_t?
			
			guard let animal = NativeAOT_CodeGeneratorInputSample_GenericAnimal_Create(animalNameC,
																					   &innerException),
				  innerException == nil else {
				XCTFail("GenericAnimal ctor should not throw and return an instance")
				
				return nil
			}
			
			return animal
		}
		
		guard let creatorDelegate = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Create(nil,
																									creator,
																									nil) else {
			XCTFail("AnimalCreatorDelegate ctor should return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Destroy(creatorDelegate) }
		
		let animalName = "Horse"
		
		var horse: NativeAOT_CodeGeneratorInputSample_IAnimal_t?
		
		animalName.withCString { animalNameC in
			horse = NativeAOT_CodeGeneratorInputSample_AnimalFactory_CreateAnimal1(animalNameC,
																				   creatorDelegate,
																				   &exception)
		}
		
		guard let horse,
			  exception == nil else {
			XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_IAnimal_Destroy(horse) }
		
		guard let retrievedAnimalNameC = NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(horse,
																							 &exception),
			  exception == nil else {
			XCTFail()
			
			return
		}
		
		defer { retrievedAnimalNameC.deallocate() }
		
		let retrievedAnimalName = String(cString: retrievedAnimalNameC)
		
		XCTAssertEqual(animalName, retrievedAnimalName)
	}
    
    func testGettingDefaultAnimalCreator() {
        var exception: System_Exception_t?
        
        guard let defaultCreator = NativeAOT_CodeGeneratorInputSample_AnimalFactory_DEFAULT_CREATOR_Get() else {
            XCTFail("AnimalFactory.DEFAULT_CREATOR should not return nil")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Destroy(defaultCreator) }
        
        let dogName = "Dog"
        var dog: NativeAOT_CodeGeneratorInputSample_IAnimal_t?
        
        dogName.withCString { dogNameC in
            dog = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Invoke(defaultCreator,
                                                                                  dogNameC)
        }
        
        guard let dog else {
            XCTFail("Given the animal name \"Dog\", an instance of a dog should be returned")
            
            return
        }
        
        guard let dogNameRetC = NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(dog,
                                                                                    &exception),
              exception == nil else {
            XCTFail("IAnimal.Name should not throw and return an instance of a C string")
            
            return
        }
        
        let dogNameRet = String(cString: dogNameRetC)
        
        XCTAssertEqual(dogName, dogNameRet)
        
        let catName = "Cat"
        var cat: NativeAOT_CodeGeneratorInputSample_IAnimal_t?
        
        catName.withCString { catNameC in
            cat = NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Invoke(defaultCreator,
                                                                                  catNameC)
        }
        
        guard let cat else {
            XCTFail("Given the animal name \"Cat\", an instance of a cat should be returned")
            
            return
        }
        
        guard let catNameRetC = NativeAOT_CodeGeneratorInputSample_IAnimal_Name_Get(cat,
                                                                                    &exception),
              exception == nil else {
            XCTFail("IAnimal.Name should not throw and return an instance of a C string")
            
            return
        }
        
        let catNameRet = String(cString: catNameRetC)
        
        XCTAssertEqual(catName, catNameRet)
    }
}
