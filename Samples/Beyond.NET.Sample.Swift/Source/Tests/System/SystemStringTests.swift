import XCTest
import BeyondNETSamplesSwift

final class SystemStringTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
    func testString() {
        var exception: System_Exception_t?
        
        guard let emptyStringDN = System_String_Empty_Get() else {
            XCTFail("System.String.Empty should return an empty string")
            
            return
        }
        
        defer { System_String_Destroy(emptyStringDN) }
        
		guard let emptyString = String(cDotNETString: emptyStringDN) else {
			XCTFail("Failed to convert string")
			
			return
		}
		
        XCTAssertTrue(emptyString.isEmpty)
        
        let isNullOrEmpty = System_String_IsNullOrEmpty(emptyStringDN,
                                                        &exception)
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrEmpty should not throw")
            
            return
        }
        
        XCTAssertTrue(isNullOrEmpty)
        
        let isNullOrWhiteSpace = System_String_IsNullOrWhiteSpace(emptyStringDN,
                                                                  &exception)
        
        guard exception == nil else {
            XCTFail("System.String.IsNullOrWhiteSpace should not throw")
            
            return
        }
        
        XCTAssertTrue(isNullOrWhiteSpace)
        
        let nonEmptyString = "Hello World!"
		let nonEmptyStringDN = nonEmptyString.cDotNETString()
		
		defer { System_String_Destroy(nonEmptyStringDN) }
        
        let isNonEmptyStringNullOrEmpty = System_String_IsNullOrEmpty(nonEmptyStringDN,
																	  &exception)
        
		XCTAssertNil(exception)
		XCTAssertFalse(isNonEmptyStringNullOrEmpty)
		
		let stringForTrimmingDN = " \(nonEmptyString) ".cDotNETString()
		
		defer { System_String_Destroy(stringForTrimmingDN) }
		
		guard let trimmedString = String(cDotNETString: System_String_Trim(stringForTrimmingDN,
																		  &exception),
										 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.String.Trim should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nonEmptyString, trimmedString)
		
		let worldDN = "World".cDotNETString()
		
		defer { System_String_Destroy(worldDN) }
		
		let expectedIndexOfWorld: Int32 = 6
		
		let indexOfWorld = System_String_IndexOf_4(nonEmptyStringDN,
												   worldDN,
												   &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(expectedIndexOfWorld, indexOfWorld)
		
		let splitOptions: System_StringSplitOptions_t = [ .removeEmptyEntries, .trimEntries ]
		
		let blankDN = " ".cDotNETString()
		
		defer { System_String_Destroy(blankDN) }
		
		guard let split = System_String_Split_6(nonEmptyStringDN,
												blankDN,
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
		let emptyString = ""
		
		let expectedString = originalString.replacingOccurrences(of: hello,
																 with: emptyString)
		
		let originalStringDN = originalString.cDotNETString()
		defer { System_String_Destroy(originalStringDN) }
		
		let helloDN = hello.cDotNETString()
		defer { System_String_Destroy(helloDN) }
		
		let emptyStringDN = emptyString.cDotNETString()
		defer { System_String_Destroy(emptyStringDN) }
		
		guard let replacedString = String(cDotNETString: System_String_Replace_3(originalStringDN,
																				helloDN,
																				emptyStringDN,
																				&exception),
										  destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.String.Replace should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedString, replacedString)
	}
	
	func testStringSubstring() {
		var exception: System_Exception_t?
		
		let string = "Hello World 😀"
		let needle = "World"
		
		let stringDN = string.cDotNETString()
		defer { System_String_Destroy(stringDN) }
		
		let needleDN = needle.cDotNETString()
		defer { System_String_Destroy(needleDN) }
		
		let expectedIndex: Int32 = 6
		
		let index = System_String_IndexOf_4(stringDN,
											needleDN,
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
		let separatorDN = separator.cDotNETString()
		defer { System_String_Destroy(separatorDN) }
		
		let joined = components.joined(separator: separator)
		let joinedDN = joined.cDotNETString()
		defer { System_String_Destroy(joinedDN) }
		
		let cleanedJoined = cleanedComponents.joined(separator: separator)
		
		let splitOptions: System_StringSplitOptions_t = [ .removeEmptyEntries, .trimEntries ]
		
		guard let split = System_String_Split_6(joinedDN,
												separatorDN,
												splitOptions,
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
			
			let componentRet = String(cDotNETString: componentRetObj,
									  destroyDotNETString: true)
			
			XCTAssertEqual(component, componentRet)
		}
		
		guard let joinedRet = String(cDotNETString: System_String_Join_1(separatorDN,
																		split,
																		&exception),
									 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.String.Join should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(cleanedJoined, joinedRet)
	}
}
