import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTupleTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTupleA1() {
        let systemStringType = System_String.typeOf()
        
        let string = "Hello World"
        let stringDN = string.dotNETString()

        guard let tupleOfString = try? System_Tuple_A1(systemStringType,
                                                       stringDN) else {
            XCTFail("System.Tuple<System.String> ctor should not throw and return an instance")

            return
        }

        guard let stringRetDN = try? tupleOfString.item1_get(systemStringType)?.castAs(System_String.self) else {
            XCTFail("System.Tuple<System.String>.Item1 getter should not throw and return an instance")

            return
        }

        let stringRet = stringRetDN.string()
        XCTAssertEqual(string, stringRet)
    }
    
    func testTupleA2() {
        let systemStringType = System_String.typeOf()
        let systemExceptionType = System_Exception.typeOf()
        
        let string = "The Exception Message"
        let stringDN = string.dotNETString()
        
        guard let exception = try? System_Exception(stringDN) else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }

        guard let tupleOfStringAndException = try? System_Tuple_A2(systemStringType,
                                                                   systemExceptionType,
                                                                   stringDN,
                                                                   exception) else {
            XCTFail("System.Tuple<System.String, System.Exception> ctor should not throw and return an instance")

            return
        }

        guard let stringRetDN = try? tupleOfStringAndException.item1_get(systemStringType,
                                                                         systemExceptionType)?.castAs(System_String.self) else {
            XCTFail("System.Tuple<System.String, System.Exception>.Item1 getter should not throw and return an instance")

            return
        }

        let stringRet = stringRetDN.string()
        XCTAssertEqual(string, stringRet)
        
        guard let exceptionRet = try? tupleOfStringAndException.item2_get(systemStringType,
                                                                          systemExceptionType)?.castAs(System_Exception.self) else {
            XCTFail("System.Tuple<System.String, System.Exception>.Item2 getter should not throw and return an instance")

            return
        }

        XCTAssertTrue(exception == exceptionRet)
    }
}
