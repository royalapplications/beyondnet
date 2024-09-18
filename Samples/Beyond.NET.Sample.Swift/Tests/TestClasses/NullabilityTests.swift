import XCTest
import BeyondDotNETSampleKit

final class NullabilityTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testMethodPropertyAndFieldNullability() throws {
        let test = try XCTUnwrap(Beyond.NET.Sample.NullabilityTests())
        
        let helloString = "Hello".dotNETString()
        let nilString: System.String? = nil
        
        XCTAssertEqual(try test.methodWithNonNullableStringParameter(helloString), helloString)
        XCTAssertEqual(try test.methodWithNullableStringParameter(nilString), nilString)
        
        XCTAssertEqual(try test.nonNullableStringProperty, helloString)
        XCTAssertEqual(test.nonNullableStringField, helloString)
        XCTAssertEqual(try test.nullableStringProperty, nilString)
        XCTAssertEqual(test.nullableStringField, nilString)
        
        XCTAssertEqual(try test.methodWithNonNullableStringReturnValue(), helloString)
        XCTAssertEqual(try test.methodWithNullableStringReturnValue(), nilString)
    }
    
    func testConstructor() throws {
        let _ = try Beyond.NET.Sample.NullabilityTests(false) // Should not throw
        XCTAssertThrowsError(try Beyond.NET.Sample.NullabilityTests(true)) // Should throw
    }
}
