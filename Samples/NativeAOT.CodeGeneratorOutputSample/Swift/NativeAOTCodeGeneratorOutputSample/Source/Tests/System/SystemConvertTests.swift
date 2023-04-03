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
}
