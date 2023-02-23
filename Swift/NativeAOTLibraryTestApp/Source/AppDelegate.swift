import Cocoa

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!

    func applicationDidFinishLaunching(_ aNotification: Notification) {
        let tests = Tests()
        
        tests.run()
    }
}
