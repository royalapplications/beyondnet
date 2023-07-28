import XCTest
import BeyondDotNETSampleKit

final class SystemActionTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testSystemActionType() {
		let systemActionType = System_Action.typeOf
		
		guard let systemActionFullTypeName = try? systemActionType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual("System.Action", systemActionFullTypeName)
	}
	
	func testSystemAction() {
		var numberOfTimesCalled = 0
		
		let closure: System_Action.ClosureType = {
			numberOfTimesCalled += 1
		}
		
		guard let action = System_Action(closure) else {
			XCTFail("System.Action ctor should return an instance")
			
			return
		}
		
		XCTAssertEqual(0, numberOfTimesCalled)
		
		XCTAssertNoThrow(try action.invoke())
		XCTAssertEqual(1, numberOfTimesCalled)
		
		XCTAssertNoThrow(try action.invoke())
		XCTAssertNoThrow(try action.invoke())
		XCTAssertNoThrow(try action.invoke())
		XCTAssertEqual(4, numberOfTimesCalled)
	}
}
