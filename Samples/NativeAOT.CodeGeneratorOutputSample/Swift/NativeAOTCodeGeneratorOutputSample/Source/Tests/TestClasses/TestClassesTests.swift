import XCTest
import NativeAOTCodeGeneratorOutputSample

final class TestClassesTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.gcCollect()
	}
	
	@MainActor
	override class func tearDown() {
		Self.gcCollect()
	}
	
    func testTestClass() {
        var exception: System_Exception_t?
        
        let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception)
        
        guard exception == nil else {
            XCTFail("init should not throw")
            
            return
        }
        
        guard let testClass else {
            XCTFail("init should return an instance")
            
            return
        }
        
        defer {
            NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass)
        }
        
        let testClassType = System_Object_GetType(testClass,
                                                  &exception)
        
        guard exception == nil else {
            XCTFail("GetType should not throw")
            
            return
        }
        
        guard let testClassType else {
            XCTFail("GetType should return a type object")
            
            return
        }
        
        defer {
            System_Object_Destroy(testClassType)
        }
        
        let systemObjectTypeName = "System.Object"
		let systemObjectTypeNameDN = systemObjectTypeName.dotNETString()
		defer { System_String_Destroy(systemObjectTypeNameDN) }
        
		guard let systemObjectType = System_Type_GetType(systemObjectTypeNameDN,
														 true,
														 false,
														 &exception),
			  exception == nil else {
            XCTFail("System.Type.GetType should not throw")
            
            return
        }
        
        defer { System_Object_Destroy(systemObjectType) }
        
		guard let retrievedSystemObjectTypeName = String(dotNETString: System_Type_ToString(systemObjectType,
																							&exception),
														 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.ToString should not throw and return an instance")
			
			return
		}
        
        XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
        
        let isTestClassAssignableToSystemObject = System_Type_IsAssignableTo(testClassType,
                                                                             systemObjectType,
                                                                             &exception)
        
        guard exception == nil else {
            XCTFail("IsAssignableTo should not throw")
            
            return
        }
        
        XCTAssertTrue(isTestClassAssignableToSystemObject)
        
        let isSystemObjectAssignableToTestClass = System_Type_IsAssignableTo(systemObjectType,
                                                                             testClassType,
                                                                             &exception)
        
        guard exception == nil else {
            XCTFail("IsAssignableTo should not throw")
            
            return
        }
        
        XCTAssertFalse(isSystemObjectAssignableToTestClass)
        
        NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(testClass,
                                                              &exception)
        
        guard exception == nil else {
            XCTFail("SayHello should not throw")
            
            return
        }
		
		let john = "John"
		let johnDN = john.dotNETString()
		defer { System_String_Destroy(johnDN) }
        
		NativeAOT_CodeGeneratorInputSample_TestClass_SayHello_1(testClass,
																johnDN,
																&exception)
        
        guard exception == nil else {
            XCTFail("SayHello1 should not throw")
            
            return
        }
        
        guard let hello = String(dotNETString: NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(testClass,
																									 &exception),
								 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("GetHello should not throw and return an instance")
			
			return
		}
		
        XCTAssertEqual("Hello", hello)
        
        let number1: Int32 = 85
        let number2: Int32 = 262
        
        let expectedResult = number1 + number2
        
        let result = NativeAOT_CodeGeneratorInputSample_TestClass_Add(testClass,
                                                                      number1,
                                                                      number2,
                                                                      &exception)
        
        guard exception == nil else {
            XCTFail("Add should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedResult, result)
    }
    
    func testEnum() {
        var exception: System_Exception_t?
        
        let enumValue = NativeAOT_CodeGeneratorInputSample_TestEnum.secondCase
        
		guard let enumName = String(dotNETString: NativeAOT_CodeGeneratorInputSample_TestClass_GetTestEnumName(enumValue,
																											   &exception),
									destroyDotNETString: true),
			  exception == nil else {
			XCTFail("GetTestEnumName should not throw and return an instance")
			
			return
		}
		
        XCTAssertEqual("SecondCase", enumName)
    }
	
	func testInt32ByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer {
			NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass)
		}
		
		let originalValue: Int32 = 5
		var valueToModify: Int32 = originalValue
		let targetValue: Int32 = 10
		
		let originalValueRet = NativeAOT_CodeGeneratorInputSample_TestClass_ModifyByRefValueAndReturnOriginalValue(testClass,
																												   &valueToModify,
																												   targetValue,
																												   &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(originalValue, originalValueRet)
		XCTAssertEqual(targetValue, valueToModify)
	}
	
	func testEnumByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer {
			NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass)
		}
		
		let originalValue = NativeAOT_CodeGeneratorInputSample_TestEnum.firstCase
		var valueToModify = originalValue
		let expectedValue = NativeAOT_CodeGeneratorInputSample_TestEnum.secondCase
		
		NativeAOT_CodeGeneratorInputSample_TestClass_ModifyByRefEnum(testClass,
																	 &valueToModify,
																	 &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(expectedValue, valueToModify)
	}
	
	func testBookByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass) }
		
		guard let originalBook = NativeAOT_CodeGeneratorInputSample_Book_DonQuixote_Get() else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Book_Destroy(originalBook) }
		
		guard let targetBook = NativeAOT_CodeGeneratorInputSample_Book_TheLordOfTheRings_Get() else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Book_Destroy(targetBook) }
		
		var bookToModify: NativeAOT_CodeGeneratorInputSample_Book_t? = originalBook
		var originalBookRet: NativeAOT_CodeGeneratorInputSample_Book_t?
		
		NativeAOT_CodeGeneratorInputSample_TestClass_ModifyByRefBookAndReturnOriginalBookAsOutParameter(testClass,
																										&bookToModify,
																										targetBook,
																										&originalBookRet,
																										&exception)
		
		defer { NativeAOT_CodeGeneratorInputSample_Book_Destroy(originalBookRet) }
		
		XCTAssertNil(exception)
		
		XCTAssertTrue(System_Object_Equals(originalBook, originalBookRet, &exception))
		XCTAssertNil(exception)
		
		XCTAssertTrue(System_Object_Equals(targetBook, bookToModify, &exception))
		XCTAssertNil(exception)
	}
	
	func testGetCurrentBookByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass) }
		
		guard let expectedBook = NativeAOT_CodeGeneratorInputSample_TestClass_CurrentBook_Get(testClass) else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Book_Destroy(expectedBook) }
		
		guard let pointerToCurrentBook = NativeAOT_CodeGeneratorInputSample_TestClass_GetCurrentBookByRef(testClass,
																										  &exception),
			  let currentBook = pointerToCurrentBook.pointee,
			  exception == nil else {
			XCTFail("TestClass.GetCurrentBookByRef should not throw and return an instance wrapped in a pointer")
			
			return
		}
		
		defer {
			NativeAOT_CodeGeneratorInputSample_Book_Destroy(currentBook)
			free(pointerToCurrentBook)
		}
		
		let currentBookIsEqualToInitialBook = System_Object_Equals(expectedBook,
																   currentBook,
																   &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(currentBookIsEqualToInitialBook)
	}
	
	func testIncreaseAndGetCurrentIntValueByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass) }
		
		let expectedValue = NativeAOT_CodeGeneratorInputSample_TestClass_CurrentIntValue_Get(testClass) + 1
		
		guard let pointerToCurrentValue = NativeAOT_CodeGeneratorInputSample_TestClass_IncreaseAndGetCurrentIntValueByRef(testClass,
																														  &exception),
			  exception == nil else {
			XCTFail("TestClass.IncreaseAndGetCurrentIntValueByRef should not throw and return an instance wrapped in a pointer")
			
			return
		}
		
		defer {
			free(pointerToCurrentValue)
		}
		
		let currentValue = pointerToCurrentValue.pointee
		
		XCTAssertEqual(expectedValue, currentValue)
	}
}
