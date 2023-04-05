import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemExceptionTests: XCTestCase {
    func testSystemException() {
        var exception: System_Exception_t?
        
        let exceptionMessage = "I'm a nice exception"
        
        var createdException: System_Exception_t?
        
        exceptionMessage.withCString { exceptionMessageC in
			createdException = System_Exception_Create_1(exceptionMessageC,
														 &exception)
        }
        
        guard let createdException,
              exception == nil else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Exception_Destroy(createdException) }
        
        guard let retrievedExceptionMessageC = System_Exception_Message_Get(createdException,
                                                                            &exception),
              exception == nil else {
            XCTFail("System.Exception.Message getter should not throw and return an instance of a C string")
            
            return
        }
        
        defer { retrievedExceptionMessageC.deallocate() }
        
        let retrievedExceptionMessage = String(String(cString: retrievedExceptionMessageC))
        
        XCTAssertEqual(exceptionMessage, retrievedExceptionMessage)
    }
}
