// Number of generated types: 837
// Number of generated members: 0

// MARK: - BEGIN Header
import Foundation

// MARK: - END Header

// MARK: - BEGIN Utils
final class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
    
    convenience init(_ value: T) {
        self.init(value: value)
    }
    
//    deinit {
//        print("Deinitializing \(Self.self)")
//    }
}

// MARK: - To Pointer
extension NativeBox {
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
extension NativeBox {
    static func fromPointer(_ pointer: UnsafeRawPointer) -> Self {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        let box = unmanaged.takeUnretainedValue()
        
        return box
    }
}

// MARK: - Release
extension NativeBox {
    static func release(_ pointer: UnsafeRawPointer) {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        unmanaged.release()
    }
    
    func release(_ pointer: UnsafeRawPointer) {
        Self.release(pointer)
    }
}

// MARK: - END Utils

// MARK: - BEGIN Common Types


// MARK: - END Common Types

// MARK: - BEGIN Unsupported Types
// Omitted due to settings

// MARK: - END Unsupported Types

// MARK: - BEGIN Type Definitions
public struct System_Reflection_MemberTypes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_MemberTypes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_MemberTypes_t { System_Reflection_MemberTypes_t(rawValue: rawValue) }

	public static let constructor = System_Reflection_MemberTypes(rawValue: 1)
	public static let event = System_Reflection_MemberTypes(rawValue: 2)
	public static let field = System_Reflection_MemberTypes(rawValue: 4)
	public static let method = System_Reflection_MemberTypes(rawValue: 8)
	public static let property = System_Reflection_MemberTypes(rawValue: 16)
	public static let typeInfo = System_Reflection_MemberTypes(rawValue: 32)
	public static let custom = System_Reflection_MemberTypes(rawValue: 64)
	public static let nestedType = System_Reflection_MemberTypes(rawValue: 128)
	public static let all = System_Reflection_MemberTypes(rawValue: 191)
}


public enum System_StringComparison: Int32 {
	init(cValue: System_StringComparison_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_StringComparison_t { System_StringComparison_t(rawValue: rawValue)! }

	case currentCulture = 0
	case currentCultureIgnoreCase = 1
	case invariantCulture = 2
	case invariantCultureIgnoreCase = 3
	case ordinal = 4
	case ordinalIgnoreCase = 5
}


public struct System_Globalization_NumberStyles: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Globalization_NumberStyles_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Globalization_NumberStyles_t { System_Globalization_NumberStyles_t(rawValue: rawValue) }

	public static let none = System_Globalization_NumberStyles([])
	public static let allowLeadingWhite = System_Globalization_NumberStyles(rawValue: 1)
	public static let allowTrailingWhite = System_Globalization_NumberStyles(rawValue: 2)
	public static let allowLeadingSign = System_Globalization_NumberStyles(rawValue: 4)
	public static let integer = System_Globalization_NumberStyles(rawValue: 7)
	public static let allowTrailingSign = System_Globalization_NumberStyles(rawValue: 8)
	public static let allowParentheses = System_Globalization_NumberStyles(rawValue: 16)
	public static let allowDecimalPoint = System_Globalization_NumberStyles(rawValue: 32)
	public static let allowThousands = System_Globalization_NumberStyles(rawValue: 64)
	public static let number = System_Globalization_NumberStyles(rawValue: 111)
	public static let allowExponent = System_Globalization_NumberStyles(rawValue: 128)
	public static let float = System_Globalization_NumberStyles(rawValue: 167)
	public static let allowCurrencySymbol = System_Globalization_NumberStyles(rawValue: 256)
	public static let currency = System_Globalization_NumberStyles(rawValue: 383)
	public static let any = System_Globalization_NumberStyles(rawValue: 511)
	public static let allowHexSpecifier = System_Globalization_NumberStyles(rawValue: 512)
	public static let hexNumber = System_Globalization_NumberStyles(rawValue: 515)
}


public enum System_TypeCode: Int32 {
	init(cValue: System_TypeCode_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_TypeCode_t { System_TypeCode_t(rawValue: rawValue)! }

	case empty = 0
	case object = 1
	case dBNull = 2
	case boolean = 3
	case char = 4
	case sByte = 5
	case byte = 6
	case int16 = 7
	case uInt16 = 8
	case int32 = 9
	case uInt32 = 10
	case int64 = 11
	case uInt64 = 12
	case single = 13
	case double = 14
	case decimal = 15
	case dateTime = 16
	case string = 18
}


public struct System_Globalization_CultureTypes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Globalization_CultureTypes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Globalization_CultureTypes_t { System_Globalization_CultureTypes_t(rawValue: rawValue) }

	public static let neutralCultures = System_Globalization_CultureTypes(rawValue: 1)
	public static let specificCultures = System_Globalization_CultureTypes(rawValue: 2)
	public static let installedWin32Cultures = System_Globalization_CultureTypes(rawValue: 4)
	public static let allCultures = System_Globalization_CultureTypes(rawValue: 7)
	public static let userCustomCulture = System_Globalization_CultureTypes(rawValue: 8)
	public static let replacementCultures = System_Globalization_CultureTypes(rawValue: 16)
	public static let windowsOnlyCultures = System_Globalization_CultureTypes(rawValue: 32)
	public static let frameworkCultures = System_Globalization_CultureTypes(rawValue: 64)
}


public enum System_Reflection_ProcessorArchitecture: Int32 {
	init(cValue: System_Reflection_ProcessorArchitecture_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Reflection_ProcessorArchitecture_t { System_Reflection_ProcessorArchitecture_t(rawValue: rawValue)! }

	case none = 0
	case mSIL = 1
	case x86 = 2
	case iA64 = 3
	case amd64 = 4
	case arm = 5
}


public enum System_Reflection_AssemblyContentType: Int32 {
	init(cValue: System_Reflection_AssemblyContentType_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Reflection_AssemblyContentType_t { System_Reflection_AssemblyContentType_t(rawValue: rawValue)! }

	case `default` = 0
	case windowsRuntime = 1
}


public struct System_Reflection_AssemblyNameFlags: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_AssemblyNameFlags_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_AssemblyNameFlags_t { System_Reflection_AssemblyNameFlags_t(rawValue: rawValue) }

