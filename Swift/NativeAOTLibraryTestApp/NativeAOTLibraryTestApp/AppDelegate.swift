import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        Main.writeToConsole()
        
        let person = Person(age: 18)
        
        print("Person is \(person.age) years old.")
    }
}
