import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemRandomTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testRandom() {
        guard let random = try? System_Random() else {
            XCTFail("System.Random ctor should not throw and return an instance")
            
            return
        }
        
        let minValue: Int32 = 5
        let maxValue: Int32 = 15
        
        for _ in 0..<200 {
            let value: Int32
            
            do {
                value = try random.next(minValue,
                                        maxValue)
            } catch {
                XCTFail("System.Random.Next should not throw")
                
                return
            }
            
            XCTAssertGreaterThanOrEqual(value, minValue)
            XCTAssertLessThan(value, maxValue)
        }
    }
}
