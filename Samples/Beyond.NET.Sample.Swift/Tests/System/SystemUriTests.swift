import XCTest
import BeyondDotNETSampleKit

final class SystemUriTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testUriCreationOptions() throws {
        let creationOptions = try System.UriCreationOptions()
        let type = System.UriCreationOptions.typeOf
		let typeRet = try creationOptions.getType()
		XCTAssertTrue(type == typeRet)
		
		let value = true

        try creationOptions.dangerousDisablePathAndQueryCanonicalization_set(value)
        let valueRet = try creationOptions.dangerousDisablePathAndQueryCanonicalization
        XCTAssertEqual(value, valueRet)
	}
	
	func testTryCreateUriWithInParameter() throws {
		let urlString = "https://royalapps.com/"
		
        var creationOptions = try System.UriCreationOptions()
		
		var uriRet: System_Uri?
        
        guard try System.Uri.tryCreate(urlString.dotNETString(),
                                       &creationOptions,
                                       &uriRet),
              let uriRet else {
            XCTFail("System.Uri.TryCreate should not throw, return true and an System.Uri object as out parameter")
            
            return
        }
		
		let absoluteUriString = try uriRet.absoluteUri.string()
		XCTAssertEqual(urlString, absoluteUriString)
        
        guard let url = URL(string: absoluteUriString) else {
            XCTFail("Should be able to init a Swift URL with a .NET URI String")
            
            return
        }
        
        let absoluteURLString = url.absoluteString
        
        XCTAssertEqual(absoluteUriString, absoluteURLString)
	}
}
