import XCTest
import BeyondDotNETSampleKit

final class SystemExceptionTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemException() throws {
        let exceptionMessage = "I'm a nice exception"
        
        let createdException = try System_Exception(exceptionMessage.dotNETString())
        let retrievedExceptionMessage = try createdException.message.string()
        
        XCTAssertEqual(exceptionMessage, retrievedExceptionMessage)
    }
}
