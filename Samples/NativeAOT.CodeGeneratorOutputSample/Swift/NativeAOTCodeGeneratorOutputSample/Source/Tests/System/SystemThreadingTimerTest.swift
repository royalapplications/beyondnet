import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemThreadingTimerTests: XCTestCase {
	override class func setUp() {
		Self.gcCollect()
	}
	
	override class func tearDown() {
		Self.gcCollect()
	}
	
	class Context {
		var timer: System_Threading_Timer_t?
		
		let maximumNumberOfTimesToCall = 30
		
		var numberOfTimesCalled = 0
		var numberOfTimesDestructorCalled = 0
	}
	
	func testTimer() {
		var exception: System_Exception_t?
		
		let swiftyContext = Context()
		let contextBox = NativeBox(swiftyContext)
		let context = contextBox.retainedPointer()
		
		let cFunction: System_Threading_TimerCallback_CFunction_t = { innerContext, state in
			if let state {
				// We need to release any reference types that are given to us through the delegate
				System_Object_Destroy(state)
			}
			
			var innerException: System_Exception_t?
			
			guard let innerContext else {
				XCTFail("Context is nil")
				
				return
			}
			
			XCTAssertFalse(Thread.isMainThread)
			
			let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
			let innerSwiftyContext = innerContextBox.value
			
			innerSwiftyContext.numberOfTimesCalled += 1
			
			if innerSwiftyContext.numberOfTimesCalled == innerSwiftyContext.maximumNumberOfTimesToCall {
				guard let innerTimer = innerSwiftyContext.timer else {
					XCTFail("Inner timer is nil")
					
					return
				}
				
				System_Threading_Timer_Dispose_1(innerTimer,
												 &innerException)
				
				XCTAssertNil(innerException)
			}
		}
		
		let cDestructorFunction: System_Threading_TimerCallback_CDestructorFunction_t = { innerContext in
			guard let innerContext else {
				XCTFail("Context is nil")
				
				return
			}
			
			let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
			let innerSwiftyContext = innerContextBox.value
			
			innerSwiftyContext.numberOfTimesDestructorCalled += 1
			
			XCTAssertEqual(1, innerSwiftyContext.numberOfTimesDestructorCalled)
			
			innerContextBox.release(innerContext)
		}
		
		let callback = System_Threading_TimerCallback_Create(context,
															 cFunction,
															 cDestructorFunction)
		
		guard let timer = System_Threading_Timer_Create(callback,
														nil,
														50,
														10,
														&exception),
			  exception == nil else {
			if let exception,
			   let exceptionString = String(dotNETString: System_Exception_ToString(exception, nil),
											destroyDotNETString: true) {
				print("System.Threading.Timer ctor failed with exception: \(exceptionString)")
			}
			
			XCTFail("System.Threading.Timer ctor should not throw and return an instance")
			
			return
		}
		
		swiftyContext.timer = timer
		
		while swiftyContext.numberOfTimesCalled != swiftyContext.maximumNumberOfTimesToCall {
			Thread.sleep(forTimeInterval: 0.01)
		}
		
		System_Threading_Timer_Destroy(timer)
		System_Threading_TimerCallback_Destroy(callback)
		
		System_GC_Collect_1(&exception)
		XCTAssertNil(exception)
		
		System_GC_WaitForPendingFinalizers(&exception)
		XCTAssertNil(exception)
		
		XCTAssertGreaterThanOrEqual(1, swiftyContext.numberOfTimesDestructorCalled)
	}
}
