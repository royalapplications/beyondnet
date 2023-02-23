// swiftlint:disable cyclomatic_complexity

import Foundation

import NativeAOTLibraryTest

struct Tests {
    func run() {
        let johnDoe = Person(firstName: "TODO",
                             lastName: "TODO",
                             age: 17)
        
        johnDoe.age += 1
        johnDoe.firstName = "John"
        johnDoe.lastName = "Doe"
        
        let fullName = johnDoe.fullName
        let age = johnDoe.age
        
        guard fullName == "John Doe" else {
            fatalError("The person's name should be John Doe")
        }
        
        print("\(fullName) is \(age) years old.")
        
        let elliSmith = Person(firstName: "Elli",
                               lastName: "Smith",
                               age: 2)
        
        let aGhost = Person(firstName: "A",
                            lastName: "Ghost",
                            age: 500)
        
        let company = Company(name: "Royal Apps GmbH")
        
        company.addEmployee(johnDoe)
        company.addEmployee(elliSmith)
        
        if !company.containsEmployee(elliSmith) {
            fatalError("The company should contain employee Elli Smith")
        }
        
        if company.containsEmployee(aGhost) {
            fatalError("The company should not contain employee A Ghost")
        }
        
        let companyName = company.name
        let numberOfEmployees = company.numberOfEmployees
        
        guard numberOfEmployees == 2 else {
            fatalError("The company should contain 2 employees")
        }
        
        print("\(companyName) has \(numberOfEmployees) employees")
        
        if numberOfEmployees > 0 {
            for index in 0..<numberOfEmployees {
                guard let employee = company.employee(at: index) else {
                    fatalError("Failed to get employee at index \(index)")
                }
                
                switch index {
                case 0:
                    guard employee == johnDoe else {
                        fatalError("Employee at index 0 should be John Doe")
                    }
                case 1:
                    guard employee == elliSmith else {
                        fatalError("Employee at index 1 should be Elli Smith")
                    }
                default:
                    fatalError("We should only have two employees")
                }
                
                print("Employee No. \(index + 1): \(employee.fullName)")
            }
        }
    }
}
