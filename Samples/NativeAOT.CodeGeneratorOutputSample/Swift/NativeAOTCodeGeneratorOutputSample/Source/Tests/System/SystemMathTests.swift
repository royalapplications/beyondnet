import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemMathTests: XCTestCase {
	func testMath() {
		var exception: System_Exception_t?
		
		XCTAssertEqual(599, System_Math_Abs(-599, &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(599.995, System_Math_Abs6(-599.995, &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(-7.0, System_Math_Ceiling(-7.6, &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(0.0, System_Math_Floor(0.12, &exception))
		XCTAssertNil(exception)
		
		XCTAssertEqual(100, System_Math_Clamp4(500, 0, 100, &exception))
		XCTAssertNil(exception)
	}
}
