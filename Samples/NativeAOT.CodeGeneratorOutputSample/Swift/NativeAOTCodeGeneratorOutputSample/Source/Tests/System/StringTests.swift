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
        
        XCTAssertTrue(isNullOrEmpty)
        
        let isNullOrWhiteSpace = System_String_IsNullOrWhiteSpace(emptyStringC,
                                                                  &exception)
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrWhiteSpace should not throw")
            
            return
        }
        
        XCTAssertTrue(isNullOrWhiteSpace)
        
        let nonEmptyString = "Hello World!"
        
        var isNonEmptyStringNullOrEmpty = true
        
        nonEmptyString.withCString { nonEmptyStringC in
            isNonEmptyStringNullOrEmpty = System_String_IsNullOrEmpty(nonEmptyStringC,
                                                                      &exception)
        }
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrEmpty should not throw")
            
            return
        }
        
        XCTAssertFalse(isNonEmptyStringNullOrEmpty)
    }
}
