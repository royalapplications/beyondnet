import XCTest
import NativeAOTCodeGeneratorOutputSample

final class StringTests: XCTestCase {
    func testString() {
        var exception: System_Exception_t?
        
        guard let emptyStringC = System_String_Empty_Get() else {
            XCTFail("System.String.Empty should return an empty string")
            
            return
        }
        
        defer { emptyStringC.deallocate() }
        
        let emptyString = String(cString: emptyStringC)
        XCTAssertTrue(emptyString.isEmpty)
        
        let isNullOrEmpty = System_String_IsNullOrEmpty(emptyStringC,
                                                        &exception)
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrEmpty should not throw")
            
            return
        }
        
        XCTAssertEqual(isNullOrEmpty, .yes)
        
        let isNullOrWhiteSpace = System_String_IsNullOrWhiteSpace(emptyStringC,
                                                                  &exception)
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrWhiteSpace should not throw")
            
            return
        }
        
        XCTAssertEqual(isNullOrWhiteSpace, .yes)
        
        let nonEmptyString = "Hello World!"
        
        var isNonEmptyStringNullOrEmpty = CBool.yes
        
        nonEmptyString.withCString { nonEmptyStringC in
            isNonEmptyStringNullOrEmpty = System_String_IsNullOrEmpty(nonEmptyStringC,
                                                                      &exception)
        }
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrEmpty should not throw")
            
            return
        }
        
        XCTAssertEqual(isNonEmptyStringNullOrEmpty, .no)
    }
}
