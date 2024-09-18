import XCTest
import BeyondDotNETSampleKit

final class ComparisonTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testObjectComparisons() throws {
        let optionalObject: System.Object? = try System.Object()
        
        guard let nonOptionalObject = optionalObject else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        let anotherOptionalObject: System.Object? = try System_Object()
        
        guard let anotherNonOptionalObject = anotherOptionalObject else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(optionalObject == optionalObject)
        XCTAssertFalse(optionalObject != optionalObject)
        XCTAssertTrue(optionalObject === optionalObject)
        XCTAssertFalse(optionalObject !== optionalObject)
        
        XCTAssertTrue(optionalObject == nonOptionalObject)
        XCTAssertFalse(optionalObject != nonOptionalObject)
        XCTAssertTrue(optionalObject === nonOptionalObject)
        XCTAssertFalse(optionalObject !== nonOptionalObject)
        
        XCTAssertFalse(optionalObject == anotherOptionalObject)
        XCTAssertTrue(optionalObject != anotherOptionalObject)
        XCTAssertFalse(optionalObject === anotherOptionalObject)
        XCTAssertTrue(optionalObject !== anotherOptionalObject)
        
        XCTAssertFalse(nonOptionalObject == anotherOptionalObject)
        XCTAssertTrue(nonOptionalObject != anotherOptionalObject)
        XCTAssertFalse(nonOptionalObject === anotherOptionalObject)
        XCTAssertTrue(nonOptionalObject !== anotherOptionalObject)
        
        XCTAssertFalse(optionalObject == anotherNonOptionalObject)
        XCTAssertTrue(optionalObject != anotherNonOptionalObject)
        XCTAssertFalse(optionalObject === anotherNonOptionalObject)
        XCTAssertTrue(optionalObject !== anotherNonOptionalObject)
        
        XCTAssertFalse(nonOptionalObject == anotherNonOptionalObject)
        XCTAssertTrue(nonOptionalObject != anotherNonOptionalObject)
        XCTAssertFalse(nonOptionalObject === anotherNonOptionalObject)
        XCTAssertTrue(nonOptionalObject !== anotherNonOptionalObject)
    }
    
    func testPrimitiveComparisons() {
        let int32Value: Int32 = 1234
        let boxedInt32Value = int32Value.dotNETObject()
        
        let sameInt32Value: Int32 = 1234
        let boxedSameInt32Value = sameInt32Value.dotNETObject()
        
        let otherInt32Value: Int32 = 4321
        let boxedOtherInt32Value = otherInt32Value.dotNETObject()
        
        XCTAssertTrue(boxedInt32Value == boxedSameInt32Value)
        XCTAssertTrue(boxedInt32Value as System.Object? == boxedSameInt32Value)
        XCTAssertTrue(boxedInt32Value as System.Object? == boxedSameInt32Value as System.Object?)
        XCTAssertFalse(boxedInt32Value === boxedSameInt32Value)
        XCTAssertFalse(boxedInt32Value === boxedSameInt32Value as System.Object?)
        XCTAssertFalse(boxedInt32Value as System.Object? === boxedSameInt32Value as System.Object?)
        XCTAssertFalse(boxedInt32Value != boxedSameInt32Value)
        XCTAssertFalse(boxedInt32Value as System.Object? != boxedSameInt32Value)
        XCTAssertFalse(boxedInt32Value as System.Object? != boxedSameInt32Value as System.Object?)
        XCTAssertTrue(boxedInt32Value !== boxedSameInt32Value)
        XCTAssertTrue(boxedInt32Value !== boxedSameInt32Value as System.Object?)
        XCTAssertTrue(boxedInt32Value as System.Object? !== boxedSameInt32Value as System.Object?)
        
        XCTAssertFalse(boxedInt32Value == boxedOtherInt32Value)
        XCTAssertFalse(boxedInt32Value as System.Object? == boxedOtherInt32Value)
        XCTAssertFalse(boxedInt32Value as System.Object? == boxedOtherInt32Value as System.Object?)
        XCTAssertFalse(boxedInt32Value === boxedOtherInt32Value)
        XCTAssertFalse(boxedInt32Value === boxedOtherInt32Value as System.Object?)
        XCTAssertFalse(boxedInt32Value as System.Object? === boxedOtherInt32Value as System.Object?)
        XCTAssertTrue(boxedInt32Value != boxedOtherInt32Value)
        XCTAssertTrue(boxedInt32Value as System.Object? != boxedOtherInt32Value)
        XCTAssertTrue(boxedInt32Value as System.Object? != boxedOtherInt32Value as System.Object?)
        XCTAssertTrue(boxedInt32Value !== boxedOtherInt32Value)
        XCTAssertTrue(boxedInt32Value !== boxedOtherInt32Value as System.Object?)
        XCTAssertTrue(boxedInt32Value as System.Object? !== boxedOtherInt32Value as System.Object?)
    }
    
    func testGuidComparison() {
        let uuid1 = UUID()
        let uuid2 = UUID()
        
        guard let guid1 = uuid1.dotNETGuid() else {
            XCTFail("Converting a Swift UUID to a System.Guid should return an instance")
            
            return
        }
        
        guard let guid1Copy = uuid1.dotNETGuid() else {
            XCTFail("Converting a Swift UUID to a System.Guid should return an instance")
            
            return
        }
        
        guard let guid2 = uuid2.dotNETGuid() else {
            XCTFail("Converting a Swift UUID to a System.Guid should return an instance")
            
            return
        }
        
        XCTAssertTrue(guid1 == guid1Copy)
        XCTAssertFalse(guid1 != guid1Copy)
        XCTAssertFalse(guid1 === guid1Copy)
        XCTAssertTrue(guid1 !== guid1Copy)
        
        XCTAssertFalse(guid1 == guid2)
        XCTAssertTrue(guid1 != guid2)
        XCTAssertFalse(guid1 === guid2)
        XCTAssertTrue(guid1 !== guid2)
    }
}
