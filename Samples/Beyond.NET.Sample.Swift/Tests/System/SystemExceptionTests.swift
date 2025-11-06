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
    
    func testSystemExceptionConvertedToSwiftError() throws {
        let exMessage = "I'm a nice exception"
        
        let ex = try System_Exception(exMessage.dotNETString())
        let err = ex.swiftError
        
        XCTAssertEqual(try ex.message.string(), err.localizedDescription)
        
        let caughtErr: DNError?
        
        do {
            try Beyond_NET_Sample_ExceptionTests.testThrow(ex)
            caughtErr = nil
        } catch let error as DNError {
            caughtErr = error
        }
        
        guard let caughtErr else {
            XCTFail("No error caught")
            return
        }
        
        let caughtEx = caughtErr.exception
        
        XCTAssertEqual(ex, caughtEx)
        
        let expectedMessage = try caughtEx.message.string()
        let errMessage = caughtErr.errorDescription
        
        XCTAssertEqual(expectedMessage, errMessage)
        
        let expectedStackTrace = try caughtEx.stackTrace?.string()
        let errStackTrace = caughtErr.stackTrace()
        
        XCTAssertEqual(expectedStackTrace, errStackTrace)
        
        let expectedDebugDescription = try caughtEx.toString().string()
        let errDebugDescription = caughtErr.debugDescription
        
        XCTAssertEqual(expectedDebugDescription, errDebugDescription)
    }
}
