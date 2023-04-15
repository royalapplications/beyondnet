namespace NativeAOT.CodeGenerator.Generator.Swift;

public class SwiftSharedCode
{
    public const string SharedCode = """
final class NativeBox<T> {
    let value: T
    
    init(value: T) {
        self.value = value
    }
    
    convenience init(_ value: T) {
        self.init(value: value)
    }
    
//    deinit {
//        print("Deinitializing \(Self.self)")
//    }
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

public class DNObject {
    let __handle: UnsafeMutableRawPointer

    public var typeName: String { "" }
    public var fullTypeName: String { "" }

    required init(handle: UnsafeMutableRawPointer) {
		self.__handle = handle
	}

	convenience init?(handle: UnsafeMutableRawPointer?) {
		guard let handle else { return nil }

		self.init(handle: handle)
	}

    public class var typeOf: System_Type /* System.Type */ {
        fatalError("Override in subclass")
    }

    internal func destroy(handle: UnsafeMutableRawPointer) {
        // Override in subclass
    }

    deinit {
		destroy(handle: self.__handle)
	}
}
""";
}