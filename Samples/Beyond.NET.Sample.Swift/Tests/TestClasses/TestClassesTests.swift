import XCTest
import BeyondDotNETSampleKit

final class TestClassesTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTestClass() throws {
        let testClass = try Beyond_NET_Sample_TestClass()
        let testClassType = try testClass.getType()
        
        let systemObjectTypeName = "System.Object"
        let systemObjectTypeNameDN = systemObjectTypeName.dotNETString()
        
        guard let systemObjectType = try System_Type.getType(systemObjectTypeNameDN,
                                                              true,
                                                              false) else {
            XCTFail("System.Type.GetType should not throw and return an instance")
            
            return
        }
        
        let retrievedSystemObjectTypeName = try systemObjectType.toString().string()
        XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
        
        let isTestClassAssignableToSystemObject = try testClassType.isAssignableTo(systemObjectType)
        XCTAssertTrue(isTestClassAssignableToSystemObject)
        
        let isSystemObjectAssignableToTestClass = try systemObjectType.isAssignableTo(testClassType)
        XCTAssertFalse(isSystemObjectAssignableToTestClass)
        
        try testClass.sayHello()
        
        let john = "John"
        let johnDN = john.dotNETString()
        
        try testClass.sayHello(johnDN)
        
        let hello = try testClass.getHello().string()
        XCTAssertEqual("Hello", hello)
        
        let number1: Int32 = 85
        let number2: Int32 = 262
        
        let expectedResult = number1 + number2
        
        let result = try testClass.add(number1, number2)
        XCTAssertEqual(expectedResult, result)
    }
    
    func testEnum() throws {
        let enumValue = Beyond_NET_Sample_TestEnum.secondCase
        
        let enumName = try Beyond_NET_Sample_TestClass.getTestEnumName(enumValue).string()
        XCTAssertEqual("SecondCase", enumName)
    }
    
    func testInt32ByRef() throws {
        let testClass = try Beyond_NET_Sample_TestClass()
        
        let originalValue: Int32 = 5
        var valueToModify: Int32 = originalValue
        let targetValue: Int32 = 10
        
        let originalValueRet = try testClass.modifyByRefValueAndReturnOriginalValue(&valueToModify,
                                                                                    targetValue)
        
        XCTAssertEqual(originalValue, originalValueRet)
        XCTAssertEqual(targetValue, valueToModify)
    }
    
    func testEnumByRef() throws {
        let testClass = try Beyond_NET_Sample_TestClass()
        
        let originalValue = Beyond_NET_Sample_TestEnum.firstCase
        var valueToModify = originalValue
        let expectedValue = Beyond_NET_Sample_TestEnum.secondCase
        
        try testClass.modifyByRefEnum(&valueToModify)
        XCTAssertEqual(expectedValue, valueToModify)
    }
	
	func testDelegateReturningChar() throws {
		let testClass = try Beyond_NET_Sample_TestClass()
		
		let value = DNChar(cValue: 5)
		
		let charReturnerDelegate = Beyond_NET_Sample_CharReturnerDelegate {
			value
		}
		
		let retVal = try testClass.getChar(charReturnerDelegate)
		XCTAssertEqual(value, retVal)
	}
    
    func testBookByRef() throws {
        let testClass = try Beyond_NET_Sample_TestClass()
        
        let originalBook = Beyond_NET_Sample_Book.donQuixote
        let targetBook = Beyond_NET_Sample_Book.theLordOfTheRings
        
        var bookToModify = originalBook
        var originalBookRet = Beyond_NET_Sample_Book.outParameterPlaceholder
        
        try testClass.modifyByRefBookAndReturnOriginalBookAsOutParameter(&bookToModify,
                                                                         targetBook,
                                                                         &originalBookRet)
        
        XCTAssertTrue(originalBook == originalBookRet)
        XCTAssertTrue(targetBook == bookToModify)
    }
    
    // TODO: By Ref return values are not supported in Swift yet
//    func testGetCurrentBookByRef() {
//        guard let testClass = try? Beyond_NET_Sample_TestClass() else {
//            XCTFail("TestClass ctor should not throw and return an instance")
//
//            return
//        }
//
//        guard let expectedBook = testClass.currentBook else {
//            XCTFail("Failed to get book")
//
//            return
//        }
//
//        guard let pointerToCurrentBook = Beyond_NET_Sample_TestClass_GetCurrentBookByRef(testClass,
//                                                                                                          &exception),
//              let currentBook = pointerToCurrentBook.pointee,
//              exception == nil else {
//            XCTFail("TestClass.GetCurrentBookByRef should not throw and return an instance wrapped in a pointer")
//
//            return
//        }
//
//        defer {
//            Beyond_NET_Sample_Book_Destroy(currentBook)
//            free(pointerToCurrentBook)
//        }
//
//        let currentBookIsEqualToInitialBook = System_Object_Equals(expectedBook,
//                                                                   currentBook,
//                                                                   &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertTrue(currentBookIsEqualToInitialBook)
//    }
    
    // TODO: By Ref return values are not supported in Swift yet
//    func testIncreaseAndGetCurrentIntValueByRef() {
//        var exception: System_Exception_t?
//
//        guard let testClass = Beyond_NET_Sample_TestClass_Create(&exception),
//              exception == nil else {
//            XCTFail("TestClass ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { Beyond_NET_Sample_TestClass_Destroy(testClass) }
//
//        let expectedValue = Beyond_NET_Sample_TestClass_CurrentIntValue_Get(testClass) + 1
//
//        guard let pointerToCurrentValue = Beyond_NET_Sample_TestClass_IncreaseAndGetCurrentIntValueByRef(testClass,
//                                                                                                                          &exception),
//              exception == nil else {
//            XCTFail("TestClass.IncreaseAndGetCurrentIntValueByRef should not throw and return an instance wrapped in a pointer")
//
//            return
//        }
//
//        defer {
//            free(pointerToCurrentValue)
//        }
//
//        let currentValue = pointerToCurrentValue.pointee
//
//        XCTAssertEqual(expectedValue, currentValue)
//    }
}
