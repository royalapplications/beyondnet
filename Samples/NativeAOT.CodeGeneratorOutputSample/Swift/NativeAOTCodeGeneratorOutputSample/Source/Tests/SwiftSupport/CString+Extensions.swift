import Foundation
import NativeAOTCodeGeneratorOutputSample

extension CString? {
	func string(deallocateCString: Bool = true) -> String? {
		guard let self else {
			return nil
		}
		
		return self.string(deallocateCString: deallocateCString)
	}
}

extension CString {
	func string(deallocateCString: Bool = true) -> String {
		let str = String(cString: self)
		
		defer {
			if deallocateCString {
				self.deallocate()
			}
		}
		
		return str
	}
}
