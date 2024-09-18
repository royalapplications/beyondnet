import XCTest
import BeyondDotNETSampleKit

final class SystemEnumTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemEnum() throws {
        let systemDateTimeKindType = System_DateTimeKind.typeOf

        let enumNames = try systemDateTimeKindType.getEnumNames()

        let namesCount = try enumNames.length
        XCTAssertEqual(3, namesCount)

        var names = [String]()
        
        for enumNameDN in enumNames {
            let enumName = enumNameDN.string()

            names.append(enumName)
        }

        let unspecified = "Unspecified"
        let utc = "Utc"
        let local = "Local"

        XCTAssertEqual(unspecified, names[0])
        XCTAssertEqual(utc, names[1])
        XCTAssertEqual(local, names[2])
        
        XCTAssertEqual(System_DateTimeKind.unspecified.rawValue, 0)
        XCTAssertEqual(System_DateTimeKind.utc.rawValue, 1)
        XCTAssertEqual(System_DateTimeKind.local.rawValue, 2)
        
        XCTAssertEqual(System_DateTimeKind(rawValue: System_DateTimeKind.unspecified.rawValue),
                       System_DateTimeKind.unspecified)
        
        XCTAssertEqual(System_DateTimeKind(rawValue: System_DateTimeKind.utc.rawValue),
                       System_DateTimeKind.utc)
        
        XCTAssertEqual(System_DateTimeKind(rawValue: System_DateTimeKind.local.rawValue),
                       System_DateTimeKind.local)
    }
}
