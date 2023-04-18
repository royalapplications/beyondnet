import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemArrayTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
    func testSystemArray() {
        var exception: System_Exception_t?
        
        guard let now = System_DateTime_Now_Get(&exception),
              exception == nil else {
            XCTFail("System.DateTime.Now should not throw and return an instance")
            
            return
        }
        
        defer { System_DateTime_Destroy(now) }
        
        guard let dateTimeType = System_Object_GetType(now,
                                                       &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(dateTimeType) }
        
        let arrayLength: Int32 = 1
        
        guard let arrayOfDateTime = System_Array_CreateInstance(dateTimeType,
                                                                arrayLength,
                                                                &exception),
              exception == nil else {
            XCTFail("System.Array.CreateInstance should not fail and return an instance")
            
            return
        }
        
        defer { System_Array_Destroy(arrayOfDateTime) }
        
        let index: Int32 = 0
        
        System_Array_SetValue(arrayOfDateTime,
                              now,
                              index,
                              &exception)
        
        XCTAssertNil(exception)
        
		guard let retrievedNow = System_Array_GetValue_1(arrayOfDateTime,
														 index,
														 &exception),
              exception == nil else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(retrievedNow) }
        
        let equals = System_Object_Equals(now,
                                          retrievedNow,
                                          &exception)
        
        XCTAssertNil(exception)
        XCTAssertTrue(equals)
    }
	
	func testEmptyArrayWithGenerics() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		guard let emptyArrayOfString = System_Array_Empty_A1(systemStringType,
															 &exception) else {
			XCTFail("System.Array<System.String>.Empty should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(emptyArrayOfString) }
		
		let length = System_Array_Length_Get(emptyArrayOfString,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(0, .init(length))
		
		guard let arrayType = System_Object_GetType(emptyArrayOfString,
													&exception) else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(arrayType) }
		
		let isArray = System_Type_IsArray_Get(arrayType,
											  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(isArray)
		
		guard let arrayElementType = System_Type_GetElementType(arrayType,
																&exception),
			  exception == nil else {
			XCTFail("System.Type.GetElementType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(arrayElementType) }
		
		let arrayElementTypeIsSystemString = System_Type_Equals(systemStringType,
																arrayElementType,
																&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(arrayElementTypeIsSystemString)
	}
	
	func testFillArrayWithGenerics() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		let numberOfElements: Int32 = 5
		
		guard let arrayOfString = System_Array_CreateInstance(systemStringType,
															  numberOfElements,
															  &exception),
			  exception == nil else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(arrayOfString) }
		
		let string = "Abc"
		let stringDN = string.cDotNETString()
		defer { System_String_Destroy(stringDN) }
		
		System_Array_Fill_A1(systemStringType,
							 arrayOfString,
							 stringDN,
							 &exception)
		
		XCTAssertNil(exception)
		
		let length = System_Array_Length_Get(arrayOfString,
											 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(numberOfElements, length)
		
		for idx in 0..<length {
			guard let stringElement = String(cDotNETString: System_Array_GetValue_1(arrayOfString,
																				   idx,
																				   &exception),
											 destroyDotNETString: true),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			XCTAssertEqual(string, stringElement)
		}
	}
	
	func testReverseArrayWithGenerics() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		let strings = [
			"1",
			"2"
		]
		
		let numberOfElements: Int32 = .init(strings.count)
		
		guard let arrayOfString = System_Array_CreateInstance(systemStringType,
															  numberOfElements,
															  &exception),
			  exception == nil else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(arrayOfString) }
		
		for (idx, string) in strings.enumerated() {
			let stringDN = string.cDotNETString()
			defer { System_String_Destroy(stringDN) }
			
			System_Array_SetValue(arrayOfString,
								  stringDN,
								  .init(idx),
								  &exception)
			
			XCTAssertNil(exception)
		}
		
		System_Array_Reverse_A1(systemStringType,
								arrayOfString,
								&exception)
		
		XCTAssertNil(exception)
		
		let reversedStrings = [String](strings.reversed())
		
		for idx in 0..<numberOfElements {
			guard let stringElement = String(cDotNETString: System_Array_GetValue_1(arrayOfString,
																				   idx,
																				   &exception),
											 destroyDotNETString: true),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			let expectedString = reversedStrings[.init(idx)]
			
			XCTAssertEqual(expectedString, stringElement)
		}
	}
}
