import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemEnumTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemEnum() {
        // TODO: We need typeOf's for enums
//        let systemDateTimeKindType = System_DateTimeKind.typeOf()
//        
//        guard let enumNames = System_Type_GetEnumNames(systemDateTimeKindType,
//                                                       &exception),
//              exception == nil else {
//            XCTFail("System.Type.GetEnumNames should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Array_Destroy(enumNames) }
//
//        let namesCount = System_Array_Length_Get(enumNames,
//                                                 &exception)
//
//        XCTAssertNil(exception)
//
//        XCTAssertEqual(3, namesCount)
//        
//        var names = [String]()
//        
//        for idx in 0..<namesCount {
//            guard let stringElement = String(cDotNETString: System_Array_GetValue_1(enumNames,
//                                                                                   idx,
//                                                                                   &exception),
//                                             destroyDotNETString: true),
//                  exception == nil else {
//                XCTFail("System.Array.GetValue should not throw and return an instance")
//                
//                return
//            }
//            
//            names.append(stringElement)
//        }
//        
//        let unspecified = "Unspecified"
//        let utc = "Utc"
//        let local = "Local"
//        
//        XCTAssertEqual(unspecified, names[0])
//        XCTAssertEqual(utc, names[1])
//        XCTAssertEqual(local, names[2])
        
        // TODO: Incomplete/Does not work
//        let utcDN = utc.cDotNETString()
//        defer { System_String_Destroy(utcDN) }
//
//        guard let parsed = System_Enum_Parse_A1(systemDateTimeKindType,
//                                                utcDN,
//                                                &exception),
//              exception == nil else {
//            XCTFail("System.Enum.Parse should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Object_Destroy(parsed) }
    }
}
