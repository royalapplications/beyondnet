import XCTest
import BeyondNETSampleSwift

// TODO: Tests fail on iOS Simulator
final class TestClassesTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
    func testTestClass() {
        var exception: System_Exception_t?
        
        let testClass = Beyond_NET_Sample_TestClass_Create(&exception)
        
        guard exception == nil else {
            XCTFail("init should not throw")
            
            return
        }
        
        guard let testClass else {
            XCTFail("init should return an instance")
            
            return
        }
        
        defer {
            Beyond_NET_Sample_TestClass_Destroy(testClass)
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
		let systemObjectTypeNameDN = systemObjectTypeName.cDotNETString()
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
        
		guard let retrievedSystemObjectTypeName = String(cDotNETString: System_Type_ToString(systemObjectType,
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
        
        Beyond_NET_Sample_TestClass_SayHello(testClass,
                                                              &exception)
        
        guard exception == nil else {
            XCTFail("SayHello should not throw")
            
            return
        }
		
		let john = "John"
		let johnDN = john.cDotNETString()
		defer { System_String_Destroy(johnDN) }
        
		Beyond_NET_Sample_TestClass_SayHello_1(testClass,
																johnDN,
																&exception)
        
        guard exception == nil else {
            XCTFail("SayHello1 should not throw")
            
            return
        }
        
        guard let hello = String(cDotNETString: Beyond_NET_Sample_TestClass_GetHello(testClass,
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
        
        let result = Beyond_NET_Sample_TestClass_Add(testClass,
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
        
		let enumValue = Beyond_NET_Sample_TestEnum_t.secondCase
        
		guard let enumName = String(cDotNETString: Beyond_NET_Sample_TestClass_GetTestEnumName(enumValue,
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
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer {
			Beyond_NET_Sample_TestClass_Destroy(testClass)
		}
		
		let originalValue: Int32 = 5
		var valueToModify: Int32 = originalValue
		let targetValue: Int32 = 10
		
		let originalValueRet = Beyond_NET_Sample_TestClass_ModifyByRefValueAndReturnOriginalValue(testClass,
																												   &valueToModify,
																												   targetValue,
																												   &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(originalValue, originalValueRet)
		XCTAssertEqual(targetValue, valueToModify)
	}
	
	func testEnumByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer {
			Beyond_NET_Sample_TestClass_Destroy(testClass)
		}
		
		let originalValue = Beyond_NET_Sample_TestEnum_t.firstCase
		var valueToModify = originalValue
		let expectedValue = Beyond_NET_Sample_TestEnum_t.secondCase
		
		Beyond_NET_Sample_TestClass_ModifyByRefEnum(testClass,
																	 &valueToModify,
																	 &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(expectedValue, valueToModify)
	}
	
	func testDelegateReturningChar() {
		var exception: System_Exception_t?
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_TestClass_Destroy(testClass) }
		
		let value: wchar_t = 5
		
		let charReturnerFunc: Beyond_NET_Sample_CharReturnerDelegate_CFunction_t = { _ in
			return 5
		}
		
		guard let charReturnerDelegate = Beyond_NET_Sample_CharReturnerDelegate_Create(nil,
																										charReturnerFunc,
																										nil) else {
			XCTFail("CharReturnerDelegate ctor should return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_CharReturnerDelegate_Destroy(charReturnerDelegate) }
		
		let retVal = Beyond_NET_Sample_TestClass_GetChar(testClass,
																		  charReturnerDelegate,
																		  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, retVal)
	}
	
	func testBookByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_TestClass_Destroy(testClass) }
		
		guard let originalBook = Beyond_NET_Sample_Book_DonQuixote_Get() else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { Beyond_NET_Sample_Book_Destroy(originalBook) }
		
		guard let targetBook = Beyond_NET_Sample_Book_TheLordOfTheRings_Get() else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { Beyond_NET_Sample_Book_Destroy(targetBook) }
		
		var bookToModify: Beyond_NET_Sample_Book_t? = originalBook
		var originalBookRet: Beyond_NET_Sample_Book_t?
		
		Beyond_NET_Sample_TestClass_ModifyByRefBookAndReturnOriginalBookAsOutParameter(testClass,
																										&bookToModify,
																										targetBook,
																										&originalBookRet,
																										&exception)
		
		defer { Beyond_NET_Sample_Book_Destroy(originalBookRet) }
		
		XCTAssertNil(exception)
		
		XCTAssertTrue(System_Object_Equals(originalBook, originalBookRet, &exception))
		XCTAssertNil(exception)
		
		XCTAssertTrue(System_Object_Equals(targetBook, bookToModify, &exception))
		XCTAssertNil(exception)
	}
	
	func testGetCurrentBookByRef() {
		var exception: System_Exception_t?
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_TestClass_Destroy(testClass) }
		
		guard let expectedBook = Beyond_NET_Sample_TestClass_CurrentBook_Get(testClass) else {
			XCTFail("Failed to get book")
			
			return
		}
		
		defer { Beyond_NET_Sample_Book_Destroy(expectedBook) }
		
		guard let pointerToCurrentBook = Beyond_NET_Sample_TestClass_GetCurrentBookByRef(testClass,
																										  &exception),
			  let currentBook = pointerToCurrentBook.pointee,
			  exception == nil else {
			XCTFail("TestClass.GetCurrentBookByRef should not throw and return an instance wrapped in a pointer")
			
			return
		}
		
		defer {
			Beyond_NET_Sample_Book_Destroy(currentBook)
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
		
		guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
			  exception == nil else {
			XCTFail("TestClass ctor should not throw and return an instance")
			
			return
		}
		
		defer { Beyond_NET_Sample_TestClass_Destroy(testClass) }
		
		let expectedValue = Beyond_NET_Sample_TestClass_CurrentIntValue_Get(testClass) + 1
		
		guard let pointerToCurrentValue = Beyond_NET_Sample_TestClass_IncreaseAndGetCurrentIntValueByRef(testClass,
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
