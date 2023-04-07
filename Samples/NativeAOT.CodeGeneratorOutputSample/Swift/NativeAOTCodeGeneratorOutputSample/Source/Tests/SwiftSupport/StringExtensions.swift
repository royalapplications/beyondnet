import Foundation
import NativeAOTCodeGeneratorOutputSample

public extension String {
	func dotNETString() -> System_String_t {
		DNStringFromC(self)
	}
	
	init?(dotNETString: System_String_t?) {
		self.init(dotNETString: dotNETString,
				  destroyDotNETString: false)
	}
	
	init?(dotNETString: System_String_t?,
		  destroyDotNETString: Bool) {
		guard let cString = DNStringToC(dotNETString) else {
			return nil
		}
		
		defer {
			cString.deallocate()
			
			if destroyDotNETString {
				System_String_Destroy(dotNETString)
			}
		}
		
		self.init(cString: cString)
	}
}
