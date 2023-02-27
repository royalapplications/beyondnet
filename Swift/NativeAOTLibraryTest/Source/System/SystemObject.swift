import Foundation

public class SystemObject {
    internal let handle: System_Object_t
    
    internal required init(handle: System_Object_t) {
        self.handle = handle
    }
    
    public lazy var swiftTypeName: String = {
		Self.swiftTypeName
    }()
	
	// NOTE: This is very, very expensive and should only be used for debugging purposes
	public static var swiftTypeName: String {
		String(describing: Self.self)
	}
	
	class var type: SystemType {
		.init(handle: System_Object_TypeOf())
	}
    
    deinit {
		Debug.log("Will destroy \(swiftTypeName)")
        
        System_Object_Destroy(handle)
        
		Debug.log("Did destroy \(swiftTypeName)")
    }
}

// MARK: - Public API
public extension SystemObject {
	enum TypeConversionError: Error {
		case unknownCastToError
	}
	
	var type: SystemType {
		guard let typeHandle = System_Object_GetType(handle) else {
			fatalError("Failed to get type of \(swiftTypeName)")
		}
		
		let type = SystemType(handle: typeHandle)
		
		return type
	}
	
	func toString() -> String? {
		Debug.log("Will call toString of \(swiftTypeName)")
		
		guard let valueC = System_Object_ToString(handle) else {
			return nil
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did call toString of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	func cast<T>() throws -> T where T: SystemObject {
		let targetType = T.type
		var exceptionHandle: System_Exception_t?

		let instanceHandleOfTargetType = System_Object_CastTo(handle,
															  targetType.handle,
															  &exceptionHandle)

		guard let instanceHandleOfTargetType else {
			let error: Error

			if let exceptionHandle {
				let exception = SystemException(handle: exceptionHandle)
				error = exception.error
			} else {
				error = TypeConversionError.unknownCastToError
			}

			throw error
		}

		let instanceOfTargetType = T(handle: instanceHandleOfTargetType)

		return instanceOfTargetType
	}
}

internal extension SystemObject {
	static func equals(_ lhs: System_Object_t,
					   _ rhs: System_Object_t) -> Bool {
		let result = System_Object_Equals(lhs, rhs) == .yes
		
		return result
	}
}

extension SystemObject: Equatable {
    public static func == (lhs: SystemObject,
                           rhs: SystemObject) -> Bool {
		let lhsTypeName = lhs.swiftTypeName
		let rhsTypeName = rhs.swiftTypeName
		
		Debug.log("Will check equality of \(lhsTypeName) and \(rhsTypeName)")
        
        let equal = Self.equals(lhs.handle,
                                rhs.handle)
        
		Debug.log("Did check equality of \(lhsTypeName) and \(rhsTypeName)")
        
        return equal
    }
}
