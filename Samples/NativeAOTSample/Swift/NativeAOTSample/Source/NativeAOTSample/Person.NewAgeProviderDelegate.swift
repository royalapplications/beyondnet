import Foundation

public extension Person {
	class NewAgeProviderDelegate: System.Object {
		public typealias Function = () -> Int32
		
		public override class var type: System._Type {
			.init(handle: NativeAOTSample_Person_NewAgeProviderDelegate_TypeOf())
		}
		
		public var context: UnsafeRawPointer? {
			Debug.log("Will get context of \(Self.swiftTypeName)")
			
			let value = NativeAOTSample_Person_NewAgeProviderDelegate_Context_Get(handle)
			
			Debug.log("Did get context of \(Self.swiftTypeName)")
			
			return value
		}
		
		public var cFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t {
			Debug.log("Will get cFunction of \(Self.swiftTypeName)")
			
			let value = NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_Get(handle)!
			
			Debug.log("Did get cFunction of \(Self.swiftTypeName)")
			
			return value
		}
		
		public var cDestructorFunction: NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t? {
			Debug.log("Will get cDestructorFunction of \(Self.swiftTypeName)")
			
			let value = NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_Get(handle)
			
			Debug.log("Did get cDestructorFunction of \(Self.swiftTypeName)")
			
			return value
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
