import XCTest
@testable import NativeAOTSample

final class SystemTypeTests: XCTestCase {
	func testSystemType() {
		let systemObjectTypeName = "Object"
		let systemObjectTypeFullName = "System.Object"
		
        guard let systemObjectType = System._Type(typeName: systemObjectTypeFullName) else {
			XCTFail("Failed to get type of \(systemObjectTypeFullName) via name")
			
			return
		}
		
        let systemObjectTypeViaObject = System.Object.type
		
		XCTAssertTrue(systemObjectType == systemObjectTypeViaObject)
		XCTAssertEqual(systemObjectType.name, systemObjectTypeName)
		XCTAssertEqual(systemObjectType.fullName, systemObjectTypeFullName)
		
		XCTAssertTrue(systemObjectType.isAssignableFrom(systemObjectTypeViaObject))
		XCTAssertTrue(systemObjectType.isAssignableTo(systemObjectTypeViaObject))
		
        let systemTypeType = System._Type.type
		
		XCTAssertTrue(systemObjectType.isAssignableFrom(systemTypeType))
		XCTAssertFalse(systemObjectType.isAssignableTo(systemTypeType))
		
		XCTAssertFalse(systemTypeType.isAssignableFrom(systemObjectType))
		XCTAssertTrue(systemTypeType.isAssignableTo(systemObjectType))
		
        XCTAssertTrue(systemObjectType.is(of: System.Object.self))
	}
	
	func testSystemTypeCreationPerformance() {
		let debugLoggingWasEnabled = Debug.isLoggingEnabled
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = debugLoggingWasEnabled }
		
		let iterations = 10_000
		
        guard let typeName = System._Type.type.fullName else {
			XCTFail("Failed to get type of System.Type")
			
			return
		}
		
		measure {
			for _ in 0..<iterations {
                _ = System._Type(typeName: typeName)
			}
		}
	}
}
