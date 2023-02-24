import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!
	
	@IBOutlet private weak var checkBoxDebugLogging: NSButton!
	
	@IBOutlet private weak var textFieldCompanyName: NSTextField!
	@IBOutlet private weak var textFieldNumberOfEmployees: NSTextField!
	
	@IBOutlet private weak var progressIndicatorMain: NSProgressIndicator!
	@IBOutlet private weak var buttonCreate: NSButton!
	@IBOutlet private weak var buttonDestroy: NSButton!
	
	@IBOutlet private weak var tabViewMain: NSTabView!
	@IBOutlet private weak var tabViewItemCompany: NSTabViewItem!
	@IBOutlet private weak var tabViewCompany: NSView!
	
	private let companyViewController = CompanyViewController()
	
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
		
		Task {
			showProgress()
			
			let company = await createCompany(name: companyName,
											  numberOfEmployees: numberOfEmployees)
			
			hideProgress()
			
			self.company = company
		}
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
	
	func showProgress() {
		DispatchQueue.main.async { [weak self] in
			guard let self else { return }
	
			self.buttonCreate.isEnabled = false
			self.buttonDestroy.isEnabled = false
			self.progressIndicatorMain.startAnimation(nil)
		}
	}
	
	func hideProgress() {
		DispatchQueue.main.async { [weak self] in
			guard let self else { return }
	
			self.buttonCreate.isEnabled = true
			self.buttonDestroy.isEnabled = true
			self.progressIndicatorMain.stopAnimation(nil)
		}
	}
	
	func formattedDateDelta(startDate: Date) -> String {
		let endDate = Date()
		let delta = startDate.distance(to: endDate)
		let formattedDelta = String(format: "%.3f", delta)
		
		return formattedDelta
	}
	
	func createCompany(name companyName: String,
					   numberOfEmployees: Int) async -> Company {
		let randomDataStartDate = Date()
		
		async let randomFirstNames = Person.randomFirstNames(count: numberOfEmployees)
		async let randomLastNames = Person.randomLastNames(count: numberOfEmployees)
		async let randomAges = Person.randomAges(count: numberOfEmployees)
		
		let employees = await Company.Employees(firstNames: randomFirstNames,
												lastNames: randomLastNames,
												ages: randomAges)
		
		let randomDataDelta = formattedDateDelta(startDate: randomDataStartDate)
		
		let creationStartDate = Date()
		
		let company = Company(companyName: companyName,
							  employees: employees)

		let creationDelta = formattedDateDelta(startDate: creationStartDate)

		DispatchQueue.main.async { [weak window] in
			guard let window else { return }
			
			let alert = NSAlert()
			alert.messageText = "\(companyName) was created"
			alert.informativeText = "Company \"\(companyName)\" was created with \(numberOfEmployees) random employees.\n\nRandom data generation took \(randomDataDelta) seconds.\nActual object creation took \(creationDelta) seconds."
			alert.addButton(withTitle: "OK")

			alert.beginSheetModal(for: window)
		}
		
		return company
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
			
			let company = self.company
			let tabViewItemLabel: String
			
			if let company {
				tabViewItemLabel = "\(company.name) - \(company.numberOfEmployees) employees"
			} else {
				tabViewItemLabel = "No Company"
			}
			
			self.tabViewItemCompany.label = tabViewItemLabel
			self.companyViewController.company = company
		}
	}
}
