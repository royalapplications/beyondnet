import XCTest
import NativeAOTLibraryTest

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
			
			let nsError = error as NSError
			let stackTrace = nsError.userInfo[SystemException.stackTraceKey]
			
			XCTAssertEqual("Age cannot be negative.", error.localizedDescription)
			XCTAssertNotNil(stackTrace)
		}
    }
}
