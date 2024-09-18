import XCTest
import BeyondDotNETSampleKit

final class TestRecordTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testRecords() throws {
        let expectedString = "Hello 👍"
        
        let aRecord = try Beyond.NET.Sample.TestRecord(expectedString.dotNETString())
        
        let retString = try aRecord.aString.string()
        XCTAssertEqual(expectedString, retString)
        
        var deconstructedStringDN = System.String.outParameterPlaceholder
        
        try aRecord.deconstruct(&deconstructedStringDN)
        
        let deconstructedString = deconstructedStringDN.string()
        
        XCTAssertEqual(expectedString, deconstructedString)
    }
    
    func testReadOnlyRecordStruct() throws {
        let expectedInt: Int32 = .max
        
        let aRecord = try Beyond.NET.Sample.TestReadOnlyRecordStruct(expectedInt)
        let retInt = try aRecord.anInt
        
        XCTAssertEqual(expectedInt, retInt)
    }
}
