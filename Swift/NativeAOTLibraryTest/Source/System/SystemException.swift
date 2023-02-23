import Foundation

public class SystemException: SystemObject { }

public extension SystemException {
	var message: String {
		Debug.log("Will get message of \(swiftTypeName)")
		
		guard let valueC = System_Exception_Message_Get(handle) else {
			fatalError("Failed to get message of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get message of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
}
