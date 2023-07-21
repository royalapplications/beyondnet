import XCTest
import BeyondNETSampleSwift

// TODO: Missing C Test
final class TestRecordTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testRecords() {
        let expectedString = "Hello 👍"
        
        guard let aRecord = try? Beyond.NET.Sample.TestRecord(expectedString.dotNETString()) else {
            XCTFail("Beyond.NET.Sample.TestRecord ctor should not throw and return an instance")
            
            return
        }
        
        guard let retString = try? aRecord.aString?.string() else {
            XCTFail("Beyond.NET.Sample.TestRecord AString getter should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual(expectedString, retString)
        
        var deconstructedStringDN: System.String?
        
        XCTAssertNoThrow(try aRecord.deconstruct(&deconstructedStringDN))
        
        guard let deconstructedString = deconstructedStringDN?.string() else {
            XCTFail("Failed to convert a System.String to a swift string")
            
            return
        }
        
        XCTAssertEqual(expectedString, deconstructedString)
    }
    
    func testReadOnlyRecordStruct() {
        let expectedInt: Int32 = .max
        
        guard let aRecord = try? Beyond.NET.Sample.TestReadOnlyRecordStruct(expectedInt) else {
            XCTFail("Beyond.NET.Sample.TestReadOnlyRecordStruct ctor should not throw and return an instance")
            
            return
        }
        
        guard let retInt = try? aRecord.anInt else {
            XCTFail("Beyond.NET.Sample.TestReadOnlyRecordStruct AnInt getter should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual(expectedInt, retInt)
    }
}
