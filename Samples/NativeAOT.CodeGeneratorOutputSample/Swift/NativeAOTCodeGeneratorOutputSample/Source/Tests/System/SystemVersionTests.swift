import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemVersionTests: XCTestCase {
    func testSystemVersionFromComponents() {
        var exception: System_Exception_t?
        
        guard let systemVersionType = System_Version_TypeOf() else {
            XCTFail("typeof(System.Version should return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(systemVersionType) }
        
        let major: Int32    = 1
        let minor: Int32    = 2
        let build: Int32    = 3
        let revision: Int32 = 4
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        
        guard let version = System_Version_Create(major,
                                                  minor,
                                                  build,
                                                  revision,
                                                  &exception),
              exception == nil else {
            XCTFail("System.Version ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Version_Destroy(version) }
        
        guard let versionFromComponentsType = System_Object_GetType(version,
                                                                    &exception),
              exception == nil else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        defer { System_Type_Destroy(version) }
        
        guard System_Object_Equals(systemVersionType,
                                   versionFromComponentsType,
                                   &exception) == .yes,
              exception == nil else {
            XCTFail("System.Object.Equals should not throw and return true")
            
            return
        }
        
        let majorRet = System_Version_Major_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(major, majorRet)
        
        let minorRet = System_Version_Minor_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(minor, minorRet)
        
        let buildRet = System_Version_Build_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(build, buildRet)
        
        let revisionRet = System_Version_Revision_Get(version,
                                                      &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(revision, revisionRet)
        
        guard let versionStringRetC = System_Version_ToString(version,
                                                              &exception),
              exception == nil else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        defer { versionStringRetC.deallocate() }
        
        let versionStringRet = String(cString: versionStringRetC)
        
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionFromString() {
        var exception: System_Exception_t?
        
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
        
        var version: System_Version_t?
        
        versionString.withCString { versionStringC in
            version = System_Version_Create3(versionString,
                                             &exception)
        }
        
        guard let version,
              exception == nil else {
            XCTFail("System.Version ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_Version_Destroy(version) }
        
        let majorRet = System_Version_Major_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(major, majorRet)
        
        let minorRet = System_Version_Minor_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(minor, minorRet)
        
        let buildRet = System_Version_Build_Get(version,
                                                &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(build, buildRet)
        
        let revisionRet = System_Version_Revision_Get(version,
                                                      &exception)
        
        XCTAssertNil(exception)
        XCTAssertEqual(revision, revisionRet)
        
        guard let versionStringRetC = System_Version_ToString(version,
                                                              &exception),
              exception == nil else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        defer { versionStringRetC.deallocate() }
        
        let versionStringRet = String(cString: versionStringRetC)
        
        XCTAssertEqual(versionString, versionStringRet)
    }
}
