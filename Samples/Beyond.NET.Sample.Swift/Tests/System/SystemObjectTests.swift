import XCTest
import BeyondDotNETSampleKit

final class SystemObjectTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemObject() throws {
        let systemObjectType = System.Object.typeOf
        
        let object1 = try System.Object()
        let object1Type = try object1.getType()
        
        guard systemObjectType == object1Type else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        let object2 = try System.Object()
        
        guard object1 != object2 else {
            XCTFail("System.Object.Equals should not throw and return false")
            
            return
        }
        
        guard object1 !== object2 else {
            XCTFail("System.Object.ReferenceEquals should not throw and return false")
            
            return
        }
    }
    
    func testCreatingAndDestroyingManyObjects() {
        measure {
            let numberOfObjects = 10_000

            for _ in 0..<numberOfObjects {
                guard let _ = try? System.Object() else {
                    XCTFail("System.Object ctor should not throw and return an instance")

                    return
                }
            }
            
            do {
                try System.GC.collect()
            } catch {
                XCTFail("System.GC.Collect should not throw")
                
                return
            }
        }
    }
    
    func testCreatingAndDestroyingManyObjectsByInstantlyCollectingGC() {
        measure {
            let numberOfObjects = 500

            for _ in 0..<numberOfObjects {
                {
                    guard let _ = try? System.Object() else {
                        XCTFail("System.Object ctor should not throw and return an instance")
                        
                        return
                    }
                }()
                
                do {
                    try System.GC.collect()
                } catch {
                    XCTFail("System.GC.Collect should not throw")
                    
                    return
                }
                
                do {
                    try System.GC.waitForPendingFinalizers()
                } catch {
                    XCTFail("System.GC.WaitForPendingFinalizers should not throw")
                    
                    return
                }
            }
        }
    }
}
