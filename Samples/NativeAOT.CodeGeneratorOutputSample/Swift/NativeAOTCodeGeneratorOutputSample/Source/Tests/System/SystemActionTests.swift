import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemActionTests: XCTestCase {
    func testSystemAction() {
//        var exception: System_Exception_t?
        
        let swiftyContext = "My Context"
        let contextBox = NativeBox(swiftyContext)
        let context = contextBox.unretainedPointer()
        
        let cFunction: System_Action_CFunction_t = { innerContext in
            guard let innerContext else {
                XCTFail("Context is nil")
                
                return
            }
            
            let innerContextBox = NativeBox<String>.fromPointer(innerContext)
            let innerSwiftyContext = innerContextBox.value
            
            XCTAssertEqual("My Context", innerSwiftyContext)
        }
        
        let action = System_Action_Create(context,
                                          cFunction,
                                          nil)
        
        defer { System_Action_Destroy(action) }
        
        // TODO
//        System_Action_Invoke()
    }
}
