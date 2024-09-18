import XCTest
import BeyondDotNETSampleKit

final class SystemVersionTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemVersionFromComponents() throws {
        let systemVersionType = System_Version.typeOf
        
        let major: Int32    = 1
        let minor: Int32    = 2
        let build: Int32    = 3
        let revision: Int32 = 4
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        
        let version = try System_Version(major,
                                         minor,
                                         build,
                                         revision)
        
        let versionFromComponentsType = try version.getType()
        
        guard systemVersionType == versionFromComponentsType else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        let majorRet = try version.major
        XCTAssertEqual(major, majorRet)
        
        let minorRet = try version.minor
        XCTAssertEqual(minor, minorRet)
        
        let buildRet = try version.build
        XCTAssertEqual(build, buildRet)
        
        let revisionRet = try version.revision
        XCTAssertEqual(revision, revisionRet)
        
        let versionStringRet = try version.toString().string()
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionFromString() throws {
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        let versionStringDN = versionString.dotNETString()
        
        let version = try System_Version(versionStringDN)
        
        let majorRet = try version.major
        XCTAssertEqual(major, majorRet)
        
        let minorRet = try version.minor
        XCTAssertEqual(minor, minorRet)
        
        let buildRet = try version.build
        XCTAssertEqual(build, buildRet)
        
        let revisionRet = try version.revision
        XCTAssertEqual(revision, revisionRet)
        
        let versionStringRet = try version.toString().string()
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionParse() throws {
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        let versionStringDN = versionString.dotNETString()
        
        var version: System_Version?
        
        guard try System_Version.tryParse(versionStringDN,
                                          &version),
              let version else {
            XCTFail("System.Version.TryParse should not throw, return true and return an instance as out parameter")
            
            return
        }
        
        let majorRet = try version.major
        XCTAssertEqual(major, majorRet)
        
        let minorRet = try version.minor
        XCTAssertEqual(minor, minorRet)
        
        let buildRet = try version.build
        XCTAssertEqual(build, buildRet)
        
        let revisionRet = try version.revision
        XCTAssertEqual(revision, revisionRet)
        
        let versionStringRet = try version.toString().string()
        XCTAssertEqual(versionString, versionStringRet)
    }
}
