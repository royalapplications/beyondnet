import XCTest
import BeyondDotNETSampleKit

final class SystemRuntimeInteropServicesMarshalTests: XCTestCase {
    struct Bytes {
        static let kb1      =   1 * 1024
        static let kb10     =  10 * 1024
        static let kb50     =  50 * 1024
        static let kb100    = 100 * 1024
        static let kb200    = 200 * 1024
        static let kb250    = 250 * 1024
        static let kb500    = 500 * 1024
        
        static let mb1      =   1 * 1024 * 1024
        static let mb10     =  10 * 1024 * 1024
        static let mb50     =  50 * 1024 * 1024
        static let mb100    = 100 * 1024 * 1024
        static let mb200    = 200 * 1024 * 1024
        static let mb250    = 250 * 1024 * 1024
        static let mb500    = 500 * 1024 * 1024
        
        static let gb1      = 1 * 1024 * 1024 * 1024
    }
    
    private let correctnessTestsByteCount = Bytes.kb1
    
    // NOTE: Since the .NET runtime likes to raise SIGUSR1 (which makes lldb halt execution) when using higher numbers here, we set it to a relatively number.
    // Feel free to increase the byte count but don't forget to tell lldb to ignore SIGUSR1 before running the tests (`process handle SIGUSR1 -n true -p true -s false`).
    private let performanceTestsByteCount = Bytes.kb500
    
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSwiftDataToSystemByteArray() {
        let dataCount = Int32(correctnessTestsByteCount)
        
        guard let data = Data.randomData(count: .init(dataCount)) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, dataCount) else {
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
                                                                dataCount)
            } catch {
                XCTFail("System.Runtime.InteropServices.Marshal.Copy should not throw")
                
                return
            }
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSwiftDataToSystemByteArrayWithExtension() {
        let dataCount = correctnessTestsByteCount
        
        guard let data = Data.randomData(count: dataCount) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        guard let systemByteArray = try? data.dotNETByteArray() else {
            XCTFail("Swift Data.dotNETByteArray should not throw and return an instance")
            
            return
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testPerformanceOfSwiftDataToSystemByteArray() {
        let dataCount = performanceTestsByteCount
        
        guard let data = Data.randomData(count: dataCount) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        var systemByteArray: System_Byte_Array?
        
        measure {
            systemByteArray = try? data.dotNETByteArray()
        }
        
        guard let systemByteArray else {
            XCTFail("Swift Data.dotNETByteArray should not throw and return an instance")
            
            return
        }
        
        guard let systemByteArrayLength = try? systemByteArray.length else {
            XCTFail("byte[].Length should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(.init(systemByteArrayLength), dataCount)
    }
    
    func testEmptySwiftDataToSystemByteArrayWithExtension() {
        let data = Data()
        
        guard let systemByteArray = try? data.dotNETByteArray() else {
            XCTFail("Swift Data.dotNETByteArray should not throw and return an instance")
            
            return
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSystemByteArrayToSwiftData() {
        let bytesCount = correctnessTestsByteCount
        
        guard let systemByteArray = randomSystemByteArray(count: bytesCount) else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        guard let systemByteArrayLength = try? systemByteArray.length else {
            XCTFail("System.Array.Length should not throw and return an integer")
            
            return
        }
        
        XCTAssertEqual(.init(systemByteArrayLength), bytesCount)
        
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
        let bytesCount = correctnessTestsByteCount
        
        guard let systemByteArray = randomSystemByteArray(count: bytesCount) else {
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
    
    func testPerformanceOfSystemByteArrayToSwiftDataWithExtension() {
        let bytesCount = performanceTestsByteCount
        
        guard let systemByteArray = randomSystemByteArray(count: bytesCount) else {
            XCTFail("Failed to create random byte[]")
            
            return
        }
        
        guard let systemByteArrayLength = try? systemByteArray.length else {
            XCTFail("System.Array.Length should not throw and return an integer")
            
            return
        }
        
        var data: Data?
        
        measure {
            data = try? systemByteArray.data()
        }
        
        guard let data else {
            XCTFail("System_Byte_Array.data should not throw and return an instance")
            
            return
        }
        
        let dataCount = data.count
        
        XCTAssertEqual(dataCount, .init(systemByteArrayLength))
    }
    
    func testEmptySystemByteArrayToSwiftDataWithExtension() {
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, 0) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
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
        
        let dataCount = data.count
        
        XCTAssertEqual(Int(systemByteArrayLength), dataCount)
        
        for idx in 0..<dataCount {
            guard let idxAsInt32 = Int32(exactly: idx) else {
                XCTFail("Index doesn't fit in Int32")
                
                return
            }
            
            let dataByte = data[idx]
            
            guard let systemByteObject = try? systemByteArray.getValue(idxAsInt32) else {
                XCTFail("System.Array.GetValue should not throw and return an instance")
                
                return
            }
            
            guard let systemByte = try? systemByteObject.castToUInt8() else {
                XCTFail("Should get a byte/UInt8 but failed to cast")
                
                return
            }
            
            XCTAssertEqual(systemByte, dataByte)
        }
    }
    
    func randomSystemByteArray(count: Int) -> System_Byte_Array? {
        guard let random = try? System.Random() else {
            XCTFail("System.Random ctor should not throw and return an instance")
            
            return nil
        }
        
        guard let systemArray = try? System.Array.createInstance(System.Byte.typeOf, .init(count)) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return nil
        }
        
        guard let systemByteArray: System_Byte_Array = systemArray.castAs() else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return nil
        }
        
        do {
            try random.nextBytes(systemByteArray)
        } catch {
            XCTFail("System.Random.NextBytes should not throw")
            
            return nil
        }
        
        return systemByteArray
    }
}
