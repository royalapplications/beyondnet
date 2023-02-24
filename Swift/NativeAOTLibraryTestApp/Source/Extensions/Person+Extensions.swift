import Foundation
import NativeAOTLibraryTest

extension Person {
	static func createRandom() -> Person {
		let firstName = randomFirstName()
		let lastName = randomLastName()
		let age = randomAge()
		
		let person = Person(firstName: firstName,
							lastName: lastName,
							age: age)
		
		return person
	}
}

extension Person {
	struct RandomData {
		static let ages: [Int32] = [
			0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100
		]
		
		static let firstNames = [
			"John",
			"Max",
			"Karl",
			"Felix",
			"Leo",
			"Philippa",
			"Elli",
			"Sarah",
			"Daniel",
			"Donald",
			"Alexander",
			"Patrick",
			"Emma",
			"Isabella",
			"Evelyn",
			"Amelia",
			"Charlotte",
			"Zoe"
		]
		
		static let lastNames = [
			"Agosto",
			"Doe",
			"Parker",
			"Marx",
			"Black",
			"Franco",
			"Smith",
			"Jones",
			"Garcia",
			"Davis",
			"Hernandez",
			"Green",
			"Carter",
			"Zuckero"
		]
	}
	
	static func randomFirstName() -> String {
		RandomData.firstNames.randomElement() ?? "Unnamed"
	}
	
	static func randomFirstNames(count: Int) -> [String] {
		(0..<count).map({ _ in randomFirstName() })
	}
	
	static func randomLastName() -> String {
		RandomData.lastNames.randomElement() ?? "Unnamed"
	}
	
	static func randomLastNames(count: Int) -> [String] {
		(0..<count).map({ _ in randomLastName() })
	}
	
	static func randomAge() -> Int32 {
		RandomData.ages.randomElement() ?? 0
	}
	
	static func randomAges(count: Int) -> [Int32] {
		(0..<count).map({ _ in randomAge() })
	}
}
