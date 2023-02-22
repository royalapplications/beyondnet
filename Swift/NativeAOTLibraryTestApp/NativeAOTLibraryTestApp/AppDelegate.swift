import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        Main.writeToConsole()
        
        let person = Person(firstName: "John",
                            lastName: "Doe",
                            age: 18)
        
        let fullName = person.fullName
        let age = person.age
        
        print("\(fullName) is \(person.age) years old.")
    }
}
