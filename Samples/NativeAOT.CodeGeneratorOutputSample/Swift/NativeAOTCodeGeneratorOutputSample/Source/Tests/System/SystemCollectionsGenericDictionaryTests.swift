import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemCollectionsGenericDictionaryTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testCreate() {
		var exception: System_Exception_t?
		
		guard let systemStringType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemStringType) }
		
		guard let systemExceptionType = System_Exception_TypeOf() else {
			XCTFail("typeof(System.Exception) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemExceptionType) }
		
		guard let dictionary = System_Collections_Generic_Dictionary_A2_Create(systemStringType,
																			   systemExceptionType,
																			   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Collections_Generic_Dictionary_A2_Destroy(dictionary) }
		
		guard let dictionaryType = System_Object_GetType(dictionary,
															&exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(dictionaryType) }
		
		guard let dictionaryTypeName = String(dotNETString: System_Type_FullName_Get(dictionaryType,
																					 &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(dictionaryTypeName.contains("System.Collections.Generic.Dictionary`2[[System.String"))
		XCTAssertTrue(dictionaryTypeName.contains(",[System.Exception"))
	}
	
	// TODO
//	func testUse() {
//		var exception: System_Exception_t?
//
//		guard let systemStringType = System_String_TypeOf() else {
//			XCTFail("typeof(System.String) should return an instance")
//
//			return
//		}
//
//		defer { System_Type_Destroy(systemStringType) }
//
//		guard let systemExceptionType = System_Exception_TypeOf() else {
//			XCTFail("typeof(System.Exception) should return an instance")
//
//			return
//		}
//
//		defer { System_Type_Destroy(systemExceptionType) }
//
//		guard let dictionary = System_Collections_Generic_Dictionary_A2_Create(systemStringType,
//																			   systemExceptionType,
//																			   &exception),
//			  exception == nil else {
//			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")
//
//			return
//		}
//
//		defer { System_Collections_Generic_Dictionary_A2_Destroy(dictionary) }
//
//		let stringExceptionMapping: [System_String_t: System_Exception_t] = [
//
//		]
//	}
}
