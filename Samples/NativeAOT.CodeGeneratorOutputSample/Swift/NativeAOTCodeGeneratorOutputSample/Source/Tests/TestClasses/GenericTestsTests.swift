import XCTest
import NativeAOTCodeGeneratorOutputSample

final class GenericTestsTests: XCTestCase {
	func testGenericMethodCallWithReferenceType() {
		var exception: System_Exception_t?
		
		guard let genericType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
		
		guard let typeRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericType_A1(genericType,
																								 &exception),
			  exception == nil else {
			XCTFail("ReturnGenericType<System.String> should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(typeRet) }
		
		let typesEqual = System_Object_Equals(genericType,
											  typeRet,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(typesEqual)
	}
	
	// Currently, value types as generic parameters aren't supported through reflection when compiling with NativeAOT
	// It's possible to specifically include these types with a Rd.xml
	// https://github.com/dotnet/runtime/blob/main/src/coreclr/nativeaot/docs/rd-xml-format.md
	func testGenericMethodCallWithValueType() {
		var exception: System_Exception_t?
		
		guard let genericType = System_Guid_TypeOf() else {
			XCTFail("typeof(System.Guid) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
		
		
		let typeRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericType_A1(genericType,
																						   &exception)
		
		XCTAssertNotNil(exception)
		
		System_Exception_Destroy(exception)
		
		if let typeRet {
			System_Type_Destroy(typeRet)
		}
	}
	
	func testGenericMethodCallWithMultipleGenericParameters() {
		var exception: System_Exception_t?
		
		guard let genericType1 = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType1) }
		
		guard let genericType2 = System_Array_TypeOf() else {
			XCTFail("typeof(System.Array) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType2) }
		
		guard let typesArrayRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericTypes_A2(genericType1,
																										genericType2,
																										&exception),
			  exception == nil else {
			XCTFail("ReturnGenericTypes<System.String, System.Array> should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(typesArrayRet) }
		
		let typesLength = System_Array_Length_Get(typesArrayRet,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(2, .init(typesLength))
		
		for idx in 0..<typesLength {
			guard let type = System_Array_GetValue_1(typesArrayRet,
													 idx,
													 &exception),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			defer { System_Type_Destroy(type) }
			
			let typeToCompareTo: System_Type_t
			
			switch idx {
				case 0:
					typeToCompareTo = genericType1
				case 1:
					typeToCompareTo = genericType2
				default:
					XCTFail("Unknown index")
					
					return
			}
			
			let typesEqual = System_Object_Equals(type,
												  typeToCompareTo,
												  &exception)
			
			XCTAssertNil(exception)
			XCTAssertTrue(typesEqual)
		}
	}
	
	func testReturnDefaultValueOfGenericType() {
		var exception: System_Exception_t?
		
		guard let genericType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
		
		let defaultValueRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnDefaultValueOfGenericType_A1(genericType,
																												 &exception)
		
		XCTAssertNil(exception)
		XCTAssertNil(defaultValueRet)
	}
	
	func testReturnArrayOfDefaultValuesOfGenericType() {
		var exception: System_Exception_t?
		
		guard let genericType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericType) }
		
		let numberOfElements: Int32 = 10
		
		guard let defaultValuesArrayRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnArrayOfDefaultValuesOfGenericType_A1(genericType,
																																	 numberOfElements,
																																	 &exception),
			  exception == nil else {
			XCTFail("ReturnArrayOfDefaultValuesOfGenericType should not throw and return an instance")
			
			return
		}
				
		defer { System_Array_Destroy(defaultValuesArrayRet) }
		
		let length = System_Array_Length_Get(defaultValuesArrayRet,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(numberOfElements, length)
		
		for i in 0..<length {
			let defaultValue = System_Array_GetValue_1(defaultValuesArrayRet,
													   i,
													   &exception)
			
			XCTAssertNil(exception)
			XCTAssertNil(defaultValue)
		}
	}
	
	func testReturnArrayOfRepeatedValues() {
		var exception: System_Exception_t?

		let numberOfElements: Int32 = 100

		guard let value = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should not throw and return an instance")

			return
		}

		defer { System_Object_Destroy(value) }

		guard let systemObjectType = System_Object_GetType(value,
														   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")

			return
		}

		defer { System_Type_Destroy(systemObjectType) }

		guard let genericTests = NativeAOT_CodeGeneratorInputSample_GenericTests_Create(&exception),
			  exception == nil else {
			XCTFail("GenericTests ctor should not throw and return an instance")

			return
		}

		defer { NativeAOT_CodeGeneratorInputSample_GenericTests_Destroy(genericTests) }

		guard let arrayRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnArrayOfRepeatedValues_A1(systemObjectType,
																											genericTests,
																											value,
																											numberOfElements,
																											&exception),
			  exception == nil else {
			XCTFail("ReturnArrayOfRepeatedValues should not throw and return an instance")

			return
		}

		defer { System_Array_Destroy(arrayRet) }

		let length = System_Array_Length_Get(arrayRet,
											 &exception)

		XCTAssertNil(exception)
		XCTAssertEqual(numberOfElements, length)

		for idx in 0..<length {
			guard let element = System_Array_GetValue_1(arrayRet,
														idx,
														&exception),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")

				return
			}

			defer { System_Object_Destroy(element) }

			let equal = System_Object_ReferenceEquals(value,
													  element,
													  &exception)

			XCTAssertNil(exception)
			XCTAssertTrue(equal)
		}
	}
	
	func testReturnSimpleKeyValuePair() {
		var exception: System_Exception_t?
		
		guard let keyType = System_Type_TypeOf() else {
			XCTFail("typeof(System.Type) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(keyType) }
		
		guard let valueType = System_Text_StringBuilder_TypeOf() else {
			XCTFail("typeof(System.Text.StringBuilder) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(valueType) }
		
		guard let key = System_Type_TypeOf() else {
			XCTFail("typeof(System.Type) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(key)}
		
		guard let value = System_Text_StringBuilder_Create(&exception),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_StringBuilder_Destroy(value) }
		
		let expectedString = "Hello World"
		
		System_Text_StringBuilder_Append_2(value,
										   expectedString,
										   &exception)
		
		XCTAssertNil(exception)
		
		guard let keyValuePair = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnSimpleKeyValuePair_A2(keyType,
																											 valueType,
																											 key,
																											 value,
																											 &exception),
			  exception == nil else {
			XCTFail("ReturnSimpleKeyValuePair should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_Destroy(keyValuePair) }
		
		guard let keyRet = NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_Key_Get(keyValuePair,
																									  &exception),
			  exception == nil else {
			XCTFail("SimpleKeyValuePair.Key getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(keyRet) }
		
		let keyEqual = System_Object_Equals(key,
											keyRet,
											&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(keyEqual)
		
		guard let valueRet = NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_Value_Get(keyValuePair,
																										  &exception),
			  exception == nil else {
			XCTFail("SimpleKeyValuePair.Value getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(valueRet) }
		
		let valueEqual = System_Object_Equals(value,
											  valueRet,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(valueEqual)
		
		guard let stringRetC = System_Text_StringBuilder_ToString(valueRet,
																  &exception),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return an instance of a string")
			
			return
		}
		
		defer { stringRetC.deallocate() }
		let stringRet = String(cString: stringRetC)
		
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testIncorrectParametersInGenericMethod() {
		var exception: System_Exception_t?
		
		guard let keyType = System_Type_TypeOf() else {
			XCTFail("typeof(System.Type) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(keyType) }
		
		guard let valueType = System_Text_StringBuilder_TypeOf() else {
			XCTFail("typeof(System.Text.StringBuilder) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(valueType) }
		
		guard let key = System_Type_TypeOf() else {
			XCTFail("typeof(System.Type) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(key) }
		
		guard let value = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(value) }
		
		let keyValuePair = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnSimpleKeyValuePair_A2(keyType,
																									   valueType,
																									   key,
																									   value,
																									   &exception)

		defer {
			if let keyValuePair {
				NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_Destroy(keyValuePair)
			}
		}

		guard let exception else {
			XCTFail("ReturnSimpleKeyValuePair should throw an exception because the value passed in does not match the provided generic type")

			return
		}

		defer { System_Exception_Destroy(exception) }

		var exception2: System_Exception_t?

		guard let exceptionMessageC = System_Exception_Message_Get(exception,
																   &exception2),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C string")

			return
		}

		defer { exceptionMessageC.deallocate() }
		let exceptionMessage = String(cString: exceptionMessageC)
		
		XCTAssertEqual(exceptionMessage, "Object of type \'System.Object\' cannot be converted to type \'System.Text.StringBuilder\'.")
	}
}
