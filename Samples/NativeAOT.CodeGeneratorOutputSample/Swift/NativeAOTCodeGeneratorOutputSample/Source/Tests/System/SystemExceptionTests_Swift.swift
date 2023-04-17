import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemExceptionTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemException() {
        let exceptionMessage = "I'm a nice exception"
        
        guard let createdException = try? System_Exception(exceptionMessage.dotNETString()) else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        guard let retrievedExceptionMessage = (try? createdException.message_get())?.string() else {
            XCTFail("System.Exception.Message getter should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(exceptionMessage, retrievedExceptionMessage)
    }
}
