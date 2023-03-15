import Foundation

public extension System {
    class UnhandledExceptionEventArgs: System.Object {
        override class var type: System._Type {
            .init(handle: System_UnhandledExceptionEventArgs_TypeOf())
        }
    }
}

// MARK: - Public API
public extension System.UnhandledExceptionEventArgs {
    var exceptionObject: System.Object {
		Debug.log("Will get exception object of \(swiftTypeName)")
		
        let value = System.Object(handle: System_UnhandledExceptionEventArgs_ExceptionObject_Get(handle))
		
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
