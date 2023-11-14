import Foundation

import BeyondDotNETSampleKit

extension Beyond.NET.Sample.ReadOnlyBytes {
    func data() throws -> Data {
        let count = Int(try self.length)
        
        var dataRet = Data(count: count)
        
        try dataRet.withUnsafeMutableBytes {
            guard let unsafeBytesPtr = $0.baseAddress else {
                throw DNSystemError.unexpectedNull
            }
            
            let unsafeBytesPointerAsInt = Int(bitPattern: unsafeBytesPtr)
            
            try copyTo(unsafeBytesPointerAsInt)
        }
        
        return dataRet
    }
    
    static func withData(_ data: Data) throws -> Beyond.NET.Sample.ReadOnlyBytes? {
        let dataCount = data.count
        var readOnlyBytes: Beyond.NET.Sample.ReadOnlyBytes?
        
        try data.withUnsafeBytes {
            guard let dataPtr = $0.baseAddress else {
                throw DNSystemError.unexpectedNull
            }
            
            let unsafeDataPointerAsInt = Int(bitPattern: dataPtr)
            
            readOnlyBytes = try Beyond.NET.Sample.ReadOnlyBytes(unsafeDataPointerAsInt,
                                                                .init(dataCount))
        }
        
        return readOnlyBytes
    }
}
