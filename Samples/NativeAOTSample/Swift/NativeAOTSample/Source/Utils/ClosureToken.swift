import Foundation

public class ClosureToken<SwiftHandler, NativeHandler> {
	internal var closureBox: NativeBox<SwiftHandler>?
	internal var handler: NativeHandler?
	
	public var isValid: Bool {
		closureBox != nil &&
		handler != nil
	}
	
	internal init(closureBox: NativeBox<SwiftHandler>,
				  handler: NativeHandler) {
		self.closureBox = closureBox
		self.handler = handler
	}
	
	internal func invalidate(andReleasePointerToClosureBox pointerToClosureBox: UnsafeRawPointer?) {
		if let pointerToClosureBox {
			closureBox?.release(pointerToClosureBox)
		}
		
		self.closureBox = nil
		self.handler = nil
	}
	
	deinit {
		Debug.log("Deinitializing \(Self.self)")
	}
}
