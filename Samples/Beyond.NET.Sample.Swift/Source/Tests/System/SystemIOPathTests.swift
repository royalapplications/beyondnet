import XCTest
import BeyondNETSampleSwift

final class SystemIOPathTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testPath() {
		var exception: System_Exception_t?
		
		let dirPath = "/some/path"
		let dirPathDN = dirPath.cDotNETString()
		defer { System_String_Destroy(dirPathDN) }
		
		let fileName = "a file.txt"
		let fileNameDN = fileName.cDotNETString()
		defer { System_String_Destroy(fileNameDN) }
		
		let expectedFilePath = (dirPath as NSString).appendingPathComponent(fileName)
		let expectedFilePathDN = expectedFilePath.cDotNETString()
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
		
		guard let filePath = String(cDotNETString: filePathDN) else {
			XCTFail("Failed to convert string")
			
			return
		}
		
		XCTAssertEqual(expectedFilePath, filePath)
		
		guard let fileExtension = String(cDotNETString: System_IO_Path_GetExtension(filePathDN,
																				   &exception),
										 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.IO.Path.GetExtension should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedFileExtension, fileExtension)
		
		guard let fileNameWithoutExtension = String(cDotNETString: System_IO_Path_GetFileNameWithoutExtension(expectedFilePathDN,
																											 &exception),
													destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.IO.Path.GetFileNameWithoutExtension should not throw and return an instance of a C String")
			
			return
		}
		
		XCTAssertEqual(expectedFileNameWithoutExtension, fileNameWithoutExtension)
	}
}
