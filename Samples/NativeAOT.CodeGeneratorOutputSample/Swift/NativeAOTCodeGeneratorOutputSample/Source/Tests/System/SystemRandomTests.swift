import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemRandomTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.gcCollect()
	}
	
	@MainActor
	override class func tearDown() {
		Self.gcCollect()
	}
	
	func testRandom() {
		var exception: System_Exception_t?
		
		guard let random = System_Random_Create(&exception),
			  exception == nil else {
			XCTFail("System.Random ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Random_Destroy(random) }
		
		let minValue: Int32 = 5
		let maxValue: Int32 = 15
		
		for _ in 0..<200 {
			let value = System_Random_Next_2(random,
											 minValue,
											 maxValue,
											 &exception)
			
			XCTAssertNil(exception)
			
			XCTAssertGreaterThanOrEqual(value, minValue)
			XCTAssertLessThan(value, maxValue)
		}
	}
}
