import XCTest
@testable import NativeAOTLibraryTest

final class PersonTests: XCTestCase {
    func testPerson() {
        let initialFirstName = "John"
        let initialLastName = "Doe"
        let initialFullName = "\(initialFirstName) \(initialLastName)"
        let initialAge: Int32 = 17
        
        let person = Person(firstName: initialFirstName,
                            lastName: initialLastName,
                            age: initialAge)
		
		XCTAssertEqual("Person", person.type.name)
		XCTAssertEqual("NativeAOTLibraryTest.Person", person.type.fullName)
        
        XCTAssertEqual(initialFirstName, person.firstName)
        XCTAssertEqual(initialLastName, person.lastName)
        XCTAssertEqual(initialFullName, person.fullName)
        XCTAssertEqual(initialAge, person.age)
        
        let adjustedFirstName = "Name with Umlauts"
        let adjustedLastName = "ÜöÄ ß"
        let adjustedFullName = "\(adjustedFirstName) \(adjustedLastName)"
        let adjustedAge = initialAge + 1
        
        person.firstName = adjustedFirstName
        person.lastName = adjustedLastName
        person.age = adjustedAge
        
        XCTAssertEqual(adjustedFirstName, person.firstName)
        XCTAssertEqual(adjustedLastName, person.lastName)
        XCTAssertEqual(adjustedFullName, person.fullName)
        XCTAssertEqual(adjustedAge, person.age)
        
        XCTAssertEqual(person, person)
        
        let secondPerson = Person(firstName: "a",
                                  lastName: "b",
                                  age: 1)
        
        XCTAssertNotEqual(person, secondPerson)
		
		XCTAssertNoThrow(try secondPerson.reduceAge(byYears: 1))
		XCTAssertEqual(0, secondPerson.age)
		
		do {
			try secondPerson.reduceAge(byYears: 1)
			
			XCTFail("Calling reduceAge should throw here but did not")
			
			return
		} catch {
			XCTAssertEqual(0, secondPerson.age)
			
			guard let systemExceptionError = error as? SystemException.Error else {
				XCTFail("Error should be of type \(String(describing: SystemException.Error.self)) but is not")
				
				return
			}
			
			let errorDescription = error.localizedDescription
			let stackTrace = systemExceptionError.stackTrace
			
			XCTAssertEqual("Age cannot be negative.", errorDescription)
			XCTAssertNotNil(stackTrace)
		}
    }
	
	func testPersonExceptionPerformance() {
		let debugLoggingWasEnabled = Debug.isLoggingEnabled
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = debugLoggingWasEnabled }
		
		let iterations = 10_000
		
		let firstName = "First Name"
		let lastName = "Last Name"
		let age: Int32 = 0
		
		var persons = [Person]()
		
		for _ in 0..<iterations {
			persons.append(.init(firstName: firstName,
								 lastName: lastName,
								 age: age))
		}
		
		measure {
			for person in persons {
				do {
					try person.reduceAge(byYears: 1)
					
					XCTFail("reduceAge should throw here but did not")
				} catch { }
			}
		}
	}
}
