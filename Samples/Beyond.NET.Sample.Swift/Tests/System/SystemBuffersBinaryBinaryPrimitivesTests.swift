import XCTest
import BeyondDotNETSampleKit

final class SystemBuffersBinaryBinaryPrimitivesTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testReadInt64() throws {
        let value = Int64.max
        let valueData = value.data
        
        let valueRet = try System.Buffers.Binary.BinaryPrimitives.readInt64LittleEndian(valueData)
        XCTAssertEqual(value, valueRet)
    }
    
    func testTryReadUInt32() throws {
        let value = UInt32.max
        let valueData = value.data
        
        var valueRet: UInt32 = .min
        
        guard try System.Buffers.Binary.BinaryPrimitives.tryReadUInt32LittleEndian(valueData, &valueRet) else {
            XCTFail("System.Buffers.Binary.BinaryPrimitives.TryReadUInt32LittleEndian should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(value, valueRet)
    }
}


fileprivate extension Int64 {
    var data: Data {
        var value = self
        return Data(bytes: &value, count: MemoryLayout<Int>.size)
    }
}

fileprivate extension UInt32 {
    var data: Data {
        var value = self
        return Data(bytes: &value, count: MemoryLayout<UInt32>.size)
    }
}
