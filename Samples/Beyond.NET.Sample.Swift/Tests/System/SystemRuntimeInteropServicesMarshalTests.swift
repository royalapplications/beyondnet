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
    
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSwiftDataToSystemByteArray() throws {
        let dataCount = Int32(correctnessTestsByteCount)
        
        guard let data = Data.randomData(count: .init(dataCount)) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        let systemByteArray = try DNArray<System_Byte>(length: dataCount)
        
        data.withUnsafeBytes {
            guard let unsafeBytesPointer = $0.baseAddress else {
                XCTFail("Failed to get unsafe bytes")
                
                return
            }
            
            do {
                try System.Runtime.InteropServices.Marshal.copy(.init(mutating: unsafeBytesPointer),
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
    
    func testSwiftDataToSystemByteArrayWithExtension() throws {
        let dataCount = correctnessTestsByteCount
        
        guard let data = Data.randomData(count: dataCount) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        let systemByteArray = try data.dotNETByteArray()
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testPerformanceOfSwiftDataToSystemByteArray() {
        let dataCount = performanceTestsByteCount
        
        guard let data = Data.randomData(count: dataCount) else {
            XCTFail("Failed to generate random data")
            
            return
        }
        
        var systemByteArray: DNArray<System_Byte>?
        
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
    
    func testEmptySwiftDataToSystemByteArrayWithExtension() throws {
        let data = Data()
        
        let systemByteArray = try data.dotNETByteArray()
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSystemByteArrayToSwiftData() throws {
        let bytesCount = correctnessTestsByteCount
        
        guard let systemByteArray = try randomSystemByteArray(count: bytesCount) else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        let systemByteArrayLength = try systemByteArray.length
        
        XCTAssertEqual(.init(systemByteArrayLength), bytesCount)
        
        var data = Data(count: .init(systemByteArrayLength))
        
        data.withUnsafeMutableBytes {
            guard let unsafeBytesPointer = $0.baseAddress else {
                XCTFail("Failed to get unsafe bytes")
                
                return
            }
            
            do {
                try System.Runtime.InteropServices.Marshal.copy(systemByteArray,
                                                                0,
                                                                unsafeBytesPointer,
                                                                systemByteArrayLength)
            } catch {
                XCTFail("System.Runtime.InteropServices.Marshal.Copy should not throw")
                
                return
            }
        }
        
        validateSystemByteArray(systemByteArray,
                                matchesData: data)
    }
    
    func testSystemByteArrayToSwiftDataWithExtension() throws {
        let bytesCount = correctnessTestsByteCount
        
        guard let systemByteArray = try randomSystemByteArray(count: bytesCount) else {
            XCTFail("System.Array should be possible to cast to byte[]")
            
            return
        }
        
        let copiedData = try systemByteArray.data()
        
        validateSystemByteArray(systemByteArray,
                                matchesData: copiedData)
        
        let pinnedData = try systemByteArray.data(noCopy: true)
        
        validateSystemByteArray(systemByteArray,
                                matchesData: pinnedData)
    }
    
    func testPerformanceOfSystemByteArrayToSwiftDataWithExtension() throws {
        let bytesCount = performanceTestsByteCount
        
        guard let systemByteArray = try randomSystemByteArray(count: bytesCount) else {
            XCTFail("Failed to create random byte[]")
            
            return
        }
        
        let systemByteArrayLength = try systemByteArray.length
        
        var copiedData: Data?
        
        measure {
            copiedData = try? systemByteArray.data()
        }
        
        guard let copiedData else {
            XCTFail("System_Byte_Array.data should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(copiedData.count, .init(systemByteArrayLength))
    }
    
    func testPerformanceOfSystemByteArrayToSwiftDataWithExtensionNoCopy() throws {
        let bytesCount = performanceTestsByteCount
        
        guard let systemByteArray = try randomSystemByteArray(count: bytesCount) else {
            XCTFail("Failed to create random byte[]")
            
            return
        }
        
        let systemByteArrayLength = try systemByteArray.length
        
        var pinnedData: Data?
        
        measure {
            pinnedData = try? systemByteArray.data(noCopy: true)
        }
        
        guard let pinnedData else {
            XCTFail("System_Byte_Array.data should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(pinnedData.count, .init(systemByteArrayLength))
    }
    
    func testEmptySystemByteArrayToSwiftDataWithExtension() throws {
        let systemByteArray = try DNArray<System_Byte>(length: 0)
        
        let copiedData = try systemByteArray.data()
        
        validateSystemByteArray(systemByteArray,
                                matchesData: copiedData)
        
        let pinnedData = try systemByteArray.data(noCopy: true)
        
        validateSystemByteArray(systemByteArray,
                                matchesData: pinnedData)
    }
}

private extension SystemRuntimeInteropServicesMarshalTests {
    func validateSystemByteArray(_ systemByteArray: DNArray<System_Byte>,
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
            
            let systemByteObject = systemByteArray[idxAsInt32]
            
            guard let systemByte = try? systemByteObject.value else {
                XCTFail("Should get a byte/UInt8 but failed to cast")
                
                return
            }
            
            XCTAssertEqual(systemByte, dataByte)
        }
    }
    
    func randomSystemByteArray(count: Int) throws -> DNArray<System_Byte>? {
        guard let random = try? System.Random() else {
            XCTFail("System.Random ctor should not throw and return an instance")
            
            return nil
        }
        
        let systemByteArray = try DNArray<System_Byte>(length: .init(count))
        
        do {
            try random.nextBytes(systemByteArray)
        } catch {
            XCTFail("System.Random.NextBytes should not throw")
            
            return nil
        }
        
        return systemByteArray
    }
}
