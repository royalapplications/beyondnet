namespace Beyond.NET.CodeGenerator.Generator.Swift;

internal static class SwiftSharedCode
{
    internal const string SharedCode = /*lang=Swift*/"""
public struct DNChar: Equatable {
    public let cValue: wchar_t

    public init(cValue: wchar_t) {
        self.cValue = cValue
    }
    
    public init?(character: Character) {
        guard let unicodeScalar = character.unicodeScalars.first else {
            return nil
        }
        
        let unicodeScalarValue = unicodeScalar.value
        let wcharValue = wchar_t(unicodeScalarValue)
        
        self.init(cValue: wcharValue)
    }
    
    public var character: Character? {
        guard let unicodeScalarValue = UnicodeScalar(UInt32(cValue)) else {
            return nil
        }
        
        let character = Character(unicodeScalarValue)
        
        return character
    }

    public static func == (lhs: DNChar, rhs: DNChar) -> Bool {
        lhs.cValue == rhs.cValue
    }
}

/// This is an "abstract" base class for all .NET types
/// It's not inteneded to be used directly.
/// Instead, use one of the derived types, like `System_Object`.
public class DNObject {
    enum DestroyMode {
        case normal
        case deallocateHandle
        case skip
    }
    
    let __handle: UnsafeMutableRawPointer
    var __destroyMode = DestroyMode.normal
    
    public private(set) var isOutParameterPlaceholder = false

    /// The .NET type name of this type.
    /// - Returns: The equivalent of calling `typeof(ADotNETType).Name` in C#.
    public class var typeName: String { "" }
    
    /// The .NET full type name of this type.
    /// - Returns: The equivalent of calling `typeof(ADotNETType).FullName` in C#.
    public class var fullTypeName: String { "" }
    
    /// The .NET System.Type of this type.
    /// - Returns: The equivalent of calling `typeof(ADotNETType)` in C#. 
    public class var typeOf: System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    required init(handle: UnsafeMutableRawPointer) {
        // Enable for debugging
        // print("[DEBUG] Initializing \(Self.fullTypeName) with handle \(handle)")
        
		self.__handle = handle
	}

	convenience init?(handle: UnsafeMutableRawPointer?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}
	
	/// This returns a "placeholder" object for calling .NET APIs that return a non-optional value as `out` parameter. In this case, you can either provide a proper default value (which will never be used) or you can get an out parameter placeholder using this function.
	/// Because Swift does not support `out` parameters like .NET does, you might run into situations where you need to provide a default value to satisfy the compiler but you don't want to or simply can't create an object of the `out` parameter's type.
	/// Do NOT(!) call any APIs on this "placeholder" object as it will crash the program! Use it ONLY(!) to provide a "fake" default value for .NET APIs that receive a value via an `out` parameter. 
	/// Do NOT(!) use this to pass a default value to .NET APIs that receive an optional(!) value as `out` parameter! In this case, just use a regular Swift optional. 
	public static var outParameterPlaceholder: Self {
        let byteCount = MemoryLayout<AnyObject>.stride
        let alignment = MemoryLayout<AnyObject>.alignment
        
        let handle = UnsafeMutableRawPointer.allocate(byteCount: byteCount,
                                                      alignment: alignment)
        
        handle.initializeMemory(as: Int.self, to: 0)
        
        let inst = self.init(handle: handle)
        
        // Mark this instance as being an out parameter placeholder. Makes debugging easier in case something goes wrong.
        inst.isOutParameterPlaceholder = true
        
        inst.__destroyMode = .deallocateHandle
        
        return inst
    }
	
    internal func destroy() {
        // Override in subclass
    }

    deinit {
        switch __destroyMode {
            case .normal:
                // Enable for debugging
                // print("[DEBUG] Will destroy \(Self.fullTypeName) with handle \(self.__handle)")
        
                destroy()
        
                // Enable for debugging
                // print("[DEBUG] Did destroy \(Self.fullTypeName) with handle \(self.__handle)")
            case .deallocateHandle:
                // Enable for debugging
                // print("[DEBUG] Will deallocate \(Self.fullTypeName) with handle \(self.__handle)")
                
                self.__handle.deallocate()
                
                // Enable for debugging
                // print("[DEBUG] Did deallocate \(Self.fullTypeName) with handle \(self.__handle)")
            case .skip:
                // Enable for debugging
                // print("[DEBUG] Skipping deallocate for \(Self.fullTypeName) with handle \(self.__handle)")
                return
        }
	}
}

/// This is a generic base class for all single-dimensional .NET array types with nullable element types.
/// The element type of the array is specified as the first and only generic type argument.
public class DNNullableArray<T: System_Object>: System_Array, MutableCollection {
    public typealias Element = T?
    public typealias Index = Int32
    
    public override class var typeOf: System_Type /* System.Type */ {
        let elementType = T.typeOf
        
        if let arrayType = try? elementType.makeArrayType() {
            return arrayType
        }
        
        return super.typeOf
    }
    
    public override class var typeName: String {
        let type = typeOf
        
        if let name = try? type.name.string() {
            return name
        }
        
        return "\(T.typeName)[]"
    }
    
    public override class var fullTypeName: String {
        let type = typeOf
        
        if let name = try? type.fullName?.string() {
            return name
        }
        
        return "\(T.fullTypeName)[]"
    }
    
    /// - Returns: An empty .NET array of the specified type.
    public static var empty: DNNullableArray<T> { get throws {
        try DNNullableArray<T>.init()
    }}
    
    /// Creates and initializes an empty .NET array of the specified type.
    public convenience init() throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, 0, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Creates and initializes a .NET array of the specified type and length.
    public convenience init(length: Index) throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, length, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Get or set and element of this .NET array at the specified position/index.
    /// If an exception is raised on the .NET side while indexing into the array, a fatalError will be raised on the Swift side of things.
    public subscript(position: Index) -> Element {
        get {
            assert(position >= startIndex && position < endIndex, "Out of bounds")
            
            do {
                guard let element = try getValue(position) else {
                    return nil
                }
                
                return try element.castTo()
            } catch {
                fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
            }
        }
        set {
            assert(position >= startIndex && position < endIndex, "Out of bounds")

            do {
                try setValue(newValue, position)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }
    
    public func nonNullable() throws -> DNArray<T> {
        try self.castTo()
    }
}

/// This is a generic base class for all single-dimensional .NET array types with non-null element types.
/// The element type of the array is specified as the first and only generic type argument.
public class DNArray<T: System_Object>: System_Array, MutableCollection {
    public typealias Element = T
    public typealias Index = Int32
    
    public override class var typeOf: System_Type /* System.Type */ {
        let elementType = T.typeOf
        
        if let arrayType = try? elementType.makeArrayType() {
            return arrayType
        }
        
        return super.typeOf
    }
    
    public override class var typeName: String {
        let type = typeOf
        
        if let name = try? type.name.string() {
            return name
        }
        
        return "\(T.typeName)[]"
    }
    
    public override class var fullTypeName: String {
        let type = typeOf
        
        if let name = try? type.fullName?.string() {
            return name
        }
        
        return "\(T.fullTypeName)[]"
    }
    
    /// - Returns: An empty .NET array of the specified type.
    public static var empty: DNArray<T> { get throws {
        try DNArray<T>.init()
    }}
    
    /// Creates and initializes an empty .NET array of the specified type.
    public convenience init() throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, 0, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Creates and initializes a .NET array of the specified type and length.
    public convenience init(length: Index) throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, length, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Get or set and element of this .NET array at the specified position/index.
    /// If an exception is raised on the .NET side while indexing into the array, a fatalError will be raised on the Swift side of things.
    public subscript(position: Index) -> Element {
        get {
            assert(position >= startIndex && position < endIndex, "Out of bounds")
            
            do {
                guard let element = try getValue(position) else {
                    throw DNSystemError.unexpectedNull
                }
                
                return try element.castTo()
            } catch {
                fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
            }
        }
        set {
            assert(position >= startIndex && position < endIndex, "Out of bounds")

            do {
                try setValue(newValue, position)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }
    
    public func nullable() throws -> DNNullableArray<T> {
        try self.castTo()
    }
}

/// This is a generic base class for all multidimensional .NET array types with nullable element types.
/// The element type of the array is specified as the first and only generic type argument.
public class DNNullableMultidimensionalArray<T: System_Object>: System_Array {
    public typealias Element = T?
    public typealias Indices = [Int32]
    
    /// Creates and initializes a multidimensional .NET array of the specified type and lengths.
    public convenience init(lengths: Indices) throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        let lengthsDN = try Self.indicesToDN(lengths)
        let lengthsDNC = lengthsDN.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance_3(elementTypeC, lengthsDNC, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Get or set and element of this multidimensional .NET array at the specified indices.
    /// If an exception is raised on the .NET side while indexing into the array, a fatalError will be raised on the Swift side of things.
    public subscript(position: Indices) -> Element {
        get {
            do {
                let indices = try Self.indicesToDN(position)
                
                guard let element = try getValue(indices) else {
                    return nil
                }
                
                return try element.castTo()
            } catch {
                fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
            }
        }
        set {
            do {
                let indices = try Self.indicesToDN(position)

                try setValue(newValue, indices)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }

    private static func indicesToDN(_ indices: Indices) throws -> DNArray<System_Int32> {
        let count = Int32(indices.count)
        let indicesDN = try DNArray<System_Int32>(length: count)
        
        for (idx, index) in indices.enumerated() {
            indicesDN[Int32(idx)] = index.dotNETObject()
        }

        return indicesDN
    }
    
    public func nonNullable() throws -> DNMultidimensionalArray<T> {
        try self.castTo()
    }
}

/// This is a generic base class for all multidimensional .NET array types with non-null element types.
/// The element type of the array is specified as the first and only generic type argument.
public class DNMultidimensionalArray<T: System_Object>: System_Array {
    public typealias Element = T
    public typealias Indices = [Int32]
    
    /// Creates and initializes a multidimensional .NET array of the specified type and lengths.
    public convenience init(lengths: Indices) throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        let lengthsDN = try Self.indicesToDN(lengths)
        let lengthsDNC = lengthsDN.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance_3(elementTypeC, lengthsDNC, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Get or set and element of this multidimensional .NET array at the specified indices.
    /// If an exception is raised on the .NET side while indexing into the array, a fatalError will be raised on the Swift side of things.
    public subscript(position: Indices) -> Element {
        get {
            do {
                let indices = try Self.indicesToDN(position)
                
                guard let element = try getValue(indices) else {
                    throw DNSystemError.unexpectedNull
                }
                
                return try element.castTo()
            } catch {
                fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
            }
        }
        set {
            do {
                let indices = try Self.indicesToDN(position)

                try setValue(newValue, indices)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }

    private static func indicesToDN(_ indices: Indices) throws -> DNArray<System_Int32> {
        let count = Int32(indices.count)
        let indicesDN = try DNArray<System_Int32>(length: count)
        
        for (idx, index) in indices.enumerated() {
            indicesDN[Int32(idx)] = index.dotNETObject()
        }

        return indicesDN
    }
    
    public func nullable() throws -> DNNullableMultidimensionalArray<T> {
        try self.castTo()
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
    /// Cast the targeted .NET object to a Bool.
    /// - Returns: A Bool value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown. 
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

    /// Boxes the specified Bool value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromBool(_ boolValue: Bool) -> System_Boolean {
        let castedObjectC = DNObjectFromBool(boolValue)
		
		return .init(handle: castedObjectC)
	}
	
	/// Cast the targeted .NET object to a Char.
    /// - Returns: A Char value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown. 
    func castToChar() throws -> DNChar {
        var exceptionC: System_Exception_t?
        
        let castedValueC = DNObjectCastToChar(self.__handle, &exceptionC)
        
        if let exceptionC {
            let exception = System_Exception(handle: exceptionC)
            let exceptionError = exception.error
            
            throw exceptionError
        }
        
        let castedValue = DNChar(cValue: castedValueC)
        
        return castedValue 
    }

    /// Boxes the specified Char value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromChar(_ charValue: DNChar) -> System_Char {
        let castedObjectC = DNObjectFromChar(charValue.cValue)
		
		return .init(handle: castedObjectC)
	}
	
    /// Cast the targeted .NET object to a Float.
    /// - Returns: A Float value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Float value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromFloat(_ floatValue: Float) -> System_Single {
        let castedObjectC = DNObjectFromFloat(floatValue)
        
		return .init(handle: castedObjectC)
	}

    /// Cast the targeted .NET object to a Double.
    /// - Returns: A Double value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Double value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromDouble(_ doubleValue: Double) -> System_Double {
        let castedObjectC = DNObjectFromDouble(doubleValue)
		
		return .init(handle: castedObjectC)
	}

    /// Cast the targeted .NET object to an Int8.
    /// - Returns: An Int8 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Int8 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromInt8(_ int8Value: Int8) -> System_SByte {
        let castedObjectC = DNObjectFromInt8(int8Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an UInt8.
    /// - Returns: An UInt8 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified UInt8 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromUInt8(_ uint8Value: UInt8) -> System_Byte {
        let castedObjectC = DNObjectFromUInt8(uint8Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an Int16.
    /// - Returns: An Int16 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Int16 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromInt16(_ int16Value: Int16) -> System_Int16 {
        let castedObjectC = DNObjectFromInt16(int16Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an UInt16.
    /// - Returns: An UInt16 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified UInt16 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromUInt16(_ uint16Value: UInt16) -> System_UInt16 {
        let castedObjectC = DNObjectFromUInt16(uint16Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an Int32.
    /// - Returns: An Int32 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Int32 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromInt32(_ int32Value: Int32) -> System_Int32 {
        let castedObjectC = DNObjectFromInt32(int32Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an UInt32.
    /// - Returns: An UInt32 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified UInt32 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromUInt32(_ uint32Value: UInt32) -> System_UInt32 {
        let castedObjectC = DNObjectFromUInt32(uint32Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an Int64.
    /// - Returns: An Int64 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified Int64 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromInt64(_ int64Value: Int64) -> System_Int64 {
        let castedObjectC = DNObjectFromInt64(int64Value)
        
        return .init(handle: castedObjectC)
    }

    /// Cast the targeted .NET object to an UInt64.
    /// - Returns: An UInt64 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
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

    /// Boxes the specified UInt64 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    static func fromUInt64(_ uint64Value: UInt64) -> System_UInt64 {
        let castedObjectC = DNObjectFromUInt64(uint64Value)
        
        return .init(handle: castedObjectC)
    }
}

extension Bool {
    /// Boxes the targeted Bool value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Boolean {
        return .fromBool(self)
    }
}

extension System_Boolean {
    /// Cast the targeted .NET object to a Bool.
    /// - Returns: A Bool value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Bool { get throws {
        try castToBool()
    }}
}

extension DNChar {
    /// Boxes the targeted Char value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Char {
        return .fromChar(self)
    }
}

extension System_Char {
    /// Cast the targeted .NET object to a Char.
    /// - Returns: A Char value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: DNChar { get throws {
        try castToChar()
    }}
}

extension Float {
    /// Boxes the targeted Float value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Single {
        return .fromFloat(self)
    }
}

extension System_Single {
    /// Cast the targeted .NET object to a Float.
    /// - Returns: A Float value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Float { get throws {
        try castToFloat()
    }}
}

extension Double {
    /// Boxes the targeted Double value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Double {
        return .fromDouble(self)
    }
}

extension System_Double {
    /// Cast the targeted .NET object to a Double.
    /// - Returns: A Double value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Double { get throws {
        try castToDouble()
    }}
}

extension Int8 {
    /// Boxes the targeted Int8 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_SByte {
        return .fromInt8(self)
    }
}

extension System_SByte {
    /// Cast the targeted .NET object to an Int8.
    /// - Returns: An Int8 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Int8 { get throws {
        try castToInt8()
    }}
}

extension UInt8 {
    /// Boxes the targeted UInt8 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Byte {
        return .fromUInt8(self)
    }
}

extension System_Byte {
    /// Cast the targeted .NET object to an UInt8.
    /// - Returns: An UInt8 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: UInt8 { get throws {
        try castToUInt8()
    }}
}

extension Int16 {
    /// Boxes the targeted Int16 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Int16 {
        return .fromInt16(self)
    }
}

extension System_Int16 {
    /// Cast the targeted .NET object to an Int16.
    /// - Returns: An Int16 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Int16 { get throws {
        try castToInt16()
    }}
}

extension UInt16 {
    /// Boxes the targeted UInt16 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_UInt16 {
        return .fromUInt16(self)
    }
}

extension System_UInt16 {
    /// Cast the targeted .NET object to an UInt16.
    /// - Returns: An UInt16 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: UInt16 { get throws {
        try castToUInt16()
    }}
}

extension Int32 {
    /// Boxes the targeted Int32 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Int32 {
        return .fromInt32(self)
    }
}

extension System_Int32 {
    /// Cast the targeted .NET object to an Int32.
    /// - Returns: An Int32 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Int32 { get throws {
        try castToInt32()
    }}
}

extension UInt32 {
    /// Boxes the targeted UInt32 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_UInt32 {
        return .fromUInt32(self)
    }
}

extension System_UInt32 {
    /// Cast the targeted .NET object to an UInt32.
    /// - Returns: An UInt32 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: UInt32 { get throws {
        try castToUInt32()
    }}
}

extension Int64 {
    /// Boxes the targeted Int64 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_Int64 {
        return .fromInt64(self)
    }
}

extension System_Int64 {
    /// Cast the targeted .NET object to an Int64.
    /// - Returns: An Int64 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: Int64 { get throws {
        try castToInt64()
    }}
}

extension UInt64 {
    /// Boxes the targeted UInt64 value in an .NET object.
    /// - Returns: An .NET object containing the boxed value.
    public func dotNETObject() -> System_UInt64 {
        return .fromUInt64(self)
    }
}

extension System_UInt64 {
    /// Cast the targeted .NET object to an UInt64.
    /// - Returns: An UInt64 value if the cast succeeded.
    /// - Throws: If the cast fails, an error is thrown.
    public var value: UInt64 { get throws {
        try castToUInt64()
    }}
}

/// A Swift error type that wraps a .NET `System.Exception` object.
public class DNError: LocalizedError, CustomDebugStringConvertible {
    /// The underlying .NET `System.Exception` object.
    public let exception: System_Exception
    
    /// Initializes an `DNError` with a .NET `System.Exception` object.
    public init(exception: System_Exception) {
        self.exception = exception
    }
    
    /// - Returns: The stack trace of the wrapped .NET `System.Exception` object. 
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
    
    public var debugDescription: String {
        do {
            return try String(dotNETString: exception.toString())
        } catch {
            return errorDescription ?? localizedDescription
        }
    }
}

extension System_Exception {
    /// Converts the targeted .NET `System.Exception` object to an `DNError` instance.
    public var error: DNError {
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
    /// Converts the targeted Swift `String` to a .NET `System.String`.
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
    /// Converts the targeted .NET `System.String` to a Swift `String` instance.
    func string() -> String {
        return String(dotNETString: self)
    }
}

public extension [String] {
    /// Converts a Swift String Array into a .NET `System.String` Array (`System.String[]`).
    func dotNETStringArray() throws -> DNArray<System_String> {
        let arr = try DNArray<System_String>(length: .init(count))
        
        for (idx, el) in self.enumerated() {
            let elDN = el.dotNETString()
            
            try arr.setValue(elDN, Int32(idx))
        }
        
        return arr
    }
}

public extension [String?] {
    /// Converts a Swift String? Array into a .NET `System.String?` Array (`System.String?[]`).
    func dotNETStringArray() throws -> DNNullableArray<System_String> {
        let arr = try DNNullableArray<System_String>(length: .init(count))
        
        for (idx, el) in self.enumerated() {
            let elDN = el?.dotNETString()
            
            try arr.setValue(elDN, Int32(idx))
        }
        
        return arr
    }
}

public extension DNArray<System_String> {
    /// Converts a .NET `System.String` Array (`System.String[]`) into a Swift String Array
    func array() throws -> [String] {
        let len = try self.length
        
        guard len > 0 else {
            return .init()
        }
        
        var arr = [String]()
        
        for strDN in self {
            let str = strDN.string()
            
            arr.append(str)
        }
        
        return arr
    }
}

public extension DNNullableArray<System_String> {
    /// Converts a .NET `System.String?` Array (`System.String?[]`) into a Swift String? Array
    func array() throws -> [String?] {
        let len = try self.length
        
        guard len > 0 else {
            return .init()
        }
        
        var arr = [String?]()
        
        for strDN in self {
            let str = strDN?.string()
            
            arr.append(str)
        }
        
        return arr
    }
}

extension System_Object: Equatable {
    public static func == (lhs: System_Object,
                           rhs: System_Object) -> Bool {
        let result: Bool
        
        do {
            result = try System_Object.equals(lhs, rhs)
        } catch {
            result = false
        }
        
        return result
    }
}

public func == (lhs: System_Object?,
                rhs: System_Object?) -> Bool {
    let result: Bool
    
    do {
        result = try System_Object.equals(lhs, rhs)
    } catch {
        result = false
    }
    
    return result
}

public func != (lhs: System_Object?,
                rhs: System_Object?) -> Bool {
    let result: Bool
    
    do {
        result = try System_Object.equals(lhs, rhs)
    } catch {
        result = false
    }
    
    return !result
}

public func === (lhs: System_Object?,
                 rhs: System_Object?) -> Bool {
    let result: Bool
    
    do {
        result = try System_Object.referenceEquals(lhs, rhs)
    } catch {
        result = false
    }
    
    return result
}

public func !== (lhs: System_Object?,
                 rhs: System_Object?) -> Bool {
    let result: Bool
    
    do {
        result = try System_Object.referenceEquals(lhs, rhs)
    } catch {
        result = false
    }
    
    return !result
}

fileprivate struct DNDateTimeUtils {
    static var calendarForDateTimeToSwiftDateConversions: Calendar {
        var calendar = Calendar(identifier: .gregorian)
        calendar.timeZone = .gmt
        
        return calendar
    }
}

extension System_DateTime {
    public enum DNSystemDateTimeErrors: LocalizedError {
        case dateTimeKindIsUnspecified
        case dateFromCalendarReturnedNil
        
        public var errorDescription: String? {
            switch self {
                case .dateTimeKindIsUnspecified:
                    return "DateTimeKind.Unspecified cannot be safely converted"
                case .dateFromCalendarReturnedNil:
                    return "Failed to get date from calendar"
            }
        }
    }
    
    private func dateComponents(fromSystemDateTime dateTime: System_DateTime) throws -> DateComponents {
        let nanoSecsPerTicks: Int64 = 100
        
        let ticks = try dateTime.ticks
        let ticksPerSecond = System_TimeSpan.ticksPerSecond
        
        // Compute the sub-second fraction of nanoseconds.
        let subsecondTicks = ticks % ticksPerSecond
        let nanoseconds = subsecondTicks * nanoSecsPerTicks
        
        let day = try dateTime.day
        let month = try dateTime.month
        let year = try dateTime.year
        let hour = try dateTime.hour
        let minute = try dateTime.minute
        let second = try dateTime.second
        
        var dateComponents = DateComponents()
        dateComponents.day = Int(day)
        dateComponents.month = Int(month)
        dateComponents.year = Int(year)
        dateComponents.hour = Int(hour)
        dateComponents.minute = Int(minute)
        dateComponents.second = Int(second)
        dateComponents.nanosecond = Int(nanoseconds)
        
        return dateComponents
    }
    
    /// Converts the targeted .NET `System.DateTime` to a Swift `Date` instance.
    public func swiftDate() throws -> Date {
        let dateTimeKind = try self.kind
        
        guard dateTimeKind != .unspecified else {
            throw DNSystemDateTimeErrors.dateTimeKindIsUnspecified
        }
        
        let universalDateTime = try self.toUniversalTime()
        
        let dateComponents = try dateComponents(fromSystemDateTime: universalDateTime)
        let calendar = DNDateTimeUtils.calendarForDateTimeToSwiftDateConversions
        
        guard let retDate = calendar.date(from: dateComponents) else {
            throw DNSystemDateTimeErrors.dateFromCalendarReturnedNil
        }
        
        return retDate
    }
}

extension Date {
    public enum DNDateErrors: LocalizedError {
        case dateComponentReturnedNil(_ component: String)
        case dateOutsideOfSystemDateTimeRange(_ secondsSinceReferenceDate: TimeInterval? = nil)

        public var errorDescription: String? {
            switch self {
                case .dateComponentReturnedNil(let component):
                    return "Failed to get \(component) from calendar components"
                case .dateOutsideOfSystemDateTimeRange(let secondsSinceReferenceDate):
                    let baseDescription = "The date is outside the range of System.DateTime"

                    if let secondsSinceReferenceDate {
                        return "\(baseDescription): \(secondsSinceReferenceDate)"
                    } else {
                        return baseDescription
                    }
            }
        }
    }

    /// Converts the targeted Swift `Date` instance to a .NET `System.DateTime` object.
    public func dotNETDateTime() throws -> System_DateTime {
        let nanoSecsPerTick = 100
        let nanoSecsPerMicrosec = 1000
        let nanoSecsPerMillisec = 1000000

        let referenceSwiftDate = Date(timeIntervalSince1970: 0)

        let components: Set<Calendar.Component> = [
            .era,
            .year,
            .month,
            .day,
            .hour,
            .minute,
            .second,
            .nanosecond,
            .calendar
        ]

        var calendar = Calendar(identifier: .gregorian)
        calendar.timeZone = .gmt

        let calComponents = calendar.dateComponents(components,
                                                    from: referenceSwiftDate)

        if let year = calComponents.year,
           year >= 10_000 {
            if self.timeIntervalSinceReferenceDate == 252423993600 {
                let dateTime = try System_DateTime.specifyKind(.maxValue,
                                                               .utc)

                return dateTime
            } else {
                throw DNDateErrors.dateOutsideOfSystemDateTimeRange(self.timeIntervalSinceReferenceDate)
            }
        }

        if let era = calComponents.era,
           era != 1 {
            throw DNDateErrors.dateOutsideOfSystemDateTimeRange()
        }

        guard var nanosecondsLeft = calComponents.nanosecond else {
            throw DNDateErrors.dateComponentReturnedNil("nanosencond")
        }

        let milliseconds = nanosecondsLeft / nanoSecsPerMillisec
        nanosecondsLeft -= milliseconds * nanoSecsPerMillisec
        let microseconds = nanosecondsLeft / nanoSecsPerMicrosec
        nanosecondsLeft -= microseconds * nanoSecsPerMicrosec
        let ticks = nanosecondsLeft / nanoSecsPerTick

        guard let year = calComponents.year else {
            throw DNDateErrors.dateComponentReturnedNil("year")
        }

        guard let month = calComponents.month else {
            throw DNDateErrors.dateComponentReturnedNil("month")
        }

        guard let day = calComponents.day else {
            throw DNDateErrors.dateComponentReturnedNil("day")
        }

        guard let hour = calComponents.hour else {
            throw DNDateErrors.dateComponentReturnedNil("hour")
        }

        guard let minute = calComponents.minute else {
            throw DNDateErrors.dateComponentReturnedNil("minute")
        }

        guard let second = calComponents.second else {
            throw DNDateErrors.dateComponentReturnedNil("second")
        }

        var retDate = try System_DateTime(Int32(year),
                                          Int32(month),
                                          Int32(day),
                                          Int32(hour),
                                          Int32(minute),
                                          Int32(second),
                                          Int32(milliseconds),
                                          Int32(microseconds),
                                          .utc)

        if ticks > 0 {
            let adjustedRetDate = try retDate.addTicks(.init(ticks))
            retDate = adjustedRetDate
        }

        return retDate
    }
}

public extension Data {
    /// Creates a .NET byte array (`System.Byte[]`) by copying the data from the Swift `Data` object.
    func dotNETByteArray() throws -> DNArray<System_Byte> {
        let bytesCount = Int32(self.count)
        
        let systemByteArray = try DNArray<System_Byte>(length: bytesCount)
        
        guard bytesCount > 0 else {
            return systemByteArray
        }
        
        try self.withUnsafeBytes {
            guard let unsafeBytesPointer = $0.baseAddress else {
                throw DNSystemError.unexpectedNull
            }
            
            try System_Runtime_InteropServices_Marshal.copy(.init(mutating: unsafeBytesPointer),
                                                            systemByteArray,
                                                            0,
                                                            bytesCount)
        }
        
        return systemByteArray
    }
}

public extension DNArray<System_Byte> {
    /// Creates a Swift `Data` object by either copying the data from the .NET byte array (`System.Byte[]`) or getting a pinned pointer to the underlying data in .NET which is freed as soon as the Swift `Data` object is deallocated. 
    func data(noCopy: Bool = false) throws -> Data {
        let bytesCount = try self.length
        
        guard bytesCount > 0 else {
            return .init()
        }
        
        if noCopy {
            var __exceptionC: System_Exception_t?
            var __gcHandleC: System_Runtime_InteropServices_GCHandle_t?
            
            let ptr = DNGetPinnedPointerToByteArray(self.__handle,
                                                    &__gcHandleC,
                                                    &__exceptionC)
                                                    
            if let __exceptionC {
                let __exception = System_Exception(handle: __exceptionC)
                let __error = __exception.error
                
                throw __error
            }
            
            guard let ptr else {
                if let __gcHandleC {
                    System_Runtime_InteropServices_GCHandle_Destroy(__gcHandleC)
                }
                
                throw DNSystemError.unexpectedNull
            }
            
            guard let __gcHandleC else {
                  throw DNSystemError.unexpectedNull
            }
            
            let data = Data(bytesNoCopy: .init(mutating: ptr),
                count: .init(bytesCount),
                deallocator: .custom({ _ /* ptr */, _ /* count */ in
                defer {
                    System_Runtime_InteropServices_GCHandle_Destroy(__gcHandleC)
                }
                
                let isAllocated = System_Runtime_InteropServices_GCHandle_IsAllocated_Get(__gcHandleC,
                                                                                          nil)
                
                guard isAllocated else {
                    // If the GCHandle is not allocated, there's nothing to do here
                    return
                }
                
                var __freeExceptionC: System_Exception_t?
                
                System_Runtime_InteropServices_GCHandle_Free(__gcHandleC, 
                                                             &__freeExceptionC)
                
                if let __freeExceptionC {
                    let errorMessage: String
                    
                    if let exceptionMessage = System_String(handle: System_Exception_ToString(__freeExceptionC, nil))?.string() {
                        errorMessage = exceptionMessage
                    } else {
                        errorMessage = "N/A"
                    }
                    
                    fatalError("An error occurred while freeing a GCHandle of a pinned pointer to a .NET byte[]: \(errorMessage)")
                }
            }))
            
            return data
        } else {
            var data = Data(count: .init(bytesCount))
            
            try data.withUnsafeMutableBytes {
                guard let unsafeBytesPointer = $0.baseAddress else {
                    throw DNSystemError.unexpectedNull
                }
                
                try System_Runtime_InteropServices_Marshal.copy(self,
                                                                0,
                                                                unsafeBytesPointer,
                                                                bytesCount)
            }
            
            return data
        }
    }
}

fileprivate extension Data {
    init?(readOnlySpanOfByte: DNReadOnlySpanOfByte) {
        guard let dataPointer = readOnlySpanOfByte.dataPointer,
              readOnlySpanOfByte.dataLength > 0 else {
            return nil
        }

        let dataLength = Int(readOnlySpanOfByte.dataLength)
        
        self.init(bytesNoCopy: .init(mutating: dataPointer),
                  count: dataLength, 
                  deallocator: .free)
    }
}

extension DNReadOnlySpanOfByte {
    static let empty = DNReadOnlySpanOfByte(dataPointer: nil,
                                            dataLength: 0)
    
    func data() -> Data? {
        .init(readOnlySpanOfByte: self)
    }
}

extension Data {
    func readOnlySpanOfByte() -> DNReadOnlySpanOfByte {
        let length = self.count
        
        guard length > 0 else {
            return .empty
        }
        
        guard let lengthInt32 = Int32(exactly: length) else {
            fatalError("Data is larger than \(Int32.max) bytes which cannot be represented by .NET")
        }
        
        let bufferPointer = UnsafeMutableBufferPointer<UInt8>.allocate(capacity: length)
        _ = bufferPointer.initialize(from: self)
        
        guard let dataPointer = bufferPointer.baseAddress else {
            fatalError("Failed to get the baseAddress of a buffer pointer")
        }
        
        let span = DNReadOnlySpanOfByte(dataPointer: dataPointer,
                                        dataLength: lengthInt32)
        
        return span
    }
}

extension Data? {
    func readOnlySpanOfByte() -> DNReadOnlySpanOfByte {
        guard let self else {
            return .empty
        }
        
        return self.readOnlySpanOfByte()
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
    
    public const string GuidExtensions = /*lang=Swift*/"""
extension UUID {
    /// Tries to convert the targeted Swift `UUID` object to a .NET `System.Guid`.
    /// - Returns: nil if the conversion fails or an instance of `System.Guid` if it succeeds.
    public func dotNETGuid() -> System_Guid? {
        let uuidByteTuple = self.uuid
        
        let a: UInt32 = (UInt32(uuidByteTuple.0) << 24) | (UInt32(uuidByteTuple.1) << 16) | (UInt32(uuidByteTuple.2) << 8)  | UInt32(uuidByteTuple.3)
        let b: UInt16 = (UInt16(uuidByteTuple.4) << 8) | UInt16(uuidByteTuple.5)
        let c: UInt16 = (UInt16(uuidByteTuple.6) << 8) | UInt16(uuidByteTuple.7)
        let d: UInt8 = uuidByteTuple.8
        let e: UInt8 = uuidByteTuple.9
        let f: UInt8 = uuidByteTuple.10
        let g: UInt8 = uuidByteTuple.11
        let h: UInt8 = uuidByteTuple.12
        let i: UInt8 = uuidByteTuple.13
        let j: UInt8 = uuidByteTuple.14
        let k: UInt8 = uuidByteTuple.15
        
        guard let systemGuid = try? System_Guid(a, b, c, d, e, f, g, h, i, j, k) else {
            assertionFailure("System.Guid.ctor(uint, ushort, ushort, byte, byte, byte, byte, byte, byte, byte, byte) either threw an exception or returned null")
            
            return nil
        }
        
        return systemGuid
    }
    
