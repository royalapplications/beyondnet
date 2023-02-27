import Foundation

public class UnhandledExceptionTest { }

// MARK: - Public API
public extension UnhandledExceptionTest {
	static func throwUnhandledException() {
		NativeAOTLibraryTest_UnhandledExceptionTest_ThrowUnhandledException()
	}
}
