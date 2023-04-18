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

    func `is`<T>(_ type: T.Type? = nil) -> Bool where T: System_Object {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf()
        } else {
            dnType = T.typeOf()
        }
        
        return DNObjectIs(self.__handle, dnType.__handle)
    }

    func castAs<T>(_ type: T.Type? = nil) -> T? where T: System_Object {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf()
        } else {
            dnType = T.typeOf()
        }
        
        guard let castedObjectC = DNObjectCastAs(self.__handle, dnType.__handle) else {
            return nil
        }
        
        let castedObject = T(handle: castedObjectC)
        
        return castedObject
    }

    func castTo<T>(_ type: T.Type? = nil) throws -> T where T: System_Object {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf()
        } else {
            dnType = T.typeOf()
        }
    
        var exceptionC: System_Exception_t?
        
        let castedObjectC = DNObjectCastTo(self.__handle, dnType.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
    
        guard let castedObjectC else {
            fatalError("DNObjectCastTo didn't throw an exception but returned nil") 
        }
        
        let castedObject = T(handle: castedObjectC)
        
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

    func castToDouble() throws -> Double {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToDouble(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromDouble(_ doubleValue: Double) -> System_Object {
        let castedObjectC = DNObjectFromDouble(doubleValue)
		let castedObject = System_Object(handle: castedObjectC)

        return castedObject
	}

    func castToInt8() throws -> Int8 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToInt8(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromInt8(_ int8Value: Int8) -> System_Object {
        let castedObjectC = DNObjectFromInt8(int8Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToUInt8() throws -> UInt8 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToUInt8(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromUInt8(_ uint8Value: UInt8) -> System_Object {
        let castedObjectC = DNObjectFromUInt8(uint8Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToInt16() throws -> Int16 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToInt16(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromInt16(_ int16Value: Int16) -> System_Object {
        let castedObjectC = DNObjectFromInt16(int16Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToUInt16() throws -> UInt16 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToUInt16(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromUInt16(_ uint16Value: UInt16) -> System_Object {
        let castedObjectC = DNObjectFromUInt16(uint16Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToInt32() throws -> Int32 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToInt32(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromInt32(_ int32Value: Int32) -> System_Object {
        let castedObjectC = DNObjectFromInt32(int32Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToUInt32() throws -> UInt32 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToUInt32(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromUInt32(_ uint32Value: UInt32) -> System_Object {
        let castedObjectC = DNObjectFromUInt32(uint32Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToInt64() throws -> Int64 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToInt64(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromInt64(_ int64Value: Int64) -> System_Object {
        let castedObjectC = DNObjectFromInt64(int64Value)
        let castedObject = System_Object(handle: castedObjectC)

        return castedObject
    }

    func castToUInt64() throws -> UInt64 {
        var exceptionC: System_Exception_t?
        
        let castedValue = DNObjectCastToUInt64(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        return castedValue 
    }

    static func fromUInt64(_ uint64Value: UInt64) -> System_Object {
        let castedObjectC = DNObjectFromUInt64(uint64Value)
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

extension System_Object: Equatable {
    public static func == (lhs: System_Object,
                           rhs: System_Object) -> Bool {
        return (try? Self.equals(lhs, rhs)) ?? false
    }
    
    public static func === (lhs: System_Object,
                            rhs: System_Object) -> Bool {
        return (try? Self.referenceEquals(lhs, rhs)) ?? false
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