    public init?(dotNETGuid: System_Guid) {
        // NOTE: See https://en.wikipedia.org/wiki/Universally_unique_identifier#Endianess
        let bigEndian = true
        
        guard let uuidByteArray = try? dotNETGuid.toByteArray(bigEndian) else {
            assertionFailure("System.Guid.ToByteArray either threw an error or returned null")
            
            return nil
        }
        
        guard let uuidData = try? uuidByteArray.data(noCopy: true) else {
            assertionFailure("Failed to convert .NET byte[] to Swift Data")
            
            return nil
        }
        
        assert(uuidData.count == 16, "System.Guid.ToByteArray returned a byte array of length \(uuidData.count) (should be 16)")
        
        let cUUID: uuid_t = (
            uuidData[0],
            uuidData[1],
            uuidData[2],
            uuidData[3],
            uuidData[4],
            uuidData[5],
            uuidData[6],
            uuidData[7],
            uuidData[8],
            uuidData[9],
            uuidData[10],
            uuidData[11],
            uuidData[12],
            uuidData[13],
            uuidData[14],
            uuidData[15]
        )
        
        self.init(uuid: cUUID)
    }
}

extension System_Guid {
    /// Tries to convert the targeted .NET `System.Guid` to a Swift `UUID` object.
    /// - Returns: nil if the conversion fails or an instance of `UUID` if it succeeds.
    public func uuid() -> UUID? {
        return UUID(dotNETGuid: self)
    }
}
""";
    
    public const string ArrayExtensions = /*lang=Swift*/"""
extension System_Array {
    public typealias Index = Int32
    
    public var startIndex: Index {
        0
    }
    
    public var endIndex: Index {
        let length: Int32
        
        do {
            length = try self.length
        } catch {
            fatalError("An exception was thrown while calling System.Array.Length: \(error.localizedDescription)")
        }
        
        guard length > 0 else {
            return 0
        }
        
        return length
    }
    
    public func index(after i: Index) -> Index {
        i + 1
    }
    
    public func index(before i: Index) -> Index {
        i - 1
    }
}
""";
}