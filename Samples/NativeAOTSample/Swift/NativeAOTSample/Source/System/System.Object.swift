import Foundation

public extension System {
    class Object {
        internal let handle: System_Object_t
        
        internal required init(handle: System_Object_t) {
            self.handle = handle
        }
        
        public convenience init() {
            Debug.log("Will create instance of \(Self.swiftTypeName)")
            
            guard let handle = System_Object_Create() else {
                fatalError("Failed to create instance of System.Object")
            }
            
            Debug.log("Did create instance of \(Self.swiftTypeName)")
            
            self.init(handle: handle)
        }
        
        public lazy var swiftTypeName: String = {
            Self.swiftTypeName
        }()
        
        // NOTE: This is very, very expensive and should only be used for debugging purposes
        public static var swiftTypeName: String {
            String(describing: Self.self)
        }
        
        class var type: System._Type {
            .init(handle: System_Object_TypeOf())
        }
        
        deinit {
            Debug.log("Will destroy \(swiftTypeName)")
            
            System_Object_Destroy(handle)
            
            Debug.log("Did destroy \(swiftTypeName)")
        }
    }
}

// MARK: - Public API
public extension System.Object {
    var type: System._Type {
		guard let typeHandle = System_Object_GetType(handle) else {
			fatalError("Failed to get type of \(swiftTypeName)")
		}
		
        let type = System._Type(handle: typeHandle)
		
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
	
    func cast<T>(as _: T.Type) -> T? where T: System.Object {
		let targetType = T.type
		
		Debug.log("Will cast \(self.type.name) as \(targetType.name)")
		
		let castedInstance: T?
		
		if let castedInstanceHandle = System_Object_CastAs(handle, targetType.handle) {
			castedInstance = .init(handle: castedInstanceHandle)
		} else {
			castedInstance = nil
		}
		
		Debug.log("Did cast \(self.type.name) as \(targetType.name)")
		
		return castedInstance
	}
	
    func `is`<T>(of _: T.Type) -> Bool where T: System.Object {
		let targetType = T.type
		
		Debug.log("Will check if \(self.type.name) is of \(targetType.name)")
		
		let result = System_Object_Is(handle, targetType.handle).boolValue
		
		Debug.log("Did check if \(self.type.name) is of \(targetType.name)")
		
		return result
	}
}

internal extension System.Object {
	static func equals(_ lhs: System_Object_t,
					   _ rhs: System_Object_t) -> Bool {
		let result = System_Object_Equals(lhs, rhs).boolValue
		
		return result
	}
	
	static func referenceEquals(_ lhs: System_Object_t,
								_ rhs: System_Object_t) -> Bool {
		let result = System_Object_ReferenceEquals(lhs, rhs).boolValue
		
		return result
	}
}

extension System.Object: Equatable {
    public static func == (lhs: System.Object,
                           rhs: System.Object) -> Bool {
		Debug.log("Will check equality of \(lhs.swiftTypeName) and \(rhs.swiftTypeName)")
        
        let equal = Self.equals(lhs.handle,
                                rhs.handle)
        
		Debug.log("Did check equality of \(lhs.swiftTypeName) and \(rhs.swiftTypeName)")
        
        return equal
    }
	
    public static func === (lhs: System.Object,
                            rhs: System.Object) -> Bool {
		Debug.log("Will check reference equality of \(lhs.swiftTypeName) and \(rhs.swiftTypeName)")
		
		let equal = Self.referenceEquals(lhs.handle,
										 rhs.handle)
		
		Debug.log("Did check reference equality of \(lhs.swiftTypeName) and \(rhs.swiftTypeName)")
		
		return equal
	}
}
