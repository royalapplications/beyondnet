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
        
        guard let result = try? task.result(System.Int32.typeOf),
              let unboxedResult = try? result.castToInt32() else {
            XCTFail("System.Threading.Tasks.Task.Wait should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(unboxedResult, expectedResult)
    }
}
