import XCTest
import BeyondDotNETSampleKit

final class SystemReflectionAssemblyTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testAssembly() {
        guard let assembly = try? System_Reflection_Assembly.getExecutingAssembly() else {
            XCTFail("System.Reflection.Assembly.GetExecutingAssembly should not throw and return an instance")
            
            return
        }
        
        guard let assemblyName = try? assembly.getName() else {
            XCTFail("System.Reflection.Assembly.GetName should not throw and return an instance")
            
            return
        }
        
        guard let assemblyNameString = try? assemblyName.name?.string() else {
            XCTFail("System.Reflection.AssemblyName.Name getter should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual("BeyondDotNETSampleKit", assemblyNameString)
    }
    
    func testLoadAssembly() {
        let data = Data([ 0, 1 ])
        
        guard let byteArray = try? data.dotNETByteArray() else {
            XCTFail("Failed to convert data to byte array")
            
            return
        }
        
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
