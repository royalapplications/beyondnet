import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemGuidTests: XCTestCase {
    func testSystemGuid() {
        var exception: System_Exception_t?
        
        let uuid = UUID()
        let uuidString = uuid.uuidString
        
        var guid: System_Guid_t?
        
        uuidString.withCString { uuidStringC in
            guid = System_Guid_Create2(uuidStringC,
                                       &exception)
        }
        
        guard exception == nil else {
            XCTFail("System_Guid_Create2 should not throw")
            
            return
        }
        
        guard let guid else {
            XCTFail("System_Guid_Create2 should return an instance of a Guid")
            
            return
        }
        
        defer {
            System_Object_Destroy(guid)
        }
        
        let guidCString = System_Guid_ToString(guid,
                                               &exception)
        
        guard exception == nil else {
            XCTFail("System_Guid_ToString should not throw")
            
            return
        }
        
        guard let guidCString else {
            XCTFail("System_Guid_ToString should return an instance of a String")
            
            return
        }
        
        let guidString = String(cString: guidCString)
        
        guidCString.deallocate()
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        let guidTypeName = "System.Guid"
        
        var guidType: System_Type_t?
        
        guidTypeName.withCString { guidTypeNameC in
            guidType = System_Type_GetType2(guidTypeNameC,
                                            &exception)
        }
        
        guard let guidType,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        let guidTypeFromInstance = System_Object_GetType(guid,
                                                         &exception)
        
        guard let guidTypeFromInstance,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        let equals = System_Object_Equals(guidType,
                                          guidTypeFromInstance,
                                          &exception)
        
        guard exception == nil else {
            XCTFail("Equals should not throw")
            
            return
        }
        
        XCTAssertEqual(equals, .yes)
        
        guard let emptyGuid = System_Guid_Empty_Get() else {
            XCTFail("System.Guid.Empty getter should return an instance")
            
            return
        }
        
        guard let emptyGuidStringC = System_Guid_ToString(emptyGuid,
                                                          &exception),
              exception == nil else {
            XCTFail("System.Guid.ToString should not throw and return a string")
            
            return
        }
        
        let emptyGuidString = String(cString: emptyGuidStringC)
        
        emptyGuidStringC.deallocate()
        
        XCTAssertEqual("00000000-0000-0000-0000-000000000000", emptyGuidString)
    }
}
