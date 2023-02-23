import Foundation

public class SystemObject {
    public static func equals(_ lhs: System_Object_t,
                              _ rhs: System_Object_t) -> Bool {
        let result = System_Object_Equals(lhs, rhs)
        let boolResult = result == .yes
        
        return boolResult
    }
}
