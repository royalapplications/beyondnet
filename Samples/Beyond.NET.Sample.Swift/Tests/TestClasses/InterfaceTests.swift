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
    
    func testInterfaceAdapter() throws {
        let typeThatUsesInterfaces = try Beyond_NET_Sample_TypeThatUsesInterfaces()
        
        let methodInIInterface1CalledExpectation = expectation(description: "IInterface1.MethodInIInterface1 called in Swift")
        
        // Compiler ensures we provide all interface requirements.
        // Ideally, we'd have an auto-generated Swift wrapper type that sets up all the delegate -> closure callbacks and allows us to just override the members required to satisfy the interface/protocol requirements.
        let interface1Adapter = try Beyond_NET_Sample_IInterface1_DelegateAdapter(.init({
            print("IInterface1.MethodInIInterface1 called in Swift")
            
            methodInIInterface1CalledExpectation.fulfill()
        }))
        
        try typeThatUsesInterfaces.callMethod1InIInterface1(try interface1Adapter.castTo())
        
        wait(for: [ methodInIInterface1CalledExpectation ])
    }
}
