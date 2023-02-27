import Foundation

public class SystemAppDomain: SystemObject { }

// MARK: - Public API
public extension SystemAppDomain {
	static func current() -> SystemAppDomain {
		Debug.log("Will get current domain of \(swiftTypeName)")
		
		let value = SystemAppDomain(handle: System_AppDomain_CurrentDomain_Get())
		
		Debug.log("Did get current domain of \(swiftTypeName)")
		
		return value
	}
	
	var id: Int32 {
		Debug.log("Will get ID of \(swiftTypeName)")
		
		let value = System_AppDomain_Id_Get(handle)
		
		Debug.log("Will get ID of \(swiftTypeName)")
		
		return value
	}
	
	func isDefault() -> Bool {
		Debug.log("Will get isDefault of \(swiftTypeName)")
		
		let value = System_AppDomain_IsDefaultAppDomain(handle) == .yes
		
		Debug.log("Did get isDefault of \(swiftTypeName)")
		
		return value
	}
	
	var baseDirectory: String {
		Debug.log("Will get base directory of \(swiftTypeName)")
		
		guard let valueC = System_AppDomain_BaseDirectory_Get(handle) else {
			fatalError("Failed to get base directory of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get base directory of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
}
