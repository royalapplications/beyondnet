import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemStringTests: XCTestCase {
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
		
		guard let trimmedStringC = System_String_Trim(" \(nonEmptyString) ",
													  &exception),
			  exception == nil else {
			XCTFail("System.String.Trim should not throw and return an instance of a C String")
			
			return
		}
		
		defer { trimmedStringC.deallocate() }
		
		let trimmedString = String(cString: trimmedStringC)
		
		XCTAssertEqual(nonEmptyString, trimmedString)
		
		let expectedIndexOfWorld: Int32 = 6
		
		let indexOfWorld = System_String_IndexOf4(nonEmptyString,
												  "World",
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(expectedIndexOfWorld, indexOfWorld)
		
		guard let splitOptions: System_StringSplitOptions = .init(rawValue: System_StringSplitOptions.removeEmptyEntries.rawValue | System_StringSplitOptions.trimEntries.rawValue) else {
			XCTFail("Failed to get string split options")
			
			return
		}
		
		guard let split = System_String_Split6(nonEmptyString,
											   " ",
											   splitOptions,
											   &exception),
			  exception == nil else {
			XCTFail("System.String.Split should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(split) }
		
		guard System_Array_Length_Get(split,
									  &exception) == 2,
			  exception == nil else {
			XCTFail("System.Array.Length getter should not throw and return 2")
			
			return
		}
    }
}
