import Foundation

public class TestClass {
    private let handle: NativeAOT_CodeGeneratorInputSample_TestClass_t
    
    public enum Errors: Error {
        case unknown
    }
    
    public convenience init() throws {
        var exception: System_Exception_t?
        
        guard let handle = NativeAOT_CodeGeneratorInputSample_TestClass_Create(&exception) else {
            throw Errors.unknown
        }
        
        guard exception == nil else {
            throw Errors.unknown
        }
        
        self.init(handle: handle)
    }
    
    init(handle: NativeAOT_CodeGeneratorInputSample_TestClass_t) {
        self.handle = handle
    }
    
    deinit {
        NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(handle)
    }
    
    public func sayHello() throws {
        var exception: System_Exception_t?
        
        NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(handle,
                                                              &exception)
        
        guard exception == nil else {
            throw Errors.unknown
        }
    }
    
    public func sayHello(name: String) throws {
        var exception: System_Exception_t?
        
        name.withCString { nameC in
            NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(handle,
                                                                   nameC,
                                                                   &exception)
        }
        
        guard exception == nil else {
            throw Errors.unknown
        }
    }
    
    public func getHello() throws -> String? {
        var exception: System_Exception_t?
        
        let cString = NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(handle, &exception)
        
        defer {
            cString?.deallocate()
        }
        
        guard exception == nil else {
            throw Errors.unknown
        }
        
        guard let cString else {
            return nil
        }
        
        let string = String(cString: cString)
        
        return string
    }
    
    public func add(number1: Int32, number2: Int32) throws -> Int32 {
        var exception: System_Exception_t?
        
        let result = NativeAOT_CodeGeneratorInputSample_TestClass_Add(handle,
                                                                      number1,
                                                                      number2,
                                                                      &exception)
        
        guard exception == nil else {
            throw Errors.unknown
        }
        
        return result
    }
}
