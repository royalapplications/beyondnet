import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTypeTests: XCTestCase {
	func testSystemType() {
		var exception: System_Exception_t?
		
		let systemObjectTypeName = "System.Object"
		
        guard let systemObjectType = System_Object_TypeOf() else {
            XCTFail("typeof(System.Object) should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemObjectType) }
        
        guard let systemObjectTypeViaName = System_Type_GetType2(systemObjectTypeName,
                                                                 &exception),
			  exception == nil else {
			XCTFail("System.Type.GetType should not throw and return an instance of System.Type")
			
			return
		}
		
		defer { System_Type_Destroy(systemObjectTypeViaName) }
        
        guard System_Object_Equals(systemObjectType,
                                   systemObjectTypeViaName,
                                   &exception),
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
		
		guard let retrievedSystemObjectTypeNameC = System_Type_FullName_Get(systemObjectType,
																			&exception),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { retrievedSystemObjectTypeNameC.deallocate() }
		
		let retrievedSystemObjectTypeName = String(cString: retrievedSystemObjectTypeNameC)
		
		XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
	}
	
	func testInvalidType() {
		var exception: System_Exception_t?
		
		let invalidTypeName = "! This.Type.Surely.Does.Not.Exist !"
		
		let invalidType = System_Type_GetType1(invalidTypeName,
											   true,
											   &exception)
		
		XCTAssertNil(invalidType)
		
		guard let exception else {
			XCTFail("System.Type.GetType should throw")
			
			return
		}
		
		defer { System_Exception_Destroy(exception) }
		
		var exception2: System_Exception_t?
		
		guard let exceptionMessageC = System_Exception_Message_Get(exception,
																   &exception2),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { exceptionMessageC.deallocate() }
		
		let exceptionMessage = String(cString: exceptionMessageC)
		
		XCTAssertTrue(exceptionMessage.contains("The type \'\(invalidTypeName)\' cannot be found"))
	}
}
