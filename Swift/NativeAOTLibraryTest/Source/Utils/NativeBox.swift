import Foundation

class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
    
    func unretainedPointer() -> UnsafeRawPointer {
        let unmanaged = Unmanaged.passUnretained(self)
        let opaque = unmanaged.toOpaque()
        let pointer = UnsafeRawPointer(opaque)
        
        return pointer
    }
    
    static func fromUnretainedPointer(_ pointer: UnsafeRawPointer) -> Self {
        let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
        
        let box = unmanaged.takeUnretainedValue()
        
        return box
    }
}
