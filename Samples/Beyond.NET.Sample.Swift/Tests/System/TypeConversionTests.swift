import XCTest
import BeyondDotNETSampleNative

final class TypeConversionTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testIs() {
        let systemObjectType = System.Object.self
        let systemObjectTypeDN = systemObjectType.typeOf
        
        let systemStringType = System.String.self
        let systemStringTypeDN = systemStringType.typeOf
        
        let systemExceptionType = System.Exception.self
        let systemExceptionTypeDN = systemExceptionType.typeOf
        
        let systemGuidType = System.Guid.self
        let systemGuidTypeDN = systemGuidType.typeOf
        
        // MARK: - System.Object
        guard let systemObject = try? System.Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemObject.is(systemObjectTypeDN))
        XCTAssertTrue(systemObject.is(systemObjectType))
        
        XCTAssertFalse(systemObject.is(systemStringTypeDN))
        XCTAssertFalse(systemObject.is(systemStringType))
        
        XCTAssertFalse(systemObject.is(systemExceptionTypeDN))
        XCTAssertFalse(systemObject.is(systemExceptionType))
        
        XCTAssertFalse(systemObject.is(systemGuidTypeDN))
        XCTAssertFalse(systemObject.is(systemGuidType))
        
        // MARK: - System.String
        guard let systemString = System.String.empty else {
            XCTFail("System.String.Empty getter should return an instance")
            
            return
        }
        
        XCTAssertTrue(systemString.is(systemObjectTypeDN))
        XCTAssertTrue(systemString.is(systemObjectType))
        
        XCTAssertTrue(systemString.is(systemStringTypeDN))
        XCTAssertTrue(systemString.is(systemStringType))
        
        XCTAssertFalse(systemString.is(systemExceptionTypeDN))
        XCTAssertFalse(systemString.is(systemExceptionType))
        
        XCTAssertFalse(systemString.is(systemGuidTypeDN))
        XCTAssertFalse(systemString.is(systemGuidType))
        
        // MARK: - System.Exception
        guard let systemException = try? System.Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemException.is(systemObjectTypeDN))
        XCTAssertTrue(systemException.is(systemObjectType))
        
        XCTAssertFalse(systemException.is(systemStringTypeDN))
        XCTAssertFalse(systemException.is(systemStringType))
        
        XCTAssertTrue(systemException.is(systemExceptionTypeDN))
        XCTAssertTrue(systemException.is(systemExceptionType))
        
        XCTAssertFalse(systemException.is(systemGuidTypeDN))
        XCTAssertFalse(systemException.is(systemGuidType))
        
        // MARK: - System.Guid
        guard let systemGuid = try? System.Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(systemGuid.is(systemObjectTypeDN))
        XCTAssertTrue(systemGuid.is(systemObjectType))
        
        XCTAssertFalse(systemGuid.is(systemStringTypeDN))
        XCTAssertFalse(systemGuid.is(systemStringType))
        
        XCTAssertFalse(systemGuid.is(systemExceptionTypeDN))
        XCTAssertFalse(systemGuid.is(systemExceptionType))
        
        XCTAssertTrue(systemGuid.is(systemGuidTypeDN))
        XCTAssertTrue(systemGuid.is(systemGuidType))
    }
    
    func testCastAs() {
        guard let systemObject = try? System.Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")

            return
        }

        guard let systemGuid = try? System.Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")

            return
        }

        guard let systemException = try? System.Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")

            return
        }

        guard let systemNullReferenceException = try? System.NullReferenceException() else {
            XCTFail("System.NullReferenceException ctor should not throw and return an instance")

            return
        }

        let systemObjectCastToSystemObject: System.Object? = systemObject.castAs()
        XCTAssertNotNil(systemObjectCastToSystemObject)

        let systemObjectCastToSystemGuid: System.Guid? = systemObject.castAs()
        XCTAssertNil(systemObjectCastToSystemGuid)

        let systemGuidCastToSystemGuid: System.Guid? = systemGuid.castAs()
        XCTAssertNotNil(systemGuidCastToSystemGuid)

        let systemGuidCastToSystemObject: System.Object? = systemGuid.castAs()
        XCTAssertNotNil(systemGuidCastToSystemObject)

        let systemExceptionCastToSystemNullReferenceException = systemException.castAs(System.NullReferenceException.self)
        XCTAssertNil(systemExceptionCastToSystemNullReferenceException)

        let systemNullReferenceExceptionCastToSystemException = systemNullReferenceException.castAs(System.Exception.self)
        XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
    }
    
    func testCastTo() {
        guard let systemObject = try? System.Object() else {
            XCTFail("System.Object ctor should not throw and return an instance")

            return
        }

        guard let systemGuid = try? System.Guid.newGuid() else {
            XCTFail("System.Guid.NewGuid should not throw and return an instance")

            return
        }

        guard let systemException = try? System.Exception() else {
            XCTFail("System.Exception ctor should not throw and return an instance")

            return
        }

        guard let systemNullReferenceException = try? System.NullReferenceException() else {
            XCTFail("System.NullReferenceException ctor should not throw and return an instance")

            return
        }

        do {
            let systemObjectCastToSystemObject = try systemObject.castTo(System.Object.self)
            XCTAssertNotNil(systemObjectCastToSystemObject)
        } catch {
            XCTFail("Should not throw")
            return
        }

        do {
            let _: System.Guid = try systemObject.castTo()

            XCTFail("Should throw")
            return
        } catch { }

        do {
            let systemGuidCastToSystemGuid = try systemGuid.castTo(System.Guid.self)
            XCTAssertNotNil(systemGuidCastToSystemGuid)
        } catch {
            XCTFail("Should not throw")
            return
        }

        do {
            let systemGuidCastToSystemObject = try systemGuid.castTo(System.Object.self)
            XCTAssertNotNil(systemGuidCastToSystemObject)
        } catch {
            XCTFail("Should not throw")
            return
        }

        do {
            let _: System.NullReferenceException = try systemException.castTo()

            XCTFail("Should throw")
            return
        } catch { }

        do {
            let systemNullReferenceExceptionCastToSystemException = try systemNullReferenceException.castTo(System.Exception.self)
            XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
        } catch {
            XCTFail("Should not throw")
            return
        }
    }
}