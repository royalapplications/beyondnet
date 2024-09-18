import XCTest
import BeyondDotNETSampleKit

// TODO
final class GenericTestClassTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testWith1GenericArgument() throws {
		let systemStringType = System_String.typeOf
		
        let genericTestClass = try Beyond_NET_Sample_GenericTestClass_A1(T: systemStringType)
		let genericTestClassType = try genericTestClass.getType()
		
		guard let genericTestClassTypeName = try genericTestClassType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`1[[System.String"))
		
        let genericArgumentType = try genericTestClass.returnGenericClassType(T: systemStringType)
		
		let typesEqual = systemStringType == genericArgumentType
		XCTAssertTrue(typesEqual)
		
		let value: Int32 = 5
		
        try genericTestClass.aProperty_set(T: systemStringType, value)
		
        let propValueRet = try genericTestClass.aProperty(T: systemStringType)
		
		XCTAssertEqual(value, propValueRet)
		
        genericTestClass.aField_set(T: systemStringType, value)
		
        let fieldValueRet = genericTestClass.aField(T: systemStringType)
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array.typeOf
        
        let genericArgumentAndMethodType = try Beyond_NET_Sample_GenericTestClass_A1.returnGenericClassTypeAndGenericMethodType(T: systemStringType,
                                                                                                                                TM: systemArrayType)
		
		let genericArgumentAndMethodTypeLength = try genericArgumentAndMethodType.length
		XCTAssertEqual(2, genericArgumentAndMethodTypeLength)
		
		guard let genericTypeArgument = try genericArgumentAndMethodType.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemStringType == genericTypeArgument)
		
		guard let genericMethodArgument = try genericArgumentAndMethodType.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemArrayType == genericMethodArgument)
	}
	
	func testExtremeWith1GenericArgument() throws {
		let systemStringType = System_String.typeOf
		let systemExceptionType = System_Exception.typeOf
		
        let genericTestClass = try Beyond_NET_Sample_GenericTestClass_A1(T: systemStringType)
		
		let string = "Hello World"
		let stringDN = string.dotNETString()
		
		let originalException = try System_Exception(stringDN)
		
		var inOutExceptionAsObject: System_Object? = originalException
		
		let countIn: Int32 = 15
		var countOut: Int32 = -1
		
		var output: System_Object?
		
        let returnValue = try genericTestClass.extreme(T: systemStringType,
                                                       TM: systemExceptionType,
                                                       countIn,
                                                       &countOut,
                                                       stringDN,
                                                       &output,
                                                       &inOutExceptionAsObject)
		
		XCTAssertEqual(countIn, countOut)
		
		let outputEqualsInput = stringDN == output
		XCTAssertTrue(outputEqualsInput)
		
		let returnValueEqualsInput = stringDN == returnValue
		XCTAssertTrue(returnValueEqualsInput)
		
		XCTAssertNil(inOutExceptionAsObject)
	}
	
	func testWith2GenericArguments() throws {
		let systemStringType = System_String.typeOf
		let systemExceptionType = System_Exception.typeOf
		
        let genericTestClass = try Beyond_NET_Sample_GenericTestClass_A2(T1: systemStringType,
                                                                         T2: systemExceptionType)
		
		let genericTestClassType = try genericTestClass.getType()
		
		guard let genericTestClassTypeName = try genericTestClassType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(genericTestClassTypeName.contains("GenericTestClass`2[[System.String"))
		XCTAssertTrue(genericTestClassTypeName.contains(",[System.Exception"))
		
        let genericArgumentTypes = try genericTestClass.returnGenericClassTypes(T1: systemStringType,
                                                                                T2: systemExceptionType)
		
		let numberOfGenericArguments = try genericArgumentTypes.length
		XCTAssertEqual(2, numberOfGenericArguments)
		
		guard let firstGenericArgument = try genericArgumentTypes.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let firstGenericTypeEqual = systemStringType == firstGenericArgument
		XCTAssertTrue(firstGenericTypeEqual)
		
		guard let secondGenericArgument = try genericArgumentTypes.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		let secondGenericTypeEqual = systemExceptionType == secondGenericArgument
		XCTAssertTrue(secondGenericTypeEqual)
		
		let value: Int32 = 5
		
        try genericTestClass.aProperty_set(T1: systemStringType,
                                           T2: systemExceptionType,
                                           value)
        
        let propValueRet = try genericTestClass.aProperty(T1: systemStringType,
                                                          T2: systemExceptionType)
        
        XCTAssertEqual(value, propValueRet)
		
        genericTestClass.aField_set(T1: systemStringType,
                                    T2: systemExceptionType,
									value)
		
        let fieldValueRet = genericTestClass.aField(T1: systemStringType,
                                                    T2: systemExceptionType)
		
		XCTAssertEqual(value, fieldValueRet)
		
		let systemArrayType = System_Array.typeOf
		
        let genericArgumentsAndMethodType = try Beyond_NET_Sample_GenericTestClass_A2.returnGenericClassTypeAndGenericMethodType(T1: systemStringType,
                                                                                                                                 T2: systemExceptionType,
                                                                                                                                 TM: systemArrayType)
		
		let genericArgumentsAndMethodTypeLength = try genericArgumentsAndMethodType.length
		XCTAssertEqual(3, genericArgumentsAndMethodTypeLength)
		
		guard let firstGenericTypeArgument = try genericArgumentsAndMethodType.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemStringType == firstGenericTypeArgument)
		
		guard let secondGenericTypeArgument = try genericArgumentsAndMethodType.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemExceptionType == secondGenericTypeArgument)
		
		guard let genericMethodArgument = try genericArgumentsAndMethodType.getValue(2 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(systemArrayType == genericMethodArgument)
	}
}
