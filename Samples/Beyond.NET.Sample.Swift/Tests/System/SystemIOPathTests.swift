import XCTest
import BeyondDotNETSampleKit

final class SystemIOPathTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testPath() throws {
        let dirPath = "/some/path"
        let dirPathDN = dirPath.dotNETString()
        
        let fileName = "a file.txt"
        let fileNameDN = fileName.dotNETString()
        
        let expectedFilePath = (dirPath as NSString).appendingPathComponent(fileName)
        let expectedFilePathDN = expectedFilePath.dotNETString()
        
        let expectedFileExtension = ".\((expectedFilePath as NSString).pathExtension)"
        let expectedFileNameWithoutExtension = (fileName as NSString).deletingPathExtension
        
        let filePathDN = try System_IO_Path.combine(dirPathDN,
                                                    fileNameDN)
        
        let filePath = filePathDN.string()
        XCTAssertEqual(expectedFilePath, filePath)
        
        guard let fileExtension = try? System_IO_Path.getExtension(filePathDN)?.string() else {
            XCTFail("System.IO.Path.GetExtension should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedFileExtension, fileExtension)
        
        guard let fileNameWithoutExtension = try? System_IO_Path.getFileNameWithoutExtension(expectedFilePathDN)?.string() else {
            XCTFail("System.IO.Path.GetFileNameWithoutExtension should not throw and return an instance of a C String")
            
            return
        }
        
        XCTAssertEqual(expectedFileNameWithoutExtension, fileNameWithoutExtension)
    }
}
