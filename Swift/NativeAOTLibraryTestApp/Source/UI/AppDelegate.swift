import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!
	
	@IBOutlet private weak var checkBoxDebugLogging: NSButton!
	
	@IBOutlet private weak var textFieldCompanyName: NSTextField!
	@IBOutlet private weak var textFieldNumberOfEmployees: NSTextField!
	
	@IBOutlet private weak var tabViewMain: NSTabView!
	@IBOutlet private weak var tabViewCompany: NSView!
	
	let companyViewController = CompanyViewController()
	
	private var company: Company? {
		didSet {
			didUpdateCompany()
		}
	}
	
    func applicationDidFinishLaunching(_ aNotification: Notification) {
		isDebugLoggingEnabled = Debug.isLoggingEnabled
		
		let companyView = companyViewController.view
		companyView.frame = tabViewCompany.bounds
		
		tabViewCompany.addSubview(companyView)
		
		company = nil
    }
	
	@IBAction private func checkBoxDebugLogging_action(_ sender: Any) {
		Debug.isLoggingEnabled = isDebugLoggingEnabled
	}
	
	@IBAction private func buttonCreate_action(_ sender: Any) {
		let companyName = textFieldCompanyName.stringValue
		let numberOfEmployees = textFieldNumberOfEmployees.integerValue
		
		createCompany(name: companyName,
					  numberOfEmployees: numberOfEmployees)
	}
	
	@IBAction private func buttonDestroy_action(_ sender: Any) {
		destroyCompany()
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
	
	func formattedDateDelta(startDate: Date) -> String {
		let endDate = Date()
		let delta = startDate.distance(to: endDate)
		let formattedDelta = String(format: "%.3f", delta)
		
		return formattedDelta
	}
	
	func createCompany(name companyName: String,
					   numberOfEmployees: Int) {
		let startDate = Date()
		
		let company = Company.createRandom(companyName: companyName,
										   numberOfEmployees: numberOfEmployees)
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "\(companyName) was created"
		alert.informativeText = "Company \"\(company.name)\" was created with \(company.numberOfEmployees) random employees.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")

		alert.beginSheetModal(for: window)
		
		self.company = company
	}
	
	func destroyCompany() {
		guard let company else { return }
		
		let companyName = company.name
		let numberOfEmployees = company.numberOfEmployees
		
		let startDate = Date()
		
		self.company = nil
		SystemGC.collect()
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "\(companyName) was destroyed"
		alert.informativeText = "Company \"\(companyName)\" was destroyed with \(numberOfEmployees) employees and garbage has been collected.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
	
	func didUpdateCompany() {
		DispatchQueue.main.async { [weak self] in
			guard let self else { return }
			
			self.companyViewController.company = self.company
		}
	}
}
