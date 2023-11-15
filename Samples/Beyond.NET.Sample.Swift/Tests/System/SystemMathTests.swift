import XCTest
import BeyondDotNETSampleKit

final class SystemMathTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testMath() {
        do {
            let result = try System_Math.abs(Int64(-599))
            XCTAssertEqual(599, result)
        } catch {
            XCTFail("System.Math.Abs should not throw")
            
            return
        }
        
        do {
            let result = try System_Math.abs(-599.995)
            XCTAssertEqual(599.995, result)
        } catch {
            XCTFail("System.Math.Abs should not throw")
            
            return
        }
        
        do {
            let result = try System_Math.ceiling(-7.6)
            XCTAssertEqual(-7.0, result)
        } catch {
            XCTFail("System.Math.Ceiling should not throw")
            
            return
        }
        
        do {
            let result = try System_Math.floor(0.12)
            XCTAssertEqual(0.0, result)
        } catch {
            XCTFail("System.Math.Floor should not throw")
            
            return
        }
        
        do {
            let result = try System_Math.clamp(Int64(500), Int64(0), Int64(100))
            XCTAssertEqual(100, result)
        } catch {
            XCTFail("System.Math.Clamp should not throw")
            
            return
        }
    }
}
