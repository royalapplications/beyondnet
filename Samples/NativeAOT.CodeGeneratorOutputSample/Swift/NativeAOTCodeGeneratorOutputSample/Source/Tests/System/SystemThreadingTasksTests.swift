import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemThreadingTasksTests: XCTestCase {
	private class System_Action_Swift {
		let actionFunction: () -> Void
		let destructorFunction: () -> Void
		
		private lazy var actionFunctionC: System_Action_CFunction_t = {
			{ innerContext in
				guard let innerContext else {
					XCTFail("Inner context is nil")
					
					return
				}
				
				let innerBoxedContext = NativeBox<System_Action_Swift>.fromPointer(innerContext)
				let innerSwiftyContext = innerBoxedContext.value
				
				innerSwiftyContext.actionFunction()
			}
		}()
		
		private lazy var destructorFunctionC: System_Action_CDestructorFunction_t = {
			{ innerContext in
				guard let innerContext else {
					XCTFail("Inner context is nil")
					
					return
				}
				
				let innerBoxedContext = NativeBox<System_Action_Swift>.fromPointer(innerContext)
				let innerSwiftyContext = innerBoxedContext.value
				
				innerSwiftyContext.destructorFunction()
				
				innerBoxedContext.release(innerContext)
			}
		}()
		
		private lazy var boxedContext: UnsafeRawPointer = {
			NativeBox(self).retainedPointer()
		}()
		
		lazy var systemAction: System_Action_t? = {
			System_Action_Create(boxedContext,
								 actionFunctionC,
								 destructorFunctionC)
		}()
		
		init(actionFunction: @escaping () -> Void,
			 destructorFunction: @escaping () -> Void) {
			self.actionFunction = actionFunction
			self.destructorFunction = destructorFunction
		}
	}
	
	func testTasks() {
		var exception: System_Exception_t?
		
		var actionFunctionHasCompleted = false
		var destructorFunctionHasCompleted = false
		
		let swiftySystemAction = System_Action_Swift(actionFunction: {
			actionFunctionHasCompleted = true
		}, destructorFunction: {
			destructorFunctionHasCompleted = true
		})
		
		guard let action = swiftySystemAction.systemAction else {
			XCTFail("System.Action ctor should return an instance")
			
			return
		}
		
		guard let task = System_Threading_Tasks_Task_Run_2(action,
														   nil,
														   &exception),
			  exception == nil else {
			XCTFail("System.Threading.Tasks.Task.Run should not throw and return an instance")
			
			return
		}
		
		System_Threading_Tasks_Task_Wait(task,
										 &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertTrue(actionFunctionHasCompleted)
		
		System_Action_Destroy(action)
		System_Threading_Tasks_Task_Destroy(task)
		
		System_GC_Collect_1(&exception)
		XCTAssertNil(exception)
		
		System_GC_WaitForPendingFinalizers(&exception)
		XCTAssertNil(exception)
		
		XCTAssertTrue(destructorFunctionHasCompleted)
	}
}
