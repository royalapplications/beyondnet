import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemCollectionsGenericDictionaryTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testCreate() {
		var exception: System_Exception_t?
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }
		
		guard let dictionary = System_Collections_Generic_Dictionary_A2_Create(systemStringType,
																			   systemExceptionType,
																			   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Collections_Generic_Dictionary_A2_Destroy(dictionary) }
		
		guard let dictionaryType = System_Object_GetType(dictionary,
															&exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(dictionaryType) }
		
		guard let dictionaryTypeName = String(cDotNETString: System_Type_FullName_Get(dictionaryType,
																					 &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(dictionaryTypeName.contains("System.Collections.Generic.Dictionary`2[[System.String"))
		XCTAssertTrue(dictionaryTypeName.contains(",[System.Exception"))
	}
	
	func testUse() {
		var exception: System_Exception_t?

		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }

		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }

		guard let dictionary = System_Collections_Generic_Dictionary_A2_Create(systemStringType,
																			   systemExceptionType,
																			   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")

			return
		}

		defer { System_Collections_Generic_Dictionary_A2_Destroy(dictionary) }

		let exceptionMessage = "My Exception Message"
        
        let exceptionMessageDN = exceptionMessage.cDotNETString()
        defer { System_String_Destroy(exceptionMessageDN) }
        
        guard let exceptionValue = System_Exception_Create_1(exceptionMessageDN,
                                                             &exception) else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Exception_Destroy(exceptionValue) }
        
        guard System_Collections_Generic_Dictionary_A2_TryAdd(dictionary,
                                                              systemStringType,
                                                              systemExceptionType,
                                                              exceptionMessageDN,
                                                              exceptionValue,
                                                              &exception),
              exception == nil else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryAdd should not throw and return true")
            
            return
        }
        
        guard let emptyString = System_String_Empty_Get() else {
            XCTFail("System.String.Empty should return an instance")
            
            return
        }
        
        defer { System_String_Destroy(emptyString) }
        
        var valueForEmptyString: System_Object_t?

        guard !System_Collections_Generic_Dictionary_A2_TryGetValue(dictionary,
                                                                    systemStringType,
                                                                    systemExceptionType,
                                                                    emptyString,
                                                                    &valueForEmptyString,
                                                                    &exception),
              exception == nil,
              valueForEmptyString == nil else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryGetValue should not throw and return false")

            return
        }
		
		guard let valueRet = System_Collections_Generic_Dictionary_A2_Item_Get(dictionary,
																			   systemStringType,
																			   systemExceptionType,
																			   exceptionMessageDN,
																			   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>[] should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(valueRet) }

        let equal = System_Object_Equals(exceptionValue,
                                         valueRet,
                                         &exception)

        XCTAssertNil(exception)
        XCTAssertTrue(equal)
	}
}
