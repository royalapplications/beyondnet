import Foundation

public struct Debug {
	private static let debugPrefix = "[DEBUG] "
	
	public static var isLoggingEnabled = false
	
	public static func log(_ logMessageCallback: (() -> String)) {
#if DEBUG
		guard isLoggingEnabled else { return }
		
		let message = logMessageCallback()
		
        let fullMessage = "\(debugPrefix)\(message)"
        print(fullMessage)
#endif
    }
}
