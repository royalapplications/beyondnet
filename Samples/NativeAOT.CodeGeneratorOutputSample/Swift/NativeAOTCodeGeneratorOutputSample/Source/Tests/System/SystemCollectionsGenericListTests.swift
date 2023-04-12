import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemCollectionsGenericListTests: XCTestCase {
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
		
		guard let systemStringType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemStringType) }
		
		guard let list = System_Collections_Generic_List_A1_Create(systemStringType,
																   &exception),
			  exception == nil else {
			XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
			
			return
		}
		
		defer { System_Collections_Generic_List_A1_Destroy(list) }
		
		guard let listType = System_Object_GetType(list,
												   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(listType) }
		
		guard let listTypeName = String(dotNETString: System_Type_FullName_Get(listType,
																			   &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(listTypeName.contains("System.Collections.Generic.List`1[[System.String"))
	}
}
