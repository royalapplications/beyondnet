import XCTest

import BeyondNETSampleSwift
import BeyondDotNETSampleNative

final class SystemTextStringBuilderTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testStringBuilder() {
        let hello = "Hello"
        let helloDN = hello.dotNETString()
        
        let lineBreak = "\n"
        
        let world = "World"
        let worldDN = world.dotNETString()
        
        let expectedFinalString = "\(hello)\(lineBreak)\(world)"
        
        guard let sb = try? System_Text_StringBuilder(helloDN) else {
            XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
            
            return
        }
        
        guard let helloRet = try? sb.toString()?.string() else {
            XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual(hello, helloRet)
        
        XCTAssertNoThrow(try sb.appendLine())
        XCTAssertNoThrow(try sb.append(worldDN))
        
        guard let finalStringRet = try? sb.toString()?.string() else {
            XCTFail("System.Text.StringBuilder.ToString should not throw and return a string")
            
            return
        }
        
        XCTAssertEqual(expectedFinalString, finalStringRet)
    }
}
