import XCTest
@testable import NativeAOTLibraryTest

final class SystemObjectTests: XCTestCase {
	func testSystemObject() {
		let systemObjectType = SystemObject.type
		
		let object1 = SystemObject()
		let object2 = SystemObject()
		
		XCTAssertEqual(systemObjectType, object1.type)
		XCTAssertEqual("Object", object1.type.name)
		XCTAssertEqual("System.Object", object1.type.fullName)
		XCTAssertEqual("System.Object", object1.toString())
		// swiftlint:disable:next identical_operands
		XCTAssertTrue(object1 == object1)
		XCTAssertFalse(object1 == object2)
		
		let object1AsSystemType = object1.cast(as: SystemType.self)
		XCTAssertNil(object1AsSystemType)
		
		let object1AsSystemObject = object1.cast(as: SystemObject.self)
		XCTAssertNotNil(object1AsSystemObject)
		XCTAssertTrue(object1 == object1AsSystemObject)
	}
}
