import Foundation

public class SystemObject {
    internal static func equals(_ lhs: System_Object_t,
                                _ rhs: System_Object_t) -> Bool {
        let result = System_Object_Equals(lhs, rhs) == .yes
        
        return result
    }
}
