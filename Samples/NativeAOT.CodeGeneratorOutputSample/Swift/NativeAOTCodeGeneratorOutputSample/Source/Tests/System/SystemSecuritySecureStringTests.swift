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
		
		let retStringPtr = System_Runtime_InteropServices_Marshal_SecureStringToGlobalAllocAnsi(secureString,
																								&exception)
		
		XCTAssertNil(exception)
		
		defer {
			System_Runtime_InteropServices_Marshal_FreeHGlobal(retStringPtr,
															   &exception)
			
			XCTAssertNil(exception)
		}
		
		guard let retStringC = System_Runtime_InteropServices_Marshal_PtrToStringAuto1(retStringPtr,
																					   &exception),
			  exception == nil else {
			XCTFail("System.Runtime.InteropServices.Marshal.PtrToStringAuto should not throw and return an instance of a C String")
			
			return
		}
		
		defer { retStringC.deallocate() }
		
		let retString = String(cString: retStringC)
		
		XCTAssertEqual(string, retString)
	}
}
