import Foundation
import NativeAOTLibraryTest

extension Company {
	static func createRandom(numberOfEmployees: Int) -> Company {
		let name = randomName()
		
		let company = createRandom(companyName: name,
								   numberOfEmployees: numberOfEmployees)
		
		return company
	}
	
	static func createRandom(companyName: String,
							 numberOfEmployees: Int) -> Company {
		let employeeFirstNames = Person.randomFirstNames(count: numberOfEmployees)
		let employeeLastNames = Person.randomLastNames(count: numberOfEmployees)
		let employeeAges = Person.randomAges(count: numberOfEmployees)
		
		let company = Company(companyName: companyName,
							  employeeFirstNames: employeeFirstNames,
							  employeeLastNames: employeeLastNames,
							  employeeAges: employeeAges)
		
		return company
	}
	
	convenience init(companyName: String,
					 employeeFirstNames: [String],
					 employeeLastNames: [String],
					 employeeAges: [Int32]) {
		self.init(name: companyName)
		
		let numberOfEmployeesToCreate = employeeFirstNames.count
		
		for idx in 0..<numberOfEmployeesToCreate {
			let firstName = employeeFirstNames[idx]
			let lastName = employeeLastNames[idx]
			let age = employeeAges[idx]
			
			let employee = Person(firstName: firstName,
								  lastName: lastName,
								  age: age)
			
			addEmployee(employee)
		}
	}
}

private extension Company {
	static func randomName() -> String {
		return "TODO"
	}
}
