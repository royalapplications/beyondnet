import XCTest
import NativeAOTCodeGeneratorOutputSample

// TODO
final class SystemActionTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	class Context {
		var numberOfTimesCalled = 0
		var numberOfTimesDestructorCalled = 0
	}
	
	func testSystemAction() {
		var numberOfTimesCalled = 0
		
		let closure: System_Action.ClosureType = {
			numberOfTimesCalled += 1
		}
		
		let action = System_Action(closure)
		
		XCTAssertEqual(0, numberOfTimesCalled)
		
		// TODO
//		System_Action_Invoke(action, &exception)
//		XCTAssertNil(exception)
//		XCTAssertEqual(1, swiftyContext.numberOfTimesCalled)
//
//		System_Action_Invoke(action, &exception)
//		XCTAssertNil(exception)
//
//		System_Action_Invoke(action, &exception)
//		XCTAssertNil(exception)
//
//		System_Action_Invoke(action, &exception)
//		XCTAssertNil(exception)
//
//		XCTAssertEqual(4, swiftyContext.numberOfTimesCalled)
//
//		System_Action_Destroy(action)
//
//		System_GC_Collect_1(&exception)
//		XCTAssertNil(exception)
//
//		System_GC_WaitForPendingFinalizers(&exception)
//		XCTAssertNil(exception)
//
//		XCTAssertEqual(1, swiftyContext.numberOfTimesDestructorCalled)
	}
}
