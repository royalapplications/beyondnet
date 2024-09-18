import XCTest
import BeyondDotNETSampleKit

final class SystemConvertTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testBooleanConversion() throws {
        let trueStringDN = "true".dotNETString()
        let falseStringDN = "false".dotNETString()
        let nonsenseStringDN = "nonsense".dotNETString()
        
        let result1 = try System.Convert.toBoolean(trueStringDN)
        XCTAssertTrue(result1)
        
        let result2 = try System.Convert.toBoolean(falseStringDN)
        XCTAssertFalse(result2)
        
        do {
            let _ = try System_Convert.toBoolean(nonsenseStringDN)
            
            XCTFail("System.Convert.ToBoolean should throw")
        } catch { }
    }
    
    func testIntegerConversion() throws {
        let number1: Int32 = 123456789
        let number1StringDN = "\(number1)".dotNETString()
        
        XCTAssertEqual(number1,
                       try System.Convert.toInt32(number1StringDN))
        
        let number2: Int64 = -123456789
        let number2StringDN = "\(number2)".dotNETString()
        
        XCTAssertEqual(number2,
                       try System.Convert.toInt64(number2StringDN))
        
        let number3StringDN = "nonsense".dotNETString()
        XCTAssertThrowsError(try System.Convert.toInt64(number3StringDN))
        
        let number4StringDN = "nonsense".dotNETString()
        XCTAssertThrowsError(try System.Convert.toUInt64(number4StringDN))
    }
    
    func testBase64Conversion() throws {
        let text = "Hello World!"
        let textDN = text.dotNETString()
        
        let utf8Encoding = try System_Text_Encoding.uTF8
        let textBytes = try utf8Encoding.getBytes(textDN)
        let textAsBase64StringDN = try System_Convert.toBase64String(textBytes)
        let textAsBase64String = textAsBase64StringDN.string()
        
        guard let textAsBase64Data = textAsBase64String.data(using: .utf8) else {
            XCTFail("Failed to convert base64 string to data")
            
            return
        }
        
        guard let textData = Data(base64Encoded: textAsBase64Data) else {
            XCTFail("Failed to decode base64 encoded data")
            
            return
        }
        
        guard let decodedText = String(data: textData, encoding: .utf8) else {
            XCTFail("Failed to decode text data to string")
            
            return
        }
        
        XCTAssertEqual(text, decodedText)
        
        let textBytesRet = try System_Convert.fromBase64String(textAsBase64StringDN)
        let textRet = try utf8Encoding.getString(textBytesRet).string()
        XCTAssertEqual(text, textRet)
    }
}
