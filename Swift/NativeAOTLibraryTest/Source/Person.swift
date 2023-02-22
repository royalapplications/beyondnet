import Foundation

public class Person {
    private let personHandle: nativeaotlibrarytest_person_t
    
    public init(firstName: String,
                lastName: String,
                age: Int32) {
        let firstNameC = firstName.cString(using: .utf8)
        let lastNameC = lastName.cString(using: .utf8)
        
        personHandle = nativeaotlibrarytest_person_create(firstNameC,
                                                          lastNameC,
                                                          age)
    }
    
    deinit {
        nativeaotlibrarytest_person_destroy(personHandle)
    }
    
    public var age: Int32 {
        nativeaotlibrarytest_person_age_get(personHandle)
    }
    
    public var firstName: String {
        guard let cString = nativeaotlibrarytest_person_firstname_get(personHandle) else {
            fatalError("Failed to get firstName")
        }
        
        defer { cString.deallocate() }
        
        let string = String(cString: cString)
        
        return string
    }
    
    public var lastName: String {
        guard let cString = nativeaotlibrarytest_person_lastname_get(personHandle) else {
            fatalError("Failed to get lastName")
        }
        
        defer { cString.deallocate() }
        
        let string = String(cString: cString)
        
        return string
    }
    
    public var fullName: String {
        guard let cString = nativeaotlibrarytest_person_fullname_get(personHandle) else {
            fatalError("Failed to get fullName")
        }
        
        defer { cString.deallocate() }
        
        let string = String(cString: cString)
        
        return string
    }
}
