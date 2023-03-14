import XCTest
import NativeAOTCodeGeneratorOutputSample

final class NativeAOTCodeGeneratorOutputSampleTests: XCTestCase {
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
        
        "John".withCString { cString in
            NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(testClass,
                                                                   cString,
                                                                   &exception)
        }
        
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
        
        let divNumber1: Int32 = 85
        let divNumber2: Int32 = 0
        
        _ = NativeAOT_CodeGeneratorInputSample_TestClass_Divide(testClass,
                                                                divNumber1,
                                                                divNumber2,
                                                                &exception)
        
        guard exception != nil else {
            XCTFail("Divide should throw but did not")
            
            return
        }
        
        var exception2: System_Exception_t?
        
        let exceptionAsStringC = System_Exception_ToString(exception,
                                                           &exception2)
        
        guard exception2 == nil else {
            XCTFail("System_Exception_ToString should not throw")
            
            return
        }
        
        guard let exceptionAsStringC else {
            XCTFail("System_Exception_ToString should return an instance of a string")
            
            return
        }
        
        let exceptionAsString = String(cString: exceptionAsStringC)
        
        exceptionAsStringC.deallocate()
        
        guard exceptionAsString.contains("DivideByZeroException") else {
            XCTFail("Exception string should contain \"DivideByZeroException\"")
            
            return
        }
    }
    
    func testPerson() {
        let firstName = "John"
        let lastName = "Doe"
        let age: Int32 = 24
        let expectedNiceLevel: NativeAOT_CodeGeneratorInputSample_NiceLevels = .veryNice
        let expectedNiceLevelString = "Very nice"
        
        let expectedFullName = "\(firstName) \(lastName)"
        let expectedWelcomeMessage = "Welcome, \(expectedFullName)! You're \(age) years old and \(expectedNiceLevelString)."
        
        var exception: System_Exception_t?
        var person: NativeAOT_CodeGeneratorInputSample_Person_t?
        
        firstName.withCString { firstNameC in
            lastName.withCString { lastNameC in
                person = NativeAOT_CodeGeneratorInputSample_Person_Create(firstNameC,
                                                                          lastNameC,
                                                                          age,
                                                                          &exception)
            }
        }
        
        guard let person,
              exception == nil else {
            XCTFail("Person initializer should not throw and return an instance")
            
            return
        }
        
        defer {
            NativeAOT_CodeGeneratorInputSample_Person_Destroy(person)
        }
        
        NativeAOT_CodeGeneratorInputSample_Person_NiceLevel_Set(person,
                                                                expectedNiceLevel,
                                                                &exception)
        
        guard exception == nil else {
            XCTFail("Person.NiceLevel setter should not throw")
            
            return
        }
        
        let retrievedNiceLevel = NativeAOT_CodeGeneratorInputSample_Person_NiceLevel_Get(person,
                                                                                         &exception)
        
        guard exception == nil else {
            XCTFail("Person.NiceLevel getter should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedNiceLevel, retrievedNiceLevel)
        
        let retrievedFirstNameC = NativeAOT_CodeGeneratorInputSample_Person_FirstName_Get(person,
                                                                                          &exception)
        
        guard let retrievedFirstNameC,
              exception == nil else {
            XCTFail("Person.FirstName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(firstName, .init(cString: retrievedFirstNameC))
        
        retrievedFirstNameC.deallocate()
        
        let retrievedLastNameC = NativeAOT_CodeGeneratorInputSample_Person_LastName_Get(person,
                                                                                        &exception)
        
        guard let retrievedLastNameC,
              exception == nil else {
            XCTFail("Person.LastName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(lastName, .init(cString: retrievedLastNameC))
        
        retrievedLastNameC.deallocate()
        
        let retrievedFullNameC = NativeAOT_CodeGeneratorInputSample_Person_FullName_Get(person,
                                                                                        &exception)
        
        guard let retrievedFullNameC,
              exception == nil else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedFullName, .init(cString: retrievedFullNameC))
        
        retrievedFullNameC.deallocate()
        
        let retrievedAge = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
                                                                             &exception)
        
        guard exception == nil else {
            XCTFail("Person.Age getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(age, retrievedAge)
        
        let retrievedWelcomeMessageC = NativeAOT_CodeGeneratorInputSample_Person_GetWelcomeMessage(person,
                                                                                                   &exception)
        
        guard let retrievedWelcomeMessageC,
              exception == nil else {
            XCTFail("Person.GetWelcomeMessage should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedWelcomeMessage, .init(cString: retrievedWelcomeMessageC))
        
        retrievedWelcomeMessageC.deallocate()
        
        let newFirstName = "Max 😉"
        let expectedNewFullName = "\(newFirstName) \(lastName)"
        
        newFirstName.withCString { newFirstNameC in
            NativeAOT_CodeGeneratorInputSample_Person_FirstName_Set(person,
                                                                    newFirstNameC,
                                                                    &exception)
        }
        
        guard exception == nil else {
            XCTFail("Person.FirstName setter should not throw")
            
            return
        }
        
        let newFullNameC = NativeAOT_CodeGeneratorInputSample_Person_FullName_Get(person,
                                                                                  &exception)
        
        guard let newFullNameC,
              exception == nil else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedNewFullName, .init(cString: newFullNameC))
        
        newFullNameC.deallocate()
    }
    
    func testSystemGuid() {
        var exception: System_Exception_t?
        
        let uuid = UUID()
        let uuidString = uuid.uuidString
        
        var guid: System_Guid_t?
        
        uuidString.withCString { uuidStringC in
            guid = System_Guid_Create2(uuidStringC,
                                       &exception)
        }
        
        guard exception == nil else {
            XCTFail("System_Guid_Create2 should not throw")
            
            return
        }
        
        guard let guid else {
            XCTFail("System_Guid_Create2 should return an instance of a Guid")
            
            return
        }
        
        defer {
            System_Object_Destroy(guid)
        }
        
        let guidCString = System_Guid_ToString(guid,
                                               &exception)
        
        guard exception == nil else {
            XCTFail("System_Guid_ToString should not throw")
            
            return
        }
        
        guard let guidCString else {
            XCTFail("System_Guid_ToString should return an instance of a String")
            
            return
        }
        
        let guidString = String(cString: guidCString)
        
        guidCString.deallocate()
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        let guidTypeName = "System.Guid"
        
        var guidType: System_Type_t?
        
        guidTypeName.withCString { guidTypeNameC in
            guidType = System_Type_GetType2(guidTypeNameC,
                                            &exception)
        }
        
        guard let guidType,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        let guidTypeFromInstance = System_Object_GetType(guid,
                                                         &exception)
        
        guard let guidTypeFromInstance,
              exception == nil else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        let equals = System_Object_Equals(guidType,
                                          guidTypeFromInstance,
                                          &exception)
        
        guard exception == nil else {
            XCTFail("Equals should not throw")
            
            return
        }
        
        XCTAssertEqual(equals, .yes)
    }
    
    func testEnum() {
        var exception: System_Exception_t?
        
        let enumValue = NativeAOT_CodeGeneratorInputSample_TestEnum.secondCase
        
        let enumNameC = NativeAOT_CodeGeneratorInputSample_TestClass_GetTestEnumName(enumValue,
                                                                                     &exception)
        
        guard exception == nil else {
            XCTFail("Should not throw")
            
            return
        }
        
        guard let enumNameC else {
            XCTFail("Should have enum name")
            
            return
        }
        
        defer {
            enumNameC.deallocate()
        }
        
        let enumName = String(cString: enumNameC)
        
        XCTAssertEqual("SecondCase", enumName)
    }
}
