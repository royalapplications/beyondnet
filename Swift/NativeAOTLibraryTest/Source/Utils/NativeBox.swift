import Foundation

// TODO: Memory management
class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
    
    func retainedPointer() -> UnsafeRawPointer {
        let unmanaged = Unmanaged.passRetained(self)
        let opaque = unmanaged.toOpaque()
        let pointer = UnsafeRawPointer(opaque)
        
        return pointer
    }
    
    static func fromRetainedPointer(_ pointer: UnsafeRawPointer) -> Self {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        let box = unmanaged.takeUnretainedValue()
        
        return box
    }
}
