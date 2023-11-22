import XCTest
import BeyondDotNETSampleKit

final class IndexerTestsTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testGetIndexedPropertyWith3Parameters() {
		guard let indexerTests = try? Beyond_NET_Sample_IndexerTests() else {
			XCTFail("IndexerTests ctor should not throw and return an instance")
			
			return
		}
		
		let aString = "A String"
		let aStringDN = aString.dotNETString()
		
		let aNumber: Int32 = 123456
		
		guard let aGuid = try? System_Guid.newGuid() else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
        guard let arrayRet = try? indexerTests.item(aStringDN,
                                                    aNumber,
                                                    aGuid) else {
			XCTFail("IndexerTests[] should not throw and return an instance")
			
			return
		}
		
		let arrayLength = (try? arrayRet.length) ?? -1
		XCTAssertEqual(3, arrayLength)
		
		guard let item1RetAsString = try? arrayRet.getValue(0 as Int32)?.castAs(System_String.self)?.string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, item1RetAsString)
		
		guard let item2RetAsInt32 = try? arrayRet.getValue(1 as Int32)?.castToInt32() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		guard let item3Ret = try? arrayRet.getValue(2 as Int32)?.castTo(System_Guid.self) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let guidEqual = aGuid == item3Ret
		
		XCTAssertTrue(guidEqual)
	}
	
	func testSetIndexedPropertyWith3Parameters() {
		guard let indexerTests = try? Beyond_NET_Sample_IndexerTests() else {
			XCTFail("IndexerTests ctor should not throw and return an instance")
			
			return
		}
		
		let aString = "A String"
		let aStringDN = aString.dotNETString()
		
		let aNumber: Int32 = 123456
		
		guard let aGuid = try? System_Guid.newGuid() else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		let systemObjectType = System_Object.typeOf
		
		guard let array = try? System_Array.createInstance(systemObjectType,
														   3) else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		XCTAssertNoThrow(try array.setValue(aStringDN, 0 as Int32))
		XCTAssertNoThrow(try array.setValue(DNObject.fromInt32(aNumber), 1 as Int32))
		XCTAssertNoThrow(try array.setValue(aGuid, 2 as Int32))
		
		XCTAssertNoThrow(try indexerTests.item_set(aStringDN,
												   aNumber,
												   aGuid,
												   array.castTo()))
		
		guard let arrayRet = try? indexerTests.storedValue else {
			XCTFail("IndexerTests.StoredValue getter should not throw and return an instance")
			
			return
		}
		
		let arrayLength = (try? arrayRet.length) ?? -1
		XCTAssertEqual(3, arrayLength)
		
		guard let item1RetAsString = try? arrayRet.getValue(0 as Int32)?.castTo(System_String.self).string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, item1RetAsString)
		
		guard let storedString = try? indexerTests.storedString.string() else {
			XCTFail("IndexerTests.StoredString getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, storedString)
		
		guard let item2RetAsInt32 = try? arrayRet.getValue(1 as Int32)?.castToInt32() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		let storedNumber = (try? indexerTests.storedNumber) ?? -1
		XCTAssertEqual(aNumber, storedNumber)
		
		guard let item3Ret = try? arrayRet.getValue(2 as Int32)?.castTo(System_Guid.self) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let guidEqual = aGuid == item3Ret
		XCTAssertTrue(guidEqual)
		
		guard let storedGuid = try? indexerTests.storedGuid else {
			XCTFail("IndexerTests.StoredGuid getter should not throw and return an instance")
			
			return
		}
		
		let storedGuidEqual = aGuid == storedGuid
		XCTAssertTrue(storedGuidEqual)
	}
}
