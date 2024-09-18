import XCTest
import BeyondDotNETSampleKit

final class SystemWeakReferenceTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testWeakReferenceToLongLivedObject() throws {
        let anObject = try System.Object()
        
        guard let weakRef = weakReferenceToObject(anObject) else {
            XCTFail("System.WeakReference ctor should not throw and return an instance")
            
            return
        }
        
        try System.GC.collect()
        
        let isAlive = try weakRef.isAlive
        let target = try weakRef.target
        
        XCTAssertTrue(isAlive)
        
        guard let unwrappedTarget = target else {
            XCTFail("We should have a target object")
            
            return
        }
        
        let isTargetSameAsObject = try System.Object.referenceEquals(anObject, target)
        let isUnwrappedTargetSameAsObjectUsingBuiltInComparison = anObject === unwrappedTarget
        let isUnwrappedTargetNotSameAsObjectUsingBuiltInComparison = anObject !== unwrappedTarget
        let isTargetSameAsObjectUsingBuiltInComparison = anObject === target
        let isTargetNotSameAsObjectUsingBuiltInComparison = anObject !== target
        
        XCTAssertTrue(isTargetSameAsObject)
        XCTAssertTrue(isUnwrappedTargetSameAsObjectUsingBuiltInComparison)
        XCTAssertFalse(isUnwrappedTargetNotSameAsObjectUsingBuiltInComparison)
        XCTAssertTrue(isTargetSameAsObjectUsingBuiltInComparison)
        XCTAssertFalse(isTargetNotSameAsObjectUsingBuiltInComparison)
    }
    
    func testWeakReferenceToTemporaryObject() {
        guard let weakRef = weakReferenceToTemporaryObject() else {
            XCTFail("System.WeakReference ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try System.GC.collect())
        
        let isAlive = (try? weakRef.isAlive) ?? false
        
        XCTAssertFalse(isAlive)
    }
}

private extension SystemWeakReferenceTests {
    func weakReferenceToObject(_ targetObject: System.Object) -> System.WeakReference? {
        guard let weakRef = try? System.WeakReference(targetObject) else {
            XCTFail("System.WeakReference ctor should not throw and return an instance")
            
            return nil
        }
        
        return weakRef
    }
    
    func weakReferenceToTemporaryObject() -> System.WeakReference? {
        guard let tempObject = try? System.Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return nil
        }
        
        let weakRef = weakReferenceToObject(tempObject)
        
        return weakRef
    }
}
