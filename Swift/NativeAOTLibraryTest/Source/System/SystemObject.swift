import Foundation

public class SystemObject {
    internal let handle: System_Object_t
    
    internal init(handle: System_Object_t) {
        self.handle = handle
    }
    
    public lazy var swiftTypeName: String = {
		Self.swiftTypeName
    }()
	
	// TODO: This is very, very expensive and only aids in debugging !!!
	public static var swiftTypeName: String {
		String(describing: Self.self)
	}
    
    deinit {
        Debug.log("Will destroy \(swiftTypeName)")
        
        System_Object_Destroy(handle)
        
        Debug.log("Did destroy \(swiftTypeName)")
    }
}

// MARK: - Public API
public extension SystemObject {
	var type: SystemType {
		guard let typeHandle = System_Object_GetType(handle) else {
			fatalError("Failed to get type of \(swiftTypeName)")
		}
		
		let type = SystemType(handle: typeHandle)
		
		return type
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
