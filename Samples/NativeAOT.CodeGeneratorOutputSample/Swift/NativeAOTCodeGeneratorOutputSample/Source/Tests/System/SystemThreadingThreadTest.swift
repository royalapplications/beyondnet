import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemThreadingThreadTests: XCTestCase {
	class Context {
		var numberOfTimesCalled = 0
		var numberOfTimesDestructorCalled = 0
	}
	
	func testThread() {
		var exception: System_Exception_t?
		
		let swiftyContext = Context()
		let contextBox = NativeBox(swiftyContext)
		let context = contextBox.retainedPointer()
		
		let cFunction: System_Threading_ThreadStart_CFunction_t = { innerContext in
			guard let innerContext else {
				XCTFail("Context is nil")
				
				return
			}
			
			XCTAssertFalse(Thread.isMainThread)
			
			let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
			let innerSwiftyContext = innerContextBox.value
			
			innerSwiftyContext.numberOfTimesCalled += 1
		}
		
		let cDestructorFunction: System_Threading_ThreadStart_CDestructorFunction_t = { innerContext in
			guard let innerContext else {
				XCTFail("Context is nil")
				
				return
			}
			
			let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
			let innerSwiftyContext = innerContextBox.value
			
			innerSwiftyContext.numberOfTimesDestructorCalled += 1
			
			innerContextBox.release(innerContext)
		}
		
		let threadStart = System_Threading_ThreadStart_Create(context,
															  cFunction,
															  cDestructorFunction)
		
		guard let thread = System_Threading_Thread_Create(threadStart,
														  &exception),
			  exception == nil else {
			XCTFail("System.Threading.Thread ctor should not throw and return an instance")
			
			return
		}
		
		System_Threading_Thread_Start1(thread,
									   &exception)
		
		XCTAssertNil(exception)
		
		while swiftyContext.numberOfTimesCalled < 1 {
			Thread.sleep(forTimeInterval: 0.1)
		}
		
		System_Threading_Thread_Destroy(thread)
		System_Threading_ThreadStart_Destroy(threadStart)
		
		System_GC_Collect1(&exception)
		XCTAssertNil(exception)
		
		System_GC_WaitForPendingFinalizers(&exception)
		XCTAssertNil(exception)
		
		XCTAssertEqual(1, swiftyContext.numberOfTimesDestructorCalled)
	}
}
