import Foundation

extension CStatus {
	var boolValue: Bool {
		switch self {
			case .success:
				return true
			case .failure:
				return false
			default:
				fatalError("Invalid CStatus value: \(self.rawValue)")
		}
	}
}
