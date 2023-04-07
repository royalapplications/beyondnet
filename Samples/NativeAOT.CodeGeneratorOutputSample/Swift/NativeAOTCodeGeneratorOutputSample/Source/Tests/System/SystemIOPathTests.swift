import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemIOPathTests: XCTestCase {
	override class func setUp() {
		Self.gcCollect()
	}
	
	override class func tearDown() {
		Self.gcCollect()
	}
	
	func testPath() {
		var exception: System_Exception_t?
		
		let dirPath = "/some/path"
		let dirPathDN = dirPath.dotNETString()
		defer { System_String_Destroy(dirPathDN) }
		
		let fileName = "a file.txt"
		let fileNameDN = fileName.dotNETString()
		defer { System_String_Destroy(fileNameDN) }
		
		let expectedFilePath = (dirPath as NSString).appendingPathComponent(fileName)
		let expectedFilePathDN = expectedFilePath.dotNETString()
		defer { System_String_Destroy(expectedFilePathDN) }
		
		let expectedFileExtension = ".\((expectedFilePath as NSString).pathExtension)"
		let expectedFileNameWithoutExtension = (fileName as NSString).deletingPathExtension
		
		guard let filePathDN = System_IO_Path_Combine(dirPathDN,
													  fileNameDN,
													  &exception),
			  exception == nil else {
			XCTFail("System.IO.Path.Combine should not throw and return an instance")
			
			return
		}
		
		defer { System_String_Destroy(filePathDN) }
		
		guard let filePath = String(dotNETString: filePathDN) else {
			XCTFail("Failed to convert string")
			
			return
		}
		
		XCTAssertEqual(expectedFilePath, filePath)
		
		guard let fileExtension = String(dotNETString: System_IO_Path_GetExtension(filePathDN,
																				   &exception),
										 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.IO.Path.GetExtension should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedFileExtension, fileExtension)
		
		guard let fileNameWithoutExtension = String(dotNETString: System_IO_Path_GetFileNameWithoutExtension(expectedFilePathDN,
																											 &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.IO.Path.GetFileNameWithoutExtension should not throw and return an instance of a C String")
			
			return
		}
		
		XCTAssertEqual(expectedFileNameWithoutExtension, fileNameWithoutExtension)
	}
}
