import XCTest
import BeyondNETSamplesSwift

final class AnimalTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testDog() {
        var exception: System_Exception_t?
        
        guard let dogNameDN = Beyond_NET_Sample_Dog_DogName_Get() else {
            XCTFail("Dog.DogName should return an instance")
            
            return
        }
        
        guard let dogName = String(cDotNETString: dogNameDN) else {
            XCTFail("Failed to convert string")
            
            return
        }
        
        defer { System_String_Destroy(dogNameDN) }
        
        guard let dog = Beyond_NET_Sample_AnimalFactory_CreateAnimal(dogNameDN,
                                                                     &exception),
              exception == nil else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        guard let retrievedDogName = String(cDotNETString: Beyond_NET_Sample_IAnimal_Name_Get(dog,
                                                                                              &exception),
                                            destroyDotNETString: true),
              exception == nil else {
            XCTFail("Dog.Name getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(dogName, retrievedDogName)
        
        let food = "Bone"
        let foodDN = food.cDotNETString()
        defer { System_String_Destroy(foodDN) }
        
        guard let eat = String(cDotNETString: Beyond_NET_Sample_IAnimal_Eat(dog,
                                                                            foodDN,
                                                                            &exception),
                               destroyDotNETString: true),
              exception == nil else {
            XCTFail("IAnimal.Eat should not throw and return an instance")
            
            return
        }
        
        let expectedEat = "\(dogName) is eating \(food)."
        
        XCTAssertEqual(expectedEat, eat)
    }
    
    func testCat() {
        var exception: System_Exception_t?
        
        guard let catNameDN = Beyond_NET_Sample_Cat_CatName_Get() else {
            XCTFail("Cat.CatName should return an instance of a string")
            
            return
        }
        
        guard let catName = String(cDotNETString: catNameDN) else {
            XCTFail("Failed to convert string")
            
            return
        }
        
        defer { System_String_Destroy(catNameDN) }
        
        guard let cat = Beyond_NET_Sample_AnimalFactory_CreateAnimal(catNameDN,
                                                                     &exception),
              exception == nil else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        guard let retrievedCatName = String(cDotNETString: Beyond_NET_Sample_IAnimal_Name_Get(cat,
                                                                                              &exception),
                                            destroyDotNETString: true),
              exception == nil else {
            XCTFail("Cat.Name getter should not throw and return an instance of a string")
            
            return
        }
        
        XCTAssertEqual(catName, retrievedCatName)
        
        let food = "Catnip"
        let foodDN = food.cDotNETString()
        defer { System_String_Destroy(foodDN) }
        
        guard let eat = String(cDotNETString: Beyond_NET_Sample_IAnimal_Eat(cat,
                                                                            foodDN,
                                                                            &exception),
                               destroyDotNETString: true),
              exception == nil else {
            XCTFail("IAnimal.Eat should not throw and return an instance of a string")
            
            return
        }
        
        let expectedEat = "\(catName) is eating \(food)."
        
        XCTAssertEqual(expectedEat, eat)
    }
    
    func testCustomAnimalCreator() {
        var exception: System_Exception_t?
        
        let creator: Beyond_NET_Sample_AnimalCreatorDelegate_CFunction_t = { _, animalNameDN in
            guard let animalNameDN else {
                // Can't create an animal without a name
                
                return nil
            }
            
            // We need to release any reference types that are given to us through the delegate
            defer { System_String_Destroy(animalNameDN) }
            
            var innerException: System_Exception_t?
            
            guard let animal = Beyond_NET_Sample_GenericAnimal_Create(animalNameDN,
                                                                      &innerException),
                  innerException == nil else {
                XCTFail("GenericAnimal ctor should not throw and return an instance")
                
                return nil
            }
            
            return animal
        }
        
        guard let creatorDelegate = Beyond_NET_Sample_AnimalCreatorDelegate_Create(nil,
                                                                                   creator,
                                                                                   nil) else {
            XCTFail("AnimalCreatorDelegate ctor should return an instance")
            
            return
        }
        
        defer { Beyond_NET_Sample_AnimalCreatorDelegate_Destroy(creatorDelegate) }
        
        let animalName = "Horse"
        let animalNameDN = animalName.cDotNETString()
        defer { System_String_Destroy(animalNameDN) }
        
        guard let horse = Beyond_NET_Sample_AnimalFactory_CreateAnimal_1(animalNameDN,
                                                                         creatorDelegate,
                                                                         &exception),
              exception == nil else {
            XCTFail("AnimalFactory.CreateAnimal should not throw and return an instance")
            
            return
        }
        
        defer { Beyond_NET_Sample_IAnimal_Destroy(horse) }
        
        guard let retrievedAnimalName = String(cDotNETString: Beyond_NET_Sample_IAnimal_Name_Get(horse,
                                                                                                 &exception),
                                               destroyDotNETString: true),
              exception == nil else {
            XCTFail()
            
            return
        }
        
        XCTAssertEqual(animalName, retrievedAnimalName)
    }
    
    func testGettingDefaultAnimalCreator() {
        var exception: System_Exception_t?
        
        guard let defaultCreator = Beyond_NET_Sample_AnimalFactory_DEFAULT_CREATOR_Get() else {
            XCTFail("AnimalFactory.DEFAULT_CREATOR should not return nil")
            
            return
        }
        
        defer { Beyond_NET_Sample_AnimalCreatorDelegate_Destroy(defaultCreator) }
        
        let dogName = "Dog"
        let dogNameDN = dogName.cDotNETString()
        defer { System_String_Destroy(dogNameDN) }
        
        guard let dog = Beyond_NET_Sample_AnimalCreatorDelegate_Invoke(defaultCreator,
                                                                       dogNameDN,
                                                                       &exception),
              exception == nil else {
            XCTFail("Given the animal name \"Dog\", an instance of a dog should be returned")
            
            return
        }
        
        defer { Beyond_NET_Sample_IAnimal_Destroy(dog) }
        
        guard let dogNameRet = String(cDotNETString: Beyond_NET_Sample_IAnimal_Name_Get(dog,
                                                                                        &exception),
                                      destroyDotNETString: true),
              exception == nil else {
            XCTFail("IAnimal.Name should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(dogName, dogNameRet)
        
        let catName = "Cat"
        let catNameDN = catName.cDotNETString()
        defer { System_String_Destroy(catNameDN) }
        
        guard let cat = Beyond_NET_Sample_AnimalFactory_CreateAnimal_1(catNameDN,
                                                                       defaultCreator,
                                                                       &exception),
              exception == nil else {
            XCTFail("Given the animal name \"Cat\", an instance of a cat should be returned")
            
            return
        }
        
        defer { Beyond_NET_Sample_IAnimal_Destroy(cat) }
        
        guard let catNameRet = String(cDotNETString: Beyond_NET_Sample_IAnimal_Name_Get(cat,
                                                                                        &exception),
                                      destroyDotNETString: true),
              exception == nil else {
            XCTFail("IAnimal.Name should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(catName, catNameRet)
    }
    
    func testCreatingAnimalThroughGenerics() {
        var exception: System_Exception_t?
        
        // MARK: Cat
        let catType = Beyond_NET_Sample_Cat_TypeOf()
        defer { System_Type_Destroy(catType) }
        
        guard let cat = Beyond_NET_Sample_AnimalFactory_CreateAnimal_A1(catType,
                                                                        &exception),
              exception == nil else {
            XCTFail("CreateAnimal<Cat> should not throw and return an instance")
            
            return
        }
        
        defer { Beyond_NET_Sample_IAnimal_Destroy(cat) }
        
        guard let catTypeRet = System_Object_GetType(cat,
                                                     &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(catTypeRet) }
        
        let catTypesAreEqual = System_Type_Equals(catType,
                                                  catTypeRet,
                                                  &exception)
        
        XCTAssertNil(exception)
        XCTAssertTrue(catTypesAreEqual)
        
        // MARK: Dog
        let dogType = Beyond_NET_Sample_Dog_TypeOf()
        defer { System_Type_Destroy(dogType) }
        
        guard let dog = Beyond_NET_Sample_AnimalFactory_CreateAnimal_A1(dogType,
                                                                        &exception),
              exception == nil else {
            XCTFail("CreateAnimal<Dog> should not throw and return an instance")
            
            return
        }
        
        defer { Beyond_NET_Sample_IAnimal_Destroy(dog) }
        
        guard let dogTypeRet = System_Object_GetType(dog,
                                                     &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(dogTypeRet) }
        
        let dogTypesAreEqual = System_Type_Equals(dogType,
                                                  dogTypeRet,
                                                  &exception)
        
        XCTAssertNil(exception)
        XCTAssertTrue(dogTypesAreEqual)
        
        // MARK: Invalid Type
        let stringType = System_String_TypeOf()
        defer { System_Type_Destroy(stringType) }
        
        let invalidAnimal = Beyond_NET_Sample_AnimalFactory_CreateAnimal_A1(stringType,
                                                                            &exception)
        
        XCTAssertNil(invalidAnimal)
        
        guard let exception else {
            XCTFail("CreateAnimal with an invalid type should throw an exception")
            
            return
        }
        
        defer { System_Exception_Destroy(exception) }
        
        var exception2: System_Exception_t?
        
        guard let exceptionMessage = String(cDotNETString: System_Exception_Message_Get(exception,
                                                                                        &exception2),
                                            destroyDotNETString: true),
              exception2 == nil else {
            XCTFail("System.Exception.Message getter should not throw and return an instance of a string")
            
            return
        }
        
        XCTAssertTrue(exceptionMessage.contains("violates the constraint"))
    }
}
