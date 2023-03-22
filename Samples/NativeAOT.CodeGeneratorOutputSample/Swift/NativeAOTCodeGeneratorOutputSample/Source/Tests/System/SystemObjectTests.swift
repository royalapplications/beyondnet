import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemObjectTests: XCTestCase {
    func testSystemObject() {
        var exception: System_Exception_t?
        
        let systemObjectTypeName = "System.Object"
        
        guard let systemObjectType = System_Type_GetType1(systemObjectTypeName,
                                                          .yes,
                                                          &exception) else {
            XCTFail("System.Type.GetType should not throw and return an instance")
            
            return
        }
        
        guard let object1 = System_Object_Create(&exception),
              exception == nil else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        guard let object1Type = System_Object_GetType(object1,
                                                      &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard System_Object_Equals(systemObjectType,
                                   object1Type,
                                   &exception) == .yes,
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
    }
}
