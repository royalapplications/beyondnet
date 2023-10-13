import XCTest
import BeyondDotNETSampleKit

final class SystemRuntimeInteropServicesMarshalTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSwiftDataToSystemByteArray() {
        let bytes: [UInt8] = [ 0, 1, 2, 3 ]
        let bytesCount = Int32(bytes.count)
        let data = Data(bytes)
        
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, bytesCount) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        guard let systemByteArray: System_Byte_Array = systemArray.castAs() else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        data.withUnsafeBytes {
            guard let unsafeBytesPointer = $0.baseAddress else {
                XCTFail("Failed to get unsafe bytes")
                
                return
            }
            
            let unsafeBytesPointerAsInt = Int(bitPattern: unsafeBytesPointer)
            
            do {
                try System.Runtime.InteropServices.Marshal.copy(unsafeBytesPointerAsInt,
                                                                systemByteArray,
                                                                0,
                                                                bytesCount)
            } catch {
                XCTFail("System.Runtime.InteropServices.Marshal.Copy should not throw")
                
                return
            }
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSwiftDataToSystemByteArrayWithExtension() {
        let bytes: [UInt8] = [ 0, 1, 2, 3 ]
        let data = Data(bytes)
        
        guard let systemByteArray = try? data.dotNETByteArray() else {
            XCTFail("Swift Data.dotNETByteArray should not throw and return an instance")
            
            return
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSystemByteArrayToSwiftData() {
        let bytes: [UInt8] = [ 0, 1, 2, 3 ]
        let bytesCount = Int32(bytes.count)
        
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, bytesCount) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        for (idx, byte) in bytes.enumerated() {
            let byteObject = byte.dotNETObject()
            
            do {
                try systemArray.setValue(byteObject, Int32(idx))
            } catch {
                XCTFail("System.Array.SetValue should not throw")
                
                return
            }
        }
        
        guard let systemByteArray: System_Byte_Array = systemArray.castAs() else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        guard let systemByteArrayLength = try? systemByteArray.length else {
            XCTFail("System.Array.Length should not throw and return an integer")
            
            return
        }
        
        XCTAssertEqual(systemByteArrayLength, bytesCount)
        
        var data = Data(count: .init(systemByteArrayLength))
        
        data.withUnsafeMutableBytes {
            guard let unsafeBytesPointer = $0.baseAddress else {
                XCTFail("Failed to get unsafe bytes")
                
                return
            }
            
            let unsafeBytesPointerAsInt = Int(bitPattern: unsafeBytesPointer)
            
            do {
                try System.Runtime.InteropServices.Marshal.copy(systemByteArray,
                                                                0,
                                                                unsafeBytesPointerAsInt,
                                                                systemByteArrayLength)
            } catch {
                XCTFail("System.Runtime.InteropServices.Marshal.Copy should not throw")
                
                return
            }
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSystemByteArrayToSwiftDataWithExtension() {
        let bytes: [UInt8] = [ 0, 1, 2, 3 ]
        let bytesCount = Int32(bytes.count)
        
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, bytesCount) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        for (idx, byte) in bytes.enumerated() {
            let byteObject = byte.dotNETObject()
            
            do {
                try systemArray.setValue(byteObject, Int32(idx))
            } catch {
                XCTFail("System.Array.SetValue should not throw")
                
                return
            }
        }
        
        guard let systemByteArray: System_Byte_Array = systemArray.castAs() else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        guard let data = try? systemByteArray.data() else {
            XCTFail("System_Byte_Array.data should not throw and return an instance")
            
            return
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
}

private extension SystemRuntimeInteropServicesMarshalTests {
    func validateSystemByteArray(_ systemByteArray: System_Byte_Array,
                                 matchesData data: Data) {
        guard let systemByteArrayLength = try? systemByteArray.length else {
            XCTFail("System.Array.Length should not throw and return an integer")
            
            return
        }
        
        let bytesCount = Int32(data.count)
        
        XCTAssertEqual(systemByteArrayLength, bytesCount)
        
        for idx in 0..<systemByteArrayLength {
            guard let byteObject = try? systemByteArray.getValue(idx) else {
                XCTFail("System.Array.GetValue should not throw and return an instance")
                
                return
            }
            
            guard let byte = try? byteObject.castToUInt8() else {
                XCTFail("Should get a byte/UInt8 but failed to cast")
                
                return
            }
            
            let expectedByte = data[.init(idx)]
            
            XCTAssertEqual(byte, expectedByte)
        }
    }
}
