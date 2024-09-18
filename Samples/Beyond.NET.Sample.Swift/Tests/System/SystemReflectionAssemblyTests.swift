import XCTest
import BeyondDotNETSampleKit

final class SystemReflectionAssemblyTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testAssembly() throws {
        let assembly = try System_Reflection_Assembly.getExecutingAssembly()
        let assemblyName = try assembly.getName()
        let assemblyNameString = try assemblyName.name?.string()
        
        XCTAssertEqual("BeyondDotNETSampleKit", assemblyNameString)
    }
    
    func testLoadAssembly() throws {
        let data = Data([ 0, 1 ])
        let byteArray = try data.dotNETByteArray()
        
        do {
            _ = try System_Reflection_Assembly.load(byteArray)
            
            XCTFail("System.Reflection.Assembly.Load should throw but did not")
        } catch {
            guard let dnError = error as? DNError else {
                XCTFail("System.Reflection.Assembly.Load should throw an DNError but did not")
                
                return
            }
            
            let ex = dnError.exception
            
            guard ex.is(System_PlatformNotSupportedException.typeOf) else {
                XCTFail("System.Reflection.Assembly.Load should throw a System.PlatformNotSupportedException but did not")
                
                return
            }
        }
    }
}
