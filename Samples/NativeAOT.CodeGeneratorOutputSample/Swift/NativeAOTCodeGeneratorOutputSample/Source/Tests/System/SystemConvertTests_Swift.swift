import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemConvertTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testBooleanConversion() {
        let trueStringDN = "true".dotNETString()
        let falseStringDN = "false".dotNETString()
        let nonsenseStringDN = "nonsense".dotNETString()
        
        do {
            let result = try System_Convert.toBoolean(trueStringDN)
            
            XCTAssertTrue(result)
        } catch {
            XCTFail("System.Convert.ToBoolean should not throw")
            
            return
        }
        
        do {
            let result = try System_Convert.toBoolean(falseStringDN)
            
            XCTAssertFalse(result)
        } catch {
            XCTFail("System.Convert.ToBoolean should not throw")
            
            return
        }
        
        do {
            let _ = try System_Convert.toBoolean(nonsenseStringDN)
            
            XCTFail("System.Convert.ToBoolean should throw")
        } catch { }
    }
    
    func testIntegerConversion() {
        let number1: Int32 = 123456789
        let number1StringDN = "\(number1)".dotNETString()
        
        XCTAssertEqual(number1,
                       (try? System_Convert.toInt32(number1StringDN)) ?? -1)
        
        let number2: Int64 = -123456789
        let number2StringDN = "\(number2)".dotNETString()
        
        XCTAssertEqual(number2,
                       (try? System_Convert.toInt64(number2StringDN)) ?? -1)
        
        let number3: Int64 = -1
        let number3StringDN = "nonsense".dotNETString()
        
        XCTAssertEqual(number3,
                       (try? System_Convert.toInt64(number3StringDN)) ?? -1)
        
        let number4: UInt64 = 0
        let number4StringDN = "nonsense".dotNETString()
        
        XCTAssertEqual(number4,
                       (try? System_Convert.toUInt64(number4StringDN)) ?? 0)
    }
    
    func testBase64Conversion() {
        let text = "Hello World!"
        let textDN = text.dotNETString()
        
        guard let utf8Encoding = try? System_Text_Encoding.uTF8_get() else {
            XCTFail("System.Text.Encoding.UTF8 getter should not throw and return an instance")
            
            return
        }
        
        guard let textBytes = try? utf8Encoding.getBytes(textDN) else {
            XCTFail("System.Text.Encoding.GetBytes should not throw and return an instance")
            
            return
        }
        
        guard let textAsBase64StringDN = try? System_Convert.toBase64String(textBytes) else {
            XCTFail("System.Convert.ToBase64String should not throw and return an instance of a C String")
            
            return
        }
        
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
        
        guard let textBytesRet = try? System_Convert.fromBase64String(textAsBase64StringDN) else {
            XCTFail("System.Convert.FromBase64String should not throw and return an instance")
            
            return
        }
        
        guard let textRet = (try? utf8Encoding.getString(textBytesRet))?.string() else {
            XCTFail("System.Text.Encoding.GetString should not throw and return an instance of a C String")
            
            return
        }
        
        XCTAssertEqual(text, textRet)
    }
}
