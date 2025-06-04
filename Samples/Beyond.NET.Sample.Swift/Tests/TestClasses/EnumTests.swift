import XCTest
import BeyondDotNETSampleKit

final class EnumTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }

    override class func tearDown() {
        Self.sharedTearDown()
    }

    // TODO: Add support for .NET enums with unknown values (ie. `var value = (TestEnum)42`)
//    func testGettingUnknownEnumValue() throws {
//        let obj = try Beyond_NET_Sample_Source_EnumTests()
//        let value = try obj.getUnknownEnumValue()
//    }
}

// TODO: Testbed for Swift enums with custom values
/*
public enum TestikusEnum {
    public typealias CValue = Int32
    
//    public init?(rawValue: CValue) {
//        
//    }
    
    var cValue: CValue {
        switch self {
            case .a:
                0
            case .b:
                1
            case .dnUnknown(let value):
                value
        }
    }
    
//    init(cValue: Int32) {
//        self = .a
//    }

    // TODO
//    var cValue: Beyond_NET_Sample_Source_TestEnum_t { get {
//        Beyond_NET_Sample_Source_TestEnum_t(rawValue: rawValue)!
//    }}

    case a
    case b
    case dnUnknown(value: Int32)
}
*/
