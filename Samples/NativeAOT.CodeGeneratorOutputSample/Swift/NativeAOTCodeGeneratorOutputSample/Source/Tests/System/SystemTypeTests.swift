import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTypeTests: XCTestCase {
	func testSystemType() {
		var exception: System_Exception_t?
		
		let systemObjectTypeName = "System.Object"
		
		guard let systemObjectType = System_Type_GetType2(systemObjectTypeName,
														  &exception),
			  exception == nil else {
			XCTFail("System.Type.GetType should not throw and return an instance of System.Type")
			
			return
		}
		
		defer { System_Type_Destroy(systemObjectType) }
		
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
}
