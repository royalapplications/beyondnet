import Foundation
import BeyondNETSamplesSwift

public extension String {
	func cDotNETString() -> System_String_t {
		DNStringFromC(self)
	}
	
	init?(cDotNETString: System_String_t?) {
		self.init(cDotNETString: cDotNETString,
				  destroyDotNETString: false)
	}
	
	init?(cDotNETString: System_String_t?,
		  destroyDotNETString: Bool) {
		guard let cDotNETString else {
			return nil
		}
		
		defer {
			if destroyDotNETString {
				System_String_Destroy(cDotNETString)
			}
		}
		
		let cString = DNStringToC(cDotNETString)
		defer { cString.deallocate() }
		
		self.init(cString: cString)
	}
}