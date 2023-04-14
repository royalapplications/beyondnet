// Number of generated types: 837
// Number of generated members: 4384

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

// MARK: - BEGIN APIs
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


public class System_Object /* System.Object */ {
	let _handle: System_Object_t

	required init(handle: System_Object_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Object_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_Object? /* System.Object */ objA, System_Object? /* System.Object */ objB) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func referenceEquals(System_Object? /* System.Object */ objA, System_Object? /* System.Object */ objB) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Object_TypeOf())
		
	
	}
	
	deinit {
		System_Object_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Type /* System.Type */ {
	static func getType(System_String? /* System.String */ typeName, Bool /* System.Boolean */ throwOnError, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getType(System_String? /* System.String */ typeName, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getType(System_String? /* System.String */ typeName) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromHandle(System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ handle) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getElementType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getArrayRank() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericTypeDefinition() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericArguments() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOptionalCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRequiredCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericParameterConstraints() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isAssignableTo(System_Type? /* System.Type */ targetType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructor(System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructor(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructor(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructor(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Reflection_CallingConventions /* System.Reflection.CallingConventions */ callConvention, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructors() throws -> System_Reflection_ConstructorInfo_Array? /* System.Reflection.ConstructorInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstructors(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_ConstructorInfo_Array? /* System.Reflection.ConstructorInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEvent(System_String? /* System.String */ name) throws -> System_Reflection_EventInfo? /* System.Reflection.EventInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEvent(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_EventInfo? /* System.Reflection.EventInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEvents() throws -> System_Reflection_EventInfo_Array? /* System.Reflection.EventInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEvents(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_EventInfo_Array? /* System.Reflection.EventInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getField(System_String? /* System.String */ name) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getField(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFields() throws -> System_Reflection_FieldInfo_Array? /* System.Reflection.FieldInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFields(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_FieldInfo_Array? /* System.Reflection.FieldInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFunctionPointerCallingConventions() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFunctionPointerReturnType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFunctionPointerParameterTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMember(System_String? /* System.String */ name) throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMember(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMember(System_String? /* System.String */ name, System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ type, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMembers() throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberWithSameMetadataDefinitionAs(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ member) throws -> System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMembers(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Reflection_CallingConventions /* System.Reflection.CallingConventions */ callConvention, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, Int32 /* System.Int32 */ genericParameterCount, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, Int32 /* System.Int32 */ genericParameterCount, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, Int32 /* System.Int32 */ genericParameterCount, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, Int32 /* System.Int32 */ genericParameterCount, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Reflection_CallingConventions /* System.Reflection.CallingConventions */ callConvention, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethods() throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethods(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNestedType(System_String? /* System.String */ name) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNestedType(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNestedTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNestedTypes(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Type? /* System.Type */ returnType) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Type? /* System.Type */ returnType, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Type? /* System.Type */ returnType, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperty(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Type? /* System.Type */ returnType, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperties() throws -> System_Reflection_PropertyInfo_Array? /* System.Reflection.PropertyInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getProperties(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_PropertyInfo_Array? /* System.Reflection.PropertyInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDefaultMembers() throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeHandle(System_Object? /* System.Object */ o) throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeArray(System_Object_Array? /* System.Object[] */ args) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeCode(System_Type? /* System.Type */ type) throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromCLSID(System_Guid? /* System.Guid */ clsid) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromCLSID(System_Guid? /* System.Guid */ clsid, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromCLSID(System_Guid? /* System.Guid */ clsid, System_String? /* System.String */ server) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromCLSID(System_Guid? /* System.Guid */ clsid, System_String? /* System.String */ server, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromProgID(System_String? /* System.String */ progID) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromProgID(System_String? /* System.String */ progID, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromProgID(System_String? /* System.String */ progID, System_String? /* System.String */ server) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromProgID(System_String? /* System.String */ progID, System_String? /* System.String */ server, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invokeMember(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object? /* System.Object */ target, System_Object_Array? /* System.Object[] */ args) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invokeMember(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object? /* System.Object */ target, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invokeMember(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object? /* System.Object */ target, System_Object_Array? /* System.Object[] */ args, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_String_Array? /* System.String[] */ namedParameters) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInterface(System_String? /* System.String */ name) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInterface(System_String? /* System.String */ name, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInterfaces() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInterfaceMap(System_Type? /* System.Type */ interfaceType) throws -> System_Reflection_InterfaceMapping? /* System.Reflection.InterfaceMapping */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isInstanceOfType(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isEquivalentTo(System_Type? /* System.Type */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumUnderlyingType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumValues() throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumValuesAsUnderlyingType() throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeArrayType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeArrayType(Int32 /* System.Int32 */ rank) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeByRefType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeGenericType(System_Type_Array? /* System.Type[] */ typeArguments) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makePointerType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func makeGenericSignatureType(System_Type? /* System.Type */ genericTypeDefinition, System_Type_Array? /* System.Type[] */ typeArguments) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func makeGenericMethodParameter(Int32 /* System.Int32 */ position) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Type? /* System.Type */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reflectionOnlyGetType(System_String? /* System.String */ typeName, Bool /* System.Boolean */ throwIfNotFound, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isEnumDefined(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumName(System_Object? /* System.Object */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func findInterfaces(System_Reflection_TypeFilter? /* System.Reflection.TypeFilter */ filter, System_Object? /* System.Object */ filterCriteria) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func findMembers(System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ memberType, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_MemberFilter? /* System.Reflection.MemberFilter */ filter, System_Object? /* System.Object */ filterCriteria) throws -> System_Reflection_MemberInfo_Array? /* System.Reflection.MemberInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isSubclassOf(System_Type? /* System.Type */ c) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isAssignableFrom(System_Type? /* System.Type */ c) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsInterface() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNamespace() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAssemblyQualifiedName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getModule() throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNested() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaringType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaringMethod() throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReflectedType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUnderlyingSystemType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsTypeDefinition() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsArray() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsByRef() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPointer() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsConstructedGenericType() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericParameter() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericTypeParameter() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericMethodParameter() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericType() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericTypeDefinition() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSZArray() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsVariableBoundArray() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsByRefLike() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFunctionPointer() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsUnmanagedFunctionPointer() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHasElementType() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGenericTypeArguments() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGenericParameterPosition() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGenericParameterAttributes() throws -> System_Reflection_GenericParameterAttributes /* System.Reflection.GenericParameterAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_Reflection_TypeAttributes /* System.Reflection.TypeAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAbstract() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsImport() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSealed() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSpecialName() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsClass() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedFamANDAssem() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedFamily() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedFamORAssem() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedPrivate() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNestedPublic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNotPublic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPublic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAutoLayout() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsExplicitLayout() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsLayoutSequential() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAnsiClass() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAutoClass() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsUnicodeClass() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCOMObject() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsContextful() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsEnum() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsMarshalByRef() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPrimitive() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsValueType() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSignatureType() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecuritySafeCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityTransparent() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getStructLayoutAttribute() throws -> System_Runtime_InteropServices_StructLayoutAttribute? /* System.Runtime.InteropServices.StructLayoutAttribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTypeInitializer() throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTypeHandle() throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGUID() throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBaseType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDefaultBinder() throws -> System_Reflection_Binder? /* System.Reflection.Binder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSerializable() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getContainsGenericParameters() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsVisible() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDelimiter() -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getEmptyTypes() -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMissing() -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFilterAttribute() -> System_Reflection_MemberFilter? /* System.Reflection.MemberFilter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFilterName() -> System_Reflection_MemberFilter? /* System.Reflection.MemberFilter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFilterNameIgnoreCase() -> System_Reflection_MemberFilter? /* System.Reflection.MemberFilter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Type_TypeOf())
		
	
	}
	
	deinit {
		System_Type_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_MemberInfo /* System.Reflection.MemberInfo */ {
	func hasSameMetadataDefinitionAs(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefined(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributesData() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaringType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReflectedType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getModule() throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCustomAttributes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCollectible() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMetadataToken() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_MemberInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_MemberInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Enum /* System.Enum */ {
	static func getName(System_Type? /* System.Type */ TEnum, System_Object? /* System.Object */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getName(System_Type? /* System.Type */ enumType, System_Object? /* System.Object */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNames(System_Type? /* System.Type */ TEnum) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNames(System_Type? /* System.Type */ enumType) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getUnderlyingType(System_Type? /* System.Type */ enumType) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getValues(System_Type? /* System.Type */ TEnum) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getValues(System_Type? /* System.Type */ enumType) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getValuesAsUnderlyingType(System_Type? /* System.Type */ TEnum) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getValuesAsUnderlyingType(System_Type? /* System.Type */ enumType) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func hasFlag(System_Enum? /* System.Enum */ flag) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Type? /* System.Type */ TEnum, System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Type? /* System.Type */ enumType, System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_Type? /* System.Type */ enumType, System_String? /* System.String */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_Type? /* System.Type */ enumType, System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_Type? /* System.Type */ TEnum, System_String? /* System.String */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_Type? /* System.Type */ TEnum, System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_Type? /* System.Type */ enumType, System_String? /* System.String */ value, inout System_Object? /* System.Object */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_Type? /* System.Type */ enumType, System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase, inout System_Object? /* System.Object */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_Type? /* System.Type */ TEnum, System_String? /* System.String */ value, inout System_Object? /* System.Object */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_Type? /* System.Type */ TEnum, System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase, inout System_Object? /* System.Object */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ target) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_Type? /* System.Type */ enumType, System_Object? /* System.Object */ value, System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypeCode() throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, System_Object? /* System.Object */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, Int8 /* System.SByte */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, Int16 /* System.Int16 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, Int32 /* System.Int32 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, UInt8 /* System.Byte */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, UInt16 /* System.UInt16 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, UInt32 /* System.UInt32 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, Int64 /* System.Int64 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toObject(System_Type? /* System.Type */ enumType, UInt64 /* System.UInt64 */ value) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Enum_TypeOf())
		
	
	}
	
	deinit {
		System_Enum_Destroy(self._handle)
		
	
	}
	
	

}


public class System_ValueType /* System.ValueType */ {
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_ValueType_TypeOf())
		
	
	}
	
	deinit {
		System_ValueType_Destroy(self._handle)
		
	
	}
	
	

}




public class System_String /* System.String */ {
	let _handle: System_String_t

	required init(handle: System_String_t) {
		self._handle = handle
	}

	convenience init?(handle: System_String_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func intern(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isInterned(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, System_String? /* System.String */ strB) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, System_String? /* System.String */ strB, Bool /* System.Boolean */ ignoreCase) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, System_String? /* System.String */ strB, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, System_String? /* System.String */ strB, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, System_String? /* System.String */ strB, Bool /* System.Boolean */ ignoreCase, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length, Bool /* System.Boolean */ ignoreCase) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length, Bool /* System.Boolean */ ignoreCase, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compareOrdinal(System_String? /* System.String */ strA, System_String? /* System.String */ strB) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compareOrdinal(System_String? /* System.String */ strA, Int32 /* System.Int32 */ indexA, System_String? /* System.String */ strB, Int32 /* System.Int32 */ indexB, Int32 /* System.Int32 */ length) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_String? /* System.String */ strB) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endsWith(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endsWith(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endsWith(System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endsWith(UInt8 /* System.Char */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_String? /* System.String */ a, System_String? /* System.String */ b) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_String? /* System.String */ a, System_String? /* System.String */ b, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startsWith(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startsWith(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startsWith(System_String? /* System.String */ value, Bool /* System.Boolean */ ignoreCase, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startsWith(UInt8 /* System.Char */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(Int32 /* System.Int32 */ sourceIndex, System_Char_Array? /* System.Char[] */ destination, Int32 /* System.Int32 */ destinationIndex, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toCharArray() throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toCharArray(Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isNullOrEmpty(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isNullOrWhiteSpace(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_CharEnumerator? /* System.CharEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateRunes() throws -> System_Text_StringRuneEnumerator? /* System.Text.StringRuneEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypeCode() throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isNormalized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isNormalized(System_Text_NormalizationForm /* System.Text.NormalizationForm */ normalizationForm) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func normalize() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func normalize(System_Text_NormalizationForm /* System.Text.NormalizationForm */ normalizationForm) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_Object? /* System.Object */ arg0) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_Object_Array? /* System.Object[] */ args) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ values) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_String? /* System.String */ str0, System_String? /* System.String */ str1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_String? /* System.String */ str0, System_String? /* System.String */ str1, System_String? /* System.String */ str2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_String? /* System.String */ str0, System_String? /* System.String */ str1, System_String? /* System.String */ str2, System_String? /* System.String */ str3) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func concat(System_String_Array? /* System.String[] */ values) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_Type? /* System.Type */ TArg0, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_Type? /* System.Type */ TArg0, System_Type? /* System.Type */ TArg1, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_Type? /* System.Type */ TArg0, System_Type? /* System.Type */ TArg1, System_Type? /* System.Type */ TArg2, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func format(System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ startIndex, System_String? /* System.String */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(UInt8 /* System.Char */ separator, System_String_Array? /* System.String[] */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ separator, System_String_Array? /* System.String[] */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(UInt8 /* System.Char */ separator, System_String_Array? /* System.String[] */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ separator, System_String_Array? /* System.String[] */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ separator, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ values) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(UInt8 /* System.Char */ separator, System_Object_Array? /* System.Object[] */ values) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ separator, System_Object_Array? /* System.Object[] */ values) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func padLeft(Int32 /* System.Int32 */ totalWidth) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func padLeft(Int32 /* System.Int32 */ totalWidth, UInt8 /* System.Char */ paddingChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func padRight(Int32 /* System.Int32 */ totalWidth) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func padRight(Int32 /* System.Int32 */ totalWidth, UInt8 /* System.Char */ paddingChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(Int32 /* System.Int32 */ startIndex) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ oldValue, System_String? /* System.String */ newValue, Bool /* System.Boolean */ ignoreCase, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ oldValue, System_String? /* System.String */ newValue, System_StringComparison /* System.StringComparison */ comparisonType) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(UInt8 /* System.Char */ oldChar, UInt8 /* System.Char */ newChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ oldValue, System_String? /* System.String */ newValue) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replaceLineEndings() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replaceLineEndings(System_String? /* System.String */ replacementText) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(UInt8 /* System.Char */ separator, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(UInt8 /* System.Char */ separator, Int32 /* System.Int32 */ count, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_Char_Array? /* System.Char[] */ separator) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_Char_Array? /* System.Char[] */ separator, Int32 /* System.Int32 */ count) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_Char_Array? /* System.Char[] */ separator, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_Char_Array? /* System.Char[] */ separator, Int32 /* System.Int32 */ count, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_String? /* System.String */ separator, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_String? /* System.String */ separator, Int32 /* System.Int32 */ count, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_String_Array? /* System.String[] */ separator, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func split(System_String_Array? /* System.String[] */ separator, Int32 /* System.Int32 */ count, System_StringSplitOptions /* System.StringSplitOptions */ options) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func substring(Int32 /* System.Int32 */ startIndex) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func substring(Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLower() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLower(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLowerInvariant() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUpper() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUpper(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUpperInvariant() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trim() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trim(UInt8 /* System.Char */ trimChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trim(System_Char_Array? /* System.Char[] */ trimChars) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimStart() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimStart(UInt8 /* System.Char */ trimChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimStart(System_Char_Array? /* System.Char[] */ trimChars) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimEnd() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimEnd(UInt8 /* System.Char */ trimChar) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimEnd(System_Char_Array? /* System.Char[] */ trimChars) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(UInt8 /* System.Char */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(UInt8 /* System.Char */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(UInt8 /* System.Char */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(UInt8 /* System.Char */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOfAny(System_Char_Array? /* System.Char[] */ anyOf) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOfAny(System_Char_Array? /* System.Char[] */ anyOf, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOfAny(System_Char_Array? /* System.Char[] */ anyOf, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(UInt8 /* System.Char */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOfAny(System_Char_Array? /* System.Char[] */ anyOf) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOfAny(System_Char_Array? /* System.Char[] */ anyOf, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOfAny(System_Char_Array? /* System.Char[] */ anyOf, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_StringComparison /* System.StringComparison */ comparisonType) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Char_Array? /* System.Char[] */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Char_Array? /* System.Char[] */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt8 /* System.Char */ c, Int32 /* System.Int32 */ count) throws {
		// TODO: Constructor
		
	
	}
	
	func getLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getEmpty() -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_String_TypeOf())
		
	
	}
	
	deinit {
		System_String_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_CultureInfo /* System.Globalization.CultureInfo */ {
	let _handle: System_Globalization_CultureInfo_t

	required init(handle: System_Globalization_CultureInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_CultureInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func createSpecificCulture(System_String? /* System.String */ name) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultures(System_Globalization_CultureTypes /* System.Globalization.CultureTypes */ types) throws -> System_Globalization_CultureInfo_Array? /* System.Globalization.CultureInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFormat(System_Type? /* System.Type */ formatType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clearCachedData() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConsoleFallbackUICulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ ci) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultureInfo(Int32 /* System.Int32 */ culture) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultureInfo(System_String? /* System.String */ name) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultureInfo(System_String? /* System.String */ name, System_String? /* System.String */ altName) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultureInfo(System_String? /* System.String */ name, Bool /* System.Boolean */ predefinedOnly) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCultureInfoByIetfLanguageTag(System_String? /* System.String */ name) throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ name) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ name, Bool /* System.Boolean */ useUserOverride) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ culture) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ culture, Bool /* System.Boolean */ useUserOverride) throws {
		// TODO: Constructor
		
	
	}
	
	static func getCurrentCulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCurrentCulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentUICulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCurrentUICulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getInstalledUICulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDefaultThreadCurrentCulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setDefaultThreadCurrentCulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDefaultThreadCurrentUICulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setDefaultThreadCurrentUICulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getInvariantCulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getParent() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLCID() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getKeyboardLayoutId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIetfLanguageTag() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDisplayName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNativeName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEnglishName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTwoLetterISOLanguageName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getThreeLetterISOLanguageName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getThreeLetterWindowsLanguageName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCompareInfo() throws -> System_Globalization_CompareInfo? /* System.Globalization.CompareInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTextInfo() throws -> System_Globalization_TextInfo? /* System.Globalization.TextInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNeutralCulture() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCultureTypes() throws -> System_Globalization_CultureTypes /* System.Globalization.CultureTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberFormat() throws -> System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberFormat(System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDateTimeFormat() throws -> System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDateTimeFormat(System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCalendar() throws -> System_Globalization_Calendar? /* System.Globalization.Calendar */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getOptionalCalendars() throws -> System_Globalization_Calendar_Array? /* System.Globalization.Calendar[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUseUserOverride() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_CultureInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_CultureInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class Void /* System.Void */ {
	let _handle: void

	required init(handle: void) {
		self._handle = handle
	}

	convenience init?(handle: void?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Globalization_CultureInfo_Array /* System.Globalization.CultureInfo[] */ {
	let _handle: System_Globalization_CultureInfo_Array_t

	required init(handle: System_Globalization_CultureInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_CultureInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Array /* System.Array */ {
	static func constrainedCopy(System_Array? /* System.Array */ sourceArray, Int32 /* System.Int32 */ sourceIndex, System_Array? /* System.Array */ destinationArray, Int32 /* System.Int32 */ destinationIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clear(System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clear(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLength(Int32 /* System.Int32 */ dimension) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getUpperBound(Int32 /* System.Int32 */ dimension) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLowerBound(Int32 /* System.Int32 */ dimension) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func initialize() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, Int32 /* System.Int32 */ length) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, Int32 /* System.Int32 */ length1, Int32 /* System.Int32 */ length2) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, Int32 /* System.Int32 */ length1, Int32 /* System.Int32 */ length2, Int32 /* System.Int32 */ length3) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, System_Int32_Array? /* System.Int32[] */ lengths) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, System_Int32_Array? /* System.Int32[] */ lengths, System_Int32_Array? /* System.Int32[] */ lowerBounds) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createInstance(System_Type? /* System.Type */ elementType, System_Int64_Array? /* System.Int64[] */ lengths) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Array? /* System.Array */ sourceArray, System_Array? /* System.Array */ destinationArray, Int64 /* System.Int64 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Array? /* System.Array */ sourceArray, Int64 /* System.Int64 */ sourceIndex, System_Array? /* System.Array */ destinationArray, Int64 /* System.Int64 */ destinationIndex, Int64 /* System.Int64 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Array? /* System.Array */ sourceArray, System_Array? /* System.Array */ destinationArray, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Array? /* System.Array */ sourceArray, Int32 /* System.Int32 */ sourceIndex, System_Array? /* System.Array */ destinationArray, Int32 /* System.Int32 */ destinationIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Int32_Array? /* System.Int32[] */ indices) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int32 /* System.Int32 */ index) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int32 /* System.Int32 */ index1, Int32 /* System.Int32 */ index2) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int32 /* System.Int32 */ index1, Int32 /* System.Int32 */ index2, Int32 /* System.Int32 */ index3) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ index1, Int32 /* System.Int32 */ index2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ index1, Int32 /* System.Int32 */ index2, Int32 /* System.Int32 */ index3) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, System_Int32_Array? /* System.Int32[] */ indices) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int64 /* System.Int64 */ index) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int64 /* System.Int64 */ index1, Int64 /* System.Int64 */ index2) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(Int64 /* System.Int64 */ index1, Int64 /* System.Int64 */ index2, Int64 /* System.Int64 */ index3) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Int64_Array? /* System.Int64[] */ indices) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int64 /* System.Int64 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int64 /* System.Int64 */ index1, Int64 /* System.Int64 */ index2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, Int64 /* System.Int64 */ index1, Int64 /* System.Int64 */ index2, Int64 /* System.Int64 */ index3) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value, System_Int64_Array? /* System.Int64[] */ indices) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLongLength(Int32 /* System.Int32 */ dimension) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length, System_Object? /* System.Object */ value, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func binarySearch(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array, Int64 /* System.Int64 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func empty(System_Type? /* System.Type */ T) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fill(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fill(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func indexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func lastIndexOf(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reverse(System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reverse(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reverse(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reverse(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ array, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sort(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Array? /* System.Array */ keys, System_Array? /* System.Array */ items, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Collections_IEnumerator? /* System.Collections.IEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLongLength() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRank() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSyncRoot() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFixedSize() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSynchronized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Array_TypeOf())
		
	
	}
	
	deinit {
		System_Array_Destroy(self._handle)
		
	
	}
	
	

}



public class System_IFormatProvider /* System.IFormatProvider */ {
	func getFormat(System_Type? /* System.Type */ formatType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IFormatProvider_TypeOf())
		
	
	}
	
	deinit {
		System_IFormatProvider_Destroy(self._handle)
		
	
	}
	
	

}






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.


public class System_Int32_Array /* System.Int32[] */ {
	let _handle: System_Int32_Array_t

	required init(handle: System_Int32_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Int32_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Int64_Array /* System.Int64[] */ {
	let _handle: System_Int64_Array_t

	required init(handle: System_Int64_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Int64_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Collections_IComparer /* System.Collections.IComparer */ {
	func compare(System_Object? /* System.Object */ x, System_Object? /* System.Object */ y) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IComparer_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IComparer_Destroy(self._handle)
		
	
	}
	
	

}


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

public class System_Collections_IEnumerator /* System.Collections.IEnumerator */ {
	func moveNext() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrent() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IEnumerator_Destroy(self._handle)
		
	
	}
	
	

}



public class System_Globalization_CompareInfo /* System.Globalization.CompareInfo */ {
	let _handle: System_Globalization_CompareInfo_t

	required init(handle: System_Globalization_CompareInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_CompareInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func getCompareInfo(Int32 /* System.Int32 */ culture, System_Reflection_Assembly? /* System.Reflection.Assembly */ assembly) throws -> System_Globalization_CompareInfo? /* System.Globalization.CompareInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCompareInfo(System_String? /* System.String */ name, System_Reflection_Assembly? /* System.Reflection.Assembly */ assembly) throws -> System_Globalization_CompareInfo? /* System.Globalization.CompareInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCompareInfo(Int32 /* System.Int32 */ culture) throws -> System_Globalization_CompareInfo? /* System.Globalization.CompareInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCompareInfo(System_String? /* System.String */ name) throws -> System_Globalization_CompareInfo? /* System.Globalization.CompareInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isSortable(UInt8 /* System.Char */ ch) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isSortable(System_String? /* System.String */ text) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isSortable(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, System_String? /* System.String */ string2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, System_String? /* System.String */ string2, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, Int32 /* System.Int32 */ offset1, Int32 /* System.Int32 */ length1, System_String? /* System.String */ string2, Int32 /* System.Int32 */ offset2, Int32 /* System.Int32 */ length2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, Int32 /* System.Int32 */ offset1, System_String? /* System.String */ string2, Int32 /* System.Int32 */ offset2, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, Int32 /* System.Int32 */ offset1, System_String? /* System.String */ string2, Int32 /* System.Int32 */ offset2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compare(System_String? /* System.String */ string1, Int32 /* System.Int32 */ offset1, Int32 /* System.Int32 */ length1, System_String? /* System.String */ string2, Int32 /* System.Int32 */ offset2, Int32 /* System.Int32 */ length2, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isPrefix(System_String? /* System.String */ source, System_String? /* System.String */ prefix, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isPrefix(System_String? /* System.String */ source, System_String? /* System.String */ prefix) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isSuffix(System_String? /* System.String */ source, System_String? /* System.String */ suffix, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isSuffix(System_String? /* System.String */ source, System_String? /* System.String */ suffix) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, UInt8 /* System.Char */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_String? /* System.String */ source, System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSortKey(System_String? /* System.String */ source, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> System_Globalization_SortKey? /* System.Globalization.SortKey */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSortKey(System_String? /* System.String */ source) throws -> System_Globalization_SortKey? /* System.Globalization.SortKey */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_String? /* System.String */ source, System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getVersion() throws -> System_Globalization_SortVersion? /* System.Globalization.SortVersion */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLCID() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_CompareInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_CompareInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_Assembly /* System.Reflection.Assembly */ {
	static func load(System_String? /* System.String */ assemblyString) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func loadWithPartialName(System_String? /* System.String */ partialName) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func load(System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ assemblyRef) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExecutingAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCallingAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getExportedTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getForwardedTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getManifestResourceInfo(System_String? /* System.String */ resourceName) throws -> System_Reflection_ManifestResourceInfo? /* System.Reflection.ManifestResourceInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getManifestResourceNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getManifestResourceStream(System_String? /* System.String */ name) throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getManifestResourceStream(System_Type? /* System.Type */ type, System_String? /* System.String */ name) throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getName() throws -> System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getName(Bool /* System.Boolean */ copiedName) throws -> System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ name) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ name, Bool /* System.Boolean */ throwOnError) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ name, Bool /* System.Boolean */ throwOnError, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefined(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributesData() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ typeName) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModule(System_String? /* System.String */ name) throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModules() throws -> System_Reflection_Module_Array? /* System.Reflection.Module[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModules(Bool /* System.Boolean */ getResourceModules) throws -> System_Reflection_Module_Array? /* System.Reflection.Module[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLoadedModules() throws -> System_Reflection_Module_Array? /* System.Reflection.Module[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLoadedModules(Bool /* System.Boolean */ getResourceModules) throws -> System_Reflection_Module_Array? /* System.Reflection.Module[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getReferencedAssemblies() throws -> System_Reflection_AssemblyName_Array? /* System.Reflection.AssemblyName[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSatelliteAssembly(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSatelliteAssembly(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Version? /* System.Version */ version) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFile(System_String? /* System.String */ name) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles() throws -> System_IO_FileStream_Array? /* System.IO.FileStream[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles(Bool /* System.Boolean */ getResourceModules) throws -> System_IO_FileStream_Array? /* System.IO.FileStream[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createQualifiedName(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAssembly(System_Type? /* System.Type */ type) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEntryAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func load(System_Byte_Array? /* System.Byte[] */ rawAssembly) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func load(System_Byte_Array? /* System.Byte[] */ rawAssembly, System_Byte_Array? /* System.Byte[] */ rawSymbolStore) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func loadFile(System_String? /* System.String */ path) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func loadFrom(System_String? /* System.String */ assemblyFile) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func loadFrom(System_String? /* System.String */ assemblyFile, System_Byte_Array? /* System.Byte[] */ hashValue, System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */ hashAlgorithm) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func unsafeLoadFrom(System_String? /* System.String */ assemblyFile) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func loadModule(System_String? /* System.String */ moduleName, System_Byte_Array? /* System.Byte[] */ rawModule) throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func loadModule(System_String? /* System.String */ moduleName, System_Byte_Array? /* System.Byte[] */ rawModule, System_Byte_Array? /* System.Byte[] */ rawSymbolStore) throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reflectionOnlyLoad(System_Byte_Array? /* System.Byte[] */ rawAssembly) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reflectionOnlyLoad(System_String? /* System.String */ assemblyString) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reflectionOnlyLoadFrom(System_String? /* System.String */ assemblyFile) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDefinedTypes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExportedTypes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Type> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCodeBase() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEntryPoint() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getImageRuntimeVersion() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsDynamic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLocation() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReflectionOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCollectible() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFullyTrusted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCustomAttributes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEscapedCodeBase() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getManifestModule() throws -> System_Reflection_Module? /* System.Reflection.Module */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getModules() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.Module> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGlobalAssemblyCache() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHostContext() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSecurityRuleSet() throws -> System_Security_SecurityRuleSet /* System.Security.SecurityRuleSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addModuleResolve(System_Reflection_ModuleResolveEventHandler? /* System.Reflection.ModuleResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeModuleResolve(System_Reflection_ModuleResolveEventHandler? /* System.Reflection.ModuleResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_Assembly_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_Assembly_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_AssemblyName /* System.Reflection.AssemblyName */ {
	let _handle: System_Reflection_AssemblyName_t

	required init(handle: System_Reflection_AssemblyName_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_AssemblyName_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAssemblyName(System_String? /* System.String */ assemblyFile) throws -> System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getPublicKey() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPublicKey(System_Byte_Array? /* System.Byte[] */ publicKey) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getPublicKeyToken() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPublicKeyToken(System_Byte_Array? /* System.Byte[] */ publicKeyToken) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func onDeserialization(System_Object? /* System.Object */ sender) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func referenceMatchesDefinition(System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ reference, System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ definition) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ assemblyName) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getVersion() throws -> System_Version? /* System.Version */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setVersion(System_Version? /* System.Version */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCultureInfo() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCultureInfo(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCultureName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCultureName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCodeBase() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCodeBase(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEscapedCodeBase() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getProcessorArchitecture() throws -> System_Reflection_ProcessorArchitecture /* System.Reflection.ProcessorArchitecture */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setProcessorArchitecture(System_Reflection_ProcessorArchitecture /* System.Reflection.ProcessorArchitecture */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getContentType() throws -> System_Reflection_AssemblyContentType /* System.Reflection.AssemblyContentType */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setContentType(System_Reflection_AssemblyContentType /* System.Reflection.AssemblyContentType */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFlags() throws -> System_Reflection_AssemblyNameFlags /* System.Reflection.AssemblyNameFlags */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFlags(System_Reflection_AssemblyNameFlags /* System.Reflection.AssemblyNameFlags */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHashAlgorithm() throws -> System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setHashAlgorithm(System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getVersionCompatibility() throws -> System_Configuration_Assemblies_AssemblyVersionCompatibility /* System.Configuration.Assemblies.AssemblyVersionCompatibility */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setVersionCompatibility(System_Configuration_Assemblies_AssemblyVersionCompatibility /* System.Configuration.Assemblies.AssemblyVersionCompatibility */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getKeyPair() throws -> System_Reflection_StrongNameKeyPair? /* System.Reflection.StrongNameKeyPair */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setKeyPair(System_Reflection_StrongNameKeyPair? /* System.Reflection.StrongNameKeyPair */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_AssemblyName_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_AssemblyName_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Version /* System.Version */ {
	let _handle: System_Version_t

	required init(handle: System_Version_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Version_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ version) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Version? /* System.Version */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Version? /* System.Version */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(Int32 /* System.Int32 */ fieldCount) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ input) throws -> System_Version? /* System.Version */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ input, inout System_Version? /* System.Version */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ major, Int32 /* System.Int32 */ minor, Int32 /* System.Int32 */ build, Int32 /* System.Int32 */ revision) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ major, Int32 /* System.Int32 */ minor, Int32 /* System.Int32 */ build) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ major, Int32 /* System.Int32 */ minor) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ version) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getMajor() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMinor() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBuild() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRevision() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMajorRevision() throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMinorRevision() throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Version_TypeOf())
		
	
	}
	
	deinit {
		System_Version_Destroy(self._handle)
		
	
	}
	
	

}






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.

public class System_Byte_Array /* System.Byte[] */ {
	let _handle: System_Byte_Array_t

	required init(handle: System_Byte_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Byte_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
public class System_Reflection_StrongNameKeyPair /* System.Reflection.StrongNameKeyPair */ {
	let _handle: System_Reflection_StrongNameKeyPair_t

	required init(handle: System_Reflection_StrongNameKeyPair_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_StrongNameKeyPair_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_IO_FileStream? /* System.IO.FileStream */ keyPairFile) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Byte_Array? /* System.Byte[] */ keyPairArray) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ keyPairContainer) throws {
		// TODO: Constructor
		
	
	}
	
	func getPublicKey() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_StrongNameKeyPair_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_StrongNameKeyPair_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_FileStream /* System.IO.FileStream */ {
	let _handle: System_IO_FileStream_t

	required init(handle: System_IO_FileStream_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileStream_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func lock(Int64 /* System.Int64 */ position, Int64 /* System.Int64 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unlock(Int64 /* System.Int64 */ position, Int64 /* System.Int64 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flushAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Memory_A1? /* System.Memory<System.Byte> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Byte> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flush() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flush(Bool /* System.Boolean */ flushToDisk) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLength(Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readByte() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeByte(UInt8 /* System.Byte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_IO_Stream? /* System.IO.Stream */ destination, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyToAsync(System_IO_Stream? /* System.IO.Stream */ destination, Int32 /* System.Int32 */ bufferSize, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func beginRead(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_AsyncCallback? /* System.AsyncCallback */ callback, System_Object? /* System.Object */ state) throws -> System_IAsyncResult? /* System.IAsyncResult */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endRead(System_IAsyncResult? /* System.IAsyncResult */ asyncResult) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func beginWrite(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_AsyncCallback? /* System.AsyncCallback */ callback, System_Object? /* System.Object */ state) throws -> System_IAsyncResult? /* System.IAsyncResult */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endWrite(System_IAsyncResult? /* System.IAsyncResult */ asyncResult) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func seek(Int64 /* System.Int64 */ offset, System_IO_SeekOrigin /* System.IO.SeekOrigin */ origin) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int /* System.IntPtr */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int /* System.IntPtr */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access, Bool /* System.Boolean */ ownsHandle) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int /* System.IntPtr */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access, Bool /* System.Boolean */ ownsHandle, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int /* System.IntPtr */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access, Bool /* System.Boolean */ ownsHandle, Int32 /* System.Int32 */ bufferSize, Bool /* System.Boolean */ isAsync) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle, System_IO_FileAccess /* System.IO.FileAccess */ access, Int32 /* System.Int32 */ bufferSize, Bool /* System.Boolean */ isAsync) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share, Int32 /* System.Int32 */ bufferSize, Bool /* System.Boolean */ useAsync) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share, Int32 /* System.Int32 */ bufferSize, System_IO_FileOptions /* System.IO.FileOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	func getHandle() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanRead() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanWrite() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSafeFileHandle() throws -> Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAsync() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLength() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPosition() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPosition(Int64 /* System.Int64 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanSeek() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_FileStream_TypeOf())
		
	
	}
	
	deinit {
		System_IO_FileStream_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_Stream /* System.IO.Stream */ {
	func copyTo(System_IO_Stream? /* System.IO.Stream */ destination) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_IO_Stream? /* System.IO.Stream */ destination, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyToAsync(System_IO_Stream? /* System.IO.Stream */ destination) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyToAsync(System_IO_Stream? /* System.IO.Stream */ destination, Int32 /* System.Int32 */ bufferSize) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyToAsync(System_IO_Stream? /* System.IO.Stream */ destination, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyToAsync(System_IO_Stream? /* System.IO.Stream */ destination, Int32 /* System.Int32 */ bufferSize, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flush() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flushAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flushAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func beginRead(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_AsyncCallback? /* System.AsyncCallback */ callback, System_Object? /* System.Object */ state) throws -> System_IAsyncResult? /* System.IAsyncResult */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endRead(System_IAsyncResult? /* System.IAsyncResult */ asyncResult) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Memory_A1? /* System.Memory<System.Byte> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readExactlyAsync(System_Memory_A1? /* System.Memory<System.Byte> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readExactlyAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAtLeastAsync(System_Memory_A1? /* System.Memory<System.Byte> */ buffer, Int32 /* System.Int32 */ minimumBytes, Bool /* System.Boolean */ throwOnEndOfStream, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func beginWrite(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_AsyncCallback? /* System.AsyncCallback */ callback, System_Object? /* System.Object */ state) throws -> System_IAsyncResult? /* System.IAsyncResult */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func endWrite(System_IAsyncResult? /* System.IAsyncResult */ asyncResult) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Byte> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func seek(Int64 /* System.Int64 */ offset, System_IO_SeekOrigin /* System.IO.SeekOrigin */ origin) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLength(Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readByte() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readExactly(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Byte_Array? /* System.Byte[] */ buffer, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeByte(UInt8 /* System.Byte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_IO_Stream? /* System.IO.Stream */ stream) throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCanRead() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanWrite() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanSeek() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanTimeout() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLength() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPosition() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPosition(Int64 /* System.Int64 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReadTimeout() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setReadTimeout(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getWriteTimeout() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setWriteTimeout(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNull() -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_Stream_TypeOf())
		
	
	}
	
	deinit {
		System_IO_Stream_Destroy(self._handle)
		
	
	}
	
	

}


public class System_MarshalByRefObject /* System.MarshalByRefObject */ {
	func getLifetimeService() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func initializeLifetimeService() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_MarshalByRefObject_TypeOf())
		
	
	}
	
	deinit {
		System_MarshalByRefObject_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Tasks_Task /* System.Threading.Tasks.Task */ {
	let _handle: System_Threading_Tasks_Task_t

	required init(handle: System_Threading_Tasks_Task_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Tasks_Task_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func start() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func start(System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ scheduler) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func runSynchronously() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func runSynchronously(System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ scheduler) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAwaiter() throws -> System_Runtime_CompilerServices_TaskAwaiter? /* System.Runtime.CompilerServices.TaskAwaiter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func configureAwait(Bool /* System.Boolean */ continueOnCapturedContext) throws -> System_Runtime_CompilerServices_ConfiguredTaskAwaitable? /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func yield() throws -> System_Runtime_CompilerServices_YieldAwaitable? /* System.Runtime.CompilerServices.YieldAwaitable */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait(System_TimeSpan? /* System.TimeSpan */ timeout, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait(Int32 /* System.Int32 */ millisecondsTimeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func wait(Int32 /* System.Int32 */ millisecondsTimeout, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitAsync(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitAsync(System_TimeSpan? /* System.TimeSpan */ timeout, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, Int32 /* System.Int32 */ millisecondsTimeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, Int32 /* System.Int32 */ millisecondsTimeout, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, Int32 /* System.Int32 */ millisecondsTimeout) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks, Int32 /* System.Int32 */ millisecondsTimeout, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromException(System_Exception? /* System.Exception */ exception) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromCanceled(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func run(System_Action? /* System.Action */ action) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func run(System_Action? /* System.Action */ action, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delay(System_TimeSpan? /* System.TimeSpan */ delay) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delay(System_TimeSpan? /* System.TimeSpan */ delay, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delay(Int32 /* System.Int32 */ millisecondsDelay) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delay(Int32 /* System.Int32 */ millisecondsDelay, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func whenAll(System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task> */ tasks) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func whenAll(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func whenAny(System_Threading_Tasks_Task_Array? /* System.Threading.Tasks.Task[] */ tasks) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Threading.Tasks.Task> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func whenAny(System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ task1, System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ task2) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Threading.Tasks.Task> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func whenAny(System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task> */ tasks) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Threading.Tasks.Task> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Action? /* System.Action */ action) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Action? /* System.Action */ action, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Action? /* System.Action */ action, System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Action? /* System.Action */ action, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken, System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions) throws {
		// TODO: Constructor
		
	
	}
	
	func getId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentId() throws -> System_Nullable_A1? /* System.Nullable<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getException() throws -> System_AggregateException? /* System.AggregateException */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getStatus() throws -> System_Threading_Tasks_TaskStatus /* System.Threading.Tasks.TaskStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCanceled() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCompletedSuccessfully() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCreationOptions() throws -> System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAsyncState() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFactory() throws -> System_Threading_Tasks_TaskFactory? /* System.Threading.Tasks.TaskFactory */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCompletedTask() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFaulted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Tasks_Task_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Tasks_Task_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Tasks_TaskScheduler /* System.Threading.Tasks.TaskScheduler */ {
	static func fromCurrentSynchronizationContext() throws -> System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMaximumConcurrencyLevel() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDefault() throws -> System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrent() throws -> System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Tasks_TaskScheduler_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Tasks_TaskScheduler_Destroy(self._handle)
		
	
	}
	
	

}




public class System_AggregateException /* System.AggregateException */ {
	let _handle: System_AggregateException_t

	required init(handle: System_AggregateException_t) {
		self._handle = handle
	}

	convenience init?(handle: System_AggregateException_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBaseException() throws -> System_Exception? /* System.Exception */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flatten() throws -> System_AggregateException? /* System.AggregateException */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Exception? /* System.Exception */ innerException) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Exception> */ innerExceptions) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Exception_Array? /* System.Exception[] */ innerExceptions) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Exception> */ innerExceptions) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Exception_Array? /* System.Exception[] */ innerExceptions) throws {
		// TODO: Constructor
		
	
	}
	
	func getInnerExceptions() throws -> System_Collections_ObjectModel_ReadOnlyCollection_A1? /* System.Collections.ObjectModel.ReadOnlyCollection<System.Exception> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMessage() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_AggregateException_TypeOf())
		
	
	}
	
	deinit {
		System_AggregateException_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Exception /* System.Exception */ {
	let _handle: System_Exception_t

	required init(handle: System_Exception_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Exception_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getBaseException() throws -> System_Exception? /* System.Exception */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Exception? /* System.Exception */ innerException) throws {
		// TODO: Constructor
		
	
	}
	
	func getTargetSite() throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMessage() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getData() throws -> System_Collections_IDictionary? /* System.Collections.IDictionary */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getInnerException() throws -> System_Exception? /* System.Exception */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHelpLink() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setHelpLink(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSource() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setSource(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHResult() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setHResult(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getStackTrace() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Exception_TypeOf())
		
	
	}
	
	deinit {
		System_Exception_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_MethodBase /* System.Reflection.MethodBase */ {
	static func getMethodFromHandle(System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ handle) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getMethodFromHandle(System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ handle, System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ declaringType) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCurrentMethod() throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getParameters() throws -> System_Reflection_ParameterInfo_Array? /* System.Reflection.ParameterInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethodImplementationFlags() throws -> System_Reflection_MethodImplAttributes /* System.Reflection.MethodImplAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethodBody() throws -> System_Reflection_MethodBody? /* System.Reflection.MethodBody */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericArguments() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invoke(System_Object? /* System.Object */ obj, System_Object_Array? /* System.Object[] */ parameters) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invoke(System_Object? /* System.Object */ obj, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ parameters, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAttributes() throws -> System_Reflection_MethodAttributes /* System.Reflection.MethodAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMethodImplementationFlags() throws -> System_Reflection_MethodImplAttributes /* System.Reflection.MethodImplAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCallingConvention() throws -> System_Reflection_CallingConventions /* System.Reflection.CallingConventions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAbstract() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsConstructor() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFinal() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsHideBySig() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSpecialName() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsStatic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsVirtual() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamily() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamilyAndAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamilyOrAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPrivate() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPublic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsConstructedGenericMethod() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericMethod() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsGenericMethodDefinition() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getContainsGenericParameters() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMethodHandle() throws -> System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecuritySafeCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityTransparent() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_MethodBase_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_MethodBase_Destroy(self._handle)
		
	
	}
	
	

}


public class System_RuntimeMethodHandle /* System.RuntimeMethodHandle */ {
	let _handle: System_RuntimeMethodHandle_t

	required init(handle: System_RuntimeMethodHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_RuntimeMethodHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromIntPtr(Int /* System.IntPtr */ value) throws -> System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toIntPtr(System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ value) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ handle) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFunctionPointer() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_RuntimeMethodHandle_TypeOf())
		
	
	}
	
	deinit {
		System_RuntimeMethodHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Serialization_SerializationInfo /* System.Runtime.Serialization.SerializationInfo */ {
	let _handle: System_Runtime_Serialization_SerializationInfo_t

	required init(handle: System_Runtime_Serialization_SerializationInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_Serialization_SerializationInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func setType(System_Type? /* System.Type */ type) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Runtime_Serialization_SerializationInfoEnumerator? /* System.Runtime.Serialization.SerializationInfoEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, System_Object? /* System.Object */ value, System_Type? /* System.Type */ type) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Bool /* System.Boolean */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, UInt8 /* System.Char */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Int8 /* System.SByte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, UInt8 /* System.Byte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Int16 /* System.Int16 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, UInt16 /* System.UInt16 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Int32 /* System.Int32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, UInt32 /* System.UInt32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, UInt64 /* System.UInt64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, Float /* System.Single */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, double /* System.Double */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, System_Decimal? /* System.Decimal */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addValue(System_String? /* System.String */ name, System_DateTime? /* System.DateTime */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_String? /* System.String */ name, System_Type? /* System.Type */ type) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBoolean(System_String? /* System.String */ name) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChar(System_String? /* System.String */ name) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSByte(System_String? /* System.String */ name) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByte(System_String? /* System.String */ name) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInt16(System_String? /* System.String */ name) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getUInt16(System_String? /* System.String */ name) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInt32(System_String? /* System.String */ name) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getUInt32(System_String? /* System.String */ name) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getInt64(System_String? /* System.String */ name) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getUInt64(System_String? /* System.String */ name) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSingle(System_String? /* System.String */ name) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDouble(System_String? /* System.String */ name) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDecimal(System_String? /* System.String */ name) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDateTime(System_String? /* System.String */ name) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getString(System_String? /* System.String */ name) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ type, System_Runtime_Serialization_IFormatterConverter? /* System.Runtime.Serialization.IFormatterConverter */ converter) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ type, System_Runtime_Serialization_IFormatterConverter? /* System.Runtime.Serialization.IFormatterConverter */ converter, Bool /* System.Boolean */ requireSameTokenInPartialTrust) throws {
		// TODO: Constructor
		
	
	}
	
	func getFullTypeName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFullTypeName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAssemblyName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAssemblyName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFullTypeNameSetExplicit() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAssemblyNameSetExplicit() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMemberCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getObjectType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Serialization_SerializationInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Serialization_SerializationInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Serialization_SerializationInfoEnumerator /* System.Runtime.Serialization.SerializationInfoEnumerator */ {
	let _handle: System_Runtime_Serialization_SerializationInfoEnumerator_t

	required init(handle: System_Runtime_Serialization_SerializationInfoEnumerator_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_Serialization_SerializationInfoEnumerator_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func moveNext() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrent() throws -> System_Runtime_Serialization_SerializationEntry? /* System.Runtime.Serialization.SerializationEntry */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getObjectType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Serialization_SerializationInfoEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Serialization_SerializationInfoEnumerator_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Serialization_SerializationEntry /* System.Runtime.Serialization.SerializationEntry */ {
	let _handle: System_Runtime_Serialization_SerializationEntry_t

	required init(handle: System_Runtime_Serialization_SerializationEntry_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_Serialization_SerializationEntry_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getObjectType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Serialization_SerializationEntry_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Serialization_SerializationEntry_Destroy(self._handle)
		
	
	}
	
	

}





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


public class System_Decimal /* System.Decimal */ {
	let _handle: System_Decimal_t

	required init(handle: System_Decimal_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Decimal_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func fromOACurrency(Int64 /* System.Int64 */ cy) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toOACurrency(System_Decimal? /* System.Decimal */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func add(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ceiling(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Decimal? /* System.Decimal */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divide(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func floor(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_Globalization_NumberStyles /* System.Globalization.NumberStyles */ style) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_Globalization_NumberStyles /* System.Globalization.NumberStyles */ style, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, inout System_Decimal? /* System.Decimal */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_Globalization_NumberStyles /* System.Globalization.NumberStyles */ style, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_Decimal? /* System.Decimal */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getBits(System_Decimal? /* System.Decimal */ d) throws -> System_Int32_Array? /* System.Int32[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func remainder(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func multiply(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func negate(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, Int32 /* System.Int32 */ decimals) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, Int32 /* System.Int32 */ decimals, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func subtract(System_Decimal? /* System.Decimal */ d1, System_Decimal? /* System.Decimal */ d2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_Decimal? /* System.Decimal */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_Decimal? /* System.Decimal */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_Decimal? /* System.Decimal */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_Decimal? /* System.Decimal */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_Decimal? /* System.Decimal */ d) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_Decimal? /* System.Decimal */ d) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_Decimal? /* System.Decimal */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_Decimal? /* System.Decimal */ d) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_Decimal? /* System.Decimal */ d) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_Decimal? /* System.Decimal */ d) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func truncate(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypeCode() throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(System_Decimal? /* System.Decimal */ value, System_Decimal? /* System.Decimal */ min, System_Decimal? /* System.Decimal */ max) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copySign(System_Decimal? /* System.Decimal */ value, System_Decimal? /* System.Decimal */ sign) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(System_Decimal? /* System.Decimal */ x, System_Decimal? /* System.Decimal */ y) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(System_Decimal? /* System.Decimal */ x, System_Decimal? /* System.Decimal */ y) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(System_Decimal? /* System.Decimal */ d) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(System_Decimal? /* System.Decimal */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createChecked(System_Type? /* System.Type */ TOther, System_Object? /* System.Object */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createSaturating(System_Type? /* System.Type */ TOther, System_Object? /* System.Object */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createTruncating(System_Type? /* System.Type */ TOther, System_Object? /* System.Object */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isCanonical(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isEvenInteger(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isInteger(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isNegative(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isOddInteger(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isPositive(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func maxMagnitude(System_Decimal? /* System.Decimal */ x, System_Decimal? /* System.Decimal */ y) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func minMagnitude(System_Decimal? /* System.Decimal */ x, System_Decimal? /* System.Decimal */ y) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_Decimal? /* System.Decimal */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt32 /* System.UInt32 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int64 /* System.Int64 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt64 /* System.UInt64 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Float /* System.Single */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(double /* System.Double */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Int32_Array? /* System.Int32[] */ bits) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ lo, Int32 /* System.Int32 */ mid, Int32 /* System.Int32 */ hi, Bool /* System.Boolean */ isNegative, UInt8 /* System.Byte */ scale) throws {
		// TODO: Constructor
		
	
	}
	
	func getScale() throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getZero() -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getOne() -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMinusOne() -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxValue() -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMinValue() -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Decimal_TypeOf())
		
	
	}
	
	deinit {
		System_Decimal_Destroy(self._handle)
		
	
	}
	
	

}



// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
public class System_DateTime /* System.DateTime */ {
	let _handle: System_DateTime_t

	required init(handle: System_DateTime_t) {
		self._handle = handle
	}

	convenience init?(handle: System_DateTime_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_TimeSpan? /* System.TimeSpan */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addDays(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addHours(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMilliseconds(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMicroseconds(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMinutes(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMonths(Int32 /* System.Int32 */ months) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addSeconds(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addTicks(Int64 /* System.Int64 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addYears(Int32 /* System.Int32 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_DateTime? /* System.DateTime */ t1, System_DateTime? /* System.DateTime */ t2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_DateTime? /* System.DateTime */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func daysInMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_DateTime? /* System.DateTime */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_DateTime? /* System.DateTime */ t1, System_DateTime? /* System.DateTime */ t2) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromBinary(Int64 /* System.Int64 */ dateData) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromFileTime(Int64 /* System.Int64 */ fileTime) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromFileTimeUtc(Int64 /* System.Int64 */ fileTime) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromOADate(double /* System.Double */ d) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDaylightSavingTime() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func specifyKind(System_DateTime? /* System.DateTime */ value, System_DateTimeKind /* System.DateTimeKind */ kind) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toBinary() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isLeapYear(Int32 /* System.Int32 */ year) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ styles) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func subtract(System_DateTime? /* System.DateTime */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func subtract(System_TimeSpan? /* System.TimeSpan */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toOADate() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toFileTime() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toFileTimeUtc() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLocalTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLongDateString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLongTimeString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toShortDateString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toShortTimeString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUniversalTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, inout System_DateTime? /* System.DateTime */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ styles, inout System_DateTime? /* System.DateTime */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_DateTime? /* System.DateTime */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_DateTime? /* System.DateTime */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout System_DateOnly? /* System.DateOnly */ date, inout System_TimeOnly? /* System.TimeOnly */ time) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ year, inout Int32? /* System.Int32 */ month, inout Int32? /* System.Int32 */ day) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDateTimeFormats() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDateTimeFormats(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDateTimeFormats(UInt8 /* System.Char */ format) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDateTimeFormats(UInt8 /* System.Char */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypeCode() throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_DateTime? /* System.DateTime */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int64 /* System.Int64 */ ticks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int64 /* System.Int64 */ ticks, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_DateOnly? /* System.DateOnly */ date, System_TimeOnly? /* System.TimeOnly */ time) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_DateOnly? /* System.DateOnly */ date, System_TimeOnly? /* System.TimeOnly */ time, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ microsecond) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ microsecond, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ microsecond, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ microsecond, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar, System_DateTimeKind /* System.DateTimeKind */ kind) throws {
		// TODO: Constructor
		
	
	}
	
	func getDate() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDay() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayOfWeek() throws -> System_DayOfWeek /* System.DayOfWeek */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayOfYear() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHour() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getKind() throws -> System_DateTimeKind /* System.DateTimeKind */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMillisecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMicrosecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNanosecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMinute() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonth() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNow() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTicks() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTimeOfDay() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getToday() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getYear() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUtcNow() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMinValue() -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxValue() -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUnixEpoch() -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_DateTime_TypeOf())
		
	
	}
	
	deinit {
		System_DateTime_Destroy(self._handle)
		
	
	}
	
	

}


public class System_TimeSpan /* System.TimeSpan */ {
	let _handle: System_TimeSpan_t

	required init(handle: System_TimeSpan_t) {
		self._handle = handle
	}

	convenience init?(handle: System_TimeSpan_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_TimeSpan? /* System.TimeSpan */ ts) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func compare(System_TimeSpan? /* System.TimeSpan */ t1, System_TimeSpan? /* System.TimeSpan */ t2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_TimeSpan? /* System.TimeSpan */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromDays(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func duration() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_TimeSpan? /* System.TimeSpan */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func equals(System_TimeSpan? /* System.TimeSpan */ t1, System_TimeSpan? /* System.TimeSpan */ t2) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromHours(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromMilliseconds(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromMicroseconds(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromMinutes(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func negate() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromSeconds(double /* System.Double */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func subtract(System_TimeSpan? /* System.TimeSpan */ ts) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func multiply(double /* System.Double */ factor) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func divide(double /* System.Double */ divisor) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func divide(System_TimeSpan? /* System.TimeSpan */ ts) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromTicks(Int64 /* System.Int64 */ value) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ input, System_IFormatProvider? /* System.IFormatProvider */ formatProvider) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ input, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ formatProvider) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ input, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ formatProvider) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ input, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, System_Globalization_TimeSpanStyles /* System.Globalization.TimeSpanStyles */ styles) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ input, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, System_Globalization_TimeSpanStyles /* System.Globalization.TimeSpanStyles */ styles) throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ input, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ input, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ input, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ input, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, System_Globalization_TimeSpanStyles /* System.Globalization.TimeSpanStyles */ styles, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ input, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ formatProvider, System_Globalization_TimeSpanStyles /* System.Globalization.TimeSpanStyles */ styles, inout System_TimeSpan? /* System.TimeSpan */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ formatProvider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int64 /* System.Int64 */ ticks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ hours, Int32 /* System.Int32 */ minutes, Int32 /* System.Int32 */ seconds) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ days, Int32 /* System.Int32 */ hours, Int32 /* System.Int32 */ minutes, Int32 /* System.Int32 */ seconds) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ days, Int32 /* System.Int32 */ hours, Int32 /* System.Int32 */ minutes, Int32 /* System.Int32 */ seconds, Int32 /* System.Int32 */ milliseconds) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ days, Int32 /* System.Int32 */ hours, Int32 /* System.Int32 */ minutes, Int32 /* System.Int32 */ seconds, Int32 /* System.Int32 */ milliseconds, Int32 /* System.Int32 */ microseconds) throws {
		// TODO: Constructor
		
	
	}
	
	func getTicks() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDays() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHours() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMilliseconds() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMicroseconds() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNanoseconds() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMinutes() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSeconds() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalDays() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalHours() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalMilliseconds() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalMicroseconds() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalNanoseconds() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalMinutes() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalSeconds() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getZero() -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxValue() -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMinValue() -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNanosecondsPerTick() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerMicrosecond() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerMillisecond() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerSecond() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerMinute() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerHour() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTicksPerDay() -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_TimeSpan_TypeOf())
		
	
	}
	
	deinit {
		System_TimeSpan_Destroy(self._handle)
		
	
	}
	
	

}


public class System_String_Array /* System.String[] */ {
	let _handle: System_String_Array_t

	required init(handle: System_String_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_String_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}





public class System_DateOnly /* System.DateOnly */ {
	let _handle: System_DateOnly_t

	required init(handle: System_DateOnly_t) {
		self._handle = handle
	}

	convenience init?(handle: System_DateOnly_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func fromDayNumber(Int32 /* System.Int32 */ dayNumber) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addDays(Int32 /* System.Int32 */ value) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMonths(Int32 /* System.Int32 */ value) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addYears(Int32 /* System.Int32 */ value) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ year, inout Int32? /* System.Int32 */ month, inout Int32? /* System.Int32 */ day) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDateTime(System_TimeOnly? /* System.TimeOnly */ time) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDateTime(System_TimeOnly? /* System.TimeOnly */ time, System_DateTimeKind /* System.DateTimeKind */ kind) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromDateTime(System_DateTime? /* System.DateTime */ dateTime) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_DateOnly? /* System.DateOnly */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_DateOnly? /* System.DateOnly */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLongDateString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toShortDateString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_DateOnly? /* System.DateOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws {
		// TODO: Constructor
		
	
	}
	
	static func getMinValue() throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxValue() throws -> System_DateOnly? /* System.DateOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getYear() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonth() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDay() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayOfWeek() throws -> System_DayOfWeek /* System.DayOfWeek */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayOfYear() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayNumber() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_DateOnly_TypeOf())
		
	
	}
	
	deinit {
		System_DateOnly_Destroy(self._handle)
		
	
	}
	
	

}


public class System_TimeOnly /* System.TimeOnly */ {
	let _handle: System_TimeOnly_t

	required init(handle: System_TimeOnly_t) {
		self._handle = handle
	}

	convenience init?(handle: System_TimeOnly_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_TimeSpan? /* System.TimeSpan */ value) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func add(System_TimeSpan? /* System.TimeSpan */ value, inout Int32? /* System.Int32 */ wrappedDays) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addHours(double /* System.Double */ value) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addHours(double /* System.Double */ value, inout Int32? /* System.Int32 */ wrappedDays) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMinutes(double /* System.Double */ value) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMinutes(double /* System.Double */ value, inout Int32? /* System.Int32 */ wrappedDays) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isBetween(System_TimeOnly? /* System.TimeOnly */ start, System_TimeOnly? /* System.TimeOnly */ end) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ hour, inout Int32? /* System.Int32 */ minute) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ hour, inout Int32? /* System.Int32 */ minute, inout Int32? /* System.Int32 */ second) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ hour, inout Int32? /* System.Int32 */ minute, inout Int32? /* System.Int32 */ second, inout Int32? /* System.Int32 */ millisecond) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deconstruct(inout Int32? /* System.Int32 */ hour, inout Int32? /* System.Int32 */ minute, inout Int32? /* System.Int32 */ second, inout Int32? /* System.Int32 */ millisecond, inout Int32? /* System.Int32 */ microsecond) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromTimeSpan(System_TimeSpan? /* System.TimeSpan */ timeSpan) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromDateTime(System_DateTime? /* System.DateTime */ dateTime) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toTimeSpan() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_TimeOnly? /* System.TimeOnly */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_TimeOnly? /* System.TimeOnly */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ s, System_String_Array? /* System.String[] */ formats, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLongTimeString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toShortTimeString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_TimeOnly? /* System.TimeOnly */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ microsecond) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int64 /* System.Int64 */ ticks) throws {
		// TODO: Constructor
		
	
	}
	
	static func getMinValue() throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMaxValue() throws -> System_TimeOnly? /* System.TimeOnly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHour() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMinute() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMillisecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMicrosecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNanosecond() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTicks() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_TimeOnly_TypeOf())
		
	
	}
	
	deinit {
		System_TimeOnly_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Globalization_Calendar /* System.Globalization.Calendar */ {
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Globalization_Calendar? /* System.Globalization.Calendar */ calendar) throws -> System_Globalization_Calendar? /* System.Globalization.Calendar */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMilliseconds(System_DateTime? /* System.DateTime */ time, double /* System.Double */ milliseconds) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addDays(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ days) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addHours(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ hours) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMinutes(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ minutes) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addMonths(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ months) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addSeconds(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ seconds) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addWeeks(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ weeks) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addYears(System_DateTime? /* System.DateTime */ time, Int32 /* System.Int32 */ years) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDayOfMonth(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDayOfWeek(System_DateTime? /* System.DateTime */ time) throws -> System_DayOfWeek /* System.DayOfWeek */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDayOfYear(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDaysInMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDaysInMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ era) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDaysInYear(Int32 /* System.Int32 */ year) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDaysInYear(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ era) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEra(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHour(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMilliseconds(System_DateTime? /* System.DateTime */ time) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMinute(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMonth(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMonthsInYear(Int32 /* System.Int32 */ year) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMonthsInYear(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ era) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSecond(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getWeekOfYear(System_DateTime? /* System.DateTime */ time, System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */ rule, System_DayOfWeek /* System.DayOfWeek */ firstDayOfWeek) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getYear(System_DateTime? /* System.DateTime */ time) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapDay(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapDay(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ era) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ era) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLeapMonth(Int32 /* System.Int32 */ year) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLeapMonth(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ era) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapYear(Int32 /* System.Int32 */ year) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isLeapYear(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ era) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDateTime(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDateTime(Int32 /* System.Int32 */ year, Int32 /* System.Int32 */ month, Int32 /* System.Int32 */ day, Int32 /* System.Int32 */ hour, Int32 /* System.Int32 */ minute, Int32 /* System.Int32 */ second, Int32 /* System.Int32 */ millisecond, Int32 /* System.Int32 */ era) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toFourDigitYear(Int32 /* System.Int32 */ year) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMinSupportedDateTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxSupportedDateTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAlgorithmType() throws -> System_Globalization_CalendarAlgorithmType /* System.Globalization.CalendarAlgorithmType */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEras() throws -> System_Int32_Array? /* System.Int32[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTwoDigitYearMax() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTwoDigitYearMax(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentEra() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_Calendar_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_Calendar_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Serialization_IFormatterConverter /* System.Runtime.Serialization.IFormatterConverter */ {
	func convert(System_Object? /* System.Object */ value, System_Type? /* System.Type */ type) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func convert(System_Object? /* System.Object */ value, System_TypeCode /* System.TypeCode */ typeCode) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toBoolean(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toChar(System_Object? /* System.Object */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toSByte(System_Object? /* System.Object */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toByte(System_Object? /* System.Object */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toInt16(System_Object? /* System.Object */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUInt16(System_Object? /* System.Object */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toInt32(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUInt32(System_Object? /* System.Object */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toInt64(System_Object? /* System.Object */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUInt64(System_Object? /* System.Object */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toSingle(System_Object? /* System.Object */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDouble(System_Object? /* System.Object */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDecimal(System_Object? /* System.Object */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toDateTime(System_Object? /* System.Object */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_Object? /* System.Object */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Serialization_IFormatterConverter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Serialization_IFormatterConverter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Serialization_StreamingContext /* System.Runtime.Serialization.StreamingContext */ {
	let _handle: System_Runtime_Serialization_StreamingContext_t

	required init(handle: System_Runtime_Serialization_StreamingContext_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_Serialization_StreamingContext_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */ state) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */ state, System_Object? /* System.Object */ additional) throws {
		// TODO: Constructor
		
	
	}
	
	func getState() throws -> System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getContext() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Serialization_StreamingContext_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Serialization_StreamingContext_Destroy(self._handle)
		
	
	}
	
	

}






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
public class System_RuntimeTypeHandle /* System.RuntimeTypeHandle */ {
	let _handle: System_RuntimeTypeHandle_t

	required init(handle: System_RuntimeTypeHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_RuntimeTypeHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func fromIntPtr(Int /* System.IntPtr */ value) throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toIntPtr(System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ value) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ handle) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModuleHandle() throws -> System_ModuleHandle? /* System.ModuleHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_RuntimeTypeHandle_TypeOf())
		
	
	}
	
	deinit {
		System_RuntimeTypeHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_ModuleHandle /* System.ModuleHandle */ {
	let _handle: System_ModuleHandle_t

	required init(handle: System_ModuleHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_ModuleHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_ModuleHandle? /* System.ModuleHandle */ handle) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRuntimeTypeHandleFromMetadataToken(Int32 /* System.Int32 */ typeToken) throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveTypeHandle(Int32 /* System.Int32 */ typeToken) throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveTypeHandle(Int32 /* System.Int32 */ typeToken, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ typeInstantiationContext, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ methodInstantiationContext) throws -> System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRuntimeMethodHandleFromMetadataToken(Int32 /* System.Int32 */ methodToken) throws -> System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMethodHandle(Int32 /* System.Int32 */ methodToken) throws -> System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMethodHandle(Int32 /* System.Int32 */ methodToken, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ typeInstantiationContext, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ methodInstantiationContext) throws -> System_RuntimeMethodHandle? /* System.RuntimeMethodHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRuntimeFieldHandleFromMetadataToken(Int32 /* System.Int32 */ fieldToken) throws -> System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveFieldHandle(Int32 /* System.Int32 */ fieldToken) throws -> System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveFieldHandle(Int32 /* System.Int32 */ fieldToken, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ typeInstantiationContext, System_RuntimeTypeHandle_Array? /* System.RuntimeTypeHandle[] */ methodInstantiationContext) throws -> System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMDStreamVersion() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getEmptyHandle() -> System_ModuleHandle? /* System.ModuleHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_ModuleHandle_TypeOf())
		
	
	}
	
	deinit {
		System_ModuleHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_RuntimeTypeHandle_Array /* System.RuntimeTypeHandle[] */ {
	let _handle: System_RuntimeTypeHandle_Array_t

	required init(handle: System_RuntimeTypeHandle_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_RuntimeTypeHandle_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_RuntimeFieldHandle /* System.RuntimeFieldHandle */ {
	let _handle: System_RuntimeFieldHandle_t

	required init(handle: System_RuntimeFieldHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_RuntimeFieldHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ handle) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromIntPtr(Int /* System.IntPtr */ value) throws -> System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toIntPtr(System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ value) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_RuntimeFieldHandle_TypeOf())
		
	
	}
	
	deinit {
		System_RuntimeFieldHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_ParameterInfo_Array /* System.Reflection.ParameterInfo[] */ {
	let _handle: System_Reflection_ParameterInfo_Array_t

	required init(handle: System_Reflection_ParameterInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ParameterInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Reflection_ParameterInfo /* System.Reflection.ParameterInfo */ {
	let _handle: System_Reflection_ParameterInfo_t

	required init(handle: System_Reflection_ParameterInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ParameterInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func isDefined(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributesData() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModifiedParameterType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOptionalCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRequiredCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRealObject(System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAttributes() throws -> System_Reflection_ParameterAttributes /* System.Reflection.ParameterAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMember() throws -> System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getParameterType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPosition() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsIn() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsLcid() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsOptional() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsOut() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsRetval() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDefaultValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRawDefaultValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHasDefaultValue() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCustomAttributes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMetadataToken() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ParameterInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ParameterInfo_Destroy(self._handle)
		
	
	}
	
	

}






public class System_Reflection_CustomAttributeData /* System.Reflection.CustomAttributeData */ {
	let _handle: System_Reflection_CustomAttributeData_t

	required init(handle: System_Reflection_CustomAttributeData_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_CustomAttributeData_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func getCustomAttributes(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ target) throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Module? /* System.Reflection.Module */ target) throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Assembly? /* System.Reflection.Assembly */ target) throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ target) throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAttributeType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getConstructor() throws -> System_Reflection_ConstructorInfo? /* System.Reflection.ConstructorInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getConstructorArguments() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeTypedArgument> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNamedArguments() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeNamedArgument> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_CustomAttributeData_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_CustomAttributeData_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_Module /* System.Reflection.Module */ {
	func getPEKind(inout System_Reflection_PortableExecutableKinds? /* System.Reflection.PortableExecutableKinds */ peKind, inout System_Reflection_ImageFileMachine? /* System.Reflection.ImageFileMachine */ machine) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isResource() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefined(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributesData() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Type_Array? /* System.Type[] */ types) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethod(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Reflection_CallingConventions /* System.Reflection.CallingConventions */ callConvention, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethods() throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMethods(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingFlags) throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getField(System_String? /* System.String */ name) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getField(System_String? /* System.String */ name, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFields() throws -> System_Reflection_FieldInfo_Array? /* System.Reflection.FieldInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFields(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingFlags) throws -> System_Reflection_FieldInfo_Array? /* System.Reflection.FieldInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypes() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ className) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ className, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getType(System_String? /* System.String */ className, Bool /* System.Boolean */ throwOnError, Bool /* System.Boolean */ ignoreCase) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func findTypes(System_Reflection_TypeFilter? /* System.Reflection.TypeFilter */ filter, System_Object? /* System.Object */ filterCriteria) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveField(Int32 /* System.Int32 */ metadataToken) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveField(Int32 /* System.Int32 */ metadataToken, System_Type_Array? /* System.Type[] */ genericTypeArguments, System_Type_Array? /* System.Type[] */ genericMethodArguments) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMember(Int32 /* System.Int32 */ metadataToken) throws -> System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMember(Int32 /* System.Int32 */ metadataToken, System_Type_Array? /* System.Type[] */ genericTypeArguments, System_Type_Array? /* System.Type[] */ genericMethodArguments) throws -> System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMethod(Int32 /* System.Int32 */ metadataToken) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveMethod(Int32 /* System.Int32 */ metadataToken, System_Type_Array? /* System.Type[] */ genericTypeArguments, System_Type_Array? /* System.Type[] */ genericMethodArguments) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveSignature(Int32 /* System.Int32 */ metadataToken) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveString(Int32 /* System.Int32 */ metadataToken) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveType(Int32 /* System.Int32 */ metadataToken) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveType(Int32 /* System.Int32 */ metadataToken, System_Type_Array? /* System.Type[] */ genericTypeArguments, System_Type_Array? /* System.Type[] */ genericMethodArguments) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullyQualifiedName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMDStreamVersion() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getModuleVersionId() throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getScopeName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getModuleHandle() throws -> System_ModuleHandle? /* System.ModuleHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCustomAttributes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.CustomAttributeData> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMetadataToken() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFilterTypeName() -> System_Reflection_TypeFilter? /* System.Reflection.TypeFilter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getFilterTypeNameIgnoreCase() -> System_Reflection_TypeFilter? /* System.Reflection.TypeFilter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_Module_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_Module_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Guid /* System.Guid */ {
	let _handle: System_Guid_t

	required init(handle: System_Guid_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Guid_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func parse(System_String? /* System.String */ input) throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ input, inout System_Guid? /* System.Guid */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parseExact(System_String? /* System.String */ input, System_String? /* System.String */ format) throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParseExact(System_String? /* System.String */ input, System_String? /* System.String */ format, inout System_Guid? /* System.Guid */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toByteArray() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Guid? /* System.Guid */ g) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func compareTo(System_Guid? /* System.Guid */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_String? /* System.String */ format, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func parse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ s, System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_Guid? /* System.Guid */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func newGuid() throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Byte_Array? /* System.Byte[] */ b) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt32 /* System.UInt32 */ a, UInt16 /* System.UInt16 */ b, UInt16 /* System.UInt16 */ c, UInt8 /* System.Byte */ d, UInt8 /* System.Byte */ e, UInt8 /* System.Byte */ f, UInt8 /* System.Byte */ g, UInt8 /* System.Byte */ h, UInt8 /* System.Byte */ i, UInt8 /* System.Byte */ j, UInt8 /* System.Byte */ k) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ a, Int16 /* System.Int16 */ b, Int16 /* System.Int16 */ c, System_Byte_Array? /* System.Byte[] */ d) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ a, Int16 /* System.Int16 */ b, Int16 /* System.Int16 */ c, UInt8 /* System.Byte */ d, UInt8 /* System.Byte */ e, UInt8 /* System.Byte */ f, UInt8 /* System.Byte */ g, UInt8 /* System.Byte */ h, UInt8 /* System.Byte */ i, UInt8 /* System.Byte */ j, UInt8 /* System.Byte */ k) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ g) throws {
		// TODO: Constructor
		
	
	}
	
	static func getEmpty() -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Guid_TypeOf())
		
	
	}
	
	deinit {
		System_Guid_Destroy(self._handle)
		
	
	}
	
	

}





public class System_Object_Array /* System.Object[] */ {
	let _handle: System_Object_Array_t

	required init(handle: System_Object_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Object_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_MethodInfo /* System.Reflection.MethodInfo */ {
	func getGenericArguments() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericMethodDefinition() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeGenericMethod(System_Type_Array? /* System.Type[] */ typeArguments) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBaseDefinition() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createDelegate(System_Type? /* System.Type */ delegateType) throws -> System_Delegate? /* System.Delegate */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createDelegate(System_Type? /* System.Type */ delegateType, System_Object? /* System.Object */ target) throws -> System_Delegate? /* System.Delegate */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createDelegate(System_Type? /* System.Type */ T) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createDelegate(System_Type? /* System.Type */ T, System_Object? /* System.Object */ target) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReturnParameter() throws -> System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReturnType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReturnTypeCustomAttributes() throws -> System_Reflection_ICustomAttributeProvider? /* System.Reflection.ICustomAttributeProvider */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_MethodInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_MethodInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Type_Array /* System.Type[] */ {
	let _handle: System_Type_Array_t

	required init(handle: System_Type_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Type_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_ICustomAttributeProvider /* System.Reflection.ICustomAttributeProvider */ {
	func getCustomAttributes(Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCustomAttributes(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefined(System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ICustomAttributeProvider_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ICustomAttributeProvider_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_Binder /* System.Reflection.Binder */ {
	func bindToField(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_FieldInfo_Array? /* System.Reflection.FieldInfo[] */ match, System_Object? /* System.Object */ value, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func bindToMethod(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_MethodBase_Array? /* System.Reflection.MethodBase[] */ match, inout System_Object_Array? /* System.Object[] */ args, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_String_Array? /* System.String[] */ names, inout System_Object? /* System.Object */ state) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func changeType(System_Object? /* System.Object */ value, System_Type? /* System.Type */ type, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reorderArgumentArray(inout System_Object_Array? /* System.Object[] */ args, System_Object? /* System.Object */ state) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func selectMethod(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_MethodBase_Array? /* System.Reflection.MethodBase[] */ match, System_Type_Array? /* System.Type[] */ types, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_MethodBase? /* System.Reflection.MethodBase */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func selectProperty(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_PropertyInfo_Array? /* System.Reflection.PropertyInfo[] */ match, System_Type? /* System.Type */ returnType, System_Type_Array? /* System.Type[] */ indexes, System_Reflection_ParameterModifier_Array? /* System.Reflection.ParameterModifier[] */ modifiers) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_Binder_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_Binder_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_FieldInfo /* System.Reflection.FieldInfo */ {
	static func getFieldFromHandle(System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ handle) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFieldFromHandle(System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ handle, System_RuntimeTypeHandle? /* System.RuntimeTypeHandle */ declaringType) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Object? /* System.Object */ obj) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ value, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRawConstantValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModifiedFieldType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOptionalCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRequiredCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_Reflection_FieldAttributes /* System.Reflection.FieldAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFieldType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsInitOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsLiteral() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsNotSerialized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPinvokeImpl() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSpecialName() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsStatic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamily() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamilyAndAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFamilyOrAssembly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPrivate() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPublic() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecuritySafeCritical() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSecurityTransparent() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFieldHandle() throws -> System_RuntimeFieldHandle? /* System.RuntimeFieldHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_FieldInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_FieldInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_FieldInfo_Array /* System.Reflection.FieldInfo[] */ {
	let _handle: System_Reflection_FieldInfo_Array_t

	required init(handle: System_Reflection_FieldInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_FieldInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_MethodBase_Array /* System.Reflection.MethodBase[] */ {
	let _handle: System_Reflection_MethodBase_Array_t

	required init(handle: System_Reflection_MethodBase_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_MethodBase_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}




public class System_Reflection_ParameterModifier_Array /* System.Reflection.ParameterModifier[] */ {
	let _handle: System_Reflection_ParameterModifier_Array_t

	required init(handle: System_Reflection_ParameterModifier_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ParameterModifier_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Reflection_ParameterModifier /* System.Reflection.ParameterModifier */ {
	let _handle: System_Reflection_ParameterModifier_t

	required init(handle: System_Reflection_ParameterModifier_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ParameterModifier_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(Int32 /* System.Int32 */ parameterCount) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ParameterModifier_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ParameterModifier_Destroy(self._handle)
		
	
	}
	
	

}



public class System_Reflection_PropertyInfo /* System.Reflection.PropertyInfo */ {
	func getIndexParameters() throws -> System_Reflection_ParameterInfo_Array? /* System.Reflection.ParameterInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAccessors() throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAccessors(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGetMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGetMethod(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSetMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getSetMethod(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getModifiedPropertyType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOptionalCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRequiredCustomModifiers() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Object? /* System.Object */ obj) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Object? /* System.Object */ obj, System_Object_Array? /* System.Object[] */ index) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getValue(System_Object? /* System.Object */ obj, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ index, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getConstantValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRawConstantValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ value, System_Object_Array? /* System.Object[] */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ value, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ index, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPropertyType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_Reflection_PropertyAttributes /* System.Reflection.PropertyAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSpecialName() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanRead() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanWrite() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGetMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSetMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_PropertyInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_PropertyInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_MethodInfo_Array /* System.Reflection.MethodInfo[] */ {
	let _handle: System_Reflection_MethodInfo_Array_t

	required init(handle: System_Reflection_MethodInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_MethodInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_PropertyInfo_Array /* System.Reflection.PropertyInfo[] */ {
	let _handle: System_Reflection_PropertyInfo_Array_t

	required init(handle: System_Reflection_PropertyInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_PropertyInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_ConstructorInfo /* System.Reflection.ConstructorInfo */ {
	func invoke(System_Object_Array? /* System.Object[] */ parameters) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func invoke(System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ parameters, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getConstructorName() -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTypeConstructorName() -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ConstructorInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ConstructorInfo_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_CustomAttributeTypedArgument /* System.Reflection.CustomAttributeTypedArgument */ {
	let _handle: System_Reflection_CustomAttributeTypedArgument_t

	required init(handle: System_Reflection_CustomAttributeTypedArgument_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_CustomAttributeTypedArgument_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Reflection_CustomAttributeTypedArgument? /* System.Reflection.CustomAttributeTypedArgument */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ argumentType, System_Object? /* System.Object */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Object? /* System.Object */ value) throws {
		// TODO: Constructor
		
	
	}
	
	func getArgumentType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_CustomAttributeTypedArgument_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_CustomAttributeTypedArgument_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_CustomAttributeNamedArgument /* System.Reflection.CustomAttributeNamedArgument */ {
	let _handle: System_Reflection_CustomAttributeNamedArgument_t

	required init(handle: System_Reflection_CustomAttributeNamedArgument_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_CustomAttributeNamedArgument_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Reflection_CustomAttributeNamedArgument? /* System.Reflection.CustomAttributeNamedArgument */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ memberInfo, System_Object? /* System.Object */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ memberInfo, System_Reflection_CustomAttributeTypedArgument? /* System.Reflection.CustomAttributeTypedArgument */ typedArgument) throws {
		// TODO: Constructor
		
	
	}
	
	func getMemberInfo() throws -> System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTypedValue() throws -> System_Reflection_CustomAttributeTypedArgument? /* System.Reflection.CustomAttributeTypedArgument */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMemberName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsField() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_CustomAttributeNamedArgument_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_CustomAttributeNamedArgument_Destroy(self._handle)
		
	
	}
	
	

}



public class System_Reflection_MethodBody /* System.Reflection.MethodBody */ {
	let _handle: System_Reflection_MethodBody_t

	required init(handle: System_Reflection_MethodBody_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_MethodBody_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getILAsByteArray() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLocalSignatureMetadataToken() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLocalVariables() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.LocalVariableInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxStackSize() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getInitLocals() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExceptionHandlingClauses() throws -> System_Collections_Generic_IList_A1? /* System.Collections.Generic.IList<System.Reflection.ExceptionHandlingClause> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_MethodBody_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_MethodBody_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_LocalVariableInfo /* System.Reflection.LocalVariableInfo */ {
	let _handle: System_Reflection_LocalVariableInfo_t

	required init(handle: System_Reflection_LocalVariableInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_LocalVariableInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getLocalType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLocalIndex() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsPinned() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_LocalVariableInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_LocalVariableInfo_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_ExceptionHandlingClause /* System.Reflection.ExceptionHandlingClause */ {
	let _handle: System_Reflection_ExceptionHandlingClause_t

	required init(handle: System_Reflection_ExceptionHandlingClause_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ExceptionHandlingClause_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFlags() throws -> System_Reflection_ExceptionHandlingClauseOptions /* System.Reflection.ExceptionHandlingClauseOptions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTryOffset() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTryLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHandlerOffset() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHandlerLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFilterOffset() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCatchType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ExceptionHandlingClause_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ExceptionHandlingClause_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_IDictionary /* System.Collections.IDictionary */ {
	func contains(System_Object? /* System.Object */ key) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func add(System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Collections_IDictionaryEnumerator? /* System.Collections.IDictionaryEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Object? /* System.Object */ key) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getKeys() throws -> System_Collections_ICollection? /* System.Collections.ICollection */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValues() throws -> System_Collections_ICollection? /* System.Collections.ICollection */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFixedSize() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IDictionary_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IDictionary_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_ICollection /* System.Collections.ICollection */ {
	func copyTo(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSyncRoot() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSynchronized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_ICollection_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_ICollection_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_IDictionaryEnumerator /* System.Collections.IDictionaryEnumerator */ {
	func getKey() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEntry() throws -> System_Collections_DictionaryEntry? /* System.Collections.DictionaryEntry */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IDictionaryEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IDictionaryEnumerator_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_DictionaryEntry /* System.Collections.DictionaryEntry */ {
	let _handle: System_Collections_DictionaryEntry_t

	required init(handle: System_Collections_DictionaryEntry_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Collections_DictionaryEntry_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func deconstruct(inout System_Object? /* System.Object */ key, inout System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws {
		// TODO: Constructor
		
	
	}
	
	func getKey() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setKey(System_Object? /* System.Object */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setValue(System_Object? /* System.Object */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_DictionaryEntry_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_DictionaryEntry_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Exception_Array /* System.Exception[] */ {
	let _handle: System_Exception_Array_t

	required init(handle: System_Exception_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Exception_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}









public class System_Threading_Tasks_TaskFactory /* System.Threading.Tasks.TaskFactory */ {
	let _handle: System_Threading_Tasks_TaskFactory_t

	required init(handle: System_Threading_Tasks_TaskFactory_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Tasks_TaskFactory_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func startNew(System_Action? /* System.Action */ action) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startNew(System_Action? /* System.Action */ action, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startNew(System_Action? /* System.Action */ action, System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func startNew(System_Action? /* System.Action */ action, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken, System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions, System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ scheduler) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ scheduler) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions, System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ continuationOptions) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken, System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions, System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ continuationOptions, System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ scheduler) throws {
		// TODO: Constructor
		
	
	}
	
	func getCancellationToken() throws -> System_Threading_CancellationToken? /* System.Threading.CancellationToken */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getScheduler() throws -> System_Threading_Tasks_TaskScheduler? /* System.Threading.Tasks.TaskScheduler */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCreationOptions() throws -> System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getContinuationOptions() throws -> System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Tasks_TaskFactory_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Tasks_TaskFactory_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_CancellationToken /* System.Threading.CancellationToken */ {
	let _handle: System_Threading_CancellationToken_t

	required init(handle: System_Threading_CancellationToken_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_CancellationToken_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func register(System_Action? /* System.Action */ callback) throws -> System_Threading_CancellationTokenRegistration? /* System.Threading.CancellationTokenRegistration */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func register(System_Action? /* System.Action */ callback, Bool /* System.Boolean */ useSynchronizationContext) throws -> System_Threading_CancellationTokenRegistration? /* System.Threading.CancellationTokenRegistration */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func throwIfCancellationRequested() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Bool /* System.Boolean */ canceled) throws {
		// TODO: Constructor
		
	
	}
	
	static func getNone() throws -> System_Threading_CancellationToken? /* System.Threading.CancellationToken */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCancellationRequested() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCanBeCanceled() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getWaitHandle() throws -> System_Threading_WaitHandle? /* System.Threading.WaitHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_CancellationToken_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_CancellationToken_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_WaitHandle /* System.Threading.WaitHandle */ {
	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitOne(Int32 /* System.Int32 */ millisecondsTimeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitOne(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitOne() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitOne(Int32 /* System.Int32 */ millisecondsTimeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func waitOne(System_TimeSpan? /* System.TimeSpan */ timeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, Int32 /* System.Int32 */ millisecondsTimeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, Int32 /* System.Int32 */ millisecondsTimeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAll(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, System_TimeSpan? /* System.TimeSpan */ timeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, Int32 /* System.Int32 */ millisecondsTimeout) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, Int32 /* System.Int32 */ millisecondsTimeout, Bool /* System.Boolean */ exitContext) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitAny(System_Threading_WaitHandle_Array? /* System.Threading.WaitHandle[] */ waitHandles, System_TimeSpan? /* System.TimeSpan */ timeout, Bool /* System.Boolean */ exitContext) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func signalAndWait(System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toSignal, System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toWaitOn) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func signalAndWait(System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toSignal, System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toWaitOn, System_TimeSpan? /* System.TimeSpan */ timeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func signalAndWait(System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toSignal, System_Threading_WaitHandle? /* System.Threading.WaitHandle */ toWaitOn, Int32 /* System.Int32 */ millisecondsTimeout, Bool /* System.Boolean */ exitContext) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHandle() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setHandle(Int /* System.IntPtr */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSafeWaitHandle() throws -> Microsoft_Win32_SafeHandles_SafeWaitHandle? /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setSafeWaitHandle(Microsoft_Win32_SafeHandles_SafeWaitHandle? /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getWaitTimeout() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_WaitHandle_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_WaitHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class Microsoft_Win32_SafeHandles_SafeWaitHandle /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ {
	let _handle: Microsoft_Win32_SafeHandles_SafeWaitHandle_t

	required init(handle: Microsoft_Win32_SafeHandles_SafeWaitHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: Microsoft_Win32_SafeHandles_SafeWaitHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int /* System.IntPtr */ existingHandle, Bool /* System.Boolean */ ownsHandle) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: Microsoft_Win32_SafeHandles_SafeWaitHandle_TypeOf())
		
	
	}
	
	deinit {
		Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid /* Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid */ {
	func getIsInvalid() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_TypeOf())
		
	
	}
	
	deinit {
		Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_InteropServices_SafeHandle /* System.Runtime.InteropServices.SafeHandle */ {
	func dangerousGetHandle() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setHandleAsInvalid() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dangerousAddRef(inout Bool? /* System.Boolean */ success) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dangerousRelease() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsClosed() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsInvalid() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_InteropServices_SafeHandle_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_InteropServices_SafeHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_ConstrainedExecution_CriticalFinalizerObject /* System.Runtime.ConstrainedExecution.CriticalFinalizerObject */ {
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_ConstrainedExecution_CriticalFinalizerObject_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_ConstrainedExecution_CriticalFinalizerObject_Destroy(self._handle)
		
	
	}
	
	

}



public class System_Threading_WaitHandle_Array /* System.Threading.WaitHandle[] */ {
	let _handle: System_Threading_WaitHandle_Array_t

	required init(handle: System_Threading_WaitHandle_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_WaitHandle_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Threading_CancellationTokenRegistration /* System.Threading.CancellationTokenRegistration */ {
	let _handle: System_Threading_CancellationTokenRegistration_t

	required init(handle: System_Threading_CancellationTokenRegistration_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_CancellationTokenRegistration_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unregister() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Threading_CancellationTokenRegistration? /* System.Threading.CancellationTokenRegistration */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getToken() throws -> System_Threading_CancellationToken? /* System.Threading.CancellationToken */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_CancellationTokenRegistration_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_CancellationTokenRegistration_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Tasks_ValueTask /* System.Threading.Tasks.ValueTask */ {
	let _handle: System_Threading_Tasks_ValueTask_t

	required init(handle: System_Threading_Tasks_ValueTask_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Tasks_ValueTask_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func fromCanceled(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromException(System_Exception? /* System.Exception */ exception) throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func asTask() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func preserve() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAwaiter() throws -> System_Runtime_CompilerServices_ValueTaskAwaiter? /* System.Runtime.CompilerServices.ValueTaskAwaiter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func configureAwait(Bool /* System.Boolean */ continueOnCapturedContext) throws -> System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable? /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ task) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_Tasks_Sources_IValueTaskSource? /* System.Threading.Tasks.Sources.IValueTaskSource */ source, Int16 /* System.Int16 */ token) throws {
		// TODO: Constructor
		
	
	}
	
	static func getCompletedTask() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCompletedSuccessfully() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFaulted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsCanceled() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Tasks_ValueTask_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Tasks_ValueTask_Destroy(self._handle)
		
	
	}
	
	

}


// Type "TResult" was skipped. Reason: It has no full name.
public class System_Runtime_CompilerServices_ValueTaskAwaiter /* System.Runtime.CompilerServices.ValueTaskAwaiter */ {
	let _handle: System_Runtime_CompilerServices_ValueTaskAwaiter_t

	required init(handle: System_Runtime_CompilerServices_ValueTaskAwaiter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_ValueTaskAwaiter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getResult() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func onCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeOnCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_ValueTaskAwaiter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_ValueTaskAwaiter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable */ {
	let _handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t

	required init(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getAwaiter() throws -> System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter? /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */ {
	let _handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t

	required init(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getResult() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func onCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeOnCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Tasks_Sources_IValueTaskSource /* System.Threading.Tasks.Sources.IValueTaskSource */ {
	func getStatus(Int16 /* System.Int16 */ token) throws -> System_Threading_Tasks_Sources_ValueTaskSourceStatus /* System.Threading.Tasks.Sources.ValueTaskSourceStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getResult(Int16 /* System.Int16 */ token) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Tasks_Sources_IValueTaskSource_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Tasks_Sources_IValueTaskSource_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IAsyncResult /* System.IAsyncResult */ {
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAsyncWaitHandle() throws -> System_Threading_WaitHandle? /* System.Threading.WaitHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAsyncState() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCompletedSynchronously() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IAsyncResult_TypeOf())
		
	
	}
	
	deinit {
		System_IAsyncResult_Destroy(self._handle)
		
	
	}
	
	

}


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
public class System_Threading_Tasks_Task_Array /* System.Threading.Tasks.Task[] */ {
	let _handle: System_Threading_Tasks_Task_Array_t

	required init(handle: System_Threading_Tasks_Task_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Tasks_Task_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Runtime_CompilerServices_TaskAwaiter /* System.Runtime.CompilerServices.TaskAwaiter */ {
	let _handle: System_Runtime_CompilerServices_TaskAwaiter_t

	required init(handle: System_Runtime_CompilerServices_TaskAwaiter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_TaskAwaiter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func onCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeOnCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getResult() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_TaskAwaiter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_TaskAwaiter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_ConfiguredTaskAwaitable /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable */ {
	let _handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t

	required init(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getAwaiter() throws -> System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter? /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_ConfiguredTaskAwaitable_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */ {
	let _handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t

	required init(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func onCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeOnCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getResult() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_YieldAwaitable /* System.Runtime.CompilerServices.YieldAwaitable */ {
	let _handle: System_Runtime_CompilerServices_YieldAwaitable_t

	required init(handle: System_Runtime_CompilerServices_YieldAwaitable_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_YieldAwaitable_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getAwaiter() throws -> System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter? /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_YieldAwaitable_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_YieldAwaitable_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */ {
	let _handle: System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t

	required init(handle: System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func onCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeOnCompleted(System_Action? /* System.Action */ continuation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getResult() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsCompleted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_Destroy(self._handle)
		
	
	}
	
	

}


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






































public class System_Buffers_MemoryHandle /* System.Buffers.MemoryHandle */ {
	let _handle: System_Buffers_MemoryHandle_t

	required init(handle: System_Buffers_MemoryHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Buffers_MemoryHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Buffers_MemoryHandle_TypeOf())
		
	
	}
	
	deinit {
		System_Buffers_MemoryHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_InteropServices_GCHandle /* System.Runtime.InteropServices.GCHandle */ {
	let _handle: System_Runtime_InteropServices_GCHandle_t

	required init(handle: System_Runtime_InteropServices_GCHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_InteropServices_GCHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func alloc(System_Object? /* System.Object */ value) throws -> System_Runtime_InteropServices_GCHandle? /* System.Runtime.InteropServices.GCHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func alloc(System_Object? /* System.Object */ value, System_Runtime_InteropServices_GCHandleType /* System.Runtime.InteropServices.GCHandleType */ type) throws -> System_Runtime_InteropServices_GCHandle? /* System.Runtime.InteropServices.GCHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func free() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addrOfPinnedObject() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromIntPtr(Int /* System.IntPtr */ value) throws -> System_Runtime_InteropServices_GCHandle? /* System.Runtime.InteropServices.GCHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toIntPtr(System_Runtime_InteropServices_GCHandle? /* System.Runtime.InteropServices.GCHandle */ value) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Runtime_InteropServices_GCHandle? /* System.Runtime.InteropServices.GCHandle */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTarget() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTarget(System_Object? /* System.Object */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAllocated() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_InteropServices_GCHandle_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_InteropServices_GCHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Buffers_IPinnable /* System.Buffers.IPinnable */ {
	func pin(Int32 /* System.Int32 */ elementIndex) throws -> System_Buffers_MemoryHandle? /* System.Buffers.MemoryHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unpin() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Buffers_IPinnable_TypeOf())
		
	
	}
	
	deinit {
		System_Buffers_IPinnable_Destroy(self._handle)
		
	
	}
	
	

}


public class Microsoft_Win32_SafeHandles_SafeFileHandle /* Microsoft.Win32.SafeHandles.SafeFileHandle */ {
	let _handle: Microsoft_Win32_SafeHandles_SafeFileHandle_t

	required init(handle: Microsoft_Win32_SafeHandles_SafeFileHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: Microsoft_Win32_SafeHandles_SafeFileHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(Int /* System.IntPtr */ preexistingHandle, Bool /* System.Boolean */ ownsHandle) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getIsAsync() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsInvalid() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: Microsoft_Win32_SafeHandles_SafeFileHandle_TypeOf())
		
	
	}
	
	deinit {
		Microsoft_Win32_SafeHandles_SafeFileHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_FileStreamOptions /* System.IO.FileStreamOptions */ {
	let _handle: System_IO_FileStreamOptions_t

	required init(handle: System_IO_FileStreamOptions_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileStreamOptions_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getMode() throws -> System_IO_FileMode /* System.IO.FileMode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMode(System_IO_FileMode /* System.IO.FileMode */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAccess() throws -> System_IO_FileAccess /* System.IO.FileAccess */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAccess(System_IO_FileAccess /* System.IO.FileAccess */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getShare() throws -> System_IO_FileShare /* System.IO.FileShare */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShare(System_IO_FileShare /* System.IO.FileShare */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getOptions() throws -> System_IO_FileOptions /* System.IO.FileOptions */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setOptions(System_IO_FileOptions /* System.IO.FileOptions */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPreallocationSize() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPreallocationSize(Int64 /* System.Int64 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBufferSize() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setBufferSize(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUnixCreateMode() throws -> System_Nullable_A1? /* System.Nullable<System.IO.UnixFileMode> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setUnixCreateMode(System_Nullable_A1? /* System.Nullable<System.IO.UnixFileMode> */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_FileStreamOptions_TypeOf())
		
	
	}
	
	deinit {
		System_IO_FileStreamOptions_Destroy(self._handle)
		
	
	}
	
	

}








public class System_Reflection_TypeInfo /* System.Reflection.TypeInfo */ {
	func asType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredEvent(System_String? /* System.String */ name) throws -> System_Reflection_EventInfo? /* System.Reflection.EventInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredField(System_String? /* System.String */ name) throws -> System_Reflection_FieldInfo? /* System.Reflection.FieldInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredMethod(System_String? /* System.String */ name) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredNestedType(System_String? /* System.String */ name) throws -> System_Reflection_TypeInfo? /* System.Reflection.TypeInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredProperty(System_String? /* System.String */ name) throws -> System_Reflection_PropertyInfo? /* System.Reflection.PropertyInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDeclaredMethods(System_String? /* System.String */ name) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isAssignableFrom(System_Reflection_TypeInfo? /* System.Reflection.TypeInfo */ typeInfo) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getGenericTypeParameters() throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredConstructors() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.ConstructorInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredEvents() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.EventInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredFields() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredMembers() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredMethods() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredNestedTypes() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDeclaredProperties() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getImplementedInterfaces() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Type> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_TypeInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_TypeInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_EventInfo /* System.Reflection.EventInfo */ {
	func getOtherMethods() throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOtherMethods(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAddMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRemoveMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRaiseMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAddMethod(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRemoveMethod(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRaiseMethod(Bool /* System.Boolean */ nonPublic) throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addEventHandler(System_Object? /* System.Object */ target, System_Delegate? /* System.Delegate */ handler) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeEventHandler(System_Object? /* System.Object */ target, System_Delegate? /* System.Delegate */ handler) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMemberType() throws -> System_Reflection_MemberTypes /* System.Reflection.MemberTypes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_Reflection_EventAttributes /* System.Reflection.EventAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSpecialName() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAddMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRemoveMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRaiseMethod() throws -> System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsMulticast() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEventHandlerType() throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_EventInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_EventInfo_Destroy(self._handle)
		
	
	}
	
	

}






























public class System_Reflection_ManifestResourceInfo /* System.Reflection.ManifestResourceInfo */ {
	let _handle: System_Reflection_ManifestResourceInfo_t

	required init(handle: System_Reflection_ManifestResourceInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ManifestResourceInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_Reflection_Assembly? /* System.Reflection.Assembly */ containingAssembly, System_String? /* System.String */ containingFileName, System_Reflection_ResourceLocation /* System.Reflection.ResourceLocation */ resourceLocation) throws {
		// TODO: Constructor
		
	
	}
	
	func getReferencedAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFileName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getResourceLocation() throws -> System_Reflection_ResourceLocation /* System.Reflection.ResourceLocation */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_ManifestResourceInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_ManifestResourceInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_ResolveEventArgs /* System.ResolveEventArgs */ {
	let _handle: System_ResolveEventArgs_t

	required init(handle: System_ResolveEventArgs_t) {
		self._handle = handle
	}

	convenience init?(handle: System_ResolveEventArgs_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_String? /* System.String */ name) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ name, System_Reflection_Assembly? /* System.Reflection.Assembly */ requestingAssembly) throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRequestingAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_ResolveEventArgs_TypeOf())
		
	
	}
	
	deinit {
		System_ResolveEventArgs_Destroy(self._handle)
		
	
	}
	
	

}


public class System_EventArgs /* System.EventArgs */ {
	let _handle: System_EventArgs_t

	required init(handle: System_EventArgs_t) {
		self._handle = handle
	}

	convenience init?(handle: System_EventArgs_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	static func getEmpty() -> System_EventArgs? /* System.EventArgs */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_EventArgs_TypeOf())
		
	
	}
	
	deinit {
		System_EventArgs_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Reflection_Module_Array /* System.Reflection.Module[] */ {
	let _handle: System_Reflection_Module_Array_t

	required init(handle: System_Reflection_Module_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_Module_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}







public class System_Reflection_AssemblyName_Array /* System.Reflection.AssemblyName[] */ {
	let _handle: System_Reflection_AssemblyName_Array_t

	required init(handle: System_Reflection_AssemblyName_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_AssemblyName_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_IO_FileStream_Array /* System.IO.FileStream[] */ {
	let _handle: System_IO_FileStream_Array_t

	required init(handle: System_IO_FileStream_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileStream_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Text_Rune /* System.Text.Rune */ {
	let _handle: System_Text_Rune_t

	required init(handle: System_Text_Rune_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_Rune_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func compareTo(System_Text_Rune? /* System.Text.Rune */ other) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Text_Rune? /* System.Text.Rune */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getRuneAt(System_String? /* System.String */ input, Int32 /* System.Int32 */ index) throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValid(Int32 /* System.Int32 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValid(UInt32 /* System.UInt32 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryCreate(UInt8 /* System.Char */ ch, inout System_Text_Rune? /* System.Text.Rune */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryCreate(UInt8 /* System.Char */ highSurrogate, UInt8 /* System.Char */ lowSurrogate, inout System_Text_Rune? /* System.Text.Rune */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryCreate(Int32 /* System.Int32 */ value, inout System_Text_Rune? /* System.Text.Rune */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryCreate(UInt32 /* System.UInt32 */ value, inout System_Text_Rune? /* System.Text.Rune */ result) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryGetRuneAt(System_String? /* System.String */ input, Int32 /* System.Int32 */ index, inout System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNumericValue(System_Text_Rune? /* System.Text.Rune */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getUnicodeCategory(System_Text_Rune? /* System.Text.Rune */ value) throws -> System_Globalization_UnicodeCategory /* System.Globalization.UnicodeCategory */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isControl(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDigit(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isLetter(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isLetterOrDigit(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isLower(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isNumber(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isPunctuation(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isSeparator(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isSymbol(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isUpper(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isWhiteSpace(System_Text_Rune? /* System.Text.Rune */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toLower(System_Text_Rune? /* System.Text.Rune */ value, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toLowerInvariant(System_Text_Rune? /* System.Text.Rune */ value) throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUpper(System_Text_Rune? /* System.Text.Rune */ value, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture) throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUpperInvariant(System_Text_Rune? /* System.Text.Rune */ value) throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(UInt8 /* System.Char */ ch) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt8 /* System.Char */ highSurrogate, UInt8 /* System.Char */ lowSurrogate) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(UInt32 /* System.UInt32 */ value) throws {
		// TODO: Constructor
		
	
	}
	
	func getIsAscii() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsBmp() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPlane() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getReplacementChar() throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUtf16SequenceLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUtf8SequenceLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_Rune_TypeOf())
		
	
	}
	
	deinit {
		System_Text_Rune_Destroy(self._handle)
		
	
	}
	
	

}



public class System_Globalization_SortKey /* System.Globalization.SortKey */ {
	let _handle: System_Globalization_SortKey_t

	required init(handle: System_Globalization_SortKey_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_SortKey_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func compare(System_Globalization_SortKey? /* System.Globalization.SortKey */ sortkey1, System_Globalization_SortKey? /* System.Globalization.SortKey */ sortkey2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getOriginalString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getKeyData() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_SortKey_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_SortKey_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_SortVersion /* System.Globalization.SortVersion */ {
	let _handle: System_Globalization_SortVersion_t

	required init(handle: System_Globalization_SortVersion_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_SortVersion_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Globalization_SortVersion? /* System.Globalization.SortVersion */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ fullVersion, System_Guid? /* System.Guid */ sortId) throws {
		// TODO: Constructor
		
	
	}
	
	func getFullVersion() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSortId() throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_SortVersion_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_SortVersion_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_TextInfo /* System.Globalization.TextInfo */ {
	let _handle: System_Globalization_TextInfo_t

	required init(handle: System_Globalization_TextInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_TextInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Globalization_TextInfo? /* System.Globalization.TextInfo */ textInfo) throws -> System_Globalization_TextInfo? /* System.Globalization.TextInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLower(UInt8 /* System.Char */ c) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toLower(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUpper(UInt8 /* System.Char */ c) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toUpper(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toTitleCase(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getANSICodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getOEMCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMacCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEBCDICCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLCID() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCultureName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getListSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setListSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsRightToLeft() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_TextInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_TextInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_NumberFormatInfo /* System.Globalization.NumberFormatInfo */ {
	let _handle: System_Globalization_NumberFormatInfo_t

	required init(handle: System_Globalization_NumberFormatInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_NumberFormatInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func getInstance(System_IFormatProvider? /* System.IFormatProvider */ formatProvider) throws -> System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFormat(System_Type? /* System.Type */ formatType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ nfi) throws -> System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	static func getInvariantInfo() throws -> System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyDecimalDigits() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyDecimalDigits(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyDecimalSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyDecimalSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyGroupSizes() throws -> System_Int32_Array? /* System.Int32[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyGroupSizes(System_Int32_Array? /* System.Int32[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberGroupSizes() throws -> System_Int32_Array? /* System.Int32[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberGroupSizes(System_Int32_Array? /* System.Int32[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentGroupSizes() throws -> System_Int32_Array? /* System.Int32[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentGroupSizes(System_Int32_Array? /* System.Int32[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyGroupSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyGroupSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencySymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencySymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentInfo() throws -> System_Globalization_NumberFormatInfo? /* System.Globalization.NumberFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNaNSymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNaNSymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyNegativePattern() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyNegativePattern(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberNegativePattern() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberNegativePattern(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentPositivePattern() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentPositivePattern(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentNegativePattern() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentNegativePattern(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNegativeInfinitySymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNegativeInfinitySymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNegativeSign() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNegativeSign(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberDecimalDigits() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberDecimalDigits(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberDecimalSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberDecimalSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberGroupSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNumberGroupSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrencyPositivePattern() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrencyPositivePattern(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPositiveInfinitySymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPositiveInfinitySymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPositiveSign() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPositiveSign(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentDecimalDigits() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentDecimalDigits(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentDecimalSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentDecimalSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentGroupSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentGroupSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPercentSymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPercentSymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPerMilleSymbol() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPerMilleSymbol(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNativeDigits() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNativeDigits(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDigitSubstitution() throws -> System_Globalization_DigitShapes /* System.Globalization.DigitShapes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDigitSubstitution(System_Globalization_DigitShapes /* System.Globalization.DigitShapes */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_NumberFormatInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_NumberFormatInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_DateTimeFormatInfo /* System.Globalization.DateTimeFormatInfo */ {
	let _handle: System_Globalization_DateTimeFormatInfo_t

	required init(handle: System_Globalization_DateTimeFormatInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_DateTimeFormatInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func getInstance(System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFormat(System_Type? /* System.Type */ formatType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEra(System_String? /* System.String */ eraName) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEraName(Int32 /* System.Int32 */ era) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAbbreviatedEraName(Int32 /* System.Int32 */ era) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAbbreviatedDayName(System_DayOfWeek /* System.DayOfWeek */ dayofweek) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getShortestDayName(System_DayOfWeek /* System.DayOfWeek */ dayOfWeek) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAllDateTimePatterns() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAllDateTimePatterns(UInt8 /* System.Char */ format) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDayName(System_DayOfWeek /* System.DayOfWeek */ dayofweek) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAbbreviatedMonthName(Int32 /* System.Int32 */ month) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMonthName(Int32 /* System.Int32 */ month) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ dtfi) throws -> System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAllDateTimePatterns(System_String_Array? /* System.String[] */ patterns, UInt8 /* System.Char */ format) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	static func getInvariantInfo() throws -> System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentInfo() throws -> System_Globalization_DateTimeFormatInfo? /* System.Globalization.DateTimeFormatInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAMDesignator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAMDesignator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCalendar() throws -> System_Globalization_Calendar? /* System.Globalization.Calendar */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCalendar(System_Globalization_Calendar? /* System.Globalization.Calendar */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDateSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDateSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFirstDayOfWeek() throws -> System_DayOfWeek /* System.DayOfWeek */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFirstDayOfWeek(System_DayOfWeek /* System.DayOfWeek */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCalendarWeekRule() throws -> System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCalendarWeekRule(System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullDateTimePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFullDateTimePattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLongDatePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLongDatePattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLongTimePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLongTimePattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonthDayPattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMonthDayPattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPMDesignator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPMDesignator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRFC1123Pattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getShortDatePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShortDatePattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getShortTimePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShortTimePattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSortableDateTimePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTimeSeparator() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTimeSeparator(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUniversalSortableDateTimePattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getYearMonthPattern() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setYearMonthPattern(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAbbreviatedDayNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAbbreviatedDayNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getShortestDayNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShortestDayNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDayNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDayNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAbbreviatedMonthNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAbbreviatedMonthNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonthNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMonthNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNativeCalendarName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAbbreviatedMonthGenitiveNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAbbreviatedMonthGenitiveNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonthGenitiveNames() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMonthGenitiveNames(System_String_Array? /* System.String[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Globalization_DateTimeFormatInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Globalization_DateTimeFormatInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Globalization_Calendar_Array /* System.Globalization.Calendar[] */ {
	let _handle: System_Globalization_Calendar_Array_t

	required init(handle: System_Globalization_Calendar_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Globalization_Calendar_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



// Type "TState" was skipped. Reason: It has no full name.
public class System_Char_Array /* System.Char[] */ {
	let _handle: System_Char_Array_t

	required init(handle: System_Char_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Char_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_CharEnumerator /* System.CharEnumerator */ {
	let _handle: System_CharEnumerator_t

	required init(handle: System_CharEnumerator_t) {
		self._handle = handle
	}

	convenience init?(handle: System_CharEnumerator_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveNext() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrent() throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_CharEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_CharEnumerator_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_StringRuneEnumerator /* System.Text.StringRuneEnumerator */ {
	let _handle: System_Text_StringRuneEnumerator_t

	required init(handle: System_Text_StringRuneEnumerator_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_StringRuneEnumerator_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getEnumerator() throws -> System_Text_StringRuneEnumerator? /* System.Text.StringRuneEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveNext() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrent() throws -> System_Text_Rune? /* System.Text.Rune */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_StringRuneEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_Text_StringRuneEnumerator_Destroy(self._handle)
		
	
	}
	
	

}






public class System_Text_CompositeFormat /* System.Text.CompositeFormat */ {
	let _handle: System_Text_CompositeFormat_t

	required init(handle: System_Text_CompositeFormat_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_CompositeFormat_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func parse(System_String? /* System.String */ format) throws -> System_Text_CompositeFormat? /* System.Text.CompositeFormat */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryParse(System_String? /* System.String */ format, inout System_Text_CompositeFormat? /* System.Text.CompositeFormat */ compositeFormat) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFormat() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_CompositeFormat_TypeOf())
		
	
	}
	
	deinit {
		System_Text_CompositeFormat_Destroy(self._handle)
		
	
	}
	
	

}



// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg0" was skipped. Reason: It has no full name.
// Type "TArg1" was skipped. Reason: It has no full name.
// Type "TArg2" was skipped. Reason: It has no full name.
public class System_Text_Encoding /* System.Text.Encoding */ {
	static func convert(System_Text_Encoding? /* System.Text.Encoding */ srcEncoding, System_Text_Encoding? /* System.Text.Encoding */ dstEncoding, System_Byte_Array? /* System.Byte[] */ bytes) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func convert(System_Text_Encoding? /* System.Text.Encoding */ srcEncoding, System_Text_Encoding? /* System.Text.Encoding */ dstEncoding, System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func registerProvider(System_Text_EncodingProvider? /* System.Text.EncodingProvider */ provider) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEncoding(Int32 /* System.Int32 */ codepage) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEncoding(Int32 /* System.Int32 */ codepage, System_Text_EncoderFallback? /* System.Text.EncoderFallback */ encoderFallback, System_Text_DecoderFallback? /* System.Text.DecoderFallback */ decoderFallback) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEncoding(System_String? /* System.String */ name) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEncoding(System_String? /* System.String */ name, System_Text_EncoderFallback? /* System.Text.EncoderFallback */ encoderFallback, System_Text_DecoderFallback? /* System.Text.DecoderFallback */ decoderFallback) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEncodings() throws -> System_Text_EncodingInfo_Array? /* System.Text.EncodingInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getPreamble() throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByteCount(System_Char_Array? /* System.Char[] */ chars) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByteCount(System_String? /* System.String */ s) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByteCount(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByteCount(System_String? /* System.String */ s, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_Char_Array? /* System.Char[] */ chars) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex, Int32 /* System.Int32 */ charCount, System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_String? /* System.String */ s) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_String? /* System.String */ s, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_String? /* System.String */ s, Int32 /* System.Int32 */ charIndex, Int32 /* System.Int32 */ charCount, System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCharCount(System_Byte_Array? /* System.Byte[] */ bytes) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCharCount(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChars(System_Byte_Array? /* System.Byte[] */ bytes) throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChars(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChars(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Int32 /* System.Int32 */ byteCount, System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isAlwaysNormalized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isAlwaysNormalized(System_Text_NormalizationForm /* System.Text.NormalizationForm */ form) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDecoder() throws -> System_Text_Decoder? /* System.Text.Decoder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEncoder() throws -> System_Text_Encoder? /* System.Text.Encoder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMaxByteCount(Int32 /* System.Int32 */ charCount) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getMaxCharCount(Int32 /* System.Int32 */ byteCount) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getString(System_Byte_Array? /* System.Byte[] */ bytes) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getString(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createTranscodingStream(System_IO_Stream? /* System.IO.Stream */ innerStream, System_Text_Encoding? /* System.Text.Encoding */ innerStreamEncoding, System_Text_Encoding? /* System.Text.Encoding */ outerStreamEncoding, Bool /* System.Boolean */ leaveOpen) throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDefault() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBodyName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEncodingName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHeaderName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getWebName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getWindowsCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsBrowserDisplay() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsBrowserSave() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsMailNewsDisplay() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsMailNewsSave() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSingleByte() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEncoderFallback() throws -> System_Text_EncoderFallback? /* System.Text.EncoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setEncoderFallback(System_Text_EncoderFallback? /* System.Text.EncoderFallback */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDecoderFallback() throws -> System_Text_DecoderFallback? /* System.Text.DecoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDecoderFallback(System_Text_DecoderFallback? /* System.Text.DecoderFallback */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getASCII() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getLatin1() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUnicode() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getBigEndianUnicode() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUTF7() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUTF8() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getUTF32() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_Encoding_TypeOf())
		
	
	}
	
	deinit {
		System_Text_Encoding_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_EncodingProvider /* System.Text.EncodingProvider */ {
	func getEncoding(System_String? /* System.String */ name) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEncoding(Int32 /* System.Int32 */ codepage) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEncoding(System_String? /* System.String */ name, System_Text_EncoderFallback? /* System.Text.EncoderFallback */ encoderFallback, System_Text_DecoderFallback? /* System.Text.DecoderFallback */ decoderFallback) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEncoding(Int32 /* System.Int32 */ codepage, System_Text_EncoderFallback? /* System.Text.EncoderFallback */ encoderFallback, System_Text_DecoderFallback? /* System.Text.DecoderFallback */ decoderFallback) throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEncodings() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.Text.EncodingInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_EncodingProvider_TypeOf())
		
	
	}
	
	deinit {
		System_Text_EncodingProvider_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_EncoderFallback /* System.Text.EncoderFallback */ {
	func createFallbackBuffer() throws -> System_Text_EncoderFallbackBuffer? /* System.Text.EncoderFallbackBuffer */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getReplacementFallback() throws -> System_Text_EncoderFallback? /* System.Text.EncoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getExceptionFallback() throws -> System_Text_EncoderFallback? /* System.Text.EncoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxCharCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_EncoderFallback_TypeOf())
		
	
	}
	
	deinit {
		System_Text_EncoderFallback_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_EncoderFallbackBuffer /* System.Text.EncoderFallbackBuffer */ {
	func fallback(UInt8 /* System.Char */ charUnknown, Int32 /* System.Int32 */ index) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func fallback(UInt8 /* System.Char */ charUnknownHigh, UInt8 /* System.Char */ charUnknownLow, Int32 /* System.Int32 */ index) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNextChar() throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func movePrevious() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRemaining() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_EncoderFallbackBuffer_TypeOf())
		
	
	}
	
	deinit {
		System_Text_EncoderFallbackBuffer_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_DecoderFallback /* System.Text.DecoderFallback */ {
	func createFallbackBuffer() throws -> System_Text_DecoderFallbackBuffer? /* System.Text.DecoderFallbackBuffer */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getReplacementFallback() throws -> System_Text_DecoderFallback? /* System.Text.DecoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getExceptionFallback() throws -> System_Text_DecoderFallback? /* System.Text.DecoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxCharCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_DecoderFallback_TypeOf())
		
	
	}
	
	deinit {
		System_Text_DecoderFallback_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_DecoderFallbackBuffer /* System.Text.DecoderFallbackBuffer */ {
	func fallback(System_Byte_Array? /* System.Byte[] */ bytesUnknown, Int32 /* System.Int32 */ index) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getNextChar() throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func movePrevious() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRemaining() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_DecoderFallbackBuffer_TypeOf())
		
	
	}
	
	deinit {
		System_Text_DecoderFallbackBuffer_Destroy(self._handle)
		
	
	}
	
	

}






public class System_Text_EncodingInfo /* System.Text.EncodingInfo */ {
	let _handle: System_Text_EncodingInfo_t

	required init(handle: System_Text_EncodingInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_EncodingInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getEncoding() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Text_EncodingProvider? /* System.Text.EncodingProvider */ provider, Int32 /* System.Int32 */ codePage, System_String? /* System.String */ name, System_String? /* System.String */ displayName) throws {
		// TODO: Constructor
		
	
	}
	
	func getCodePage() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDisplayName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_EncodingInfo_TypeOf())
		
	
	}
	
	deinit {
		System_Text_EncodingInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_EncodingInfo_Array /* System.Text.EncodingInfo[] */ {
	let _handle: System_Text_EncodingInfo_Array_t

	required init(handle: System_Text_EncodingInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_EncodingInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Text_Decoder /* System.Text.Decoder */ {
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCharCount(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCharCount(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count, Bool /* System.Boolean */ flush) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChars(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Int32 /* System.Int32 */ byteCount, System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChars(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Int32 /* System.Int32 */ byteCount, System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex, Bool /* System.Boolean */ flush) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func convert(System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Int32 /* System.Int32 */ byteCount, System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex, Int32 /* System.Int32 */ charCount, Bool /* System.Boolean */ flush, inout Int32? /* System.Int32 */ bytesUsed, inout Int32? /* System.Int32 */ charsUsed, inout Bool? /* System.Boolean */ completed) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFallback() throws -> System_Text_DecoderFallback? /* System.Text.DecoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFallback(System_Text_DecoderFallback? /* System.Text.DecoderFallback */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFallbackBuffer() throws -> System_Text_DecoderFallbackBuffer? /* System.Text.DecoderFallbackBuffer */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_Decoder_TypeOf())
		
	
	}
	
	deinit {
		System_Text_Decoder_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_Encoder /* System.Text.Encoder */ {
	func reset() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getByteCount(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count, Bool /* System.Boolean */ flush) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getBytes(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex, Int32 /* System.Int32 */ charCount, System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Bool /* System.Boolean */ flush) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func convert(System_Char_Array? /* System.Char[] */ chars, Int32 /* System.Int32 */ charIndex, Int32 /* System.Int32 */ charCount, System_Byte_Array? /* System.Byte[] */ bytes, Int32 /* System.Int32 */ byteIndex, Int32 /* System.Int32 */ byteCount, Bool /* System.Boolean */ flush, inout Int32? /* System.Int32 */ charsUsed, inout Int32? /* System.Int32 */ bytesUsed, inout Bool? /* System.Boolean */ completed) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFallback() throws -> System_Text_EncoderFallback? /* System.Text.EncoderFallback */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFallback(System_Text_EncoderFallback? /* System.Text.EncoderFallback */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFallbackBuffer() throws -> System_Text_EncoderFallbackBuffer? /* System.Text.EncoderFallbackBuffer */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_Encoder_TypeOf())
		
	
	}
	
	deinit {
		System_Text_Encoder_Destroy(self._handle)
		
	
	}
	
	

}




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
public class System_Runtime_InteropServices_StructLayoutAttribute /* System.Runtime.InteropServices.StructLayoutAttribute */ {
	let _handle: System_Runtime_InteropServices_StructLayoutAttribute_t

	required init(handle: System_Runtime_InteropServices_StructLayoutAttribute_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_InteropServices_StructLayoutAttribute_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_Runtime_InteropServices_LayoutKind /* System.Runtime.InteropServices.LayoutKind */ layoutKind) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int16 /* System.Int16 */ layoutKind) throws {
		// TODO: Constructor
		
	
	}
	
	func getValue() throws -> System_Runtime_InteropServices_LayoutKind /* System.Runtime.InteropServices.LayoutKind */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPack() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPack(Int32 /* System.Int32 */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSize() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setSize(Int32 /* System.Int32 */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCharSet() -> System_Runtime_InteropServices_CharSet /* System.Runtime.InteropServices.CharSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCharSet(System_Runtime_InteropServices_CharSet /* System.Runtime.InteropServices.CharSet */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_InteropServices_StructLayoutAttribute_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_InteropServices_StructLayoutAttribute_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Attribute /* System.Attribute */ {
	static func getCustomAttributes(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_MemberInfo? /* System.Reflection.MemberInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_ParameterInfo? /* System.Reflection.ParameterInfo */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Module? /* System.Reflection.Module */ element) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Module? /* System.Reflection.Module */ element, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_Module? /* System.Reflection.Module */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Assembly? /* System.Reflection.Assembly */ element) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttributes(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, Bool /* System.Boolean */ inherit) throws -> System_Attribute_Array? /* System.Attribute[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDefined(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCustomAttribute(System_Reflection_Assembly? /* System.Reflection.Assembly */ element, System_Type? /* System.Type */ attributeType, Bool /* System.Boolean */ inherit) throws -> System_Attribute? /* System.Attribute */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func match(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefaultAttribute() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getTypeId() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Attribute_TypeOf())
		
	
	}
	
	deinit {
		System_Attribute_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Attribute_Array /* System.Attribute[] */ {
	let _handle: System_Attribute_Array_t

	required init(handle: System_Attribute_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Attribute_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_ConstructorInfo_Array /* System.Reflection.ConstructorInfo[] */ {
	let _handle: System_Reflection_ConstructorInfo_Array_t

	required init(handle: System_Reflection_ConstructorInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_ConstructorInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_EventInfo_Array /* System.Reflection.EventInfo[] */ {
	let _handle: System_Reflection_EventInfo_Array_t

	required init(handle: System_Reflection_EventInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_EventInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_MemberInfo_Array /* System.Reflection.MemberInfo[] */ {
	let _handle: System_Reflection_MemberInfo_Array_t

	required init(handle: System_Reflection_MemberInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_MemberInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Reflection_InterfaceMapping /* System.Reflection.InterfaceMapping */ {
	let _handle: System_Reflection_InterfaceMapping_t

	required init(handle: System_Reflection_InterfaceMapping_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_InterfaceMapping_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getTargetType() -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTargetType(System_Type? /* System.Type */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getInterfaceType() -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setInterfaceType(System_Type? /* System.Type */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTargetMethods() -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTargetMethods(System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getInterfaceMethods() -> System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setInterfaceMethods(System_Reflection_MethodInfo_Array? /* System.Reflection.MethodInfo[] */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Reflection_InterfaceMapping_TypeOf())
		
	
	}
	
	deinit {
		System_Reflection_InterfaceMapping_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_InteropServices_Marshal /* System.Runtime.InteropServices.Marshal */ {
	static func offsetOf(System_Type? /* System.Type */ t, System_String? /* System.String */ fieldName) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readByte(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt16(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt32(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt64(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeByte(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs, UInt8 /* System.Byte */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt32(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs, Int32 /* System.Int32 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt64(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs, Int64 /* System.Int64 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastPInvokeError() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastPInvokeError(Int32 /* System.Int32 */ error) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExceptionPointers() throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExceptionCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func structureToPtr(System_Object? /* System.Object */ structure, Int /* System.IntPtr */ ptr, Bool /* System.Boolean */ fDeleteOld) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func destroyStructure(Int /* System.IntPtr */ ptr, System_Type? /* System.Type */ structuretype) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocHGlobal(Int32 /* System.Int32 */ cb) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringAnsi(Int /* System.IntPtr */ ptr) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringAnsi(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ len) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringUni(Int /* System.IntPtr */ ptr) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringUni(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ len) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringUTF8(Int /* System.IntPtr */ ptr) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringUTF8(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ byteLen) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sizeOf(System_Object? /* System.Object */ structure) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sizeOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ structure) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sizeOf(System_Type? /* System.Type */ t) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sizeOf(System_Type? /* System.Type */ T) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func queryInterface(Int /* System.IntPtr */ pUnk, inout System_Guid? /* System.Guid */ iid, inout Int? /* System.IntPtr */ ppv) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func addRef(Int /* System.IntPtr */ pUnk) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func release(Int /* System.IntPtr */ pUnk) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func unsafeAddrOfPinnedArrayElement(System_Array? /* System.Array */ arr, Int32 /* System.Int32 */ index) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func unsafeAddrOfPinnedArrayElement(System_Type? /* System.Type */ T, System_Array? /* System.Array */ arr, Int32 /* System.Int32 */ index) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func offsetOf(System_Type? /* System.Type */ T, System_String? /* System.String */ fieldName) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Int32_Array? /* System.Int32[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Char_Array? /* System.Char[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Int16_Array? /* System.Int16[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Int64_Array? /* System.Int64[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Single_Array? /* System.Single[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Double_Array? /* System.Double[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_Byte_Array? /* System.Byte[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_IntPtr_Array? /* System.IntPtr[] */ source, Int32 /* System.Int32 */ startIndex, Int /* System.IntPtr */ destination, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Int32_Array? /* System.Int32[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Char_Array? /* System.Char[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Int16_Array? /* System.Int16[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Int64_Array? /* System.Int64[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Single_Array? /* System.Single[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Double_Array? /* System.Double[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_Byte_Array? /* System.Byte[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(Int /* System.IntPtr */ source, System_IntPtr_Array? /* System.IntPtr[] */ destination, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readByte(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readByte(Int /* System.IntPtr */ ptr) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt16(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt16(Int /* System.IntPtr */ ptr) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt32(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt32(Int /* System.IntPtr */ ptr) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readIntPtr(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readIntPtr(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readIntPtr(Int /* System.IntPtr */ ptr) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt64(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readInt64(Int /* System.IntPtr */ ptr) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeByte(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs, UInt8 /* System.Byte */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeByte(Int /* System.IntPtr */ ptr, UInt8 /* System.Byte */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt32(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs, Int32 /* System.Int32 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt32(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeIntPtr(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs, Int /* System.IntPtr */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeIntPtr(System_Object? /* System.Object */ ptr, Int32 /* System.Int32 */ ofs, Int /* System.IntPtr */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeIntPtr(Int /* System.IntPtr */ ptr, Int /* System.IntPtr */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt64(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ ofs, Int64 /* System.Int64 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeInt64(Int /* System.IntPtr */ ptr, Int64 /* System.Int64 */ val) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func prelink(System_Reflection_MethodInfo? /* System.Reflection.MethodInfo */ m) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func prelinkAll(System_Type? /* System.Type */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func structureToPtr(System_Type? /* System.Type */ T, System_Object? /* System.Object */ structure, Int /* System.IntPtr */ ptr, Bool /* System.Boolean */ fDeleteOld) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStructure(Int /* System.IntPtr */ ptr, System_Type? /* System.Type */ structureType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStructure(Int /* System.IntPtr */ ptr, System_Object? /* System.Object */ structure) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStructure(System_Type? /* System.Type */ T, Int /* System.IntPtr */ ptr, System_Object? /* System.Object */ structure) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStructure(System_Type? /* System.Type */ T, Int /* System.IntPtr */ ptr) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func destroyStructure(System_Type? /* System.Type */ T, Int /* System.IntPtr */ ptr) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getHINSTANCE(System_Reflection_Module? /* System.Reflection.Module */ m) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExceptionForHR(Int32 /* System.Int32 */ errorCode) throws -> System_Exception? /* System.Exception */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExceptionForHR(Int32 /* System.Int32 */ errorCode, Int /* System.IntPtr */ errorInfo) throws -> System_Exception? /* System.Exception */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func throwExceptionForHR(Int32 /* System.Int32 */ errorCode) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func throwExceptionForHR(Int32 /* System.Int32 */ errorCode, Int /* System.IntPtr */ errorInfo) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func secureStringToBSTR(System_Security_SecureString? /* System.Security.SecureString */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func secureStringToCoTaskMemAnsi(System_Security_SecureString? /* System.Security.SecureString */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func secureStringToCoTaskMemUnicode(System_Security_SecureString? /* System.Security.SecureString */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func secureStringToGlobalAllocAnsi(System_Security_SecureString? /* System.Security.SecureString */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func secureStringToGlobalAllocUnicode(System_Security_SecureString? /* System.Security.SecureString */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToHGlobalAnsi(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToHGlobalUni(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToCoTaskMemUni(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToCoTaskMemUTF8(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToCoTaskMemAnsi(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func generateGuidForType(System_Type? /* System.Type */ type) throws -> System_Guid? /* System.Guid */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func generateProgIdForType(System_Type? /* System.Type */ type) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDelegateForFunctionPointer(Int /* System.IntPtr */ ptr, System_Type? /* System.Type */ t) throws -> System_Delegate? /* System.Delegate */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDelegateForFunctionPointer(System_Type? /* System.Type */ TDelegate, Int /* System.IntPtr */ ptr) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFunctionPointerForDelegate(System_Delegate? /* System.Delegate */ d) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFunctionPointerForDelegate(System_Type? /* System.Type */ TDelegate, System_Object? /* System.Object */ d) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getHRForLastWin32Error() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeBSTR(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeCoTaskMemAnsi(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeCoTaskMemUnicode(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeCoTaskMemUTF8(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeGlobalAllocAnsi(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func zeroFreeGlobalAllocUnicode(Int /* System.IntPtr */ s) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToBSTR(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringBSTR(Int /* System.IntPtr */ ptr) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypeFromCLSID(System_Guid? /* System.Guid */ clsid) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func initHandle(System_Runtime_InteropServices_SafeHandle? /* System.Runtime.InteropServices.SafeHandle */ safeHandle, Int /* System.IntPtr */ handle) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWin32Error() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastPInvokeErrorMessage() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getHRForException(System_Exception? /* System.Exception */ e) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func areComObjectsAvailableForCleanup() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createAggregatedObject(Int /* System.IntPtr */ pOuter, System_Object? /* System.Object */ o) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bindToMoniker(System_String? /* System.String */ monikerName) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func cleanupUnusedObjectsInCurrentContext() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createAggregatedObject(System_Type? /* System.Type */ T, Int /* System.IntPtr */ pOuter, System_Object? /* System.Object */ o) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createWrapperOfType(System_Object? /* System.Object */ o, System_Type? /* System.Type */ t) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createWrapperOfType(System_Type? /* System.Type */ T, System_Type? /* System.Type */ TWrapper, System_Object? /* System.Object */ o) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func changeWrapperHandleStrength(System_Object? /* System.Object */ otp, Bool /* System.Boolean */ fIsWeak) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func finalReleaseComObject(System_Object? /* System.Object */ o) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getComInterfaceForObject(System_Object? /* System.Object */ o, System_Type? /* System.Type */ T) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getComInterfaceForObject(System_Object? /* System.Object */ o, System_Type? /* System.Type */ T, System_Runtime_InteropServices_CustomQueryInterfaceMode /* System.Runtime.InteropServices.CustomQueryInterfaceMode */ mode) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getComInterfaceForObject(System_Type? /* System.Type */ T, System_Type? /* System.Type */ TInterface, System_Object? /* System.Object */ o) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getComObjectData(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ key) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getIDispatchForObject(System_Object? /* System.Object */ o) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getIUnknownForObject(System_Object? /* System.Object */ o) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNativeVariantForObject(System_Object? /* System.Object */ obj, Int /* System.IntPtr */ pDstNativeVariant) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNativeVariantForObject(System_Type? /* System.Type */ T, System_Object? /* System.Object */ obj, Int /* System.IntPtr */ pDstNativeVariant) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTypedObjectForIUnknown(Int /* System.IntPtr */ pUnk, System_Type? /* System.Type */ t) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getObjectForIUnknown(Int /* System.IntPtr */ pUnk) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getObjectForNativeVariant(Int /* System.IntPtr */ pSrcNativeVariant) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getObjectForNativeVariant(System_Type? /* System.Type */ T, Int /* System.IntPtr */ pSrcNativeVariant) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getObjectsForNativeVariants(Int /* System.IntPtr */ aSrcNativeVariant, Int32 /* System.Int32 */ cVars) throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getObjectsForNativeVariants(System_Type? /* System.Type */ T, Int /* System.IntPtr */ aSrcNativeVariant, Int32 /* System.Int32 */ cVars) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getStartComSlot(System_Type? /* System.Type */ t) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getEndComSlot(System_Type? /* System.Type */ t) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getUniqueObjectForIUnknown(Int /* System.IntPtr */ unknown) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isComObject(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isTypeVisibleFromCom(System_Type? /* System.Type */ t) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func releaseComObject(System_Object? /* System.Object */ o) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setComObjectData(System_Object? /* System.Object */ obj, System_Object? /* System.Object */ key, System_Object? /* System.Object */ data) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringAuto(Int /* System.IntPtr */ ptr, Int32 /* System.Int32 */ len) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ptrToStringAuto(Int /* System.IntPtr */ ptr) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToHGlobalAuto(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func stringToCoTaskMemAuto(System_String? /* System.String */ s) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocHGlobal(Int /* System.IntPtr */ cb) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func freeHGlobal(Int /* System.IntPtr */ hglobal) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reAllocHGlobal(Int /* System.IntPtr */ pv, Int /* System.IntPtr */ cb) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocCoTaskMem(Int32 /* System.Int32 */ cb) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func freeCoTaskMem(Int /* System.IntPtr */ ptr) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reAllocCoTaskMem(Int /* System.IntPtr */ pv, Int32 /* System.Int32 */ cb) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func freeBSTR(Int /* System.IntPtr */ ptr) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastSystemError() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastSystemError(Int32 /* System.Int32 */ error) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getPInvokeErrorMessage(Int32 /* System.Int32 */ error) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getSystemDefaultCharSize() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getSystemMaxDBCSCharSize() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_InteropServices_Marshal_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_InteropServices_Marshal_Destroy_1(self._handle)
		
	
	}
	
	

}


// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

public class System_Int16_Array /* System.Int16[] */ {
	let _handle: System_Int16_Array_t

	required init(handle: System_Int16_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Int16_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Single_Array /* System.Single[] */ {
	let _handle: System_Single_Array_t

	required init(handle: System_Single_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Single_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_Double_Array /* System.Double[] */ {
	let _handle: System_Double_Array_t

	required init(handle: System_Double_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Double_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


public class System_IntPtr_Array /* System.IntPtr[] */ {
	let _handle: System_IntPtr_Array_t

	required init(handle: System_IntPtr_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IntPtr_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}


// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
public class System_Security_SecureString /* System.Security.SecureString */ {
	let _handle: System_Security_SecureString_t

	required init(handle: System_Security_SecureString_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Security_SecureString_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func appendChar(UInt8 /* System.Char */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copy() throws -> System_Security_SecureString? /* System.Security.SecureString */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insertAt(Int32 /* System.Int32 */ index, UInt8 /* System.Char */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func makeReadOnly() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAt(Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAt(Int32 /* System.Int32 */ index, UInt8 /* System.Char */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_SecureString_TypeOf())
		
	
	}
	
	deinit {
		System_Security_SecureString_Destroy(self._handle)
		
	
	}
	
	

}


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

public class System_NullReferenceException /* System.NullReferenceException */ {
	let _handle: System_NullReferenceException_t

	required init(handle: System_NullReferenceException_t) {
		self._handle = handle
	}

	convenience init?(handle: System_NullReferenceException_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Exception? /* System.Exception */ innerException) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_NullReferenceException_TypeOf())
		
	
	}
	
	deinit {
		System_NullReferenceException_Destroy(self._handle)
		
	
	}
	
	

}


public class System_SystemException /* System.SystemException */ {
	let _handle: System_SystemException_t

	required init(handle: System_SystemException_t) {
		self._handle = handle
	}

	convenience init?(handle: System_SystemException_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ message, System_Exception? /* System.Exception */ innerException) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_SystemException_TypeOf())
		
	
	}
	
	deinit {
		System_SystemException_Destroy(self._handle)
		
	
	}
	
	

}






// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
// Type "TOther" was skipped. Reason: It has no full name.
public class System_GC /* System.GC */ {
	static func getGCMemoryInfo() throws -> System_GCMemoryInfo? /* System.GCMemoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getGCMemoryInfo(System_GCKind /* System.GCKind */ kind) throws -> System_GCMemoryInfo? /* System.GCMemoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func addMemoryPressure(Int64 /* System.Int64 */ bytesAllocated) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func removeMemoryPressure(Int64 /* System.Int64 */ bytesAllocated) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getGeneration(System_Object? /* System.Object */ obj) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collect(Int32 /* System.Int32 */ generation) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collect() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collect(Int32 /* System.Int32 */ generation, System_GCCollectionMode /* System.GCCollectionMode */ mode) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collect(Int32 /* System.Int32 */ generation, System_GCCollectionMode /* System.GCCollectionMode */ mode, Bool /* System.Boolean */ blocking) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collect(Int32 /* System.Int32 */ generation, System_GCCollectionMode /* System.GCCollectionMode */ mode, Bool /* System.Boolean */ blocking, Bool /* System.Boolean */ compacting) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func collectionCount(Int32 /* System.Int32 */ generation) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func keepAlive(System_Object? /* System.Object */ obj) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getGeneration(System_WeakReference? /* System.WeakReference */ wo) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForPendingFinalizers() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func suppressFinalize(System_Object? /* System.Object */ obj) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reRegisterForFinalize(System_Object? /* System.Object */ obj) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTotalMemory(Bool /* System.Boolean */ forceFullCollection) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAllocatedBytesForCurrentThread() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTotalAllocatedBytes(Bool /* System.Boolean */ precise) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func registerForFullGCNotification(Int32 /* System.Int32 */ maxGenerationThreshold, Int32 /* System.Int32 */ largeObjectHeapThreshold) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func cancelFullGCNotification() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCApproach() throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCApproach(Int32 /* System.Int32 */ millisecondsTimeout) throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCComplete() throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCComplete(Int32 /* System.Int32 */ millisecondsTimeout) throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryStartNoGCRegion(Int64 /* System.Int64 */ totalSize) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryStartNoGCRegion(Int64 /* System.Int64 */ totalSize, Int64 /* System.Int64 */ lohSize) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryStartNoGCRegion(Int64 /* System.Int64 */ totalSize, Bool /* System.Boolean */ disallowFullBlockingGC) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tryStartNoGCRegion(Int64 /* System.Int64 */ totalSize, Int64 /* System.Int64 */ lohSize, Bool /* System.Boolean */ disallowFullBlockingGC) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func endNoGCRegion() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocateUninitializedArray(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ length, Bool /* System.Boolean */ pinned) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocateArray(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ length, Bool /* System.Boolean */ pinned) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTotalPauseDuration() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getConfigurationVariables() throws -> System_Collections_Generic_IReadOnlyDictionary_A2? /* System.Collections.Generic.IReadOnlyDictionary<System.String,System.Object> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCApproach(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func waitForFullGCComplete(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> System_GCNotificationStatus /* System.GCNotificationStatus */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getMaxGeneration() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_GC_TypeOf())
		
	
	}
	
	deinit {
		System_GC_Destroy(self._handle)
		
	
	}
	
	

}


public class System_GCMemoryInfo /* System.GCMemoryInfo */ {
	let _handle: System_GCMemoryInfo_t

	required init(handle: System_GCMemoryInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_GCMemoryInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getHighMemoryLoadThresholdBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMemoryLoadBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalAvailableMemoryBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getHeapSizeBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFragmentedBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIndex() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getGeneration() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCompacted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getConcurrent() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTotalCommittedBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPromotedBytes() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPinnedObjectsCount() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFinalizationPendingCount() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPauseTimePercentage() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_GCMemoryInfo_TypeOf())
		
	
	}
	
	deinit {
		System_GCMemoryInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_WeakReference /* System.WeakReference */ {
	let _handle: System_WeakReference_t

	required init(handle: System_WeakReference_t) {
		self._handle = handle
	}

	convenience init?(handle: System_WeakReference_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Object? /* System.Object */ target) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Object? /* System.Object */ target, Bool /* System.Boolean */ trackResurrection) throws {
		// TODO: Constructor
		
	
	}
	
	func getTrackResurrection() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAlive() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTarget() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTarget(System_Object? /* System.Object */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_WeakReference_TypeOf())
		
	
	}
	
	deinit {
		System_WeakReference_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.







public class System_Math /* System.Math */ {
	static func acos(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func acosh(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func asin(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func asinh(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func atan(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func atanh(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func atan2(double /* System.Double */ y, double /* System.Double */ x) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func cbrt(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ceiling(double /* System.Double */ a) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func cos(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func cosh(double /* System.Double */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func exp(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func floor(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fusedMultiplyAdd(double /* System.Double */ x, double /* System.Double */ y, double /* System.Double */ z) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func log(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func log2(double /* System.Double */ x) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func log10(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func pow(double /* System.Double */ x, double /* System.Double */ y) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sin(double /* System.Double */ a) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sinCos(double /* System.Double */ x) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.Double,System.Double> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sinh(double /* System.Double */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sqrt(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tan(double /* System.Double */ a) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func tanh(double /* System.Double */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Int16 /* System.Int16 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Int32 /* System.Int32 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Int64 /* System.Int64 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Int /* System.IntPtr */ value) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Int8 /* System.SByte */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(System_Decimal? /* System.Decimal */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(double /* System.Double */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func abs(Float /* System.Single */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bigMul(Int32 /* System.Int32 */ a, Int32 /* System.Int32 */ b) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bigMul(UInt64 /* System.UInt64 */ a, UInt64 /* System.UInt64 */ b, inout UInt64? /* System.UInt64 */ low) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bigMul(Int64 /* System.Int64 */ a, Int64 /* System.Int64 */ b, inout Int64? /* System.Int64 */ low) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bitDecrement(double /* System.Double */ x) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func bitIncrement(double /* System.Double */ x) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copySign(double /* System.Double */ x, double /* System.Double */ y) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int32 /* System.Int32 */ a, Int32 /* System.Int32 */ b, inout Int32? /* System.Int32 */ result) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int64 /* System.Int64 */ a, Int64 /* System.Int64 */ b, inout Int64? /* System.Int64 */ result) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int8 /* System.SByte */ left, Int8 /* System.SByte */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.SByte,System.SByte> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(UInt8 /* System.Byte */ left, UInt8 /* System.Byte */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.Byte,System.Byte> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int16 /* System.Int16 */ left, Int16 /* System.Int16 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.Int16,System.Int16> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(UInt16 /* System.UInt16 */ left, UInt16 /* System.UInt16 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.UInt16,System.UInt16> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int32 /* System.Int32 */ left, Int32 /* System.Int32 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.Int32,System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(UInt32 /* System.UInt32 */ left, UInt32 /* System.UInt32 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.UInt32,System.UInt32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int64 /* System.Int64 */ left, Int64 /* System.Int64 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.Int64,System.Int64> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(UInt64 /* System.UInt64 */ left, UInt64 /* System.UInt64 */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.UInt64,System.UInt64> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(Int /* System.IntPtr */ left, Int /* System.IntPtr */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.IntPtr,System.IntPtr> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func divRem(UInt /* System.UIntPtr */ left, UInt /* System.UIntPtr */ right) throws -> System_ValueTuple_A2? /* System.ValueTuple<System.UIntPtr,System.UIntPtr> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func ceiling(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(UInt8 /* System.Byte */ value, UInt8 /* System.Byte */ min, UInt8 /* System.Byte */ max) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(System_Decimal? /* System.Decimal */ value, System_Decimal? /* System.Decimal */ min, System_Decimal? /* System.Decimal */ max) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(double /* System.Double */ value, double /* System.Double */ min, double /* System.Double */ max) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Int16 /* System.Int16 */ value, Int16 /* System.Int16 */ min, Int16 /* System.Int16 */ max) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Int32 /* System.Int32 */ value, Int32 /* System.Int32 */ min, Int32 /* System.Int32 */ max) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Int64 /* System.Int64 */ value, Int64 /* System.Int64 */ min, Int64 /* System.Int64 */ max) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Int /* System.IntPtr */ value, Int /* System.IntPtr */ min, Int /* System.IntPtr */ max) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Int8 /* System.SByte */ value, Int8 /* System.SByte */ min, Int8 /* System.SByte */ max) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(Float /* System.Single */ value, Float /* System.Single */ min, Float /* System.Single */ max) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(UInt16 /* System.UInt16 */ value, UInt16 /* System.UInt16 */ min, UInt16 /* System.UInt16 */ max) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(UInt32 /* System.UInt32 */ value, UInt32 /* System.UInt32 */ min, UInt32 /* System.UInt32 */ max) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(UInt64 /* System.UInt64 */ value, UInt64 /* System.UInt64 */ min, UInt64 /* System.UInt64 */ max) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func clamp(UInt /* System.UIntPtr */ value, UInt /* System.UIntPtr */ min, UInt /* System.UIntPtr */ max) throws -> UInt /* System.UIntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func floor(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func iEEERemainder(double /* System.Double */ x, double /* System.Double */ y) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func iLogB(double /* System.Double */ x) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func log(double /* System.Double */ a, double /* System.Double */ newBase) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(UInt8 /* System.Byte */ val1, UInt8 /* System.Byte */ val2) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(System_Decimal? /* System.Decimal */ val1, System_Decimal? /* System.Decimal */ val2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(double /* System.Double */ val1, double /* System.Double */ val2) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Int16 /* System.Int16 */ val1, Int16 /* System.Int16 */ val2) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Int32 /* System.Int32 */ val1, Int32 /* System.Int32 */ val2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Int64 /* System.Int64 */ val1, Int64 /* System.Int64 */ val2) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Int /* System.IntPtr */ val1, Int /* System.IntPtr */ val2) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Int8 /* System.SByte */ val1, Int8 /* System.SByte */ val2) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(Float /* System.Single */ val1, Float /* System.Single */ val2) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(UInt16 /* System.UInt16 */ val1, UInt16 /* System.UInt16 */ val2) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(UInt32 /* System.UInt32 */ val1, UInt32 /* System.UInt32 */ val2) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(UInt64 /* System.UInt64 */ val1, UInt64 /* System.UInt64 */ val2) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func max(UInt /* System.UIntPtr */ val1, UInt /* System.UIntPtr */ val2) throws -> UInt /* System.UIntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func maxMagnitude(double /* System.Double */ x, double /* System.Double */ y) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(UInt8 /* System.Byte */ val1, UInt8 /* System.Byte */ val2) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(System_Decimal? /* System.Decimal */ val1, System_Decimal? /* System.Decimal */ val2) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(double /* System.Double */ val1, double /* System.Double */ val2) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Int16 /* System.Int16 */ val1, Int16 /* System.Int16 */ val2) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Int32 /* System.Int32 */ val1, Int32 /* System.Int32 */ val2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Int64 /* System.Int64 */ val1, Int64 /* System.Int64 */ val2) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Int /* System.IntPtr */ val1, Int /* System.IntPtr */ val2) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Int8 /* System.SByte */ val1, Int8 /* System.SByte */ val2) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(Float /* System.Single */ val1, Float /* System.Single */ val2) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(UInt16 /* System.UInt16 */ val1, UInt16 /* System.UInt16 */ val2) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(UInt32 /* System.UInt32 */ val1, UInt32 /* System.UInt32 */ val2) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(UInt64 /* System.UInt64 */ val1, UInt64 /* System.UInt64 */ val2) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func min(UInt /* System.UIntPtr */ val1, UInt /* System.UIntPtr */ val2) throws -> UInt /* System.UIntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func minMagnitude(double /* System.Double */ x, double /* System.Double */ y) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reciprocalEstimate(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func reciprocalSqrtEstimate(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, Int32 /* System.Int32 */ decimals) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(System_Decimal? /* System.Decimal */ d, Int32 /* System.Int32 */ decimals, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(double /* System.Double */ a) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(double /* System.Double */ value, Int32 /* System.Int32 */ digits) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(double /* System.Double */ value, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func round(double /* System.Double */ value, Int32 /* System.Int32 */ digits, System_MidpointRounding /* System.MidpointRounding */ mode) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(System_Decimal? /* System.Decimal */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(double /* System.Double */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Int16 /* System.Int16 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Int32 /* System.Int32 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Int64 /* System.Int64 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Int /* System.IntPtr */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Int8 /* System.SByte */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sign(Float /* System.Single */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func truncate(System_Decimal? /* System.Decimal */ d) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func truncate(double /* System.Double */ d) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func scaleB(double /* System.Double */ x, Int32 /* System.Int32 */ n) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getE() -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getPI() -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTau() -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Math_TypeOf())
		
	
	}
	
	deinit {
		System_Math_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Convert /* System.Convert */ {
	static func getTypeCode(System_Object? /* System.Object */ value) throws -> System_TypeCode /* System.TypeCode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isDBNull(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func changeType(System_Object? /* System.Object */ value, System_TypeCode /* System.TypeCode */ typeCode) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func changeType(System_Object? /* System.Object */ value, System_TypeCode /* System.TypeCode */ typeCode, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func changeType(System_Object? /* System.Object */ value, System_Type? /* System.Type */ conversionType) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func changeType(System_Object? /* System.Object */ value, System_Type? /* System.Type */ conversionType, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Bool /* System.Boolean */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Int8 /* System.SByte */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(UInt8 /* System.Char */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(UInt8 /* System.Byte */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Int16 /* System.Int16 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(UInt16 /* System.UInt16 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Int32 /* System.Int32 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(UInt32 /* System.UInt32 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Int64 /* System.Int64 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(UInt64 /* System.UInt64 */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(Float /* System.Single */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(double /* System.Double */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_Decimal? /* System.Decimal */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBoolean(System_DateTime? /* System.DateTime */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_Object? /* System.Object */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Bool /* System.Boolean */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(UInt8 /* System.Char */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Int8 /* System.SByte */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(UInt8 /* System.Byte */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Int16 /* System.Int16 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(UInt16 /* System.UInt16 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Int32 /* System.Int32 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(UInt32 /* System.UInt32 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Int64 /* System.Int64 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(UInt64 /* System.UInt64 */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_String? /* System.String */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(Float /* System.Single */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(double /* System.Double */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_Decimal? /* System.Decimal */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toChar(System_DateTime? /* System.DateTime */ value) throws -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_Object? /* System.Object */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Bool /* System.Boolean */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Int8 /* System.SByte */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(UInt8 /* System.Char */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(UInt8 /* System.Byte */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Int16 /* System.Int16 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(UInt16 /* System.UInt16 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Int32 /* System.Int32 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(UInt32 /* System.UInt32 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Int64 /* System.Int64 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(UInt64 /* System.UInt64 */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(Float /* System.Single */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(double /* System.Double */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_Decimal? /* System.Decimal */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_String? /* System.String */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_DateTime? /* System.DateTime */ value) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_Object? /* System.Object */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Bool /* System.Boolean */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(UInt8 /* System.Byte */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(UInt8 /* System.Char */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Int8 /* System.SByte */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Int16 /* System.Int16 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(UInt16 /* System.UInt16 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Int32 /* System.Int32 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(UInt32 /* System.UInt32 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Int64 /* System.Int64 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(UInt64 /* System.UInt64 */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(Float /* System.Single */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(double /* System.Double */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_Decimal? /* System.Decimal */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_String? /* System.String */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_DateTime? /* System.DateTime */ value) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_Object? /* System.Object */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Bool /* System.Boolean */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(UInt8 /* System.Char */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Int8 /* System.SByte */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(UInt8 /* System.Byte */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(UInt16 /* System.UInt16 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Int32 /* System.Int32 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(UInt32 /* System.UInt32 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Int16 /* System.Int16 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Int64 /* System.Int64 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(UInt64 /* System.UInt64 */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(Float /* System.Single */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(double /* System.Double */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_Decimal? /* System.Decimal */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_String? /* System.String */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_DateTime? /* System.DateTime */ value) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_Object? /* System.Object */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Bool /* System.Boolean */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(UInt8 /* System.Char */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Int8 /* System.SByte */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(UInt8 /* System.Byte */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Int16 /* System.Int16 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Int32 /* System.Int32 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(UInt16 /* System.UInt16 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(UInt32 /* System.UInt32 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Int64 /* System.Int64 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(UInt64 /* System.UInt64 */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(Float /* System.Single */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(double /* System.Double */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_Decimal? /* System.Decimal */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_String? /* System.String */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_DateTime? /* System.DateTime */ value) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Bool /* System.Boolean */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(UInt8 /* System.Char */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Int8 /* System.SByte */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(UInt8 /* System.Byte */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Int16 /* System.Int16 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(UInt16 /* System.UInt16 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(UInt32 /* System.UInt32 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Int32 /* System.Int32 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Int64 /* System.Int64 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(UInt64 /* System.UInt64 */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(Float /* System.Single */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(double /* System.Double */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_Decimal? /* System.Decimal */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_String? /* System.String */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_DateTime? /* System.DateTime */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_Object? /* System.Object */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Bool /* System.Boolean */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(UInt8 /* System.Char */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Int8 /* System.SByte */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(UInt8 /* System.Byte */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Int16 /* System.Int16 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(UInt16 /* System.UInt16 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Int32 /* System.Int32 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(UInt32 /* System.UInt32 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Int64 /* System.Int64 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(UInt64 /* System.UInt64 */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(Float /* System.Single */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(double /* System.Double */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_Decimal? /* System.Decimal */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_String? /* System.String */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_DateTime? /* System.DateTime */ value) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_Object? /* System.Object */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Bool /* System.Boolean */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(UInt8 /* System.Char */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Int8 /* System.SByte */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(UInt8 /* System.Byte */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Int16 /* System.Int16 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(UInt16 /* System.UInt16 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Int32 /* System.Int32 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(UInt32 /* System.UInt32 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(UInt64 /* System.UInt64 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Int64 /* System.Int64 */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(Float /* System.Single */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(double /* System.Double */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_Decimal? /* System.Decimal */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_String? /* System.String */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_DateTime? /* System.DateTime */ value) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_Object? /* System.Object */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Bool /* System.Boolean */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(UInt8 /* System.Char */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Int8 /* System.SByte */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(UInt8 /* System.Byte */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Int16 /* System.Int16 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(UInt16 /* System.UInt16 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Int32 /* System.Int32 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(UInt32 /* System.UInt32 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Int64 /* System.Int64 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(UInt64 /* System.UInt64 */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(Float /* System.Single */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(double /* System.Double */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_Decimal? /* System.Decimal */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_String? /* System.String */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_DateTime? /* System.DateTime */ value) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_Object? /* System.Object */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Int8 /* System.SByte */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(UInt8 /* System.Byte */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(UInt8 /* System.Char */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Int16 /* System.Int16 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(UInt16 /* System.UInt16 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Int32 /* System.Int32 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(UInt32 /* System.UInt32 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Int64 /* System.Int64 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(UInt64 /* System.UInt64 */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Float /* System.Single */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(double /* System.Double */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_Decimal? /* System.Decimal */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_String? /* System.String */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(Bool /* System.Boolean */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSingle(System_DateTime? /* System.DateTime */ value) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_Object? /* System.Object */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Int8 /* System.SByte */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(UInt8 /* System.Byte */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Int16 /* System.Int16 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(UInt8 /* System.Char */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(UInt16 /* System.UInt16 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Int32 /* System.Int32 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(UInt32 /* System.UInt32 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Int64 /* System.Int64 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(UInt64 /* System.UInt64 */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Float /* System.Single */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(double /* System.Double */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_Decimal? /* System.Decimal */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_String? /* System.String */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(Bool /* System.Boolean */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDouble(System_DateTime? /* System.DateTime */ value) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_Object? /* System.Object */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Int8 /* System.SByte */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(UInt8 /* System.Byte */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(UInt8 /* System.Char */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Int16 /* System.Int16 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(UInt16 /* System.UInt16 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Int32 /* System.Int32 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(UInt32 /* System.UInt32 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Int64 /* System.Int64 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(UInt64 /* System.UInt64 */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Float /* System.Single */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(double /* System.Double */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_String? /* System.String */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_Decimal? /* System.Decimal */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(Bool /* System.Boolean */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDecimal(System_DateTime? /* System.DateTime */ value) throws -> System_Decimal? /* System.Decimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_DateTime? /* System.DateTime */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_Object? /* System.Object */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_String? /* System.String */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Int8 /* System.SByte */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(UInt8 /* System.Byte */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Int16 /* System.Int16 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(UInt16 /* System.UInt16 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Int32 /* System.Int32 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(UInt32 /* System.UInt32 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Int64 /* System.Int64 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(UInt64 /* System.UInt64 */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Bool /* System.Boolean */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(UInt8 /* System.Char */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(Float /* System.Single */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(double /* System.Double */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toDateTime(System_Decimal? /* System.Decimal */ value) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_Object? /* System.Object */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_Object? /* System.Object */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Bool /* System.Boolean */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Bool /* System.Boolean */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt8 /* System.Char */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt8 /* System.Char */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int8 /* System.SByte */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int8 /* System.SByte */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt8 /* System.Byte */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt8 /* System.Byte */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int16 /* System.Int16 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int16 /* System.Int16 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt16 /* System.UInt16 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt16 /* System.UInt16 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int32 /* System.Int32 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int32 /* System.Int32 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt32 /* System.UInt32 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt32 /* System.UInt32 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int64 /* System.Int64 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int64 /* System.Int64 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt64 /* System.UInt64 */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt64 /* System.UInt64 */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Float /* System.Single */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Float /* System.Single */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(double /* System.Double */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(double /* System.Double */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_Decimal? /* System.Decimal */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_Decimal? /* System.Decimal */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_DateTime? /* System.DateTime */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_DateTime? /* System.DateTime */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_String? /* System.String */ value) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(System_String? /* System.String */ value, System_IFormatProvider? /* System.IFormatProvider */ provider) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toByte(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toSByte(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt16(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt16(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt32(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt32(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toInt64(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toUInt64(System_String? /* System.String */ value, Int32 /* System.Int32 */ fromBase) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(UInt8 /* System.Byte */ value, Int32 /* System.Int32 */ toBase) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int16 /* System.Int16 */ value, Int32 /* System.Int32 */ toBase) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int32 /* System.Int32 */ value, Int32 /* System.Int32 */ toBase) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toString(Int64 /* System.Int64 */ value, Int32 /* System.Int32 */ toBase) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64String(System_Byte_Array? /* System.Byte[] */ inArray) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64String(System_Byte_Array? /* System.Byte[] */ inArray, System_Base64FormattingOptions /* System.Base64FormattingOptions */ options) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64String(System_Byte_Array? /* System.Byte[] */ inArray, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ length) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64String(System_Byte_Array? /* System.Byte[] */ inArray, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ length, System_Base64FormattingOptions /* System.Base64FormattingOptions */ options) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64CharArray(System_Byte_Array? /* System.Byte[] */ inArray, Int32 /* System.Int32 */ offsetIn, Int32 /* System.Int32 */ length, System_Char_Array? /* System.Char[] */ outArray, Int32 /* System.Int32 */ offsetOut) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toBase64CharArray(System_Byte_Array? /* System.Byte[] */ inArray, Int32 /* System.Int32 */ offsetIn, Int32 /* System.Int32 */ length, System_Char_Array? /* System.Char[] */ outArray, Int32 /* System.Int32 */ offsetOut, System_Base64FormattingOptions /* System.Base64FormattingOptions */ options) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromBase64String(System_String? /* System.String */ s) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromBase64CharArray(System_Char_Array? /* System.Char[] */ inArray, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ length) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromHexString(System_String? /* System.String */ s) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toHexString(System_Byte_Array? /* System.Byte[] */ inArray) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func toHexString(System_Byte_Array? /* System.Byte[] */ inArray, Int32 /* System.Int32 */ offset, Int32 /* System.Int32 */ length) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDBNull() -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Convert_TypeOf())
		
	
	}
	
	deinit {
		System_Convert_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Thread /* System.Threading.Thread */ {
	let _handle: System_Threading_Thread_t

	required init(handle: System_Threading_Thread_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Thread_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func spinWait(Int32 /* System.Int32 */ iterations) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func yield() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getApartmentState() throws -> System_Threading_ApartmentState /* System.Threading.ApartmentState */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disableComObjectEagerCleanup() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func interrupt() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func join(Int32 /* System.Int32 */ millisecondsTimeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func start(System_Object? /* System.Object */ parameter) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeStart(System_Object? /* System.Object */ parameter) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func start() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func unsafeStart() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sleep(Int32 /* System.Int32 */ millisecondsTimeout) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func abort() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func abort(System_Object? /* System.Object */ stateInfo) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func resetAbort() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func suspend() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resume() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func beginCriticalRegion() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func endCriticalRegion() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func beginThreadAffinity() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func endThreadAffinity() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocateDataSlot() throws -> System_LocalDataStoreSlot? /* System.LocalDataStoreSlot */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func allocateNamedDataSlot(System_String? /* System.String */ name) throws -> System_LocalDataStoreSlot? /* System.LocalDataStoreSlot */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNamedDataSlot(System_String? /* System.String */ name) throws -> System_LocalDataStoreSlot? /* System.LocalDataStoreSlot */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func freeNamedDataSlot(System_String? /* System.String */ name) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getData(System_LocalDataStoreSlot? /* System.LocalDataStoreSlot */ slot) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setData(System_LocalDataStoreSlot? /* System.LocalDataStoreSlot */ slot, System_Object? /* System.Object */ data) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setApartmentState(System_Threading_ApartmentState /* System.Threading.ApartmentState */ state) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trySetApartmentState(System_Threading_ApartmentState /* System.Threading.ApartmentState */ state) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCompressedStack() throws -> System_Threading_CompressedStack? /* System.Threading.CompressedStack */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCompressedStack(System_Threading_CompressedStack? /* System.Threading.CompressedStack */ stack) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDomain() throws -> System_AppDomain? /* System.AppDomain */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDomainID() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func join() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func join(System_TimeSpan? /* System.TimeSpan */ timeout) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func memoryBarrier() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func sleep(System_TimeSpan? /* System.TimeSpan */ timeout) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout UInt8? /* System.Byte */ address) throws -> UInt8 /* System.Byte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout double? /* System.Double */ address) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Int16? /* System.Int16 */ address) throws -> Int16 /* System.Int16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Int32? /* System.Int32 */ address) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Int64? /* System.Int64 */ address) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Int? /* System.IntPtr */ address) throws -> Int /* System.IntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout System_Object? /* System.Object */ address) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Int8? /* System.SByte */ address) throws -> Int8 /* System.SByte */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout Float? /* System.Single */ address) throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout UInt16? /* System.UInt16 */ address) throws -> UInt16 /* System.UInt16 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout UInt32? /* System.UInt32 */ address) throws -> UInt32 /* System.UInt32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout UInt64? /* System.UInt64 */ address) throws -> UInt64 /* System.UInt64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileRead(inout UInt? /* System.UIntPtr */ address) throws -> UInt /* System.UIntPtr */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout UInt8? /* System.Byte */ address, UInt8 /* System.Byte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout double? /* System.Double */ address, double /* System.Double */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Int16? /* System.Int16 */ address, Int16 /* System.Int16 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Int32? /* System.Int32 */ address, Int32 /* System.Int32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Int64? /* System.Int64 */ address, Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Int? /* System.IntPtr */ address, Int /* System.IntPtr */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout System_Object? /* System.Object */ address, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Int8? /* System.SByte */ address, Int8 /* System.SByte */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout Float? /* System.Single */ address, Float /* System.Single */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout UInt16? /* System.UInt16 */ address, UInt16 /* System.UInt16 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout UInt32? /* System.UInt32 */ address, UInt32 /* System.UInt32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout UInt64? /* System.UInt64 */ address, UInt64 /* System.UInt64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func volatileWrite(inout UInt? /* System.UIntPtr */ address, UInt /* System.UIntPtr */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCurrentProcessorId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Threading_ThreadStart? /* System.Threading.ThreadStart */ start) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_ThreadStart? /* System.Threading.ThreadStart */ start, Int32 /* System.Int32 */ maxStackSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_ParameterizedThreadStart? /* System.Threading.ParameterizedThreadStart */ start) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_ParameterizedThreadStart? /* System.Threading.ParameterizedThreadStart */ start, Int32 /* System.Int32 */ maxStackSize) throws {
		// TODO: Constructor
		
	
	}
	
	func getManagedThreadId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAlive() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsBackground() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setIsBackground(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsThreadPoolThread() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPriority() throws -> System_Threading_ThreadPriority /* System.Threading.ThreadPriority */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPriority(System_Threading_ThreadPriority /* System.Threading.ThreadPriority */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getThreadState() throws -> System_Threading_ThreadState /* System.Threading.ThreadState */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrentCulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrentCulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrentUICulture() throws -> System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrentUICulture(System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentPrincipal() throws -> System_Security_Principal_IPrincipal? /* System.Security.Principal.IPrincipal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCurrentPrincipal(System_Security_Principal_IPrincipal? /* System.Security.Principal.IPrincipal */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCurrentThread() throws -> System_Threading_Thread? /* System.Threading.Thread */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExecutionContext() throws -> System_Threading_ExecutionContext? /* System.Threading.ExecutionContext */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getApartmentState() throws -> System_Threading_ApartmentState /* System.Threading.ApartmentState */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setApartmentState(System_Threading_ApartmentState /* System.Threading.ApartmentState */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Thread_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Thread_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Security_Principal_IPrincipal /* System.Security.Principal.IPrincipal */ {
	func isInRole(System_String? /* System.String */ role) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIdentity() throws -> System_Security_Principal_IIdentity? /* System.Security.Principal.IIdentity */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_Principal_IPrincipal_TypeOf())
		
	
	}
	
	deinit {
		System_Security_Principal_IPrincipal_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Security_Principal_IIdentity /* System.Security.Principal.IIdentity */ {
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAuthenticationType() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsAuthenticated() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_Principal_IIdentity_TypeOf())
		
	
	}
	
	deinit {
		System_Security_Principal_IIdentity_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_ExecutionContext /* System.Threading.ExecutionContext */ {
	let _handle: System_Threading_ExecutionContext_t

	required init(handle: System_Threading_ExecutionContext_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_ExecutionContext_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func capture() throws -> System_Threading_ExecutionContext? /* System.Threading.ExecutionContext */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func suppressFlow() throws -> System_Threading_AsyncFlowControl? /* System.Threading.AsyncFlowControl */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func restoreFlow() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isFlowSuppressed() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func run(System_Threading_ExecutionContext? /* System.Threading.ExecutionContext */ executionContext, System_Threading_ContextCallback? /* System.Threading.ContextCallback */ callback, System_Object? /* System.Object */ state) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func restore(System_Threading_ExecutionContext? /* System.Threading.ExecutionContext */ executionContext) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createCopy() throws -> System_Threading_ExecutionContext? /* System.Threading.ExecutionContext */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_ExecutionContext_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_ExecutionContext_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_AsyncFlowControl /* System.Threading.AsyncFlowControl */ {
	let _handle: System_Threading_AsyncFlowControl_t

	required init(handle: System_Threading_AsyncFlowControl_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_AsyncFlowControl_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func undo() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Threading_AsyncFlowControl? /* System.Threading.AsyncFlowControl */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_AsyncFlowControl_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_AsyncFlowControl_Destroy(self._handle)
		
	
	}
	
	

}


public class System_LocalDataStoreSlot /* System.LocalDataStoreSlot */ {
	let _handle: System_LocalDataStoreSlot_t

	required init(handle: System_LocalDataStoreSlot_t) {
		self._handle = handle
	}

	convenience init?(handle: System_LocalDataStoreSlot_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_LocalDataStoreSlot_TypeOf())
		
	
	}
	
	deinit {
		System_LocalDataStoreSlot_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_CompressedStack /* System.Threading.CompressedStack */ {
	let _handle: System_Threading_CompressedStack_t

	required init(handle: System_Threading_CompressedStack_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_CompressedStack_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func capture() throws -> System_Threading_CompressedStack? /* System.Threading.CompressedStack */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createCopy() throws -> System_Threading_CompressedStack? /* System.Threading.CompressedStack */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCompressedStack() throws -> System_Threading_CompressedStack? /* System.Threading.CompressedStack */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func run(System_Threading_CompressedStack? /* System.Threading.CompressedStack */ compressedStack, System_Threading_ContextCallback? /* System.Threading.ContextCallback */ callback, System_Object? /* System.Object */ state) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_CompressedStack_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_CompressedStack_Destroy(self._handle)
		
	
	}
	
	

}


public class System_AppDomain /* System.AppDomain */ {
	let _handle: System_AppDomain_t

	required init(handle: System_AppDomain_t) {
		self._handle = handle
	}

	convenience init?(handle: System_AppDomain_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func setDynamicBase(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func applyPolicy(System_String? /* System.String */ assemblyName) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createDomain(System_String? /* System.String */ friendlyName) throws -> System_AppDomain? /* System.AppDomain */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssembly(System_String? /* System.String */ assemblyFile) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssembly(System_String? /* System.String */ assemblyFile, System_String_Array? /* System.String[] */ args) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssembly(System_String? /* System.String */ assemblyFile, System_String_Array? /* System.String[] */ args, System_Byte_Array? /* System.Byte[] */ hashValue, System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */ hashAlgorithm) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssemblyByName(System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ assemblyName, System_String_Array? /* System.String[] */ args) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssemblyByName(System_String? /* System.String */ assemblyName) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func executeAssemblyByName(System_String? /* System.String */ assemblyName, System_String_Array? /* System.String[] */ args) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getData(System_String? /* System.String */ name) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setData(System_String? /* System.String */ name, System_Object? /* System.Object */ data) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isCompatibilitySwitchSet(System_String? /* System.String */ value) throws -> System_Nullable_A1? /* System.Nullable<System.Boolean> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isDefaultAppDomain() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isFinalizingForUnload() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func unload(System_AppDomain? /* System.AppDomain */ domain) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func load(System_Byte_Array? /* System.Byte[] */ rawAssembly) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func load(System_Byte_Array? /* System.Byte[] */ rawAssembly, System_Byte_Array? /* System.Byte[] */ rawSymbolStore) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func load(System_Reflection_AssemblyName? /* System.Reflection.AssemblyName */ assemblyRef) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func load(System_String? /* System.String */ assemblyString) throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reflectionOnlyGetAssemblies() throws -> System_Reflection_Assembly_Array? /* System.Reflection.Assembly[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCurrentThreadId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendPrivatePath(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clearPrivatePath() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clearShadowCopyPath() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCachePath(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShadowCopyFiles() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setShadowCopyPath(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getAssemblies() throws -> System_Reflection_Assembly_Array? /* System.Reflection.Assembly[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPrincipalPolicy(System_Security_Principal_PrincipalPolicy /* System.Security.Principal.PrincipalPolicy */ policy) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setThreadPrincipal(System_Security_Principal_IPrincipal? /* System.Security.Principal.IPrincipal */ principal) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstance(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceAndUnwrap(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceAndUnwrap(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceAndUnwrap(System_String? /* System.String */ assemblyName, System_String? /* System.String */ typeName, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFrom(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFrom(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFrom(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Runtime_Remoting_ObjectHandle? /* System.Runtime.Remoting.ObjectHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFromAndUnwrap(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFromAndUnwrap(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName, Bool /* System.Boolean */ ignoreCase, System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr, System_Reflection_Binder? /* System.Reflection.Binder */ binder, System_Object_Array? /* System.Object[] */ args, System_Globalization_CultureInfo? /* System.Globalization.CultureInfo */ culture, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createInstanceFromAndUnwrap(System_String? /* System.String */ assemblyFile, System_String? /* System.String */ typeName, System_Object_Array? /* System.Object[] */ activationAttributes) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCurrentDomain() throws -> System_AppDomain? /* System.AppDomain */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBaseDirectory() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRelativeSearchPath() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSetupInformation() throws -> System_AppDomainSetup? /* System.AppDomainSetup */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getPermissionSet() throws -> System_Security_PermissionSet? /* System.Security.PermissionSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDynamicDirectory() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFriendlyName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getId() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFullyTrusted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsHomogenous() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMonitoringIsEnabled() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setMonitoringIsEnabled(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonitoringSurvivedMemorySize() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getMonitoringSurvivedProcessMemorySize() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonitoringTotalAllocatedMemorySize() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getShadowCopyFiles() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMonitoringTotalProcessorTime() throws -> System_TimeSpan? /* System.TimeSpan */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addUnhandledException(System_UnhandledExceptionEventHandler? /* System.UnhandledExceptionEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeUnhandledException(System_UnhandledExceptionEventHandler? /* System.UnhandledExceptionEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addDomainUnload(System_EventHandler? /* System.EventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeDomainUnload(System_EventHandler? /* System.EventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addProcessExit(System_EventHandler? /* System.EventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeProcessExit(System_EventHandler? /* System.EventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addAssemblyLoad(System_AssemblyLoadEventHandler? /* System.AssemblyLoadEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAssemblyLoad(System_AssemblyLoadEventHandler? /* System.AssemblyLoadEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addAssemblyResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAssemblyResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addReflectionOnlyAssemblyResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeReflectionOnlyAssemblyResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addTypeResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeTypeResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addResourceResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeResourceResolve(System_ResolveEventHandler? /* System.ResolveEventHandler */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_AppDomain_TypeOf())
		
	
	}
	
	deinit {
		System_AppDomain_Destroy(self._handle)
		
	
	}
	
	

}


public class System_AppDomainSetup /* System.AppDomainSetup */ {
	let _handle: System_AppDomainSetup_t

	required init(handle: System_AppDomainSetup_t) {
		self._handle = handle
	}

	convenience init?(handle: System_AppDomainSetup_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getApplicationBase() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getTargetFrameworkName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_AppDomainSetup_TypeOf())
		
	
	}
	
	deinit {
		System_AppDomainSetup_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Security_PermissionSet /* System.Security.PermissionSet */ {
	let _handle: System_Security_PermissionSet_t

	required init(handle: System_Security_PermissionSet_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Security_PermissionSet_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func addPermission(System_Security_IPermission? /* System.Security.IPermission */ perm) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func assert() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func containsNonCodeAccessPermissions() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func convertPermissionSet(System_String? /* System.String */ inFormat, System_Byte_Array? /* System.Byte[] */ inData, System_String? /* System.String */ outFormat) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copy() throws -> System_Security_PermissionSet? /* System.Security.PermissionSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func demand() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func deny() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Object? /* System.Object */ o) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func fromXml(System_Security_SecurityElement? /* System.Security.SecurityElement */ et) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Collections_IEnumerator? /* System.Collections.IEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getPermission(System_Type? /* System.Type */ permClass) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func intersect(System_Security_PermissionSet? /* System.Security.PermissionSet */ other) throws -> System_Security_PermissionSet? /* System.Security.PermissionSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isEmpty() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isSubsetOf(System_Security_PermissionSet? /* System.Security.PermissionSet */ target) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isUnrestricted() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func permitOnly() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removePermission(System_Type? /* System.Type */ permClass) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func revertAssert() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setPermission(System_Security_IPermission? /* System.Security.IPermission */ perm) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toXml() throws -> System_Security_SecurityElement? /* System.Security.SecurityElement */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func union(System_Security_PermissionSet? /* System.Security.PermissionSet */ other) throws -> System_Security_PermissionSet? /* System.Security.PermissionSet */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Security_Permissions_PermissionState /* System.Security.Permissions.PermissionState */ state) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Security_PermissionSet? /* System.Security.PermissionSet */ permSet) throws {
		// TODO: Constructor
		
	
	}
	
	func getCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSynchronized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSyncRoot() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_PermissionSet_TypeOf())
		
	
	}
	
	deinit {
		System_Security_PermissionSet_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Security_IPermission /* System.Security.IPermission */ {
	func copy() throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func demand() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func intersect(System_Security_IPermission? /* System.Security.IPermission */ target) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func isSubsetOf(System_Security_IPermission? /* System.Security.IPermission */ target) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func union(System_Security_IPermission? /* System.Security.IPermission */ target) throws -> System_Security_IPermission? /* System.Security.IPermission */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_IPermission_TypeOf())
		
	
	}
	
	deinit {
		System_Security_IPermission_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Security_SecurityElement /* System.Security.SecurityElement */ {
	let _handle: System_Security_SecurityElement_t

	required init(handle: System_Security_SecurityElement_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Security_SecurityElement_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func addAttribute(System_String? /* System.String */ name, System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addChild(System_Security_SecurityElement? /* System.Security.SecurityElement */ child) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equal(System_Security_SecurityElement? /* System.Security.SecurityElement */ other) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copy() throws -> System_Security_SecurityElement? /* System.Security.SecurityElement */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValidTag(System_String? /* System.String */ tag) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValidText(System_String? /* System.String */ text) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValidAttributeName(System_String? /* System.String */ name) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isValidAttributeValue(System_String? /* System.String */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func escape(System_String? /* System.String */ str) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func attribute(System_String? /* System.String */ name) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func searchForChildByTag(System_String? /* System.String */ tag) throws -> System_Security_SecurityElement? /* System.Security.SecurityElement */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func searchForTextOfTag(System_String? /* System.String */ tag) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fromString(System_String? /* System.String */ xml) throws -> System_Security_SecurityElement? /* System.Security.SecurityElement */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ tag) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ tag, System_String? /* System.String */ text) throws {
		// TODO: Constructor
		
	
	}
	
	func getTag() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setTag(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_Collections_Hashtable? /* System.Collections.Hashtable */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAttributes(System_Collections_Hashtable? /* System.Collections.Hashtable */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getText() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setText(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getChildren() throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setChildren(System_Collections_ArrayList? /* System.Collections.ArrayList */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Security_SecurityElement_TypeOf())
		
	
	}
	
	deinit {
		System_Security_SecurityElement_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_Hashtable /* System.Collections.Hashtable */ {
	let _handle: System_Collections_Hashtable_t

	required init(handle: System_Collections_Hashtable_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Collections_Hashtable_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_Object? /* System.Object */ key) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func containsKey(System_Object? /* System.Object */ key) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func containsValue(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ arrayIndex) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Collections_IDictionaryEnumerator? /* System.Collections.IDictionaryEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Object? /* System.Object */ key) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_Collections_Hashtable? /* System.Collections.Hashtable */ table) throws -> System_Collections_Hashtable? /* System.Collections.Hashtable */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func onDeserialization(System_Object? /* System.Object */ sender) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, Float /* System.Single */ loadFactor) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, Float /* System.Single */ loadFactor, System_Collections_IEqualityComparer? /* System.Collections.IEqualityComparer */ equalityComparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IHashCodeProvider? /* System.Collections.IHashCodeProvider */ hcp, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IEqualityComparer? /* System.Collections.IEqualityComparer */ equalityComparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, System_Collections_IHashCodeProvider? /* System.Collections.IHashCodeProvider */ hcp, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, System_Collections_IEqualityComparer? /* System.Collections.IEqualityComparer */ equalityComparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d, Float /* System.Single */ loadFactor) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d, System_Collections_IHashCodeProvider? /* System.Collections.IHashCodeProvider */ hcp, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d, System_Collections_IEqualityComparer? /* System.Collections.IEqualityComparer */ equalityComparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, Float /* System.Single */ loadFactor, System_Collections_IHashCodeProvider? /* System.Collections.IHashCodeProvider */ hcp, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d, Float /* System.Single */ loadFactor, System_Collections_IHashCodeProvider? /* System.Collections.IHashCodeProvider */ hcp, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_IDictionary? /* System.Collections.IDictionary */ d, Float /* System.Single */ loadFactor, System_Collections_IEqualityComparer? /* System.Collections.IEqualityComparer */ equalityComparer) throws {
		// TODO: Constructor
		
	
	}
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFixedSize() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSynchronized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getKeys() throws -> System_Collections_ICollection? /* System.Collections.ICollection */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValues() throws -> System_Collections_ICollection? /* System.Collections.ICollection */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSyncRoot() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_Hashtable_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_Hashtable_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_IEqualityComparer /* System.Collections.IEqualityComparer */ {
	func equals(System_Object? /* System.Object */ x, System_Object? /* System.Object */ y) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_Object? /* System.Object */ obj) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IEqualityComparer_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IEqualityComparer_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_IHashCodeProvider /* System.Collections.IHashCodeProvider */ {
	func getHashCode(System_Object? /* System.Object */ obj) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IHashCodeProvider_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IHashCodeProvider_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_ArrayList /* System.Collections.ArrayList */ {
	let _handle: System_Collections_ArrayList_t

	required init(handle: System_Collections_ArrayList_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Collections_ArrayList_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	static func adapter(System_Collections_IList? /* System.Collections.IList */ list) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func add(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addRange(System_Collections_ICollection? /* System.Collections.ICollection */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func binarySearch(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count, System_Object? /* System.Object */ value, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func binarySearch(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func binarySearch(System_Object? /* System.Object */ value, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clone() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_Object? /* System.Object */ item) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Array? /* System.Array */ array, Int32 /* System.Int32 */ arrayIndex) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(Int32 /* System.Int32 */ index, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ arrayIndex, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fixedSize(System_Collections_IList? /* System.Collections.IList */ list) throws -> System_Collections_IList? /* System.Collections.IList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func fixedSize(System_Collections_ArrayList? /* System.Collections.ArrayList */ list) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator() throws -> System_Collections_IEnumerator? /* System.Collections.IEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getEnumerator(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Collections_IEnumerator? /* System.Collections.IEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insertRange(Int32 /* System.Int32 */ index, System_Collections_ICollection? /* System.Collections.ICollection */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Collections_IList? /* System.Collections.IList */ list) throws -> System_Collections_IList? /* System.Collections.IList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readOnly(System_Collections_ArrayList? /* System.Collections.ArrayList */ list) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Object? /* System.Object */ obj) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAt(Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeRange(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func repeat(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ count) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reverse() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reverse(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setRange(Int32 /* System.Int32 */ index, System_Collections_ICollection? /* System.Collections.ICollection */ c) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRange(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func sort() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func sort(System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func sort(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count, System_Collections_IComparer? /* System.Collections.IComparer */ comparer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_Collections_IList? /* System.Collections.IList */ list) throws -> System_Collections_IList? /* System.Collections.IList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_Collections_ArrayList? /* System.Collections.ArrayList */ list) throws -> System_Collections_ArrayList? /* System.Collections.ArrayList */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toArray() throws -> System_Object_Array? /* System.Object[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toArray(System_Type? /* System.Type */ type) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimToSize() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Collections_ICollection? /* System.Collections.ICollection */ c) throws {
		// TODO: Constructor
		
	
	}
	
	func getCapacity() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCapacity(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCount() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFixedSize() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsSynchronized() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getSyncRoot() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_ArrayList_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_ArrayList_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_IList /* System.Collections.IList */ {
	func add(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Object? /* System.Object */ value) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAt(Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsFixedSize() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_IList_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_IList_Destroy(self._handle)
		
	
	}
	
	

}


public class System_UnhandledExceptionEventArgs /* System.UnhandledExceptionEventArgs */ {
	let _handle: System_UnhandledExceptionEventArgs_t

	required init(handle: System_UnhandledExceptionEventArgs_t) {
		self._handle = handle
	}

	convenience init?(handle: System_UnhandledExceptionEventArgs_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_Object? /* System.Object */ exception, Bool /* System.Boolean */ isTerminating) throws {
		// TODO: Constructor
		
	
	}
	
	func getExceptionObject() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsTerminating() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_UnhandledExceptionEventArgs_TypeOf())
		
	
	}
	
	deinit {
		System_UnhandledExceptionEventArgs_Destroy(self._handle)
		
	
	}
	
	

}




public class System_Reflection_Assembly_Array /* System.Reflection.Assembly[] */ {
	let _handle: System_Reflection_Assembly_Array_t

	required init(handle: System_Reflection_Assembly_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Reflection_Assembly_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_AssemblyLoadEventArgs /* System.AssemblyLoadEventArgs */ {
	let _handle: System_AssemblyLoadEventArgs_t

	required init(handle: System_AssemblyLoadEventArgs_t) {
		self._handle = handle
	}

	convenience init?(handle: System_AssemblyLoadEventArgs_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_Reflection_Assembly? /* System.Reflection.Assembly */ loadedAssembly) throws {
		// TODO: Constructor
		
	
	}
	
	func getLoadedAssembly() throws -> System_Reflection_Assembly? /* System.Reflection.Assembly */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_AssemblyLoadEventArgs_TypeOf())
		
	
	}
	
	deinit {
		System_AssemblyLoadEventArgs_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Runtime_Remoting_ObjectHandle /* System.Runtime.Remoting.ObjectHandle */ {
	let _handle: System_Runtime_Remoting_ObjectHandle_t

	required init(handle: System_Runtime_Remoting_ObjectHandle_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Runtime_Remoting_ObjectHandle_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func unwrap() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Object? /* System.Object */ o) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Runtime_Remoting_ObjectHandle_TypeOf())
		
	
	}
	
	deinit {
		System_Runtime_Remoting_ObjectHandle_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_Timer /* System.Threading.Timer */ {
	let _handle: System_Threading_Timer_t

	required init(handle: System_Threading_Timer_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_Timer_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func change(Int32 /* System.Int32 */ dueTime, Int32 /* System.Int32 */ period) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func change(System_TimeSpan? /* System.TimeSpan */ dueTime, System_TimeSpan? /* System.TimeSpan */ period) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func change(UInt32 /* System.UInt32 */ dueTime, UInt32 /* System.UInt32 */ period) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func change(Int64 /* System.Int64 */ dueTime, Int64 /* System.Int64 */ period) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose(System_Threading_WaitHandle? /* System.Threading.WaitHandle */ notifyObject) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Threading_TimerCallback? /* System.Threading.TimerCallback */ callback, System_Object? /* System.Object */ state, Int32 /* System.Int32 */ dueTime, Int32 /* System.Int32 */ period) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_TimerCallback? /* System.Threading.TimerCallback */ callback, System_Object? /* System.Object */ state, System_TimeSpan? /* System.TimeSpan */ dueTime, System_TimeSpan? /* System.TimeSpan */ period) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_TimerCallback? /* System.Threading.TimerCallback */ callback, System_Object? /* System.Object */ state, UInt32 /* System.UInt32 */ dueTime, UInt32 /* System.UInt32 */ period) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_TimerCallback? /* System.Threading.TimerCallback */ callback, System_Object? /* System.Object */ state, Int64 /* System.Int64 */ dueTime, Int64 /* System.Int64 */ period) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Threading_TimerCallback? /* System.Threading.TimerCallback */ callback) throws {
		// TODO: Constructor
		
	
	}
	
	static func getActiveCount() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_Timer_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_Timer_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_CancellationTokenSource /* System.Threading.CancellationTokenSource */ {
	let _handle: System_Threading_CancellationTokenSource_t

	required init(handle: System_Threading_CancellationTokenSource_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_CancellationTokenSource_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func cancel() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func cancel(Bool /* System.Boolean */ throwOnFirstException) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func cancelAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func cancelAfter(System_TimeSpan? /* System.TimeSpan */ delay) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func cancelAfter(Int32 /* System.Int32 */ millisecondsDelay) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func tryReset() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createLinkedTokenSource(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ token1, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ token2) throws -> System_Threading_CancellationTokenSource? /* System.Threading.CancellationTokenSource */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createLinkedTokenSource(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ token) throws -> System_Threading_CancellationTokenSource? /* System.Threading.CancellationTokenSource */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createLinkedTokenSource(System_Threading_CancellationToken_Array? /* System.Threading.CancellationToken[] */ tokens) throws -> System_Threading_CancellationTokenSource? /* System.Threading.CancellationTokenSource */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_TimeSpan? /* System.TimeSpan */ delay) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ millisecondsDelay) throws {
		// TODO: Constructor
		
	
	}
	
	func getIsCancellationRequested() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getToken() throws -> System_Threading_CancellationToken? /* System.Threading.CancellationToken */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Threading_CancellationTokenSource_TypeOf())
		
	
	}
	
	deinit {
		System_Threading_CancellationTokenSource_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Threading_CancellationToken_Array /* System.Threading.CancellationToken[] */ {
	let _handle: System_Threading_CancellationToken_Array_t

	required init(handle: System_Threading_CancellationToken_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Threading_CancellationToken_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_Random /* System.Random */ {
	let _handle: System_Random_t

	required init(handle: System_Random_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Random_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func next() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func next(Int32 /* System.Int32 */ maxValue) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func next(Int32 /* System.Int32 */ minValue, Int32 /* System.Int32 */ maxValue) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextInt64() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextInt64(Int64 /* System.Int64 */ maxValue) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextInt64(Int64 /* System.Int64 */ minValue, Int64 /* System.Int64 */ maxValue) throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextSingle() throws -> Float /* System.Single */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextDouble() throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func nextBytes(System_Byte_Array? /* System.Byte[] */ buffer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getItems(System_Type? /* System.Type */ T, System_Array? /* System.Array */ choices, Int32 /* System.Int32 */ length) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func shuffle(System_Type? /* System.Type */ T, System_Array? /* System.Array */ values) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ Seed) throws {
		// TODO: Constructor
		
	
	}
	
	static func getShared() throws -> System_Random? /* System.Random */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Random_TypeOf())
		
	
	}
	
	deinit {
		System_Random_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

public class System_Text_StringBuilder /* System.Text.StringBuilder */ {
	let _handle: System_Text_StringBuilder_t

	required init(handle: System_Text_StringBuilder_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_StringBuilder_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func ensureCapacity(Int32 /* System.Int32 */ capacity) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear() throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getChunks() throws -> System_Text_StringBuilder_ChunkEnumerator? /* System.Text.StringBuilder.ChunkEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt8 /* System.Char */ value, Int32 /* System.Int32 */ repeatCount) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Char_Array? /* System.Char[] */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ charCount) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_String? /* System.String */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Text_StringBuilder? /* System.Text.StringBuilder */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Text_StringBuilder? /* System.Text.StringBuilder */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendLine() throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendLine(System_String? /* System.String */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(Int32 /* System.Int32 */ sourceIndex, System_Char_Array? /* System.Char[] */ destination, Int32 /* System.Int32 */ destinationIndex, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_String? /* System.String */ value, Int32 /* System.Int32 */ count) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Bool /* System.Boolean */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt8 /* System.Char */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Int8 /* System.SByte */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt8 /* System.Byte */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Int16 /* System.Int16 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Int32 /* System.Int32 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Int64 /* System.Int64 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(Float /* System.Single */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(double /* System.Double */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Decimal? /* System.Decimal */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt16 /* System.UInt16 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt32 /* System.UInt32 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(UInt64 /* System.UInt64 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Object? /* System.Object */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_Char_Array? /* System.Char[] */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(inout System_Text_StringBuilder_AppendInterpolatedStringHandler? /* System.Text.StringBuilder.AppendInterpolatedStringHandler */ handler) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func append(System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_Text_StringBuilder_AppendInterpolatedStringHandler? /* System.Text.StringBuilder.AppendInterpolatedStringHandler */ handler) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendLine(inout System_Text_StringBuilder_AppendInterpolatedStringHandler? /* System.Text.StringBuilder.AppendInterpolatedStringHandler */ handler) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendLine(System_IFormatProvider? /* System.IFormatProvider */ provider, inout System_Text_StringBuilder_AppendInterpolatedStringHandler? /* System.Text.StringBuilder.AppendInterpolatedStringHandler */ handler) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendJoin(System_String? /* System.String */ separator, System_Object_Array? /* System.Object[] */ values) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendJoin(System_String? /* System.String */ separator, System_String_Array? /* System.String[] */ values) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendJoin(UInt8 /* System.Char */ separator, System_Object_Array? /* System.Object[] */ values) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendJoin(UInt8 /* System.Char */ separator, System_String_Array? /* System.String[] */ values) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_String? /* System.String */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Bool /* System.Boolean */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Int8 /* System.SByte */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, UInt8 /* System.Byte */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Int16 /* System.Int16 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, UInt8 /* System.Char */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Char_Array? /* System.Char[] */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Char_Array? /* System.Char[] */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ charCount) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Int64 /* System.Int64 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, Float /* System.Single */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, double /* System.Double */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Decimal? /* System.Decimal */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, UInt16 /* System.UInt16 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, UInt32 /* System.UInt32 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, UInt64 /* System.UInt64 */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(Int32 /* System.Int32 */ index, System_Object? /* System.Object */ value) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_IFormatProvider? /* System.IFormatProvider */ provider, System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_Type? /* System.Type */ TArg0, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_Type? /* System.Type */ TArg0, System_Type? /* System.Type */ TArg1, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_Type? /* System.Type */ TArg0, System_Type? /* System.Type */ TArg1, System_Type? /* System.Type */ TArg2, System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormat(System_IFormatProvider? /* System.IFormatProvider */ provider, System_Text_CompositeFormat? /* System.Text.CompositeFormat */ format, System_Object_Array? /* System.Object[] */ args) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ oldValue, System_String? /* System.String */ newValue) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func equals(System_Text_StringBuilder? /* System.Text.StringBuilder */ sb) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ oldValue, System_String? /* System.String */ newValue, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(UInt8 /* System.Char */ oldChar, UInt8 /* System.Char */ newChar) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(UInt8 /* System.Char */ oldChar, UInt8 /* System.Char */ newChar, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ count) throws -> System_Text_StringBuilder? /* System.Text.StringBuilder */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ value) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ value, Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ value, Int32 /* System.Int32 */ startIndex, Int32 /* System.Int32 */ length, Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ capacity, Int32 /* System.Int32 */ maxCapacity) throws {
		// TODO: Constructor
		
	
	}
	
	func getCapacity() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCapacity(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxCapacity() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLength() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLength(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_StringBuilder_TypeOf())
		
	
	}
	
	deinit {
		System_Text_StringBuilder_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Text_StringBuilder_ChunkEnumerator /* System.Text.StringBuilder.ChunkEnumerator */ {
	let _handle: System_Text_StringBuilder_ChunkEnumerator_t

	required init(handle: System_Text_StringBuilder_ChunkEnumerator_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_StringBuilder_ChunkEnumerator_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getEnumerator() throws -> System_Text_StringBuilder_ChunkEnumerator? /* System.Text.StringBuilder.ChunkEnumerator */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveNext() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrent() throws -> System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_StringBuilder_ChunkEnumerator_TypeOf())
		
	
	}
	
	deinit {
		System_Text_StringBuilder_ChunkEnumerator_Destroy(self._handle)
		
	
	}
	
	

}










public class System_Text_StringBuilder_AppendInterpolatedStringHandler /* System.Text.StringBuilder.AppendInterpolatedStringHandler */ {
	let _handle: System_Text_StringBuilder_AppendInterpolatedStringHandler_t

	required init(handle: System_Text_StringBuilder_AppendInterpolatedStringHandler_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Text_StringBuilder_AppendInterpolatedStringHandler_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func appendLiteral(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_Type? /* System.Type */ T, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_Type? /* System.Type */ T, System_Object? /* System.Object */ value, System_String? /* System.String */ format) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_Type? /* System.Type */ T, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ alignment) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_Type? /* System.Type */ T, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ alignment, System_String? /* System.String */ format) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_String? /* System.String */ value, Int32 /* System.Int32 */ alignment, System_String? /* System.String */ format) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendFormatted(System_Object? /* System.Object */ value, Int32 /* System.Int32 */ alignment, System_String? /* System.String */ format) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ literalLength, Int32 /* System.Int32 */ formattedCount, System_Text_StringBuilder? /* System.Text.StringBuilder */ stringBuilder) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(Int32 /* System.Int32 */ literalLength, Int32 /* System.Int32 */ formattedCount, System_Text_StringBuilder? /* System.Text.StringBuilder */ stringBuilder, System_IFormatProvider? /* System.IFormatProvider */ provider) throws {
		// TODO: Constructor
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Text_StringBuilder_AppendInterpolatedStringHandler_TypeOf())
		
	
	}
	
	deinit {
		System_Text_StringBuilder_AppendInterpolatedStringHandler_Destroy(self._handle)
		
	
	}
	
	

}


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
public class System_IO_Path /* System.IO.Path */ {
	static func changeExtension(System_String? /* System.String */ path, System_String? /* System.String */ extension) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func exists(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectoryName(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getExtension(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileName(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileNameWithoutExtension(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getRandomFileName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isPathFullyQualified(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func hasExtension(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func combine(System_String? /* System.String */ path1, System_String? /* System.String */ path2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func combine(System_String? /* System.String */ path1, System_String? /* System.String */ path2, System_String? /* System.String */ path3) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func combine(System_String? /* System.String */ path1, System_String? /* System.String */ path2, System_String? /* System.String */ path3, System_String? /* System.String */ path4) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func combine(System_String_Array? /* System.String[] */ paths) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ path1, System_String? /* System.String */ path2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ path1, System_String? /* System.String */ path2, System_String? /* System.String */ path3) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String? /* System.String */ path1, System_String? /* System.String */ path2, System_String? /* System.String */ path3, System_String? /* System.String */ path4) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func join(System_String_Array? /* System.String[] */ paths) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getRelativePath(System_String? /* System.String */ relativeTo, System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func trimEndingDirectorySeparator(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func endsInDirectorySeparator(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getInvalidFileNameChars() throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getInvalidPathChars() throws -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFullPath(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFullPath(System_String? /* System.String */ path, System_String? /* System.String */ basePath) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTempPath() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTempFileName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func isPathRooted(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getPathRoot(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectorySeparatorChar() -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getAltDirectorySeparatorChar() -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getVolumeSeparatorChar() -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getPathSeparator() -> UInt8 /* System.Char */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getInvalidPathChars() -> System_Char_Array? /* System.Char[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_Path_TypeOf())
		
	
	}
	
	deinit {
		System_IO_Path_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_File /* System.IO.File */ {
	static func openText(System_String? /* System.String */ path) throws -> System_IO_StreamReader? /* System.IO.StreamReader */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createText(System_String? /* System.String */ path) throws -> System_IO_StreamWriter? /* System.IO.StreamWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendText(System_String? /* System.String */ path) throws -> System_IO_StreamWriter? /* System.IO.StreamWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destFileName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func copy(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destFileName, Bool /* System.Boolean */ overwrite) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func create(System_String? /* System.String */ path) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func create(System_String? /* System.String */ path, Int32 /* System.Int32 */ bufferSize) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func create(System_String? /* System.String */ path, Int32 /* System.Int32 */ bufferSize, System_IO_FileOptions /* System.IO.FileOptions */ options) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delete(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func exists(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func open(System_String? /* System.String */ path, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func open(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func open(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func open(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func openHandle(System_String? /* System.String */ path, System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share, System_IO_FileOptions /* System.IO.FileOptions */ options, Int64 /* System.Int64 */ preallocationSize) throws -> Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ creationTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ creationTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ creationTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ creationTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastAccessTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ lastAccessTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastAccessTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ lastAccessTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastWriteTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ lastWriteTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastWriteTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_DateTime? /* System.DateTime */ lastWriteTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTime(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTimeUtc(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAttributes(System_String? /* System.String */ path) throws -> System_IO_FileAttributes /* System.IO.FileAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAttributes(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_IO_FileAttributes /* System.IO.FileAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setAttributes(System_String? /* System.String */ path, System_IO_FileAttributes /* System.IO.FileAttributes */ fileAttributes) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setAttributes(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_IO_FileAttributes /* System.IO.FileAttributes */ fileAttributes) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getUnixFileMode(System_String? /* System.String */ path) throws -> System_IO_UnixFileMode /* System.IO.UnixFileMode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getUnixFileMode(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle) throws -> System_IO_UnixFileMode /* System.IO.UnixFileMode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setUnixFileMode(System_String? /* System.String */ path, System_IO_UnixFileMode /* System.IO.UnixFileMode */ mode) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setUnixFileMode(Microsoft_Win32_SafeHandles_SafeFileHandle? /* Microsoft.Win32.SafeHandles.SafeFileHandle */ fileHandle, System_IO_UnixFileMode /* System.IO.UnixFileMode */ mode) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func openRead(System_String? /* System.String */ path) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func openWrite(System_String? /* System.String */ path) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllText(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllText(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllText(System_String? /* System.String */ path, System_String? /* System.String */ contents) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllText(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllBytes(System_String? /* System.String */ path) throws -> System_Byte_Array? /* System.Byte[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllBytes(System_String? /* System.String */ path, System_Byte_Array? /* System.Byte[] */ bytes) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllLines(System_String? /* System.String */ path) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllLines(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readLines(System_String? /* System.String */ path) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readLines(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readLinesAsync(System_String? /* System.String */ path, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Collections_Generic_IAsyncEnumerable_A1? /* System.Collections.Generic.IAsyncEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readLinesAsync(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Collections_Generic_IAsyncEnumerable_A1? /* System.Collections.Generic.IAsyncEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLines(System_String? /* System.String */ path, System_String_Array? /* System.String[] */ contents) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLines(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLines(System_String? /* System.String */ path, System_String_Array? /* System.String[] */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLines(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllText(System_String? /* System.String */ path, System_String? /* System.String */ contents) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllText(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllLines(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllLines(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func replace(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destinationFileName, System_String? /* System.String */ destinationBackupFileName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func replace(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destinationFileName, System_String? /* System.String */ destinationBackupFileName, Bool /* System.Boolean */ ignoreMetadataErrors) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func move(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destFileName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func move(System_String? /* System.String */ sourceFileName, System_String? /* System.String */ destFileName, Bool /* System.Boolean */ overwrite) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func encrypt(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func decrypt(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllTextAsync(System_String? /* System.String */ path, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllTextAsync(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllTextAsync(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllTextAsync(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllBytesAsync(System_String? /* System.String */ path, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Byte[]> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllBytesAsync(System_String? /* System.String */ path, System_Byte_Array? /* System.Byte[] */ bytes, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllLinesAsync(System_String? /* System.String */ path, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String[]> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func readAllLinesAsync(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String[]> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLinesAsync(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func writeAllLinesAsync(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllTextAsync(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllTextAsync(System_String? /* System.String */ path, System_String? /* System.String */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllLinesAsync(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func appendAllLinesAsync(System_String? /* System.String */ path, System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ contents, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createSymbolicLink(System_String? /* System.String */ path, System_String? /* System.String */ pathToTarget) throws -> System_IO_FileSystemInfo? /* System.IO.FileSystemInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func resolveLinkTarget(System_String? /* System.String */ linkPath, Bool /* System.Boolean */ returnFinalTarget) throws -> System_IO_FileSystemInfo? /* System.IO.FileSystemInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_File_TypeOf())
		
	
	}
	
	deinit {
		System_IO_File_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_StreamReader /* System.IO.StreamReader */ {
	let _handle: System_IO_StreamReader_t

	required init(handle: System_IO_StreamReader_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_StreamReader_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func discardBufferedData() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func peek() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEnd() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlock(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLine() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLineAsync() throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLineAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEndAsync() throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEndAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Memory_A1? /* System.Memory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlockAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlockAsync(System_Memory_A1? /* System.Memory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks, Int32 /* System.Int32 */ bufferSize, Bool /* System.Boolean */ leaveOpen) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, Bool /* System.Boolean */ detectEncodingFromByteOrderMarks, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	func getCurrentEncoding() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBaseStream() throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEndOfStream() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNull() -> System_IO_StreamReader? /* System.IO.StreamReader */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_StreamReader_TypeOf())
		
	
	}
	
	deinit {
		System_IO_StreamReader_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_TextReader /* System.IO.TextReader */ {
	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func peek() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func read(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEnd() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlock(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLine() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLineAsync() throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readLineAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEndAsync() throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readToEndAsync(System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readAsync(System_Memory_A1? /* System.Memory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlockAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task_A1? /* System.Threading.Tasks.Task<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func readBlockAsync(System_Memory_A1? /* System.Memory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_ValueTask_A1? /* System.Threading.Tasks.ValueTask<System.Int32> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_IO_TextReader? /* System.IO.TextReader */ reader) throws -> System_IO_TextReader? /* System.IO.TextReader */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getNull() -> System_IO_TextReader? /* System.IO.TextReader */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_TextReader_TypeOf())
		
	
	}
	
	deinit {
		System_IO_TextReader_Destroy(self._handle)
		
	
	}
	
	

}






















public class System_IO_StreamWriter /* System.IO.StreamWriter */ {
	let _handle: System_IO_StreamWriter_t

	required init(handle: System_IO_StreamWriter_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_StreamWriter_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flush() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(UInt8 /* System.Char */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Char_Array? /* System.Char[] */ buffer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ arg) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ arg) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(UInt8 /* System.Char */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_String? /* System.String */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(UInt8 /* System.Char */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_String? /* System.String */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flushAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_IO_Stream? /* System.IO.Stream */ stream, System_Text_Encoding? /* System.Text.Encoding */ encoding, Int32 /* System.Int32 */ bufferSize, Bool /* System.Boolean */ leaveOpen) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, Bool /* System.Boolean */ append) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, Bool /* System.Boolean */ append, System_Text_Encoding? /* System.Text.Encoding */ encoding) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, Bool /* System.Boolean */ append, System_Text_Encoding? /* System.Text.Encoding */ encoding, Int32 /* System.Int32 */ bufferSize) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ path, System_Text_Encoding? /* System.Text.Encoding */ encoding, System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws {
		// TODO: Constructor
		
	
	}
	
	func getAutoFlush() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAutoFlush(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBaseStream() throws -> System_IO_Stream? /* System.IO.Stream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEncoding() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNull() -> System_IO_StreamWriter? /* System.IO.StreamWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_StreamWriter_TypeOf())
		
	
	}
	
	deinit {
		System_IO_StreamWriter_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_TextWriter /* System.IO.TextWriter */ {
	func close() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func dispose() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func disposeAsync() throws -> System_Threading_Tasks_ValueTask? /* System.Threading.Tasks.ValueTask */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flush() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(UInt8 /* System.Char */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Char_Array? /* System.Char[] */ buffer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(Bool /* System.Boolean */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(Int32 /* System.Int32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(UInt32 /* System.UInt32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(UInt64 /* System.UInt64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(Float /* System.Single */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(double /* System.Double */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Decimal? /* System.Decimal */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_Text_StringBuilder? /* System.Text.StringBuilder */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func write(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ arg) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(UInt8 /* System.Char */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_Char_Array? /* System.Char[] */ buffer) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(Bool /* System.Boolean */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(Int32 /* System.Int32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(UInt32 /* System.UInt32 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(Int64 /* System.Int64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(UInt64 /* System.UInt64 */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(Float /* System.Single */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(double /* System.Double */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_Decimal? /* System.Decimal */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_Text_StringBuilder? /* System.Text.StringBuilder */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object? /* System.Object */ arg0, System_Object? /* System.Object */ arg1, System_Object? /* System.Object */ arg2) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLine(System_String? /* System.String */ format, System_Object_Array? /* System.Object[] */ arg) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(UInt8 /* System.Char */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_String? /* System.String */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Text_StringBuilder? /* System.Text.StringBuilder */ value, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Char_Array? /* System.Char[] */ buffer) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(UInt8 /* System.Char */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_String? /* System.String */ value) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_Text_StringBuilder? /* System.Text.StringBuilder */ value, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_Char_Array? /* System.Char[] */ buffer) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_Char_Array? /* System.Char[] */ buffer, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync(System_ReadOnlyMemory_A1? /* System.ReadOnlyMemory<System.Char> */ buffer, System_Threading_CancellationToken? /* System.Threading.CancellationToken */ cancellationToken) throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func writeLineAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func flushAsync() throws -> System_Threading_Tasks_Task? /* System.Threading.Tasks.Task */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func synchronized(System_IO_TextWriter? /* System.IO.TextWriter */ writer) throws -> System_IO_TextWriter? /* System.IO.TextWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFormatProvider() throws -> System_IFormatProvider? /* System.IFormatProvider */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getEncoding() throws -> System_Text_Encoding? /* System.Text.Encoding */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNewLine() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNewLine(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getNull() -> System_IO_TextWriter? /* System.IO.TextWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_TextWriter_TypeOf())
		
	
	}
	
	deinit {
		System_IO_TextWriter_Destroy(self._handle)
		
	
	}
	
	

}














































public class System_IO_FileSystemInfo /* System.IO.FileSystemInfo */ {
	func getObjectData(System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func delete() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createAsSymbolicLink(System_String? /* System.String */ pathToTarget) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func resolveLinkTarget(Bool /* System.Boolean */ returnFinalTarget) throws -> System_IO_FileSystemInfo? /* System.IO.FileSystemInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func refresh() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFullName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExtension() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExists() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCreationTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCreationTime(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCreationTimeUtc() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCreationTimeUtc(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLastAccessTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLastAccessTime(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLastAccessTimeUtc() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLastAccessTimeUtc(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLastWriteTime() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLastWriteTime(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLastWriteTimeUtc() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLastWriteTimeUtc(System_DateTime? /* System.DateTime */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLinkTarget() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getUnixFileMode() throws -> System_IO_UnixFileMode /* System.IO.UnixFileMode */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setUnixFileMode(System_IO_UnixFileMode /* System.IO.UnixFileMode */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributes() throws -> System_IO_FileAttributes /* System.IO.FileAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAttributes(System_IO_FileAttributes /* System.IO.FileAttributes */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_FileSystemInfo_TypeOf())
		
	
	}
	
	deinit {
		System_IO_FileSystemInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_FileInfo /* System.IO.FileInfo */ {
	let _handle: System_IO_FileInfo_t

	required init(handle: System_IO_FileInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func open(System_IO_FileStreamOptions? /* System.IO.FileStreamOptions */ options) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func openText() throws -> System_IO_StreamReader? /* System.IO.StreamReader */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func createText() throws -> System_IO_StreamWriter? /* System.IO.StreamWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func appendText() throws -> System_IO_StreamWriter? /* System.IO.StreamWriter */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_String? /* System.String */ destFileName) throws -> System_IO_FileInfo? /* System.IO.FileInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_String? /* System.String */ destFileName, Bool /* System.Boolean */ overwrite) throws -> System_IO_FileInfo? /* System.IO.FileInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func create() throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func delete() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func open(System_IO_FileMode /* System.IO.FileMode */ mode) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func open(System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func open(System_IO_FileMode /* System.IO.FileMode */ mode, System_IO_FileAccess /* System.IO.FileAccess */ access, System_IO_FileShare /* System.IO.FileShare */ share) throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func openRead() throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func openWrite() throws -> System_IO_FileStream? /* System.IO.FileStream */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveTo(System_String? /* System.String */ destFileName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveTo(System_String? /* System.String */ destFileName, Bool /* System.Boolean */ overwrite) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ destinationFileName, System_String? /* System.String */ destinationBackupFileName) throws -> System_IO_FileInfo? /* System.IO.FileInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func replace(System_String? /* System.String */ destinationFileName, System_String? /* System.String */ destinationBackupFileName, Bool /* System.Boolean */ ignoreMetadataErrors) throws -> System_IO_FileInfo? /* System.IO.FileInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func decrypt() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func encrypt() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ fileName) throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLength() throws -> Int64 /* System.Int64 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDirectoryName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDirectory() throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIsReadOnly() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setIsReadOnly(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExists() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_FileInfo_TypeOf())
		
	
	}
	
	deinit {
		System_IO_FileInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_DirectoryInfo /* System.IO.DirectoryInfo */ {
	let _handle: System_IO_DirectoryInfo_t

	required init(handle: System_IO_DirectoryInfo_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_DirectoryInfo_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func createSubdirectory(System_String? /* System.String */ path) throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func create() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles() throws -> System_IO_FileInfo_Array? /* System.IO.FileInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles(System_String? /* System.String */ searchPattern) throws -> System_IO_FileInfo_Array? /* System.IO.FileInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_IO_FileInfo_Array? /* System.IO.FileInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFiles(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_IO_FileInfo_Array? /* System.IO.FileInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFileSystemInfos() throws -> System_IO_FileSystemInfo_Array? /* System.IO.FileSystemInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFileSystemInfos(System_String? /* System.String */ searchPattern) throws -> System_IO_FileSystemInfo_Array? /* System.IO.FileSystemInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFileSystemInfos(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_IO_FileSystemInfo_Array? /* System.IO.FileSystemInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getFileSystemInfos(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_IO_FileSystemInfo_Array? /* System.IO.FileSystemInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDirectories() throws -> System_IO_DirectoryInfo_Array? /* System.IO.DirectoryInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDirectories(System_String? /* System.String */ searchPattern) throws -> System_IO_DirectoryInfo_Array? /* System.IO.DirectoryInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDirectories(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_IO_DirectoryInfo_Array? /* System.IO.DirectoryInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDirectories(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_IO_DirectoryInfo_Array? /* System.IO.DirectoryInfo[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateDirectories() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.DirectoryInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateDirectories(System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.DirectoryInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateDirectories(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.DirectoryInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateDirectories(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.DirectoryInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFiles() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFiles(System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFiles(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFiles(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFileSystemInfos() throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFileSystemInfos(System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFileSystemInfos(System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func enumerateFileSystemInfos(System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.IO.FileSystemInfo> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func moveTo(System_String? /* System.String */ destDirName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func delete() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func delete(Bool /* System.Boolean */ recursive) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ path) throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getParent() throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getRoot() throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getExists() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_DirectoryInfo_TypeOf())
		
	
	}
	
	deinit {
		System_IO_DirectoryInfo_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_FileInfo_Array /* System.IO.FileInfo[] */ {
	let _handle: System_IO_FileInfo_Array_t

	required init(handle: System_IO_FileInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_IO_EnumerationOptions /* System.IO.EnumerationOptions */ {
	let _handle: System_IO_EnumerationOptions_t

	required init(handle: System_IO_EnumerationOptions_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_EnumerationOptions_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getRecurseSubdirectories() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setRecurseSubdirectories(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getIgnoreInaccessible() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setIgnoreInaccessible(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getBufferSize() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setBufferSize(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAttributesToSkip() throws -> System_IO_FileAttributes /* System.IO.FileAttributes */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAttributesToSkip(System_IO_FileAttributes /* System.IO.FileAttributes */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMatchType() throws -> System_IO_MatchType /* System.IO.MatchType */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMatchType(System_IO_MatchType /* System.IO.MatchType */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMatchCasing() throws -> System_IO_MatchCasing /* System.IO.MatchCasing */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMatchCasing(System_IO_MatchCasing /* System.IO.MatchCasing */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getMaxRecursionDepth() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setMaxRecursionDepth(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getReturnSpecialDirectories() throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setReturnSpecialDirectories(Bool /* System.Boolean */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_EnumerationOptions_TypeOf())
		
	
	}
	
	deinit {
		System_IO_EnumerationOptions_Destroy(self._handle)
		
	
	}
	
	

}


public class System_IO_FileSystemInfo_Array /* System.IO.FileSystemInfo[] */ {
	let _handle: System_IO_FileSystemInfo_Array_t

	required init(handle: System_IO_FileSystemInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_FileSystemInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class System_IO_DirectoryInfo_Array /* System.IO.DirectoryInfo[] */ {
	let _handle: System_IO_DirectoryInfo_Array_t

	required init(handle: System_IO_DirectoryInfo_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: System_IO_DirectoryInfo_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}















public class System_IO_Directory /* System.IO.Directory */ {
	static func getParent(System_String? /* System.String */ path) throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createDirectory(System_String? /* System.String */ path) throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createDirectory(System_String? /* System.String */ path, System_IO_UnixFileMode /* System.IO.UnixFileMode */ unixCreateMode) throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createTempSubdirectory(System_String? /* System.String */ prefix) throws -> System_IO_DirectoryInfo? /* System.IO.DirectoryInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func exists(System_String? /* System.String */ path) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ creationTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCreationTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ creationTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCreationTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastWriteTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastWriteTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastWriteTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastWriteTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTime(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastAccessTime) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setLastAccessTimeUtc(System_String? /* System.String */ path, System_DateTime? /* System.DateTime */ lastAccessTimeUtc) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTime(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLastAccessTimeUtc(System_String? /* System.String */ path) throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFiles(System_String? /* System.String */ path) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectories(System_String? /* System.String */ path) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileSystemEntries(System_String? /* System.String */ path) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateDirectories(System_String? /* System.String */ path) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateDirectories(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFiles(System_String? /* System.String */ path) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFiles(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFileSystemEntries(System_String? /* System.String */ path) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_SearchOption /* System.IO.SearchOption */ searchOption) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func enumerateFileSystemEntries(System_String? /* System.String */ path, System_String? /* System.String */ searchPattern, System_IO_EnumerationOptions? /* System.IO.EnumerationOptions */ enumerationOptions) throws -> System_Collections_Generic_IEnumerable_A1? /* System.Collections.Generic.IEnumerable<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDirectoryRoot(System_String? /* System.String */ path) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getCurrentDirectory() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setCurrentDirectory(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func move(System_String? /* System.String */ sourceDirName, System_String? /* System.String */ destDirName) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delete(System_String? /* System.String */ path) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func delete(System_String? /* System.String */ path, Bool /* System.Boolean */ recursive) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getLogicalDrives() throws -> System_String_Array? /* System.String[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createSymbolicLink(System_String? /* System.String */ path, System_String? /* System.String */ pathToTarget) throws -> System_IO_FileSystemInfo? /* System.IO.FileSystemInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func resolveLinkTarget(System_String? /* System.String */ linkPath, Bool /* System.Boolean */ returnFinalTarget) throws -> System_IO_FileSystemInfo? /* System.IO.FileSystemInfo */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_IO_Directory_TypeOf())
		
	
	}
	
	deinit {
		System_IO_Directory_Destroy(self._handle)
		
	
	}
	
	

}


public class System_Collections_Generic_List_A1 /* System.Collections.Generic.List<> */ {
	let _handle: System_Collections_Generic_List_A1_t

	required init(handle: System_Collections_Generic_List_A1_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Collections_Generic_List_A1_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func binarySearch(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear(System_Type? /* System.Type */ T) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func contains(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ arrayIndex, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func copyTo(System_Type? /* System.Type */ T, System_Array? /* System.Array */ array, Int32 /* System.Int32 */ arrayIndex) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func ensureCapacity(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ capacity) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getRange(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func slice(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ start, Int32 /* System.Int32 */ length) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item, Int32 /* System.Int32 */ index) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func indexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func insert(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index, System_Object? /* System.Object */ item) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item, Int32 /* System.Int32 */ index) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func lastIndexOf(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Type? /* System.Type */ T, System_Object? /* System.Object */ item) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeAt(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeRange(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reverse(System_Type? /* System.Type */ T) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func reverse(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ index, Int32 /* System.Int32 */ count) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func sort(System_Type? /* System.Type */ T) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toArray(System_Type? /* System.Type */ T) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimExcess(System_Type? /* System.Type */ T) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	func getCapacity(System_Type? /* System.Type */ T) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCapacity(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCount(System_Type? /* System.Type */ T) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_Generic_List_A1_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_Generic_List_A1_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.

public class System_Collections_Generic_Dictionary_A2 /* System.Collections.Generic.Dictionary<,> */ {
	let _handle: System_Collections_Generic_Dictionary_A2_t

	required init(handle: System_Collections_Generic_Dictionary_A2_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Collections_Generic_Dictionary_A2_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func add(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func clear(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func containsKey(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func containsValue(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getObjectData(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Runtime_Serialization_SerializationInfo? /* System.Runtime.Serialization.SerializationInfo */ info, System_Runtime_Serialization_StreamingContext? /* System.Runtime.Serialization.StreamingContext */ context) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func onDeserialization(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ sender) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func remove(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key, inout System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func tryGetValue(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key, inout System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func tryAdd(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func ensureCapacity(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, Int32 /* System.Int32 */ capacity) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimExcess(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func trimExcess(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, Int32 /* System.Int32 */ capacity) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, Int32 /* System.Int32 */ capacity) throws {
		// TODO: Constructor
		
	
	}
	
	func getCount(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Collections_Generic_Dictionary_A2_TypeOf())
		
	
	}
	
	deinit {
		System_Collections_Generic_Dictionary_A2_Destroy(self._handle)
		
	
	}
	
	

}


// Type "TValue" was skipped. Reason: It has no full name.
// Type "TKey" was skipped. Reason: It has no full name.

public class System_Tuple_A1 /* System.Tuple<> */ {
	let _handle: System_Tuple_A1_t

	required init(handle: System_Tuple_A1_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Tuple_A1_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func equals(System_Type? /* System.Type */ T1, System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_Type? /* System.Type */ T1) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_Type? /* System.Type */ T1) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T1, System_Object? /* System.Object */ item1) throws {
		// TODO: Constructor
		
	
	}
	
	func getItem1(System_Type? /* System.Type */ T1) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Tuple_A1_TypeOf())
		
	
	}
	
	deinit {
		System_Tuple_A1_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T1" was skipped. Reason: It has no full name.
public class System_Tuple_A2 /* System.Tuple<,> */ {
	let _handle: System_Tuple_A2_t

	required init(handle: System_Tuple_A2_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Tuple_A2_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func equals(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Object? /* System.Object */ item1, System_Object? /* System.Object */ item2) throws {
		// TODO: Constructor
		
	
	}
	
	func getItem1(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getItem2(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Tuple_A2_TypeOf())
		
	
	}
	
	deinit {
		System_Tuple_A2_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T1" was skipped. Reason: It has no full name.
// Type "T2" was skipped. Reason: It has no full name.
public class System_Tuple_A3 /* System.Tuple<,,> */ {
	let _handle: System_Tuple_A3_t

	required init(handle: System_Tuple_A3_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Tuple_A3_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func equals(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3, System_Object? /* System.Object */ obj) throws -> Bool /* System.Boolean */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHashCode(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func toString(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3, System_Object? /* System.Object */ item1, System_Object? /* System.Object */ item2, System_Object? /* System.Object */ item3) throws {
		// TODO: Constructor
		
	
	}
	
	func getItem1(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getItem2(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getItem3(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ T3) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Tuple_A3_TypeOf())
		
	
	}
	
	deinit {
		System_Tuple_A3_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T1" was skipped. Reason: It has no full name.
// Type "T2" was skipped. Reason: It has no full name.
// Type "T3" was skipped. Reason: It has no full name.
public class NativeAOT_CodeGeneratorInputSample_Address /* NativeAOT.CodeGeneratorInputSample.Address */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Address_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Address_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Address_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func move(NativeAOT_CodeGeneratorInputSample_MoveDelegate? /* NativeAOT.CodeGeneratorInputSample.MoveDelegate */ mover, System_String? /* System.String */ newStreet, System_String? /* System.String */ newCity) throws -> NativeAOT_CodeGeneratorInputSample_Address? /* NativeAOT.CodeGeneratorInputSample.Address */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ street, System_String? /* System.String */ city) throws {
		// TODO: Constructor
		
	
	}
	
	func getStreet() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCity() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Address_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Address_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_IAnimal /* NativeAOT.CodeGeneratorInputSample.IAnimal */ {
	func eat(System_String? /* System.String */ food) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_IAnimal_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_IAnimal_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_AnimalFactory /* NativeAOT.CodeGeneratorInputSample.AnimalFactory */ {
	static func createAnimal(System_String? /* System.String */ animalName) throws -> NativeAOT_CodeGeneratorInputSample_IAnimal? /* NativeAOT.CodeGeneratorInputSample.IAnimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createAnimal(System_String? /* System.String */ animalName, NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate? /* NativeAOT.CodeGeneratorInputSample.AnimalCreatorDelegate */ creator) throws -> NativeAOT_CodeGeneratorInputSample_IAnimal? /* NativeAOT.CodeGeneratorInputSample.IAnimal */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func createAnimal(System_Type? /* System.Type */ T) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getDEFAULT_CREATOR() -> NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate? /* NativeAOT.CodeGeneratorInputSample.AnimalCreatorDelegate */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_AnimalFactory_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_AnimalFactory_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T" was skipped. Reason: It has no full name.
public class NativeAOT_CodeGeneratorInputSample_BaseAnimal /* NativeAOT.CodeGeneratorInputSample.BaseAnimal */ {
	func eat(System_String? /* System.String */ food) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_BaseAnimal_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_BaseAnimal_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_Cat /* NativeAOT.CodeGeneratorInputSample.Cat */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Cat_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Cat_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Cat_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getCatName() -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Cat_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Cat_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_Dog /* NativeAOT.CodeGeneratorInputSample.Dog */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Dog_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Dog_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Dog_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDogName() -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Dog_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Dog_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_GenericAnimal /* NativeAOT.CodeGeneratorInputSample.GenericAnimal */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_GenericAnimal_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_GenericAnimal_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_GenericAnimal_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_String? /* System.String */ name) throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_GenericAnimal_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_GenericAnimal_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1 /* NativeAOT.CodeGeneratorInputSample.GenericTestClass<> */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func returnGenericClassType(System_Type? /* System.Type */ T) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericClassTypeAndGenericMethodType(System_Type? /* System.Type */ T, System_Type? /* System.Type */ TM) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func extreme(System_Type? /* System.Type */ T, System_Type? /* System.Type */ TM, Int32 /* System.Int32 */ countIn, inout Int32? /* System.Int32 */ countOut, System_Object? /* System.Object */ typeGenericInput, inout System_Object? /* System.Object */ typeGenericOutput, inout System_Object? /* System.Object */ methodGenericInputThatIsToBeReplacedWithDefault) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T) throws {
		// TODO: Constructor
		
	
	}
	
	func getAProperty(System_Type? /* System.Type */ T) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAProperty(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAField(System_Type? /* System.Type */ T) -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAField(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_Destroy(self._handle)
		
	
	}
	
	

}


// Type "T" was skipped. Reason: It has no full name.

// Type "TM" was skipped. Reason: It has no full name.

public class NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2 /* NativeAOT.CodeGeneratorInputSample.GenericTestClass<,> */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func returnGenericClassTypes(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericClassTypeAndGenericMethodType(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, System_Type? /* System.Type */ TM) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws {
		// TODO: Constructor
		
	
	}
	
	func getAProperty(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAProperty(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAField(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAField(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2, Int32 /* System.Int32 */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_GenericTests /* NativeAOT.CodeGeneratorInputSample.GenericTests */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_GenericTests_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func joinListOfStrings(System_Collections_Generic_List_A1? /* System.Collections.Generic.List<System.String> */ listOfString, System_String? /* System.String */ separator) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericType(System_Type? /* System.Type */ T) throws -> System_Type? /* System.Type */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericTypeAsOutParameter(System_Type? /* System.Type */ T, inout System_Type? /* System.Type */ typeOfT) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericTypeAsRefParameter(System_Type? /* System.Type */ T, inout System_Type? /* System.Type */ typeOfT) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnGenericTypes(System_Type? /* System.Type */ T1, System_Type? /* System.Type */ T2) throws -> System_Type_Array? /* System.Type[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnSimpleKeyValuePair(System_Type? /* System.Type */ TKey, System_Type? /* System.Type */ TValue, System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws -> NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair? /* NativeAOT.CodeGeneratorInputSample.GenericTests.SimpleKeyValuePair */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnDefaultValueOfGenericType(System_Type? /* System.Type */ T) throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnArrayOfDefaultValuesOfGenericType(System_Type? /* System.Type */ T, Int32 /* System.Int32 */ numberOfElements) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func returnArrayOfRepeatedValues(System_Type? /* System.Type */ T, System_Object? /* System.Object */ value, Int32 /* System.Int32 */ numberOfElements) throws -> System_Array? /* System.Array */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func returnStringOfJoinedArray(System_Type? /* System.Type */ T, System_Array? /* System.Array */ values, System_String? /* System.String */ separator) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getListOfStrings() throws -> System_Collections_Generic_List_A1? /* System.Collections.Generic.List<System.String> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setListOfStrings(System_Collections_Generic_List_A1? /* System.Collections.Generic.List<System.String> */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getDictionaryOfStringKeysAndExceptionValues() throws -> System_Collections_Generic_Dictionary_A2? /* System.Collections.Generic.Dictionary<System.String,System.Exception> */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setDictionaryOfStringKeysAndExceptionValues(System_Collections_Generic_Dictionary_A2? /* System.Collections.Generic.Dictionary<System.String,System.Exception> */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_GenericTests_Destroy(self._handle)
		
	
	}
	
	

}






































public class NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair /* NativeAOT.CodeGeneratorInputSample.GenericTests.SimpleKeyValuePair */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_Object? /* System.Object */ key, System_Object? /* System.Object */ value) throws {
		// TODO: Constructor
		
	
	}
	
	func getKey() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getValue() throws -> System_Object? /* System.Object */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_GenericTests_SimpleKeyValuePair_Destroy(self._handle)
		
	
	}
	
	

}


// Type "TKey" was skipped. Reason: It has no full name.
// Type "TValue" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

// Type "T[]" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.

public class NativeAOT_CodeGeneratorInputSample_Person /* NativeAOT.CodeGeneratorInputSample.Person */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Person_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Person_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Person_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func getNiceLevelString() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getWelcomeMessage() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func addChild(NativeAOT_CodeGeneratorInputSample_Person? /* NativeAOT.CodeGeneratorInputSample.Person */ child) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeChild(NativeAOT_CodeGeneratorInputSample_Person? /* NativeAOT.CodeGeneratorInputSample.Person */ child) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeChildAt(Int32 /* System.Int32 */ index) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func childAt(Int32 /* System.Int32 */ index) throws -> NativeAOT_CodeGeneratorInputSample_Person? /* NativeAOT.CodeGeneratorInputSample.Person */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func changeAge(NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate? /* NativeAOT.CodeGeneratorInputSample.Person.NewAgeProviderDelegate */ newAgeProvider) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?(System_String? /* System.String */ firstName, System_String? /* System.String */ lastName, Int32 /* System.Int32 */ age) throws {
		// TODO: Constructor
		
	
	}
	
	convenience init?(System_String? /* System.String */ firstName, System_String? /* System.String */ lastName) throws {
		// TODO: Constructor
		
	
	}
	
	func getFirstName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setFirstName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getLastName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setLastName(System_String? /* System.String */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAge() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAge(Int32 /* System.Int32 */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getAddress() throws -> NativeAOT_CodeGeneratorInputSample_Address? /* NativeAOT.CodeGeneratorInputSample.Address */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setAddress(NativeAOT_CodeGeneratorInputSample_Address? /* NativeAOT.CodeGeneratorInputSample.Address */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getChildren() throws -> NativeAOT_CodeGeneratorInputSample_Person_Array? /* NativeAOT.CodeGeneratorInputSample.Person[] */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setChildren(NativeAOT_CodeGeneratorInputSample_Person_Array? /* NativeAOT.CodeGeneratorInputSample.Person[] */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNiceLevel() throws -> NativeAOT_CodeGeneratorInputSample_NiceLevels /* NativeAOT.CodeGeneratorInputSample.NiceLevels */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setNiceLevel(NativeAOT_CodeGeneratorInputSample_NiceLevels /* NativeAOT.CodeGeneratorInputSample.NiceLevels */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getFullName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getNumberOfChildren() throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func addNumberOfChildrenChanged(NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate? /* NativeAOT.CodeGeneratorInputSample.Person.NumberOfChildrenChangedDelegate */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func removeNumberOfChildrenChanged(NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate? /* NativeAOT.CodeGeneratorInputSample.Person.NumberOfChildrenChangedDelegate */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getAGE_WHEN_BORN() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDEFAULT_AGE() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Person_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Person_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_Person_Array /* NativeAOT.CodeGeneratorInputSample.Person[] */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Person_Array_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Person_Array_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Person_Array_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	

}



public class NativeAOT_CodeGeneratorInputSample_Person_Extensions /* NativeAOT.CodeGeneratorInputSample.Person_Extensions */ {
	static func increaseAge(NativeAOT_CodeGeneratorInputSample_Person? /* NativeAOT.CodeGeneratorInputSample.Person */ person, Int32 /* System.Int32 */ byYears) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Person_Extensions_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Person_Extensions_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_Book /* NativeAOT.CodeGeneratorInputSample.Book */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_Book_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_Book_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_Book_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	convenience init?(System_String? /* System.String */ name) throws {
		// TODO: Constructor
		
	
	}
	
	func getName() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getDonQuixote() -> NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getATaleOfTwoCities() -> NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func getTheLordOfTheRings() -> NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Book_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Book_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_TestClass /* NativeAOT.CodeGeneratorInputSample.TestClass */ {
	let _handle: NativeAOT_CodeGeneratorInputSample_TestClass_t

	required init(handle: NativeAOT_CodeGeneratorInputSample_TestClass_t) {
		self._handle = handle
	}

	convenience init?(handle: NativeAOT_CodeGeneratorInputSample_TestClass_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

	func sayHello() throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func sayHello(System_String? /* System.String */ name) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getHello() throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getDate() throws -> System_DateTime? /* System.DateTime */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func add(Int32 /* System.Int32 */ number1, Int32 /* System.Int32 */ number2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func divide(Int32 /* System.Int32 */ number1, Int32 /* System.Int32 */ number2) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getTestEnumName(NativeAOT_CodeGeneratorInputSample_TestEnum /* NativeAOT.CodeGeneratorInputSample.TestEnum */ testEnum) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func modifyByRefValueAndReturnOriginalValue(inout Int32? /* System.Int32 */ valueToModify, Int32 /* System.Int32 */ targetValue) throws -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func modifyByRefEnum(inout NativeAOT_CodeGeneratorInputSample_TestEnum? /* NativeAOT.CodeGeneratorInputSample.TestEnum */ enumToModify) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func modifyByRefBookAndReturnOriginalBookAsOutParameter(inout NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ bookToModify, NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ targetBook, inout NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ originalBook) throws {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func getCurrentBookByRef() throws -> inout NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func increaseAndGetCurrentIntValueByRef() throws -> inout Int32? /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	convenience init?() throws {
		// TODO: Constructor
		
	
	}
	
	func getCurrentBook() -> NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrentBook(NativeAOT_CodeGeneratorInputSample_Book? /* NativeAOT.CodeGeneratorInputSample.Book */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	func getCurrentIntValue() -> Int32 /* System.Int32 */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	func setCurrentIntValue(Int32 /* System.Int32 */ value) ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_TestClass_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(self._handle)
		
	
	}
	
	

}




public class NativeAOT_CodeGeneratorInputSample_Transformer /* NativeAOT.CodeGeneratorInputSample.Transformer */ {
	static func transformString(System_String? /* System.String */ inputString, NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate */ stringTransformer) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func transformDoubles(double /* System.Double */ number1, double /* System.Double */ number2, NativeAOT_CodeGeneratorInputSample_Transformer_DoublesTransformerDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.DoublesTransformerDelegate */ doublesTransformer) throws -> double /* System.Double */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func getAndTransformString(NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.StringGetterDelegate */ stringGetter, NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate */ stringTransformer) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func uppercaseString(System_String? /* System.String */ inputString) throws -> System_String? /* System.String */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Transformer_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Transformer_Destroy(self._handle)
		
	
	}
	
	

}


public class NativeAOT_CodeGeneratorInputSample_Transformer_BuiltInTransformers /* NativeAOT.CodeGeneratorInputSample.Transformer.BuiltInTransformers */ {
	static func getUppercaseStringTransformer() throws -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate */ {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	static func setUppercaseStringTransformer(NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate? /* NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate */ value) throws ->  {
		// TODO: Method/Property/Field/Event Handler adder/remover
		
	
	}
	
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Transformer_BuiltInTransformers_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_Transformer_BuiltInTransformers_Destroy(self._handle)
		
	
	}
	
	

}


// TODO: Delegate Type Defition (System.Delegate)
	deinit {
		System_Delegate_Destroy(self._handle)
		
	
	}
	
	



// Type "T" was skipped. Reason: It has no full name.
// Type "T" was skipped. Reason: It has no full name.
// TODO: Delegate Type Defition (System.Reflection.TypeFilter)
	deinit {
		System_Reflection_TypeFilter_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.MulticastDelegate)
	deinit {
		System_MulticastDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Action)
	deinit {
		System_Action_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.AsyncCallback)
	deinit {
		System_AsyncCallback_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Reflection.ModuleResolveEventHandler)
	deinit {
		System_Reflection_ModuleResolveEventHandler_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Reflection.MemberFilter)
	deinit {
		System_Reflection_MemberFilter_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Threading.ContextCallback)
	deinit {
		System_Threading_ContextCallback_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.UnhandledExceptionEventHandler)
	deinit {
		System_UnhandledExceptionEventHandler_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.EventHandler)
	deinit {
		System_EventHandler_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.AssemblyLoadEventHandler)
	deinit {
		System_AssemblyLoadEventHandler_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.ResolveEventHandler)
	deinit {
		System_ResolveEventHandler_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Threading.ThreadStart)
	deinit {
		System_Threading_ThreadStart_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Threading.ParameterizedThreadStart)
	deinit {
		System_Threading_ParameterizedThreadStart_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (System.Threading.TimerCallback)
	deinit {
		System_Threading_TimerCallback_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.MoveDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_MoveDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.AnimalCreatorDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_AnimalCreatorDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.Person.NumberOfChildrenChangedDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildrenChangedDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.Person.NewAgeProviderDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_Person_NewAgeProviderDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.ByRefReturnValueDelegate)
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.ByRefParametersDelegate)
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.OutParametersDelegate)
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.Transformer.DoublesTransformerDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_Transformer_DoublesTransformerDelegate_Destroy(self._handle)
		
	
	}
	
	



// TODO: Delegate Type Defition (NativeAOT.CodeGeneratorInputSample.Transformer.StringGetterDelegate)
	deinit {
		NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate_Destroy(self._handle)
		
	
	}
	
	




// MARK: - END APIs

// MARK: - BEGIN Footer


// MARK: - END Footer

