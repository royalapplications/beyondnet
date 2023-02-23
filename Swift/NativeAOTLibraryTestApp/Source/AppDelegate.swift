import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!
	@IBOutlet private weak var textFieldNumberOfObjects: NSTextField!
	
	private var companies = [Company]()
	
    func applicationDidFinishLaunching(_ aNotification: Notification) {
        let tests = Tests()
        
        tests.run()
    }
	
	@IBAction private func buttonCreateObjects_action(_ sender: Any) {
		createObjects(count: numberOfObjects)
	}
	
	@IBAction private func buttonDestroyObjects_action(_ sender: Any) {
		destroyObjects()
	}
}

private extension AppDelegate {
	var numberOfObjects: Int {
		textFieldNumberOfObjects?.integerValue ?? 0
	}
	
	func createObjects(count: Int) {
		let companyName = "Fancy Company"
		
		for _ in 0..<count {
			companies.append(.init(name: companyName))
		}
	}
	
	func destroyObjects() {
		companies.removeAll()
		
		SystemGC.collect()
	}
}
