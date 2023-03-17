import Foundation

public extension Person {
	class NewAgeProviderDelegate: System.Object {
		public typealias Function = () -> Int32
		
		public override class var type: System._Type {
			.init(handle: NativeAOTSample_Person_NewAgeProviderDelegate_TypeOf())
		}
		
		public var context: UnsafeRawPointer {
			NativeAOTSample_Person_NewAgeProviderDelegate_Context_Get(handle)
		}
		
		public var cFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t {
			NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_Get(handle)
		}
		
		public var cDestructorFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t {
			NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_Get(handle)
		}
		
		public convenience init(_ function: @escaping Function) {
			Debug.log("Will create \(Self.swiftTypeName)")
			
			let closureBox = NativeBox(function)
			
			let handlerFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t = { innerContext in
				guard let innerContext else {
					fatalError("No context")
				}
				
				let innerClosureBox = NativeBox<Function>.fromPointer(innerContext)
				let innerClosure = innerClosureBox.value
				
				let result = innerClosure()
				
				return result
			}
			
			let destructorFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t = { innerContext in
				guard let innerContext else {
					fatalError("No context")
				}
				
				Debug.log("Destroying Delegate")
				
				NativeBox<Function>.release(innerContext)
			}
			
			let context = closureBox.retainedPointer()
			
			let delegateHandle = NativeAOTSample_Person_NewAgeProviderDelegate_Create(context,
																					  handlerFunction,
																					  destructorFunction)
			
			guard let delegateHandle else {
				fatalError("Failed to create \(Self.swiftTypeName)")
			}
			
			self.init(handle: delegateHandle)
			
			Debug.log("Did create \(Self.swiftTypeName)")
		}
	}
}
