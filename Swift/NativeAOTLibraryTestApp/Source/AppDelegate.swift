import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!
	@IBOutlet private weak var textFieldNumberOfObjects: NSTextField!
	@IBOutlet private weak var checkBoxDebugLogging: NSButton!
	
	private var companies = [Company]()
	
    func applicationDidFinishLaunching(_ aNotification: Notification) {
		isDebugLoggingEnabled = Debug.isLoggingEnabled
		
        let tests = Tests()
        
        tests.run()
    }
	
	@IBAction private func checkBoxDebugLogging_action(_ sender: Any) {
		Debug.isLoggingEnabled = isDebugLoggingEnabled
	}
	
	@IBAction private func buttonCreateObjects_action(_ sender: Any) {
		createObjects(count: numberOfObjects)
	}
	
	@IBAction private func buttonDestroyObjects_action(_ sender: Any) {
		destroyObjects()
	}
	
	@IBAction private func buttonGetNames_action(_ sender: Any) {
		getNames()
	}
	
	@IBAction private func buttonSetNames_action(_ sender: Any) {
		setNames()
	}
}

private extension AppDelegate {
	var isDebugLoggingEnabled: Bool {
		get {
			checkBoxDebugLogging.state == .on
		}
		set {
			checkBoxDebugLogging.state = newValue ? .on : .off
		}
	}
	
	var numberOfObjects: Int {
		textFieldNumberOfObjects?.integerValue ?? 0
	}
	
	func formattedDateDelta(startDate: Date) -> String {
		let endDate = Date()
		let delta = startDate.distance(to: endDate)
		let formattedDelta = String(format: "%.3f", delta)
		
		return formattedDelta
	}
	
	func createObjects(count: Int) {
		let companyName = "Fancy Company"
		
		let startDate = Date()
		
		for _ in 0..<count {
			companies.append(.init(name: companyName))
		}
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let totalCount = companies.count
		
		let alert = NSAlert()
		alert.messageText = "\(count) objects were created"
		alert.informativeText = "\(count) new objects have been created.\nThe total count of objects is now \(totalCount).\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
	
	func destroyObjects() {
		let countBeforeRemoval = companies.count
		
		let startDate = Date()
		
		companies.removeAll()
		SystemGC.collect()
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "\(countBeforeRemoval) objects have been destroyed"
		alert.informativeText = "All \(countBeforeRemoval) objects have been destroyed and the GC.Collect has been called.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
	
	func getNames() {
		let count = companies.count
		
		let startDate = Date()
		
		for company in companies {
			_ = company.name
		}
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "Names of \(count) objects have been accessed"
		alert.informativeText = "All \(count) objects have been iterated over and their names were read.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
	
	func setNames() {
		let count = companies.count
		let newName = "New fancy company"
		
		let startDate = Date()
		
		for company in companies {
			company.name = newName
		}
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "Names of \(count) objects have been changed"
		alert.informativeText = "All \(count) objects have been iterated over and their names were changed.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
}
