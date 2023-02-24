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
		let company = Company(name: companyName)
		
		for _ in 0..<numberOfEmployees {
			let employee = Person.createRandom()
			
			company.addEmployee(employee)
		}
		
		return company
	}
}

private extension Company {
	static func randomName() -> String {
		return "TODO"
	}
}
