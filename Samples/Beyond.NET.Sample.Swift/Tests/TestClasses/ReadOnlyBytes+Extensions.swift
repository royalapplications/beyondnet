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
}
