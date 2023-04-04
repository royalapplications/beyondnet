import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemDecimalTests: XCTestCase {
	func testDecimalParse() {
		var exception: System_Exception_t?
		
		let number = 1234
		let numberString = "\(number)"
		
		var decimal: System_Decimal_t?
		
		let parseSuccess = System_Decimal_TryParse(numberString,
												   &decimal,
												   &exception)
		
		guard let decimal,
			  exception == nil,
			  parseSuccess else {
			XCTFail("System.Decimal.TryParse should not throw, return true and an instance as out parameter")
			
			return
		}
		
		defer { System_Decimal_Destroy(decimal) }
		
		let numberRet = System_Decimal_ToInt64(decimal,
											   &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(number, .init(numberRet))
		
		guard let numberStringRetC = System_Decimal_ToString(decimal,
															 &exception),
			  exception == nil else {
			XCTFail("System.Decimal.ToString should not throw and return an instance of a C String")
			
			return
		}
		
		defer { numberStringRetC.deallocate() }
		
		let numberStringRet = String(cString: numberStringRetC)
		
		XCTAssertEqual(numberString, numberStringRet)
	}
}
