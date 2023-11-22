import XCTest
import BeyondDotNETSampleKit

final class AsyncTestTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testAddAsync() {
        let number1: Int32 = 5
        let number2: Int32 = 10
        let expectedResult = number1 + number2
        
        guard let asyncTests = try? Beyond.NET.Sample.AsyncTests() else {
            XCTFail("Beyond.NET.Sample.AsyncTests ctor should not throw and return an instance")
            
            return
        }
        
        guard let task = try? asyncTests.addAsync(number1, number2) else {
            XCTFail("Beyond.NET.Sample.AsyncTests.AddAsync should not throw and return an instance of a task")
            
            return
        }
        
        do {
            try task.wait()
        } catch {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw")
            
            return
        }
        
        guard let result = try? task.result(TResult: System.Int32.typeOf),
              let unboxedResult = try? result.castToInt32() else {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(unboxedResult, expectedResult)
    }
    
    func testTransformNumbersAsync() {
        let number1: Int32 = 5
        let number2: Int32 = 10
        
        func multiplierFunc(inputNumber1: Int32,
                            inputNumber2: Int32) -> Int32 {
            inputNumber1 * inputNumber2
        }
        
        let expectedResult = multiplierFunc(inputNumber1: number1,
                                            inputNumber2: number2)
        
        guard let asyncTests = try? Beyond.NET.Sample.AsyncTests() else {
            XCTFail("Beyond.NET.Sample.AsyncTests ctor should not throw and return an instance")
            
            return
        }
        
        let transformerDelegate = Beyond.NET.Sample.AsyncTests_TransformerDelegate(multiplierFunc)
        
        guard let task = try? asyncTests.transformNumbersAsync(number1, number2, transformerDelegate) else {
            XCTFail("Beyond.NET.Sample.AsyncTests.TransformNumbersAsync should not throw and return an instance of a task")
            
            return
        }
        
        do {
            try task.wait()
        } catch {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw")
            
            return
        }
        
        guard let result = try? task.result(TResult: System.Int32.typeOf),
              let unboxedResult = try? result.castToInt32() else {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(unboxedResult, expectedResult)
    }
}
