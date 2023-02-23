import Foundation

public extension SystemException {
	class Error {
		public let exception: SystemException
		
		public init(exception: SystemException) {
			self.exception = exception
		}
	}
}

public extension SystemException.Error {
	var stackTrace: String? {
		exception.stackTrace
	}
	
	var hResult: Int32 {
		exception.hResult
	}
}

extension SystemException.Error: LocalizedError {
	public var errorDescription: String? {
		exception.message
	}
}
