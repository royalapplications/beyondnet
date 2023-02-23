import Foundation

public struct Debug {
	public static var isLoggingEnabled = false
	
	public static func log(_ message: String) {
#if DEBUG
		guard isLoggingEnabled else { return }
		
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
#endif
    }
}
