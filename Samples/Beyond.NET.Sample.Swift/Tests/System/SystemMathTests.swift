import XCTest
import BeyondDotNETSampleKit

final class SystemMathTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testMath() throws {
        XCTAssertEqual(599, try System_Math.abs(Int64(-599)))
        XCTAssertEqual(599.995, try System_Math.abs(-599.995))
        XCTAssertEqual(-7.0, try System_Math.ceiling(-7.6))
        XCTAssertEqual(0.0, try System_Math.floor(0.12))
        XCTAssertEqual(100, try System_Math.clamp(Int64(500), Int64(0), Int64(100)))
    }
}
