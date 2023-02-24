import Foundation

public class Company: SystemObject { }

// MARK: - Public API
public extension Company {
	convenience init(name: String) {
		Debug.log { "Will create \(Self.swiftTypeName)" }

		let handle = name.withCString {
			NativeAOTLibraryTest_Company_Create($0)
		}

		guard let handle else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}

		self.init(handle: handle)

		Debug.log { "Did create \(Self.swiftTypeName)" }
	}
	
	var name: String {
		get {
			Debug.log { "Will get name of \(swiftTypeName)" }
			
			guard let valueC = NativeAOTLibraryTest_Company_Name_Get(handle) else {
				fatalError("Failed to get name of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log { "Did get name of \(swiftTypeName)" }
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log { "Will set name of \(swiftTypeName)" }
			
			newValue.withCString {
				NativeAOTLibraryTest_Company_Name_Set(handle, $0)
			}
			
			Debug.log { "Did set name of \(swiftTypeName)" }
		}
	}
	
	var numberOfEmployees: Int32 {
		Debug.log { "Will get numberOfEmployees of \(swiftTypeName)" }
		
		let value = NativeAOTLibraryTest_Company_NumberOfEmployees_Get(handle)
		
		Debug.log { "Did get numberOfEmployees of \(swiftTypeName)" }
		
		return value
	}
	
	@discardableResult
	func addEmployee(_ employee: Person) -> Bool {
		Debug.log { "Will add employee to \(swiftTypeName)" }
		
		let result = NativeAOTLibraryTest_Company_AddEmployee(handle,
															  employee.handle)
		
		let success = result == .success
		
		Debug.log { "Did add employee to \(swiftTypeName)" }
		
		return success
	}
	
	@discardableResult
	func removeEmployee(_ employee: Person) -> Bool {
		Debug.log { "Will remove employee from \(swiftTypeName)" }
		
		let result = NativeAOTLibraryTest_Company_RemoveEmployee(handle,
																 employee.handle)
		
		let success = result == .success
		
		Debug.log { "Did remove employee from \(swiftTypeName)" }
		
		return success
	}
	
	func containsEmployee(_ employee: Person) -> Bool {
		Debug.log { "Will check if \(swiftTypeName) contains employee" }
		
		let boolResult = NativeAOTLibraryTest_Company_ContainsEmployee(handle,
																	   employee.handle)
		
		let value = boolResult == .yes
		
		Debug.log { "Did check if \(swiftTypeName) contains employee" }
		
		return value
	}
	
	func employee(at index: Int32) -> Person? {
		Debug.log { "Will get employee at index of \(swiftTypeName)" }
		
		let employee: Person?
		
		if let employeeHandle = NativeAOTLibraryTest_Company_GetEmployeeAtIndex(handle,
																				index) {
			employee = .init(handle: employeeHandle)
		} else {
			employee = nil
		}
		
		Debug.log { "Did get employee at index of \(swiftTypeName)" }
		
		return employee
	}
	
	var numberOfEmployeesChanged: ((@convention(block) () -> Void)?) {
		get {
			// TODO
			return nil
		}
		set {
			// TODO: Add context
//			let context = bridge(object: unsafeBitCast(newValue, to: AnyObject.self))
			
			NativeAOTLibraryTest_Company_NumberOfEmployeesChanged_Set(handle) {
				print("TODO: numberOfEmployeesChanged called")
			}
		}
	}
}
