import XCTest
import BeyondNETSamplesSwift

final class SystemThreadingThreadTests_Swift: XCTestCase {
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
	
	func testThread() {
		var numberOfTimesCalled = 0
		
		let closure: System_Threading_ThreadStart.ClosureType = {
			XCTAssertFalse(Thread.isMainThread)
			
			numberOfTimesCalled += 1
		}
		
		let threadStart = System_Threading_ThreadStart(closure)
		
		guard let thread = try? System_Threading_Thread(threadStart) else {
			XCTFail("System.Threading.Thread ctor should not throw and return an instance")

			return
		}
		
		XCTAssertNoThrow(try thread.start())

		while numberOfTimesCalled < 1 {
			Thread.sleep(forTimeInterval: 0.01)
		}
		
		XCTAssertEqual(1, numberOfTimesCalled)
	}
}
