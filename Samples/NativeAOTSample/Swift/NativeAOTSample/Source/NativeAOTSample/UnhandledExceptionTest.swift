import Foundation

public class UnhandledExceptionTest: SystemObject {
	override class var type: SystemType {
		.init(handle: NativeAOTSample_UnhandledExceptionTest_TypeOf())
	}
}

// MARK: - Public API
public extension UnhandledExceptionTest {
	static func throwUnhandledException() {
		NativeAOTSample_UnhandledExceptionTest_ThrowUnhandledException()
	}
}
