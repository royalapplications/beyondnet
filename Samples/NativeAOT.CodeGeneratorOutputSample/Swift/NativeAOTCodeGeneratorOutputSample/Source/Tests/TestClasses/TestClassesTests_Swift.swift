import XCTest
import NativeAOTCodeGeneratorOutputSample

final class TestClassesTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTestClass() {
        guard let testClass = try? NativeAOT_CodeGeneratorInputSample_TestClass() else {
            XCTFail("TestClass ctor should not throw")
            
            return
        }
        
        guard let testClassType = try? testClass.getType() else {
            XCTFail("TestClass.GetType should not throw and return an instance")
            
            return
        }
        
        let systemObjectTypeName = "System.Object"
        let systemObjectTypeNameDN = systemObjectTypeName.dotNETString()
        
        guard let systemObjectType = try? System_Type.getType(systemObjectTypeNameDN,
                                                              true,
                                                              false) else {
            XCTFail("System.Type.GetType should not throw and return an instance")
            
            return
        }
        
        guard let retrievedSystemObjectTypeName = try? systemObjectType.toString()?.string() else {
            XCTFail("System.Type.ToString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
        
        let isTestClassAssignableToSystemObject = (try? testClassType.isAssignableTo(systemObjectType)) ?? false
        XCTAssertTrue(isTestClassAssignableToSystemObject)
        
        let isSystemObjectAssignableToTestClass = (try? systemObjectType.isAssignableTo(testClassType)) ?? true
        XCTAssertFalse(isSystemObjectAssignableToTestClass)
        
        XCTAssertNoThrow(try testClass.sayHello())
        
        let john = "John"
        let johnDN = john.dotNETString()
        
        XCTAssertNoThrow(try testClass.sayHello(johnDN))
        
        guard let hello = try? testClass.getHello()?.string() else {
            XCTFail("TestClass.GetHello should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("Hello", hello)
        
        let number1: Int32 = 85
        let number2: Int32 = 262
        
        let expectedResult = number1 + number2
        
        let result = (try? testClass.add(number1,
                                         number2)) ?? -999
        
        XCTAssertEqual(expectedResult, result)
    }
    
    func testEnum() {
        let enumValue = NativeAOT_CodeGeneratorInputSample_TestEnum.secondCase
        
        guard let enumName = try? NativeAOT_CodeGeneratorInputSample_TestClass.getTestEnumName(enumValue)?.string() else {
            XCTFail("TestClass.GetTestEnumName should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("SecondCase", enumName)
    }
    
    func testInt32ByRef() {
        guard let testClass = try? NativeAOT_CodeGeneratorInputSample_TestClass() else {
            XCTFail("TestClass ctor should not throw and return an instance")
            
            return
        }
        
        let originalValue: Int32 = 5
        var valueToModify: Int32 = originalValue
        let targetValue: Int32 = 10
        
        do {
            let originalValueRet = try testClass.modifyByRefValueAndReturnOriginalValue(&valueToModify,
                                                                                        targetValue)
            
            XCTAssertEqual(originalValue, originalValueRet)
            XCTAssertEqual(targetValue, valueToModify)
        } catch {
            XCTFail("TestClass.ModifyByRefValueAndReturnOriginalValue should not throw")
            
            return
        }
    }
    
    func testEnumByRef() {
        guard let testClass = try? NativeAOT_CodeGeneratorInputSample_TestClass() else {
            XCTFail("TestClass ctor should not throw and return an instance")
            
            return
        }
        
        let originalValue = NativeAOT_CodeGeneratorInputSample_TestEnum.firstCase
        var valueToModify = originalValue
        let expectedValue = NativeAOT_CodeGeneratorInputSample_TestEnum.secondCase
        
        XCTAssertNoThrow(try testClass.modifyByRefEnum(&valueToModify))
        XCTAssertEqual(expectedValue, valueToModify)
    }
    
    func testBookByRef() {
        guard let testClass = try? NativeAOT_CodeGeneratorInputSample_TestClass() else {
            XCTFail("TestClass ctor should not throw and return an instance")
            
            return
        }
        
        guard let originalBook = NativeAOT_CodeGeneratorInputSample_Book.donQuixote_get() else {
            XCTFail("Failed to get book")
            
            return
        }
        
        guard let targetBook = NativeAOT_CodeGeneratorInputSample_Book.theLordOfTheRings_get() else {
            XCTFail("Failed to get book")
            
            return
        }
        
        var bookToModify: NativeAOT_CodeGeneratorInputSample_Book? = originalBook
        var originalBookRet: NativeAOT_CodeGeneratorInputSample_Book?
        
        XCTAssertNoThrow(try testClass.modifyByRefBookAndReturnOriginalBookAsOutParameter(&bookToModify,
                                                                                          targetBook,
                                                                                          &originalBookRet))
        
        XCTAssertTrue(originalBook == originalBookRet)
        XCTAssertTrue(targetBook == bookToModify)
    }
    
    // TODO: By Ref return values are not supported in Swift yet
//    func testGetCurrentBookByRef() {
//        guard let testClass = try? NativeAOT_CodeGeneratorInputSample_TestClass() else {
//            XCTFail("TestClass ctor should not throw and return an instance")
//
//            return
//        }
//
//        guard let expectedBook = testClass.currentBook_get() else {
//            XCTFail("Failed to get book")
//
//            return
//        }
//
//        guard let pointerToCurrentBook = NativeAOT_CodeGeneratorInputSample_TestClass_GetCurrentBookByRef(testClass,
//                                                                                                          &exception),
//              let currentBook = pointerToCurrentBook.pointee,
//              exception == nil else {
//            XCTFail("TestClass.GetCurrentBookByRef should not throw and return an instance wrapped in a pointer")
//
//            return
//        }
//
//        defer {
//            NativeAOT_CodeGeneratorInputSample_Book_Destroy(currentBook)
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
//        guard let testClass = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception),
//              exception == nil else {
//            XCTFail("TestClass ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(testClass) }
//
//        let expectedValue = NativeAOT_CodeGeneratorInputSample_TestClass_CurrentIntValue_Get(testClass) + 1
//
//        guard let pointerToCurrentValue = NativeAOT_CodeGeneratorInputSample_TestClass_IncreaseAndGetCurrentIntValueByRef(testClass,
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