import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemConvertTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testBooleanConversion() {
		var exception: System_Exception_t?
		
		let trueStringDN = "true".cDotNETString()
		defer { System_String_Destroy(trueStringDN) }
		
		let falseStringDN = "false".cDotNETString()
		defer { System_String_Destroy(falseStringDN) }
		
		let nonsenseStringDN = "nonsense".cDotNETString()
		defer { System_String_Destroy(nonsenseStringDN) }
		
		XCTAssertTrue(System_Convert_ToBoolean_12(trueStringDN, &exception))
		XCTAssertNil(exception)
		
		XCTAssertFalse(System_Convert_ToBoolean_12(falseStringDN, &exception))
		XCTAssertNil(exception)
		
		XCTAssertFalse(System_Convert_ToBoolean_12(nonsenseStringDN, &exception))
		XCTAssertNotNil(exception)
	}
	
	func testIntegerConversion() {
		var exception: System_Exception_t?
		
		let number1: Int32 = 123456789
		let number1StringDN = "\(number1)".cDotNETString()
		defer { System_String_Destroy(number1StringDN) }
		
		XCTAssertEqual(number1,
					   System_Convert_ToInt32_15(number1StringDN, &exception))
		XCTAssertNil(exception)
		
		let number2: Int64 = -123456789
		let number2StringDN = "\(number2)".cDotNETString()
		defer { System_String_Destroy(number2StringDN) }
		
		XCTAssertEqual(number2,
					   System_Convert_ToInt64_15(number2StringDN, &exception))
		XCTAssertNil(exception)
		
		let number3: Int64 = -1
		let number3StringDN = "nonsense".cDotNETString()
		defer { System_String_Destroy(number3StringDN) }
		
		XCTAssertEqual(number3,
					   System_Convert_ToInt64_15(number3StringDN, &exception))
		XCTAssertNotNil(exception)
		
		let number4: UInt64 = 0
		let number4StringDN = "nonsense".cDotNETString()
		defer { System_String_Destroy(number4StringDN) }
		
		XCTAssertEqual(number4,
					   System_Convert_ToUInt64_15(number4StringDN, &exception))
		XCTAssertNotNil(exception)
	}
	
	func testBase64Conversion() {
		var exception: System_Exception_t?
		
		let text = "Hello World!"
		let textDN = text.cDotNETString()
		defer { System_String_Destroy(textDN) }
		
		guard let utf8Encoding = System_Text_Encoding_UTF8_Get(&exception),
			  exception == nil else {
			XCTFail("System.Text.Encoding.UTF8 getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_Encoding_Destroy(utf8Encoding) }
		
		guard let textBytes = System_Text_Encoding_GetBytes_3(utf8Encoding,
															  textDN,
															  &exception),
			  exception == nil else {
			XCTFail("System.Text.Encoding.GetBytes should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(textBytes) }
		
		guard let textAsBase64StringDN = System_Convert_ToBase64String(textBytes,
																	   &exception),
			  exception == nil else {
			XCTFail("System.Convert.ToBase64String should not throw and return an instance of a C String")
			
			return
		}
		
		defer { System_String_Destroy(textAsBase64StringDN) }
		
		guard let textAsBase64String = String(cDotNETString: textAsBase64StringDN) else {
			XCTFail("Failed to convert string")
			
			return
		}
		
		guard let textAsBase64Data = textAsBase64String.data(using: .utf8) else {
			XCTFail("Failed to convert base64 string to data")
			
			return
		}
		
		guard let textData = Data(base64Encoded: textAsBase64Data) else {
			XCTFail("Failed to decode base64 encoded data")
			
			return
		}
		
		guard let decodedText = String(data: textData, encoding: .utf8) else {
			XCTFail("Failed to decode text data to string")
			
			return
		}
		
		XCTAssertEqual(text, decodedText)
		
		guard let textBytesRet = System_Convert_FromBase64String(textAsBase64StringDN,
																 &exception),
			  exception == nil else {
			XCTFail("System.Convert.FromBase64String should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(textBytesRet) }
		
		guard let textRet = String(cDotNETString: System_Text_Encoding_GetString(utf8Encoding,
																				textBytesRet,
																				&exception),
								   destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Text.Encoding.GetString should not throw and return an instance of a C String")
			
			return
		}
		
		XCTAssertEqual(text, textRet)
	}
}
