import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SwiftSyntaxPlaygroundTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testSugaredGenerics() {
		guard let listOfString = try? System_Collections_Generic_List<System_String>() else {
			XCTFail()
			
			return
		}
		
		let firstString = "Hello".dotNETString()
		
		XCTAssertNoThrow(try listOfString.add(firstString))
		
		guard let firstStringRet = try? listOfString.item_get(0) else {
			XCTFail()
			
			return
		}
		
		XCTAssertTrue(firstString == firstStringRet)
	}
}
