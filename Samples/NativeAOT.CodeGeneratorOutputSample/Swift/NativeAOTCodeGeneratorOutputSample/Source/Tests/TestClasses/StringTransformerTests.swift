import XCTest
import NativeAOTCodeGeneratorOutputSample

final class StringTransformerTests: XCTestCase {
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
			outputStringC = NativeAOT_CodeGeneratorInputSample_StringTransformer_TransformString(inputStringC,
																								 stringTransformerDelegate,
																								 &exception)
		}
		
		guard let outputStringC,
			  exception == nil else {
			XCTFail("StringTransformer.TransformString should not throw and return an instance of a c string")
			
			return
		}
		
		defer { outputStringC.deallocate() }
		
		let outputString = String(cString: outputStringC)
		
		XCTAssertEqual(expectedOutputString, outputString)
	}
}
