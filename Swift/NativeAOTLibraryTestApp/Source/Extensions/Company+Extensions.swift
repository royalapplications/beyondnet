import Foundation
import NativeAOTLibraryTest

extension Company {
	struct Employees {
		let firstNames: [String]
		let lastNames: [String]
		let ages: [Int32]
	}
	
	convenience init(companyName: String,
					 employees: Employees) {
		self.init(name: companyName)
		
		let numberOfEmployeesToCreate = employees.firstNames.count
		
		for idx in 0..<numberOfEmployeesToCreate {
			let firstName = employees.firstNames[idx]
			let lastName = employees.lastNames[idx]
			let age = employees.ages[idx]
			
			let employee = Person(firstName: firstName,
								  lastName: lastName,
								  age: age)
			
			addEmployee(employee)
		}
	}
}
