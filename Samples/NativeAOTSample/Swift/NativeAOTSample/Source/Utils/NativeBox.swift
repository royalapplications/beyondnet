import Foundation

final class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
	
	deinit {
		Debug.log("Deinitializing \(Self.self)")
	}
}

// MARK: - To Pointer
extension NativeBox {
	func unretainedPointer() -> UnsafeRawPointer {
		pointer(retained: false)
	}
	
	func retainedPointer() -> UnsafeRawPointer {
		pointer(retained: true)
	}
}

private extension NativeBox {
	func pointer(retained: Bool) -> UnsafeRawPointer {
		let unmanaged: Unmanaged<NativeBox<T>>
		
		if retained {
			unmanaged = Unmanaged.passRetained(self)
		} else {
			unmanaged = Unmanaged.passUnretained(self)
		}
		
		let opaque = unmanaged.toOpaque()
		
		let pointer = UnsafeRawPointer(opaque)
		
		return pointer
	}
}

// MARK: - From Pointer
extension NativeBox {
	static func fromPointer(_ pointer: UnsafeRawPointer) -> Self {
		let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
		
		let box = unmanaged.takeUnretainedValue()
		
		return box
	}
}

// MARK: - Release
extension NativeBox {
	static func release(_ pointer: UnsafeRawPointer) {
		let unmanaged = Unmanaged<Self>.fromOpaque(pointer)
		
		unmanaged.release()
	}
	
	func release(_ pointer: UnsafeRawPointer) {
		Self.release(pointer)
	}
}
