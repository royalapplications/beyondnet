import XCTest
import NativeAOTCodeGeneratorOutputSample

// TODO
final class GenericTestClassTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testWith1GenericArgument() {
		let systemStringType = System_String.typeOf
		
		guard let genericTestClass = try? Beyond_NET_Sample_GenericTestClass_A1(systemStringType) else {
			XCTFail("GenericTestClass<System.String>.GenericTestClass ctor should not throw and return an instance")
			
			return
		}
		
		guard let genericTestClassType = try? genericTestClass.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		guard let genericTestClassTypeName = try? genericTestClassType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`1[[System.String"))
		
		guard let genericArgumentType = try? genericTestClass.returnGenericClassType(systemStringType) else {
			XCTFail("GenericTestClass<System.String>.ReturnGenericClassType should not throw and return an instance")
			
			return
		}
		
		let typesEqual = systemStringType == genericArgumentType
		XCTAssertTrue(typesEqual)
		
		let value: Int32 = 5
		
		XCTAssertNoThrow(try genericTestClass.aProperty_set(systemStringType, value))
		
		guard let propValueRet = try? genericTestClass.aProperty(systemStringType) else {
			XCTFail("Should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(value, propValueRet)
		
		genericTestClass.aField_set(systemStringType, value)
		
		let fieldValueRet = genericTestClass.aField(systemStringType)
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array.typeOf
		
		guard let genericArgumentAndMethodType = try? Beyond_NET_Sample_GenericTestClass_A1.returnGenericClassTypeAndGenericMethodType(systemStringType,
																																						systemArrayType) else {
			XCTFail("GenericTestClass<System.String>.ReturnGenericClassTypeAndGenericMethodType<System.Array> should not throw and return an instance")
			
			return
		}
		
		let genericArgumentAndMethodTypeLength = (try? genericArgumentAndMethodType.length) ?? -1
		XCTAssertEqual(2, genericArgumentAndMethodTypeLength)
		
		guard let genericTypeArgument = try? genericArgumentAndMethodType.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemStringType == genericTypeArgument)
		
		guard let genericMethodArgument = try? genericArgumentAndMethodType.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemArrayType == genericMethodArgument)
	}
	
	func testExtremeWith1GenericArgument() {
		let systemStringType = System_String.typeOf
		let systemExceptionType = System_Exception.typeOf
		
		guard let genericTestClass = try? Beyond_NET_Sample_GenericTestClass_A1(systemStringType) else {
			XCTFail("GenericTestClass<System.String>.GenericTestClass ctor should not throw and return an instance")
			
			return
		}
		
		let string = "Hello World"
		let stringDN = string.dotNETString()
		
		guard let originalException = try? System_Exception(stringDN) else {
			XCTFail("System.Exception ctor should not throw and return an instance")
			
			return
		}
		
		var inOutExceptionAsObject: System_Object? = originalException
		
		let countIn: Int32 = 15
		var countOut: Int32 = -1
		
		var output: System_Object?
		
		guard let returnValue = try? genericTestClass.extreme(systemStringType,
															  systemExceptionType,
															  countIn,
															  &countOut,
															  stringDN,
															  &output,
															  &inOutExceptionAsObject) else {
			XCTFail("GenericTestClass<System.String>.Extreme should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(countIn, countOut)
		
		let outputEqualsInput = stringDN == output
		XCTAssertTrue(outputEqualsInput)
		
		let returnValueEqualsInput = stringDN == returnValue
		XCTAssertTrue(returnValueEqualsInput)
		
		XCTAssertNil(inOutExceptionAsObject)
	}
	
	func testWith2GenericArguments() {
		let systemStringType = System_String.typeOf
		let systemExceptionType = System_Exception.typeOf
		
		guard let genericTestClass = try? Beyond_NET_Sample_GenericTestClass_A2(systemStringType,
																								 systemExceptionType) else {
			XCTFail("GenericTestClass<System.String, System.Exception> ctor should not throw and return an instance")
			
			return
		}
		
		guard let genericTestClassType = try? genericTestClass.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		guard let genericTestClassTypeName = try? genericTestClassType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`2[[System.String"))
		XCTAssertTrue(genericTestClassTypeName.contains(",[System.Exception"))
		
		guard let genericArgumentTypes = try? genericTestClass.returnGenericClassTypes(systemStringType,
																					   systemExceptionType) else {
			XCTFail("GenericTestClass<System.String, System.Exception>.ReturnGenericClassTypes should not throw and return an instance")
			
			return
		}
		
		let numberOfGenericArguments = (try? genericArgumentTypes.length) ?? -1
		XCTAssertEqual(2, numberOfGenericArguments)
		
		guard let firstGenericArgument = try? genericArgumentTypes.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let firstGenericTypeEqual = systemStringType == firstGenericArgument
		XCTAssertTrue(firstGenericTypeEqual)
		
		guard let secondGenericArgument = try? genericArgumentTypes.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let secondGenericTypeEqual = systemExceptionType == secondGenericArgument
		XCTAssertTrue(secondGenericTypeEqual)
		
		let value: Int32 = 5
		
		XCTAssertNoThrow(try genericTestClass.aProperty_set(systemStringType,
															systemExceptionType,
															value))
		
		do {
			let propValueRet = try genericTestClass.aProperty(systemStringType,
                                                              systemExceptionType)
			
			XCTAssertEqual(value, propValueRet)
		} catch {
			XCTFail("Should not throw")
			
			return
		}
		
		genericTestClass.aField_set(systemStringType,
									systemExceptionType,
									value)
		
        let fieldValueRet = genericTestClass.aField(systemStringType,
                                                    systemExceptionType)
		
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array.typeOf
		
		guard let genericArgumentsAndMethodType = try? Beyond_NET_Sample_GenericTestClass_A2.returnGenericClassTypeAndGenericMethodType(systemStringType,
																																						 systemExceptionType,
																																						 systemArrayType) else {
			XCTFail("GenericTestClass<System.String, System.Exception>.ReturnGenericClassTypeAndGenericMethodType<System.Array> should not throw and return an instance")
			
			return
		}
		
		let genericArgumentsAndMethodTypeLength = (try? genericArgumentsAndMethodType.length) ?? -1
		XCTAssertEqual(3, genericArgumentsAndMethodTypeLength)
		
		guard let firstGenericTypeArgument = try? genericArgumentsAndMethodType.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemStringType == firstGenericTypeArgument)
		
		guard let secondGenericTypeArgument = try? genericArgumentsAndMethodType.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemExceptionType == secondGenericTypeArgument)
		
		guard let genericMethodArgument = try? genericArgumentsAndMethodType.getValue(2 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemArrayType == genericMethodArgument)
	}
}
