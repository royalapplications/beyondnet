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
        
        // TODO: This should be replaced by a call to IAnimal.Name once interfaces are properly supported
        guard let retrievedDogNameC = NativeAOT_CodeGeneratorInputSample_Dog_Name_Get(dog,
                                                                                      &exception),
              exception == nil else {
            XCTFail("Dog.Name getter should not throw and return an instance of a string")
            
            return
        }
        
        let retrievedDogName = String(cString: retrievedDogNameC)
        
        XCTAssertEqual(dogName, retrievedDogName)
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
        
        // TODO: This should be replaced by a call to IAnimal.Name once interfaces are properly supported
        guard let retrievedCatNameC = NativeAOT_CodeGeneratorInputSample_Cat_Name_Get(cat,
                                                                                      &exception),
              exception == nil else {
            XCTFail("Cat.Name getter should not throw and return an instance of a string")
            
            return
        }
        
        let retrievedCatName = String(cString: retrievedCatNameC)
        
        XCTAssertEqual(catName, retrievedCatName)
    }
}
