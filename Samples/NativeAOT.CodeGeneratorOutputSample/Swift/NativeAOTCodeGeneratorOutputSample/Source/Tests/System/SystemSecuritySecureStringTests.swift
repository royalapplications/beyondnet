import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemSecuritySecureStringTests: XCTestCase {
	func testSecureString() {
		var exception: System_Exception_t?
		
		let string = "Hello"
		
		guard let secureString = System_Security_SecureString_Create(&exception),
			  exception == nil else {
			XCTFail("System.Security.SecureString ctor should not throw and return an instance")
			
			return
		}
		
		defer {
			System_Security_SecureString_Dispose(secureString,
												 &exception)
			
			XCTAssertNil(exception)
			
			System_Security_SecureString_Destroy(secureString)
		}
		
		let characters = string.utf8.map{ UInt8($0) }
		
		for character in characters {
			System_Security_SecureString_AppendChar(secureString,
													character,
													&exception)
			
			XCTAssertNil(exception)
		}
		
		// TODO: Need Marshal class to get back a regular string
	}
}