	public static let none = System_Reflection_AssemblyNameFlags([])
	public static let publicKey = System_Reflection_AssemblyNameFlags(rawValue: 1)
	public static let retargetable = System_Reflection_AssemblyNameFlags(rawValue: 256)
	public static let enableJITcompileOptimizer = System_Reflection_AssemblyNameFlags(rawValue: 16384)
	public static let enableJITcompileTracking = System_Reflection_AssemblyNameFlags(rawValue: 32768)
}


public enum System_Configuration_Assemblies_AssemblyHashAlgorithm: Int32 {
	init(cValue: System_Configuration_Assemblies_AssemblyHashAlgorithm_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Configuration_Assemblies_AssemblyHashAlgorithm_t { System_Configuration_Assemblies_AssemblyHashAlgorithm_t(rawValue: rawValue)! }

	case none = 0
	case mD5 = 32771
	case sHA1 = 32772
	case sHA256 = 32780
	case sHA384 = 32781
	case sHA512 = 32782
}


public enum System_Configuration_Assemblies_AssemblyVersionCompatibility: Int32 {
	init(cValue: System_Configuration_Assemblies_AssemblyVersionCompatibility_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Configuration_Assemblies_AssemblyVersionCompatibility_t { System_Configuration_Assemblies_AssemblyVersionCompatibility_t(rawValue: rawValue)! }

	case sameMachine = 1
	case sameProcess = 2
	case sameDomain = 3
}


public enum System_Globalization_UnicodeCategory: Int32 {
	init(cValue: System_Globalization_UnicodeCategory_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Globalization_UnicodeCategory_t { System_Globalization_UnicodeCategory_t(rawValue: rawValue)! }

	case uppercaseLetter = 0
	case lowercaseLetter = 1
	case titlecaseLetter = 2
	case modifierLetter = 3
	case otherLetter = 4
	case nonSpacingMark = 5
	case spacingCombiningMark = 6
	case enclosingMark = 7
	case decimalDigitNumber = 8
	case letterNumber = 9
	case otherNumber = 10
	case spaceSeparator = 11
	case lineSeparator = 12
	case paragraphSeparator = 13
	case control = 14
	case format = 15
	case surrogate = 16
	case privateUse = 17
	case connectorPunctuation = 18
	case dashPunctuation = 19
	case openPunctuation = 20
	case closePunctuation = 21
	case initialQuotePunctuation = 22
	case finalQuotePunctuation = 23
	case otherPunctuation = 24
	case mathSymbol = 25
	case currencySymbol = 26
	case modifierSymbol = 27
	case otherSymbol = 28
	case otherNotAssigned = 29
}


public enum System_MidpointRounding: Int32 {
	init(cValue: System_MidpointRounding_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_MidpointRounding_t { System_MidpointRounding_t(rawValue: rawValue)! }

	case toEven = 0
	case awayFromZero = 1
	case toZero = 2
	case toNegativeInfinity = 3
	case toPositiveInfinity = 4
}


public struct System_Globalization_TimeSpanStyles: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Globalization_TimeSpanStyles_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Globalization_TimeSpanStyles_t { System_Globalization_TimeSpanStyles_t(rawValue: rawValue) }

	public static let none = System_Globalization_TimeSpanStyles([])
	public static let assumeNegative = System_Globalization_TimeSpanStyles(rawValue: 1)
}


public enum System_DateTimeKind: Int32 {
	init(cValue: System_DateTimeKind_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_DateTimeKind_t { System_DateTimeKind_t(rawValue: rawValue)! }

	case unspecified = 0
	case utc = 1
	case local = 2
}


public enum System_DayOfWeek: Int32 {
	init(cValue: System_DayOfWeek_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_DayOfWeek_t { System_DayOfWeek_t(rawValue: rawValue)! }

	case sunday = 0
	case monday = 1
	case tuesday = 2
	case wednesday = 3
	case thursday = 4
	case friday = 5
	case saturday = 6
}


public struct System_Globalization_DateTimeStyles: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Globalization_DateTimeStyles_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Globalization_DateTimeStyles_t { System_Globalization_DateTimeStyles_t(rawValue: rawValue) }

	public static let none = System_Globalization_DateTimeStyles([])
	public static let allowLeadingWhite = System_Globalization_DateTimeStyles(rawValue: 1)
	public static let allowTrailingWhite = System_Globalization_DateTimeStyles(rawValue: 2)
	public static let allowInnerWhite = System_Globalization_DateTimeStyles(rawValue: 4)
	public static let allowWhiteSpaces = System_Globalization_DateTimeStyles(rawValue: 7)
	public static let noCurrentDateDefault = System_Globalization_DateTimeStyles(rawValue: 8)
	public static let adjustToUniversal = System_Globalization_DateTimeStyles(rawValue: 16)
	public static let assumeLocal = System_Globalization_DateTimeStyles(rawValue: 32)
	public static let assumeUniversal = System_Globalization_DateTimeStyles(rawValue: 64)
	public static let roundtripKind = System_Globalization_DateTimeStyles(rawValue: 128)
}


public enum System_Globalization_CalendarAlgorithmType: Int32 {
	init(cValue: System_Globalization_CalendarAlgorithmType_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Globalization_CalendarAlgorithmType_t { System_Globalization_CalendarAlgorithmType_t(rawValue: rawValue)! }

	case unknown = 0
	case solarCalendar = 1
	case lunarCalendar = 2
	case lunisolarCalendar = 3
}


public enum System_Globalization_CalendarWeekRule: Int32 {
	init(cValue: System_Globalization_CalendarWeekRule_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Globalization_CalendarWeekRule_t { System_Globalization_CalendarWeekRule_t(rawValue: rawValue)! }

	case firstDay = 0
	case firstFullWeek = 1
	case firstFourDayWeek = 2
}


public struct System_Runtime_Serialization_StreamingContextStates: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Runtime_Serialization_StreamingContextStates_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Runtime_Serialization_StreamingContextStates_t { System_Runtime_Serialization_StreamingContextStates_t(rawValue: rawValue) }

