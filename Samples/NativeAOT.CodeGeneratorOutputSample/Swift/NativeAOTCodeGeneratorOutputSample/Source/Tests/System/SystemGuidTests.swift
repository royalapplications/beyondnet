import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemGuidTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
    func testSystemGuid() {
        var exception: System_Exception_t?
        
        let uuid = UUID()
        
		let uuidString = uuid.uuidString
		let uuidStringDN = uuidString.dotNETString()
		defer { System_String_Destroy(uuidStringDN) }
        
        guard let guid = System_Guid_Create_4(uuidStringDN,
											  &exception),
			  exception == nil else {
            XCTFail("System_Guid_Create2 should not throw and return an instance")
            
            return
        }
        
        defer {
            System_Object_Destroy(guid)
        }
        
        guard let guidString = String(dotNETString: System_Guid_ToString(guid,
																		 &exception),
									  destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System_Guid_ToString should not throw and return an instance")
			
			return
		}
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        let guidTypeName = "System.Guid"
		let guidTypeNameDN = guidTypeName.dotNETString()
		defer { System_String_Destroy(guidTypeNameDN) }
        
        guard let guidType = System_Type_GetType_2(guidTypeNameDN,
												   &exception),
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
        
        guard let emptyGuidString = String(dotNETString: System_Guid_ToString(emptyGuid,
																			  &exception),
										   destroyDotNETString: true),
              exception == nil else {
            XCTFail("System.Guid.ToString should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual("00000000-0000-0000-0000-000000000000", emptyGuidString)
    }
	
	func testSystemGuidParsing() {
		var exception: System_Exception_t?
		
		let uuid = UUID()
		
		let uuidString = uuid.uuidString
		let uuidStringDN = uuidString.dotNETString()
		defer { System_String_Destroy(uuidStringDN) }
		
		var guid: System_Guid_t?
		let success = System_Guid_TryParse(uuidStringDN,
										   &guid,
										   &exception)
		
		guard let guid,
			  exception == nil,
			  success else {
			XCTFail("System.Guid.TryParse should not throw and return an instance as out parameter")
			
			return
		}
		
		defer { System_Guid_Destroy(guid) }
		
		guard let uuidStringRet = String(dotNETString: System_Guid_ToString(guid,
																			&exception),
										 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Guid.ToString should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
	}
	
	func testInvalidSystemGuidParsing() {
		guard let emptyGuid = System_Guid_Empty_Get() else {
			XCTFail("Failed to get System.Guid.Empty")
			
			return
		}
		
		var exception: System_Exception_t?
		
		let uuidString = "nonsense"
		let uuidStringDN = uuidString.dotNETString()
		defer { System_String_Destroy(uuidStringDN) }
		
		var guid: System_Guid_t?
		let success = System_Guid_TryParse(uuidStringDN,
										   &guid,
										   &exception)
		
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
