import Foundation
import XCTest
import NativeAOTCodeGeneratorOutputSample

extension XCTestCase {
	@MainActor
	class func gcCollect() {
		var exception: System_Exception_t?
		
		System_GC_Collect_1(&exception)
		
		XCTAssertNil(exception)
	}
	
	@MainActor
	class func sharedSetUp() {
		gcCollect()
	}
	
	@MainActor
	class func sharedTearDown() {
		gcCollect()
	}
}
