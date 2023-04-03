import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemConvertTests: XCTestCase {
	func testBooleanConversion() {
		var exception: System_Exception_t?
		
		XCTAssertTrue(System_Convert_ToBoolean12("true", &exception))
		XCTAssertNil(exception)
		
		XCTAssertFalse(System_Convert_ToBoolean12("false", &exception))
		XCTAssertNil(exception)
		
		XCTAssertFalse(System_Convert_ToBoolean12("nonsense", &exception))
		XCTAssertNotNil(exception)
	}
	
	func testIntegerConversion() {
		var exception: System_Exception_t?
		
		XCTAssertEqual(123456789,
					   System_Convert_ToInt3215("123456789", &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(-123456789,
					   System_Convert_ToInt6415("-123456789", &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(-1,
					   System_Convert_ToInt6415("nonsense", &exception))
		XCTAssertNotNil(exception)
		
		XCTAssertEqual(0,
					   System_Convert_ToUInt6415("nonsense", &exception))
		XCTAssertNotNil(exception)
	}
	
	func testBase64Conversion() {
		var exception: System_Exception_t?
		
		let text = "Hello World!"
		
		guard let utf8Encoding = System_Text_Encoding_UTF8_Get(&exception),
			  exception == nil else {
			XCTFail("System.Text.Encoding.UTF8 getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_Encoding_Destroy(utf8Encoding) }
		
		guard let textBytes = System_Text_Encoding_GetBytes3(utf8Encoding,
															 text,
															 &exception),
			  exception == nil else {
			XCTFail("System.Text.Encoding.GetBytes should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(textBytes) }
		
		guard let textAsBase64StringC = System_Convert_ToBase64String(textBytes,
																	  &exception),
			  exception == nil else {
			XCTFail("System.Convert.ToBase64String should not throw and return an instance of a C String")
			
			return
		}
		
		defer { textAsBase64StringC.deallocate() }
		
		let textAsBase64String = String(cString: textAsBase64StringC)
		
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
		
		guard let textBytesRet = System_Convert_FromBase64String(textAsBase64String,
																 &exception),
			  exception == nil else {
			XCTFail("System.Convert.FromBase64String should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(textBytesRet) }
		
		guard let textRetC = System_Text_Encoding_GetString(utf8Encoding,
															textBytesRet,
															&exception),
			  exception == nil else {
			XCTFail("System.Text.Encoding.GetString should not throw and return an instance of a C String")
			
			return
		}
		
		defer { textRetC.deallocate() }
		
		let textRet = String(cString: textRetC)
		
		XCTAssertEqual(text, textRet)
	}
}
