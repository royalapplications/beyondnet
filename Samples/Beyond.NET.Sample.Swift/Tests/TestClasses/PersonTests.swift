import XCTest
import BeyondDotNETSampleKit

final class PersonTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testPerson() throws {
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
        
        let person = try Beyond_NET_Sample_Person(firstNameDN,
                                                  lastNameDN)
        
        let personAge = try person.age
        XCTAssertEqual(defaultAge, personAge)
        
        try person.niceLevel_set(expectedNiceLevel)
        
        let retrievedNiceLevel = try person.niceLevel
        XCTAssertEqual(expectedNiceLevel, retrievedNiceLevel)
        
        let retrievedFirstName = try person.firstName.string()
        XCTAssertEqual(firstName, retrievedFirstName)
        
        let retrievedLastName = try person.lastName.string()
        XCTAssertEqual(lastName, retrievedLastName)
        
        let retrievedFullName = try person.fullName.string()
        XCTAssertEqual(expectedFullName, retrievedFullName)
        
        let retrievedAge = try person.age
        XCTAssertEqual(defaultAge, retrievedAge)
        
        try person.age_set(age)
        
        let retrievedWelcomeMessage = try person.getWelcomeMessage().string()
        XCTAssertEqual(expectedWelcomeMessage, retrievedWelcomeMessage)
        
        let newFirstName = "Max 😉"
        let newFirstNameDN = newFirstName.dotNETString()
        
        let expectedNewFullName = "\(newFirstName) \(lastName)"
        
        try person.firstName_set(newFirstNameDN)
        
        let newFullName = try person.fullName.string()
        XCTAssertEqual(expectedNewFullName, newFullName)
        
