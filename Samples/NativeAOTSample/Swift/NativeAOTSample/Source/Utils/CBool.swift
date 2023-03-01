import Foundation

extension CBool {
	var boolValue: Bool {
		switch self {
			case .yes:
				return true
			case .no:
				return false
			default:
				fatalError("Invalid CBool value: \(self.rawValue)")
		}
	}
}
