import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemRuntimeInteropServicesMarshalTests: XCTestCase {
	func testMarshalAlloc() {
		var exception: System_Exception_t?
		
		let value: Int32 = 59
		let sizeOfInt32 = MemoryLayout.size(ofValue: Int32.self)
		
		let ptr = System_Runtime_InteropServices_Marshal_AllocHGlobal_1(sizeOfInt32,
																		&exception)
		
		XCTAssertNil(exception)
		
		defer {
			System_Runtime_InteropServices_Marshal_FreeHGlobal(ptr,
															   &exception)
			
			XCTAssertNil(exception)
		}
		
		System_Runtime_InteropServices_Marshal_WriteInt32_2(ptr,
															value,
															&exception)
		
		XCTAssertNil(exception)
		
		let retValue = System_Runtime_InteropServices_Marshal_ReadInt32_2(ptr,
																		  &exception)
		
		XCTAssertNil(exception)
		XCTAssertEqual(value, retValue)
	}
	
	func testMarshalStrings() {
		var exception: System_Exception_t?
		
		let string = "Hello World! 😀"
		var retStringC: CString?
		
		string.withCString { cString in
			retStringC = System_Runtime_InteropServices_Marshal_PtrToStringAuto_1(.init(bitPattern: cString),
																				  &exception)
		}
		
		guard let retStringC,
			  exception == nil else {
			XCTFail("System.Runtime.InteropServices.Marshal.PtrToStringAuto should not throw and return an instance of a C String")
			
			return
		}
		
		defer { retStringC.deallocate() }
		
		let retString = String(cString: retStringC)
		
		XCTAssertEqual(string, retString)
	}
}
