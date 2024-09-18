import XCTest
import BeyondDotNETSampleKit

final class SystemTypeTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemType() throws {
        let systemObjectTypeName = System_Object.fullTypeName
        let systemObjectType = System_Object.typeOf
        
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
    
    func testPrimitiveTypes() {
        verifyTypeFullName(System_Boolean.typeOf, "System.Boolean")
        verifyTypeFullName(System_Char.typeOf, "System.Char")
        verifyTypeFullName(System_Double.typeOf, "System.Double")
        verifyTypeFullName(System_Single.typeOf, "System.Single")
        verifyTypeFullName(System_SByte.typeOf, "System.SByte")
        verifyTypeFullName(System_Int16.typeOf, "System.Int16")
        verifyTypeFullName(System_Int32.typeOf, "System.Int32")
        verifyTypeFullName(System_Int64.typeOf, "System.Int64")
        verifyTypeFullName(System_IntPtr.typeOf, "System.IntPtr")
        verifyTypeFullName(System_Byte.typeOf, "System.Byte")
        verifyTypeFullName(System_UInt16.typeOf, "System.UInt16")
        verifyTypeFullName(System_UInt32.typeOf, "System.UInt32")
        verifyTypeFullName(System_UInt64.typeOf, "System.UInt64")
        verifyTypeFullName(System_UIntPtr.typeOf, "System.UIntPtr")
    }
}

private extension SystemTypeTests {
    func verifyTypeFullName(_ type: System_Type,
                            _ expectedFullName: String) {
        let fullTypeName: String?
        
        do {
            fullTypeName = try type.fullName?.string()
        } catch {
            XCTFail("Failed to get type name: \((error as NSError).description)")
            
            return
        }
        
        guard let fullTypeName else {
            XCTFail("Failed to get type name")
            
            return
        }
        
        XCTAssertEqual(fullTypeName, expectedFullName)
    }
}
