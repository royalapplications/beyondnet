namespace NativeAOT.CodeGenerator.Generator.Swift;

public class SwiftSharedCode
{
    public const string SharedCode = """
public struct DNChar {
    public let cValue: wchar_t

    public init(cValue: wchar_t) {
        self.cValue = cValue
    }
}

public class DNObject {
    let __handle: UnsafeMutableRawPointer

    public class var typeName: String { "" }
    public class var fullTypeName: String { "" }

    required init(handle: UnsafeMutableRawPointer) {
		self.__handle = handle
	}

	convenience init?(handle: UnsafeMutableRawPointer?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

    public class func typeOf() -> System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    internal func destroy() {
        // Override in subclass
    }

    deinit {
        // Enable for debugging
        // print("[DEBUG] Will destroy \(Self.fullTypeName)")

		destroy()

        // Enable for debugging
        // print("[DEBUG] Did destroy \(Self.fullTypeName)")
	}
}

// MARK: - Type Conversion Extensions
public extension DNObject {
    func `is`(_ type: System_Type) -> Bool {
        return DNObjectIs(self.__handle, type.__handle)
    }

    func castAs(_ type: System_Type) -> System_Object? {
        let castedObjectC = DNObjectCastAs(self.__handle, type.__handle)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castTo(_ type: System_Type) throws -> System_Object? {
        var exceptionC: System_Exception_t?
        
        let castedObjectC = DNObjectCastTo(self.__handle, type.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        let castedObject = System_Object(handle: castedObjectC)
        
        return castedObject 
    }
}

// MARK: - Primitive Conversion Extensions
public extension DNObject {
    func castToBool() throws -> Bool {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToBool(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromBool(_ boolValue: Bool) -> System_Object {
        let castedObjectC = DNObjectFromBool(boolValue)
		let castedObject = System_Object(handle: castedObjectC)

        return castedObject
	}

    func castToFloat() throws -> Float {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToFloat(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromFloat(_ floatValue: Float) -> System_Object {
        let castedObjectC = DNObjectFromFloat(floatValue)
		let castedObject = System_Object(handle: castedObjectC)

        return castedObject
	}
}

public class DNError: LocalizedError {
    public let exception: System_Exception
    
    public init(exception: System_Exception) {
        self.exception = exception
    }
    
    public func stackTrace() -> String? {
        do {
            return try String(dotNETString: exception.stackTrace_get())
        } catch {
            return nil
        }
    }
    
    public var errorDescription: String? {
        do {
            return try String(dotNETString: exception.message_get())
        } catch {
            return nil
        }
    }
}

public extension System_Exception {
    var error: DNError {
        return DNError(exception: self)
    }
}

public extension String {
	func dotNETString() -> System_String {
		let dotNetStringHandle = DNStringFromC(self)
		
		return System_String(handle: dotNetStringHandle)
	}
	
	init?(dotNETString: System_String?) {
		guard let dotNETString else { return nil }
		
		self.init(dotNETString: dotNETString)
	}
	
	init(dotNETString: System_String) {
		let cString = DNStringToC(dotNETString.__handle)
		
		self.init(cString: cString)
		
		cString.deallocate()
	}
}

public extension System_String {
    func string() -> String {
        return String(dotNETString: self)
    }
}

public final class NativeBox<T> {
    public let value: T
    
    public init(value: T) {
        self.value = value
    }
    
    public convenience init(_ value: T) {
        self.init(value: value)
    }
    
//    deinit {
//        print("Deinitializing \(Self.self)")
//    }
}

// MARK: - To Pointer
public extension NativeBox {
    func unretainedPointer() -> UnsafeRawPointer {
        pointer(retained: false)
    }
    
    func retainedPointer() -> UnsafeRawPointer {
        pointer(retained: true)
    }
}

private extension NativeBox {
    func pointer(retained: Bool) -> UnsafeRawPointer {
        let unmanaged: Unmanaged<NativeBox<T>>
        
        if retained {
            unmanaged = Unmanaged.passRetained(self)
        } else {
            unmanaged = Unmanaged.passUnretained(self)
        }
        
        let opaque = unmanaged.toOpaque()
        
        let pointer = UnsafeRawPointer(opaque)
        
        return pointer
    }
}

// MARK: - From Pointer
public extension NativeBox {
    static func fromPointer(_ pointer: UnsafeRawPointer) -> Self {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        let box = unmanaged.takeUnretainedValue()
        
        return box
    }
}

// MARK: - Release
public extension NativeBox {
    static func release(_ pointer: UnsafeRawPointer) {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        unmanaged.release()
    }
    
    func release(_ pointer: UnsafeRawPointer) {
        Self.release(pointer)
    }
}
""";
}