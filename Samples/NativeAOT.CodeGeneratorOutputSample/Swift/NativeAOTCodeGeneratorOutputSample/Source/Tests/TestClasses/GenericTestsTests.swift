import XCTest
import NativeAOTCodeGeneratorOutputSample

final class GenericTestsTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testReturnGenericTypeWithReferenceType() {
		var exception: System_Exception_t?
		
		let genericType = System_String_TypeOf()
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
	func testReturnGenericTypeWithValueType() {
		var exception: System_Exception_t?
		
		let genericType = System_Guid_TypeOf()
		defer { System_Type_Destroy(genericType) }
		
		let typeRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericType_A1(genericType,
																						   &exception)
		
		XCTAssertNotNil(exception)
		
		System_Exception_Destroy(exception)
		
		if let typeRet {
			System_Type_Destroy(typeRet)
		}
	}
	
	func testReturnGenericTypeAsOutParameter() {
		var exception: System_Exception_t?
		
		let genericType = System_String_TypeOf()
		defer { System_Type_Destroy(genericType) }
		
		var typeRet: System_Type_t?
		
		NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericTypeAsOutParameter_A1(genericType,
																						   &typeRet,
																						   &exception)
		
		guard exception == nil,
			  let typeRet else {
			XCTFail("ReturnGenericTypeAsOutParameter<System.String> should not throw and return an instance as out parameter")
			
			return
		}
		
		defer { System_Type_Destroy(typeRet) }
		
		let typesEqual = System_Object_Equals(genericType,
											  typeRet,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypeAsRefParameter() {
		var exception: System_Exception_t?
		
		let genericType = System_String_TypeOf()
		defer { System_Type_Destroy(genericType) }
		
		let systemObjectType = System_Object_TypeOf()
		defer { System_Type_Destroy(systemObjectType) }
		
        var typeRet: System_Type_t? = systemObjectType
		
		NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnGenericTypeAsRefParameter_A1(genericType,
																						   &typeRet,
																						   &exception)
		
		guard exception == nil,
			  let typeRet else {
			XCTFail("ReturnGenericTypeAsRefParameter<System.String> should not throw and return an instance as ref parameter")
			
			return
		}
		
		defer { System_Type_Destroy(typeRet) }
		
		let typesEqual = System_Object_Equals(genericType,
											  typeRet,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypes() {
		var exception: System_Exception_t?
		
		let genericType1 = System_String_TypeOf()
		defer { System_Type_Destroy(genericType1) }
		
		let genericType2 = System_Array_TypeOf()
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
		
		let genericType = System_String_TypeOf()
		defer { System_Type_Destroy(genericType) }
		
		let defaultValueRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnDefaultValueOfGenericType_A1(genericType,
																												 &exception)
        
        defer {
            if let defaultValueRet {
                System_Object_Destroy(defaultValueRet)
            }
        }
		
		XCTAssertNil(exception)
		XCTAssertNil(defaultValueRet)
	}
	
	func testReturnArrayOfDefaultValuesOfGenericType() {
		var exception: System_Exception_t?
		
		let genericType = System_String_TypeOf()
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
            
            defer {
                if let defaultValue {
                    System_Object_Destroy(defaultValue)
                }
            }
			
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

		guard let arrayRet = NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnArrayOfRepeatedValues_A1(genericTests,
																											systemObjectType,
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
		
		let keyType = System_Type_TypeOf()
		defer { System_Type_Destroy(keyType) }
		
		let valueType = System_Text_StringBuilder_TypeOf()
		defer { System_Type_Destroy(valueType) }
		
		let key = System_Type_TypeOf()
		defer { System_Type_Destroy(key)}
		
		guard let value = System_Text_StringBuilder_Create(&exception),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Text_StringBuilder_Destroy(value) }
		
		let expectedString = "Hello World"
		let expectedStringDN = expectedString.cDotNETString()
		defer { System_String_Destroy(expectedStringDN) }
		
		System_Text_StringBuilder_Append_2(value,
										   expectedStringDN,
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
		
		guard let stringRet = String(cDotNETString: System_Text_StringBuilder_ToString(valueRet,
																					  &exception),
									 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return an instance of a string")
			
			return
		}
		
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testIncorrectParametersInGenericMethod() {
		var exception: System_Exception_t?
		
		let keyType = System_Type_TypeOf()
		defer { System_Type_Destroy(keyType) }
		
		let valueType = System_Text_StringBuilder_TypeOf()
		defer { System_Type_Destroy(valueType) }
		
		let key = System_Type_TypeOf()
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

		guard let exceptionMessage = String(cDotNETString: System_Exception_Message_Get(exception,
																					   &exception2),
											destroyDotNETString: true),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C string")

			return
		}

		XCTAssertEqual(exceptionMessage, "Object of type \'System.Object\' cannot be converted to type \'System.Text.StringBuilder\'.")
	}
	
	func testReturnStringOfJoinedArray() {
		var exception: System_Exception_t?

		let numberOfElements: Int32 = 10
		let stringPrefix = "Hello_"
		
		let separator = ";"
		let separatorDN = separator.cDotNETString()
		defer { System_String_Destroy(separatorDN) }

		let stringType = System_String_TypeOf()
		defer { System_Type_Destroy(stringType) }

		guard let arrayOfStrings = System_Array_CreateInstance(stringType,
															   numberOfElements,
															   &exception),
			  exception == nil else {
			XCTFail("System.Array ctor should not throw and return an instance")

			return
		}

		defer { System_Array_Destroy(arrayOfStrings) }

        var dnStrings = [System_String_t]()
		var strings = [String]()
		
		for idx in 0..<numberOfElements {
			let string = "\(stringPrefix)\(idx)"
			let stringDN = string.cDotNETString()
			
            dnStrings.append(stringDN)
			strings.append(string)

			System_Array_SetValue(arrayOfStrings,
								  stringDN,
								  idx,
								  &exception)

			XCTAssertNil(exception)
		}
		
		let expectedString = strings.joined(separator: separator)
        
        for dnString in dnStrings {
            System_String_Destroy(dnString)
        }

		guard let stringRet = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_GenericTests_ReturnStringOfJoinedArray_A1(stringType,
																																arrayOfStrings,
																																separatorDN,
																																&exception),
									 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("ReturnStringOfJoinedArray should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedString, stringRet)
	}
    
    func testConstructedGenericListOfStrings() {
        var exception: System_Exception_t?
        
        guard let genericTests = NativeAOT_CodeGeneratorInputSample_GenericTests_Create(&exception),
              exception == nil else {
            XCTFail("GenericTests ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_GenericTests_Destroy(genericTests) }
        
        guard let listOfStrings = NativeAOT_CodeGeneratorInputSample_GenericTests_ListOfStrings_Get(genericTests,
                                                                                                    &exception),
              exception == nil else {
            XCTFail("GenericTests.ListOfStrings getter should not throw and return an instance")
            
            return
        }
        
        defer { System_Collections_Generic_List_A1_Destroy(listOfStrings) }
        
        let listType = System_Collections_Generic_List_A1_TypeOf()
        defer { System_Type_Destroy(listType) }
        
        let systemTypeType = System_Type_TypeOf()
        defer { System_Type_Destroy(systemTypeType) }
        
        let systemStringType = System_String_TypeOf()
        defer { System_Type_Destroy(systemStringType) }
        
        guard let typeArguments = System_Array_CreateInstance(systemTypeType,
                                                              1,
                                                              &exception),
              exception == nil else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        defer { System_Array_Destroy(typeArguments) }
        
        System_Array_SetValue_4(typeArguments,
                                systemStringType,
                                0,
                                &exception)
        
        XCTAssertNil(exception)
        
        guard let listOfStringType = System_Type_MakeGenericType(listType,
                                                                 typeArguments,
                                                                 &exception),
              exception == nil else {
            XCTFail("System.Type.MakeGenericType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(listOfStringType) }
        
        XCTAssertTrue(DNObjectIs(listOfStrings, listOfStringType))
        
        guard let arrayOfStrings = System_Collections_Generic_List_A1_ToArray(listOfStrings,
                                                                              systemStringType,
                                                                              &exception),
              exception == nil else {
            XCTFail("System.Collections.Generic.List<System.String>.ToArray should not throw and return an instance")
            
            return
        }
        
        defer { System_Array_Destroy(arrayOfStrings) }
        
        let length = System_Array_Length_Get(arrayOfStrings,
                                             &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(2, length)
        
        guard let firstObject = System_Array_GetValue_1(arrayOfStrings,
                                                        0,
                                                        &exception),
              exception == nil,
              DNObjectIs(firstObject, systemStringType),
              let firstString = String(cDotNETString: firstObject,
                                       destroyDotNETString: true) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("A", firstString)
        
        guard let secondObject = System_Array_GetValue_1(arrayOfStrings,
                                                         1,
                                                         &exception),
              exception == nil,
              DNObjectIs(secondObject, systemStringType),
              let secondString = String(cDotNETString: secondObject,
                                       destroyDotNETString: true) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("B", secondString)
    }
}