        let numberOfChildren = try person.numberOfChildren
        XCTAssertEqual(0, numberOfChildren)
    }
    
    func testPersonChildren() throws {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        let mother = try Beyond_NET_Sample_Person(motherFirstNameDN,
                                                  lastNameDN,
                                                  40)
        
        let son = try Beyond_NET_Sample_Person(sonFirstNameDN,
                                               lastNameDN,
                                               4)
        
        try mother.addChild(son)
        
        let numberOfChildren = try mother.numberOfChildren
        
        XCTAssertEqual(1, numberOfChildren)
        
        guard let firstChild = try? mother.childAt(0) else {
            XCTFail("Person.ChildAt should not throw and return an instance")
            
            return
        }
        
        let firstChildEqualsSon = firstChild === son
        XCTAssertTrue(firstChildEqualsSon)
        
        try mother.removeChild(son)
        
        do {
            try mother.removeChildAt(0)
            
            XCTFail("Person.RemoveChild should throw because the sole child has been removed previously")
        } catch {
            let errorMessage = error.localizedDescription
            
            XCTAssertTrue(errorMessage.contains("Index was out of range"))
        }
    }
    
    func testPersonChildrenArray() throws {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        let mother = try Beyond_NET_Sample_Person(motherFirstNameDN,
                                                  lastNameDN,
                                                  40)
        
        let son = try Beyond_NET_Sample_Person(sonFirstNameDN,
                                               lastNameDN,
                                               4)
        
        try mother.addChild(son)
        
        let numberOfChildren = try mother.numberOfChildren
        
        XCTAssertEqual(1, numberOfChildren)
        
        let children = try mother.children
        
        let childrenLength = try children.length
        
        XCTAssertEqual(numberOfChildren, childrenLength)
        
        guard let firstChild = try? children.getValue(0 as Int32) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        let firstChildEqualsSon = firstChild === son
        XCTAssertTrue(firstChildEqualsSon)
    }
    
    func testPersonExtensionMethods() throws {
        let initialAge: Int32 = 0
        
        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        let baby = try Beyond_NET_Sample_Person(firstNameDN,
                                                lastNameDN,
                                                0)
        
        let increaseAgeByYears: Int32 = 4
        
        try baby.increaseAge(increaseAgeByYears)
        
        let expectedAge = initialAge + increaseAgeByYears
        
        let age = try baby.age
        
        XCTAssertEqual(expectedAge, age)
		
		var nilAddressRet: Beyond_NET_Sample_Address?
		let nilAddressSuccess = try baby.tryGetAddress(&nilAddressRet)
		
		XCTAssertFalse(nilAddressSuccess)
		XCTAssertNil(nilAddressRet)
		
		let address = try Beyond_NET_Sample_Address("Street Name".dotNETString(),
                                                    "City Name".dotNETString())
		
		try baby.address_set(address)
		
		var addressRet: Beyond_NET_Sample_Address?
		let addressSuccess = try baby.tryGetAddress(&addressRet)
		
		XCTAssertTrue(addressSuccess)
		XCTAssertNotNil(addressRet)
    }
    
    func testPersonChangeAge() throws {
        let initialAge: Int32 = 0

        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()

        let person = try Beyond_NET_Sample_Person(firstNameDN,
                                                  lastNameDN,
                                                  initialAge)

		let ageAfterCreation = try person.age
        XCTAssertEqual(initialAge, ageAfterCreation)

		let newAgeProviderDelegate = Beyond_NET_Sample_Person_NewAgeProviderDelegate({
			10
		})

		try person.changeAge(newAgeProviderDelegate)

		let age = try person.age
        XCTAssertEqual(10, age)
    }
    
    func testPersonAddress() throws {
        let firstNameDN = "Johanna".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        let person = try Beyond_NET_Sample_Person(firstNameDN,
                                                  lastNameDN,
                                                  15)
        
        let street = "Stephansplatz"
        let streetDN = street.dotNETString()
        
        let city = "Vienna"
        let cityDN = city.dotNETString()
        
        let address = try Beyond_NET_Sample_Address(streetDN,
                                                    cityDN)
        
        try person.address_set(address)
        
        guard let retrievedAddress = try? person.address else {
            XCTFail("Person.Address getter should return an instance and not throw")
            
            return
        }
        
        let retrievedStreet = try retrievedAddress.street.string()
        
        XCTAssertEqual(street, retrievedStreet)
        
        let retrievedCity = try retrievedAddress.city.string()
        
        XCTAssertEqual(city, retrievedCity)
    }
    
    func testPersonEvents() throws {
        var numberOfTimesNumberOfChildrenChangedWasCalled: Int32 = 0

        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let daugtherFirstNameDN = "Marie".dotNETString()
        let lastNameDN = "Doe".dotNETString()
		
        let mother = try Beyond_NET_Sample_Person(motherFirstNameDN,
                                                  lastNameDN,
                                                  40)

		let numberOfChildrenChangedDelegate = Beyond_NET_Sample_Person_NumberOfChildrenChangedDelegate({
			numberOfTimesNumberOfChildrenChangedWasCalled += 1
		})
		
		mother.numberOfChildrenChanged_add(numberOfChildrenChangedDelegate)

        let son = try Beyond_NET_Sample_Person(sonFirstNameDN,
                                               lastNameDN,
                                               4)
		
		try mother.addChild(son)
		
        let daugther = try Beyond_NET_Sample_Person(daugtherFirstNameDN,
                                                    lastNameDN,
                                                    10)
		
		try mother.addChild(daugther)

		let numberOfChildren = try mother.numberOfChildren

        let expectedNumberOfChildren: Int32 = 2

        XCTAssertEqual(expectedNumberOfChildren, numberOfChildren)
        XCTAssertEqual(expectedNumberOfChildren, numberOfTimesNumberOfChildrenChangedWasCalled)

		try mother.removeChild(daugther)

        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)
		
		mother.numberOfChildrenChanged_remove(numberOfChildrenChangedDelegate)
		
		try mother.removeChildAt(0)
        XCTAssertEqual(3, numberOfTimesNumberOfChildrenChangedWasCalled)

		let numberOfChildrenAfterRemoval = try mother.numberOfChildren
        let expectedNumberOfChildrenAfterRemoval: Int32 = 0
        XCTAssertEqual(expectedNumberOfChildrenAfterRemoval, numberOfChildrenAfterRemoval)
    }
    
    func testPersonChildrenArrayChange() throws {
        let motherFirstNameDN = "Johanna".dotNETString()
        let sonFirstNameDN = "Max".dotNETString()
        let daugtherFirstNameDN = "Marie".dotNETString()
        let lastNameDN = "Doe".dotNETString()
        
        let mother = try Beyond_NET_Sample_Person(motherFirstNameDN,
                                                  lastNameDN,
                                                  40)
        
        let son = try Beyond_NET_Sample_Person(sonFirstNameDN,
                                               lastNameDN,
                                               4)
        
        try mother.addChild(son)
        
        let originalChildren = try mother.children
        
        let numberOfChildrenBeforeDaugther = try originalChildren.length
        
        XCTAssertEqual(1, numberOfChildrenBeforeDaugther)
        
        let newChildren = try DNArray<Beyond_NET_Sample_Person>(length: 2)
        
        try System_Array.copy(originalChildren,
                              newChildren,
                              1 as Int32)
        
        let daugther = try Beyond_NET_Sample_Person(daugtherFirstNameDN,
                                                    lastNameDN,
                                                    10)
        
        try newChildren.setValue(daugther, 1 as Int32)
        
        try mother.children_set(newChildren)
        
        let numberOfChildrenAfterDaugther = try mother.numberOfChildren
        
        XCTAssertEqual(2, numberOfChildrenAfterDaugther)
    }
}
