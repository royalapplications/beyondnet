import XCTest
import BeyondDotNETSampleKit

final class IndexerTestsTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testGetIndexedPropertyWith3Parameters() throws {
		let indexerTests = try Beyond_NET_Sample_IndexerTests()
		
		let aString = "A String"
		let aStringDN = aString.dotNETString()
		
		let aNumber: Int32 = 123456
		
		let aGuid = try System_Guid.newGuid()
		
        let arrayRet = try indexerTests.item(aStringDN,
                                             aNumber,
                                             aGuid)
		
		let arrayLength = try arrayRet.length
		XCTAssertEqual(3, arrayLength)
		
		guard let item1RetAsString = try arrayRet.getValue(0 as Int32)?.castAs(System_String.self)?.string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, item1RetAsString)
		
		guard let item2RetAsInt32 = try arrayRet.getValue(1 as Int32)?.castToInt32() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		guard let item3Ret = try arrayRet.getValue(2 as Int32)?.castTo(System_Guid.self) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let guidEqual = aGuid == item3Ret
		
		XCTAssertTrue(guidEqual)
	}
	
	func testSetIndexedPropertyWith3Parameters() throws {
		let indexerTests = try Beyond_NET_Sample_IndexerTests()
		
		let aString = "A String"
		let aStringDN = aString.dotNETString()
		
		let aNumber: Int32 = 123456
		
		let aGuid = try System_Guid.newGuid()
		
        let array = try DNArray<System_Object>(length: 3)
		
		try array.setValue(aStringDN, 0 as Int32)
		try array.setValue(DNObject.fromInt32(aNumber), 1 as Int32)
		try array.setValue(aGuid, 2 as Int32)
        
        try indexerTests.item_set(aStringDN,
                                  aNumber,
                                  aGuid,
                                  array.castTo())
		
		let arrayRet = try indexerTests.storedValue
		let arrayLength = try arrayRet.length
		XCTAssertEqual(3, arrayLength)
		
		guard let item1RetAsString = try arrayRet.getValue(0 as Int32)?.castTo(System_String.self).string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, item1RetAsString)
		
		let storedString = try indexerTests.storedString.string()
		XCTAssertEqual(aString, storedString)
		
		guard let item2RetAsInt32 = try arrayRet.getValue(1 as Int32)?.castToInt32() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		let storedNumber = try indexerTests.storedNumber
		XCTAssertEqual(aNumber, storedNumber)
		
		guard let item3Ret = try arrayRet.getValue(2 as Int32)?.castTo(System_Guid.self) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let guidEqual = aGuid == item3Ret
		XCTAssertTrue(guidEqual)
		
		let storedGuid = try indexerTests.storedGuid
		let storedGuidEqual = aGuid == storedGuid
		XCTAssertTrue(storedGuidEqual)
	}
}
