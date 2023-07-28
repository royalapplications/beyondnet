import XCTest
import BeyondDotNETSampleKit

final class SystemWeakReferenceTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testWeakReferenceToLongLivedObject() {
        guard let anObject = try? System.Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        guard let weakRef = weakReferenceToObject(anObject) else {
            XCTFail("System.WeakReference ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try System.GC.collect())
        
        let isAlive = (try? weakRef.isAlive) ?? false
        let target = try? weakRef.target
        
        XCTAssertTrue(isAlive)
        
        // TODO: Why does comparison using === not yield the correct result here?
        // Is our Equatable conformance for System_Object not working?!
        XCTAssertTrue((try? System.Object.referenceEquals(anObject, target)) ?? false)
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
