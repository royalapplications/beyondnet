import XCTest
import NativeAOTCodeGeneratorOutputSample

final class GenericTestsTests: XCTestCase {
	func testGenericReferenceTypeThroughReflection() {
		var exception: System_Exception_t?
		
		guard let genericType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
	 
		guard let typeRet = NativeAOT_CodeGeneratorInputSample_GenericTests_CallReturnGenericTypeThroughReflection(genericType,
																												   &exception),
			  exception == nil else {
			if let exceptionAsStringC = System_Exception_ToString(exception, nil) {
				defer { exceptionAsStringC.deallocate() }
				
				let exceptionAsString = String(cString: exceptionAsStringC)
				
				print(exceptionAsString)
			}
			
			XCTFail("CallReturnGenericTypeThroughReflection should not throw and return an instance")
			
			return
		}
		
		let typesEqual = System_Object_Equals(genericType,
											  typeRet,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(typesEqual)
	}
	
	// Currently, value types as generic parameters aren't supported through reflection when compiling with NativeAOT
	func testGenericValueTypeThroughReflection() {
		var exception: System_Exception_t?
		
		guard let genericType = System_Guid_TypeOf() else {
			XCTFail("typeof(System.Guid) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
	 
		let typeRet = NativeAOT_CodeGeneratorInputSample_GenericTests_CallReturnGenericTypeThroughReflection(genericType,
																											 &exception)
		
		XCTAssertNotNil(exception)
		
		System_Exception_Destroy(exception)
		
		if let typeRet {
			System_Type_Destroy(typeRet)
		}
	}
}