	public static let crossProcess = System_Runtime_Serialization_StreamingContextStates(rawValue: 1)
	public static let crossMachine = System_Runtime_Serialization_StreamingContextStates(rawValue: 2)
	public static let file = System_Runtime_Serialization_StreamingContextStates(rawValue: 4)
	public static let persistence = System_Runtime_Serialization_StreamingContextStates(rawValue: 8)
	public static let remoting = System_Runtime_Serialization_StreamingContextStates(rawValue: 16)
	public static let other = System_Runtime_Serialization_StreamingContextStates(rawValue: 32)
	public static let clone = System_Runtime_Serialization_StreamingContextStates(rawValue: 64)
	public static let crossAppDomain = System_Runtime_Serialization_StreamingContextStates(rawValue: 128)
	public static let all = System_Runtime_Serialization_StreamingContextStates(rawValue: 255)
}


public struct System_Reflection_ParameterAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_ParameterAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_ParameterAttributes_t { System_Reflection_ParameterAttributes_t(rawValue: rawValue) }

	public static let none = System_Reflection_ParameterAttributes([])
	public static let `in` = System_Reflection_ParameterAttributes(rawValue: 1)
	public static let out = System_Reflection_ParameterAttributes(rawValue: 2)
	public static let lcid = System_Reflection_ParameterAttributes(rawValue: 4)
	public static let retval = System_Reflection_ParameterAttributes(rawValue: 8)
	public static let optional = System_Reflection_ParameterAttributes(rawValue: 16)
	public static let hasDefault = System_Reflection_ParameterAttributes(rawValue: 4096)
	public static let hasFieldMarshal = System_Reflection_ParameterAttributes(rawValue: 8192)
	public static let reserved3 = System_Reflection_ParameterAttributes(rawValue: 16384)
	public static let reserved4 = System_Reflection_ParameterAttributes(rawValue: 32768)
	public static let reservedMask = System_Reflection_ParameterAttributes(rawValue: 61440)
}


public struct System_Reflection_PortableExecutableKinds: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_PortableExecutableKinds_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_PortableExecutableKinds_t { System_Reflection_PortableExecutableKinds_t(rawValue: rawValue) }

	public static let notAPortableExecutableImage = System_Reflection_PortableExecutableKinds([])
	public static let iLOnly = System_Reflection_PortableExecutableKinds(rawValue: 1)
	public static let required32Bit = System_Reflection_PortableExecutableKinds(rawValue: 2)
	public static let pE32Plus = System_Reflection_PortableExecutableKinds(rawValue: 4)
	public static let unmanaged32Bit = System_Reflection_PortableExecutableKinds(rawValue: 8)
	public static let preferred32Bit = System_Reflection_PortableExecutableKinds(rawValue: 16)
}


public enum System_Reflection_ImageFileMachine: Int32 {
	init(cValue: System_Reflection_ImageFileMachine_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Reflection_ImageFileMachine_t { System_Reflection_ImageFileMachine_t(rawValue: rawValue)! }

	case i386 = 332
	case aRM = 452
	case iA64 = 512
	case aMD64 = 34404
}


public struct System_Reflection_BindingFlags: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_BindingFlags_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_BindingFlags_t { System_Reflection_BindingFlags_t(rawValue: rawValue) }

	public static let `default` = System_Reflection_BindingFlags([])
	public static let ignoreCase = System_Reflection_BindingFlags(rawValue: 1)
	public static let declaredOnly = System_Reflection_BindingFlags(rawValue: 2)
	public static let instance = System_Reflection_BindingFlags(rawValue: 4)
	public static let `static` = System_Reflection_BindingFlags(rawValue: 8)
	public static let `public` = System_Reflection_BindingFlags(rawValue: 16)
	public static let nonPublic = System_Reflection_BindingFlags(rawValue: 32)
	public static let flattenHierarchy = System_Reflection_BindingFlags(rawValue: 64)
	public static let invokeMethod = System_Reflection_BindingFlags(rawValue: 256)
	public static let createInstance = System_Reflection_BindingFlags(rawValue: 512)
	public static let getField = System_Reflection_BindingFlags(rawValue: 1024)
	public static let setField = System_Reflection_BindingFlags(rawValue: 2048)
	public static let getProperty = System_Reflection_BindingFlags(rawValue: 4096)
	public static let setProperty = System_Reflection_BindingFlags(rawValue: 8192)
	public static let putDispProperty = System_Reflection_BindingFlags(rawValue: 16384)
	public static let putRefDispProperty = System_Reflection_BindingFlags(rawValue: 32768)
	public static let exactBinding = System_Reflection_BindingFlags(rawValue: 65536)
	public static let suppressChangeType = System_Reflection_BindingFlags(rawValue: 131072)
	public static let optionalParamBinding = System_Reflection_BindingFlags(rawValue: 262144)
	public static let ignoreReturn = System_Reflection_BindingFlags(rawValue: 16777216)
	public static let doNotWrapExceptions = System_Reflection_BindingFlags(rawValue: 33554432)
}


public struct System_Reflection_FieldAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_FieldAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_FieldAttributes_t { System_Reflection_FieldAttributes_t(rawValue: rawValue) }

	public static let privateScope = System_Reflection_FieldAttributes([])
	public static let `private` = System_Reflection_FieldAttributes(rawValue: 1)
	public static let famANDAssem = System_Reflection_FieldAttributes(rawValue: 2)
	public static let assembly = System_Reflection_FieldAttributes(rawValue: 3)
	public static let family = System_Reflection_FieldAttributes(rawValue: 4)
	public static let famORAssem = System_Reflection_FieldAttributes(rawValue: 5)
	public static let `public` = System_Reflection_FieldAttributes(rawValue: 6)
	public static let fieldAccessMask = System_Reflection_FieldAttributes(rawValue: 7)
	public static let `static` = System_Reflection_FieldAttributes(rawValue: 16)
	public static let initOnly = System_Reflection_FieldAttributes(rawValue: 32)
	public static let literal = System_Reflection_FieldAttributes(rawValue: 64)
	public static let notSerialized = System_Reflection_FieldAttributes(rawValue: 128)
	public static let hasFieldRVA = System_Reflection_FieldAttributes(rawValue: 256)
	public static let specialName = System_Reflection_FieldAttributes(rawValue: 512)
	public static let rTSpecialName = System_Reflection_FieldAttributes(rawValue: 1024)
	public static let hasFieldMarshal = System_Reflection_FieldAttributes(rawValue: 4096)
	public static let pinvokeImpl = System_Reflection_FieldAttributes(rawValue: 8192)
	public static let hasDefault = System_Reflection_FieldAttributes(rawValue: 32768)
	public static let reservedMask = System_Reflection_FieldAttributes(rawValue: 38144)
}


public struct System_Reflection_PropertyAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_PropertyAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_PropertyAttributes_t { System_Reflection_PropertyAttributes_t(rawValue: rawValue) }

