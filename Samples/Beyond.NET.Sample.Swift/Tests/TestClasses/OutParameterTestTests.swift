import XCTest
import BeyondDotNETSampleKit

final class OutParameterTestTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    private func makeInstance() throws -> Beyond.NET.Sample.Source.OutParameterTests {
        try Beyond.NET.Sample.Source.OutParameterTests()
    }
    
    // MARK: - Primitives
    func testNonOptionalPrimitives() throws {
        let inst = try makeInstance()
        
        var returnValue: Int32 = 0
        try inst.return_Int_1_NonOptional(&returnValue)
        XCTAssertEqual(returnValue, 1)
    }
    
    // NOTE: Beyond.NET does not currently support nullable primitives. That's why the other test cases are not implemented.
//    func testOptionalPrimitives() throws { }
//    func testOptionalNullPrimitives() throws { }
    
    // MARK: - Enums
    func testNonOptionalEnums() throws {
        let inst = try makeInstance()
        
        var returnValue = System.DateTimeKind.unspecified
        try inst.return_DateTimeKind_Utc_NonOptional(&returnValue)
        XCTAssertEqual(returnValue, System.DateTimeKind.utc)
    }
    
    // NOTE: Beyond.NET does not currently support nullable enums. That's why the other test cases are not implemented.
