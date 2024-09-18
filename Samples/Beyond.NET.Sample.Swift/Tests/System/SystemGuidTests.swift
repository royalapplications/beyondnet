import XCTest
import BeyondDotNETSampleKit

final class SystemGuidTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemGuid() throws {
        let uuid = UUID()
        
        let uuidString = uuid.uuidString
        let uuidStringDN = uuidString.dotNETString()
        
        let guid = try System_Guid(uuidStringDN)
        let guidString = try guid.toString().string()
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        let guidTypeName = "System.Guid"
        let guidTypeNameDN = guidTypeName.dotNETString()
        
        let guidType = try System_Type.getType(guidTypeNameDN)
        let guidTypeFromInstance = try guid.getType()
        
        let equals = guidType == guidTypeFromInstance
        XCTAssertTrue(equals)
        
        let emptyGuid = System_Guid.empty
        
        let emptyGuidString = try emptyGuid.toString().string()
        
        XCTAssertEqual("00000000-0000-0000-0000-000000000000", emptyGuidString)
    }
    
    func testSystemGuidParameterlessConstructor() throws {
        let guid = try System_Guid()
        let emptyGuid = System_Guid.empty
        
        XCTAssertTrue(guid == emptyGuid)
        
        let guidString = try guid.toString().string()
        let emptyGuidString = try emptyGuid.toString().string()
        
        XCTAssertEqual(guidString.lowercased(), emptyGuidString.lowercased())
    }
    
    func testSystemGuidParsing() throws {
        let uuid = UUID()
        
        let uuidString = uuid.uuidString
        let uuidStringDN = uuidString.dotNETString()
        
        var guid = System_Guid.empty
        
        guard try System_Guid.tryParse(uuidStringDN, &guid) else {
            XCTFail("System.Guid.TryParse should not throw and return an instance as out parameter")
            
            return
        }
        
        let uuidStringRet = try guid.toString().string()
        
        XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
    }
    
    func testInvalidSystemGuidParsing() throws {
        let emptyGuid = System_Guid.empty
        
        let uuidString = "nonsense"
        let uuidStringDN = uuidString.dotNETString()
        
        var guid = System_Guid.empty
        
        guard !(try System_Guid.tryParse(uuidStringDN, &guid)) else {
            XCTFail("System.Guid.TryParse should not throw, the return value should be false and the returned instance as out parameter should be an empty System.Guid")
            
            return
        }
        
        let equal = emptyGuid == guid
        XCTAssertTrue(equal)
    }
    
    func testSwiftUUIDConversion() throws {
        let uuidString = "7F579986-D12F-4889-9A14-FDE340A59E08"
        
        var guid = System_Guid.empty
        
        guard try System_Guid.tryParse(uuidString.dotNETString(),
                                       &guid) else {
            XCTFail("System.Guid.TryParse should not throw, return true and an instance as out parameter")
            
            return
        }
        
        let guidString = try guid.toString().string()
        
        XCTAssertEqual(uuidString.lowercased(), guidString.lowercased())
        
        guard let uuid = UUID(uuidString: guidString) else {
            XCTFail("Should be able to form a UUID from a System.Guid's string")
            
            return
        }
        
        let uuidStringRet = uuid.uuidString
        
        XCTAssertEqual(uuidString.lowercased(), uuidStringRet.lowercased())
    }
	
	func testSwiftUUIDConversionWithExtensions() throws {
		let iterations = 100
		
		// System.Guid -> UUID
		for _ in 0..<iterations {
			let newGuid = try System_Guid.newGuid()
			
			guard let uuidRet = newGuid.uuid() else {
				XCTFail("Should be able to convert a System.Guid to a Swift UUID")
				
				return
			}
			
			let newGuidString = try newGuid.toString().string()
			
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
			
			let guidRetString = try guidRet.toString().string()
			
			let newUUIDString = newUUID.uuidString
			
			XCTAssertEqual(newUUIDString.lowercased(), guidRetString.lowercased())
		}
	}
    
    // MARK: - Performance Tests
//    private let numberOfIDs = 100_000
//
//    private func makeGuidString() throws -> String {
//        let guid = try System.Guid.newGuid()
//        let guidStrDN = try guid.toString()
//        let guidStr = guidStrDN.string()
//        
//        return guidStr
//    }
//    
//    private func makeUUIDString() -> String {
//        let uuid = UUID()
//        let uuidStr = uuid.uuidString
//        
//        return uuidStr
//    }
//
//
//    func testSystemGuidPerformance() throws {
//        let numberOfIDs = self.numberOfIDs
//        
//        measure {
//            for _ in 0..<numberOfIDs {
//                _ = try? makeGuidString()
//            }
//        }
//    }
//    
//    func testUUIDPerformance() throws {
//        let numberOfIDs = self.numberOfIDs
//        
//        measure {
//            for _ in 0..<numberOfIDs {
//                _ = makeUUIDString()
//            }
//        }
//    }
//    
//    func testGuidToUUIDPerformance() throws {
//        let numberOfIDs = self.numberOfIDs
//        
//        var guids = [System.Guid]()
//        
//        for _ in 0..<numberOfIDs {
//            let newGuid = try System.Guid.newGuid()
//            guids.append(newGuid)
//        }
//        
//        measure {
//            for guid in guids {
//                _ = guid.uuid()
//            }
//        }
//    }
//    
//    func testUUIDToGuidPerformance() throws {
//        let numberOfIDs = self.numberOfIDs
//        
//        var uuids = [UUID]()
//        
//        for _ in 0..<numberOfIDs {
//            let newUUID = UUID()
//            uuids.append(newUUID)
//        }
//        
//        measure {
//            for uuid in uuids {
//                _ = uuid.dotNETGuid()
//            }
//        }
//    }
}