	public static let none = System_Reflection_PropertyAttributes([])
	public static let specialName = System_Reflection_PropertyAttributes(rawValue: 512)
	public static let rTSpecialName = System_Reflection_PropertyAttributes(rawValue: 1024)
	public static let hasDefault = System_Reflection_PropertyAttributes(rawValue: 4096)
	public static let reserved2 = System_Reflection_PropertyAttributes(rawValue: 8192)
	public static let reserved3 = System_Reflection_PropertyAttributes(rawValue: 16384)
	public static let reserved4 = System_Reflection_PropertyAttributes(rawValue: 32768)
	public static let reservedMask = System_Reflection_PropertyAttributes(rawValue: 62464)
}


public struct System_Reflection_CallingConventions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_CallingConventions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_CallingConventions_t { System_Reflection_CallingConventions_t(rawValue: rawValue) }

	public static let standard = System_Reflection_CallingConventions(rawValue: 1)
	public static let varArgs = System_Reflection_CallingConventions(rawValue: 2)
	public static let any = System_Reflection_CallingConventions(rawValue: 3)
	public static let hasThis = System_Reflection_CallingConventions(rawValue: 32)
	public static let explicitThis = System_Reflection_CallingConventions(rawValue: 64)
}


public struct System_Reflection_MethodAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_MethodAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_MethodAttributes_t { System_Reflection_MethodAttributes_t(rawValue: rawValue) }

	public static let privateScope = System_Reflection_MethodAttributes([])
	public static let reuseSlot = System_Reflection_MethodAttributes([])
	public static let `private` = System_Reflection_MethodAttributes(rawValue: 1)
	public static let famANDAssem = System_Reflection_MethodAttributes(rawValue: 2)
	public static let assembly = System_Reflection_MethodAttributes(rawValue: 3)
	public static let family = System_Reflection_MethodAttributes(rawValue: 4)
	public static let famORAssem = System_Reflection_MethodAttributes(rawValue: 5)
	public static let `public` = System_Reflection_MethodAttributes(rawValue: 6)
	public static let memberAccessMask = System_Reflection_MethodAttributes(rawValue: 7)
	public static let unmanagedExport = System_Reflection_MethodAttributes(rawValue: 8)
	public static let `static` = System_Reflection_MethodAttributes(rawValue: 16)
	public static let `final` = System_Reflection_MethodAttributes(rawValue: 32)
	public static let virtual = System_Reflection_MethodAttributes(rawValue: 64)
	public static let hideBySig = System_Reflection_MethodAttributes(rawValue: 128)
	public static let newSlot = System_Reflection_MethodAttributes(rawValue: 256)
	public static let vtableLayoutMask = System_Reflection_MethodAttributes(rawValue: 256)
	public static let checkAccessOnOverride = System_Reflection_MethodAttributes(rawValue: 512)
	public static let abstract = System_Reflection_MethodAttributes(rawValue: 1024)
	public static let specialName = System_Reflection_MethodAttributes(rawValue: 2048)
	public static let rTSpecialName = System_Reflection_MethodAttributes(rawValue: 4096)
	public static let pinvokeImpl = System_Reflection_MethodAttributes(rawValue: 8192)
	public static let hasSecurity = System_Reflection_MethodAttributes(rawValue: 16384)
	public static let requireSecObject = System_Reflection_MethodAttributes(rawValue: 32768)
	public static let reservedMask = System_Reflection_MethodAttributes(rawValue: 53248)
}


public enum System_Reflection_MethodImplAttributes: Int32 {
	init(cValue: System_Reflection_MethodImplAttributes_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Reflection_MethodImplAttributes_t { System_Reflection_MethodImplAttributes_t(rawValue: rawValue)! }

	case iL = 0
	public static let managed = System_Reflection_MethodImplAttributes.iL
	case native = 1
	case oPTIL = 2
	case codeTypeMask = 3
	public static let runtime = System_Reflection_MethodImplAttributes.codeTypeMask
	case managedMask = 4
	public static let unmanaged = System_Reflection_MethodImplAttributes.managedMask
	case noInlining = 8
	case forwardRef = 16
	case synchronized = 32
	case noOptimization = 64
	case preserveSig = 128
	case aggressiveInlining = 256
	case aggressiveOptimization = 512
	case internalCall = 4096
	case maxMethodImplVal = 65535
}


public struct System_Reflection_ExceptionHandlingClauseOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_ExceptionHandlingClauseOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_ExceptionHandlingClauseOptions_t { System_Reflection_ExceptionHandlingClauseOptions_t(rawValue: rawValue) }

	public static let clause = System_Reflection_ExceptionHandlingClauseOptions([])
	public static let filter = System_Reflection_ExceptionHandlingClauseOptions(rawValue: 1)
	public static let finally = System_Reflection_ExceptionHandlingClauseOptions(rawValue: 2)
	public static let fault = System_Reflection_ExceptionHandlingClauseOptions(rawValue: 4)
}


public enum System_Threading_Tasks_TaskStatus: Int32 {
	init(cValue: System_Threading_Tasks_TaskStatus_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Threading_Tasks_TaskStatus_t { System_Threading_Tasks_TaskStatus_t(rawValue: rawValue)! }

	case created = 0
	case waitingForActivation = 1
	case waitingToRun = 2
	case running = 3
	case waitingForChildrenToComplete = 4
	case ranToCompletion = 5
	case canceled = 6
	case faulted = 7
}


public struct System_Threading_Tasks_TaskCreationOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Threading_Tasks_TaskCreationOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Threading_Tasks_TaskCreationOptions_t { System_Threading_Tasks_TaskCreationOptions_t(rawValue: rawValue) }

