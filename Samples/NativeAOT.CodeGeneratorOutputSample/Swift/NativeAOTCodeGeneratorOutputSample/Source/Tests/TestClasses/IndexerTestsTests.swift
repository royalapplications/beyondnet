import XCTest
import NativeAOTCodeGeneratorOutputSample

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
		var exception: System_Exception_t?
		
		guard let indexerTests = NativeAOT_CodeGeneratorInputSample_IndexerTests_Create(&exception),
			  exception == nil else {
			XCTFail("IndexerTests ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_IndexerTests_Destroy(indexerTests) }
		
		let aString = "A String"
		let aStringDN = aString.cDotNETString()
		defer { System_String_Destroy(aStringDN) }
		
		let aNumber: Int32 = 123456
		
		guard let aGuid = System_Guid_NewGuid(&exception),
			  exception == nil else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(aGuid) }
		
		guard let arrayRet = NativeAOT_CodeGeneratorInputSample_IndexerTests_Item_Get(indexerTests,
																					  aStringDN,
																					  aNumber,
																					  aGuid,
																					  &exception),
			  exception == nil else {
			XCTFail("IndexerTests[] should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(arrayRet) }
		
		let arrayLength = System_Array_Length_Get(arrayRet,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(3, arrayLength)
		
		guard let item1Ret = System_Array_GetValue_1(arrayRet,
													 0,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let item1RetAsString = String(cDotNETString: item1Ret,
									  destroyDotNETString: true)
		
		XCTAssertEqual(aString, item1RetAsString)
		
		guard let item2Ret = System_Array_GetValue_1(arrayRet,
													 1,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(item2Ret) }
		
		let item2RetAsInt32 = DNObjectCastToInt32(item2Ret,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		guard let item3Ret = System_Array_GetValue_1(arrayRet,
													 2,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(item3Ret) }
		
		let guidEqual = System_Object_Equals(aGuid,
											 item3Ret,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(guidEqual)
	}
	
	func testSetIndexedPropertyWith3Parameters() {
		var exception: System_Exception_t?
		
		guard let indexerTests = NativeAOT_CodeGeneratorInputSample_IndexerTests_Create(&exception),
			  exception == nil else {
			XCTFail("IndexerTests ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_IndexerTests_Destroy(indexerTests) }
		
		let aString = "A String"
		let aStringDN = aString.cDotNETString()
		defer { System_String_Destroy(aStringDN) }
		
		let aNumber: Int32 = 123456
		
		guard let aGuid = System_Guid_NewGuid(&exception),
			  exception == nil else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(aGuid) }
		
		let systemObjectType = System_Object_TypeOf()
		defer { System_Type_Destroy(systemObjectType) }
		
		guard let array = System_Array_CreateInstance(systemObjectType,
													  3,
													  &exception),
			  exception == nil else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(array) }
		
		System_Array_SetValue(array,
							  aStringDN,
							  0,
							  &exception)
		
		XCTAssertNil(exception)
		
		let aNumberDN = DNObjectFromInt32(aNumber)
		defer { System_Object_Destroy(aNumberDN) }
		
		System_Array_SetValue(array,
							  aNumberDN,
							  1,
							  &exception)
		
		XCTAssertNil(exception)
		
		System_Array_SetValue(array,
							  aGuid,
							  2,
							  &exception)
		
		XCTAssertNil(exception)
		
		NativeAOT_CodeGeneratorInputSample_IndexerTests_Item_Set(indexerTests,
																 aStringDN,
																 aNumber,
																 aGuid,
																 array,
																 &exception)
		
		XCTAssertNil(exception)
		
		guard let arrayRet = NativeAOT_CodeGeneratorInputSample_IndexerTests_StoredValue_Get(indexerTests,
																							 &exception),
			  exception == nil else {
			XCTFail("IndexerTests.StoredValue getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(arrayRet) }
		
		let arrayLength = System_Array_Length_Get(arrayRet,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(3, arrayLength)
		
		guard let item1Ret = System_Array_GetValue_1(arrayRet,
													 0,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let item1RetAsString = String(cDotNETString: item1Ret,
									  destroyDotNETString: true)
		
		XCTAssertEqual(aString, item1RetAsString)
		
		guard let storedString = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_IndexerTests_StoredString_Get(indexerTests,
																														&exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("IndexerTests.StoredString getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(aString, storedString)
		
		guard let item2Ret = System_Array_GetValue_1(arrayRet,
													 1,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(item2Ret) }
		
		let item2RetAsInt32 = DNObjectCastToInt32(item2Ret,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(aNumber, item2RetAsInt32)
		
		let storedNumber = NativeAOT_CodeGeneratorInputSample_IndexerTests_StoredNumber_Get(indexerTests,
																							&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(aNumber, storedNumber)
		
		guard let item3Ret = System_Array_GetValue_1(arrayRet,
													 2,
													 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(item3Ret) }
		
		let guidEqual = System_Object_Equals(aGuid,
											 item3Ret,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(guidEqual)
		
		guard let storedGuid = NativeAOT_CodeGeneratorInputSample_IndexerTests_StoredGuid_Get(indexerTests,
																							  &exception),
			  exception == nil else {
			XCTFail("IndexerTests.StoredGuid getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(storedGuid) }
		
		let storedGuidEqual = System_Object_Equals(aGuid,
												   storedGuid,
												   &exception)
		   
		XCTAssertNil(exception)
		XCTAssertTrue(storedGuidEqual)
	}
}
