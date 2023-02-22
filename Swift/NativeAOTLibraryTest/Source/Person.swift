import Foundation

public class Person {
    internal let handle: nativeaotlibrarytest_person_t
    
    public convenience init(firstName: String,
                            lastName: String,
                            age: Int32) {
        let firstNameC = firstName.cString(using: .utf8)
        let lastNameC = lastName.cString(using: .utf8)
        
        Debug.log("Will create Person")
        
        guard let handle = nativeaotlibrarytest_person_create(firstNameC,
                                                              lastNameC,
                                                              age) else {
            fatalError("Failed to create person")
        }
        
        self.init(handle: handle)
        
        Debug.log("Did create Person")
    }
    
    internal init(handle: nativeaotlibrarytest_person_t) {
        self.handle = handle
    }
    
    deinit {
        Debug.log("Will destroy Person")
        
        nativeaotlibrarytest_person_destroy(handle)
        
        Debug.log("Did destroy Person")
    }
    
    public var age: Int32 {
        get {
            Debug.log("Will get age of Person")
            
            let value = nativeaotlibrarytest_person_age_get(handle)
            
            Debug.log("Did get age of Person")
            
            return value
        }
        set {
            Debug.log("Will set age of Person")
            
            nativeaotlibrarytest_person_age_set(handle, newValue)
            
            Debug.log("Did set age of Person")
        }
    }
    
    public var firstName: String {
        get {
            Debug.log("Will get firstName of Person")
            
            guard let valueC = nativeaotlibrarytest_person_firstname_get(handle) else {
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
            nativeaotlibrarytest_person_firstname_set(handle, newValueC)
            
            Debug.log("Did set firstName of Person")
        }
    }
    
    public var lastName: String {
        get {
            Debug.log("Will get lastName of Person")
            
            guard let valueC = nativeaotlibrarytest_person_lastname_get(handle) else {
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
            nativeaotlibrarytest_person_lastname_set(handle, newValueC)
            
            Debug.log("Did set lastName of Person")
        }
    }
    
    public var fullName: String {
        Debug.log("Will get fullName of Person")
        
        guard let valueC = nativeaotlibrarytest_person_fullname_get(handle) else {
            fatalError("Failed to get fullName")
        }
        
        defer { valueC.deallocate() }
        
        Debug.log("Did get fullName of Person")
        
        let value = String(cString: valueC)
        
        return value
    }
}
