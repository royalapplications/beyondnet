import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemTypeTests: XCTestCase {
	func testSystemType() {
		var exception: System_Exception_t?
		
		let systemObjectTypeName = "System.Object"
		
        guard let systemObjectType = System_Object_TypeOf() else {
            XCTFail("typeof(System.Object) should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemObjectType) }
        
		guard let systemObjectTypeViaName = System_Type_GetType_2(systemObjectTypeName,
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
		
		guard let retrievedSystemObjectTypeNameC = System_Type_FullName_Get(systemObjectType,
																			&exception),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { retrievedSystemObjectTypeNameC.deallocate() }
		
		let retrievedSystemObjectTypeName = String(cString: retrievedSystemObjectTypeNameC)
		
		XCTAssertEqual(systemObjectTypeName, retrievedSystemObjectTypeName)
	}
	
	func testInvalidType() {
		var exception: System_Exception_t?
		
		let invalidTypeName = "! This.Type.Surely.Does.Not.Exist !"
		
		let invalidType = System_Type_GetType_1(invalidTypeName,
												true,
												&exception)
		
		XCTAssertNil(invalidType)
		
		guard let exception else {
			XCTFail("System.Type.GetType should throw")
			
			return
		}
		
		defer { System_Exception_Destroy(exception) }
		
		var exception2: System_Exception_t?
		
		guard let exceptionMessageC = System_Exception_Message_Get(exception,
																   &exception2),
			  exception2 == nil else {
			XCTFail("System.Exception.Message getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { exceptionMessageC.deallocate() }
		
		let exceptionMessage = String(cString: exceptionMessageC)
		
		XCTAssertTrue(exceptionMessage.contains("The type \'\(invalidTypeName)\' cannot be found"))
	}
	
	func testReflectingOverSystemObject() {
		var exception: System_Exception_t?
		
		guard let typeOfSystemObject = System_Object_TypeOf() else {
			XCTFail("typeof(System.Object) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(typeOfSystemObject) }
		
		let methodName = "ToString"
		
		guard let toStringMethod = System_Type_GetMethod(typeOfSystemObject,
														 methodName,
														 &exception),
			  exception == nil else {
			XCTFail("System.Type.GetMethod should not throw and return an instance")
			
			return
		}
		
		defer { System_Reflection_MethodInfo_Destroy(toStringMethod) }
		
		guard let methodNameRetC = System_Reflection_MemberInfo_Name_Get(toStringMethod,
																	  &exception),
			  exception == nil else {
			XCTFail("System.Reflection.MemberInfo.Name getter should not throw and return an instance of a C String")
			
			return
		}
		
		defer { methodNameRetC.deallocate() }
		
		let methodNameRet = String(cString: methodNameRetC)
		
		XCTAssertEqual(methodName, methodNameRet)
		
		guard let returnType = System_Reflection_MethodInfo_ReturnType_Get(toStringMethod,
																		   &exception),
			  exception == nil else {
			XCTFail("System.Reflection.MethodInfo.ReturnType getter should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(returnType) }
		
		guard let typeOfSystemString = System_String_TypeOf() else {
			XCTFail("typeof(System.String) should return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(typeOfSystemString) }
		
		let equalReturnTypes = System_Object_Equals(typeOfSystemString,
													returnType,
													&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(equalReturnTypes)
		
		let isReturnTypeAssignableToSystemObjectType = System_Type_IsAssignableTo(returnType,
																				  typeOfSystemObject,
																				  &exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(isReturnTypeAssignableToSystemObjectType)
		
		let isSystemObjectTypeAssignableFromSystemStringType = System_Type_IsAssignableFrom(typeOfSystemObject,
																							returnType,
																							&exception)
		
		XCTAssertNil(exception)
		XCTAssertTrue(isSystemObjectTypeAssignableFromSystemStringType)
	}
}