	public static let none = System_Threading_Tasks_TaskCreationOptions([])
	public static let preferFairness = System_Threading_Tasks_TaskCreationOptions(rawValue: 1)
	public static let longRunning = System_Threading_Tasks_TaskCreationOptions(rawValue: 2)
	public static let attachedToParent = System_Threading_Tasks_TaskCreationOptions(rawValue: 4)
	public static let denyChildAttach = System_Threading_Tasks_TaskCreationOptions(rawValue: 8)
	public static let hideScheduler = System_Threading_Tasks_TaskCreationOptions(rawValue: 16)
	public static let runContinuationsAsynchronously = System_Threading_Tasks_TaskCreationOptions(rawValue: 64)
}


public enum System_Threading_Tasks_Sources_ValueTaskSourceStatus: Int32 {
	init(cValue: System_Threading_Tasks_Sources_ValueTaskSourceStatus_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Threading_Tasks_Sources_ValueTaskSourceStatus_t { System_Threading_Tasks_Sources_ValueTaskSourceStatus_t(rawValue: rawValue)! }

	case pending = 0
	case succeeded = 1
	case faulted = 2
	case canceled = 3
}


public struct System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_t { System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_t(rawValue: rawValue) }

	public static let none = System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags([])
	public static let useSchedulingContext = System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags(rawValue: 1)
	public static let flowExecutionContext = System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags(rawValue: 2)
}


public struct System_Threading_Tasks_TaskContinuationOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Threading_Tasks_TaskContinuationOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Threading_Tasks_TaskContinuationOptions_t { System_Threading_Tasks_TaskContinuationOptions_t(rawValue: rawValue) }

	public static let none = System_Threading_Tasks_TaskContinuationOptions([])
	public static let preferFairness = System_Threading_Tasks_TaskContinuationOptions(rawValue: 1)
	public static let longRunning = System_Threading_Tasks_TaskContinuationOptions(rawValue: 2)
	public static let attachedToParent = System_Threading_Tasks_TaskContinuationOptions(rawValue: 4)
	public static let denyChildAttach = System_Threading_Tasks_TaskContinuationOptions(rawValue: 8)
	public static let hideScheduler = System_Threading_Tasks_TaskContinuationOptions(rawValue: 16)
	public static let lazyCancellation = System_Threading_Tasks_TaskContinuationOptions(rawValue: 32)
	public static let runContinuationsAsynchronously = System_Threading_Tasks_TaskContinuationOptions(rawValue: 64)
	public static let notOnRanToCompletion = System_Threading_Tasks_TaskContinuationOptions(rawValue: 65536)
	public static let notOnFaulted = System_Threading_Tasks_TaskContinuationOptions(rawValue: 131072)
	public static let onlyOnCanceled = System_Threading_Tasks_TaskContinuationOptions(rawValue: 196608)
	public static let notOnCanceled = System_Threading_Tasks_TaskContinuationOptions(rawValue: 262144)
	public static let onlyOnFaulted = System_Threading_Tasks_TaskContinuationOptions(rawValue: 327680)
	public static let onlyOnRanToCompletion = System_Threading_Tasks_TaskContinuationOptions(rawValue: 393216)
	public static let executeSynchronously = System_Threading_Tasks_TaskContinuationOptions(rawValue: 524288)
}


public enum System_Runtime_InteropServices_GCHandleType: Int32 {
	init(cValue: System_Runtime_InteropServices_GCHandleType_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Runtime_InteropServices_GCHandleType_t { System_Runtime_InteropServices_GCHandleType_t(rawValue: rawValue)! }

	case `weak` = 0
	case weakTrackResurrection = 1
	case normal = 2
	case pinned = 3
}


public enum System_IO_SeekOrigin: Int32 {
	init(cValue: System_IO_SeekOrigin_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_IO_SeekOrigin_t { System_IO_SeekOrigin_t(rawValue: rawValue)! }

	case begin = 0
	case current = 1
	case end = 2
}


public struct System_IO_FileAccess: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_IO_FileAccess_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_IO_FileAccess_t { System_IO_FileAccess_t(rawValue: rawValue) }

	public static let read = System_IO_FileAccess(rawValue: 1)
	public static let write = System_IO_FileAccess(rawValue: 2)
	public static let readWrite = System_IO_FileAccess(rawValue: 3)
}


public enum System_IO_FileMode: Int32 {
	init(cValue: System_IO_FileMode_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_IO_FileMode_t { System_IO_FileMode_t(rawValue: rawValue)! }

	case createNew = 1
	case create = 2
	case `open` = 3
	case openOrCreate = 4
	case truncate = 5
	case append = 6
}


public struct System_IO_FileShare: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_IO_FileShare_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_IO_FileShare_t { System_IO_FileShare_t(rawValue: rawValue) }

	public static let none = System_IO_FileShare([])
	public static let read = System_IO_FileShare(rawValue: 1)
	public static let write = System_IO_FileShare(rawValue: 2)
	public static let readWrite = System_IO_FileShare(rawValue: 3)
	public static let delete = System_IO_FileShare(rawValue: 4)
	public static let inheritable = System_IO_FileShare(rawValue: 16)
}


public struct System_IO_FileOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_IO_FileOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_IO_FileOptions_t { System_IO_FileOptions_t(rawValue: rawValue) }

	public static let none = System_IO_FileOptions([])
	public static let encrypted = System_IO_FileOptions(rawValue: 16384)
	public static let deleteOnClose = System_IO_FileOptions(rawValue: 67108864)
	public static let sequentialScan = System_IO_FileOptions(rawValue: 134217728)
	public static let randomAccess = System_IO_FileOptions(rawValue: 268435456)
	public static let asynchronous = System_IO_FileOptions(rawValue: 1073741824)
	public static let writeThrough = System_IO_FileOptions(rawValue: -2147483648)
}


public struct System_IO_UnixFileMode: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_IO_UnixFileMode_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_IO_UnixFileMode_t { System_IO_UnixFileMode_t(rawValue: rawValue) }

	public static let none = System_IO_UnixFileMode([])
	public static let otherExecute = System_IO_UnixFileMode(rawValue: 1)
	public static let otherWrite = System_IO_UnixFileMode(rawValue: 2)
	public static let otherRead = System_IO_UnixFileMode(rawValue: 4)
	public static let groupExecute = System_IO_UnixFileMode(rawValue: 8)
	public static let groupWrite = System_IO_UnixFileMode(rawValue: 16)
	public static let groupRead = System_IO_UnixFileMode(rawValue: 32)
	public static let userExecute = System_IO_UnixFileMode(rawValue: 64)
	public static let userWrite = System_IO_UnixFileMode(rawValue: 128)
	public static let userRead = System_IO_UnixFileMode(rawValue: 256)
	public static let stickyBit = System_IO_UnixFileMode(rawValue: 512)
	public static let setGroup = System_IO_UnixFileMode(rawValue: 1024)
	public static let setUser = System_IO_UnixFileMode(rawValue: 2048)
}


