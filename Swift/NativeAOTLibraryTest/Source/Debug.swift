import Foundation

struct Debug {
    static func log(_ message: String) {
#if DEBUG
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
#endif
    }
}
