import XCTest
import BeyondDotNETSampleNative

final class SystemThreadingTimerTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testTimer() {
		let maximumNumberOfTimesToCall = 30
		var numberOfTimesCalled = 0
		
		var timer: System_Threading_Timer? = nil
		
		let closure: System_Threading_TimerCallback.ClosureType = { state in
			XCTAssertFalse(Thread.isMainThread)
			
			numberOfTimesCalled += 1
			
			if numberOfTimesCalled == maximumNumberOfTimesToCall {
				guard let timer else {
					XCTFail("timer is nil")
					
					return
				}
				
				do {
					try timer.dispose()
				} catch {
					XCTFail("System.Threading.Timer.Dispose should not throw")
					
					return
				}
			}
		}
		
		let callback = System_Threading_TimerCallback(closure)
		
		guard let _timer = try? System_Threading_Timer(callback,
													  nil,
													  50 as Int32,
													  10 as Int32) else {
			XCTFail("System.Threading.Timer ctor should not throw and return an instance")
			
			return
		}
		
		timer = _timer
		
		while numberOfTimesCalled != maximumNumberOfTimesToCall {
			Thread.sleep(forTimeInterval: 0.01)
		}
		
		XCTAssertEqual(numberOfTimesCalled, maximumNumberOfTimesToCall)
	}
}