public struct System_Reflection_EventAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_EventAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_EventAttributes_t { System_Reflection_EventAttributes_t(rawValue: rawValue) }

	public static let none = System_Reflection_EventAttributes([])
	public static let specialName = System_Reflection_EventAttributes(rawValue: 512)
	public static let rTSpecialName = System_Reflection_EventAttributes(rawValue: 1024)
	public static let reservedMask = System_Reflection_EventAttributes(rawValue: 1024)
}


public struct System_Reflection_ResourceLocation: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_ResourceLocation_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_ResourceLocation_t { System_Reflection_ResourceLocation_t(rawValue: rawValue) }

	public static let embedded = System_Reflection_ResourceLocation(rawValue: 1)
	public static let containedInAnotherAssembly = System_Reflection_ResourceLocation(rawValue: 2)
	public static let containedInManifestFile = System_Reflection_ResourceLocation(rawValue: 4)
}


public enum System_Security_SecurityRuleSet: UInt8 {
	init(cValue: System_Security_SecurityRuleSet_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Security_SecurityRuleSet_t { System_Security_SecurityRuleSet_t(rawValue: rawValue)! }

	case none = 0
	case level1 = 1
	case level2 = 2
}


public enum System_Buffers_OperationStatus: Int32 {
	init(cValue: System_Buffers_OperationStatus_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Buffers_OperationStatus_t { System_Buffers_OperationStatus_t(rawValue: rawValue)! }

	case done = 0
	case destinationTooSmall = 1
	case needMoreData = 2
	case invalidData = 3
}


public struct System_Globalization_CompareOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Globalization_CompareOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Globalization_CompareOptions_t { System_Globalization_CompareOptions_t(rawValue: rawValue) }

	public static let none = System_Globalization_CompareOptions([])
	public static let ignoreCase = System_Globalization_CompareOptions(rawValue: 1)
	public static let ignoreNonSpace = System_Globalization_CompareOptions(rawValue: 2)
	public static let ignoreSymbols = System_Globalization_CompareOptions(rawValue: 4)
	public static let ignoreKanaType = System_Globalization_CompareOptions(rawValue: 8)
	public static let ignoreWidth = System_Globalization_CompareOptions(rawValue: 16)
	public static let ordinalIgnoreCase = System_Globalization_CompareOptions(rawValue: 268435456)
	public static let stringSort = System_Globalization_CompareOptions(rawValue: 536870912)
	public static let ordinal = System_Globalization_CompareOptions(rawValue: 1073741824)
}


public enum System_Globalization_DigitShapes: Int32 {
	init(cValue: System_Globalization_DigitShapes_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Globalization_DigitShapes_t { System_Globalization_DigitShapes_t(rawValue: rawValue)! }

	case context = 0
	case none = 1
	case nativeNational = 2
}


public enum System_Text_NormalizationForm: Int32 {
	init(cValue: System_Text_NormalizationForm_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Text_NormalizationForm_t { System_Text_NormalizationForm_t(rawValue: rawValue)! }

	case formC = 1
	case formD = 2
	case formKC = 5
	case formKD = 6
}


public struct System_StringSplitOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_StringSplitOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_StringSplitOptions_t { System_StringSplitOptions_t(rawValue: rawValue) }

	public static let none = System_StringSplitOptions([])
	public static let removeEmptyEntries = System_StringSplitOptions(rawValue: 1)
	public static let trimEntries = System_StringSplitOptions(rawValue: 2)
}


public struct System_Reflection_GenericParameterAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_GenericParameterAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_GenericParameterAttributes_t { System_Reflection_GenericParameterAttributes_t(rawValue: rawValue) }

	public static let none = System_Reflection_GenericParameterAttributes([])
	public static let covariant = System_Reflection_GenericParameterAttributes(rawValue: 1)
	public static let contravariant = System_Reflection_GenericParameterAttributes(rawValue: 2)
	public static let varianceMask = System_Reflection_GenericParameterAttributes(rawValue: 3)
	public static let referenceTypeConstraint = System_Reflection_GenericParameterAttributes(rawValue: 4)
	public static let notNullableValueTypeConstraint = System_Reflection_GenericParameterAttributes(rawValue: 8)
	public static let defaultConstructorConstraint = System_Reflection_GenericParameterAttributes(rawValue: 16)
	public static let specialConstraintMask = System_Reflection_GenericParameterAttributes(rawValue: 28)
}


public struct System_Reflection_TypeAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Reflection_TypeAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Reflection_TypeAttributes_t { System_Reflection_TypeAttributes_t(rawValue: rawValue) }

