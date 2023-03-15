import Foundation

public extension System {
    class AppDomain: System.Object {
        public typealias UnhandledExceptionHandler = (_ sender: System.Object, _ eventArgs: System.UnhandledExceptionEventArgs) -> Void
        
        override class var type: System._Type {
            .init(handle: System_AppDomain_TypeOf())
        }
    }
}

// MARK: - Public API
public extension System.AppDomain {
    static func current() -> System.AppDomain {
		Debug.log("Will get current domain of \(swiftTypeName)")
		
        let value = System.AppDomain(handle: System_AppDomain_CurrentDomain_Get())
		
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
	
	typealias UnhandledExceptionHandlerToken = ClosureToken<UnhandledExceptionHandler, UnhandledExceptionEventHandler_t>
	
	@discardableResult
	func addUnhandledExceptionHandler(_ handler: @escaping UnhandledExceptionHandler) -> UnhandledExceptionHandlerToken {
		Debug.log("Will add unhandled exception event handler to \(swiftTypeName)")
		
		let token = UnhandledExceptionHandlerToken(closure: handler,
												   handler: { innerContext, senderHandle, eventArgsHandle in
			guard let innerContext,
				  let senderHandle,
				  let eventArgsHandle else {
				return
			}
			
			let closure = NativeBox<UnhandledExceptionHandler>.fromPointer(innerContext).value
			
			closure(.init(handle: senderHandle),
					.init(handle: eventArgsHandle))
		})
		
		guard let context = token.retainedPointerToClosureBox(),
			  let handler = token.handler else {
			fatalError("context and handler should not be nil here")
		}
		
		System_AppDomain_UnhandledException_Add(handle,
												context,
												handler)
		
		Debug.log("Did add unhandled exception event handler to \(swiftTypeName)")
		
		return token
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
