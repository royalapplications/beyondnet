import XCTest
@testable import NativeAOTSample

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
		XCTAssertEqual("NativeAOTSample.Person", person.type.fullName)
        
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
        
		// swiftlint:disable:next identical_operands
		XCTAssertTrue(person == person)
		
		let secondPerson = Person(firstName: "a",
								  lastName: "b",
								  age: 1)
	
		XCTAssertFalse(person == secondPerson)
    }
	
	func testPersonUnicode() {
		let firstName = "First 👍 Name"
		let lastName = "Last 👎 Name"
		
		let person = Person(firstName: firstName,
							lastName: lastName,
							age: 0)
		
		XCTAssertEqual(firstName, person.firstName)
		XCTAssertEqual(lastName, person.lastName)
		XCTAssertEqual("\(firstName) \(lastName)", person.fullName)
	}
	
	func testPersonEquality() {
		let firstName = "First Name"
		let lastName = "Last Name"
		let age: Int32 = 100
		
		let firstPerson = Person(firstName: firstName,
								 lastName: lastName,
								 age: age)
		
		let secondPerson = Person(firstName: firstName,
								  lastName: lastName,
								  age: age)
		
		// Equals Checks
		
		// This should yield true because Person implements Equals and has the same values
		XCTAssertTrue(firstPerson == secondPerson)
		
		// swiftlint:disable:next identical_operands
		XCTAssertTrue(firstPerson == firstPerson)
		
		// Reference Equals Checks
		
		// This should yield false because we're comparing two distinct Person instances
		XCTAssertFalse(firstPerson === secondPerson)
		
		// This should yield true because we're comparing the very same Person instance
		// swiftlint:disable:next identical_operands
		XCTAssertTrue(firstPerson === firstPerson)
	}
	
	func testPersonExceptions() {
		let person = Person(firstName: "a",
							lastName: "b",
							age: 1)
		
		XCTAssertNoThrow(try person.reduceAge(byYears: 1))
		XCTAssertEqual(0, person.age)
		
		do {
			try person.reduceAge(byYears: 1)
			
			XCTFail("Calling reduceAge should throw here but did not")
			
			return
		} catch {
			XCTAssertEqual(0, person.age)
			
            guard let systemExceptionError = error as? System.Exception.Error else {
                XCTFail("Error should be of type \(String(describing: System.Exception.Error.self)) but is not")
				
				return
			}
			
			let errorDescription = error.localizedDescription
			let stackTrace = systemExceptionError.stackTrace
			
			XCTAssertEqual("Age cannot be negative.", errorDescription)
			XCTAssertNotNil(stackTrace)
		}
	}
	
	func testPersonClosures() {
		let initialAge: Int32 = 0
		let newAge: Int32 = 5
		let invalidAge: Int32 = -5
		
		let person = Person(firstName: "A",
							lastName: "B",
							age: initialAge)
		
		XCTAssertEqual(initialAge, person.age)
		
		// Test without a newAgeProvider
		XCTAssertNoThrow(try person.changeAge(nil))
		XCTAssertEqual(initialAge, person.age)
		
		// Test with a newAgeProvider that changes the age to a valid value
		XCTAssertNoThrow(try person.changeAge { newAge })
		XCTAssertEqual(newAge, person.age)
		
		// Test with a newAgeProvider that fails because it tries to change the age to an invalid value
		XCTAssertThrowsError(try person.changeAge { invalidAge })
		XCTAssertEqual(newAge, person.age)
	}
	
	func testPersonClosurePerformance() {
		let debugLoggingWasEnabled = Debug.isLoggingEnabled
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = debugLoggingWasEnabled }
		
		let iterations = 100_000
		
		let persons = createPersons(count: iterations)
		let newAge: Int32 = 5
		let newAgeProvider: Person.ChangeAgeNewAgeProvider = { newAge }
		
		measure {
			for person in persons {
				do {
					try person.changeAge(newAgeProvider)
				} catch {
					XCTFail("changeAge should not throw here")
					
					return
				}
			}
		}
	}
	
	func testPersonExceptionPerformance() {
		let debugLoggingWasEnabled = Debug.isLoggingEnabled
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = debugLoggingWasEnabled }
		
		let iterations = 10_000
		
		let yearsToReduceBy: Int32 = 1
		
		let persons = createPersons(count: iterations,
									firstName: "First Name",
									lastName: "Last Name",
									age: 0)
		
		measure {
			for person in persons {
				do {
					try person.reduceAge(byYears: yearsToReduceBy)
					
					XCTFail("reduceAge should throw here but did not")
				} catch { }
			}
		}
	}
    
    typealias ChangeAgeNewAgeProvider = @convention(c) () -> Int32
    
    func testCDelegates() {
        let person = Person(firstName: "John",
                            lastName: "Doe",
                            age: 24)
        
        do {
            try person.changeAgeNew { _ in
                return 1
            }
        } catch {
            XCTFail("Should not throw")
        }
    }
}

private extension PersonTests {
	func createPersons(count: Int,
					   firstName: String = "First Name",
					   lastName: String = "Last Name",
					   age: Int32 = 0) -> [Person] {
		var persons = [Person]()
		
		for _ in 0..<count {
			let person = Person(firstName: firstName,
								lastName: lastName,
								age: age)
			
			persons.append(person)
		}
		
		return persons
	}
}
