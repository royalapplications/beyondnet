import XCTest
import BeyondNETSamplesSwift

final class SystemExceptionTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
    func testSystemException() {
        var exception: System_Exception_t?
        
        let exceptionMessage = "I'm a nice exception"
		let exceptionMessageDN = exceptionMessage.cDotNETString()
		
		defer { System_String_Destroy(exceptionMessageDN) }
        
        guard let createdException = System_Exception_Create_1(exceptionMessageDN,
															   &exception),
              exception == nil else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Exception_Destroy(createdException) }
        
        guard let retrievedExceptionMessage = String(cDotNETString: System_Exception_Message_Get(createdException,
																								&exception),
													 destroyDotNETString: true),
              exception == nil else {
            XCTFail("System.Exception.Message getter should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(exceptionMessage, retrievedExceptionMessage)
    }
}
