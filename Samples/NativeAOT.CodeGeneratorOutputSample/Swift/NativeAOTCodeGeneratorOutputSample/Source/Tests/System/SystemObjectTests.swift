import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemObjectTests: XCTestCase {
    func testSystemObject() {
        var exception: System_Exception_t?
        
        let systemObjectTypeName = "System.Object"
        
        guard let systemObjectType = System_Object_TypeOf() else {
            XCTFail("typeof(System.Object should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemObjectType) }
        
        guard let object1 = System_Object_Create(&exception),
              exception == nil else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(object1) }
        
        guard let object1Type = System_Object_GetType(object1,
                                                      &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(object1Type) }
        
        guard System_Object_Equals(systemObjectType,
                                   object1Type,
                                   &exception) == .yes,
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        guard let object2 = System_Object_Create(&exception),
              exception == nil else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(object2) }
        
        guard System_Object_Equals(object1,
                                   object2,
                                   &exception) == .no,
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return false")
            
            return
        }
        
        guard System_Object_ReferenceEquals(object1,
                                            object2,
                                            &exception) == .no,
              exception == nil else {
            XCTFail("System.Object.ReferenceEquals should not throw and return false")
            
            return
        }
    }
}
