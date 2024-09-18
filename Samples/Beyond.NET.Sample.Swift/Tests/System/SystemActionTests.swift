import XCTest
import BeyondDotNETSampleKit

final class SystemActionTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testSystemActionType() throws {
		let systemActionType = System_Action.typeOf
		
		let systemActionFullTypeName = try systemActionType.fullName?.string()
		XCTAssertEqual("System.Action", systemActionFullTypeName)
	}
	
	func testSystemAction() throws {
		var numberOfTimesCalled = 0
		
		let closure: System_Action.ClosureType = {
			numberOfTimesCalled += 1
		}
		
		let action = System_Action(closure)
		
		XCTAssertEqual(0, numberOfTimesCalled)
		
		try action.invoke()
		XCTAssertEqual(1, numberOfTimesCalled)
		
		try action.invoke()
		try action.invoke()
		try action.invoke()
		XCTAssertEqual(4, numberOfTimesCalled)
	}
}
