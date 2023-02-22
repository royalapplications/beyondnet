import Foundation

public class Company {
    internal let handle: nativeaotlibrarytest_person_t
    
    public convenience init(name: String) {
        let nameC = name.cString(using: .utf8)

        Debug.log("Will create Company")

        guard let handle = nativeaotlibrarytest_company_create(nameC) else {
            fatalError("Failed to create Company")
        }
        
        self.init(handle: handle)

        Debug.log("Did create Company")
    }
    
    internal init(handle: nativeaotlibrarytest_person_t) {
        self.handle = handle
    }
    
    deinit {
        Debug.log("Will destroy Company")

        nativeaotlibrarytest_company_destroy(handle)

        Debug.log("Did destroy Company")
    }
    
    public var name: String {
        Debug.log("Will get name of Company")

        guard let valueC = nativeaotlibrarytest_company_name_get(handle) else {
            fatalError("Failed to get name of Company")
        }

        defer { valueC.deallocate() }

        Debug.log("Did get name of Company")

        let value = String(cString: valueC)

        return value
    }
    
    public var numberOfEmployees: Int32 {
        Debug.log("Will get numberOfEmployees of Company")
        
        let value = nativeaotlibrarytest_company_numberofemployees_get(handle)
        
        Debug.log("Did get numberOfEmployees of Company")
        
        return value
    }
    
    @discardableResult
    public func addEmployee(_ employee: Person) -> Bool {
        Debug.log("Will add employee to Company")
        
        let result = nativeaotlibrarytest_company_addemployee(handle, employee.handle)
        let success = result == .success
        
        Debug.log("Did add employee to Company")
        
        return success
    }
    
    @discardableResult
    public func removeEmployee(_ employee: Person) -> Bool {
        Debug.log("Will remove employee from Company")
        
        let result = nativeaotlibrarytest_company_removeemployee(handle, employee.handle)
        let success = result == .success
        
        Debug.log("Did remove employee from Company")
        
        return success
    }
    
    public func containsEmployee(_ employee: Person) -> Bool {
        Debug.log("Will check if Company contains employee")
        
        let boolResult = nativeaotlibrarytest_company_containsemployee(handle, employee.handle)
        let value = boolResult == .yes
        
        Debug.log("Did check if Company contains employee")
        
        return value
    }
    
    public func employee(at index: Int32) -> Person? {
        Debug.log("Will get employee at index of Company")
        
        let employee: Person?
        
        if let employeeHandle = nativeaotlibrarytest_company_getemployeeatindex(handle, index) {
            employee = .init(handle: employeeHandle)
        } else {
            employee = nil
        }
        
        Debug.log("Did get employee at index of Company")
        
        return employee
    }
}
