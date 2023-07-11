import XCTest
import BeyondNETSamplesSwift

final class TransformerTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
//    func testStringTransformerAsInDocs() {
//        let inputString = "Hello World".dotNETString()
//        
//        let outputString = try! Beyond.NET.Sample.Transformer.transformString(inputString, .init({
//            try! $0!.toUpper()
//        }))!.string()
//        
//        print(outputString) // Prints "HELLO WORLD!"
//    }
    
    func testStringTransformer() {
        guard let uppercaser = createUppercaser() else {
            XCTFail("Failed to create uppercaser")
            
            return
        }
        
        let inputString = "Hello"
        let inputStringDN = inputString.dotNETString()
        
        let expectedOutputString = inputString.uppercased()
        
        guard let outputString = try? Beyond_NET_Sample_Transformer.transformString(inputStringDN,
                                                                                    uppercaser)?.string() else {
            XCTFail("Transformer.TransformString should not throw and return an instance of a c string")
            
            return
        }
        
        XCTAssertEqual(expectedOutputString, outputString)
    }
    
    func testStringGetterAndTransformer() {
        guard let fixedStringProvider = createFixedStringProvider() else {
            XCTFail("Failed to create random string provider")
            
            return
        }
        
        guard let uppercaser = createUppercaser() else {
            XCTFail("Failed to create uppercaser")
            
            return
        }
        
        guard let outputString = try? Beyond_NET_Sample_Transformer.getAndTransformString(fixedStringProvider,
                                                                                          uppercaser)?.string() else {
            XCTFail("Transformer.GetAndTransformString should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("FIXED STRING", outputString)
    }
    
    func testDoublesTransformer() {
        let multiplier: Beyond_NET_Sample_Transformer_DoublesTransformerDelegate.ClosureType = { number1, number2 in
            let result = number1 * number2
            
            return result
        }
        
        guard let doublesTransformerDelegate = Beyond_NET_Sample_Transformer_DoublesTransformerDelegate(multiplier) else {
            XCTFail("DoublesTransformerDelegate ctor should return an instance")
            
            return
        }
        
        let inputNumber1: Double = 2.5
        let inputNumber2: Double = 3.5
        
        let expectedResult = inputNumber1 * inputNumber2
        
        do {
            let result = try Beyond_NET_Sample_Transformer.transformDoubles(inputNumber1,
                                                                            inputNumber2,
                                                                            doublesTransformerDelegate)
            
            XCTAssertEqual(expectedResult, result)
        } catch {
            XCTFail("Should not throw")
            
            return
        }
    }
    
    func testUppercaserThatActuallyLowercases() {
        guard let lowercaser = createLowercaser() else {
            XCTFail("Failed to create lowercaser")
            
            return
        }
        
        XCTAssertNoThrow(try Beyond_NET_Sample_Transformer_BuiltInTransformers.uppercaseStringTransformer_set(lowercaser))
        
        let inputString = "Hello"
        let inputStringDN = inputString.dotNETString()
        
        let expectedOutputString = inputString.lowercased()
        
        guard let outputString = try? Beyond_NET_Sample_Transformer.uppercaseString(inputStringDN)?.string() else {
            XCTFail("Transformer.UppercaseString should not throw and return an instance of a c string")
            
            return
        }
        
        XCTAssertEqual(expectedOutputString, outputString)
    }
}

private extension TransformerTests_Swift {
    func createFixedStringProvider() -> Beyond_NET_Sample_Transformer_StringGetterDelegate? {
        let fixedStringProvider: Beyond_NET_Sample_Transformer_StringGetterDelegate.ClosureType = {
            let outputString = "Fixed String"
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        guard let stringGetterDelegate = Beyond_NET_Sample_Transformer_StringGetterDelegate(fixedStringProvider) else {
            XCTFail("StringGetterDelegate ctor should return an instance")
            
            return nil
        }
        
        return stringGetterDelegate
    }
    
    func createUppercaser() -> Beyond_NET_Sample_Transformer_StringTransformerDelegate? {
        let caser: Beyond_NET_Sample_Transformer_StringTransformerDelegate.ClosureType = { inputStringDN in
            guard let inputStringDN else { return nil }
            
            let inputString = inputStringDN.string()
            
            let outputString = inputString.uppercased()
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        guard let stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate(caser) else {
            XCTFail("StringTransformerDelegate ctor should return an instance")
            
            return nil
        }
        
        return stringTransformerDelegate
    }
    
    func createLowercaser() -> Beyond_NET_Sample_Transformer_StringTransformerDelegate? {
        let caser: Beyond_NET_Sample_Transformer_StringTransformerDelegate.ClosureType = { inputStringDN in
            guard let inputStringDN else { return nil }
            
            let inputString = inputStringDN.string()
            
            let outputString = inputString.lowercased()
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        guard let stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate(caser) else {
            XCTFail("StringTransformerDelegate ctor should return an instance")
            
            return nil
        }
        
        return stringTransformerDelegate
    }
}
