import Foundation

public class SystemType: SystemObject {
	override class var type: SystemType {
		.init(handle: System_Type_TypeOf())
	}
}

// MARK: - Public API
public extension SystemType {
	convenience init?(typeName: String) {
		Debug.log("Will call GetType of \(Self.swiftTypeName)")
		
		let handle = typeName.withCString { typeNameC in
			System_Type_GetType(typeNameC)
		}
		
		Debug.log("Did call GetType of \(Self.swiftTypeName)")
		
		guard let handle else {
			return nil
		}
		
		self.init(handle: handle)
	}
	
	var name: String {
		Debug.log("Will get name of \(swiftTypeName)")
		
		guard let valueC = System_Type_Name_Get(handle) else {
			fatalError("Failed to get name of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get name of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	var fullName: String? {
		Debug.log("Will get fullName of \(swiftTypeName)")
		
		guard let valueC = System_Type_FullName_Get(handle) else {
			return nil
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get fullName of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	func isAssignableFrom(_ targetType: SystemType) -> Bool {
		Debug.log("Will call isAssignableFrom of \(swiftTypeName)")
		
		let result = System_Type_IsAssignableFrom(handle, targetType.handle).boolValue
		
		Debug.log("Did call isAssignableFrom of \(swiftTypeName)")
		
		return result
	}
	
	func isAssignableTo(_ targetType: SystemType) -> Bool {
		Debug.log("Will call isAssignableTo of \(swiftTypeName)")
		
		let result = System_Type_IsAssignableTo(handle, targetType.handle).boolValue
		
		Debug.log("Did call isAssignableTo of \(swiftTypeName)")
		
		return result
	}
}
