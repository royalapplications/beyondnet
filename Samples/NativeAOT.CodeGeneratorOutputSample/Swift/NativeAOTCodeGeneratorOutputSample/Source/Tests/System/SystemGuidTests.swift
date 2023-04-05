import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemGuidTests: XCTestCase {
    func testSystemGuid() {
        var exception: System_Exception_t?
        
        let uuid = UUID()
        let uuidString = uuid.uuidString
        
        var guid: System_Guid_t?
        
        uuidString.withCString { uuidStringC in
			guid = System_Guid_Create_4(uuidStringC,
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
			guidType = System_Type_GetType_2(guidTypeNameC,
											 &exception)
        }
        
        guard let guidType,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        defer { System_Type_Destroy(guidType) }
        
        let guidTypeFromInstance = System_Object_GetType(guid,
                                                         &exception)
        
        guard let guidTypeFromInstance,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        defer { System_Type_Destroy(guidTypeFromInstance) }
        
        let equals = System_Object_Equals(guidType,
                                          guidTypeFromInstance,
                                          &exception)
        
        guard exception == nil else {
            XCTFail("Equals should not throw")
            
            return
        }
        
        XCTAssertTrue(equals)
        
        guard let emptyGuid = System_Guid_Empty_Get() else {
            XCTFail("System.Guid.Empty getter should return an instance")
            
            return
        }
        
        defer { System_Guid_Destroy(emptyGuid) }
        
        guard let emptyGuidStringC = System_Guid_ToString(emptyGuid,
                                                          &exception),
              exception == nil else {
            XCTFail("System.Guid.ToString should not throw and return a string")
            
            return
        }
        
        defer { emptyGuidStringC.deallocate() }
        
        let emptyGuidString = String(cString: emptyGuidStringC)
        
        XCTAssertEqual("00000000-0000-0000-0000-000000000000", emptyGuidString)
    }
	
	func testSystemGuidParsing() {
		var exception: System_Exception_t?
		
		let uuid = UUID()
		let uuidString = uuid.uuidString
		
		var success = false
		var guid: System_Guid_t?
		
		uuidString.withCString { uuidStringC in
			success = System_Guid_TryParse(uuidStringC,
										   &guid,
										   &exception)
		}
		
		guard let guid,
			  exception == nil,
			  success else {
			XCTFail("System.Guid.TryParse should not throw and return an instance as out parameter")
			
			return
		}
		
		defer { System_Guid_Destroy(guid) }
		
		guard let uuidStringRetC = System_Guid_ToString(guid,
														&exception),
			  exception == nil else {
			XCTFail("System.Guid.ToString should not throw and return an instance of a C String")
			
			return
		}
		
		defer { uuidStringRetC.deallocate() }
		
		let uuidStringRet = String(cString: uuidStringRetC)
		
		XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
	}
	
	func testInvalidSystemGuidParsing() {
		guard let emptyGuid = System_Guid_Empty_Get() else {
			XCTFail("Failed to get System.Guid.Empty")
			
			return
		}
		
		var exception: System_Exception_t?
		
		let uuidString = "nonsense"
		
		var success = false
		var guid: System_Guid_t?
		
		uuidString.withCString { uuidStringC in
			success = System_Guid_TryParse(uuidStringC,
										   &guid,
										   &exception)
		}
		
		guard let guid,
			  !success,
			  exception == nil else {
			XCTFail("System.Guid.TryParse should not throw, the return value should be false and the returned instance as out parameter should be an empty System.Guid")
			
			return
		}
		
		defer { System_Guid_Destroy(guid) }
		
		let equal = System_Object_Equals(emptyGuid,
										 guid,
										 &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(equal)
	}
}
