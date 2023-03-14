import XCTest
import NativeAOTCodeGeneratorOutputSample

final class NativeAOTCodeGeneratorOutputSampleTests: XCTestCase {
    func testExample() throws {
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
        
        var systemObjectType: System_Type_t?
        
        systemObjectTypeName.withCString { systemObjectTypeNameC in
            systemObjectType = System_Type_GetType(systemObjectTypeNameC,
                                                   .yes,
                                                   .no,
                                                   &exception)
        }
        
        guard exception == nil else {
            XCTFail("System.Type.GetType should not throw")
            
            return
        }
        
        guard let systemObjectType else {
            XCTFail("System.Type.GetType should return a type object")
            
            return
        }
        
        defer {
            System_Object_Destroy(systemObjectType)
        }
        
        let systemObjectTypeNameC = System_Type_ToString(systemObjectType,
                                                         &exception)
        
        guard exception == nil else {
            XCTFail("System.Type.ToString should not throw")
            
            return
        }
        
        guard let systemObjectTypeNameC else {
            XCTFail("System.Type.ToString should return an instance of a C string")
            
            return
        }
        
        let retrievedSystemObjectTypeName = String(cString: systemObjectTypeNameC)
        
        systemObjectTypeNameC.deallocate()
        
        XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
        
        let isTestClassAssignableToSystemObject = System_Type_IsAssignableTo(testClassType,
                                                                             systemObjectType,
                                                                             &exception)
        
        guard exception == nil else {
            XCTFail("IsAssignableTo should not throw")
            
            return
        }
        
        XCTAssertEqual(CBool.yes, isTestClassAssignableToSystemObject)
        
        let isSystemObjectAssignableToTestClass = System_Type_IsAssignableTo(systemObjectType,
                                                                             testClassType,
                                                                             &exception)
        
        guard exception == nil else {
            XCTFail("IsAssignableTo should not throw")
            
            return
        }
        
        XCTAssertEqual(CBool.no, isSystemObjectAssignableToTestClass)
        
        NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(testClass,
                                                              &exception)
        
        guard exception == nil else {
            XCTFail("SayHello should not throw")
            
            return
        }
        
        NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(testClass,
                                                               "John",
                                                               &exception)
        
        guard exception == nil else {
            XCTFail("SayHello1 should not throw")
            
            return
        }
        
        let hello = NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(testClass,
                                                                          &exception)
        
        guard exception == nil else {
            XCTFail("GetHello should not throw")
            
            return
        }
        
        guard let hello else {
            XCTFail("hello should not be nil")
            
            return
        }
        
        let helloString = String(cString: hello)
        
        hello.deallocate()
        
        XCTAssertEqual("Hello", helloString)
        
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
}
