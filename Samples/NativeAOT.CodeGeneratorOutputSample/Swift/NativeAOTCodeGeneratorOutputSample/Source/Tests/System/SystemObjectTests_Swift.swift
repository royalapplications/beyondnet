import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemObjectTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemObject() {
        var exception: System_Exception_t?
        
        guard let systemObjectType = System_Object.typeOf() else {
            XCTFail("typeof(System.Object) should return an instance")
            
            return
        }
        
        guard let object1 = try? System_Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        guard let object1Type = try? object1.getType(),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard (try? systemObjectType.equals(object1Type)) ?? false else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        guard let object2 = try? System_Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        guard !((try? object1.equals(object2)) ?? false),
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return false")
            
            return
        }
        
        guard !((try? System_Object.referenceEquals(object1,
                                                    object2)) ?? false) else {
            XCTFail("System.Object.ReferenceEquals should not throw and return false")
            
            return
        }
    }
    
    func testCreatingAndDestroyingManyObjects() {
        measure {
            let numberOfObjects = 10_000

            for _ in 0..<numberOfObjects {
                guard let _ = try? System_Object() else {
                    XCTFail("System.Object ctor should not throw and return an instance")

                    return
                }
            }
            
            do {
                try System_GC.collect()
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
                guard let _ = try? System_Object() else {
                    XCTFail("System.Object ctor should not throw and return an instance")

                    return
                }
                
                do {
                    try System_GC.collect()
                } catch {
                    XCTFail("System.GC.Collect should not throw")
                    
                    return
                }
                
                do {
                    try System_GC.waitForPendingFinalizers()
                } catch {
                    XCTFail("System.GC.WaitForPendingFinalizers should not throw")
                    
                    return
                }
            }
        }
    }
}
