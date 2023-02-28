import XCTest
@testable import NativeAOTLibraryTest

final class SystemTypeTests: XCTestCase {
	func testSystemType() {
		let systemObjectTypeName = "Object"
		let systemObjectTypeFullName = "System.Object"
		
		guard let systemObjectType = SystemType(typeName: systemObjectTypeFullName) else {
			XCTFail("Failed to get type of \(systemObjectTypeFullName) via name")
			
			return
		}
		
		let systemObjectTypeViaObject = SystemObject.type
		
		XCTAssertTrue(systemObjectType == systemObjectTypeViaObject)
		XCTAssertEqual(systemObjectType.name, systemObjectTypeName)
		XCTAssertEqual(systemObjectType.fullName, systemObjectTypeFullName)
		
		XCTAssertTrue(systemObjectType.isAssignableFrom(systemObjectTypeViaObject))
		XCTAssertTrue(systemObjectType.isAssignableTo(systemObjectTypeViaObject))
		
		let systemTypeType = SystemType.type
		
		XCTAssertTrue(systemObjectType.isAssignableFrom(systemTypeType))
		XCTAssertFalse(systemObjectType.isAssignableTo(systemTypeType))
		
		XCTAssertFalse(systemTypeType.isAssignableFrom(systemObjectType))
		XCTAssertTrue(systemTypeType.isAssignableTo(systemObjectType))
		
		XCTAssertTrue(systemObjectType.is(of: SystemObject.self))
	}
}
