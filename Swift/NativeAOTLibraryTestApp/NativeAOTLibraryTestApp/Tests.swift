import Foundation

import NativeAOTLibraryTest

struct Tests {
    func run() {
        Main.writeToConsole()
        
        let johnDoe = Person(firstName: "TODO",
                             lastName: "TODO",
                             age: 17)
        
        johnDoe.age += 1
        johnDoe.firstName = "John"
        johnDoe.lastName = "Doe"
        
        let fullName = johnDoe.fullName
        let age = johnDoe.age
        
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
            print("WARNING: The company should contain employee Elli Smith")
        }
        
        if company.containsEmployee(aGhost) {
            print("WARNING: The company should not contain employee A Ghost")
        }
        
        let companyName = company.name
        let numberOfEmployees = company.numberOfEmployees
        
        print("\(companyName) has \(numberOfEmployees) employees")
        
        if numberOfEmployees > 0 {
            for index in 0..<numberOfEmployees {
                guard let employee = company.employee(at: index) else {
                    fatalError("Failed to get employee at index \(index)")
                }
                
                print("Employee No. \(index + 1): \(employee.fullName)")
            }
        }
    }
}
