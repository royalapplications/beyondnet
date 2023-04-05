import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemReflectionAssemblyTests: XCTestCase {
	func testAssembly() {
		var exception: System_Exception_t?
		
		guard let assembly = System_Reflection_Assembly_GetExecutingAssembly(&exception),
			  exception == nil else {
			XCTFail("System.Reflection.Assembly.GetExecutingAssembly should not throw and return an instance")
			
			return
		}
		
		defer { System_Reflection_Assembly_Destroy(assembly) }
		
		guard let assemblyName = System_Reflection_Assembly_GetName(assembly,
																	&exception),
			  exception == nil else {
			XCTFail("System.Reflection.Assembly.GetName should not throw and return an instance")
			
			return
		}
		
		defer { System_Reflection_AssemblyName_Destroy(assemblyName) }
		
		guard let assemblyNameStringC = System_Reflection_AssemblyName_Name_Get(assemblyName,
																				&exception),
			  exception == nil else {
			XCTFail("System.Reflection.AssemblyName.Name getter should not throw and return an instance of a C string")
			
			return
		}
		
		defer { assemblyNameStringC.deallocate() }
		
		let assemblyNameString = String(cString: assemblyNameStringC)
		
		XCTAssertEqual("NativeAOT.CodeGeneratorOutputSample", assemblyNameString)
		
		guard let typesArray = System_Reflection_Assembly_GetExportedTypes(assembly,
																		   &exception),
			  exception == nil else {
			XCTFail("System.Reflection.Assembly.GetTypes should not throw and return an instance")
			
			return
		}
		
		defer { System_Array_Destroy(typesArray) }
		
		let typesLength = System_Array_Length_Get(typesArray,
												  &exception)
		
		XCTAssertNil(exception)
		XCTAssertGreaterThanOrEqual(typesLength, 1)
		
		var types = [System_Type_t]()
		
		for i in 0..<typesLength {
			guard let type = System_Array_GetValue_1(typesArray,
													 i,
													 &exception),
				  exception == nil else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			types.append(type)
		}
		
		defer {
			for type in types {
				System_Type_Destroy(type)
			}
		}
		
		let personType = NativeAOT_CodeGeneratorInputSample_Person_TypeOf()
		defer { System_Type_Destroy(personType) }
		
		var personTypeFound = false
		
		for type in types {
			let equal = System_Object_Equals(type,
											 personType,
											 &exception)
			
			XCTAssertNil(exception)
			
			if equal {
				personTypeFound = true
				break
			}
		}
		
		XCTAssertTrue(personTypeFound)
	}
}
