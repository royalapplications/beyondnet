import Foundation

public class Person {
    internal let handle: NativeAOTLibraryTest_Person_t
    
    public convenience init(firstName: String,
                            lastName: String,
                            age: Int32) {
        let firstNameC = firstName.cString(using: .utf8)
        let lastNameC = lastName.cString(using: .utf8)
        
        Debug.log("Will create Person")
        
        guard let handle = NativeAOTLibraryTest_Person_Create(firstNameC,
                                                              lastNameC,
                                                              age) else {
            fatalError("Failed to create person")
        }
        
        self.init(handle: handle)
        
        Debug.log("Did create Person")
    }
    
    internal init(handle: NativeAOTLibraryTest_Person_t) {
        self.handle = handle
    }
    
    deinit {
        Debug.log("Will destroy Person")
        
        NativeAOTLibraryTest_Person_Destroy(handle)
        
        Debug.log("Did destroy Person")
    }
    
    public var age: Int32 {
        get {
            Debug.log("Will get age of Person")
            
            let value = NativeAOTLibraryTest_Person_Age_Get(handle)
            
            Debug.log("Did get age of Person")
            
            return value
        }
        set {
            Debug.log("Will set age of Person")
            
            NativeAOTLibraryTest_Person_Age_Set(handle, newValue)
            
            Debug.log("Did set age of Person")
        }
    }
    
    public var firstName: String {
        get {
            Debug.log("Will get firstName of Person")
            
            guard let valueC = NativeAOTLibraryTest_Person_FirstName_Get(handle) else {
                fatalError("Failed to get firstName")
            }
            
            defer { valueC.deallocate() }
            
            Debug.log("Did get firstName of Person")
            
            let value = String(cString: valueC)
            
            return value
        }
        set {
            Debug.log("Will set firstName of Person")
            
            let newValueC = newValue.cString(using: .utf8)
            NativeAOTLibraryTest_Person_FirstName_Set(handle, newValueC)
            
            Debug.log("Did set firstName of Person")
        }
    }
    
    public var lastName: String {
        get {
            Debug.log("Will get lastName of Person")
            
            guard let valueC = NativeAOTLibraryTest_Person_LastName_Get(handle) else {
                fatalError("Failed to get lastName")
            }
            
            defer { valueC.deallocate() }
            
            Debug.log("Did get lastName of Person")
            
            let value = String(cString: valueC)
            
            return value
        }
        set {
            Debug.log("Will set lastName of Person")
            
            let newValueC = newValue.cString(using: .utf8)
            NativeAOTLibraryTest_Person_LastName_Set(handle, newValueC)
            
            Debug.log("Did set lastName of Person")
        }
    }
    
    public var fullName: String {
        Debug.log("Will get fullName of Person")
        
        guard let valueC = NativeAOTLibraryTest_Person_FullName_Get(handle) else {
            fatalError("Failed to get fullName")
        }
        
        defer { valueC.deallocate() }
        
        Debug.log("Did get fullName of Person")
        
        let value = String(cString: valueC)
        
        return value
    }
}

extension Person: Equatable {
    public static func == (lhs: Person, rhs: Person) -> Bool {
        lhs.handle == rhs.handle
    }
}
