import Foundation

public class SystemUnhandledExceptionEventArgs: SystemObject {
	override class var type: SystemType {
		.init(handle: System_UnhandledExceptionEventArgs_TypeOf())
	}
}

// MARK: - Public API
public extension SystemUnhandledExceptionEventArgs {
	var exceptionObject: SystemObject {
		Debug.log("Will get exception object of \(swiftTypeName)")
		
		let value = SystemObject(handle: System_UnhandledExceptionEventArgs_ExceptionObject_Get(handle))
		
		Debug.log("Did get exception object of \(swiftTypeName)")
		
		return value
	}
	
	var isTerminating: Bool {
		Debug.log("Will get isTerminating of \(swiftTypeName)")
		
		let value = System_UnhandledExceptionEventArgs_IsTerminating_Get(handle).boolValue
		
		Debug.log("Did get isTerminating of \(swiftTypeName)")
		
		return value
	}
}
