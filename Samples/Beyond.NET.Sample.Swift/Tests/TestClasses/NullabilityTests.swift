import XCTest
import BeyondDotNETSampleKit

final class NullabilityTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testNullability() throws {
        let test = try XCTUnwrap(Beyond.NET.Sample.NullabilityTests())
        
        let helloString = "Hello".dotNETString()
        let nilString: System.String? = nil
        
        XCTAssertEqual(try test.methodWithNonNullableStringParameter(helloString), helloString)
        XCTAssertEqual(try test.methodWithNullableStringParameter(nilString), nilString)
        
        XCTAssertEqual(try test.nonNullableStringProperty, helloString)
        XCTAssertEqual(try test.nullableStringProperty, nilString)
        
        XCTAssertEqual(try test.methodWithNonNullableStringReturnValue(), helloString)
        XCTAssertEqual(try test.methodWithNullableStringReturnValue(), nilString)
        
        // TODO: Test Fields
    }
}
