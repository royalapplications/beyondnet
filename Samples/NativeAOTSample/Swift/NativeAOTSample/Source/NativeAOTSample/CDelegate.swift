import Foundation

public class CDelegate: System.Object {
    override class var type: System._Type {
        .init(handle: NativeAOT_Core_CDelegate_TypeOf())
    }
}

// MARK: - Public API
public extension CDelegate {
    convenience init(context: UnsafeRawPointer?,
                     function: UnsafeRawPointer,
                     destructorFunction: NativeAOTSample_CDelegate_Destructor_t?) {
		Debug.log("Will create \(Self.swiftTypeName)")
		
		let handle = NativeAOT_Core_CDelegate_Create(context,
													 function,
													 destructorFunction)
		
		guard let handle else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}
		
		self.init(handle: handle)
		
		Debug.log("Did create \(Self.swiftTypeName)")
    }
}
