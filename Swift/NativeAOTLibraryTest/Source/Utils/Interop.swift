import Foundation

func bridge<T: AnyObject>(object: T) -> UnsafeMutableRawPointer {
	Unmanaged.passUnretained(object).toOpaque()
}

func bridge<T: AnyObject>(pointer: UnsafeRawPointer) -> T {
	return Unmanaged<T>.fromOpaque(pointer).takeUnretainedValue()
}
