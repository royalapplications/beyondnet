import XCTest
import BeyondNETSampleSwift

final class GenericTestsTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testReturnGenericTypeWithReferenceType() {
		let genericType = System_String.typeOf
		
		guard let typeRet = try? Beyond_NET_Sample_GenericTests.returnGenericType(genericType) else {
			XCTFail("ReturnGenericType<System.String> should not throw and return an instance")
			
			return
		}
		
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	// Currently, value types as generic parameters aren't supported through reflection when compiling with NativeAOT
	// It's possible to specifically include these types with a Rd.xml
	// https://github.com/dotnet/runtime/blob/main/src/coreclr/nativeaot/docs/rd-xml-format.md
	func testReturnGenericTypeWithValueType() {
		let genericType = System_Guid.typeOf
		
		do {
			_ = try Beyond_NET_Sample_GenericTests.returnGenericType(genericType)
			
			XCTFail("This should fail")
		} catch { }
	}
	
	func testReturnGenericTypeAsOutParameter() {
		let genericType = System_String.typeOf
		
		var typeRet: System_Type?
		
		XCTAssertNoThrow(try Beyond_NET_Sample_GenericTests.returnGenericTypeAsOutParameter(genericType,
																											 &typeRet))
		
		guard let typeRet else {
			XCTFail("ReturnGenericTypeAsOutParameter<System.String> should not throw and return an instance as out parameter")
			
			return
		}
		
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypeAsRefParameter() {
		let genericType = System_String.typeOf
		let systemObjectType = System_Object.typeOf
		
		var typeRet: System_Type? = systemObjectType
		
		XCTAssertNoThrow(try Beyond_NET_Sample_GenericTests.returnGenericTypeAsRefParameter(genericType,
																											 &typeRet))
		
		guard let typeRet else {
			XCTFail("ReturnGenericTypeAsRefParameter<System.String> should not throw and return an instance as ref parameter")
			
			return
		}
		
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypes() {
		let genericType1 = System_String.typeOf
		let genericType2 = System_Array.typeOf
		
		guard let typesArrayRet = try? Beyond_NET_Sample_GenericTests.returnGenericTypes(genericType1,
																										  genericType2) else {
			XCTFail("ReturnGenericTypes<System.String, System.Array> should not throw and return an instance")
			
			return
		}
		
		let typesLength = (try? typesArrayRet.length) ?? -1
		XCTAssertEqual(2, .init(typesLength))
		
		for idx in 0..<typesLength {
			guard let type = try? typesArrayRet.getValue(idx) else {
				XCTFail("System.Array.GetValue should not throw and return an instance")
				
				return
			}
			
			let typeToCompareTo: System_Type
			
			switch idx {
				case 0:
					typeToCompareTo = genericType1
				case 1:
					typeToCompareTo = genericType2
				default:
					XCTFail("Unknown index")
					
					return
			}
			
			let typesEqual = type == typeToCompareTo
			XCTAssertTrue(typesEqual)
		}
	}
	
	func testReturnDefaultValueOfGenericType() {
		let genericType = System_String.typeOf
		
		do {
			let defaultValueRet = try Beyond_NET_Sample_GenericTests.returnDefaultValueOfGenericType(genericType)
			
			XCTAssertNil(defaultValueRet)
		} catch {
			XCTFail("Should not throw")
			
			return
		}
	}
	
	func testReturnArrayOfDefaultValuesOfGenericType() {
		let genericType = System_String.typeOf
		
		let numberOfElements: Int32 = 10
		
		guard let defaultValuesArrayRet = try? Beyond_NET_Sample_GenericTests.returnArrayOfDefaultValuesOfGenericType(genericType,
																																	   numberOfElements) else {
			XCTFail("ReturnArrayOfDefaultValuesOfGenericType should not throw and return an instance")
			
			return
		}
		
		let length = (try? defaultValuesArrayRet.length) ?? -1
		XCTAssertEqual(numberOfElements, length)
		
		for i in 0..<length {
			do {
				let defaultValue = try defaultValuesArrayRet.getValue(i)
				XCTAssertNil(defaultValue)
			} catch {
				XCTFail("Should not throw")
			}
		}
	}
	
	func testReturnArrayOfRepeatedValues() {
		let numberOfElements: Int32 = 100

		guard let value = try? System_Object() else {
			XCTFail("System.Object ctor should not throw and return an instance")

			return
		}

		guard let systemObjectType = try? value.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")

			return
		}

		guard let genericTests = try? Beyond_NET_Sample_GenericTests() else {
			XCTFail("GenericTests ctor should not throw and return an instance")

			return
		}

		guard let arrayRet = try? genericTests.returnArrayOfRepeatedValues(systemObjectType,
																		   value,
																		   numberOfElements) else {
			XCTFail("ReturnArrayOfRepeatedValues should not throw and return an instance")

			return
		}

		let length = (try? arrayRet.length) ?? -1
		XCTAssertEqual(numberOfElements, length)

		for idx in 0..<length {
			guard let element = try? arrayRet.getValue(idx) else {
				XCTFail("System.Array.GetValue should not throw and return an instance")

				return
			}

			let equal = value == element
			XCTAssertTrue(equal)
		}
	}
	
	func testReturnSimpleKeyValuePair() {
		let keyType = System_Type.typeOf
		let valueType = System_Text_StringBuilder.typeOf
		let key = System_Type.typeOf
		
		guard let value = try? System_Text_StringBuilder() else {
			XCTFail("System.Text.StringBuilder ctor should not throw and return an instance")
			
			return
		}
		
		let expectedString = "Hello World"
		let expectedStringDN = expectedString.dotNETString()
		
		XCTAssertNoThrow(try value.append(expectedStringDN))
		
		guard let keyValuePair = try? Beyond_NET_Sample_GenericTests.returnSimpleKeyValuePair(keyType,
																											   valueType,
																											   key,
																											   value) else {
			XCTFail("ReturnSimpleKeyValuePair should not throw and return an instance")
			
			return
		}
		
		guard let keyRet = try? keyValuePair.key else {
			XCTFail("SimpleKeyValuePair.Key getter should not throw and return an instance")
			
			return
		}
		
		let keyEqual = key == keyRet
		XCTAssertTrue(keyEqual)
		
		guard let valueRet = try? keyValuePair.value else {
			XCTFail("SimpleKeyValuePair.Value getter should not throw and return an instance")
			
			return
		}
		
		let valueEqual = value == valueRet
		XCTAssertTrue(valueEqual)
		
		guard let stringRet = try? valueRet.castTo(System_Text_StringBuilder.self).toString()?.string() else {
			XCTFail("System.Text.StringBuilder.ToString should not throw and return an instance of a string")
			
			return
		}
		
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testIncorrectParametersInGenericMethod() {
		let keyType = System_Type.typeOf
		let valueType = System_Text_StringBuilder.typeOf
		let key = System_Type.typeOf
		
		guard let value = try? System_Object() else {
			XCTFail("System.Object ctor should not throw and return an instance")
			
			return
		}
		
		do {
			_ = try Beyond_NET_Sample_GenericTests.returnSimpleKeyValuePair(keyType,
																							 valueType,
																							 key,
																							 value)
			
			XCTFail("ReturnSimpleKeyValuePair should throw an exception because the value passed in does not match the provided generic type")
		} catch {
			let exceptionMessage = error.localizedDescription
			
			XCTAssertEqual(exceptionMessage, "Object of type \'System.Object\' cannot be converted to type \'System.Text.StringBuilder\'.")
		}
	}
	
	func testReturnStringOfJoinedArray() {
		let numberOfElements: Int32 = 10
		let stringPrefix = "Hello_"
		
		let separator = ";"
		let separatorDN = separator.dotNETString()
		
		let stringType = System_String.typeOf

		guard let arrayOfStrings = try? System_Array.createInstance(stringType,
																	numberOfElements) else {
			XCTFail("System.Array ctor should not throw and return an instance")

			return
		}

		var dnStrings = [System_String]()
		var strings = [String]()
		
		for idx in 0..<numberOfElements {
			let string = "\(stringPrefix)\(idx)"
			let stringDN = string.dotNETString()
			
			dnStrings.append(stringDN)
			strings.append(string)

			XCTAssertNoThrow(try arrayOfStrings.setValue(stringDN, idx))
		}
		
		let expectedString = strings.joined(separator: separator)

		guard let stringRet = try? Beyond_NET_Sample_GenericTests.returnStringOfJoinedArray(stringType,
																											 arrayOfStrings,
																											 separatorDN)?.string() else {
			XCTFail("ReturnStringOfJoinedArray should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testConstructedGenericListOfStrings() {
		guard let genericTests = try? Beyond_NET_Sample_GenericTests() else {
			XCTFail("GenericTests ctor should not throw and return an instance")
			
			return
		}
		
		guard let listOfStrings = try? genericTests.listOfStrings else {
			XCTFail("GenericTests.ListOfStrings getter should not throw and return an instance")
			
			return
		}
		
		let listType = System_Collections_Generic_List_A1.typeOf
		let systemTypeType = System_Type.typeOf
		let systemStringType = System_String.typeOf
		
		guard let typeArguments = try? System_Array.createInstance(systemTypeType,
																   1) else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		XCTAssertNoThrow(try typeArguments.setValue(systemStringType, 0 as Int32))
		
		guard let listOfStringType = try? listType.makeGenericType(typeArguments.castTo(System_Type_Array.self)) else {
			XCTFail("System.Type.MakeGenericType should not throw and return an instance")
			
			return
		}
		
		XCTAssertTrue(listOfStrings.is(listOfStringType))
		
		guard let arrayOfStrings = try? listOfStrings.toArray(systemStringType) else {
			XCTFail("System.Collections.Generic.List<System.String>.ToArray should not throw and return an instance")
			
			return
		}
		
		let length = (try? arrayOfStrings.length) ?? -1
		XCTAssertEqual(2, length)
		
		guard let firstObject = try? arrayOfStrings.getValue(0 as Int32),
			  let firstString = try? firstObject.castTo(System_String.self).string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual("A", firstString)
		
		guard let secondObject = try? arrayOfStrings.getValue(1 as Int32),
			  let secondString = try? secondObject.castTo(System_String.self).string() else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual("B", secondString)
	}
}
