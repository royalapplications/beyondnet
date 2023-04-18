import XCTest
import NativeAOTCodeGeneratorOutputSample

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
		var exception: System_Exception_t?
		
		let systemObjectType = System_Object_TypeOf()
		defer { System_Type_Destroy(systemObjectType) }
		
		let systemStringType = System_String_TypeOf()
		defer { System_Type_Destroy(systemStringType) }
		
		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }
		
		let systemGuidType = System_Guid_TypeOf()
		defer { System_Type_Destroy(systemGuidType) }
		
		// MARK: - System.Object
		guard let systemObject = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(systemObject) }
		
		XCTAssertTrue(DNObjectIs(systemObject, systemObjectType))
		XCTAssertFalse(DNObjectIs(systemObject, systemStringType))
		XCTAssertFalse(DNObjectIs(systemObject, systemExceptionType))
		XCTAssertFalse(DNObjectIs(systemObject, systemGuidType))
		
		// MARK: - System.String
		guard let systemString = System_String_Empty_Get() else {
			XCTFail("System.String.Empty getter should return an instance")
			
			return
		}
		
		defer { System_String_Destroy(systemString) }
		
		XCTAssertTrue(DNObjectIs(systemString, systemObjectType))
		XCTAssertTrue(DNObjectIs(systemString, systemStringType))
		XCTAssertFalse(DNObjectIs(systemString, systemExceptionType))
		XCTAssertFalse(DNObjectIs(systemString, systemGuidType))
		
		// MARK: - System.Exception
		guard let systemException = System_Exception_Create(&exception),
			  exception == nil else {
			XCTFail("System.Exception ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Exception_Destroy(systemException) }
		
		XCTAssertTrue(DNObjectIs(systemException, systemObjectType))
		XCTAssertFalse(DNObjectIs(systemException, systemStringType))
		XCTAssertTrue(DNObjectIs(systemException, systemExceptionType))
		XCTAssertFalse(DNObjectIs(systemException, systemGuidType))
		
		// MARK: - System.Guid
		guard let systemGuid = System_Guid_NewGuid(&exception),
			  exception == nil else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(systemGuid) }
		
		XCTAssertTrue(DNObjectIs(systemGuid, systemObjectType))
		XCTAssertFalse(DNObjectIs(systemGuid, systemStringType))
		XCTAssertFalse(DNObjectIs(systemGuid, systemExceptionType))
		XCTAssertTrue(DNObjectIs(systemGuid, systemGuidType))
	}
	
	func testCastAs() {
		var exception: System_Exception_t?
		
		let systemObjectType = System_Object_TypeOf()
		defer { System_Type_Destroy(systemObjectType) }
		
		let systemGuidType = System_Guid_TypeOf()
		defer { System_Type_Destroy(systemGuidType) }
		
		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }
		
		let systemNullReferenceExceptionType = System_NullReferenceException_TypeOf()
		defer { System_Type_Destroy(systemNullReferenceExceptionType) }
		
		guard let systemObject = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(systemObject) }
		
		guard let systemGuid = System_Guid_NewGuid(&exception),
			  exception == nil else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(systemGuid) }
		
		guard let systemException = System_Exception_Create(&exception),
			  exception == nil else {
			XCTFail("System.Exception ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Exception_Destroy(systemException) }
		
		guard let systemNullReferenceException = System_NullReferenceException_Create(&exception),
			  exception == nil else {
			XCTFail("System.NullReferenceException ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Exception_Destroy(systemNullReferenceException) }
		
		let systemObjectCastToSystemObject = DNObjectCastAs(systemObject, systemObjectType)
		XCTAssertNotNil(systemObjectCastToSystemObject)
		System_Object_Destroy(systemObjectCastToSystemObject)
		
		let systemObjectCastToSystemGuid = DNObjectCastAs(systemObject, systemGuidType)
		XCTAssertNil(systemObjectCastToSystemGuid)
		
		let systemGuidCastToSystemGuid = DNObjectCastAs(systemGuid, systemGuidType)
		XCTAssertNotNil(systemGuidCastToSystemGuid)
		System_Guid_Destroy(systemGuidCastToSystemGuid)
		
		let systemGuidCastToSystemObject = DNObjectCastAs(systemGuid, systemObjectType)
		XCTAssertNotNil(systemGuidCastToSystemObject)
		System_Object_Destroy(systemGuidCastToSystemObject)
		
		let systemExceptionCastToSystemNullReferenceException = DNObjectCastAs(systemException, systemNullReferenceExceptionType)
		XCTAssertNil(systemExceptionCastToSystemNullReferenceException)
		
		let systemNullReferenceExceptionCastToSystemException = DNObjectCastAs(systemNullReferenceException, systemExceptionType)
		XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
		System_NullReferenceException_Destroy(systemNullReferenceExceptionCastToSystemException)
	}
	
	func testCastTo() {
		var exception: System_Exception_t?
		
		let systemObjectType = System_Object_TypeOf()
		defer { System_Type_Destroy(systemObjectType) }
		
		let systemGuidType = System_Guid_TypeOf()
		defer { System_Type_Destroy(systemGuidType) }
		
		let systemExceptionType = System_Exception_TypeOf()
		defer { System_Type_Destroy(systemExceptionType) }
		
		let systemNullReferenceExceptionType = System_NullReferenceException_TypeOf()
		defer { System_Type_Destroy(systemNullReferenceExceptionType) }
		
		guard let systemObject = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(systemObject) }
		
		guard let systemGuid = System_Guid_NewGuid(&exception),
			  exception == nil else {
			XCTFail("System.Guid.NewGuid should not throw and return an instance")
			
			return
		}
		
		defer { System_Guid_Destroy(systemGuid) }
		
		guard let systemException = System_Exception_Create(&exception),
			  exception == nil else {
			XCTFail("System.Exception ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Exception_Destroy(systemException) }
		
		guard let systemNullReferenceException = System_NullReferenceException_Create(&exception),
			  exception == nil else {
			XCTFail("System.NullReferenceException ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Exception_Destroy(systemNullReferenceException) }
		
		let systemObjectCastToSystemObject = DNObjectCastTo(systemObject, systemObjectType, &exception)
		XCTAssertNil(exception)
		XCTAssertNotNil(systemObjectCastToSystemObject)
		System_Object_Destroy(systemObjectCastToSystemObject)
		
		let systemObjectCastToSystemGuid = DNObjectCastTo(systemObject, systemGuidType, &exception)
		XCTAssertNotNil(exception)
		XCTAssertNil(systemObjectCastToSystemGuid)
		System_Exception_Destroy(exception)
		exception = nil
		
		let systemGuidCastToSystemGuid = DNObjectCastTo(systemGuid, systemGuidType, &exception)
		XCTAssertNil(exception)
		XCTAssertNotNil(systemGuidCastToSystemGuid)
		System_Guid_Destroy(systemGuidCastToSystemGuid)
		
		let systemGuidCastToSystemObject = DNObjectCastTo(systemGuid, systemObjectType, &exception)
		XCTAssertNil(exception)
		XCTAssertNotNil(systemGuidCastToSystemObject)
		System_Object_Destroy(systemGuidCastToSystemObject)
		
		let systemExceptionCastToSystemNullReferenceException = DNObjectCastTo(systemException, systemNullReferenceExceptionType, &exception)
		XCTAssertNotNil(exception)
		XCTAssertNil(systemExceptionCastToSystemNullReferenceException)
		System_Exception_Destroy(exception)
		exception = nil
		
		let systemNullReferenceExceptionCastToSystemException = DNObjectCastTo(systemNullReferenceException, systemExceptionType, &exception)
		XCTAssertNil(exception)
		XCTAssertNotNil(systemNullReferenceExceptionCastToSystemException)
		System_NullReferenceException_Destroy(systemNullReferenceExceptionCastToSystemException)
	}
}
