import Cocoa
import NativeAOTLibraryTest

@main
class AppDelegate: NSObject, NSApplicationDelegate {
    @IBOutlet private var window: NSWindow!
	
	@IBOutlet private weak var checkBoxDebugLogging: NSButton!
	
	@IBOutlet private weak var textFieldCompanyName: NSTextField!
	@IBOutlet private weak var textFieldNumberOfEmployees: NSTextField!
	@IBOutlet private weak var textFieldNumberOfEmployeesSpelledOut: NSTextField!
	
	@IBOutlet private weak var progressIndicatorMain: NSProgressIndicator!
	@IBOutlet private weak var buttonCreate: NSButton!
	@IBOutlet private weak var buttonDestroy: NSButton!
	
	@IBOutlet private weak var tabViewMain: NSTabView!
	@IBOutlet private weak var tabViewItemCompany: NSTabViewItem!
	@IBOutlet private weak var tabViewCompany: NSView!
	
	private let companyViewController = CompanyViewController()
	
	private lazy var spellOutNumberFormatter: NumberFormatter = {
		let numberFormatter = NumberFormatter()
		numberFormatter.numberStyle = .spellOut
		
		return numberFormatter
	}()
	
	private var company: Company? {
		didSet {
			didUpdateCompany()
		}
	}
	
    func applicationDidFinishLaunching(_ aNotification: Notification) {
		isDebugLoggingEnabled = Debug.isLoggingEnabled
		
		textFieldNumberOfEmployees.delegate = self
		didUpdateNumberOfEmployees()
		
		let companyView = companyViewController.view
		companyView.frame = tabViewCompany.bounds
		
		tabViewCompany.addSubview(companyView)
		
		company = nil
    }
	
	@IBAction private func checkBoxDebugLogging_action(_ sender: Any) {
		Debug.isLoggingEnabled = isDebugLoggingEnabled
	}
	
	@IBAction private func buttonCreate_action(_ sender: Any) {
		createCompany(onMainThread: false)
	}
	
	@IBAction private func buttonDestroy_action(_ sender: Any) {
		destroyCompany()
	}
}

// MARK: - Text Field Delegate
extension AppDelegate: NSTextFieldDelegate {
	func controlTextDidChange(_ obj: Notification) {
		guard obj.object as? NSTextField == textFieldNumberOfEmployees else { return }
		
		didUpdateNumberOfEmployees()
	}
}

// MARK: - UI stuff
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
	
	func spellOutFormattedNumber(_ number: Int) -> String {
		spellOutNumberFormatter.string(for: number) ?? .init(number)
	}
	
	func didUpdateNumberOfEmployees() {
		let numberOfEmployees = textFieldNumberOfEmployees.integerValue
		
		textFieldNumberOfEmployeesSpelledOut.stringValue = spellOutFormattedNumber(numberOfEmployees)
	}
	
	func didUpdateCompany() {
		DispatchQueue.main.async { [weak self] in
			guard let self else { return }
			
			let company = self.company
			let tabViewItemLabel: String
			
			if let company {
				let companyName = company.name
				let numberOfEmployees = company.numberOfEmployees
				let formattedNumberOfEmployees = self.spellOutFormattedNumber(.init(numberOfEmployees))
				
				tabViewItemLabel = "\(companyName) (\(formattedNumberOfEmployees) employees)"
			} else {
				tabViewItemLabel = "No Company"
			}
			
			self.tabViewItemCompany.label = tabViewItemLabel
			self.companyViewController.company = company
		}
	}
}

// MARK: - Create Company
private extension AppDelegate {
	struct RandomEmployeesData {
		let firstNames: [String]
		let lastNames: [String]
		let ages: [Int32]
	}
	
	func createCompany(onMainThread: Bool) {
		let companyName = textFieldCompanyName.stringValue
		let numberOfEmployees = textFieldNumberOfEmployees.integerValue
		
		if onMainThread {
			createCompanyWithUISynchronization(name: companyName,
											   numberOfEmployees: numberOfEmployees)
		} else {
			DispatchQueue.global().async { [weak self] in
				self?.createCompanyWithUISynchronization(name: companyName,
														 numberOfEmployees: numberOfEmployees)
			}
		}
	}
	
	func createCompanyWithUISynchronization(name companyName: String,
											numberOfEmployees: Int) {
		showProgress()
		
		let company = createCompany(name: companyName,
									numberOfEmployees: numberOfEmployees)
		
		hideProgress()
		
		self.company = company
	}
	
	func createCompany(name companyName: String,
					   numberOfEmployees: Int) -> Company {
		let formattedNumberOfEmployees = spellOutFormattedNumber(numberOfEmployees)
		
		let randomDataStartDate = Date()
		
		var randomFirstNames = Person.randomFirstNames(count: numberOfEmployees)
		var randomLastNames = Person.randomLastNames(count: numberOfEmployees)
		var randomAges = Person.randomAges(count: numberOfEmployees)
		
		let randomDataDelta = formattedDateDelta(startDate: randomDataStartDate)
		
		let creationStartDate = Date()
		
		let company = createCompany(name: companyName,
									employeeFirstNames: &randomFirstNames,
									employeeLastNames: &randomLastNames,
									employeeAges: &randomAges)
		
		let creationDelta = formattedDateDelta(startDate: creationStartDate)
		
		DispatchQueue.main.async { [weak window] in
			guard let window else { return }
			
			let alert = NSAlert()
			alert.messageText = "\(companyName) was created"
			alert.informativeText = "Company \"\(companyName)\" was created with \(formattedNumberOfEmployees) random employees.\n\nRandom data generation took \(randomDataDelta) seconds.\nActual object creation took \(creationDelta) seconds."
			alert.addButton(withTitle: "OK")

			alert.beginSheetModal(for: window)
		}
		
		return company
	}
	
	func createCompany(name companyName: String,
					   employeeFirstNames: inout [String],
					   employeeLastNames: inout [String],
					   employeeAges: inout [Int32]) -> Company {
		let company = Company(name: companyName)
		
		for idx in 0..<employeeFirstNames.count {
			let firstName = employeeFirstNames[idx]
			let lastName = employeeLastNames[idx]
			let age = employeeAges[idx]
			
			let employee = Person(firstName: firstName,
								  lastName: lastName,
								  age: age)
			
			company.addEmployee(employee)
		}
		
		return company
	}
}

// MARK: - Destroy Company
private extension AppDelegate {
	func destroyCompany() {
		guard let company else { return }
		
		let companyName = company.name
		let numberOfEmployees = company.numberOfEmployees
		let formattedNumberOfEmployees = spellOutFormattedNumber(.init(numberOfEmployees))
		
		let startDate = Date()
		
		self.company = nil
		SystemGC.collect()
		
		let formattedDelta = formattedDateDelta(startDate: startDate)
		
		let alert = NSAlert()
		alert.messageText = "\(companyName) was destroyed"
		alert.informativeText = "Company \"\(companyName)\" was destroyed with \(formattedNumberOfEmployees) employees and garbage has been collected.\nIt took \(formattedDelta) seconds."
		alert.addButton(withTitle: "OK")
		
		alert.beginSheetModal(for: window)
	}
}
