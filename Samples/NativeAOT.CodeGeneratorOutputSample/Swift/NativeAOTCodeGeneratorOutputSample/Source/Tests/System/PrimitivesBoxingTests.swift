import XCTest
import NativeAOTCodeGeneratorOutputSample

final class PrimitivesBoxingTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testBool() {
		boxAndUnbox(value: true,
					expectedTypeName: "System.Boolean",
					boxFunc: { DNObjectFromBool($0) },
					unboxFunc: { DNObjectCastToBool($0, &$1) })
	}
	
	func testInt8() {
		boxAndUnbox(value: -123 as Int8,
					expectedTypeName: "System.SByte",
					boxFunc: { DNObjectFromInt8($0) },
					unboxFunc: { DNObjectCastToInt8($0, &$1) })
	}
	
	func testUInt8() {
		boxAndUnbox(value: 123 as UInt8,
					expectedTypeName: "System.Byte",
					boxFunc: { DNObjectFromUInt8($0) },
					unboxFunc: { DNObjectCastToUInt8($0, &$1) })
	}
	
	func testInt16() {
		boxAndUnbox(value: -1234 as Int16,
					expectedTypeName: "System.Int16",
					boxFunc: { DNObjectFromInt16($0) },
					unboxFunc: { DNObjectCastToInt16($0, &$1) })
	}
	
	func testUInt16() {
		boxAndUnbox(value: 1234 as UInt16,
					expectedTypeName: "System.UInt16",
					boxFunc: { DNObjectFromUInt16($0) },
					unboxFunc: { DNObjectCastToUInt16($0, &$1) })
	}
	
	func testInt32() {
		boxAndUnbox(value: -123456789 as Int32,
					expectedTypeName: "System.Int32",
					boxFunc: { DNObjectFromInt32($0) },
					unboxFunc: { DNObjectCastToInt32($0, &$1) })
	}
	
	func testUInt32() {
		boxAndUnbox(value: 123456789 as UInt32,
					expectedTypeName: "System.UInt32",
					boxFunc: { DNObjectFromUInt32($0) },
					unboxFunc: { DNObjectCastToUInt32($0, &$1) })
	}
	
	func testInt64() {
		boxAndUnbox(value: -123456789123456789 as Int64,
					expectedTypeName: "System.Int64",
					boxFunc: { DNObjectFromInt64($0) },
					unboxFunc: { DNObjectCastToInt64($0, &$1) })
	}
	
	func testUInt64() {
		boxAndUnbox(value: 123456789123456789 as UInt64,
					expectedTypeName: "System.UInt64",
					boxFunc: { DNObjectFromUInt64($0) },
					unboxFunc: { DNObjectCastToUInt64($0, &$1) })
	}
}

private extension PrimitivesBoxingTests {
	func boxAndUnbox<T>(value: T,
						expectedTypeName: String,
						boxFunc: (_ input: T) -> System_Object_t?,
						unboxFunc: (_ input: System_Object_t, inout System_Exception_t?) -> T?) where T: Equatable {
		var exception: System_Exception_t?
		
		guard let valueObject = boxFunc(value) else {
			XCTFail("Should return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(valueObject) }
		
		guard let valueObjectType = System_Object_GetType(valueObject,
														   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(valueObjectType) }
		
		guard let numberObjectTypeName = String(dotNETString: System_Type_FullName_Get(valueObjectType,
																					   &exception),
												destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedTypeName, numberObjectTypeName)
		
		let valueRet = unboxFunc(valueObject,
								 &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, valueRet)
		
		guard let systemObject = System_Object_Create(&exception),
			  exception == nil else {
			XCTFail("System.Object ctor should return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(systemObject) }
		
		_ = unboxFunc(systemObject,
					  &exception)
		
		XCTAssertNotNil(exception)
		System_Exception_Destroy(exception)
		
		// TODO: Stuff into array and retrieve it again
	}
}
