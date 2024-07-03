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
    
    func testPassingInInterfaces() throws {
        let typeThatImplementsMultipleInterfaces = try Beyond.NET.Sample.TypeThatImplementsMultipleInterfaces()
        let typeThatUsesInterfaces = try Beyond.NET.Sample.TypeThatUsesInterfaces()
        
        let val: Int32 = 5
        try typeThatUsesInterfaces.callMethod1InIInterface1(typeThatImplementsMultipleInterfaces)
        try typeThatUsesInterfaces.setPropertyInIInterface2(typeThatImplementsMultipleInterfaces, val)
        let retVal = try typeThatUsesInterfaces.getPropertyInIInterface2(typeThatImplementsMultipleInterfaces)
        XCTAssertEqual(val, retVal)
        try typeThatUsesInterfaces.callMethod1InIInterface3(typeThatImplementsMultipleInterfaces)
    }
    
    func testRetrievingInterfaces() throws {
        let typeThatUsesInterfaces = try Beyond.NET.Sample.TypeThatUsesInterfaces()
        
        let interface1 = try typeThatUsesInterfaces.getTypeThatImplementsIInterface1()
        try interface1.methodInIInterface1()
        
        let interface2 = try typeThatUsesInterfaces.getTypeThatImplementsIInterface2()
        try interface2.propertyInIInterface2_set(42)
        
        let interface3 = try typeThatUsesInterfaces.getTypeThatImplementsIInterface3()
        try interface3.methodInIInterface3()
        
        XCTAssertTrue(interface1.is(Beyond.NET.Sample.IInterface1_DNInterface.typeOf))
        XCTAssertTrue(interface2.is(Beyond.NET.Sample.IInterface2_DNInterface.typeOf))
        XCTAssertTrue(interface3.is(Beyond.NET.Sample.IInterface3_DNInterface.typeOf))
    }
    
    func testInterfaceAdapter() throws {
        let typeThatUsesInterfaces = try Beyond.NET.Sample.TypeThatUsesInterfaces()
        
        let methodInIInterface1CalledExpectation = expectation(description: "IInterface1.MethodInIInterface1 called in Swift")
        
        // Compiler ensures we provide all interface requirements.
        // Ideally, we'd have an auto-generated Swift wrapper type that sets up all the delegate -> closure callbacks and allows us to just override the members required to satisfy the interface/protocol requirements.
        let interface1Adapter = try Beyond.NET.Sample.IInterface1_DelegateAdapter(.init({
            print("IInterface1.MethodInIInterface1 called in Swift")
            
            methodInIInterface1CalledExpectation.fulfill()
        }))
        
        try typeThatUsesInterfaces.callMethod1InIInterface1(interface1Adapter)
        
        wait(for: [ methodInIInterface1CalledExpectation ])
    }
}
