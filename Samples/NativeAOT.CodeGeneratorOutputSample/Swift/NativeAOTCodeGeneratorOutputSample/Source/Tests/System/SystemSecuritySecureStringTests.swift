import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemSecuritySecureStringTests: XCTestCase {
	func testSecureString() {
		var exception: System_Exception_t?
		
		var string = "Hello World!"
		
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
		
		string.withUTF8 { buffer in
			for character in buffer {
				System_Security_SecureString_AppendChar(secureString,
														character,
														&exception)
				
				XCTAssertNil(exception)
			}
		}
		
		let retStringPtr = System_Runtime_InteropServices_Marshal_SecureStringToGlobalAllocUnicode(secureString,
																								   &exception)
		
		XCTAssertNil(exception)
		
		defer {
			System_Runtime_InteropServices_Marshal_ZeroFreeGlobalAllocUnicode(retStringPtr,
																			  &exception)
			
			XCTAssertNil(exception)
		}
		
		guard let retString = String(dotNETString: System_Runtime_InteropServices_Marshal_PtrToStringUni(retStringPtr,
																										 &exception),
									 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Runtime.InteropServices.Marshal.PtrToStringUni should not throw and return an instance of a C String")
			
			return
		}
		
		XCTAssertEqual(string, retString)
	}
}
