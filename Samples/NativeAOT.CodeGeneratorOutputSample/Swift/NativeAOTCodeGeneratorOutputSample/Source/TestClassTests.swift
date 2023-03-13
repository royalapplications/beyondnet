import XCTest
import NativeAOTCodeGeneratorOutputSample

final class NativeAOTCodeGeneratorOutputSampleTests: XCTestCase {
    func testExample() throws {
        let testClass: TestClass
        
        do {
            testClass = try TestClass()
        } catch {
            XCTFail("init should not throw")
            
            return
        }
        
        XCTAssertNoThrow(try testClass.sayHello())
        XCTAssertNoThrow(try testClass.sayHello(name: "Felix"))
        
        do {
            let hello = try testClass.getHello()
            
            XCTAssertEqual("Hello", hello)
        } catch {
            XCTFail("getHello should not throw")
            
            return
        }
        
        do {
            let result = try testClass.add(number1: 1,
                                           number2: 2)
            
            XCTAssertEqual(3, result)
        } catch {
            XCTFail("add should not throw")
            
            return
        }
    }
}
