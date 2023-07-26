import XCTest
import BeyondDotNETSampleNative

final class SystemDecimalTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testDecimalParse() {
        let number = 1234
        let numberString = "\(number)"
        let numberStringDN = numberString.dotNETString()
        
        var decimal: System_Decimal?
        
        let parseSuccess = (try? System_Decimal.tryParse(numberStringDN,
                                                         &decimal)) ?? false
        
        guard let decimal,
              parseSuccess else {
            XCTFail("System.Decimal.TryParse should not throw, return true and an instance as out parameter")
            
            return
        }
        
        do {
            let numberRet = try System_Decimal.toInt64(decimal)
            XCTAssertEqual(number, .init(numberRet))
        } catch {
            XCTFail("System.Decimal.ToInt64 should not throw")
            
            return
        }
        
        guard let numberStringRet = try? decimal.toString()?.string() else {
            XCTFail("System.Decimal.ToString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(numberString, numberStringRet)
    }
    
    func testDecimalCalculations() {
        let number1: UInt64 = 123
        let number2: UInt64 = 321
        
        let addResult = number1 + number2
        let subtractResult = number2 - number1
        let multiplyResult = number1 * number2
        let divideResult = number2 / number1
        
        guard let decimal1 = try? System_Decimal(number1) else {
            XCTFail("System.Decimal ctor should not throw and return an instance")
            
            return
        }
        
        guard let decimal2 = try? System_Decimal(number2) else {
            XCTFail("System.Decimal ctor should not throw and return an instance")
            
            return
        }
        
        guard let addResultDecimal = try? System_Decimal.add(decimal1,
                                                             decimal2) else {
            XCTFail("System.Decimal.Add should not throw and return an instance")
            
            return
        }
        
        do {
            let addResultRet = try System_Decimal.toUInt64(addResultDecimal)
            XCTAssertEqual(addResult, addResultRet)
        } catch {
            XCTFail("System.Decimal.ToUInt64 should not throw")
            
            return
        }
        
        guard let subtractResultDecimal = try? System_Decimal.subtract(decimal2,
                                                                       decimal1) else {
            XCTFail("System.Decimal.Subtract should not throw and return an instance")
            
            return
        }
        
        do {
            let subtractResultRet = try System_Decimal.toUInt64(subtractResultDecimal)
            XCTAssertEqual(subtractResult, subtractResultRet)
        } catch {
            XCTFail("System.Decimal.ToUInt64 should not throw")
            
            return
        }
        
        guard let multiplyResultDecimal = try? System_Decimal.multiply(decimal1,
                                                                       decimal2) else {
            XCTFail("System.Decimal.Multiply should not throw and return an instance")
            
            return
        }
        
        do {
            let multiplyResultRet = try System_Decimal.toUInt64(multiplyResultDecimal)
            XCTAssertEqual(multiplyResult, multiplyResultRet)
        } catch {
            XCTFail("System.Decimal.ToUInt64 should not throw")
            
            return
        }
        
        guard let divideResultDecimal = try? System_Decimal.divide(decimal2,
                                                                   decimal1) else {
            XCTFail("System.Decimal.Divide should not throw and return an instance")
            
            return
        }
        
        do {
            let divideResultRet = try System_Decimal.toUInt64(divideResultDecimal)
            XCTAssertEqual(divideResult, divideResultRet)
        } catch {
            XCTFail("System.Decimal.ToUInt64 should not throw")
            
            return
        }
    }
    
    func testDivisionByZero() {
        guard let decimalZero = System_Decimal.zero else {
            XCTFail("System.Decimal.Zero getter should return an instance")
            
            return
        }
        
        let number1: Int32 = 123
        
        guard let decimal1 = try? System_Decimal(number1) else {
            XCTFail("System.Decimal ctor should not throw and return an instance")
            
            return
        }
        
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
