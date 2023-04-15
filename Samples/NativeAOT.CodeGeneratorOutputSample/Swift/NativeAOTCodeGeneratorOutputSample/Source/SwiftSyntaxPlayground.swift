import Foundation

public class DNObject {
    let __handle: UnsafeMutableRawPointer

    required init(handle: UnsafeMutableRawPointer) {
        self.__handle = handle
    }

    convenience init?(handle: UnsafeMutableRawPointer?) {
        guard let handle else { return nil }

        self.init(handle: handle)
    }

    internal func destroy(handle: UnsafeMutableRawPointer) {
        // Override in subclass
    }
    
    public class var typeOf: System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    deinit {
        destroy(handle: self.__handle)
    }
}

public class System_Object /* System.Object */: DNObject {
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
    
    override func destroy(handle: UnsafeMutableRawPointer) {
        System_Object_Destroy(handle)
    }
}


public class System_Type /* System.Type */: System_Object {
//	static func typeOf() -> System_Type? /* System.Type */ {
//
//	}

    override func destroy(handle: UnsafeMutableRawPointer) {
		System_Type_Destroy(handle)
	}
}

public class NativeAOT_CodeGeneratorInputSample_TestClass /* NativeAOT.CodeGeneratorInputSample.TestClass */: System_Object {
    public override class var typeOf: System_Type /* System.Type */ {
        return System_Type(handle: NativeAOT_CodeGeneratorInputSample_TestClass_TypeOf())
    }
    
    override func destroy(handle: UnsafeMutableRawPointer) {
        NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(handle)
    }
}
