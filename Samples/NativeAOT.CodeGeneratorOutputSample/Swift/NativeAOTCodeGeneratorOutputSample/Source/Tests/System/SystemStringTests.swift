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
        
        let isNonEmptyStringNullOrEmpty = System_String_IsNullOrEmpty(nonEmptyString,
																	  &exception)
        
		XCTAssertNil(exception)
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
		
		let indexOfWorld = System_String_IndexOf_4(nonEmptyString,
												   "World",
												   &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(expectedIndexOfWorld, indexOfWorld)
		
		guard let splitOptions: System_StringSplitOptions = .init(rawValue: System_StringSplitOptions.removeEmptyEntries.rawValue | System_StringSplitOptions.trimEntries.rawValue) else {
			XCTFail("Failed to get string split options")
			
			return
		}
		
		guard let split = System_String_Split_6(nonEmptyString,
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
	
	func testStringReplace() {
		var exception: System_Exception_t?
		
		let hello = "Hello"
		let otherPart = " World 😀"
		let originalString = "\(hello)\(otherPart)"
		
		let expectedString = originalString.replacingOccurrences(of: hello, with: "")
		
		guard let replacedStringC = System_String_Replace_3(originalString,
															hello,
															"",
															&exception),
			  exception == nil else {
			XCTFail("System.String.Replace should not throw and return an instance of a C String")
			
			return
		}
		
		defer { replacedStringC.deallocate() }
		
		let replacedString = String(cString: replacedStringC)
		
		XCTAssertEqual(expectedString, replacedString)
	}
	
	func testStringSubstring() {
		var exception: System_Exception_t?
		
		let string = "Hello World 😀"
		let needle = "World"
		
		let expectedIndex: Int32 = 6
		
		let index = System_String_IndexOf_4(string,
											needle,
											&exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(expectedIndex, index)
	}
	
	func testStringSplitAndJoin() {
		var exception: System_Exception_t?
		
		let components = [
			"1. First",
			" 2. Second",
			"",
			"3. Third "
		]
		
		var cleanedComponents = [String]()
		
		for component in components {
			let trimmedComponent = component.trimmingCharacters(in: .whitespacesAndNewlines)
			
			guard !trimmedComponent.isEmpty else {
				continue
			}
			
			cleanedComponents.append(trimmedComponent)
		}
		
		let separator = ";"
		
		let joined = components.joined(separator: separator)
		let cleanedJoined = cleanedComponents.joined(separator: separator)
		
		guard let options: System_StringSplitOptions = .init(rawValue: System_StringSplitOptions.removeEmptyEntries.rawValue | System_StringSplitOptions.trimEntries.rawValue) else {
			XCTFail("Failed to create string split options")
			
			return
		}
		
		guard let split = System_String_Split_6(joined,
												separator,
												options,
												&exception),
			  exception == nil else {
			XCTFail("System.String.Split should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(split) }
		
		let length = System_Array_Length_Get(split,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(cleanedComponents.count, .init(length))
		
		for (idx, component) in cleanedComponents.enumerated() {
			guard let componentRetObj = System_Array_GetValue_1(split,
																.init(idx),
																&exception),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			defer { System_Object_Destroy(componentRetObj) }
			
			guard let componentRetC = System_Object_ToString(componentRetObj,
															 &exception),
				  exception == nil else {
				XCTFail("System.Object.ToString should not throw and return an instance of a C String")
				
				return
			}
			
			defer { componentRetC.deallocate() }
			
			let componentRet = String(cString: componentRetC)
			
			XCTAssertEqual(component, componentRet)
		}
		
		guard let joinedRetC = System_String_Join_1(separator,
													split,
													&exception),
			  exception == nil else {
			XCTFail("System.String.Join should not throw and return an instance of a C String")
			
			return
		}
		
		defer { joinedRetC.deallocate() }
		
		let joinedRet = String(cString: joinedRetC)
		
		XCTAssertEqual(cleanedJoined, joinedRet)
	}
}
