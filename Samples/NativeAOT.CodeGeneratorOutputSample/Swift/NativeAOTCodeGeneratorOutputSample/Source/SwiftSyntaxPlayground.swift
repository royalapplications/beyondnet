import Foundation

public class NameTests {
	public class func `repeat`() {
		
	}
}

//public class DNObject {
//	let __handle: UnsafeMutableRawPointer
//
//	public var typeName: String { "" }
//	public var fullTypeName: String { "" }
//
//	required init(handle: UnsafeMutableRawPointer) {
//		self.__handle = handle
//	}
//
//	convenience init?(handle: UnsafeMutableRawPointer?) {
//		guard let handle else { return nil }
//
//		self.init(handle: handle)
//	}
//
//	// TODO: Should be non-optional
//	public class func typeOf() -> System_Type? /* System.Type */ {
//		fatalError("Override in subclass")
//	}
//
//	internal func destroy() {
//		// Override in subclass
//	}
//
//	deinit {
//		destroy()
//	}
//}
//
//public class DNError: LocalizedError {
//	public let exception: System_Exception
//	
//	public init(exception: System_Exception) {
//		self.exception = exception
//	}
//	
//	public func stackTrace() -> String? {
//		do {
//			return try String(dotNETString: exception.getStackTrace())
//		} catch {
//			return nil
//		}
//	}
//	
//	public var errorDescription: String? {
//		do {
//			return try String(dotNETString: exception.getMessage())
//		} catch {
//			return nil
//		}
//	}
//}
//
//public extension System_Exception {
//	var error: DNError {
//		return DNError(exception: self)
//	}
//}
//
//public extension String {
//	func dotNETString() -> System_String {
//		guard let dotNetStringHandle = DNStringFromC(self) else {
//			fatalError("Failed to convert Swift String to .NET String")
//		}
//		
//		return System_String(handle: dotNetStringHandle)
//	}
//	
//	init?(dotNETString: System_String?) {
//		guard let dotNETString else { return nil }
//		
//		self.init(dotNETString: dotNETString)
//	}
//	
//	init(dotNETString: System_String) {
//		guard let cString = DNStringToC(dotNETString.__handle) else {
//			fatalError("Failed to convert .NET String to C String")
//		}
//		
//		self.init(cString: cString)
//		
//		cString.deallocate()
//	}
//}
//
//public class System_Object /* System.Object */: DNObject {
//	public func getType() throws -> System_Type? /* System.Type */ {
//	
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_GetType(self.__handle, &__exceptionC)
//		let __returnValue = System_Type(handle: __returnValueC)
//		
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValue
//		
//	
//	}
//	
//	public func toString() throws -> System_String? /* System.String */ {
//	
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_ToString(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValue
//		
//	
//	}
//	
//	public func equals(obj: System_Object? /* System.Object */) throws -> Bool /* System.Boolean */ {
//	
//		let objC = obj?.__handle
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_Equals(self.__handle, objC, &__exceptionC)
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValueC
//		
//	
//	}
//	
//	public class func equals(objA: System_Object? /* System.Object */, objB: System_Object? /* System.Object */) throws -> Bool /* System.Boolean */ {
//	
//		let objAC = objA?.__handle
//		let objBC = objB?.__handle
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_Equals_1(objAC, objBC, &__exceptionC)
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValueC
//		
//	
//	}
//	
//	public class func referenceEquals(objA: System_Object? /* System.Object */, objB: System_Object? /* System.Object */) throws -> Bool /* System.Boolean */ {
//	
//		let objAC = objA?.__handle
//		let objBC = objB?.__handle
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_ReferenceEquals(objAC, objBC, &__exceptionC)
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValueC
//		
//	
//	}
//	
//	public func getHashCode() throws -> Int32 /* System.Int32 */ {
//	
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_GetHashCode(self.__handle, &__exceptionC)
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		return __returnValueC
//		
//	
//	}
//	
//	public convenience init?() throws {
//	
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Object_Create(&__exceptionC)
//		if let __exceptionC {
//			let __exception = System_Exception(handle: __exceptionC)
//			let __error = __exception.error
//			
//			throw __error
//		}
//		
//		guard let __returnValueC else { return nil }
//		
//		self.init(handle: __returnValueC)
//		
//	
//	}
//	
//	public override class func typeOf() -> System_Type? /* System.Type */ {
//	
//		return System_Type(handle: System_Object_TypeOf())
//		
//	
//	}
//	
//	internal override func destroy() {
//	
//		System_Object_Destroy(self.__handle)
//		
//	
//	}
//	
//	
//
//}
//
//
//public class System_String /* System.String */: System_Object {
//	
//}
//
//public class System_Exception /* System.Exception */: System_Object {
//	public func getBaseException() throws -> System_Exception? /* System.Exception */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_GetBaseException(self.__handle, &__exceptionC)
//		let __returnValue = System_Exception(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	public override func toString() throws -> System_String? /* System.String */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_ToString(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	public override func getType() throws -> System_Type? /* System.Type */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_GetType(self.__handle, &__exceptionC)
//		let __returnValue = System_Type(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//		
//	}
//	
//	public convenience init?() throws {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_Create(&__exceptionC)
//		// TODO: Exception Handling
//		
//		guard let __returnValueC else { return nil }
//		
//		self.init(handle: __returnValueC)
//		
//	
//	}
//	
//	public convenience init?(message: System_String? /* System.String */) throws {
//		let messageC = message?.__handle
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_Create_1(messageC, &__exceptionC)
//		// TODO: Exception Handling
//		
//		guard let __returnValueC else { return nil }
//		
//		self.init(handle: __returnValueC)
//		
//	
//	}
//	
//	public convenience init?(message: System_String? /* System.String */, innerException: System_Exception? /* System.Exception */) throws {
//		let messageC = message?.__handle
//		let innerExceptionC = innerException?.__handle
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_Create_2(messageC, innerExceptionC, &__exceptionC)
//		// TODO: Exception Handling
//		
//		guard let __returnValueC else { return nil }
//		
//		self.init(handle: __returnValueC)
//		
//	
//	}
//	
//	public func getMessage() throws -> System_String? /* System.String */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_Message_Get(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	public func getInnerException() throws -> System_Exception? /* System.Exception */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_InnerException_Get(self.__handle, &__exceptionC)
//		let __returnValue = System_Exception(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	
//	public func getHelpLink() throws -> System_String? /* System.String */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_HelpLink_Get(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	public func getSource() throws -> System_String? /* System.String */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_Source_Get(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	public func getHResult() throws -> Int32 /* System.Int32 */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_HResult_Get(self.__handle, &__exceptionC)
//		// TODO: Exception Handling
//		
//		return __returnValueC
//		
//	
//	}
//	
//	public func getStackTrace() throws -> System_String? /* System.String */ {
//		
//		
//		var __exceptionC: System_Exception_t?
//		
//		let __returnValueC = System_Exception_StackTrace_Get(self.__handle, &__exceptionC)
//		let __returnValue = System_String(handle: __returnValueC)
//		
//		// TODO: Exception Handling
//		
//		return __returnValue
//		
//	
//	}
//	
//	
//	public override class func typeOf() -> System_Type? /* System.Type */ {
//		return System_Type(handle: System_Exception_TypeOf())
//		
//	
//	}
//	
//	internal override func destroy() {
//		System_Exception_Destroy(self.__handle)
//		
//	
//	}
//	
//	
//
//}
//
//public class System_Reflection_MemberInfo /* System.Reflection.MemberInfo */: System_Object {
//	
//}
//
//public class System_Type /* System.Type */: System_Reflection_MemberInfo {
//	
//}
