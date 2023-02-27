import Foundation

public class UnhandledExceptionTest: SystemObject {
	override class var type: SystemType {
		.init(handle: NativeAOTLibraryTest_UnhandledExceptionTest_TypeOf())
	}
}

// MARK: - Public API
public extension UnhandledExceptionTest {
	static func throwUnhandledException() {
		NativeAOTLibraryTest_UnhandledExceptionTest_ThrowUnhandledException()
	}
}
