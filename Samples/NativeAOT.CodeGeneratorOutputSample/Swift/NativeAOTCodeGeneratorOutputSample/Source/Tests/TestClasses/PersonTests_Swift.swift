import XCTest
import NativeAOTCodeGeneratorOutputSample

final class PersonTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testPerson() {
        let firstName = "John"
        let firstNameDN = firstName.dotNETString()
        
        let lastName = "Doe"
        let lastNameDN = lastName.dotNETString()
        
        let age: Int32 = 24
        let expectedNiceLevel: NativeAOT_CodeGeneratorInputSample_NiceLevels = .veryNice
        let expectedNiceLevelString = "Very nice"
        
        let expectedFullName = "\(firstName) \(lastName)"
        let expectedWelcomeMessage = "Welcome, \(expectedFullName)! You're \(age) years old and \(expectedNiceLevelString)."
        
        let ageWhenBorn = NativeAOT_CodeGeneratorInputSample_Person.aGE_WHEN_BORN_get()
        XCTAssertEqual(0, ageWhenBorn)
        
        let defaultAge = NativeAOT_CodeGeneratorInputSample_Person.dEFAULT_AGE_get()
        XCTAssertEqual(ageWhenBorn, defaultAge)
        
        guard let person = try? NativeAOT_CodeGeneratorInputSample_Person(firstNameDN,
                                                                          lastNameDN) else {
            XCTFail("Person initializer should not throw and return an instance")
            
            return
        }
        
        guard let personAge = try? person.age_get() else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(defaultAge, personAge)
        
        XCTAssertNoThrow(try person.niceLevel_set(expectedNiceLevel))
        
