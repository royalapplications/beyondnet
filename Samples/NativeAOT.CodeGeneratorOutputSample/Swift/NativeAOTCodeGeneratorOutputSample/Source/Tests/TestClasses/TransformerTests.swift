import XCTest
import NativeAOTCodeGeneratorOutputSample

final class TransformerTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testStringTransformer() {
		var exception: System_Exception_t?
		
		guard let uppercaser = createUppercaser() else {
			XCTFail("Failed to create uppercaser")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(uppercaser) }
		
		let inputString = "Hello"
		let inputStringDN = inputString.cDotNETString()
		defer { System_String_Destroy(inputStringDN) }
		
		let expectedOutputString = inputString.uppercased()
		
		guard let outputString = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_Transformer_TransformString(inputStringDN,
																													 uppercaser,
																													 &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Transformer.TransformString should not throw and return an instance of a c string")
			
			return
		}
		
		XCTAssertEqual(expectedOutputString, outputString)
	}
	
	func testStringGetterAndTransformer() {
		var exception: System_Exception_t?
		
		guard let fixedStringProvider = createFixedStringProvider() else {
			XCTFail("Failed to create random string provider")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate_Destroy(fixedStringProvider) }
		
		guard let uppercaser = createUppercaser() else {
			XCTFail("Failed to create uppercaser")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(uppercaser) }
		
		guard let outputString = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_Transformer_GetAndTransformString(fixedStringProvider,
																														   uppercaser,
																														   &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Transformer.GetAndTransformString should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual("FIXED STRING", outputString)
	}
	
	func testDoublesTransformer() {
		var exception: System_Exception_t?
		
		let multiplier: NativeAOT_CodeGeneratorInputSample_Transformer_DoublesTransformerDelegate_CFunction_t = { _, number1, number2 in
			let result = number1 * number2
			
			return result
		}
		
		guard let doublesTransformerDelegate = NativeAOT_CodeGeneratorInputSample_Transformer_DoublesTransformerDelegate_Create(nil,
																																multiplier,
																																nil) else {
			XCTFail("DoublesTransformerDelegate ctor should return an instance")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Transformer_DoublesTransformerDelegate_Destroy(doublesTransformerDelegate) }
		
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
	
	func testUppercaserThatActuallyLowercases() {
		var exception: System_Exception_t?
		
		guard let lowercaser = createLowercaser() else {
			XCTFail("Failed to create lowercaser")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Destroy(lowercaser) }
		
		NativeAOT_CodeGeneratorInputSample_Transformer_BuiltInTransformers_UppercaseStringTransformer_Set(lowercaser,
																										  &exception)
		
		XCTAssertNil(exception)
		
		let inputString = "Hello"
		let inputStringDN = inputString.cDotNETString()
		defer { System_String_Destroy(inputStringDN) }
		
		let expectedOutputString = inputString.lowercased()
		
		guard let outputString = String(cDotNETString: NativeAOT_CodeGeneratorInputSample_Transformer_UppercaseString(inputStringDN,
																													 &exception),
										destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Transformer.UppercaseString should not throw and return an instance of a c string")
			
			return
		}
		
		XCTAssertEqual(expectedOutputString, outputString)
	}
}

private extension TransformerTests {
	func createFixedStringProvider() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate_t? {
		let fixedStringProvider: NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate_CFunction_t = { _ in
			let outputString = "Fixed String"
			let outputStringDN = outputString.cDotNETString()
			
			return outputStringDN
		}
		
		guard let stringGetterDelegate = NativeAOT_CodeGeneratorInputSample_Transformer_StringGetterDelegate_Create(nil,
																													fixedStringProvider,
																													nil) else {
			XCTFail("StringGetterDelegate ctor should return an instance")
			
			return nil
		}
		
		return stringGetterDelegate
	}
	
	func createUppercaser() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_t? {
		let caser: NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CFunction_t = { innerContext, inputStringDN in
			guard let inputStringDN else { return nil }
			
			// We need to release any reference types that are given to us through the delegate
			defer { System_String_Destroy(inputStringDN) }
			
			guard let inputString = String(cDotNETString: inputStringDN) else {
				XCTFail("Failed to convert string")
				
				return nil
			}
			
			let outputString = inputString.uppercased()
			let outputStringDN = outputString.cDotNETString()
			
			return outputStringDN
		}
		
		guard let stringTransformerDelegate = NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Create(nil,
																															  caser,
																															  nil) else {
			XCTFail("StringTransformerDelegate ctor should return an instance")
			
			return nil
		}
		
		return stringTransformerDelegate
	}
	
	func createLowercaser() -> NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_t? {
		let caser: NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_CFunction_t = { innerContext, inputStringDN in
			guard let inputStringDN else { return nil }
			
			// We need to release any reference types that are given to us through the delegate
			defer { System_String_Destroy(inputStringDN) }
			
			guard let inputString = String(cDotNETString: inputStringDN) else {
				XCTFail("Failed to convert string")
				
				return nil
			}
			
			let outputString = inputString.lowercased()
			let outputStringDN = outputString.cDotNETString()
			
			return outputStringDN
		}
		
		guard let stringTransformerDelegate = NativeAOT_CodeGeneratorInputSample_Transformer_StringTransformerDelegate_Create(nil,
																															  caser,
																															  nil) else {
			XCTFail("StringTransformerDelegate ctor should return an instance")
			
			return nil
		}
		
		return stringTransformerDelegate
	}
}
