import XCTest
import BeyondDotNETSampleNative

final class SystemVersionTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemVersionFromComponents() {
        let systemVersionType = System_Version.typeOf
        
        let major: Int32    = 1
        let minor: Int32    = 2
        let build: Int32    = 3
        let revision: Int32 = 4
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        
        guard let version = try? System_Version(major,
                                                minor,
                                                build,
                                                revision) else {
            XCTFail("System.Version ctor should not throw and return an instance")
            
            return
        }
        
        guard let versionFromComponentsType = try? version.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard systemVersionType == versionFromComponentsType else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        do {
            let majorRet = try version.major
            XCTAssertEqual(major, majorRet)
            
            let minorRet = try version.minor
            XCTAssertEqual(minor, minorRet)
            
            let buildRet = try version.build
            XCTAssertEqual(build, buildRet)
            
            let revisionRet = try version.revision
            XCTAssertEqual(revision, revisionRet)
        } catch {
            XCTFail("None of the System.Version accessors should throw")
            
            return
        }
        
        guard let versionStringRet = (try? version.toString())?.string() else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionFromString() {
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        let versionStringDN = versionString.dotNETString()
        
        guard let version = try? System_Version(versionStringDN) else {
            XCTFail("System.Version ctor should not throw and return an instance")
            
            return
        }
        
        do {
            let majorRet = try version.major
            XCTAssertEqual(major, majorRet)
            
            let minorRet = try version.minor
            XCTAssertEqual(minor, minorRet)
            
            let buildRet = try version.build
            XCTAssertEqual(build, buildRet)
            
            let revisionRet = try version.revision
            XCTAssertEqual(revision, revisionRet)
        } catch {
            XCTFail("None of the System.Version accessors should throw")
            
            return
        }
        
        guard let versionStringRet = (try? version.toString())?.string() else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionParse() {
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        let versionStringDN = versionString.dotNETString()
        
        var version: System_Version?
        
        let parseSuccess = (try? System_Version.tryParse(versionStringDN,
                                                         &version)) ?? false
        
        guard parseSuccess,
              let version else {
            XCTFail("System.Version.TryParse should not throw, return true and return an instance as out parameter")
            
            return
        }
        
        do {
            let majorRet = try version.major
            XCTAssertEqual(major, majorRet)
            
            let minorRet = try version.minor
            XCTAssertEqual(minor, minorRet)
            
            let buildRet = try version.build
            XCTAssertEqual(build, buildRet)
            
            let revisionRet = try version.revision
            XCTAssertEqual(revision, revisionRet)
        } catch {
            XCTFail("None of the System.Version accessors should throw")
            
            return
        }
        
        guard let versionStringRet = (try? version.toString())?.string() else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(versionString, versionStringRet)
    }
}
