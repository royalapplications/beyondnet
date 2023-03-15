import XCTest
@testable import NativeAOTSample

final class SystemObjectTests: XCTestCase {
	func testSystemObject() {
        let systemObjectType = System.Object.type
		
        let object1 = System.Object()
        let object2 = System.Object()
		
		XCTAssertEqual(systemObjectType, object1.type)
		XCTAssertEqual("Object", object1.type.name)
		XCTAssertEqual("System.Object", object1.type.fullName)
		XCTAssertEqual("System.Object", object1.toString())
		// swiftlint:disable:next identical_operands
		XCTAssertTrue(object1 == object1)
		XCTAssertFalse(object1 == object2)
		
        let object1AsSystemType = object1.cast(as: System._Type.self)
		XCTAssertNil(object1AsSystemType)
		
        let object1AsSystemObject = object1.cast(as: System.Object.self)
		XCTAssertNotNil(object1AsSystemObject)
		XCTAssertTrue(object1 == object1AsSystemObject)
		
        XCTAssertTrue(object1.is(of: System.Object.self))
        XCTAssertFalse(object1.is(of: System._Type.self))
		
        XCTAssertTrue(systemObjectType.is(of: System.Object.self))
        XCTAssertTrue(systemObjectType.is(of: System._Type.self))
	}
	
	func testSystemObjectCreationPerformance() {
		let debugLoggingWasEnabled = Debug.isLoggingEnabled
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = debugLoggingWasEnabled }
		
		let iterations = 100_000
		
		measure {
			for _ in 0..<iterations {
                _ = System.Object()
			}
		}
	}
}
