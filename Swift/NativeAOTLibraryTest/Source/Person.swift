import Foundation

public class Person {
    private let personHandle: nativeaotlibrarytest_person_t
    
    public init(age: Int32) {
        personHandle = nativeaotlibrarytest_person_create(age)
    }
    
    deinit {
        nativeaotlibrarytest_person_destroy(personHandle)
    }
    
    public var age: Int32 {
        get {
            nativeaotlibrarytest_person_age_get(personHandle)
        }
    }
}
