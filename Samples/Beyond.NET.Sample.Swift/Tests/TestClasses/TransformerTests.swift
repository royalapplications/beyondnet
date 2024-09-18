import XCTest
import BeyondDotNETSampleKit

final class TransformerTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    /* func testStringTransformerAsInDocs() {
        // Create an input string and convert it to a .NET System.String
        let inputString = "Hello World".dotNETString()
        
        // Call Beyond.NET.Sample.Transformer.transformString by:
        // - Providing the input string as the first argument
        // - Initializing an instance of Beyond_NET_Sample_Transformer_StringTransformerDelegate by passing it a closure that matches the .NET delegate as its sole parameter
        let outputString = try! Beyond.NET.Sample.Transformer.transformString(inputString, .init({ stringToTransform in
            // Take the string that should be transformed, call System.String.ToUpper on it and return it
            return try! stringToTransform!.toUpper()
        }))!.string() // Convert the returned System.String to a Swift String
        
        // Prints "HELLO WORLD!"
        print(outputString)
    } */
    
    func testStringTransformer() throws {
        guard let uppercaser = createUppercaser() else {
            XCTFail("Failed to create uppercaser")
            
            return
        }
        
        let inputString = "Hello"
        let inputStringDN = inputString.dotNETString()
        
        let expectedOutputString = inputString.uppercased()
        
        let outputString = try Beyond_NET_Sample_Transformer.transformString(inputStringDN,
                                                                             uppercaser).string()
        
        XCTAssertEqual(expectedOutputString, outputString)
    }
    
    func testStringGetterAndTransformer() throws {
        guard let fixedStringProvider = createFixedStringProvider() else {
            XCTFail("Failed to create random string provider")
            
            return
        }
        
        guard let uppercaser = createUppercaser() else {
            XCTFail("Failed to create uppercaser")
            
            return
        }
        
        let outputString = try Beyond_NET_Sample_Transformer.getAndTransformString(fixedStringProvider,
                                                                                   uppercaser).string()
        
        XCTAssertEqual("FIXED STRING", outputString)
    }
    
    func testDoublesTransformer() throws {
        let multiplier: Beyond_NET_Sample_Transformer_DoublesTransformerDelegate.ClosureType = { number1, number2 in
            let result = number1 * number2
            
            return result
        }
        
        let doublesTransformerDelegate = Beyond_NET_Sample_Transformer_DoublesTransformerDelegate(multiplier)
        
        let inputNumber1: Double = 2.5
        let inputNumber2: Double = 3.5
        
        let expectedResult = inputNumber1 * inputNumber2
        
        let result = try Beyond_NET_Sample_Transformer.transformDoubles(inputNumber1,
                                                                        inputNumber2,
                                                                        doublesTransformerDelegate)
        
        XCTAssertEqual(expectedResult, result)
    }
    
    func testUppercaserThatActuallyLowercases() throws {
        guard let lowercaser = createLowercaser() else {
            XCTFail("Failed to create lowercaser")
            
            return
        }
        
        try Beyond_NET_Sample_Transformer_BuiltInTransformers.uppercaseStringTransformer_set(lowercaser)
        
        let inputString = "Hello"
        let inputStringDN = inputString.dotNETString()
        
        let expectedOutputString = inputString.lowercased()
        
        let outputString = try Beyond_NET_Sample_Transformer.uppercaseString(inputStringDN).string()
        XCTAssertEqual(expectedOutputString, outputString)
    }
}

private extension TransformerTests {
    func createFixedStringProvider() -> Beyond_NET_Sample_Transformer_StringGetterDelegate? {
        let fixedStringProvider: Beyond_NET_Sample_Transformer_StringGetterDelegate.ClosureType = {
            let outputString = "Fixed String"
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        let stringGetterDelegate = Beyond_NET_Sample_Transformer_StringGetterDelegate(fixedStringProvider)
        
        return stringGetterDelegate
    }
    
    func createUppercaser() -> Beyond_NET_Sample_Transformer_StringTransformerDelegate? {
        let caser: Beyond_NET_Sample_Transformer_StringTransformerDelegate.ClosureType = { inputStringDN in
            let inputString = inputStringDN.string()
            
            let outputString = inputString.uppercased()
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        let stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate(caser)
        
        return stringTransformerDelegate
    }
    
    func createLowercaser() -> Beyond_NET_Sample_Transformer_StringTransformerDelegate? {
        let caser: Beyond_NET_Sample_Transformer_StringTransformerDelegate.ClosureType = { inputStringDN in
            let inputString = inputStringDN.string()
            
            let outputString = inputString.lowercased()
            let outputStringDN = outputString.dotNETString()
            
            return outputStringDN
        }
        
        let stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate(caser)
        
        return stringTransformerDelegate
    }
}
