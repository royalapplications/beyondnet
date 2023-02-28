import Foundation

public class Company: SystemObject {
	public typealias NumberOfEmployeesChangedHandler = () -> Void
	
	override class var type: SystemType {
		.init(handle: NativeAOTSample_Company_TypeOf())
	}
}

// MARK: - Public API
public extension Company {
	convenience init(name: String) {
		Debug.log("Will create \(Self.swiftTypeName)")
		
		let handle = name.withCString {
			NativeAOTSample_Company_Create($0)
		}
		
		guard let handle else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}
		
		self.init(handle: handle)
		
		Debug.log("Did create \(Self.swiftTypeName)")
	}
	
	var name: String {
		get {
			Debug.log("Will get name of \(swiftTypeName)")
			
			guard let valueC = NativeAOTSample_Company_Name_Get(handle) else {
				fatalError("Failed to get name of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log("Did get name of \(swiftTypeName)")
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log("Will set name of \(swiftTypeName)")
			
			newValue.withCString {
				NativeAOTSample_Company_Name_Set(handle, $0)
			}
			
			Debug.log("Did set name of \(swiftTypeName)")
		}
	}
	
	var numberOfEmployees: Int32 {
		Debug.log("Will get numberOfEmployees of \(swiftTypeName)")
		
		let value = NativeAOTSample_Company_NumberOfEmployees_Get(handle)
		
		Debug.log("Did get numberOfEmployees of \(swiftTypeName)")
		
		return value
	}
	
	@discardableResult
	func addEmployee(_ employee: Person) -> Bool {
		Debug.log("Will add employee to \(swiftTypeName)")
		
		let result = NativeAOTSample_Company_AddEmployee(handle,
														 employee.handle)
		
		let success = result == .success
		
		Debug.log("Did add employee to \(swiftTypeName)")
		
		return success
	}
	
	@discardableResult
	func removeEmployee(_ employee: Person) -> Bool {
		Debug.log("Will remove employee from \(swiftTypeName)")
		
		let result = NativeAOTSample_Company_RemoveEmployee(handle,
															employee.handle)
		
		let success = result == .success
		
		Debug.log("Did remove employee from \(swiftTypeName)")
		
		return success
	}
	
	func containsEmployee(_ employee: Person) -> Bool {
		Debug.log("Will check if \(swiftTypeName) contains employee")
		
		let boolResult = NativeAOTSample_Company_ContainsEmployee(handle,
																  employee.handle)
		
		let value = boolResult == .yes
		
		Debug.log("Did check if \(swiftTypeName) contains employee")
		
		return value
	}
	
	func employee(at index: Int32) -> Person? {
		Debug.log("Will get employee at index of \(swiftTypeName)")
		
		let employee: Person?
		
		if let employeeHandle = NativeAOTSample_Company_GetEmployeeAtIndex(handle,
																		   index) {
			employee = .init(handle: employeeHandle)
		} else {
			employee = nil
		}
		
		Debug.log("Did get employee at index of \(swiftTypeName)")
		
		return employee
	}
	
	var numberOfEmployeesChanged: NumberOfEmployeesChangedHandler? {
		get {
			Debug.log("Will get closure for numberOfEmployeesChanged of \(swiftTypeName)")
			
			var context: UnsafeRawPointer?
			var handler: ContextDelegate_t?
			let closure: NumberOfEmployeesChangedHandler?
			
			if NativeAOTSample_Company_NumberOfEmployeesChanged_Get(handle,
																	&context,
																	&handler) == .success,
			   let context,
			   handler != nil {
				closure = NativeBox<NumberOfEmployeesChangedHandler>.fromPointerUnretained(context).value
			} else {
				closure = nil
			}
			
			Debug.log("Did get closure for numberOfEmployeesChanged of \(swiftTypeName)")
			
			return closure
		}
		set {
			Debug.log("Will set closure for numberOfEmployeesChanged of \(swiftTypeName)")
			
			var currentContext: UnsafeRawPointer?
			var currentHandler: ContextDelegate_t?
			
			let currentSuccess = NativeAOTSample_Company_NumberOfEmployeesChanged_Get(handle,
																					  &currentContext,
																					  &currentHandler) == .success
			
			let newClosureBox: NativeBox<NumberOfEmployeesChangedHandler>?
			let newHandler: ContextDelegate_t?
			
			if let newValue {
				newClosureBox = .init(value: newValue)
				
				newHandler = { innerContext in
					guard let innerContext else { return }
					
					let closure = NativeBox<NumberOfEmployeesChangedHandler>.fromPointerUnretained(innerContext).value
					
					closure()
				}
			} else {
				newClosureBox = nil
				newHandler = nil
			}
			
			NativeAOTSample_Company_NumberOfEmployeesChanged_Set(handle,
																 newClosureBox?.retainedPointer(),
																 newHandler)
			
			if currentSuccess,
			   let currentContext,
			   currentHandler != nil {
				NativeBox<NumberOfEmployeesChangedHandler>.releaseRetainedPointer(currentContext)
			}
			
			Debug.log("Did set closure for numberOfEmployeesChanged of \(swiftTypeName)")
		}
	}
}
