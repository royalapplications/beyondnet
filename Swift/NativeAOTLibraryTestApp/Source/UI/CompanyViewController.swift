import Foundation
import AppKit

import NativeAOTLibraryTest

class CompanyViewController: NSViewController {
	@IBOutlet private weak var tableView: NSTableView!
	
	var company: Company? {
		willSet {
			tableView.dataSource = nil
			tableView.delegate = nil
		}
		didSet {
			tableView.dataSource = self
			tableView.delegate = self
			
			tableView.reloadData()
		}
	}
	
	init() {
		super.init(nibName: "CompanyView",
				   bundle: .main)
	}
	
	@available(*, unavailable)
	required init?(coder: NSCoder) {
		fatalError("init(coder:) has not been implemented")
	}
}

extension CompanyViewController {
	
}

private extension CompanyViewController {
	var numberOfEmployees: Int {
		guard let company else {
			return 0
		}
		
		let count = Int(company.numberOfEmployees)
		
		return count
	}
	
	enum CellType {
		case firstName
		case lastName
		case age
		
		init?(columnIdentifier: NSUserInterfaceItemIdentifier) {
			switch columnIdentifier.rawValue {
				case "FirstNameColumn":
					self = .firstName
				case "LastNameColumn":
					self = .lastName
				case "AgeColumn":
					self = .age
				default:
					return nil
			}
		}
		
		var identifier: NSUserInterfaceItemIdentifier {
			switch self {
				case .firstName:
					return .init("FirstNameCell")
				case .lastName:
					return .init("LastNameCell")
				case .age:
					return .init("AgeCell")
			}
		}
	}
}

extension CompanyViewController: NSTableViewDelegate, NSTableViewDataSource {
	func numberOfRows(in tableView: NSTableView) -> Int {
		numberOfEmployees
	}
	
	func tableView(_ tableView: NSTableView,
				   viewFor tableColumn: NSTableColumn?,
				   row: Int) -> NSView? {
		guard row >= 0,
			  row != NSNotFound,
			  let tableColumn,
			  let company else {
			return nil
		}
		
		guard row < company.numberOfEmployees,
			  let employee = company.employee(at: .init(row)) else {
			return nil
		}
		
		guard let cellType = CellType(columnIdentifier: tableColumn.identifier) else {
			return nil
		}
		
		guard let view = tableView.makeView(withIdentifier: cellType.identifier,
											owner: self) as? NSTableCellView else {
			return nil
		}
		
		guard let textField = view.textField else {
			return nil
		}
		
		switch cellType {
			case .firstName:
				textField.stringValue = employee.firstName
			case .lastName:
				textField.stringValue = employee.lastName
			case .age:
				textField.stringValue = .init(employee.age)
		}
		
		return view
	}
}
