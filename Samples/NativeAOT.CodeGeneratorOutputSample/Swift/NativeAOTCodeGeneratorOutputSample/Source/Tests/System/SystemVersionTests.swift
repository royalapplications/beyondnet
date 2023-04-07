import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemVersionTests: XCTestCase {
	override class func setUp() {
		Self.gcCollect()
	}
	
	override class func tearDown() {
		Self.gcCollect()
	}
	
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
        
        defer { System_Type_Destroy(versionFromComponentsType) }
        
        guard System_Object_Equals(systemVersionType,
                                   versionFromComponentsType,
                                   &exception),
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
        
        guard let versionStringRet = String(dotNETString: System_Version_ToString(version,
																				  &exception),
											destroyDotNETString: true),
              exception == nil else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(versionString, versionStringRet)
    }
    
    func testSystemVersionFromString() {
        var exception: System_Exception_t?
        
        let major: Int32    = 123
        let minor: Int32    = 234
        let build: Int32    = 345
        let revision: Int32 = 456
        
        let versionString = "\(major).\(minor).\(build).\(revision)"
		let versionStringDN = versionString.dotNETString()
		defer { System_String_Destroy(versionStringDN) }
        
		guard let version = System_Version_Create_3(versionStringDN,
													&exception),
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
        
        guard let versionStringRet = String(dotNETString: System_Version_ToString(version,
																				  &exception),
											destroyDotNETString: true),
              exception == nil else {
            XCTFail("System.Version.ToString should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual(versionString, versionStringRet)
    }
	
	func testSystemVersionParse() {
		var exception: System_Exception_t?
		
		let major: Int32    = 123
		let minor: Int32    = 234
		let build: Int32    = 345
		let revision: Int32 = 456
		
		let versionString = "\(major).\(minor).\(build).\(revision)"
		let versionStringDN = versionString.dotNETString()
		defer { System_String_Destroy(versionStringDN) }
		
		var version: System_Version_t?
		
		let parseSuccess = System_Version_TryParse(versionStringDN,
												   &version,
												   &exception)
		
		guard parseSuccess,
			  let version,
			  exception == nil else {
			XCTFail("System.Version.TryParse should not throw, return true and return an instance as out parameter")
			
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
		
		guard let versionStringRet = String(dotNETString: System_Version_ToString(version,
																				  &exception),
											destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Version.ToString should not throw and return an instance of a C string")
			
			return
		}
		
		XCTAssertEqual(versionString, versionStringRet)
	}
}
