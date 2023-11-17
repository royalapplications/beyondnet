import XCTest
import BeyondDotNETSampleKit

final class SpanTestTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testReadOnlySpan() {
        let helloString = "Hello"
        
        guard let helloData = helloString.data(using: .utf8) else {
            XCTFail("Failed to get data of string")
            
            return
        }
        
        guard let helloByteArray = try? helloData.dotNETByteArray() else {
            XCTFail("Failed to convert Swift Data to .NET byte[]")
            
            return
        }
        
        guard let test = try? Beyond.NET.Sample.SpanTest(helloByteArray) else {
            XCTFail("SpanTest ctor should not throw and return an instance")
            
            return
        }
        
        guard let dataAsReadOnlySpan = try? test.dataAsReadOnlySpan else {
            XCTFail("SpanTest.DataAsReadOnlySpan should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(dataAsReadOnlySpan, helloData)
        
        guard let getDataAsReadOnlySpan = try? test.getDataAsReadOnlySpan() else {
            XCTFail("SpanTest.GetDataAsReadOnlySpan should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(getDataAsReadOnlySpan, helloData)
        
        var tryGetDataAsReadOnlySpan: Data?
        
        guard (try? test.tryGetDataAsReadOnlySpan(&tryGetDataAsReadOnlySpan)) ?? false else {
            XCTFail("SpanTest.TryGetDataAsReadOnlySpan should not throw and return an instance as an out parameter")
            
            return
        }
        
        XCTAssertEqual(tryGetDataAsReadOnlySpan, helloData)
    }
    
    func testReadOnlyBytes() {
        let helloString = "Hello"
        
        guard let helloData = helloString.data(using: .utf8) else {
            XCTFail("Failed to get data of string")
            
            return
        }
        
        guard let helloReadOnlyBytes = try? Beyond.NET.Sample.ReadOnlyBytes.withData(helloData) else {
            XCTFail("Failed to convert Swift Data to ReadOnlyBytes")
            
            return
        }
        
        guard let helloByteArray = try? helloData.dotNETByteArray() else {
            XCTFail("Failed to convert Swift Data to .NET byte[]")
            
            return
        }
        
//        guard let spanTest = try? Beyond.NET.Sample.SpanTest(helloByteArray) else {
//            XCTFail("SpanTest ctor should not throw and return an instance")
//            
//            return
//        }
        
        guard let spanTest = try? Beyond.NET.Sample.SpanTest(helloReadOnlyBytes) else {
            XCTFail("SpanTest ctor should not throw and return an instance")
            
            return
        }
        
        guard let helloByteArrayRet = try? spanTest.data else {
            XCTFail("SpanTest.Data should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(helloByteArray.elementsEqual(helloByteArrayRet))
        
        guard let helloDataRet = try? helloByteArrayRet.data() else {
            XCTFail("Failed to convert .NET byte[] to Swift Data")
            
            return
        }
        
        XCTAssertEqual(helloData, helloDataRet)
        
        guard let helloStringRet = String(data: helloDataRet, encoding: .utf8) else {
            XCTFail("Failed to convert Swift Data to Swift String")
            
            return
        }
        
        XCTAssertEqual(helloString, helloStringRet)
        
        guard let helloReadOnlyBytes = try? spanTest.dataAsReadOnlyBytes else {
            XCTFail("SpanTest.DataAsReadOnlyBytes should not throw and return an instance")
            
            return
        }
        
        guard let dataRet = try? helloReadOnlyBytes.data() else {
            XCTFail("ReadOnlyBytes.data() should not throw")
            
            return
        }
        
        XCTAssertEqual(helloData, dataRet)
    }
}
