import XCTest
import BeyondDotNETSampleKit

final class SystemTupleTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTupleA1() throws {
        let systemStringType = System_String.typeOf
        
        let string = "Hello World"
        let stringDN = string.dotNETString()

        let tupleOfString = try System_Tuple_A1(T1: systemStringType,
                                                stringDN)

        guard let stringRetDN = try tupleOfString.item1(T1: systemStringType)?.castAs(System_String.self) else {
            XCTFail("System.Tuple<System.String>.Item1 getter should not throw and return an instance")

            return
        }

        let stringRet = stringRetDN.string()
        XCTAssertEqual(string, stringRet)
    }
    
    func testTupleA2() throws {
        let systemStringType = System_String.typeOf
        let systemExceptionType = System_Exception.typeOf
        
        let string = "The Exception Message"
        let stringDN = string.dotNETString()
        
        let exception = try System_Exception(stringDN)

        let tupleOfStringAndException = try System_Tuple_A2(T1: systemStringType,
                                                            T2: systemExceptionType,
                                                            stringDN,
                                                            exception)

        guard let stringRetDN = try tupleOfStringAndException.item1(T1: systemStringType,
                                                                    T2: systemExceptionType)?.castAs(System_String.self) else {
            XCTFail("System.Tuple<System.String, System.Exception>.Item1 getter should not throw and return an instance")

            return
        }

        let stringRet = stringRetDN.string()
        XCTAssertEqual(string, stringRet)
        
        guard let exceptionRet = try tupleOfStringAndException.item2(T1: systemStringType,
                                                                     T2: systemExceptionType)?.castAs(System_Exception.self) else {
            XCTFail("System.Tuple<System.String, System.Exception>.Item2 getter should not throw and return an instance")

            return
        }

        XCTAssertTrue(exception == exceptionRet)
    }
}
