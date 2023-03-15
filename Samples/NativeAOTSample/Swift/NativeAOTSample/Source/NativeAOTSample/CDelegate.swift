import Foundation

public class CDelegate: System.Object {
//    override class var type: System._Type {
//        .init(handle: NativeAOTSample_Person_TypeOf())
//    }
    
    deinit {
        NativeAOT_Core_CDelegate_Destroy(handle)
    }
}

// MARK: - Public API
public extension CDelegate {
    convenience init(context: UnsafeRawPointer?,
                     function: UnsafeRawPointer,
                     destructorFunction: NativeAOTSample_CDelegate_Destructor_t) {
        guard let handle = NativeAOT_Core_CDelegate_Create(context,
                                                           function,
                                                           destructorFunction) else {
            fatalError("Failed to create CDelegate")
        }
        
        self.init(handle: handle)
    }
}
