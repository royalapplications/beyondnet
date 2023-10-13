import Foundation

extension Data {
    static func randomData(count: Int) -> Data? {
        var bytes = [Int8](repeating: 0,
                           count: count)

        // Fill bytes with secure random data
        let status = SecRandomCopyBytes(kSecRandomDefault,
                                        count,
                                        &bytes)
        
        // A status of errSecSuccess indicates success
        guard status == errSecSuccess else {
            return nil
        }
        
        // Convert bytes to Data
        let data = Data(bytes: bytes, 
                        count: count)
        
        return data
    }
}
