import Foundation

public class System_Object /* System.Object */ {
	let _handle: System_Object_t

	required init(handle: System_Object_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Object_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

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
	
	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: System_Object_TypeOf())
	}
	
	deinit {
		System_Object_Destroy(self._handle)
	}
}


public class System_Type /* System.Type */ {
	let _handle: System_Type_t

	required init(handle: System_Type_t) {
		self._handle = handle
	}

	convenience init?(handle: System_Type_t?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}
	
//	static func typeOf() -> System_Type? /* System.Type */ {
//
//	}

	deinit {
		System_Type_Destroy(self._handle)
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

	static func typeOf() -> System_Type? /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_TestClass_TypeOf())
		
	
	}
	
	deinit {
		NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(self._handle)
		
	
	}
	
	

}
