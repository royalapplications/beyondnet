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
	
	func testDecimalCalculations() {
		var exception: System_Exception_t?
		
		let number1: UInt64 = 123
		let number2: UInt64 = 321
		
		let addResult = number1 + number2
		let subtractResult = number2 - number1
		let multiplyResult = number1 * number2
		let divideResult = number2 / number1
		
		guard let decimal1 = System_Decimal_Create3(number1,
													&exception),
			  exception == nil else {
			XCTFail("System.Decimal ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Decimal_Destroy(decimal1) }
		
		guard let decimal2 = System_Decimal_Create3(number2,
													&exception),
			  exception == nil else {
			XCTFail("System.Decimal ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Decimal_Destroy(decimal2) }
		
		guard let addResultDecimal = System_Decimal_Add(decimal1,
														decimal2,
														&exception),
			  exception == nil else {
			XCTFail("System.Decimal.Add should not throw and return an instance")
			
			return
		}
				
		defer { System_Decimal_Destroy(addResultDecimal) }
		
		let addResultRet = System_Decimal_ToUInt64(addResultDecimal,
												   &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(addResult, addResultRet)
		
		guard let subtractResultDecimal = System_Decimal_Subtract(decimal2,
																  decimal1,
																  &exception),
			  exception == nil else {
			XCTFail("System.Decimal.Subtract should not throw and return an instance")
			
			return
		}
				
		defer { System_Decimal_Destroy(subtractResultDecimal) }
		
		let subtractResultRet = System_Decimal_ToUInt64(subtractResultDecimal,
														&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(subtractResult, subtractResultRet)
		
		guard let multiplyResultDecimal = System_Decimal_Multiply(decimal1,
																  decimal2,
																  &exception),
			  exception == nil else {
			XCTFail("System.Decimal.Multiply should not throw and return an instance")
			
			return
		}
				
		defer { System_Decimal_Destroy(multiplyResultDecimal) }
		
		let multiplyResultRet = System_Decimal_ToUInt64(multiplyResultDecimal,
														&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(multiplyResult, multiplyResultRet)
		
		guard let divideResultDecimal = System_Decimal_Divide(decimal2,
															  decimal1,
															  &exception),
			  exception == nil else {
			XCTFail("System.Decimal.Divide should not throw and return an instance")
			
			return
		}
				
		defer { System_Decimal_Destroy(divideResultDecimal) }
		
		let divideResultRet = System_Decimal_ToUInt64(divideResultDecimal,
													  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(divideResult, divideResultRet)
	}
	
	func testDivisionByZero() {
		var exception: System_Exception_t?
		
		guard let decimalZero = System_Decimal_Zero_Get() else {
			XCTFail("System.Decimal.Zero getter should return an instance")
			
			return
		}
		
		defer { System_Decimal_Destroy(decimalZero) }
		
		let number1: Int32 = 123
		
		guard let decimal1 = System_Decimal_Create(number1,
												   &exception),
			  exception == nil else {
			XCTFail("System.Decimal ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Decimal_Destroy(decimal1) }
		
		let decimalResult = System_Decimal_Divide(decimal1,
												  decimalZero,
												  &exception)
		
		guard decimalResult == nil,
			  let exception else {
			XCTFail("System.Decimal.Divide should throw when dividing by zero and not return a value")
			
			return
		}
		
		defer { System_Exception_Destroy(exception) }
		
		var exception2: System_Exception_t?
		
		guard let exceptionMessageC = System_Exception_Message_Get(exception,
																   &exception2),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { exceptionMessageC.deallocate() }
		
		let exceptionMessage = String(cString: exceptionMessageC)
		
		XCTAssertTrue(exceptionMessage.contains("divide by zero"))
	}
}
