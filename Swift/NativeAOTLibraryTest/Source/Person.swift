import Foundation

public class Person {
    private let personHandle: nativeaotlibrarytest_person_t
    
    public init(firstName: String,
                lastName: String,
                age: Int32) {
        let firstNameC = firstName.cString(using: .utf8)
        let lastNameC = lastName.cString(using: .utf8)
        
        Self.logDebug("Will create Person")
        
        personHandle = nativeaotlibrarytest_person_create(firstNameC,
                                                          lastNameC,
                                                          age)
        
        Self.logDebug("Did create Person")
    }
    
    deinit {
        Self.logDebug("Will destroy Person")
        
        nativeaotlibrarytest_person_destroy(personHandle)
        
        Self.logDebug("Did destroy Person")
    }
    
    public var age: Int32 {
        get {
            Self.logDebug("Will get age of Person")
            
            let value = nativeaotlibrarytest_person_age_get(personHandle)
            
            Self.logDebug("Did get age of Person")
            
            return value
        }
        set {
            Self.logDebug("Will set age of Person")
            
            nativeaotlibrarytest_person_age_set(personHandle, newValue)
            
            Self.logDebug("Did set age of Person")
        }
    }
    
    public var firstName: String {
        get {
            Self.logDebug("Will get firstName of Person")
            
            guard let valueC = nativeaotlibrarytest_person_firstname_get(personHandle) else {
                fatalError("Failed to get firstName")
            }
            
            defer { valueC.deallocate() }
            
            Self.logDebug("Did get firstName of Person")
            
            let value = String(cString: valueC)
            
            return value
        }
        set {
            Self.logDebug("Will set firstName of Person")
            
            let newValueC = newValue.cString(using: .utf8)
            nativeaotlibrarytest_person_firstname_set(personHandle, newValueC)
            
            Self.logDebug("Did set firstName of Person")
        }
    }
    
    public var lastName: String {
        get {
            Self.logDebug("Will get lastName of Person")
            
            guard let valueC = nativeaotlibrarytest_person_lastname_get(personHandle) else {
                fatalError("Failed to get lastName")
            }
            
            defer { valueC.deallocate() }
            
            Self.logDebug("Did get lastName of Person")
            
            let value = String(cString: valueC)
            
            return value
        }
        set {
            Self.logDebug("Will set lastName of Person")
            
            let newValueC = newValue.cString(using: .utf8)
            nativeaotlibrarytest_person_lastname_set(personHandle, newValueC)
            
            Self.logDebug("Did set lastName of Person")
        }
    }
    
    public var fullName: String {
        Self.logDebug("Will get fullName of Person")
        
        guard let valueC = nativeaotlibrarytest_person_fullname_get(personHandle) else {
            fatalError("Failed to get fullName")
        }
        
        defer { valueC.deallocate() }
        
        Self.logDebug("Did get fullName of Person")
        
        let value = String(cString: valueC)
        
        return value
    }
}

private extension Person {
    static func logDebug(_ message: String) {
        #if DEBUG
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
        #endif
    }
}
