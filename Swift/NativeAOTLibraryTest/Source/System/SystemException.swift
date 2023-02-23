import Foundation

public class SystemException: SystemObject {
	public static let stackTraceKey = "stackTrace"
}

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
	
	var hResult: Int32 {
		Debug.log("Will get hResult of \(swiftTypeName)")
		
		let value = System_Exception_HResult_Get(handle)
		
		Debug.log("Did get hResult of \(swiftTypeName)")
		
		return value
	}
	
	var stackTrace: String? {
		Debug.log("Will get stackTrace of \(swiftTypeName)")
		
		guard let valueC = System_Exception_StackTrace_Get(handle) else {
			return nil
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get stackTrace of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	var error: Error {
		let message = self.message
		let hResult = self.hResult
		let stackTrace = self.stackTrace
		
		var userInfo: [String: Any] = [
			NSLocalizedDescriptionKey: message
		]
		
		if let stackTrace {
			userInfo[Self.stackTraceKey] = stackTrace
		}
		
		let nsError = NSError(domain: "SystemException",
							  code: .init(hResult),
							  userInfo: userInfo)
		
		return nsError
	}
}