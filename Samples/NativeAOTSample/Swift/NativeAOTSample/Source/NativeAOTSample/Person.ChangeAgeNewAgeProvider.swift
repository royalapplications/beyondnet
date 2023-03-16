import Foundation

public extension Person {
	class ChangeAgeNewAgeProvider {
		public typealias FunctionType = () -> Int32
		
		public static func createDelegate(_ function: @escaping FunctionType) -> CDelegate {
			let closureBox = NativeBox(function)
			
			let handlerFunction: NativeAOTSample_Person_ChangeAge_NewAgeProvider_t = { innerContext in
				guard let innerContext else {
					fatalError("No context")
				}
				
				let innerClosureBox = NativeBox<FunctionType>.fromPointer(innerContext)
				let innerClosure = innerClosureBox.value
				
				let result = innerClosure()
				
				return result
			}
			
			let destructorFunction = CDelegate.destructorForNativeBox()
			let context = closureBox.retainedPointer()
			
			let delegateHandle = NativeAOTSample_Person_ChangeAge_NewAgeProvider_Create(context,
																						handlerFunction,
																						destructorFunction)
			
			guard let delegateHandle else {
				fatalError("Failed to create CDelegate")
			}
			
			return .init(handle: delegateHandle)
		}
	}
}
