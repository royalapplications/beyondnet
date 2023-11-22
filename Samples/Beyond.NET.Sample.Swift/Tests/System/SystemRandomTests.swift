import XCTest
import BeyondDotNETSampleKit

final class SystemRandomTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testRandom() throws {
        let random = try System_Random()
        
        let minValue: Int32 = 5
        let maxValue: Int32 = 15
        
        for _ in 0..<200 {
            let value: Int32
            
            value = try random.next(minValue,
                                    maxValue)
            
            XCTAssertGreaterThanOrEqual(value, minValue)
            XCTAssertLessThan(value, maxValue)
        }
    }
}
