import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        Main.writeToConsole()
        
        let person = Person(firstName: "TODO",
                            lastName: "TODO",
                            age: 17)
        
        person.age += 1
        person.firstName = "John"
        person.lastName = "Doe"
        
        let fullName = person.fullName
        let age = person.age
        
        print("\(fullName) is \(age) years old.")
        
        let company = Company(name: "Royal Apps GmbH")
        
        company.addEmployee(person)
        
        let companyName = company.name
        let numberOfEmployees = company.numberOfEmployees
        
        print("\(companyName) has \(numberOfEmployees) employees")
    }
}
