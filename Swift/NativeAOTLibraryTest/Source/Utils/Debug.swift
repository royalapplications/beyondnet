import Foundation

struct Debug {
	static var isLoggingEnabled = true
	
    static func log(_ message: String) {
#if DEBUG
		guard isLoggingEnabled else { return }
		
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
#endif
    }
}