//    func testOptionalEnums() throws { }
//    func testOptionalNullEnums() throws { }
    
    // MARK: - Structs
    func testNonOptionalStructs() throws {
        let inst = try makeInstance()
        
        var returnValue = System.DateTime.minValue
        try inst.return_DateTime_MaxValue_NonOptional(&returnValue)
        XCTAssertEqual(returnValue, System.DateTime.maxValue)
        
        var returnValueWithPlaceholder = System.DateTime.outParameterPlaceholder
        try inst.return_DateTime_MaxValue_NonOptional(&returnValueWithPlaceholder)
        XCTAssertEqual(returnValueWithPlaceholder, System.DateTime.maxValue)
    }
    
    func testOptionalStructs() throws {
        let inst = try makeInstance()
        
        var returnValue: System.DateTime?
        try inst.return_DateTime_MaxValue_Optional(&returnValue)
        XCTAssertEqual(returnValue, System.DateTime.maxValue)
        
        var returnValueWithInValue: System.DateTime? = .minValue
        try inst.return_DateTime_MaxValue_Optional(&returnValueWithInValue)
        XCTAssertEqual(returnValueWithInValue, System.DateTime.maxValue)
        
        var returnValueWithPlaceholder: System.DateTime? = System.DateTime.outParameterPlaceholder
        try inst.return_DateTime_MaxValue_Optional(&returnValueWithPlaceholder)
        XCTAssertEqual(returnValueWithPlaceholder, System.DateTime.maxValue)
    }
    
    func testOptionalNullStructs() throws {
        let inst = try makeInstance()
        
        var returnValue: System.DateTime?
        try inst.return_DateTime_Null(&returnValue)
        XCTAssertNil(returnValue)
        
        var returnValueWithInValue: System.DateTime? = .minValue
        try inst.return_DateTime_Null(&returnValueWithInValue)
        XCTAssertNil(returnValueWithInValue)
        
        var returnValueWithPlaceholder: System.DateTime? = System.DateTime.outParameterPlaceholder
        try inst.return_DateTime_Null(&returnValueWithPlaceholder)
        XCTAssertNil(returnValueWithPlaceholder)
    }
    
    // MARK: - Classes
    func testNonOptionalClasses() throws {
        let inst = try makeInstance()
        
        let abc = "Abc".dotNETString()
        
        var returnValue = System.String.empty
        try inst.return_String_Abc_NonOptional(&returnValue)
        XCTAssertEqual(returnValue, abc)
        
        var returnValueWithPlaceholder = System.String.outParameterPlaceholder
        try inst.return_String_Abc_NonOptional(&returnValueWithPlaceholder)
        XCTAssertEqual(returnValueWithPlaceholder, abc)
    }
    
    func testOptionalClasses() throws {
        let inst = try makeInstance()
        
        let abc = "Abc".dotNETString()
        
        var returnValue: System.String?
        try inst.return_String_Abc_Optional(&returnValue)
        XCTAssertEqual(returnValue, abc)
        
        var returnValueWithInValue: System.String? = .empty
        try inst.return_String_Abc_Optional(&returnValueWithInValue)
        XCTAssertEqual(returnValue, abc)
        
        var returnValueWithPlaceholder: System.String? = System.String.outParameterPlaceholder
        try inst.return_String_Abc_Optional(&returnValueWithPlaceholder)
        XCTAssertEqual(returnValue, abc)
    }
    
    func testOptionalNullClasses() throws {
        let inst = try makeInstance()
        
        var returnValue: System.String?
        try inst.return_String_Null(&returnValue)
        XCTAssertNil(returnValue)
        
        var returnValueWithInValue: System.String? = .empty
        try inst.return_String_Null(&returnValueWithInValue)
        XCTAssertNil(returnValueWithInValue)
        
        var returnValueWithPlaceholder: System.String? = System.String.outParameterPlaceholder
        try inst.return_String_Null(&returnValueWithPlaceholder)
        XCTAssertNil(returnValueWithPlaceholder)
    }
    
    // MARK: - Interfaces
    func testNonOptionalInterfaces() throws {
        let inst = try makeInstance()
        
        let abc = "Abc".dotNETString()
        
        var returnValue: System.Collections.IEnumerable = System.String.empty
        try inst.return_IEnumerable_String_Abc_NonOptional(&returnValue)
        XCTAssertEqual(try returnValue.castTo(System.String.self), abc)
        
        var returnValueWithPlaceholder: System.Collections.IEnumerable = System.Collections.IEnumerable_DNInterface.outParameterPlaceholder
        try inst.return_IEnumerable_String_Abc_NonOptional(&returnValueWithPlaceholder)
        XCTAssertEqual(try returnValueWithPlaceholder.castTo(System.String.self), abc)
    }
    
    func testOptionalInterfaces() throws {
        let inst = try makeInstance()
        
        let abc = "Abc".dotNETString()
        
        var returnValue: System.Collections.IEnumerable?
        try inst.return_IEnumerable_String_Abc_Optional(&returnValue)
        XCTAssertEqual(try returnValue?.castTo(System.String.self), abc)
        
        var returnValueWithInValue: System.Collections.IEnumerable? = System.String.empty
        try inst.return_IEnumerable_String_Abc_Optional(&returnValueWithInValue)
        XCTAssertEqual(try returnValueWithInValue?.castTo(System.String.self), abc)
        
        var returnValueWithPlaceholder: System.Collections.IEnumerable? = System.String.outParameterPlaceholder
        try inst.return_IEnumerable_String_Abc_Optional(&returnValueWithPlaceholder)
        XCTAssertEqual(try returnValueWithPlaceholder?.castTo(System.String.self), abc)
    }
    
    func testOptionalNullInterfaces() throws {
        let inst = try makeInstance()
        
        var returnValue: System.Collections.IEnumerable?
        try inst.return_IEnumerable_Null(&returnValue)
        XCTAssertNil(returnValue)
        
        var returnValueWithInValue: System.Collections.IEnumerable? = System.String.empty
        try inst.return_IEnumerable_Null(&returnValueWithInValue)
        XCTAssertNil(returnValueWithInValue)
        
        var returnValueWithPlaceholder: System.Collections.IEnumerable? = System.Collections.IEnumerable_DNInterface.outParameterPlaceholder
        try inst.return_IEnumerable_Null(&returnValueWithPlaceholder)
        XCTAssertNil(returnValueWithPlaceholder)
    }
}
