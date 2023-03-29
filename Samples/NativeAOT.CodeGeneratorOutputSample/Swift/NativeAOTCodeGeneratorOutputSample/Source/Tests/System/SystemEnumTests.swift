import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemEnumTests: XCTestCase {
    func testSystemEnum() {
        var exception: System_Exception_t?

        guard let systemDateTimeKindType = System_DateTimeKind_TypeOf() else {
            XCTFail("typeof(System.DateTime) should return an instance")

            return
        }

        defer { System_Type_Destroy(systemDateTimeKindType) }
        
        guard let enumNames = System_Type_GetEnumNames(systemDateTimeKindType,
                                                       &exception),
              exception == nil else {
            XCTFail("System.Type.GetEnumNames should not throw and return an instance")

            return
        }

        defer { System_Array_Destroy(enumNames) }

        let namesCount = System_Array_GetLength(enumNames,
                                                0,
                                                &exception)

        XCTAssertNil(exception)

        XCTAssertEqual(3, namesCount)
    }
}
