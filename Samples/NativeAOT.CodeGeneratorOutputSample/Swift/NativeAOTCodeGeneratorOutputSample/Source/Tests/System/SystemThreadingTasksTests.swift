import XCTest
import NativeAOTCodeGeneratorOutputSample

// TODO: I think we have an issue here but I can't currently see it so this test is disabled for now
final class SystemThreadingTasksTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.gcCollect()
	}
	
	@MainActor
	override class func tearDown() {
		Self.gcCollect()
	}
	
	private class System_Action_Swift {
        private(set) var numberOfTimesActionFunctionWasCalled = 0
        private(set) var numberOfTimesDestructorFunctionWasCalled = 0
        
		let actionFunction: () -> Void
		let destructorFunction: () -> Void
		
		init(actionFunction: @escaping () -> Void,
			 destructorFunction: @escaping () -> Void) {
			self.actionFunction = actionFunction
			self.destructorFunction = destructorFunction
		}
        
        func createSystemAction() -> System_Action_t? {
            let actionFunctionC: System_Action_CFunction_t = { innerContext in
                guard let innerContext else {
                    XCTFail("Inner context is nil")
                    
                    return
                }
                
                let innerBoxedContext = NativeBox<System_Action_Swift>.fromPointer(innerContext)
                let innerSwiftyContext = innerBoxedContext.value
                
                innerSwiftyContext.numberOfTimesActionFunctionWasCalled += 1
                innerSwiftyContext.actionFunction()
            }
            
            let destructorFunctionC: System_Action_CDestructorFunction_t = { innerContext in
                guard let innerContext else {
                    XCTFail("Inner context is nil")
                    
                    return
                }
                
                let innerBoxedContext = NativeBox<System_Action_Swift>.fromPointer(innerContext)
                let innerSwiftyContext = innerBoxedContext.value
                
                innerSwiftyContext.numberOfTimesDestructorFunctionWasCalled += 1
                XCTAssertEqual(1, innerSwiftyContext.numberOfTimesDestructorFunctionWasCalled)
                
                innerSwiftyContext.destructorFunction()
                
                innerBoxedContext.release(innerContext)
            }
            
            let boxedContext = NativeBox(self).retainedPointer()
            
            let systemAction = System_Action_Create(boxedContext,
                                                    actionFunctionC,
                                                    destructorFunctionC)
            
            return systemAction
        }
	}
	
	func testTasks() {
		var exception: System_Exception_t?
		
		var actionFunctionHasCompleted = false
//		var destructorFunctionHasCompleted = false
		
		let swiftySystemAction = System_Action_Swift(actionFunction: {
			actionFunctionHasCompleted = true
		}, destructorFunction: {
//			destructorFunctionHasCompleted = true
		})
		
		guard let action = swiftySystemAction.createSystemAction() else {
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
		
        System_Threading_Tasks_Task_Destroy(task)
        System_Action_Destroy(action)
		
//		System_GC_Collect_1(&exception)
//		XCTAssertNil(exception)
//
//		System_GC_WaitForPendingFinalizers(&exception)
//		XCTAssertNil(exception)
//
//		XCTAssertTrue(destructorFunctionHasCompleted)
	}
}
