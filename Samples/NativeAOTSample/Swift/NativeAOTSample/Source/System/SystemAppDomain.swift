import Foundation

public class SystemAppDomain: SystemObject {
	public typealias UnhandledExceptionHandler = (_ sender: SystemObject, _ eventArgs: SystemUnhandledExceptionEventArgs) -> Void
	
	override class var type: SystemType {
		.init(handle: System_AppDomain_TypeOf())
	}
}

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
		
		let value = System_AppDomain_IsDefaultAppDomain(handle).boolValue
		
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
	
	typealias UnhandledExceptionHandlerToken = DelegateToken<UnhandledExceptionHandler, UnhandledExceptionEventHandler_t>
	
	@discardableResult
	func addUnhandledExceptionHandler(_ handler: @escaping UnhandledExceptionHandler) -> UnhandledExceptionHandlerToken {
		Debug.log("Will add unhandled exception event handler to \(swiftTypeName)")
		
		let newHandler: UnhandledExceptionEventHandler_t = { innerContext, senderHandle, eventArgsHandle in
			guard let innerContext,
				  let senderHandle,
				  let eventArgsHandle else {
				return
			}
			
			let closure = NativeBox<UnhandledExceptionHandler>.fromPointer(innerContext).value
			
			closure(.init(handle: senderHandle),
					.init(handle: eventArgsHandle))
		}
		
		let newClosureBox = NativeBox(value: handler)
		let outerContext = newClosureBox.retainedPointer()
		
		System_AppDomain_UnhandledException_Add(handle,
												outerContext,
												newHandler)
		
		Debug.log("Did add unhandled exception event handler to \(swiftTypeName)")
		
		return .init(closureBox: newClosureBox,
					 handler: newHandler)
	}
	
	@discardableResult
	func removeUnhandledExceptionHandler(_ handlerToken: UnhandledExceptionHandlerToken) -> Bool {
		guard let closureBox = handlerToken.closureBox,
			  let handler = handlerToken.handler else {
			return false
		}
		
		Debug.log("Will remove unhandled exception event handler to \(swiftTypeName)")
		
		let context = closureBox.unretainedPointer()
		
		let result = System_AppDomain_UnhandledException_Remove(handle,
																context,
																handler).boolValue
		
		handlerToken.invalidate(andReleasePointerToClosureBox: context)
		
		Debug.log("Did remove unhandled exception event handler to \(swiftTypeName)")
		
		return result
	}
}
