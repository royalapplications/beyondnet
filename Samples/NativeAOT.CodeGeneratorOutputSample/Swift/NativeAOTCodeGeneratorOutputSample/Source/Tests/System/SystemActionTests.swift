import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemActionTests: XCTestCase {
    class Context {
        var numberOfTimesCalled = 0
    }
    
    func testSystemAction() {
        let swiftyContext = Context()
        let contextBox = NativeBox(swiftyContext)
        let context = contextBox.unretainedPointer()
        
        let cFunction: System_Action_CFunction_t = { innerContext in
            guard let innerContext else {
                XCTFail("Context is nil")
                
                return
            }
            
            let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
            let innerSwiftyContext = innerContextBox.value
            
            innerSwiftyContext.numberOfTimesCalled += 1
        }
        
        let action = System_Action_Create(context,
                                          cFunction,
                                          nil)
        
        defer { System_Action_Destroy(action) }
        
        XCTAssertEqual(0, swiftyContext.numberOfTimesCalled)
        
        System_Action_Invoke(action)
        XCTAssertEqual(1, swiftyContext.numberOfTimesCalled)
        
        System_Action_Invoke(action)
        System_Action_Invoke(action)
        System_Action_Invoke(action)
        XCTAssertEqual(4, swiftyContext.numberOfTimesCalled)
    }
}
