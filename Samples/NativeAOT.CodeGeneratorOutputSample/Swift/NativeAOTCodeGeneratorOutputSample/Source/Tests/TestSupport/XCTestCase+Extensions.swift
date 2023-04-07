import Foundation
import XCTest
import NativeAOTCodeGeneratorOutputSample

extension XCTestCase {
	class func gcCollect() {
		var exception: System_Exception_t?
		
		System_GC_Collect_1(&exception)
		
		XCTAssertNil(exception)
	}
}
