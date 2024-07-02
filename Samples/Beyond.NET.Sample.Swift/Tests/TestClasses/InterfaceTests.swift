import XCTest
import BeyondDotNETSampleKit

final class InterfaceTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testInterfaces() throws {
        let typeThatImplementsMultipleInterfaces = try Beyond_NET_Sample_TypeThatImplementsMultipleInterfaces()
        let typeThatUsesInterfaces = try Beyond_NET_Sample_TypeThatUsesInterfaces()
        
        let interface1: Beyond_NET_Sample_IInterface1 = try typeThatImplementsMultipleInterfaces.castTo()
        let interface2: Beyond_NET_Sample_IInterface2 = try typeThatImplementsMultipleInterfaces.castTo()
        let interface3: Beyond_NET_Sample_IInterface3 = try typeThatImplementsMultipleInterfaces.castTo()
        
        let val: Int32 = 5
        try typeThatUsesInterfaces.callMethod1InIInterface1(interface1)
        try typeThatUsesInterfaces.setPropertyInIInterface2(interface2, val)
        let retVal = try typeThatUsesInterfaces.getPropertyInIInterface2(interface2)
        XCTAssertEqual(val, retVal)
        try typeThatUsesInterfaces.callMethod1InIInterface3(interface3)
    }
}
