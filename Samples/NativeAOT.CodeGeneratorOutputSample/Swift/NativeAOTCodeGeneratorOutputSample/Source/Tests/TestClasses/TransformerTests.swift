import XCTest
import NativeAOTCodeGeneratorOutputSample

final class TransformerTests: XCTestCase {
	func testStringTransformer() {
		var exception: System_Exception_t?
		
		let uppercaser: NativeAOT_CodeGeneratorInputSample_StringTransformerDelegate_CFunction_t = { innerContext, inputStringC in
			guard let inputStringC else {
				return nil
			}
			
			let inputString = String(cString: inputStringC)
			let outputString = inputString.uppercased()
			
			// TODO: This creates a memory leak
			let outputStringC = strdup(outputString)
			
			// TODO: This could crash if the runtime decides to destroy the NSString or underlying UTF8 string pointer before our managed function is done with the string.
//			let outputStringC = (outputString as NSString).utf8String
			
			// Not sure how to deal with this...
			
			return .init(outputStringC)
		}
		
		guard let stringTransformerDelegate = NativeAOT_CodeGeneratorInputSample_StringTransformerDelegate_Create(nil,
																												  uppercaser,
																												  nil) else {
			XCTFail("StringTransformerDelegate ctor should return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_StringTransformerDelegate_Destroy(stringTransformerDelegate) }
		
		let inputString = "Hello"
		let expectedOutputString = inputString.uppercased()
		
		var outputStringC: CString?
		
		inputString.withCString { inputStringC in
			outputStringC = NativeAOT_CodeGeneratorInputSample_Transformer_TransformString(inputStringC,
																						   stringTransformerDelegate,
																						   &exception)
		}
		
		guard let outputStringC,
			  exception == nil else {
			XCTFail("Transformer.TransformString should not throw and return an instance of a c string")
			
			return
		}
		
		defer { outputStringC.deallocate() }
		
		let outputString = String(cString: outputStringC)
		
		XCTAssertEqual(expectedOutputString, outputString)
	}
	
	func testDoublesTransformer() {
		var exception: System_Exception_t?
		
		let multiplier: NativeAOT_CodeGeneratorInputSample_DoublesTransformerDelegate_CFunction_t = { _, number1, number2 in
			let result = number1 * number2
			
			return result
		}
		
		guard let doublesTransformerDelegate = NativeAOT_CodeGeneratorInputSample_DoublesTransformerDelegate_Create(nil,
																													multiplier,
																													nil) else {
			XCTFail("DoublesTransformerDelegate ctor should return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_DoublesTransformerDelegate_Destroy(doublesTransformerDelegate) }
		
		let inputNumber1: Double = 2.5
		let inputNumber2: Double = 3.5
		
		let expectedResult = inputNumber1 * inputNumber2
		
		let result = NativeAOT_CodeGeneratorInputSample_Transformer_TransformDoubles(inputNumber1,
																					 inputNumber2,
																					 doublesTransformerDelegate,
																					 &exception)
		
		XCTAssertNil(exception)
		
		XCTAssertEqual(expectedResult, result)
	}
}
