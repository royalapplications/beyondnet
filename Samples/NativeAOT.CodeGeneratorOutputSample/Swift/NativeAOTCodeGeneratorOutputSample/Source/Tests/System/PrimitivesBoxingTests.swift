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
	
	func testInt32() {
		boxAndUnbox(number: 1234 as Int32,
					expectedTypeName: "System.Int32",
					boxFunc: { input in
			DNObjectFromInt32(input)
		}, unboxFunc: { input, exception in
			DNObjectCastToInt32(input, &exception)
		})
	}
}

private extension PrimitivesBoxingTests {
	func boxAndUnbox<T>(number: T,
						expectedTypeName: String,
						boxFunc: (_ input: T) -> System_Object_t?,
						unboxFunc: (_ input: System_Object_t, inout System_Exception_t?) -> T?) where T: Equatable {
		var exception: System_Exception_t?
		
		guard let numberObject = boxFunc(number) else {
			XCTFail("Should return an instance")
			
			return
		}
		
		defer { System_Object_Destroy(numberObject) }
		
		guard let numberObjectType = System_Object_GetType(numberObject,
														   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(numberObjectType) }
		
		guard let numberObjectTypeName = String(dotNETString: System_Type_FullName_Get(numberObjectType,
																					   &exception),
												destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedTypeName, numberObjectTypeName)
		
		let numberRet = unboxFunc(numberObject,
								  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(number, numberRet)
		
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
	}
}
