import Foundation

public class SystemObject {
    internal let handle: System_Object_t
    
    internal init(handle: System_Object_t) {
        self.handle = handle
    }
    
    private lazy var typeName: String = {
        String(describing: type(of: self))
    }()
    
    deinit {
        Debug.log("Will destroy \(typeName)")
        
        System_Object_Destroy(handle)
        
        Debug.log("Did destroy \(typeName)")
    }
    
    internal static func equals(_ lhs: System_Object_t,
                                _ rhs: System_Object_t) -> Bool {
        let result = System_Object_Equals(lhs, rhs) == .yes
        
        return result
    }
}

extension SystemObject: Equatable {
    public static func == (lhs: SystemObject,
                           rhs: SystemObject) -> Bool {
		let lhsTypeName = lhs.typeName
		let rhsTypeName = rhs.typeName
		
        Debug.log("Will check equality of \(lhsTypeName) and \(rhsTypeName)")
        
        let equal = Self.equals(lhs.handle,
                                rhs.handle)
        
        Debug.log("Did check equality of \(lhsTypeName) and \(rhsTypeName)")
        
        return equal
    }
}
