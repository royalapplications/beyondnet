import XCTest
import BeyondDotNETSampleKit

final class SystemTextStringBuilderTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testStringBuilder() throws {
        let hello = "Hello"
        let helloDN = hello.dotNETString()
        
        let lineBreak = "\n"
        
        let world = "World"
        let worldDN = world.dotNETString()
        
        let expectedFinalString = "\(hello)\(lineBreak)\(world)"
        
        let sb = try System_Text_StringBuilder(helloDN)
        let helloRet = try sb.toString().string()
        
        XCTAssertEqual(hello, helloRet)
        
        XCTAssertNoThrow(try sb.appendLine())
        XCTAssertNoThrow(try sb.append(worldDN))
        
        let finalStringRet = try sb.toString().string()
        XCTAssertEqual(expectedFinalString, finalStringRet)
    }
}
