import Foundation

public class TEST_NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate /* NativeAOT.CodeGeneratorInputSample.Transformer+StringTransformerDelegate */: System_MulticastDelegate {
	public override class var typeName: String { "NativeAOT.CodeGeneratorInputSample.Transformer.StringTransformerDelegate" }
	public override class var fullTypeName: String { "NativeAOT.CodeGeneratorInputSample.Transformer+StringTransformerDelegate" }
	
	public typealias ClosureType = (_ inputString: System_String?) -> System_String?
	
	private static func __createCFunction() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CFunction_t {
		return { __innerContext, inputStringC in
			guard let __innerContext else { fatalError("No context") }
			
			let __innerSwiftContext = NativeBox<ClosureType>.fromPointer(__innerContext)
			let __innerClosure = __innerSwiftContext.value
			
			let inputString = System_String(handle: inputStringC)
			
			let __returnValue = __innerClosure(inputString)
			let __returnValueC = __returnValue?.__handle
			
			return __returnValueC
		}
	}
	
	private static func __createCDestructorFunction() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CDestructorFunction_t {
		return { __innerContext in
			guard let __innerContext else { fatalError("No context") }
			
			NativeBox<ClosureType>.release(__innerContext)
		}
	}
	
	public convenience init?(_ __closure: @escaping ClosureType) {
		let __cFunction = Self.__createCFunction()
		let __cDestructorFunction = Self.__createCDestructorFunction()
		
		let __outerSwiftContext = NativeBox(__closure)
		let __outerContext = __outerSwiftContext.retainedPointer()
		
		guard let __delegateC = NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Create(__outerContext,
																												__cFunction,
																												__cDestructorFunction) else {
			return nil
		}
		
		self.init(handle: __delegateC)
	}
	
	override func destroy() {
		NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(self.__handle)
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
}
