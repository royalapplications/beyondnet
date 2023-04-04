import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemThreadingTasksTests: XCTestCase {
	private class Context {
		let actionFunction: () -> Void
		let destructorFunction: () -> Void
		
		lazy var actionFunctionC: System_Action_CFunction_t = {
			{ innerContext in
				guard let innerContext else {
					XCTFail("Inner context is nil")
					
					return
				}
				
				let innerBoxedContext = NativeBox<Context>.fromPointer(innerContext)
				let innerSwiftyContext = innerBoxedContext.value
				
				innerSwiftyContext.actionFunction()
			}
		}()
		
		lazy var destructorFunctionC: System_Action_CDestructorFunction_t = {
			{ innerContext in
				guard let innerContext else {
					XCTFail("Inner context is nil")
					
					return
				}
				
				let innerBoxedContext = NativeBox<Context>.fromPointer(innerContext)
				let innerSwiftyContext = innerBoxedContext.value
				
				innerSwiftyContext.destructorFunction()
				
				innerBoxedContext.release(innerContext)
			}
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
		
		let swiftyContext = Context(actionFunction: {
			actionFunctionHasCompleted = true
		}, destructorFunction: {
			destructorFunctionHasCompleted = true
		})
		
		let boxedContext = NativeBox(swiftyContext)
		let context = boxedContext.retainedPointer()
		
		guard let action = System_Action_Create(context,
												swiftyContext.actionFunctionC,
												swiftyContext.destructorFunctionC) else {
			XCTFail("System.Action ctor should return an instance")
			
			return
		}
		
		guard let task = System_Threading_Tasks_Task_Run2(action,
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
		
		System_GC_Collect1(&exception)
		XCTAssertNil(exception)
		
		System_GC_WaitForPendingFinalizers(&exception)
		XCTAssertNil(exception)
		
		XCTAssertTrue(destructorFunctionHasCompleted)
	}
}
