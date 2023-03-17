import XCTest
import NativeAOTCodeGeneratorOutputSample

final class PersonTests: XCTestCase {
    func testPerson() {
        let firstName = "John"
        let lastName = "Doe"
        let age: Int32 = 24
        let expectedNiceLevel: NativeAOT_CodeGeneratorInputSample_NiceLevels = .veryNice
        let expectedNiceLevelString = "Very nice"
        
        let expectedFullName = "\(firstName) \(lastName)"
        let expectedWelcomeMessage = "Welcome, \(expectedFullName)! You're \(age) years old and \(expectedNiceLevelString)."
        
        var exception: System_Exception_t?
        
        let ageWhenBorn = NativeAOT_CodeGeneratorInputSample_Person_AGE_WHEN_BORN_Get()
        XCTAssertEqual(0, ageWhenBorn)
        
        let defaultAge = NativeAOT_CodeGeneratorInputSample_Person_DEFAULT_AGE_Get()
        XCTAssertEqual(ageWhenBorn, defaultAge)
        
        let newDefaultAge: Int32 = 5
        NativeAOT_CodeGeneratorInputSample_Person_DEFAULT_AGE_Set(newDefaultAge)
        
        let newRetrievedDefaultAge = NativeAOT_CodeGeneratorInputSample_Person_DEFAULT_AGE_Get()
        XCTAssertEqual(newDefaultAge, newRetrievedDefaultAge)
        
        var person: NativeAOT_CodeGeneratorInputSample_Person_t?
        
        firstName.withCString { firstNameC in
            lastName.withCString { lastNameC in
                person = NativeAOT_CodeGeneratorInputSample_Person_Create1(firstNameC,
                                                                           lastNameC,
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
        
        let personAge = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
                                                                          &exception)
        
        guard exception == nil else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(newDefaultAge, personAge)
        
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
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(newDefaultAge, retrievedAge)
        
        NativeAOT_CodeGeneratorInputSample_Person_Age_Set(person,
                                                          age,
                                                          &exception)
        
        guard exception == nil else {
            XCTFail("Person.Age setter should not throw")
            
            return
        }
        
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
        
        let numberOfChildren = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(person,
                                                                                              &exception)
        
        guard exception == nil else {
            XCTFail("Person.NumberOfChildren getter should not throw")
            
            return
        }
        
        XCTAssertEqual(0, numberOfChildren)
    }
    
    func testPersonChildren() {
        var exception: System_Exception_t?
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create("Johanna",
                                                                            "Doe",
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create("Max",
                                                                         "Doe",
                                                                         4,
                                                                         &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(son) }
        
        NativeAOT_CodeGeneratorInputSample_Person_AddChild(mother,
                                                           son,
                                                           &exception)
        
        guard exception == nil else {
            XCTFail("Person.AddChild should not throw")
            
            return
        }
        
        let numberOfChildren = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
                                                                                              &exception)
        
        guard exception == nil else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let firstChild = NativeAOT_CodeGeneratorInputSample_Person_ChildAt(mother,
                                                                                 0,
                                                                                 &exception),
              exception == nil else {
            XCTFail("Person.ChildAt should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(firstChild) }
        
        let firstChildEqualsSon = System_Object_ReferenceEquals(firstChild,
                                                                son,
                                                                &exception)
        
        guard exception == nil else {
            XCTFail("System.Object.ReferenceEquals should not throw")
            
            return
        }
        
        XCTAssertEqual(CBool.yes, firstChildEqualsSon)
        
        NativeAOT_CodeGeneratorInputSample_Person_RemoveChild(mother,
                                                              son,
                                                              &exception)
        
        guard exception == nil else {
            XCTFail("Person.RemoveChild should not throw")
            
            return
        }
        
        NativeAOT_CodeGeneratorInputSample_Person_RemoveChildAt(mother,
                                                                0,
                                                                &exception)
        
        guard let exception else {
            XCTFail("Person.RemoveChild should throw because the sole child has been removed previously")
            
            return
        }
        
        defer { System_Exception_Destroy(exception) }
        
        var exception2: System_Exception_t?
        
        guard let exceptionMessageC = System_Exception_Message_Get(exception,
                                                                   &exception2),
              exception2 == nil else {
            XCTFail("Exception.Message getter should not throw and return an instance of a string")
            
            return
        }
        
        let exceptionMessage = String(cString: exceptionMessageC)
        
        exceptionMessageC.deallocate()
        
        XCTAssertTrue(exceptionMessage.contains("Index was out of range"))
    }
    
    func testPersonChildrenArray() {
        var exception: System_Exception_t?
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create("Johanna",
                                                                            "Doe",
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create("Max",
                                                                         "Doe",
                                                                         4,
                                                                         &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(son) }
        
        NativeAOT_CodeGeneratorInputSample_Person_AddChild(mother,
                                                           son,
                                                           &exception)
        
        guard exception == nil else {
            XCTFail("Person.AddChild should not throw")
            
            return
        }
        
        let numberOfChildren = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
                                                                                              &exception)
        
        guard exception == nil else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let childrenArray = NativeAOT_CodeGeneratorInputSample_Person_ChildrenAsArray_Get(mother,
                                                                                                &exception), exception == nil else {
            XCTFail("Person.ChildrenAsArray should not throw and return an instance")
            
            return
        }
        
        let childrenLength = System_Array_GetLength(childrenArray,
                                                    0,
                                                    &exception)
        
        guard exception == nil else {
            XCTFail("System.Array.GetLength should not throw")
            
            return
        }
        
        XCTAssertEqual(numberOfChildren, childrenLength)
        
        guard let firstChild = System_Array_GetValue(childrenArray,
                                                     0,
                                                     &exception),
              exception == nil else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(firstChild) }
        
        let firstChildEqualsSon = System_Object_ReferenceEquals(firstChild,
                                                                son,
                                                                &exception)
        
        guard exception == nil else {
            XCTFail("System.Object.ReferenceEquals should not throw")
            
            return
        }
        
        XCTAssertEqual(CBool.yes, firstChildEqualsSon)
    }
    
    func testPersonExtensionMethods() {
        var exception: System_Exception_t?
        
        let initialAge: Int32 = 0
        
        guard let baby = NativeAOT_CodeGeneratorInputSample_Person_Create("Johanna",
                                                                          "Doe",
                                                                          0,
                                                                          &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(baby) }
        
        let increaseAgeByYears: Int32 = 4
        
        NativeAOT_CodeGeneratorInputSample_Person_Extensions_IncreaseAge(baby,
                                                                         increaseAgeByYears,
                                                                         &exception)
        
        XCTAssertNil(exception)
        
        let expectedAge = initialAge + increaseAgeByYears
        
        let age = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(baby,
                                                                    &exception)
        
        XCTAssertNil(exception)
        
        XCTAssertEqual(expectedAge, age)
    }
	
	func testPersonChangeAge() {
		var exception: System_Exception_t?
		
		let initialAge: Int32 = 0
		
		guard let person = NativeAOT_CodeGeneratorInputSample_Person_Create("Johanna",
																			"Doe",
																			initialAge,
																			&exception),
			  exception == nil else {
			XCTFail("Person ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(person) }
		
		let ageAfterCreation = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
																				 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(initialAge, ageAfterCreation)
		
		let newAgeProviderFunction: NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CFunction_t = { _ in
			return 10
		}
		
		let context = malloc(10)
		
		defer { free(context) }
		
		guard let newAgeProviderDelegate = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Create(context,
																												   newAgeProviderFunction,
																												   nil) else {
			XCTFail("Person.NewAgeProviderDelegate ctor should return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Destroy(newAgeProviderDelegate) }
		
		NativeAOT_CodeGeneratorInputSample_Person_ChangeAge(person,
															newAgeProviderDelegate,
															&exception)
		
		XCTAssertNil(exception)
		
		let age = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
																	&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(10, age)
		
		let retrievedContext = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Context_Get(newAgeProviderDelegate)
		XCTAssertEqual(context, retrievedContext)
		
		let retrievedCFunction = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CFunction_Get(newAgeProviderDelegate)
		XCTAssertNotNil(retrievedCFunction)
		
		let retrievedCDestructorFunction = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CDestructorFunction_Get(newAgeProviderDelegate)
		XCTAssertNil(retrievedCDestructorFunction)
	}
}
