import XCTest
import BeyondDotNETSampleKit

final class SpanTestTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testReadOnlySpan() throws {
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
        
        let test = try Beyond.NET.Sample.SpanTest(helloData)
        
        verifySpanTest(test, matchesData: helloData)
        
        XCTAssertNoThrow(try test.setDataAsReadOnlySpan(goodbyeData))
        verifySpanTest(test, matchesData: goodbyeData)
        
        XCTAssertNoThrow(try test.dataAsReadOnlySpan_set(hugoData))
        verifySpanTest(test, matchesData: hugoData)
        
        let hugoByteArray = try test.data
        
        guard let hugoConvertedData = try test.convertByteArrayToSpan(hugoByteArray, .init({ bytes in
            try? bytes.data()
        })) else {
            XCTFail("SpanTest.ConvertByteArrayToSpan should not throw and return an instance")
            
            return
        }
        
        verifySpanTest(test, matchesData: hugoConvertedData)
        
        guard let hugoConvertedByteArray = try? test.convertSpanToByteArray(hugoConvertedData, .init({ span in
            try? span?.dotNETByteArray().nullable()
        })) else {
            XCTFail("SpanTest.ConvertSpanToByteArray should not throw and return an instance")
            
            return
        }
        
        let hugoConvertedByteArrayBackToData = try hugoConvertedByteArray.data()
        
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
