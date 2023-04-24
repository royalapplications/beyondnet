import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemCollectionsGenericDictionaryTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testCreate() {
        let systemStringType = System_String.typeOf
        let systemExceptionType = System_Exception.typeOf
        
        guard let dictionary = try? System_Collections_Generic_Dictionary_A2(systemStringType,
                                                                             systemExceptionType) else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")
            
            return
        }
        
        guard let dictionaryType = try? dictionary.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard let dictionaryTypeName = try? dictionaryType.fullName?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(dictionaryTypeName.contains("System.Collections.Generic.Dictionary`2[[System.String"))
        XCTAssertTrue(dictionaryTypeName.contains(",[System.Exception"))
    }
    
    func testUse() {
        let systemStringType = System_String.typeOf
        let systemExceptionType = System_Exception.typeOf
        
        guard let dictionary = try? System_Collections_Generic_Dictionary_A2(systemStringType,
                                                                             systemExceptionType) else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception> ctor should not throw and return an instance")

            return
        }

        let exceptionMessage = "My Exception Message"
        let exceptionMessageDN = exceptionMessage.dotNETString()
        
        guard let exceptionValue = try? System_Exception(exceptionMessageDN) else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        guard (try? dictionary.tryAdd(systemStringType,
                                      systemExceptionType,
                                      exceptionMessageDN,
                                      exceptionValue)) ?? false else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryAdd should not throw and return true")
            
            return
        }
        
        guard let emptyString = System_String.empty else {
            XCTFail("System.String.Empty should return an instance")
            
            return
        }
        
        var valueForEmptyString: System_Object?

        guard !((try? dictionary.tryGetValue(systemStringType,
                                             systemExceptionType,
                                             emptyString,
                                             &valueForEmptyString)) ?? true),
              valueForEmptyString == nil else {
            XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>.TryGetValue should not throw and return false")

            return
        }
		
        guard let valueRet = try? dictionary.item(systemStringType,
                                                  systemExceptionType,
                                                  exceptionMessageDN) else {
			XCTFail("System.Collections.Generic.Dictionary<System.String, System.Exception>[] should not throw and return an instance")

			return
		}

        let equal = exceptionValue == valueRet
        XCTAssertTrue(equal)
    }
}
