import Foundation

public struct Debug {
	private static let debugPrefix = "[DEBUG] "
	
	public static var isLoggingEnabled = false
	
	public static func log(_ logMessageProvider: @autoclosure () -> String) {
#if DEBUG
		guard isLoggingEnabled else { return }
		
		let message = logMessageProvider()
		
        let fullMessage = "\(debugPrefix)\(message)"
        print(fullMessage)
#endif
    }
}
