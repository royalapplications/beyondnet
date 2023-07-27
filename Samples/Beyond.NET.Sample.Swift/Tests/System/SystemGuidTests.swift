import XCTest
import BeyondDotNETSampleKit

final class SystemGuidTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemGuid() {
        let uuid = UUID()
        
        let uuidString = uuid.uuidString
        let uuidStringDN = uuidString.dotNETString()
        
        guard let guid = try? System_Guid(uuidStringDN) else {
            XCTFail("System_Guid_Create2 should not throw and return an instance")
            
            return
        }
        
        guard let guidString = try? guid.toString()?.string() else {
            XCTFail("System_Guid_ToString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        let guidTypeName = "System.Guid"
        let guidTypeNameDN = guidTypeName.dotNETString()
        
        guard let guidType = try? System_Type.getType(guidTypeNameDN) else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        guard let guidTypeFromInstance = try? guid.getType() else {
            XCTFail("GetType should not throw and return something")
            
            return
        }
        
        let equals = guidType == guidTypeFromInstance
        XCTAssertTrue(equals)
        
        guard let emptyGuid = System_Guid.empty else {
            XCTFail("System.Guid.Empty getter should return an instance")
            
            return
        }
        
        guard let emptyGuidString = try? emptyGuid.toString()?.string() else {
            XCTFail("System.Guid.ToString should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual("00000000-0000-0000-0000-000000000000", emptyGuidString)
    }
    
    func testSystemGuidParsing() {
        let uuid = UUID()
        
        let uuidString = uuid.uuidString
        let uuidStringDN = uuidString.dotNETString()
        
        var guid: System_Guid?
        let success = (try? System_Guid.tryParse(uuidStringDN, &guid)) ?? false
        
        guard let guid,
              success else {
            XCTFail("System.Guid.TryParse should not throw and return an instance as out parameter")
            
            return
        }
        
        guard let uuidStringRet = try? guid.toString()?.string() else {
            XCTFail("System.Guid.ToString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
    }
    
    func testInvalidSystemGuidParsing() {
        guard let emptyGuid = System_Guid.empty else {
            XCTFail("Failed to get System.Guid.Empty")
            
            return
        }
        
        let uuidString = "nonsense"
        let uuidStringDN = uuidString.dotNETString()
        
        var guid: System_Guid?
        let success = (try? System_Guid.tryParse(uuidStringDN, &guid)) ?? false
        
        guard let guid,
              !success else {
            XCTFail("System.Guid.TryParse should not throw, the return value should be false and the returned instance as out parameter should be an empty System.Guid")
            
            return
        }
        
        let equal = emptyGuid == guid
        XCTAssertTrue(equal)
    }
    
    func testSwiftUUIDConversion() {
        let uuidString = "7F579986-D12F-4889-9A14-FDE340A59E08"
        
        var guid: System_Guid?
        
        guard (try? System_Guid.tryParse(uuidString.dotNETString(),
                                         &guid)) ?? false,
              let guid else {
            XCTFail("System.Guid.TryParse should not throw, return true and an instance as out parameter")
            
            return
        }
        
        guard let guidString = try? guid.toString()?.string() else {
            XCTFail("System.Guid.ToString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        guard let uuid = UUID(uuidString: guidString) else {
            XCTFail("Should be able to form a UUID from a System.Guid's string")
            
            return
        }
        
        let uuidStringRet = uuid.uuidString
        
        XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
    }
	
	func testSwiftUUIDConversionWithExtensions() {
		let iterations = 100
		
		// System.Guid -> UUID
		for _ in 0..<iterations {
			guard let newGuid = try? System_Guid.newGuid() else {
				XCTFail("System.Guid.NewGuid should not throw and return an instance")
				
				return
			}
			
			guard let uuidRet = newGuid.uuid() else {
				XCTFail("Should be able to convert a System.Guid to a Swift UUID")
				
				return
			}
			
			guard let newGuidString = try? newGuid.toString()?.string() else {
				XCTFail("System.Guid.ToString should not throw and return an instance")
				
				return
			}
			
			let uuidRetString = uuidRet.uuidString
			
			XCTAssertEqual(newGuidString.lowercased(), uuidRetString.lowercased())
		}
		
		// UUID -> System.Guid
		for _ in 0..<iterations {
			let newUUID = UUID()
			
			guard let guidRet = newUUID.dotNETGuid() else {
				XCTFail("Should be able to convert a Swift UUID to a System.Guid")
				
				return
			}
			
			guard let guidRetString = try? guidRet.toString()?.string() else {
				XCTFail("System.Guid.ToString should not throw and return an instance")
				
				return
			}
			
			let newUUIDString = newUUID.uuidString
			
			XCTAssertEqual(newUUIDString.lowercased(), guidRetString.lowercased())
		}
	}
}
