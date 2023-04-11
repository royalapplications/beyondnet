import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemActionTests: XCTestCase {
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
		var exception: System_Exception_t?
		
        let swiftyContext = Context()
        let contextBox = NativeBox(swiftyContext)
        let context = contextBox.retainedPointer()
        
        let cFunction: System_Action_CFunction_t = { innerContext in
            guard let innerContext else {
                XCTFail("Context is nil")
                
                return
            }
            
            let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
            let innerSwiftyContext = innerContextBox.value
            
            innerSwiftyContext.numberOfTimesCalled += 1
        }
		
		let cDestructorFunction: System_Action_CDestructorFunction_t = { innerContext in
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
        
        let action = System_Action_Create(context,
                                          cFunction,
                                          cDestructorFunction)
        
        XCTAssertEqual(0, swiftyContext.numberOfTimesCalled)
        
        System_Action_Invoke(action, &exception)
		XCTAssertNil(exception)
        XCTAssertEqual(1, swiftyContext.numberOfTimesCalled)
        
        System_Action_Invoke(action, &exception)
		XCTAssertNil(exception)
		
        System_Action_Invoke(action, &exception)
		XCTAssertNil(exception)
		
        System_Action_Invoke(action, &exception)
		XCTAssertNil(exception)
		
        XCTAssertEqual(4, swiftyContext.numberOfTimesCalled)
		
		System_Action_Destroy(action)
		
		System_GC_Collect_1(&exception)
		XCTAssertNil(exception)

		System_GC_WaitForPendingFinalizers(&exception)
		XCTAssertNil(exception)
		
		XCTAssertEqual(1, swiftyContext.numberOfTimesDestructorCalled)
    }
}
