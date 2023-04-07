import XCTest
import NativeAOTCodeGeneratorOutputSample

final class PersonTests: XCTestCase {
    func testPerson() {
        let firstName = "John"
		let firstNameDN = firstName.dotNETString()
		defer { System_String_Destroy(firstNameDN) }
		
        let lastName = "Doe"
		let lastNameDN = lastName.dotNETString()
		defer { System_String_Destroy(lastNameDN) }
		
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
        
        guard let person = NativeAOT_CodeGeneratorInputSample_Person_Create_1(firstNameDN,
																			  lastNameDN,
																			  &exception),
              exception == nil else {
            XCTFail("Person initializer should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(person) }
        
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
        
        guard let retrievedFirstName =  String(dotNETString: NativeAOT_CodeGeneratorInputSample_Person_FirstName_Get(person,
																													 &exception),
											   destroyDotNETString: true),
              exception == nil else {
            XCTFail("Person.FirstName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(firstName, retrievedFirstName)
        
        guard let retrievedLastName = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Person_LastName_Get(person,
																												  &exception),
											 destroyDotNETString: true),
              exception == nil else {
            XCTFail("Person.LastName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(lastName, retrievedLastName)
        
        guard let retrievedFullName = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Person_FullName_Get(person,
																												  &exception),
											 destroyDotNETString: true),
              exception == nil else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedFullName, retrievedFullName)
        
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
        
        guard let retrievedWelcomeMessage = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Person_GetWelcomeMessage(person,
																															 &exception),
												   destroyDotNETString: true),
              exception == nil else {
            XCTFail("Person.GetWelcomeMessage should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedWelcomeMessage, retrievedWelcomeMessage)
        
        let newFirstName = "Max 😉"
		let newFirstNameDN = newFirstName.dotNETString()
		defer { System_String_Destroy(newFirstNameDN) }
		
        let expectedNewFullName = "\(newFirstName) \(lastName)"
        
		NativeAOT_CodeGeneratorInputSample_Person_FirstName_Set(person,
																newFirstNameDN,
																&exception)
        
        guard exception == nil else {
            XCTFail("Person.FirstName setter should not throw")
            
            return
        }
        
        guard let newFullName = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Person_FullName_Get(person,
																											&exception),
									   destroyDotNETString: true),
              exception == nil else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedNewFullName, newFullName)
        
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
		
		let motherFirstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(motherFirstNameDN) }
		
		let sonFirstNameDN = "Max".dotNETString()
		defer { System_String_Destroy(sonFirstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create(motherFirstNameDN,
                                                                            lastNameDN,
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create(sonFirstNameDN,
                                                                         lastNameDN,
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
        
        XCTAssertTrue(firstChildEqualsSon)
        
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
        
        guard let exceptionMessage = String(dotNETString: System_Exception_Message_Get(exception,
																					   &exception2),
											destroyDotNETString: true),
              exception2 == nil else {
            XCTFail("Exception.Message getter should not throw and return an instance of a string")
            
            return
        }
        
        XCTAssertTrue(exceptionMessage.contains("Index was out of range"))
    }
    
    func testPersonChildrenArray() {
        var exception: System_Exception_t?
		
		let motherFirstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(motherFirstNameDN) }
		
		let sonFirstNameDN = "Max".dotNETString()
		defer { System_String_Destroy(sonFirstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create(motherFirstNameDN,
                                                                            lastNameDN,
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create(sonFirstNameDN,
                                                                         lastNameDN,
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
        
        guard let children = NativeAOT_CodeGeneratorInputSample_Person_Children_Get(mother,
                                                                                    &exception), exception == nil else {
            XCTFail("Person.ChildrenAsArray should not throw and return an instance")
            
            return
        }
        
        let childrenLength = System_Array_GetLength(children,
                                                    0,
                                                    &exception)
        
        guard exception == nil else {
            XCTFail("System.Array.GetLength should not throw")
            
            return
        }
        
        XCTAssertEqual(numberOfChildren, childrenLength)
        
		guard let firstChild = System_Array_GetValue_1(children,
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
        
        XCTAssertTrue(firstChildEqualsSon)
    }
    
    func testPersonExtensionMethods() {
        var exception: System_Exception_t?
        
        let initialAge: Int32 = 0
		
		let firstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(firstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
        
        guard let baby = NativeAOT_CodeGeneratorInputSample_Person_Create(firstNameDN,
                                                                          lastNameDN,
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
		
		let firstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(firstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
		
		guard let person = NativeAOT_CodeGeneratorInputSample_Person_Create(firstNameDN,
																			lastNameDN,
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
		
		let swiftyContext = "Blah"
		let contextBox = NativeBox(swiftyContext)
		let context = contextBox.retainedPointer()
		
		let destructorFunction: NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CDestructorFunction_t = { innerContext in
			guard let innerContext else {
				XCTFail("No context")
				
				return
			}
			
			NativeBox<String>.release(innerContext)
		}
		
		guard let newAgeProviderDelegate = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Create(context,
																												   newAgeProviderFunction,
																												   destructorFunction) else {
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
		XCTAssertNotNil(retrievedCDestructorFunction)
	}
	
	func testPersonAddress() {
		var exception: System_Exception_t?
		
		let firstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(firstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
		
		guard let person = NativeAOT_CodeGeneratorInputSample_Person_Create(firstNameDN,
																			lastNameDN,
																			15,
																			&exception),
			  exception == nil else {
			XCTFail("Person ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(person) }
		
		let street = "Stephansplatz"
		let streetDN = street.dotNETString()
		defer { System_String_Destroy(streetDN) }
		
		let city = "Vienna"
		let cityDN = city.dotNETString()
		defer { System_String_Destroy(cityDN) }
		
		guard let address = NativeAOT_CodeGeneratorInputSample_Address_Create(streetDN,
																			  cityDN,
																			  &exception),
			  exception == nil else {
			XCTFail("Address ctor should return an instance and not throw")
			
			return
		}
		
		NativeAOT_CodeGeneratorInputSample_Person_Address_Set(person,
															  address,
															  &exception)
		
		XCTAssertNil(exception)
		
		guard let retrievedAddress = NativeAOT_CodeGeneratorInputSample_Person_Address_Get(person,
																						   &exception),
			  exception == nil else {
			XCTFail("Person.Address getter should return an instance and not throw")
			
			return
		}
		
		guard let retrievedStreet = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_Street_Get(retrievedAddress,
																											   &exception),
										   destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.Street getter should return an instance and not throw")
			
			return
		}
		
		XCTAssertEqual(street, retrievedStreet)
		
		guard let retrievedCity = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_City_Get(retrievedAddress,
																										   &exception),
										 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.City getter should return an instance and not throw")
			
			return
		}
		
		XCTAssertEqual(city, retrievedCity)
	}
    
    func testPersonEvents() {
        var exception: System_Exception_t?
		
		let motherFirstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(motherFirstNameDN) }
		
		let sonFirstNameDN = "Max".dotNETString()
		defer { System_String_Destroy(sonFirstNameDN) }
		
		let daugtherFirstNameDN = "Marie".dotNETString()
		defer { System_String_Destroy(daugtherFirstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create(motherFirstNameDN,
                                                                            lastNameDN,
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        var numberOfTimesNumberOfChildrenChangedWasCalled: Int32 = 0
        
        typealias NumberOfChildrenChangedDelegate_Closure = () -> Void
        
        let numberOfChildrenChangedHandlerSwift: NumberOfChildrenChangedDelegate_Closure = {
            numberOfTimesNumberOfChildrenChangedWasCalled += 1
        }
        
        let closureBox = NativeBox(numberOfChildrenChangedHandlerSwift)
        
        let context = closureBox.retainedPointer()
        
        let destructor: NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_CDestructorFunction_t = { innerContext in
            guard let innerContext else {
                XCTFail("Context is nil")
                
                return
            }
            
            NativeBox<NumberOfChildrenChangedDelegate_Closure>.release(innerContext)
        }
        
        let numberOfChildrenChangedHandler: NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_CFunction_t = { innerContext in
            guard let innerContext else {
                XCTFail("Context is nil")
                
                return
            }
            
            let innerClosureBox = NativeBox<NumberOfChildrenChangedDelegate_Closure>.fromPointer(innerContext)
            let closure = innerClosureBox.value
            
            closure()
        }
        
        guard let numberOfChildrenChangedDelegate = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_Create(context,
                                                                                                                                     numberOfChildrenChangedHandler,                                                    destructor) else {
            XCTFail("Number of children changed delegate ctor should return an instance")
            
            return
        }
        
        NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChanged_Add(mother,
                                                                              numberOfChildrenChangedDelegate)
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create(sonFirstNameDN,
                                                                         lastNameDN,
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
        
        guard let daugther = NativeAOT_CodeGeneratorInputSample_Person_Create(daugtherFirstNameDN,
                                                                              lastNameDN,
                                                                              10,
                                                                              &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(daugther) }
        
        NativeAOT_CodeGeneratorInputSample_Person_AddChild(mother,
                                                           daugther,
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
        
        let expectedNumberOfChildren: Int32 = 2
        
        XCTAssertEqual(expectedNumberOfChildren, numberOfChildren)
        XCTAssertEqual(expectedNumberOfChildren, numberOfTimesNumberOfChildrenChangedWasCalled)
        
        NativeAOT_CodeGeneratorInputSample_Person_RemoveChild(mother,
                                                              daugther,
                                                              &exception)
        
        XCTAssertNil(exception)
        
        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)
        
        NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChanged_Remove(mother,
                                                                                 numberOfChildrenChangedDelegate)
        
        NativeAOT_CodeGeneratorInputSample_Person_RemoveChildAt(mother,
                                                                0,
                                                                &exception)
        
        XCTAssertNil(exception)
        
        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)
        
        let numberOfChildrenAfterRemoval = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
                                                                                                          &exception)
        
        guard exception == nil else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        let expectedNumberOfChildrenAfterRemoval: Int32 = 0
        
        XCTAssertEqual(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
    }
    
    func testPersonChildrenArrayChange() {
        var exception: System_Exception_t?
		
		let motherFirstNameDN = "Johanna".dotNETString()
		defer { System_String_Destroy(motherFirstNameDN) }
		
		let sonFirstNameDN = "Max".dotNETString()
		defer { System_String_Destroy(sonFirstNameDN) }
		
		let daugtherFirstNameDN = "Marie".dotNETString()
		defer { System_String_Destroy(daugtherFirstNameDN) }
		
		let lastNameDN = "Doe".dotNETString()
		defer { System_String_Destroy(lastNameDN) }
        
        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create(motherFirstNameDN,
                                                                            lastNameDN,
                                                                            40,
                                                                            &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
        
        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create(sonFirstNameDN,
                                                                         lastNameDN,
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
        
        guard let originalChildren = NativeAOT_CodeGeneratorInputSample_Person_Children_Get(mother,
                                                                                            &exception),
              exception == nil else {
            XCTFail("Person.Children getter should not throw and return an instance")
            
            return
        }
        
        defer { System_Array_Destroy(originalChildren) }
        
        let numberOfChildrenBeforeDaugther = System_Array_Length_Get(originalChildren,
                                                                     &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(1, numberOfChildrenBeforeDaugther)
        
        let personType = NativeAOT_CodeGeneratorInputSample_Person_TypeOf()
        
        defer { System_Type_Destroy(personType) }
        
        guard let newChildren = System_Array_CreateInstance(personType,
                                                            2,
                                                            &exception),
              exception == nil else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        defer { System_Array_Destroy(newChildren) }
        
        System_Array_Copy(originalChildren,
                          newChildren,
                          1,
                          &exception)
        
        XCTAssertNil(exception)
        
        guard let daugther = NativeAOT_CodeGeneratorInputSample_Person_Create(daugtherFirstNameDN,
                                                                              lastNameDN,
                                                                              10,
                                                                              &exception),
              exception == nil else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(daugther) }
        
        System_Array_SetValue(newChildren,
                              daugther,
                              1,
                              &exception)
        
        XCTAssertNil(exception)
        
        NativeAOT_CodeGeneratorInputSample_Person_Children_Set(mother,
                                                               newChildren,
                                                               &exception)
        
        XCTAssertNil(exception)
        
        let numberOfChildrenAfterDaugther = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
                                                                                                           &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(2, numberOfChildrenAfterDaugther)
    }
}
