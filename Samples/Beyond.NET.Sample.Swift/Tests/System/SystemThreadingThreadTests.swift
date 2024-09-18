import XCTest
import BeyondDotNETSampleKit

final class SystemThreadingThreadTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testThread() throws {
		var numberOfTimesCalled = 0
		
		let closure: System_Threading_ThreadStart.ClosureType = {
			XCTAssertFalse(Thread.isMainThread)
			
			numberOfTimesCalled += 1
		}
		
        let threadStart = System_Threading_ThreadStart(closure)
		let thread = try System_Threading_Thread(threadStart)
		
		try thread.start()

		while numberOfTimesCalled < 1 {
			Thread.sleep(forTimeInterval: 0.01)
		}
		
		XCTAssertEqual(1, numberOfTimesCalled)
	}
}
