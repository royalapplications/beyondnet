import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTextStringBuilderTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testStringBuilder() {
		var exception: System_Exception_t?
		
		let hello = "Hello"
		let helloDN = hello.cDotNETString()
		defer { System_String_Destroy(helloDN) }
		
		let lineBreak = "\n"
		
		let world = "World"
		let worldDN = world.cDotNETString()
		defer { System_String_Destroy(worldDN) }
		
		let expectedFinalString = "\(hello)\(lineBreak)\(world)"
		
		guard let sb = System_Text_StringBuilder_Create_2(helloDN,
														  &exception),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_StringBuilder_Destroy(sb) }
		
		guard let helloRet = String(cDotNETString: System_Text_StringBuilder_ToString(sb,
																					 &exception),
									destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
			
			return
		}
		
		XCTAssertEqual(hello, helloRet)
		
		System_Text_StringBuilder_AppendLine(sb,
											 &exception)
		
		XCTAssertNil(exception)
		
		System_Text_StringBuilder_Append_2(sb,
										   worldDN,
										   &exception)
		
		XCTAssertNil(exception)
		
		guard let finalStringRet = String(cDotNETString: System_Text_StringBuilder_ToString(sb,
																						   &exception),
										  destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
			
			return
		}
		
		XCTAssertEqual(expectedFinalString, finalStringRet)
	}
}
