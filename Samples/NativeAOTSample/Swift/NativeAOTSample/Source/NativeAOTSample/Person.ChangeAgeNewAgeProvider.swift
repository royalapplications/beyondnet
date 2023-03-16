import Foundation

public extension Person {
	class ChangeAgeNewAgeProvider: CDelegate {
		public typealias FunctionType = () -> Int32
		
		public convenience init(_ function: @escaping FunctionType) {
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
			
			let destructorFunction: NativeAOTSample_CDelegate_Destructor_t = { innerContext in
				guard let innerContext else {
					fatalError("No context")
				}
				
				let innerClosureBox = NativeBox<FunctionType>.fromPointer(innerContext)
				
				Debug.log("Destroying CDelegate")
				
				innerClosureBox.release(innerContext)
			}
			
			let context = closureBox.retainedPointer()
			
			let delegateHandle = NativeAOTSample_Person_ChangeAge_NewAgeProvider_Create(context,
																						handlerFunction,
																						destructorFunction)
			
			guard let delegateHandle else {
				fatalError("Failed to create CDelegate")
			}
			
			self.init(handle: delegateHandle)
		}
	}
}
