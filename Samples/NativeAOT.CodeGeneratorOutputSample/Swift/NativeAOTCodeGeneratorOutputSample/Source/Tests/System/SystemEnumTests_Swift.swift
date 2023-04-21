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
        let systemDateTimeKindType = System_DateTimeKind.typeOf()

        guard let enumNames = try? systemDateTimeKindType.getEnumNames() else {
            XCTFail("System.Type.GetEnumNames should not throw and return an instance")

            return
        }

        let namesCount = (try? enumNames.length_get()) ?? -1
        XCTAssertEqual(3, namesCount)

        var names = [String]()

        for idx in 0..<namesCount {
            guard let stringElement = try? enumNames.getValue(idx)?.castAs(System_String.self)?.string() else {
                XCTFail("System.Array.GetValue should not throw and return an instance")

                return
            }

            names.append(stringElement)
        }

        let unspecified = "Unspecified"
        let utc = "Utc"
        let local = "Local"

        XCTAssertEqual(unspecified, names[0])
        XCTAssertEqual(utc, names[1])
        XCTAssertEqual(local, names[2])
    }
}
