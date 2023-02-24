import Foundation

public class SystemType: SystemObject { }

// MARK: - Public API
public extension SystemType {
	var name: String {
		Debug.log { "Will get name of \(swiftTypeName)" }
		
		guard let valueC = System_Type_Name_Get(handle) else {
			fatalError("Failed to get name of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log { "Did get name of \(swiftTypeName)" }
		
		let value = String(cString: valueC)
		
		return value
	}
	
	var fullName: String? {
		Debug.log { "Will get fullName of \(swiftTypeName)" }
		
		guard let valueC = System_Type_FullName_Get(handle) else {
			return nil
		}
		
		defer { valueC.deallocate() }
		
		Debug.log { "Did get fullName of \(swiftTypeName)" }
		
		let value = String(cString: valueC)
		
		return value
	}
}
