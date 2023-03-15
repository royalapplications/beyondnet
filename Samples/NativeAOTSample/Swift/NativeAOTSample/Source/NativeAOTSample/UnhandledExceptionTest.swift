import Foundation

public class UnhandledExceptionTest: System.Object {
    override class var type: System._Type {
		.init(handle: NativeAOTSample_UnhandledExceptionTest_TypeOf())
	}
}

// MARK: - Public API
public extension UnhandledExceptionTest {
	static func throwUnhandledException() {
		NativeAOTSample_UnhandledExceptionTest_ThrowUnhandledException()
	}
}
