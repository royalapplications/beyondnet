import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTypeTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemType() {
        let systemObjectTypeName = System_Object.fullTypeName
        let systemObjectType = System_Object.typeOf()
        
        guard let systemObjectTypeViaName = try? System_Type.getType(systemObjectTypeName.dotNETString()) else {
            XCTFail("System.Type.GetType should not throw and return an instance of System.Type")
            
            return
        }
        
        guard systemObjectType == systemObjectTypeViaName else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        guard let retrievedSystemObjectTypeName = (try? systemObjectType.fullName)?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
    }
    
    func testInvalidType() {
        let invalidTypeName = "! This.Type.Surely.Does.Not.Exist !"

        var invalidType: System_Type?
        
        do {
            invalidType = try System_Type.getType(invalidTypeName.dotNETString(),
                                                  true)
            
            XCTFail("System.Type.GetType should throw")
        } catch {
            XCTAssertNil(invalidType)
            
            let exceptionMessage = error.localizedDescription
            
            XCTAssertTrue(exceptionMessage.contains("The type \'\(invalidTypeName)\' cannot be found"))
        }
    }
}
