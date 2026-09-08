import XCTest
import BeyondDotNETSampleKit

final class WebProxyTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }

    override class func tearDown() {
        Self.sharedTearDown()
    }

    func testWebProxy() throws {
        let dnProxy = try Beyond_NET_Sample_WebProxyTests.createWebProxy()
        let dnCredential = try dnProxy.credentials
        XCTAssertNil(dnCredential)
        
        let proxy = try System_Net_WebProxy()
        let credential = try proxy.credentials
        XCTAssertNil(credential)
    }
}

