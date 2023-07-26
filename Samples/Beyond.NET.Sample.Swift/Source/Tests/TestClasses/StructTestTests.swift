import XCTest
import BeyondDotNETSampleNative

final class StructTestTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testStructTest() {
		let nameOrig = "Joe"
		let nameNew = "John"
		
		guard let structTest = try? Beyond_NET_Sample_StructTest(nameOrig.dotNETString()) else {
			XCTFail("StructTest ctor should not throw and return an instance")
			
			return
		}
		
		guard let nameOrigRet = try? structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameOrig, nameOrigRet)
		
		XCTAssertNoThrow(try structTest.name_set(nameNew.dotNETString()))
		
		guard let nameNewRet = try? structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameNew, nameNewRet)
	}
}