	public static let notPublic = System_Reflection_TypeAttributes([])
	public static let autoLayout = System_Reflection_TypeAttributes([])
	public static let ansiClass = System_Reflection_TypeAttributes([])
	public static let `class` = System_Reflection_TypeAttributes([])
	public static let `public` = System_Reflection_TypeAttributes(rawValue: 1)
	public static let nestedPublic = System_Reflection_TypeAttributes(rawValue: 2)
	public static let nestedPrivate = System_Reflection_TypeAttributes(rawValue: 3)
	public static let nestedFamily = System_Reflection_TypeAttributes(rawValue: 4)
	public static let nestedAssembly = System_Reflection_TypeAttributes(rawValue: 5)
	public static let nestedFamANDAssem = System_Reflection_TypeAttributes(rawValue: 6)
	public static let visibilityMask = System_Reflection_TypeAttributes(rawValue: 7)
	public static let nestedFamORAssem = System_Reflection_TypeAttributes(rawValue: 7)
	public static let sequentialLayout = System_Reflection_TypeAttributes(rawValue: 8)
	public static let explicitLayout = System_Reflection_TypeAttributes(rawValue: 16)
	public static let layoutMask = System_Reflection_TypeAttributes(rawValue: 24)
	public static let interface = System_Reflection_TypeAttributes(rawValue: 32)
	public static let classSemanticsMask = System_Reflection_TypeAttributes(rawValue: 32)
	public static let abstract = System_Reflection_TypeAttributes(rawValue: 128)
	public static let sealed = System_Reflection_TypeAttributes(rawValue: 256)
	public static let specialName = System_Reflection_TypeAttributes(rawValue: 1024)
	public static let rTSpecialName = System_Reflection_TypeAttributes(rawValue: 2048)
	public static let `import` = System_Reflection_TypeAttributes(rawValue: 4096)
	public static let serializable = System_Reflection_TypeAttributes(rawValue: 8192)
	public static let windowsRuntime = System_Reflection_TypeAttributes(rawValue: 16384)
	public static let unicodeClass = System_Reflection_TypeAttributes(rawValue: 65536)
	public static let autoClass = System_Reflection_TypeAttributes(rawValue: 131072)
	public static let stringFormatMask = System_Reflection_TypeAttributes(rawValue: 196608)
	public static let customFormatClass = System_Reflection_TypeAttributes(rawValue: 196608)
	public static let hasSecurity = System_Reflection_TypeAttributes(rawValue: 262144)
	public static let reservedMask = System_Reflection_TypeAttributes(rawValue: 264192)
	public static let beforeFieldInit = System_Reflection_TypeAttributes(rawValue: 1048576)
	public static let customFormatMask = System_Reflection_TypeAttributes(rawValue: 12582912)
}


public enum System_Runtime_InteropServices_LayoutKind: Int32 {
	init(cValue: System_Runtime_InteropServices_LayoutKind_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Runtime_InteropServices_LayoutKind_t { System_Runtime_InteropServices_LayoutKind_t(rawValue: rawValue)! }

	case sequential = 0
	case explicit = 2
	case auto = 3
}


public enum System_Runtime_InteropServices_CharSet: Int32 {
	init(cValue: System_Runtime_InteropServices_CharSet_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Runtime_InteropServices_CharSet_t { System_Runtime_InteropServices_CharSet_t(rawValue: rawValue)! }

	case none = 1
	case ansi = 2
	case unicode = 3
	case auto = 4
}


public enum System_Runtime_InteropServices_CustomQueryInterfaceMode: Int32 {
	init(cValue: System_Runtime_InteropServices_CustomQueryInterfaceMode_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Runtime_InteropServices_CustomQueryInterfaceMode_t { System_Runtime_InteropServices_CustomQueryInterfaceMode_t(rawValue: rawValue)! }

	case ignore = 0
	case allow = 1
}


public enum System_GCKind: Int32 {
	init(cValue: System_GCKind_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_GCKind_t { System_GCKind_t(rawValue: rawValue)! }

	case any = 0
	case ephemeral = 1
	case fullBlocking = 2
	case background = 3
}


public enum System_GCCollectionMode: Int32 {
	init(cValue: System_GCCollectionMode_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_GCCollectionMode_t { System_GCCollectionMode_t(rawValue: rawValue)! }

	case `default` = 0
	case forced = 1
	case optimized = 2
	case aggressive = 3
}


public enum System_GCNotificationStatus: Int32 {
	init(cValue: System_GCNotificationStatus_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_GCNotificationStatus_t { System_GCNotificationStatus_t(rawValue: rawValue)! }

	case succeeded = 0
	case failed = 1
	case canceled = 2
	case timeout = 3
	case notApplicable = 4
}


public struct System_Base64FormattingOptions: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Base64FormattingOptions_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Base64FormattingOptions_t { System_Base64FormattingOptions_t(rawValue: rawValue) }

	public static let none = System_Base64FormattingOptions([])
	public static let insertLineBreaks = System_Base64FormattingOptions(rawValue: 1)
}


public enum System_Threading_ThreadPriority: Int32 {
	init(cValue: System_Threading_ThreadPriority_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Threading_ThreadPriority_t { System_Threading_ThreadPriority_t(rawValue: rawValue)! }

	case lowest = 0
	case belowNormal = 1
	case normal = 2
	case aboveNormal = 3
	case highest = 4
}


public struct System_Threading_ThreadState: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_Threading_ThreadState_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_Threading_ThreadState_t { System_Threading_ThreadState_t(rawValue: rawValue) }

	public static let running = System_Threading_ThreadState([])
	public static let stopRequested = System_Threading_ThreadState(rawValue: 1)
	public static let suspendRequested = System_Threading_ThreadState(rawValue: 2)
	public static let background = System_Threading_ThreadState(rawValue: 4)
	public static let unstarted = System_Threading_ThreadState(rawValue: 8)
	public static let stopped = System_Threading_ThreadState(rawValue: 16)
	public static let waitSleepJoin = System_Threading_ThreadState(rawValue: 32)
	public static let suspended = System_Threading_ThreadState(rawValue: 64)
	public static let abortRequested = System_Threading_ThreadState(rawValue: 128)
	public static let aborted = System_Threading_ThreadState(rawValue: 256)
}


public enum System_Threading_ApartmentState: Int32 {
	init(cValue: System_Threading_ApartmentState_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Threading_ApartmentState_t { System_Threading_ApartmentState_t(rawValue: rawValue)! }

	case sTA = 0
	case mTA = 1
	case unknown = 2
}


public enum System_Security_Permissions_PermissionState: Int32 {
	init(cValue: System_Security_Permissions_PermissionState_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Security_Permissions_PermissionState_t { System_Security_Permissions_PermissionState_t(rawValue: rawValue)! }

	case none = 0
	case unrestricted = 1
}


public enum System_Security_Principal_PrincipalPolicy: Int32 {
	init(cValue: System_Security_Principal_PrincipalPolicy_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_Security_Principal_PrincipalPolicy_t { System_Security_Principal_PrincipalPolicy_t(rawValue: rawValue)! }

	case unauthenticatedPrincipal = 0
	case noPrincipal = 1
	case windowsPrincipal = 2
}


public struct System_IO_FileAttributes: OptionSet {
	public typealias RawValue = Int32
	public let rawValue: RawValue

	public init(rawValue: RawValue) {
		self.rawValue = rawValue
	}

