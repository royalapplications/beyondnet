import XCTest
import BeyondNETSampleSwift

// TODO: Missing C Test
final class SystemUriTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testUriCreationOptions() {
        guard let creationOptions = try? System.UriCreationOptions() else {
			XCTFail("System.UriCreationOptions ctor should not throw and return an instance")
			
			return
		}
		
        let type = System.UriCreationOptions.typeOf
		
		guard let typeRet = try? creationOptions.getType() else {
			XCTFail("System.UriCreationOptions.GetType should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(type == typeRet)
		
		let value = true

		do {
			try creationOptions.dangerousDisablePathAndQueryCanonicalization_set(value)
		} catch {
			XCTFail("System.UriCreationOptions.DangerousDisablePathAndQueryCanonicalization setter should not throw")

			return
		}

		do {
			let valueRet = try creationOptions.dangerousDisablePathAndQueryCanonicalization

			XCTAssertEqual(value, valueRet)
		} catch {
			XCTFail("System.UriCreationOptions.DangerousDisablePathAndQueryCanonicalization getter should not throw")

			return
		}
	}
	
	func testTryCreateUriWithInParameter() {
		let urlString = "https://royalapps.com/"
		
        var creationOptions = try? System.UriCreationOptions()
		
		var uriRet: System_Uri?
		
		do {
            let success = try System.Uri.tryCreate(urlString.dotNETString(),
												   &creationOptions,
												   &uriRet)
			
			XCTAssertTrue(success)
		} catch {
			XCTFail("System.Uri.TryCreate should not throw, return true and an System.Uri object as out parameter")
			
			return
		}
		
		guard let uriRet else {
			XCTFail("System.Uri.TryCreate should not throw, return true and an System.Uri object as out parameter")
			
			return
		}
		
		guard let absoluteUriString = try? uriRet.absoluteUri?.string() else {
			XCTFail("System.Uri.AbsoluteUri should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(urlString, absoluteUriString)
	}
}
