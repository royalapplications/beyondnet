import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTypeTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testSystemType() {
		var exception: System_Exception_t?
		
		let systemObjectTypeName = "System.Object"
		
        guard let systemObjectType = System_Object_TypeOf() else {
            XCTFail("typeof(System.Object) should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemObjectType) }
		
		let systemObjectTypeNameDN = systemObjectTypeName.cDotNETString()
		defer { System_String_Destroy(systemObjectTypeNameDN) }
        
		guard let systemObjectTypeViaName = System_Type_GetType_2(systemObjectTypeNameDN,
																  &exception),
			  exception == nil else {
			XCTFail("System.Type.GetType should not throw and return an instance of System.Type")
			
			return
		}
		
		defer { System_Type_Destroy(systemObjectTypeViaName) }
        
        guard System_Object_Equals(systemObjectType,
                                   systemObjectTypeViaName,
                                   &exception),
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
		
		guard let retrievedSystemObjectTypeName = String(cDotNETString: System_Type_FullName_Get(systemObjectType,
																								&exception),
														 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
	}
	
	func testInvalidType() {
		var exception: System_Exception_t?
		
		let invalidTypeName = "! This.Type.Surely.Does.Not.Exist !"
		let invalidTypeNameDN = invalidTypeName.cDotNETString()
		
		defer { System_String_Destroy(invalidTypeNameDN) }
		
		let invalidType = System_Type_GetType_1(invalidTypeNameDN,
												true,
												&exception)
		
		XCTAssertNil(invalidType)
		
		guard let exception else {
			XCTFail("System.Type.GetType should throw")
			
			return
		}
		
		defer { System_Exception_Destroy(exception) }
		
		var exception2: System_Exception_t?
		
		guard let exceptionMessage = String(cDotNETString: System_Exception_Message_Get(exception,
																					   &exception2),
											destroyDotNETString: true),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C String")
			
			return
		}
		
		XCTAssertTrue(exceptionMessage.contains("The type \'\(invalidTypeName)\' cannot be found"))
	}
	
	// TODO: Fails since .NET 8 Preview 3
//	func testReflectingOverSystemObject() {
//		var exception: System_Exception_t?
//
//		guard let typeOfSystemObject = System_Object_TypeOf() else {
//			XCTFail("typeof(System.Object) should return an instance")
//
//			return
//		}
//
//		defer { System_Type_Destroy(typeOfSystemObject) }
//
//		let methodName = "ToString"
//		let methodNameDN = methodName.cDotNETString()
//
//		defer { System_String_Destroy(methodNameDN) }
//
//		guard let toStringMethod = System_Type_GetMethod(typeOfSystemObject,
//														 methodNameDN,
//														 &exception),
//			  exception == nil else {
//			XCTFail("System.Type.GetMethod should not throw and return an instance")
//
//			return
//		}
//
//		defer { System_Reflection_MethodInfo_Destroy(toStringMethod) }
//
//		guard let methodNameRet = String(cDotNETString: System_Reflection_MemberInfo_Name_Get(toStringMethod,
//																							 &exception),
//										 destroyDotNETString: true),
//			  exception == nil else {
//			XCTFail("System.Reflection.MemberInfo.Name getter should not throw and return an instance of a C String")
//
//			return
//		}
//
//		XCTAssertEqual(methodName, methodNameRet)
//
//		guard let returnType = System_Reflection_MethodInfo_ReturnType_Get(toStringMethod,
//																		   &exception),
//			  exception == nil else {
//			XCTFail("System.Reflection.MethodInfo.ReturnType getter should not throw and return an instance")
//
//			return
//		}
//
//		defer { System_Type_Destroy(returnType) }
//
//		guard let typeOfSystemString = System_String_TypeOf() else {
//			XCTFail("typeof(System.String) should return an instance")
//
//			return
//		}
//
//		defer { System_Type_Destroy(typeOfSystemString) }
//
//		let equalReturnTypes = System_Object_Equals(typeOfSystemString,
//													returnType,
//													&exception)
//
//		XCTAssertNil(exception)
//		XCTAssertTrue(equalReturnTypes)
//
//		let isReturnTypeAssignableToSystemObjectType = System_Type_IsAssignableTo(returnType,
//																				  typeOfSystemObject,
//																				  &exception)
//
//		XCTAssertNil(exception)
//		XCTAssertTrue(isReturnTypeAssignableToSystemObjectType)
//
//		let isSystemObjectTypeAssignableFromSystemStringType = System_Type_IsAssignableFrom(typeOfSystemObject,
//																							returnType,
//																							&exception)
//
//		XCTAssertNil(exception)
//		XCTAssertTrue(isSystemObjectTypeAssignableFromSystemStringType)
//	}
}
