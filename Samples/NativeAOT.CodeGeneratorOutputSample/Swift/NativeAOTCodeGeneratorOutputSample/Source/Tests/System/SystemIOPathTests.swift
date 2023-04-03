import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemIOPathTests: XCTestCase {
	func testPath() {
		var exception: System_Exception_t?
		
		let dirPath = "/some/path"
		let fileName = "a file.txt"
		
		let expectedFilePath = (dirPath as NSString).appendingPathComponent(fileName)
		let expectedFileExtension = ".\((expectedFilePath as NSString).pathExtension)"
		let expectedFileNameWithoutExtension = (fileName as NSString).deletingPathExtension
		
		guard let filePathC = System_IO_Path_Combine(dirPath,
													 fileName,
													 &exception),
			  exception == nil else {
			XCTFail("System.IO.Path.Combine should not throw and return an instance of a C String")
			
			return
		}
		
		defer { filePathC.deallocate() }
		
		let filePath = String(cString: filePathC)
		
		XCTAssertEqual(expectedFilePath, filePath)
		
		guard let fileExtensionC = System_IO_Path_GetExtension(filePath,
															   &exception),
			  exception == nil else {
			XCTFail("System.IO.Path.GetExtension should not throw and return an instance of a C String")
			
			return
		}
		
		defer { fileExtensionC.deallocate() }
		
		let fileExtension = String(cString: fileExtensionC)
		
		XCTAssertEqual(expectedFileExtension, fileExtension)
		
		guard let fileNameWithoutExtensionC = System_IO_Path_GetFileNameWithoutExtension(expectedFilePath,
																						 &exception),
			  exception == nil else {
			XCTFail("System.IO.Path.GetFileNameWithoutExtension should not throw and return an instance of a C String")
			
			return
		}
		
		defer { fileNameWithoutExtensionC.deallocate() }
		
		let fileNameWithoutExtension = String(cString: fileNameWithoutExtensionC)
		
		XCTAssertEqual(expectedFileNameWithoutExtension, fileNameWithoutExtension)
	}
}
