import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemObjectTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.gcCollect()
	}
	
	@MainActor
	override class func tearDown() {
		Self.gcCollect()
	}
	
    func testSystemObject() {
        var exception: System_Exception_t?
        
        guard let systemObjectType = System_Object_TypeOf() else {
            XCTFail("typeof(System.Object should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemObjectType) }
        
        guard let object1 = System_Object_Create(&exception),
              exception == nil else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(object1) }
        
        guard let object1Type = System_Object_GetType(object1,
                                                      &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(object1Type) }
        
        guard System_Object_Equals(systemObjectType,
                                   object1Type,
                                   &exception),
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        guard let object2 = System_Object_Create(&exception),
              exception == nil else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(object2) }
        
		guard !System_Object_Equals(object1,
									object2,
									&exception),
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return false")
            
            return
        }
        
		guard !System_Object_ReferenceEquals(object1,
											 object2,
											 &exception),
              exception == nil else {
            XCTFail("System.Object.ReferenceEquals should not throw and return false")
            
            return
        }
    }
	
	func testCreatingAndDestroyingManyObjects() {
        measure {
            let numberOfObjects = 10_000
            var exception: System_Exception_t?
            
            for _ in 0..<numberOfObjects {
                guard let object = System_Object_Create(&exception),
                      exception == nil else {
                    XCTFail("System.Object ctor should not throw and return an instance")
                    
                    return
                }
                
                System_Object_Destroy(object)
            }
            
            System_GC_Collect_1(&exception)
            XCTAssertNil(exception)
        }
	}
	
	func testCreatingAndDestroyingManyObjectsByInstantlyCollectingGC() {
		measure {
			let numberOfObjects = 500
			var exception: System_Exception_t?
			
			for _ in 0..<numberOfObjects {
				guard let object = System_Object_Create(&exception),
					  exception == nil else {
					XCTFail("System.Object ctor should not throw and return an instance")
					
					return
				}
				
				System_Object_Destroy(object)
	
				System_GC_Collect_1(&exception)
				XCTAssertNil(exception)
				
				System_GC_WaitForPendingFinalizers(&exception)
				XCTAssertNil(exception)
			}
		}
	}
}
