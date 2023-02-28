import XCTest
@testable import NativeAOTSample

final class SystemAppDomainTests: XCTestCase {
	func testAppDomain() {
		let appDomain = SystemAppDomain.current()
		
		XCTAssertGreaterThanOrEqual(Int32(1), appDomain.id)
		XCTAssertTrue(appDomain.isDefault())
		XCTAssertFalse(appDomain.baseDirectory.isEmpty)
		
		// MARK: - These are actually System.Type/System.Object tests and should be moved
		let type = appDomain.type
		
		XCTAssertEqual("AppDomain", type.name)
		XCTAssertEqual("System.AppDomain", type.fullName)
		
		guard let systemAppDomainType = SystemType(typeName: "System.AppDomain") else {
			XCTFail("Failed to get type for System.AppDomain")
			
			return
		}
		
		XCTAssertEqual(type, systemAppDomainType)
		
		XCTAssertTrue(type.isAssignableTo(systemAppDomainType))
		XCTAssertTrue(type.isAssignableFrom(systemAppDomainType))
		
		let systemObjectType = SystemObject.type
		
		XCTAssertTrue(type.isAssignableTo(systemObjectType))
		XCTAssertFalse(type.isAssignableFrom(systemObjectType))
		
		XCTAssertFalse(systemObjectType.isAssignableTo(type))
		XCTAssertTrue(systemObjectType.isAssignableFrom(type))
		
		guard let appDomainAsSystemObject = appDomain.cast(as: SystemObject.self) else {
			XCTFail("Failed to cast System.AppDomain to System.Object")
			
			return
		}
		
		XCTAssertEqual(appDomain.type, appDomainAsSystemObject.type)
		
		guard let castedAppDomain = appDomain.cast(as: SystemAppDomain.self) else {
			XCTFail("Failed to cast System.AppDomain to System.AppDomain")
			
			return
		}
		
		XCTAssertEqual(appDomain.type, castedAppDomain.type)
	}
}
