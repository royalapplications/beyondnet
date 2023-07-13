import XCTest
import BeyondNETSampleSwift

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
        let expectedNiceLevel: Beyond_NET_Sample_NiceLevels = .veryNice
        let expectedNiceLevelString = "Very nice"
        
        let expectedFullName = "\(firstName) \(lastName)"
        let expectedWelcomeMessage = "Welcome, \(expectedFullName)! You're \(age) years old and \(expectedNiceLevelString)."
        
        let ageWhenBorn = Beyond_NET_Sample_Person.aGE_WHEN_BORN
        XCTAssertEqual(0, ageWhenBorn)
        
        let defaultAge = Beyond_NET_Sample_Person.dEFAULT_AGE
        XCTAssertEqual(ageWhenBorn, defaultAge)
        
        guard let person = try? Beyond_NET_Sample_Person(firstNameDN,
                                                                          lastNameDN) else {
            XCTFail("Person initializer should not throw and return an instance")
            
            return
        }
        
        guard let personAge = try? person.age else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(defaultAge, personAge)
        
        XCTAssertNoThrow(try person.niceLevel_set(expectedNiceLevel))
        
        guard let retrievedNiceLevel = try? person.niceLevel else {
            XCTFail("Person.NiceLevel getter should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedNiceLevel, retrievedNiceLevel)
        
        guard let retrievedFirstName = try? person.firstName?.string() else {
            XCTFail("Person.FirstName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(firstName, retrievedFirstName)
        
        guard let retrievedLastName = try? person.lastName?.string() else {
            XCTFail("Person.LastName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(lastName, retrievedLastName)
        
        guard let retrievedFullName = try? person.fullName?.string() else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedFullName, retrievedFullName)
        
        guard let retrievedAge = try? person.age else {
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
        
        guard let newFullName = try? person.fullName?.string() else {
            XCTFail("Person.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedNewFullName, newFullName)
        
        guard let numberOfChildren = try? person.numberOfChildren else {
            XCTFail("Person.NumberOfChildren getter should not throw")
            
            return
        }
        
        XCTAssertEqual(0, numberOfChildren)
    }
    
    func testPersonChildren() {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let mother = try? Beyond_NET_Sample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? Beyond_NET_Sample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let numberOfChildren = try? mother.numberOfChildren else {
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
        
        guard let mother = try? Beyond_NET_Sample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? Beyond_NET_Sample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let numberOfChildren = try? mother.numberOfChildren else {
            XCTFail("Person.NumberOfChildren should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let children = try? mother.children else {
            XCTFail("Person.ChildrenAsArray should not throw and return an instance")
            
            return
        }
        
        guard let childrenLength = try? children.length else {
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
        
        guard let baby = try? Beyond_NET_Sample_Person(firstNameDN,
                                                                        lastNameDN,
                                                                        0) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        let increaseAgeByYears: Int32 = 4
        
        XCTAssertNoThrow(try baby.increaseAge(increaseAgeByYears))
        
        let expectedAge = initialAge + increaseAgeByYears
        
        guard let age = try? baby.age else {
            XCTFail("Person.Age getter should not throw")
            
            return
        }
        
        XCTAssertEqual(expectedAge, age)
		
		var nilAddressRet: Beyond_NET_Sample_Address?
		let nilAddressSuccess: Bool
		
		do {
			nilAddressSuccess = try baby.tryGetAddress(&nilAddressRet)
		} catch {
			XCTFail("Person.TryGetAddress should not throw")
			
			return
		}
		
		XCTAssertFalse(nilAddressSuccess)
		XCTAssertNil(nilAddressRet)
		
		guard let address = try? Beyond_NET_Sample_Address("Street Name".dotNETString(),
																			"City Name".dotNETString()) else {
			XCTFail("Address ctor should not throw")
			
			return
		}
		
		XCTAssertNoThrow(try baby.address_set(address))
		
		var addressRet: Beyond_NET_Sample_Address?
		let addressSuccess: Bool
		
		do {
			addressSuccess = try baby.tryGetAddress(&addressRet)
		} catch {
			XCTFail("Person.TryGetAddress should not throw")
			
			return
		}
		
		XCTAssertTrue(addressSuccess)
		XCTAssertNotNil(addressRet)
    }
    
    func testPersonChangeAge() {
        let initialAge: Int32 = 0

        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()

		guard let person = try? Beyond_NET_Sample_Person(firstNameDN,
																		  lastNameDN,
																		  initialAge) else {
            XCTFail("Person ctor should not throw and return an instance")

            return
        }

		let ageAfterCreation = (try? person.age) ?? -1
        XCTAssertEqual(initialAge, ageAfterCreation)

		guard let newAgeProviderDelegate = Beyond_NET_Sample_Person_NewAgeProviderDelegate({
			10
		}) else {
            XCTFail("Person.NewAgeProviderDelegate ctor should return an instance")

            return
        }

		XCTAssertNoThrow(try person.changeAge(newAgeProviderDelegate))

		let age = (try? person.age) ?? -1
        XCTAssertEqual(10, age)
    }
    
    func testPersonAddress() {
        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let person = try? Beyond_NET_Sample_Person(firstNameDN,
                                                                          lastNameDN,
                                                                          15) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        let street = "Stephansplatz"
        let streetDN = street.dotNETString()
        
        let city = "Vienna"
        let cityDN = city.dotNETString()
        
        guard let address = try? Beyond_NET_Sample_Address(streetDN,
                                                                            cityDN) else {
            XCTFail("Address ctor should return an instance and not throw")
            
            return
        }
        
        XCTAssertNoThrow(try person.address_set(address))
        
        guard let retrievedAddress = try? person.address else {
            XCTFail("Person.Address getter should return an instance and not throw")
            
            return
        }
        
        guard let retrievedStreet = try? retrievedAddress.street?.string() else {
            XCTFail("Address.Street getter should return an instance and not throw")
            
            return
        }
        
        XCTAssertEqual(street, retrievedStreet)
        
        guard let retrievedCity = try? retrievedAddress.city?.string() else {
            XCTFail("Address.City getter should return an instance and not throw")
            
            return
        }
        
        XCTAssertEqual(city, retrievedCity)
    }
    
    func testPersonEvents() {
        var numberOfTimesNumberOfChildrenChangedWasCalled: Int32 = 0

        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let daugtherFirstNameDN = "Marie".dotNETString()
        let lastNameDN = "Doe".dotNETString()
		
		guard let mother = try? Beyond_NET_Sample_Person(motherFirstNameDN,
																		  lastNameDN,
																		  40) else {
            XCTFail("Person ctor should not throw and return an instance")

            return
        }

		guard let numberOfChildrenChangedDelegate = Beyond_NET_Sample_Person_NumberOfChildrenChangedDelegate({
			numberOfTimesNumberOfChildrenChangedWasCalled += 1
		}) else {
			XCTFail("Number of children changed delegate ctor should return an instance")

			return
		}
		
		mother.numberOfChildrenChanged_add(numberOfChildrenChangedDelegate)

		guard let son = try? Beyond_NET_Sample_Person(sonFirstNameDN,
																	   lastNameDN,
																	   4) else {
            XCTFail("Person ctor should not throw and return an instance")

            return
        }
		
		XCTAssertNoThrow(try mother.addChild(son))
		
		guard let daugther = try? Beyond_NET_Sample_Person(daugtherFirstNameDN,
																			lastNameDN,
																			10) else {
            XCTFail("Person ctor should not throw and return an instance")

            return
        }
		
		XCTAssertNoThrow(try mother.addChild(daugther))

		let numberOfChildren = (try? mother.numberOfChildren) ?? -1

        let expectedNumberOfChildren: Int32 = 2

        XCTAssertEqual(expectedNumberOfChildren, numberOfChildren)
        XCTAssertEqual(expectedNumberOfChildren, numberOfTimesNumberOfChildrenChangedWasCalled)

		XCTAssertNoThrow(try mother.removeChild(daugther))

        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)
		
		mother.numberOfChildrenChanged_remove(numberOfChildrenChangedDelegate)
		
		XCTAssertNoThrow(try mother.removeChildAt(0))
        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)

		let numberOfChildrenAfterRemoval = (try? mother.numberOfChildren) ?? -1
        let expectedNumberOfChildrenAfterRemoval: Int32 = 0
        XCTAssertEqual(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
    }
    
    func testPersonChildrenArrayChange() {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let daugtherFirstNameDN = "Marie".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        guard let mother = try? Beyond_NET_Sample_Person(motherFirstNameDN,
                                                                          lastNameDN,
                                                                          40) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        guard let son = try? Beyond_NET_Sample_Person(sonFirstNameDN,
                                                                       lastNameDN,
                                                                       4) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try mother.addChild(son))
        
        guard let originalChildren = try? mother.children else {
            XCTFail("Person.Children getter should not throw and return an instance")
            
            return
        }
        
        guard let numberOfChildrenBeforeDaugther = try? originalChildren.length else {
            XCTFail("System.Array.Length getter should not throw")
            
            return
        }
        
        XCTAssertEqual(1, numberOfChildrenBeforeDaugther)
        
        let personType = Beyond_NET_Sample_Person.typeOf
        
        guard let newChildren = try? System_Array.createInstance(personType,
                                                                 2)?.castAs(Beyond_NET_Sample_Person_Array.self) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try System_Array.copy(originalChildren,
                                               newChildren,
                                               1 as Int32))
        
        guard let daugther = try? Beyond_NET_Sample_Person(daugtherFirstNameDN,
                                                                            lastNameDN,
                                                                            10) else {
            XCTFail("Person ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try newChildren.setValue(daugther, 1 as Int32))
        
        XCTAssertNoThrow(try mother.children_set(newChildren))
        
        guard let numberOfChildrenAfterDaugther = try? mother.numberOfChildren else {
            XCTFail("Person.NumberOfChildren getter should not throw")
            
            return
        }
        
        XCTAssertEqual(2, numberOfChildrenAfterDaugther)
    }
}
