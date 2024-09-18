import XCTest
import BeyondDotNETSampleKit

final class AsyncTestTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testAddAsync() throws {
        let number1: Int32 = 5
        let number2: Int32 = 10
        let expectedResult = number1 + number2
        
        let asyncTests = try Beyond.NET.Sample.AsyncTests()
        
        let task = try asyncTests.addAsync(number1, number2)
        
        try task.wait()
        
        guard let result = try task.result(TResult: System.Int32.typeOf),
              let unboxedResult = try? result.castToInt32() else {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(unboxedResult, expectedResult)
    }
    
    func testTransformNumbersAsync() throws {
        let number1: Int32 = 5
        let number2: Int32 = 10
        
        func multiplierFunc(inputNumber1: Int32,
                            inputNumber2: Int32) -> Int32 {
            inputNumber1 * inputNumber2
        }
        
        let expectedResult = multiplierFunc(inputNumber1: number1,
                                            inputNumber2: number2)
        
        let asyncTests = try Beyond.NET.Sample.AsyncTests()
        
        let transformerDelegate = Beyond.NET.Sample.AsyncTests_TransformerDelegate(multiplierFunc)
        
        let task = try asyncTests.transformNumbersAsync(number1,
                                                        number2,
                                                        transformerDelegate)
        
        try task.wait()
        
        guard let result = try task.result(TResult: System.Int32.typeOf),
              let unboxedResult = try? result.castToInt32() else {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(unboxedResult, expectedResult)
    }
}
