import Foundation

public class TEST_NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate /* NativeAOT.CodeGeneratorInputSample.Transformer+StringTransformerDelegate */: System_MulticastDelegate {
	public override class var typeName: String { "NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate" }
	public override class var fullTypeName: String { "NativeAOT.CodeGeneratorInputSample.Transformer+StringTransformerDelegate" }

	public typealias ClosureType = (_ inputString: System_String? /* System.String */) -> System_String?

	private static func __createCFunction() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CFunction_t {
		return { __innerContext, inputString in
			guard let __innerContext else { fatalError("Context is nil") }

			let __innerSwiftContext = NativeBox<ClosureType>.fromPointer(__innerContext)
			let __innerClosure = __innerSwiftContext.value

			let inputStringSwift = System_String(handle: inputString)
			
			let __returnValueSwift = __innerClosure(inputStringSwift)

			let __returnValue = __returnValueSwift?.__handle
			__returnValueSwift?.__skipDestroy = true // Will be destroyed by .NET

			return __returnValue
		}
	}

	private static func __createCDestructorFunction() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CDestructorFunction_t {
		return { __innerContext in
			guard let __innerContext else { fatalError("Context is nil") }

			NativeBox<ClosureType>.release(__innerContext)
		}
	}

	public convenience init?(_ __closure: @escaping ClosureType) {
		let __cFunction = Self.__createCFunction()
		let __cDestructorFunction = Self.__createCDestructorFunction()

		let __outerSwiftContext = NativeBox(__closure)
		let __outerContext = __outerSwiftContext.retainedPointer()

		guard let __delegateC = NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Create(__outerContext, __cFunction, __cDestructorFunction) else { return nil }

		self.init(handle: __delegateC)
	}

	public func invoke(inputString: System_String?) throws -> System_String? {
		var __exceptionC: System_Exception_t?
		
		let returnValueC = NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Invoke(self.__handle,
																										   inputString?.__handle,
																										   &__exceptionC)
		
		if let __exceptionC {
			let __exception = System_Exception(handle: __exceptionC)
			let __exceptionError = __exception.error
			
			throw __exceptionError
		}
		
		let returnValue = System_String(handle: returnValueC)
		
		return returnValue
	}

	public override class func typeOf() -> System_Type /* System.Type */ {
		return System_Type(handle: NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_TypeOf())
		
	}
	
	internal override func destroy() {
		NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(self.__handle)
		
	}
}