        guard let retrievedNiceLevel = try? person.niceLevel_get() else {
            XCTFail("Person.NiceLevel getter should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedNiceLevel, retrievedNiceLevel)
        
        guard let retrievedFirstName = try? person.firstName_get()?.string() else {
            XCTFail("Person.FirstName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(firstName, retrievedFirstName)
        
        guard let retrievedLastName = try? person.lastName_get()?.string() else {
            XCTFail("Person.LastName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(lastName, retrievedLastName)
        
        guard let retrievedFullName = try? person.fullName_get()?.string() else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedFullName, retrievedFullName)
        
        guard let retrievedAge = try? person.age_get() else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(defaultAge, retrievedAge)
        
        XCTAssertNoThrow(try person.age_set(age))
        
        guard let retrievedWelcomeMessage = try? person.getWelcomeMessage()?.string() else {
            XCTFail("Person.GetWelcomeMessage should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedWelcomeMessage, retrievedWelcomeMessage)
        
        let newFirstName = "Max 😉"
        let newFirstNameDN = newFirstName.dotNETString()
        
        let expectedNewFullName = "\(newFirstName) \(lastName)"
        
        XCTAssertNoThrow(try person.firstName_set(newFirstNameDN))
        
        guard let newFullName = try? person.fullName_get()?.string() else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedNewFullName, newFullName)
        
        guard let numberOfChildren = try? person.numberOfChildren_get() else {
            XCTFail("Person.NumberOfChildren getter should not throw")
            
            return
        }
        
        XCTAssertEqual(0, numberOfChildren)
    }
    
    func testPersonChildren() {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let mother = try? NativeAOT_CodeGeneratorInputSample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? NativeAOT_CodeGeneratorInputSample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let numberOfChildren = try? mother.numberOfChildren_get() else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let firstChild = try? mother.childAt(0) else {
            XCTFail("Person.ChildAt should not throw and return an instance")
            
            return
        }
        
        let firstChildEqualsSon = firstChild === son
        XCTAssertTrue(firstChildEqualsSon)
        
        XCTAssertNoThrow(try mother.removeChild(son))
        
        do {
            try mother.removeChildAt(0)
            
            XCTFail("Person.RemoveChild should throw because the sole child has been removed previously")
        } catch {
            let errorMessage = error.localizedDescription
            
            XCTAssertTrue(errorMessage.contains("Index was out of range"))
        }
    }
    
    func testPersonChildrenArray() {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let mother = try? NativeAOT_CodeGeneratorInputSample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? NativeAOT_CodeGeneratorInputSample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let numberOfChildren = try? mother.numberOfChildren_get() else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let children = try? mother.children_get() else {
            XCTFail("Person.ChildrenAsArray should not throw and return an instance")
            
            return
        }
        
        guard let childrenLength = try? children.length_get() else {
            XCTFail("System.Array.GetLength should not throw")
            
            return
        }
        
        XCTAssertEqual(numberOfChildren, childrenLength)
        
        guard let firstChild = try? children.getValue(0 as Int32) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        let firstChildEqualsSon = firstChild === son
        XCTAssertTrue(firstChildEqualsSon)
    }
    
    func testPersonExtensionMethods() {
        let initialAge: Int32 = 0
        
        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let baby = try? NativeAOT_CodeGeneratorInputSample_Person(firstNameDN,
                                                                        lastNameDN,
                                                                        0) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        let increaseAgeByYears: Int32 = 4
        
        // TODO: This should be refactored to be a Swift extension on the Person class
        XCTAssertNoThrow(try NativeAOT_CodeGeneratorInputSample_Person_Extensions.increaseAge(baby,
                                                                                              increaseAgeByYears))
        
        let expectedAge = initialAge + increaseAgeByYears
        
        guard let age = try? baby.age_get() else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedAge, age)
    }
    
    // TODO: Swiftify
//    func testPersonChangeAge() {
//        var exception: System_Exception_t?
//
//        let initialAge: Int32 = 0
//
//        let firstNameDN = "Johanna".cDotNETString()
//        defer { System_String_Destroy(firstNameDN) }
//
//        let lastNameDN = "Doe".cDotNETString()
//        defer { System_String_Destroy(lastNameDN) }
//
//        guard let person = NativeAOT_CodeGeneratorInputSample_Person_Create(firstNameDN,
//                                                                            lastNameDN,
//                                                                            initialAge,
//                                                                            &exception),
//              exception == nil else {
//            XCTFail("Person ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(person) }
//
//        let ageAfterCreation = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
//                                                                                 &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertEqual(initialAge, ageAfterCreation)
//
//        let newAgeProviderFunction: NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CFunction_t = { _ in
//            return 10
//        }
//
//        class Context {
//            var numberOfTimesDestructorWasCalled = 0
//        }
//
//        let swiftyContext = Context()
//        let contextBox = NativeBox(swiftyContext)
//        let context = contextBox.retainedPointer()
//
//        let destructorFunction: NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CDestructorFunction_t = { innerContext in
//            guard let innerContext else {
//                XCTFail("No context")
//
//                return
//            }
//
//            let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
//            let innerSwiftyContext = innerContextBox.value
//
//            innerSwiftyContext.numberOfTimesDestructorWasCalled += 1
//
//            XCTAssertEqual(1, innerSwiftyContext.numberOfTimesDestructorWasCalled)
//
//            innerContextBox.release(innerContext)
//        }
//
//        guard let newAgeProviderDelegate = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Create(context,
//                                                                                                                   newAgeProviderFunction,
//                                                                                                                   destructorFunction) else {
//            XCTFail("Person.NewAgeProviderDelegate ctor should return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Destroy(newAgeProviderDelegate) }
//
//        NativeAOT_CodeGeneratorInputSample_Person_ChangeAge(person,
//                                                            newAgeProviderDelegate,
//                                                            &exception)
//
//        XCTAssertNil(exception)
//
//        let age = NativeAOT_CodeGeneratorInputSample_Person_Age_Get(person,
//                                                                    &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertEqual(10, age)
//
//        let retrievedContext = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Context_Get(newAgeProviderDelegate)
//        XCTAssertEqual(context, retrievedContext)
//
//        let retrievedCFunction = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CFunction_Get(newAgeProviderDelegate)
//        XCTAssertNotNil(retrievedCFunction)
//
//        let retrievedCDestructorFunction = NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_CDestructorFunction_Get(newAgeProviderDelegate)
//        XCTAssertNotNil(retrievedCDestructorFunction)
//    }
    
    func testPersonAddress() {
        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let person = try? NativeAOT_CodeGeneratorInputSample_Person(firstNameDN,
                                                                          lastNameDN,
                                                                          15) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        let street = "Stephansplatz"
        let streetDN = street.dotNETString()
        
        let city = "Vienna"
        let cityDN = city.dotNETString()
        
        guard let address = try? NativeAOT_CodeGeneratorInputSample_Address(streetDN,
                                                                            cityDN) else {
            XCTFail("Address ctor should return an instance and not throw")
            
            return
        }
        
        XCTAssertNoThrow(try person.address_set(address))
        
        guard let retrievedAddress = try? person.address_get() else {
            XCTFail("Person.Address getter should return an instance and not throw")
            
            return
        }
        
        guard let retrievedStreet = try? retrievedAddress.street_get()?.string() else {
            XCTFail("Address.Street getter should return an instance and not throw")
            
            return
        }
        
        XCTAssertEqual(street, retrievedStreet)
        
        guard let retrievedCity = try? retrievedAddress.city_get()?.string() else {
            XCTFail("Address.City getter should return an instance and not throw")
            
            return
        }
        
        XCTAssertEqual(city, retrievedCity)
    }
    
    // TODO: Swiftify
//    func testPersonEvents() {
//        typealias NumberOfChildrenChangedDelegate_Closure = (_ context: Context) -> Void
//
//        class Context {
//            var delegateClosure: NumberOfChildrenChangedDelegate_Closure
//            var numberOfTimesNumberOfChildrenChangedWasCalled: Int32 = 0
//
//            init(delegateClosure: @escaping NumberOfChildrenChangedDelegate_Closure) {
//                self.delegateClosure = delegateClosure
//            }
//
//            func invokeClosure() {
//                delegateClosure(self)
//            }
//        }
//
//        var exception: System_Exception_t?
//
//        let motherFirstNameDN = "Johanna".cDotNETString()
//        defer { System_String_Destroy(motherFirstNameDN) }
//
//        let sonFirstNameDN = "Max".cDotNETString()
//        defer { System_String_Destroy(sonFirstNameDN) }
//
//        let daugtherFirstNameDN = "Marie".cDotNETString()
//        defer { System_String_Destroy(daugtherFirstNameDN) }
//
//        let lastNameDN = "Doe".cDotNETString()
//        defer { System_String_Destroy(lastNameDN) }
//
//        guard let mother = NativeAOT_CodeGeneratorInputSample_Person_Create(motherFirstNameDN,
//                                                                            lastNameDN,
//                                                                            40,
//                                                                            &exception),
//              exception == nil else {
//            XCTFail("Person ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(mother) }
//
//        let swiftyContext = Context(delegateClosure: { innerSwiftyContext in
//            innerSwiftyContext.numberOfTimesNumberOfChildrenChangedWasCalled += 1
//        })
//
//        let contextBox = NativeBox(swiftyContext)
//        let context = contextBox.retainedPointer()
//
//        let destructor: NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_CDestructorFunction_t = { innerContext in
//            guard let innerContext else {
//                XCTFail("Context is nil")
//
//                return
//            }
//
//            NativeBox<Context>.release(innerContext)
//        }
//
//        let numberOfChildrenChangedHandler: NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_CFunction_t = { innerContext in
//            guard let innerContext else {
//                XCTFail("Context is nil")
//
//                return
//            }
//
//            let innerContextBox = NativeBox<Context>.fromPointer(innerContext)
//            let innerSwiftyContext = innerContextBox.value
//
//            innerSwiftyContext.invokeClosure()
//        }
//
//        guard let numberOfChildrenChangedDelegate = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_Create(context,
//                                                                                                                                     numberOfChildrenChangedHandler,
//                                                                                                                                     destructor) else {
//            XCTFail("Number of children changed delegate ctor should return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_Destroy(numberOfChildrenChangedDelegate) }
//
//        NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChanged_Add(mother,
//                                                                              numberOfChildrenChangedDelegate)
//
//        guard let son = NativeAOT_CodeGeneratorInputSample_Person_Create(sonFirstNameDN,
//                                                                         lastNameDN,
//                                                                         4,
//                                                                         &exception),
//              exception == nil else {
//            XCTFail("Person ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(son) }
//
//        NativeAOT_CodeGeneratorInputSample_Person_AddChild(mother,
//                                                           son,
//                                                           &exception)
//
//        guard exception == nil else {
//            XCTFail("Person.AddChild should not throw")
//
//            return
//        }
//
//        guard let daugther = NativeAOT_CodeGeneratorInputSample_Person_Create(daugtherFirstNameDN,
//                                                                              lastNameDN,
//                                                                              10,
//                                                                              &exception),
//              exception == nil else {
//            XCTFail("Person ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { NativeAOT_CodeGeneratorInputSample_Person_Destroy(daugther) }
//
//        NativeAOT_CodeGeneratorInputSample_Person_AddChild(mother,
//                                                           daugther,
//                                                           &exception)
//
//        guard exception == nil else {
//            XCTFail("Person.AddChild should not throw")
//
//            return
//        }
//
//        let numberOfChildren = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
//                                                                                              &exception)
//
//        guard exception == nil else {
//            XCTFail("Person.NumberOfChildren should not throw")
//
//            return
//        }
//
//        let expectedNumberOfChildren: Int32 = 2
//
//        XCTAssertEqual(expectedNumberOfChildren, numberOfChildren)
//        XCTAssertEqual(expectedNumberOfChildren, swiftyContext.numberOfTimesNumberOfChildrenChangedWasCalled)
//
//        NativeAOT_CodeGeneratorInputSample_Person_RemoveChild(mother,
//                                                              daugther,
//                                                              &exception)
//
//        XCTAssertNil(exception)
//
//        XCTAssertEqual(3, swiftyContext.numberOfTimesNumberOfChildrenChangedWasCalled)
//
//        NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChanged_Remove(mother,
//                                                                                 numberOfChildrenChangedDelegate)
//
//        NativeAOT_CodeGeneratorInputSample_Person_RemoveChildAt(mother,
//                                                                0,
//                                                                &exception)
//
//        XCTAssertNil(exception)
//
//        XCTAssertEqual(3, swiftyContext.numberOfTimesNumberOfChildrenChangedWasCalled)
//
//        let numberOfChildrenAfterRemoval = NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(mother,
//                                                                                                          &exception)
//
//        guard exception == nil else {
//            XCTFail("Person.NumberOfChildren should not throw")
//
//            return
//        }
//
//        let expectedNumberOfChildrenAfterRemoval: Int32 = 0
//
//        XCTAssertEqual(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
//    }
    
    func testPersonChildrenArrayChange() {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let daugtherFirstNameDN = "Marie".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let mother = try? NativeAOT_CodeGeneratorInputSample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? NativeAOT_CodeGeneratorInputSample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let originalChildren = try? mother.children_get() else {
            XCTFail("Person.Children getter should not throw and return an instance")
            
            return
        }
        
        guard let numberOfChildrenBeforeDaugther = try? originalChildren.length_get() else {
            XCTFail("System.Array.Length getter should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildrenBeforeDaugther)
        
        let personType = NativeAOT_CodeGeneratorInputSample_Person.typeOf()
        
        guard let newChildren = try? System_Array.createInstance(personType,
                                                                 2)?.castAs(NativeAOT_CodeGeneratorInputSample_Person_Array.self) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try System_Array.copy(originalChildren,
                                               newChildren,
                                               1 as Int32))
        
        guard let daugther = try? NativeAOT_CodeGeneratorInputSample_Person(daugtherFirstNameDN,
                                                                            lastNameDN,
                                                                            10) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try newChildren.setValue(daugther, 1 as Int32))
        
        XCTAssertNoThrow(try mother.children_set(newChildren))
        
        guard let numberOfChildrenAfterDaugther = try? mother.numberOfChildren_get() else {
            XCTFail("Person.NumberOfChildren getter should not throw")
            
            return
        }
        
        XCTAssertEqual(2, numberOfChildrenAfterDaugther)
    }
}
