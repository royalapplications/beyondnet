import XCTest
import BeyondDotNETSampleKit

final class SystemThreadingTimerTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testTimer() throws {
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
        
        let _timer = try System_Threading_Timer(.init(closure),
                                                nil,
                                                50 as Int32,
                                                10 as Int32)
		
		timer = _timer
		
		while numberOfTimesCalled != maximumNumberOfTimesToCall {
			Thread.sleep(forTimeInterval: 0.01)
		}
		
		XCTAssertEqual(numberOfTimesCalled, maximumNumberOfTimesToCall)
	}
}
