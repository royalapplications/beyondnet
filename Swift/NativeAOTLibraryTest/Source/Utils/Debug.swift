import Foundation

public struct Debug {
	public static var isLoggingEnabled = false
	
	public static func log(_ logMessageCallback: (() -> String)) {
#if DEBUG
		guard isLoggingEnabled else { return }
		
		let message = logMessageCallback()
		
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
#endif
    }
}
