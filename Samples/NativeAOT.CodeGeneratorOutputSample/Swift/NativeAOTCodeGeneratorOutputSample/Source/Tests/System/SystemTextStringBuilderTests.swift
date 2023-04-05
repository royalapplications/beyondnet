import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTextStringBuilderTests: XCTestCase {
	func testStringBuilder() {
		var exception: System_Exception_t?
		
		let hello = "Hello"
		let lineBreak = "\n"
		let world = "World"
		
		let expectedFinalString = "\(hello)\(lineBreak)\(world)"
		
		guard let sb = System_Text_StringBuilder_Create_2(hello,
														  &exception),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_StringBuilder_Destroy(sb) }
		
		guard let helloRet = System_Text_StringBuilder_ToString(sb,
																&exception).string(),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
			
			return
		}
		
		XCTAssertEqual(hello, helloRet)
		
		System_Text_StringBuilder_AppendLine(sb,
											 &exception)
		
		XCTAssertNil(exception)
		
		System_Text_StringBuilder_Append_2(sb,
										   world,
										   &exception)
		
		XCTAssertNil(exception)
		
		guard let finalStringRet = System_Text_StringBuilder_ToString(sb,
																	  &exception).string(),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
			
			return
		}
		
		XCTAssertEqual(expectedFinalString, finalStringRet)
	}
}
