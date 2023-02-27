import XCTest
@testable import NativeAOTLibraryTest

final class SystemAppDomainTests: XCTestCase {
	func testAppDomain() {
		let appDomain = SystemAppDomain.current()
		
		XCTAssertGreaterThanOrEqual(Int32(1), appDomain.id)
		XCTAssertTrue(appDomain.isDefault())
		XCTAssertFalse(appDomain.baseDirectory.isEmpty)
	}
}
