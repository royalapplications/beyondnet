import Foundation

class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
}

extension NativeBox {
	static func releaseRetainedPointer(_ pointer: UnsafeRawPointer) {
		let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
		
		unmanaged.release()
	}
	
	func unretainedPointer() -> UnsafeRawPointer {
		let unmanaged = Unmanaged.passUnretained(self)
		let opaque = unmanaged.toOpaque()
		let pointer = UnsafeRawPointer(opaque)
		
		return pointer
	}
	
	func retainedPointer() -> UnsafeRawPointer {
		let unmanaged = Unmanaged.passRetained(self)
		let opaque = unmanaged.toOpaque()
		let pointer = UnsafeRawPointer(opaque)
		
		return pointer
	}
	
	static func fromPointerUnretained(_ pointer: UnsafeRawPointer) -> Self {
		let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
		
		let box = unmanaged.takeUnretainedValue()
		
		return box
	}
	
	static func fromPointerRetained(_ pointer: UnsafeRawPointer) -> Self {
		let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
		
		let box = unmanaged.takeRetainedValue()
		
		return box
	}
}
