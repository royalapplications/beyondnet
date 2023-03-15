import Foundation

public extension System.Exception {
	class Error {
        public let exception: System.Exception
		
        public init(exception: System.Exception) {
			self.exception = exception
		}
	}
}

public extension System.Exception.Error {
	var stackTrace: String? {
		exception.stackTrace
	}
	
	var hResult: Int32 {
		exception.hResult
	}
}

extension System.Exception.Error: LocalizedError {
	public var errorDescription: String? {
		exception.message
	}
}
