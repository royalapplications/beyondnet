import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemReflectionAssemblyTests_Swift: XCTestCase {
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
        
        guard let assemblyNameString = try? assemblyName.name_get()?.string() else {
            XCTFail("System.Reflection.AssemblyName.Name getter should not throw and return an instance of a C string")
            
            return
        }
        
        XCTAssertEqual("NativeAOT.CodeGeneratorOutputSample", assemblyNameString)
    }
}