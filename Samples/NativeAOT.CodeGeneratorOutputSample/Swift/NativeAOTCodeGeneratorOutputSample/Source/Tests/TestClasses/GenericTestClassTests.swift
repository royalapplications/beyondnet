import XCTest
import NativeAOTCodeGeneratorOutputSample

final class GenericTestClassTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testCreateWith1GenericArgument() {
		var exception: System_Exception_t?
		
		guard let systemStringType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemStringType) }
		
		guard let genericTestClass = NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_Create(systemStringType,
																								   &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String> ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_GenericTestClass_A1_Destroy(genericTestClass) }
		
		guard let genericTestClassType = System_Object_GetType(genericTestClass,
															   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericTestClassType) }
		
		guard let genericTestClassTypeName = String(dotNETString: System_Type_FullName_Get(genericTestClassType,
																						   &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`1[[System.String"))
	}
	
	func testCreateWith2GenericArguments() {
		var exception: System_Exception_t?
		
		guard let systemStringType = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemStringType) }
		
		guard let systemExceptionType = System_Exception_TypeOf() else {
			XCTFail("typeof(System.Exception) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(systemExceptionType) }
		
		guard let genericTestClass = NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_Create(systemStringType,
																								   systemExceptionType,
																								   &exception),
			  exception == nil else {
			XCTFail("GenericTestClass<System.String, System.Exception> ctor should not throw and return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_GenericTestClass_A2_Destroy(genericTestClass) }
		
		guard let genericTestClassType = System_Object_GetType(genericTestClass,
															   &exception),
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(genericTestClassType) }
		
		guard let genericTestClassTypeName = String(dotNETString: System_Type_FullName_Get(genericTestClassType,
																						   &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`2[[System.String"))
		XCTAssertTrue(genericTestClassTypeName.contains(",[System.Exception"))
	}
}
