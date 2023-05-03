import XCTest
import NativeAOTCodeGeneratorOutputSample

final class GenericTestClassTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testWith1GenericArgument() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		guard let genericTestClass = Beyond_NET_Sample_GenericTestClass_A1_Create(systemStringType,
																								   &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String>.GenericTestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_GenericTestClass_A1_Destroy(genericTestClass) }
		
		guard let genericTestClassType = System_Object_GetType(genericTestClass,
															   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericTestClassType) }
		
		guard let genericTestClassTypeName = String(cDotNETString: System_Type_FullName_Get(genericTestClassType,
																						   &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`1[[System.String"))
		
		guard let genericArgumentType = Beyond_NET_Sample_GenericTestClass_A1_ReturnGenericClassType(genericTestClass,
																													  systemStringType,
																													  &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String>.ReturnGenericClassType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericArgumentType) }
		
		let typesEqual = System_Type_Equals(systemStringType,
											genericArgumentType,
											&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(typesEqual)
		
		let value: Int32 = 5
		
		Beyond_NET_Sample_GenericTestClass_A1_AProperty_Set(genericTestClass,
																			 systemStringType,
																			 value,
																			 &exception)
		
		XCTAssertNil(exception)
		
		let propValueRet = Beyond_NET_Sample_GenericTestClass_A1_AProperty_Get(genericTestClass,
																								systemStringType,
																								&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, propValueRet)
		
		Beyond_NET_Sample_GenericTestClass_A1_AField_Set(genericTestClass,
																		  systemStringType,
																		  value)
		
		XCTAssertNil(exception)
		
		let fieldValueRet = Beyond_NET_Sample_GenericTestClass_A1_AField_Get(genericTestClass,
																							  systemStringType)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array_TypeOf()
		defer { System_Type_Destroy(systemArrayType) }
		
		guard let genericArgumentAndMethodType = Beyond_NET_Sample_GenericTestClass_A1_ReturnGenericClassTypeAndGenericMethodType_A1(systemStringType,
																																					  systemArrayType,
																																					  &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String>.ReturnGenericClassTypeAndGenericMethodType<System.Array> should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(genericArgumentAndMethodType) }
		
		let genericArgumentAndMethodTypeLength = System_Array_Length_Get(genericArgumentAndMethodType,
																		 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(2, genericArgumentAndMethodTypeLength)
		
		guard let genericTypeArgument = System_Array_GetValue_1(genericArgumentAndMethodType,
																0,
																&exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericTypeArgument) }
		
		XCTAssertTrue(System_Type_Equals_1(systemStringType,
										   genericTypeArgument,
										   &exception))
		XCTAssertNil(exception)
		
		guard let genericMethodArgument = System_Array_GetValue_1(genericArgumentAndMethodType,
																  1,
																  &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericMethodArgument) }
		
		XCTAssertTrue(System_Type_Equals_1(systemArrayType,
										   genericMethodArgument,
										   &exception))
		XCTAssertNil(exception)
	}
    
    func testExtremeWith1GenericArgument() {
        var exception: System_Exception_t?
        
        let systemStringType = System_String_TypeOf()
        defer { System_Type_Destroy(systemStringType) }
        
        let systemExceptionType = System_Exception_TypeOf()
        defer { System_Type_Destroy(systemExceptionType) }
        
        guard let genericTestClass = Beyond_NET_Sample_GenericTestClass_A1_Create(systemStringType,
                                                                                                   &exception),
              exception == nil else {
            XCTFail("GenericTestClass<System.String>.GenericTestClass ctor should not throw and return an instance")
            
            return
        }
        
        defer { Beyond_NET_Sample_GenericTestClass_A1_Destroy(genericTestClass) }
        
        let string = "Hello World"
        let stringDN = string.cDotNETString()
        
        defer { System_String_Destroy(stringDN) }
        
        guard let originalException = System_Exception_Create_1(stringDN,
                                                                                     &exception),
              exception == nil else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Exception_Destroy(originalException) }
        
        var inOutException: System_Exception_t? = originalException
        
        let countIn: Int32 = 15
        var countOut: Int32 = -1
        
        var output: System_Object_t?
        
        guard let returnValue = Beyond_NET_Sample_GenericTestClass_A1_Extreme_A1(genericTestClass,
                                                                                                  systemStringType,
                                                                                                  systemExceptionType,
                                                                                                  countIn,
                                                                                                  &countOut,
                                                                                                  stringDN,
                                                                                                  &output,
                                                                                                  &inOutException,
                                                                                                  &exception),
              exception == nil else {
            XCTFail("GenericTestClass<System.String>.Extreme should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(countIn, countOut)
        
        let outputEqualsInput = System_Object_Equals(stringDN,
                                                     output,
                                                     &exception)
        
        XCTAssertNil(exception)
        XCTAssertTrue(outputEqualsInput)
        
        let returnValueEqualsInput = System_Object_Equals(stringDN,
                                                          returnValue,
                                                          &exception)
        
        XCTAssertNil(exception)
        XCTAssertTrue(returnValueEqualsInput)
        
        XCTAssertNil(inOutException)
    }
	
	func testWith2GenericArguments() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }
		
		guard let genericTestClass = Beyond_NET_Sample_GenericTestClass_A2_Create(systemStringType,
																								   systemExceptionType,
																								   &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String, System.Exception> ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_GenericTestClass_A2_Destroy(genericTestClass) }
		
		guard let genericTestClassType = System_Object_GetType(genericTestClass,
															   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericTestClassType) }
		
		guard let genericTestClassTypeName = String(cDotNETString: System_Type_FullName_Get(genericTestClassType,
																						   &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`2[[System.String"))
		XCTAssertTrue(genericTestClassTypeName.contains(",[System.Exception"))
		
		guard let genericArgumentTypes = Beyond_NET_Sample_GenericTestClass_A2_ReturnGenericClassTypes(genericTestClass,
																														systemStringType,
																														systemExceptionType,
																														&exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String, System.Exception>.ReturnGenericClassTypes should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(genericArgumentTypes) }
		
		let numberOfGenericArguments = System_Array_Length_Get(genericArgumentTypes,
															   &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(2, numberOfGenericArguments)
		
		guard let firstGenericArgument = System_Array_GetValue_1(genericArgumentTypes,
																 0,
																 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(firstGenericArgument) }
		
		let firstGenericTypeEqual = System_Type_Equals(systemStringType,
													   firstGenericArgument,
													   &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(firstGenericTypeEqual)
		
		guard let secondGenericArgument = System_Array_GetValue_1(genericArgumentTypes,
																  1,
																  &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(secondGenericArgument) }
		
		let secondGenericTypeEqual = System_Type_Equals(systemExceptionType,
														secondGenericArgument,
														&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(secondGenericTypeEqual)
		
		let value: Int32 = 5
		
		Beyond_NET_Sample_GenericTestClass_A2_AProperty_Set(genericTestClass,
																			 systemStringType,
																			 systemExceptionType,
																			 value,
																			 &exception)
		
		XCTAssertNil(exception)
		
		let propValueRet = Beyond_NET_Sample_GenericTestClass_A2_AProperty_Get(genericTestClass,
																								systemStringType,
																								systemExceptionType,
																								&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, propValueRet)
		
		Beyond_NET_Sample_GenericTestClass_A2_AField_Set(genericTestClass,
																		  systemStringType,
																		  systemExceptionType,
																		  value)
		
		XCTAssertNil(exception)
		
		let fieldValueRet = Beyond_NET_Sample_GenericTestClass_A2_AField_Get(genericTestClass,
																							  systemStringType,
																							  systemExceptionType)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array_TypeOf()
		defer { System_Type_Destroy(systemArrayType) }
		
		guard let genericArgumentsAndMethodType = Beyond_NET_Sample_GenericTestClass_A2_ReturnGenericClassTypeAndGenericMethodType_A1(systemStringType,
																																					   systemExceptionType,
																																					   systemArrayType,
																																					   &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String, System.Exception>.ReturnGenericClassTypeAndGenericMethodType<System.Array> should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(genericArgumentsAndMethodType) }
		
		let genericArgumentsAndMethodTypeLength = System_Array_Length_Get(genericArgumentsAndMethodType,
																		  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(3, genericArgumentsAndMethodTypeLength)
		
		guard let firstGenericTypeArgument = System_Array_GetValue_1(genericArgumentsAndMethodType,
																	 0,
																	 &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(firstGenericTypeArgument) }
		
		XCTAssertTrue(System_Type_Equals_1(systemStringType,
										   firstGenericTypeArgument,
										   &exception))
		XCTAssertNil(exception)
		
		guard let secondGenericTypeArgument = System_Array_GetValue_1(genericArgumentsAndMethodType,
																	  1,
																	  &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(secondGenericTypeArgument) }
		
		XCTAssertTrue(System_Type_Equals_1(systemExceptionType,
										   secondGenericTypeArgument,
										   &exception))
		XCTAssertNil(exception)
		
		guard let genericMethodArgument = System_Array_GetValue_1(genericArgumentsAndMethodType,
																  2,
																  &exception),
			  exception == nil else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericMethodArgument) }
		
		XCTAssertTrue(System_Type_Equals_1(systemArrayType,
										   genericMethodArgument,
										   &exception))
		XCTAssertNil(exception)
	}
}
