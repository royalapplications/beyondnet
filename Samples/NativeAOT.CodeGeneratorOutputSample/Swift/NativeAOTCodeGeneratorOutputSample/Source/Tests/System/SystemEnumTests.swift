import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemEnumTests: XCTestCase {
    func testSystemEnum() {
        // TODO: Does not currently work
        
//        var exception: System_Exception_t?
//
//        guard let systemDateTimeKindType = System_Type_GetType2("System.DateTimeKind",
//                                                                &exception),
//              exception == nil else {
//            XCTFail("System.Type.GetType should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Type_Destroy(systemDateTimeKindType) }
//
//        guard let values = System_Enum_GetValues(systemDateTimeKindType,
//                                                 &exception),
//              exception == nil else {
//            XCTFail("System.Enum.GetValues should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Array_Destroy(values) }
//
//        let valuesCount = System_Array_GetLength(values,
//                                                 1,
//                                                 &exception)
//
//        XCTAssertNil(exception)
//
//        XCTAssertEqual(3, valuesCount)
    }
}
