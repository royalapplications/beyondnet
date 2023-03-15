import Foundation

public extension System {
    class Exception: System.Object {
        public static let stackTraceKey = "stackTrace"
        
        override class var type: System._Type {
            .init(handle: System_Exception_TypeOf())
        }
        
        convenience init(message: String?) {
            Debug.log("Will create \(Self.swiftTypeName)")
            
            var handle: System_Exception_t?
            
            if let message {
                handle = message.withCString { messageC in
                    System_Exception_Create(messageC)
                }
            } else {
                handle = System_Exception_Create(nil)
            }
            
            guard let handle else {
                fatalError("Failed to create \(Self.swiftTypeName)")
            }
            
            Debug.log("Did create \(Self.swiftTypeName)")
            
            self.init(handle: handle)
        }
    }
}

public extension System.Exception {
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
	
    var error: System.Exception.Error {
		.init(exception: self)
	}
}
