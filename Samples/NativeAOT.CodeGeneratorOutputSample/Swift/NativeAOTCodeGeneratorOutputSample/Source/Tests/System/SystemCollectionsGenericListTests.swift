import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemCollectionsGenericListTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testTypeOf() {
		var exception: System_Exception_t?
		
		let systemTypeType = System_Type_TypeOf()
		defer { System_Type_Destroy(systemTypeType) }
		
		let type = System_Collections_Generic_List_A1_TypeOf()
		defer { System_Type_Destroy(type) }
		
		let isGenericType = System_Type_IsGenericType_Get(type,
														  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(isGenericType)
		
		let isConstructedGenericType = System_Type_IsConstructedGenericType_Get(type,
																				&exception)
		
		XCTAssertNil(exception)
		XCTAssertFalse(isConstructedGenericType)
		
		guard let genericArguments = System_Type_GetGenericArguments(type,
																	 &exception),
			  exception == nil else {
			XCTFail("System.Type.GetGenericArguments should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(genericArguments) }
		
		let numberOfGenericArguments = System_Array_Length_Get(genericArguments,
															   &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(1, numberOfGenericArguments)
		
		guard let genericArgument = System_Array_GetValue_1(genericArguments,
															0,
															&exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(genericArgument) }
		
		XCTAssertTrue(DNObjectIs(genericArgument, systemTypeType))
		
		guard let genericArgumentTypeName = String(cDotNETString: System_Reflection_MemberInfo_Name_Get(genericArgument,
																									   &exception),
												   destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Reflection.MemberInfo.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual("T", genericArgumentTypeName)
	}
	
	func testCreate() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		guard let list = System_Collections_Generic_List_A1_Create(systemStringType,
																   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Collections_Generic_List_A1_Destroy(list) }
		
		guard let listType = System_Object_GetType(list,
												   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(listType) }
		
		guard let listTypeName = String(cDotNETString: System_Type_FullName_Get(listType,
																			   &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(listTypeName.contains("System.Collections.Generic.List`1[[System.String"))
	}
	
	func testUse() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		guard let list = System_Collections_Generic_List_A1_Create(systemStringType,
																   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Collections_Generic_List_A1_Destroy(list) }
		
		let strings = [
			"01. A",
			"02. B",
			"03. C"
		]
		
		for string in strings {
			let stringDN = string.cDotNETString()
			
			System_Collections_Generic_List_A1_Add(list,
												   systemStringType,
												   stringDN,
												   &exception)
			
			System_String_Destroy(stringDN)
			
			if let exception,
			   let exceptionString = String(cDotNETString: System_Exception_ToString(exception, nil),
											destroyDotNETString: true) {
				XCTFail("System.Collections.Generic.List<System.String>.Add should not throw: \(exceptionString)")
				
				return
			}
			
			XCTAssertNil(exception)
		}
		
		let listCount = System_Collections_Generic_List_A1_Count_Get(list,
																	 systemStringType,
																	 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(strings.count, .init(listCount))
		
		guard let array = System_Collections_Generic_List_A1_ToArray(list,
																	 systemStringType,
																	 &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.List<System.String>.ToArray should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(array) }
		
		for idx in 0..<listCount {
			guard let element = System_Array_GetValue_1(array,
														idx,
														&exception),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			XCTAssertTrue(DNObjectIs(element, systemStringType))
			
			guard let elementString = String(cDotNETString: element,
											 destroyDotNETString: true) else {
				XCTFail("Failed to get string")
				
				return
			}
			
			let expectedString = strings[.init(idx)]
			
			XCTAssertEqual(expectedString, elementString)
		}
	}
}
