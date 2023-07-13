import XCTest
import BeyondNETSampleSwift

final class SystemReflectionAssemblyTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    // TODO: Test fails on iOS Simulator: System.Reflection.Assembly.GetName
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
        
        XCTAssertEqual("libBeyondDotNETSampleNative", assemblyNameString)
    }
}
