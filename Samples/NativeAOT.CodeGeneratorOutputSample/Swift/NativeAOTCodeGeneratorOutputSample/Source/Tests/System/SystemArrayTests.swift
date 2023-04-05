import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemArrayTests: XCTestCase {
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
		
		guard let systemStringType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
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
}
