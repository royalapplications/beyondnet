import XCTest
import NativeAOTCodeGeneratorOutputSample

final class TypeConversionTestsSwift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testIs() {
        let systemObjectType = System_Object.typeOf()
        let systemStringType = System_String.typeOf()
        let systemExceptionType = System_Exception.typeOf()
        let systemGuidType = System_Guid.typeOf()
        
        // MARK: - System.Object
        guard let systemObject = try? System_Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemObject.is(systemObjectType))
        XCTAssertFalse(systemObject.is(systemStringType))
        XCTAssertFalse(systemObject.is(systemExceptionType))
        XCTAssertFalse(systemObject.is(systemGuidType))
        
        // MARK: - System.String
        guard let systemString = System_String.empty_get() else {
            XCTFail("System.String.Empty getter should return an instance")
            
            return
        }
        
        XCTAssertTrue(systemString.is(systemObjectType))
        XCTAssertTrue(systemString.is(systemStringType))
        XCTAssertFalse(systemString.is(systemExceptionType))
        XCTAssertFalse(systemString.is(systemGuidType))
        
        // MARK: - System.Exception
        guard let systemException = try? System_Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemException.is(systemObjectType))
        XCTAssertFalse(systemException.is(systemStringType))
        XCTAssertTrue(systemException.is(systemExceptionType))
        XCTAssertFalse(systemException.is(systemGuidType))
        
        // MARK: - System.Guid
        guard let systemGuid = try? System_Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemGuid.is(systemObjectType))
        XCTAssertFalse(systemGuid.is(systemStringType))
        XCTAssertFalse(systemGuid.is(systemExceptionType))
        XCTAssertTrue(systemGuid.is(systemGuidType))
    }
    
    func testCastAs() {
        let systemObjectType = System_Object.typeOf()
        let systemGuidType = System_Guid.typeOf()
        let systemExceptionType = System_Exception.typeOf()
        let systemNullReferenceExceptionType = System_NullReferenceException.typeOf()

        guard let systemObject = try? System_Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")

            return
        }

        guard let systemGuid = try? System_Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")

            return
        }

        guard let systemException = try? System_Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")

            return
        }

        guard let systemNullReferenceException = try? System_NullReferenceException() else {
            XCTFail("System.NullReferenceException ctor should not throw and return an instance")

            return
        }

        let systemObjectCastToSystemObject = systemObject.castAs(systemObjectType)
        XCTAssertNotNil(systemObjectCastToSystemObject)

        let systemObjectCastToSystemGuid = systemObject.castAs(systemGuidType)
        XCTAssertNil(systemObjectCastToSystemGuid)

        let systemGuidCastToSystemGuid = systemGuid.castAs(systemGuidType)
        XCTAssertNotNil(systemGuidCastToSystemGuid)

        let systemGuidCastToSystemObject = systemGuid.castAs(systemObjectType)
        XCTAssertNotNil(systemGuidCastToSystemObject)

        let systemExceptionCastToSystemNullReferenceException = systemException.castAs(systemNullReferenceExceptionType)
        XCTAssertNil(systemExceptionCastToSystemNullReferenceException)

        let systemNullReferenceExceptionCastToSystemException = systemNullReferenceException.castAs(systemExceptionType)
        XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
    }
    
    func testCastTo() {
        let systemObjectType = System_Object.typeOf()
        let systemGuidType = System_Guid.typeOf()
        let systemExceptionType = System_Exception.typeOf()
        let systemNullReferenceExceptionType = System_NullReferenceException.typeOf()
        
        guard let systemObject = try? System_Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")

            return
        }

        guard let systemGuid = try? System_Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")

            return
        }

        guard let systemException = try? System_Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")

            return
        }

        guard let systemNullReferenceException = try? System_NullReferenceException() else {
            XCTFail("System.NullReferenceException ctor should not throw and return an instance")

            return
        }
        
        do {
            let systemObjectCastToSystemObject = try systemObject.castTo(systemObjectType)
            XCTAssertNotNil(systemObjectCastToSystemObject)
        } catch {
            XCTFail("Should not throw")
            return
        }
        
        do {
            let _ = try systemObject.castTo(systemGuidType)
            
            XCTFail("Should throw")
            return
        } catch { }
        
        do {
            let systemGuidCastToSystemGuid = try systemGuid.castTo(systemGuidType)
            XCTAssertNotNil(systemGuidCastToSystemGuid)
        } catch {
            XCTFail("Should not throw")
            return
        }
        
        do {
            let systemGuidCastToSystemObject = try systemGuid.castTo(systemObjectType)
            XCTAssertNotNil(systemGuidCastToSystemObject)
        } catch {
            XCTFail("Should not throw")
            return
        }
        
        do {
            let _ = try systemException.castTo(systemNullReferenceExceptionType)
            
            XCTFail("Should throw")
            return
        } catch { }
        
        do {
            let systemNullReferenceExceptionCastToSystemException = try systemNullReferenceException.castTo(systemExceptionType)
            XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
        } catch {
            XCTFail("Should not throw")
            return
        }
    }
}
