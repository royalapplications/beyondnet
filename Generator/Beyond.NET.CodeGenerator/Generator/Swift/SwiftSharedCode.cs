namespace Beyond.NET.CodeGenerator.Generator.Swift;

public class SwiftSharedCode
{
    public const string SharedCode = """
public struct DNChar: Equatable {
    public let cValue: wchar_t

    public init(cValue: wchar_t) {
        self.cValue = cValue
    }

    public static func == (lhs: DNChar, rhs: DNChar) -> Bool {
        lhs.cValue == rhs.cValue
    }
}

public class DNObject {
    let __handle: UnsafeMutableRawPointer
    var __skipDestroy = false

    public class var typeName: String { "" }
    public class var fullTypeName: String { "" }

    required init(handle: UnsafeMutableRawPointer) {
		self.__handle = handle
	}

	convenience init?(handle: UnsafeMutableRawPointer?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

    public class var typeOf: System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    internal func destroy() {
        // Override in subclass
    }

    deinit {
        guard !__skipDestroy else { return }
        
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

    func `is`<T>(_ type: T.Type? = nil) -> Bool where T: DNObject {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf
        } else {
            dnType = T.typeOf
        }
        
        return DNObjectIs(self.__handle, dnType.__handle)
    }

    func castAs<T>(_ type: T.Type? = nil) -> T? where T: DNObject {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf
        } else {
            dnType = T.typeOf
        }
        
        guard let castedObjectC = DNObjectCastAs(self.__handle, dnType.__handle) else {
            return nil
        }
        
        let castedObject = T(handle: castedObjectC)
        
        return castedObject
    }

    func castTo<T>(_ type: T.Type? = nil) throws -> T where T: DNObject {
        let dnType: System_Type
        
        if let type {
            dnType = type.typeOf
        } else {
            dnType = T.typeOf
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

extension Bool {
    public func dotNETObject() -> System_Object {
        return System_Object.fromBool(self)
    }
}

extension Float {
    public func dotNETObject() -> System_Object {
        return System_Object.fromFloat(self)
    }
}

extension Double {
    public func dotNETObject() -> System_Object {
        return System_Object.fromDouble(self)
    }
}

extension Int8 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromInt8(self)
    }
}

extension UInt8 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromUInt8(self)
    }
}

extension Int16 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromInt16(self)
    }
}

extension UInt16 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromUInt16(self)
    }
}

extension Int32 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromInt32(self)
    }
}

extension UInt32 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromUInt32(self)
    }
}

extension Int64 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromInt64(self)
    }
}

extension UInt64 {
    public func dotNETObject() -> System_Object {
        return System_Object.fromUInt64(self)
    }
}

public class DNError: LocalizedError {
    public let exception: System_Exception
    
    public init(exception: System_Exception) {
        self.exception = exception
    }
    
    public func stackTrace() -> String? {
        do {
            return try String(dotNETString: exception.stackTrace)
        } catch {
            return nil
        }
    }
    
    public var errorDescription: String? {
        do {
            return try String(dotNETString: exception.message)
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

public enum DNSystemError: LocalizedError {
    case unexpectedNull
    
    public var errorDescription: String? {
        switch self {
            case .unexpectedNull:
                return "Unexpectedly found null"
        }
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

extension [String] {
    /// Converts a Swift String Array into a .NET System.String Array
    func dotNETStringArray() throws -> System_String_Array {
        guard let arr = try System_String_Array.createInstance(System_String.typeOf,
                                                               .init(count)) else {
            throw DNSystemError.unexpectedNull
        }
        
        for (idx, el) in self.enumerated() {
            let elDN = el.dotNETString()
            
            try arr.setValue(elDN, Int32(idx))
        }
        
        let strArr: System_String_Array = try arr.castTo()
        
        return strArr
    }
}

extension System_String_Array {
    /// Converts a .NET System.String Array into a Swift String Array
    func array() throws -> [String] {
        let len = try self.length
        
        guard len > 0 else {
            return .init()
        }
        
        var arr = [String]()
        
        for idx in 0..<len {
            guard let el = try self.getValue(idx) else {
                throw DNSystemError.unexpectedNull
            }
            
            let elDNStr: System_String = try el.castTo()
            let elStr = elDNStr.string()
            
            arr.append(elStr)
        }
        
        return arr
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

public extension Data {
    /// WARNING: This is not optimized!
    /// Creates a .NET byte array by copying the data from the Swift Data object
    func dotNETByteArray() throws -> System_Byte_Array {
        guard let systemByteType = try? System_Type.getType("System.Byte".dotNETString()) else {
            throw DNSystemError.unexpectedNull
        }
        
        guard let arr = try System_Byte_Array.createInstance(systemByteType,
                                                             .init(count)) else {
            throw DNSystemError.unexpectedNull
        }
        
        for (idx, el) in self.enumerated() {
            try arr.setValue(el.dotNETObject(), Int32(idx))
        }
        
        let byteArr: System_Byte_Array = try arr.castTo()
        
        return byteArr
    }
}

public extension System_Byte_Array {
    /// WARNING: This is not optimized!
    /// Creates a Swift Data object by copying the data from the .NET byte array
    func data() throws -> Data {
        let len = try self.length
        
        guard len > 0 else {
            return .init()
        }
        
        var data = Data(count: .init(len))
        
        for idx in 0..<len {
            guard let el = try self.getValue(idx) else {
                throw DNSystemError.unexpectedNull
            }
            
            let elByte = try el.castToUInt8()
            
            data[.init(idx)] = elByte
        }
        
        return data
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
    
    public const string GuidExtensions = """
extension UUID {
    public func dotNETGuid() -> System_Guid? {
        let guidString = self.uuidString
        let guidStringDN = guidString.dotNETString()
        
        var guid: System_Guid?
        
        guard (try? System_Guid.tryParse(guidStringDN,
                                         &guid)) ?? false else {
            return nil
        }
        
        return guid
    }
    
    public init?(dotNETGuid: System_Guid) {
        guard let uuidStringDN = try? dotNETGuid.toString() else {
            return nil
        }
        
        let uuidString = uuidStringDN.string()
        
        self.init(uuidString: uuidString)
    }
}

extension System_Guid {
    public func uuid() -> UUID? {
        return UUID(dotNETGuid: self)
    }
}
""";
    
    public const string ArrayExtensions = """
extension System_Array: Collection {
	public typealias Index = Int32
	public typealias Element = System_Object?
	
	public struct Iterator: IteratorProtocol {
		private let array: System_Array
		private var index: Index = 0
		
		private var length: Int32 {
			let arrayLength = (try? self.array.length) ?? 0
			
			return arrayLength
		}
		
		init(_ array: System_Array) {
			self.array = array
		}
		
		public mutating func next() -> Element? {
			defer { index += 1 }
			guard index < length else { return nil }
			
			let element = try? self.array.getValue(index)
			
			return element
		}
	}
	
	public var startIndex: Index {
		return 0
	}
	
	public var endIndex: Index {
		let length = (try? self.length) ?? 0
		
		guard length > 0 else {
			return 0
		}
		
		let theEndIndex = length - 1
		
		return theEndIndex
	}
	
	public func index(after i: Index) -> Index {
		return i + 1
	}
	
	public subscript (position: Index) -> System_Object? {
		precondition(position >= startIndex && position <= endIndex, "Out of bounds")
		
		guard let element = try? self.getValue(position) else {
			return nil
		}
		
		return element
	}
	
	public func makeIterator() -> Iterator {
		return Iterator(self)
	}
}
""";
}