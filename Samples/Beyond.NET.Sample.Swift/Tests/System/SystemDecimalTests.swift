import XCTest
import BeyondDotNETSampleKit

final class SystemDecimalTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testDecimalParse() throws {
        let number = 1234
        let numberString = "\(number)"
        let numberStringDN = numberString.dotNETString()
        
        var decimal = System_Decimal.minValue
        
        guard try System_Decimal.tryParse(numberStringDN,
                                          &decimal) else {
            XCTFail("System.Decimal.TryParse should not throw, return true and an instance as out parameter")
            
            return
        }
        
        let numberRet = try System_Decimal.toInt64(decimal)
        XCTAssertEqual(number, .init(numberRet))
        
        let numberStringRet = try decimal.toString().string()
        XCTAssertEqual(numberString, numberStringRet)
    }
    
    func testDecimalCalculations() throws {
        let number1: UInt64 = 123
        let number2: UInt64 = 321
        
        let addResult = number1 + number2
        let subtractResult = number2 - number1
        let multiplyResult = number1 * number2
        let divideResult = number2 / number1
        
        let decimal1 = try System_Decimal(number1)
        let decimal2 = try System_Decimal(number2)
        
        let addResultDecimal = try System_Decimal.add(decimal1,
                                                      decimal2)
        
        let addResultRet = try System_Decimal.toUInt64(addResultDecimal)
        XCTAssertEqual(addResult, addResultRet)
        
        let subtractResultDecimal = try System_Decimal.subtract(decimal2,
                                                                decimal1)
        
        let subtractResultRet = try System_Decimal.toUInt64(subtractResultDecimal)
        XCTAssertEqual(subtractResult, subtractResultRet)
        
        let multiplyResultDecimal = try System_Decimal.multiply(decimal1,
                                                                decimal2)
        
        let multiplyResultRet = try System_Decimal.toUInt64(multiplyResultDecimal)
        XCTAssertEqual(multiplyResult, multiplyResultRet)
        
        let divideResultDecimal = try System_Decimal.divide(decimal2,
                                                             decimal1)
        
        let divideResultRet = try System_Decimal.toUInt64(divideResultDecimal)
        XCTAssertEqual(divideResult, divideResultRet)
    }
    
    func testDivisionByZero() throws {
        let decimalZero = System_Decimal.zero
        
        let number1: Int32 = 123
        
        let decimal1 = try System_Decimal(number1)
        
        do {
            let _ = try System_Decimal.divide(decimal1,
                                              decimalZero)
            
            XCTFail("System.Decimal.Divide should throw when dividing by zero and not return a value")
        } catch {
            guard error is DNError else {
                XCTFail("Error should be of DNError type")
                
                return
            }
            
            let errorMessage = error.localizedDescription
            
            XCTAssertTrue(errorMessage.contains("divide by zero"))
        }
    }
}