	init(cValue: System_IO_FileAttributes_t) {
		self.init(rawValue: cValue.rawValue)
	}

	var cValue: System_IO_FileAttributes_t { System_IO_FileAttributes_t(rawValue: rawValue) }

	public static let readOnly = System_IO_FileAttributes(rawValue: 1)
	public static let hidden = System_IO_FileAttributes(rawValue: 2)
	public static let system = System_IO_FileAttributes(rawValue: 4)
	public static let directory = System_IO_FileAttributes(rawValue: 16)
	public static let archive = System_IO_FileAttributes(rawValue: 32)
	public static let device = System_IO_FileAttributes(rawValue: 64)
	public static let normal = System_IO_FileAttributes(rawValue: 128)
	public static let temporary = System_IO_FileAttributes(rawValue: 256)
	public static let sparseFile = System_IO_FileAttributes(rawValue: 512)
	public static let reparsePoint = System_IO_FileAttributes(rawValue: 1024)
	public static let compressed = System_IO_FileAttributes(rawValue: 2048)
	public static let offline = System_IO_FileAttributes(rawValue: 4096)
	public static let notContentIndexed = System_IO_FileAttributes(rawValue: 8192)
	public static let encrypted = System_IO_FileAttributes(rawValue: 16384)
	public static let integrityStream = System_IO_FileAttributes(rawValue: 32768)
	public static let noScrubData = System_IO_FileAttributes(rawValue: 131072)
}


public enum System_IO_SearchOption: Int32 {
	init(cValue: System_IO_SearchOption_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_IO_SearchOption_t { System_IO_SearchOption_t(rawValue: rawValue)! }

	case topDirectoryOnly = 0
	case allDirectories = 1
}


public enum System_IO_MatchType: Int32 {
	init(cValue: System_IO_MatchType_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_IO_MatchType_t { System_IO_MatchType_t(rawValue: rawValue)! }

	case simple = 0
	case win32 = 1
}


public enum System_IO_MatchCasing: Int32 {
	init(cValue: System_IO_MatchCasing_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: System_IO_MatchCasing_t { System_IO_MatchCasing_t(rawValue: rawValue)! }

	case platformDefault = 0
	case caseSensitive = 1
	case caseInsensitive = 2
}


public enum NativeAOT_CodeGeneratorInputSample_NiceLevels: UInt32 {
	init(cValue: NativeAOT_CodeGeneratorInputSample_NiceLevels_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: NativeAOT_CodeGeneratorInputSample_NiceLevels_t { NativeAOT_CodeGeneratorInputSample_NiceLevels_t(rawValue: rawValue)! }

	case notNice = 0
	case littleBitNice = 1
	case nice = 2
	case veryNice = 3
}


public enum NativeAOT_CodeGeneratorInputSample_TestEnum: Int32 {
	init(cValue: NativeAOT_CodeGeneratorInputSample_TestEnum_t) {
		self.init(rawValue: cValue.rawValue)!
	}

	var cValue: NativeAOT_CodeGeneratorInputSample_TestEnum_t { NativeAOT_CodeGeneratorInputSample_TestEnum_t(rawValue: rawValue)! }

	case firstCase = 0
	case secondCase = 1
}



















// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.





// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TOutput[]" was skipped. Reason: It has no full name.
// Type "TOutput" was skipped. Reason: It has no full name.

// Type "TInput[]" was skipped. Reason: It has no full name.
// Type "TInput" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TKey[]" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.

// Type "TValue[]" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TKey[]" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.

// Type "TValue[]" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TKey[]" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.

// Type "TValue[]" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TKey[]" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.

// Type "TValue[]" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.










// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.





// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.

















// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.




// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.



// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.



// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.



// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.


// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.



// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
















// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.






































































// Type "TResult" was skipped. Reason: It has no full name.





// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.







// Type "TResult" was skipped. Reason: It has no full name.


// Type "Task`1" was skipped. Reason: It has no full name.
// Type "TResult[]" was skipped. Reason: It has no full name.
// Type "TResult" was skipped. Reason: It has no full name.

// Type "TaskFactory`1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
// Type "TArg3" was skipped. Reason: It has no full name.
// Type "TaskAwaiter`1" was skipped. Reason: It has no full name.
// Type "ConfiguredTaskAwaitable`1" was skipped. Reason: It has no full name.
// Type "ConfiguredTaskAwaiter" was skipped. Reason: It has no full name.
// Type "Task`1" was skipped. Reason: It has no full name.
// Type "TResult[]" was skipped. Reason: It has no full name.
// Type "TResult" was skipped. Reason: It has no full name.

// Type "TaskFactory`1" was skipped. Reason: It has no full name.
// Type "TaskAwaiter`1" was skipped. Reason: It has no full name.
// Type "ConfiguredTaskAwaitable`1" was skipped. Reason: It has no full name.
// Type "ConfiguredTaskAwaiter" was skipped. Reason: It has no full name.































































// Type "TState" was skipped. Reason: It has no full name.







// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.














// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum[]" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.

// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.
// Type "TEnum" was skipped. Reason: It has no full name.

// Type "TEnum" was skipped. Reason: It has no full name.

// Type "TEnum" was skipped. Reason: It has no full name.

// Type "TEnum" was skipped. Reason: It has no full name.

// Type "TEnum" was skipped. Reason: It has no full name.












// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.





// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TDelegate" was skipped. Reason: It has no full name.
// Type "TDelegate" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "TWrapper" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.



// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.


































// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.








// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
























































// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.


// Type "TValue" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.


// Type "T1" was skipped. Reason: It has no full name.

// Type "T1" was skipped. Reason: It has no full name.
// Type "T2" was skipped. Reason: It has no full name.

// Type "T1" was skipped. Reason: It has no full name.
// Type "T2" was skipped. Reason: It has no full name.
// Type "T3" was skipped. Reason: It has no full name.



// Type "T" was skipped. Reason: It has no full name.





// Type "T" was skipped. Reason: It has no full name.

// Type "TM" was skipped. Reason: It has no full name.






















// Type "TKey" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.












// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

























// MARK: - END Type Definitions

// MARK: - BEGIN APIs
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members
// TODO: Members

// MARK: - END APIs

// MARK: - BEGIN Footer


// MARK: - END Footer

