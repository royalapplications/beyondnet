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
                                  age: 0)
        
        XCTAssertNotEqual(person, secondPerson)
    }
}
