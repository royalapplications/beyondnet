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
        
        guard let retrievedNow = System_Array_GetValue1(arrayOfDateTime,
                                                        index,
                                                        &exception),
              exception == nil else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        defer { System_Object_Destroy(retrievedNow) }
        
        let equals = System_Object_Equals(now,
                                          retrievedNow,
                                          &exception) == .yes
        
        XCTAssertNil(exception)
        XCTAssertTrue(equals)
    }
}
