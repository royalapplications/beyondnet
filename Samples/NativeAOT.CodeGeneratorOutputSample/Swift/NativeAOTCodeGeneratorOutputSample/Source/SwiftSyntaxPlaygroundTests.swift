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
		
		guard let firstStringRet = try? listOfString.item(0) else {
			XCTFail()
			
			return
		}
		
		XCTAssertTrue(firstString == firstStringRet)
	}
	
//	func testArrayIterator() {
//		let length: Int32 = 2
//		
//		guard let arrayOfStrings = try? System_Array.createInstance(System_String.typeOf,
//																	length) else {
//			XCTFail("System.Array.CreateInstance should not throw and return an instance")
//			
//			return
//		}
//		
//		let strings = [
//			"Hello",
//			"World"
//		]
//		
//		XCTAssertNoThrow(try arrayOfStrings.setValue(strings[0].dotNETString(), 0 as Int32))
//		XCTAssertNoThrow(try arrayOfStrings.setValue(strings[1].dotNETString(), 1 as Int32))
//		
//		for (idx, string) in arrayOfStrings.enumerated() {
//			XCTAssertEqual(string, strings[idx])
//		}
//	}
}
