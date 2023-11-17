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
        let goodbyeString = "Goodbye"
        let hugoString = "Hugo"
        
        guard let helloData = helloString.data(using: .utf8) else {
            XCTFail("Failed to get data of string")
            
            return
        }
        
        guard let goodbyeData = goodbyeString.data(using: .utf8) else {
            XCTFail("Failed to get data of string")
            
            return
        }
        
        guard let hugoData = hugoString.data(using: .utf8) else {
            XCTFail("Failed to get data of string")
            
            return
        }
        
        guard let test = try? Beyond.NET.Sample.SpanTest(helloData) else {
            XCTFail("SpanTest ctor should not throw and return an instance")
            
            return
        }
        
        verifySpanTest(test, matchesData: helloData)
        
        XCTAssertNoThrow(try test.setDataAsReadOnlySpan(goodbyeData))
        verifySpanTest(test, matchesData: goodbyeData)
        
        XCTAssertNoThrow(try test.dataAsReadOnlySpan_set(hugoData))
        verifySpanTest(test, matchesData: hugoData)
        
        guard let hugoByteArray = try? test.data else {
            XCTFail("SpanTest.Data should not throw and return an instance")
            
            return
        }
        
        guard let hugoConvertedData = try? test.convertByteArrayToSpan(hugoByteArray, .init({ bytes in
            try? bytes?.data()
        })) else {
            XCTFail("SpanTest.ConvertByteArrayToSpan should not throw and return an instance")
            
            return
        }
        
        verifySpanTest(test, matchesData: hugoConvertedData)
        
        guard let hugoConvertedByteArray = try? test.convertSpanToByteArray(hugoConvertedData, .init({ span in
            try? span?.dotNETByteArray()
        })) else {
            XCTFail("SpanTest.ConvertSpanToByteArray should not throw and return an instance")
            
            return
        }
        
        guard let hugoConvertedByteArrayBackToData = try? hugoConvertedByteArray.data() else {
            XCTFail("Failed to convert byte[] to Swift Data")
            
            return
        }
        
        verifySpanTest(test, matchesData: hugoConvertedByteArrayBackToData)
        XCTAssertTrue(hugoByteArray.elementsEqual(hugoConvertedByteArray))
    }
    
    func verifySpanTest(_ test: Beyond.NET.Sample.SpanTest,
                        matchesData expectedData: Data) {
        guard let dataAsReadOnlySpan = try? test.dataAsReadOnlySpan else {
            XCTFail("SpanTest.DataAsReadOnlySpan should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(dataAsReadOnlySpan, expectedData)
        
        guard let getDataAsReadOnlySpan = try? test.getDataAsReadOnlySpan() else {
            XCTFail("SpanTest.GetDataAsReadOnlySpan should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(getDataAsReadOnlySpan, expectedData)
        
        var tryGetDataAsReadOnlySpan: Data?
        
        guard (try? test.tryGetDataAsReadOnlySpan(&tryGetDataAsReadOnlySpan)) ?? false else {
            XCTFail("SpanTest.TryGetDataAsReadOnlySpan should not throw and return an instance as an out parameter")
            
            return
        }
        
        XCTAssertEqual(tryGetDataAsReadOnlySpan, expectedData)
    }
}
