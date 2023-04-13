import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTupleTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    // TODO: Fix returnOrSetterOrEventHandlerType to make this work
//    func testTupleA1() {
//        var exception: System_Exception_t?
//
//        guard let systemStringType = System_String_TypeOf() else {
//            XCTFail("typeof(System.String) should return an instance")
//
//            return
//        }
//
//        defer { System_Type_Destroy(systemStringType) }
//
//        let string = "Hello World"
//        let stringDN = string.dotNETString()
//        defer { System_String_Destroy(stringDN) }
//
//        guard let tupleOfString = System_Tuple_A1_Create(systemStringType,
//                                                         stringDN,
//                                                         &exception),
//              exception == nil else {
//            XCTFail("System.Tuple<System.String> ctor should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Tuple_A1_Destroy(tupleOfString) }
//
//        guard let stringRetDN = System_Tuple_A1_Item1_Get(tupleOfString,
//                                                          systemStringType,
//                                                          &exception),
//              exception == nil else {
//            XCTFail("System.Tuple<System.String>.Item1 getter should not throw and return an instance")
//
//            return
//        }
//
//        let stringRet = String(dotNETString: stringRetDN,
//                               destroyDotNETString: true)
//
//        XCTAssertEqual(string, stringRet)
//    }
}
