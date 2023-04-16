import Foundation

//class DNError: LocalizedError {
//    public let exception: System_Exception
//
//    public init(exception: System_Exception) {
//        self.exception = exception
//    }
//
//    public func stackTrace() -> String? {
//        do {
//            return try exception.getStackTrace()
//        } catch {
//            return nil
//        }
//    }
//
//    public var errorDescription: String? {
//        do {
//            return try exception.getMessage()
//        } catch {
//            return nil
//        }
//    }
//}
//
//public extension System_Exception {
//    var error: DNError {
//        return DNError(exception: self)
//    }
//}

public class DNObject {
    let __handle: UnsafeMutableRawPointer
    
    public var typeName: String { "" }
    public var fullTypeName: String { "" }

    required init(handle: UnsafeMutableRawPointer) {
        self.__handle = handle
    }

    convenience init?(handle: UnsafeMutableRawPointer?) {
        guard let handle else { return nil }

        self.init(handle: handle)
    }

    internal func destroy() {
        // Override in subclass
    }
    
    public class var typeOf: System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    deinit {
        destroy()
    }
}

public class System_Object /* System.Object */: DNObject {
    public override var typeName: String { "System.Object" }
    public override var fullTypeName: String { "Blah" }
    
//	func getType() throws -> System_Type? /* System.Type */ {
//
//	}
	
//	convenience init?() throws {
//		var exception: System_Exception_t?
//
//		guard let handle = System_Object_Create(&exception) else {
//
//		}
//	}
	
    public override class var typeOf: System_Type /* System.Type */ {
		return System_Type(handle: System_Object_TypeOf())!
	}
    
    override func destroy() {
        System_Object_Destroy(self.__handle)
    }
}


public class System_Type /* System.Type */: System_Object {
//	static func typeOf() -> System_Type? /* System.Type */ {
//
//	}

    override func destroy() {
        System_Type_Destroy(self.__handle)
	}
}

public class NativeAOT_CodeGeneratorInputSample_TestClass /* NativeAOT.CodeGeneratorInputSample.TestClass */: System_Object {
    public override class var typeOf: System_Type /* System.Type */ {
        return System_Type(handle: NativeAOT_CodeGeneratorInputSample_TestClass_TypeOf())
    }
    
    override func destroy() {
        NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(self.__handle)
    }
}
