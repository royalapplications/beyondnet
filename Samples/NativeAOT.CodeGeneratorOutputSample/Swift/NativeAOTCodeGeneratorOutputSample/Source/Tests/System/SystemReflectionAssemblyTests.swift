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
	}
}
