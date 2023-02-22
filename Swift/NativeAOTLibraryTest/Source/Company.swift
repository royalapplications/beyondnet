import Foundation

public class Company {
    internal let handle: nativeaotlibrarytest_person_t
    
    public init(name: String) {
        let nameC = name.cString(using: .utf8)

        Self.logDebug("Will create Company")

        handle = nativeaotlibrarytest_company_create(nameC)

        Self.logDebug("Did create Company")
    }
    
    deinit {
        Self.logDebug("Will destroy Company")

        nativeaotlibrarytest_company_destroy(handle)

        Self.logDebug("Did destroy Company")
    }
    
    public var name: String {
        Self.logDebug("Will get name of Company")

        guard let valueC = nativeaotlibrarytest_company_name_get(handle) else {
            fatalError("Failed to get name of Company")
        }

        defer { valueC.deallocate() }

        Self.logDebug("Did get name of Company")

        let value = String(cString: valueC)

        return value
    }
    
    public var numberOfEmployees: Int32 {
        Self.logDebug("Will get numberOfEmployees of Company")
        
        let value = nativeaotlibrarytest_company_numberofemployees_get(handle)
        
        Self.logDebug("Did get numberOfEmployees of Company")
        
        return value
    }
    
    @discardableResult
    public func addEmployee(_ employee: Person) -> Bool {
        Self.logDebug("Will add employee to Company")
        
        let result = nativeaotlibrarytest_company_addemployee(handle, employee.handle)
        let success = result == STATUS_SUCCESS
        
        Self.logDebug("Did add employee to Company")
        
        return success
    }
    
    @discardableResult
    public func removeEmployee(_ employee: Person) -> Bool {
        Self.logDebug("Will remove employee from Company")
        
        let result = nativeaotlibrarytest_company_removeemployee(handle, employee.handle)
        let success = result == STATUS_SUCCESS
        
        Self.logDebug("Did remove employee from Company")
        
        return success
    }
    
    public func containsEmployee(_ employee: Person) -> Bool {
        Self.logDebug("Will check if Company contains employee")
        
        let boolResult = nativeaotlibrarytest_company_containsemployee(handle, employee.handle)
        let value = boolResult == BOOL_TRUE
        
        Self.logDebug("Did check if Company contains employee")
        
        return value
    }
}

private extension Company {
    static func logDebug(_ message: String) {
        #if DEBUG
        let fullMessage = "[DEBUG] \(message)"
        print(fullMessage)
        #endif
    }
}
