import XCTest
import BeyondDotNETSampleKit

final class SystemCollectionsGenericDictionaryTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testCreate() throws {
        let systemStringType = System_String.typeOf
        let systemExceptionType = System_Exception.typeOf
        
        let dictionary = try System_Collections_Generic_Dictionary_A2(TKey: systemStringType,
                                                                      TValue: systemExceptionType)
        
        let dictionaryType = try dictionary.getType()
        
        guard let dictionaryTypeName = try? dictionaryType.fullName?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(dictionaryTypeName.contains("System.Collections.Generic.Dictionary`2[[System.String"))
        XCTAssertTrue(dictionaryTypeName.contains(",[System.Exception"))
    }
    
    func testUse() throws {
        let systemStringType = System_String.typeOf
        let systemExceptionType = System_Exception.typeOf
        
        let dictionary = try System_Collections_Generic_Dictionary_A2(TKey: systemStringType,
                                                                      TValue: systemExceptionType)

        let exceptionMessage = "My Exception Message"
        let exceptionMessageDN = exceptionMessage.dotNETString()
        
        let exceptionValue = try System_Exception(exceptionMessageDN)
        
        guard try dictionary.tryAdd(TKey: systemStringType,
                                    TValue: systemExceptionType,
                                    exceptionMessageDN,
                                    exceptionValue) else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryAdd should not throw and return true")
            
            return
        }
        
        let emptyString = System_String.empty
        
        var valueForEmptyString: System_Object?

        guard !(try dictionary.tryGetValue(TKey: systemStringType,
                                             TValue: systemExceptionType,
                                             emptyString,
                                             &valueForEmptyString)),
              valueForEmptyString == nil else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryGetValue should not throw and return false")

            return
        }
		
        guard let valueRet = try dictionary.item(TKey: systemStringType,
                                                 TValue: systemExceptionType,
                                                 exceptionMessageDN) else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>[] should not throw and return an instance")

			return
		}

        let equal = exceptionValue == valueRet
        XCTAssertTrue(equal)
    }
}
