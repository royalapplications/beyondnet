import XCTest
import BeyondDotNETSampleKit

final class GenericTestsTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testReturnGenericTypeWithReferenceType() throws {
		let genericType = System_String.typeOf
		
        let typeRet = try Beyond_NET_Sample_GenericTests.returnGenericType(T: genericType)
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	// Currently, value types as generic parameters aren't supported through reflection when compiling with NativeAOT
	// It's possible to specifically include these types with a Rd.xml
	// https://github.com/dotnet/runtime/blob/main/src/coreclr/nativeaot/docs/rd-xml-format.md
	func testReturnGenericTypeWithValueType() {
		let genericType = System_Guid.typeOf
		
		do {
            _ = try Beyond_NET_Sample_GenericTests.returnGenericType(T: genericType)
			
			XCTFail("This should fail")
		} catch { }
	}
	
	func testReturnGenericTypeAsOutParameter() throws {
        let genericType = System_String.typeOf
        
        var typeRet: System_Type?
        
        try Beyond_NET_Sample_GenericTests.returnGenericTypeAsOutParameter(T: genericType,
                                                                           &typeRet)
        
        guard let typeRet else {
			XCTFail("ReturnGenericTypeAsOutParameter<System.String> should not throw and return an instance as out parameter")
			
			return
		}
		
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypeAsRefParameter() throws {
		let genericType = System_String.typeOf
		let systemObjectType = System_Object.typeOf
        
        var typeRet: System_Type? = systemObjectType
        
        try Beyond_NET_Sample_GenericTests.returnGenericTypeAsRefParameter(T: genericType,
                                                                           &typeRet)
        
        guard let typeRet else {
            XCTFail("ReturnGenericTypeAsRefParameter<System.String> should not throw and return an instance as ref parameter")
            
			return
		}
		
		let typesEqual = genericType == typeRet
		XCTAssertTrue(typesEqual)
	}
	
	func testReturnGenericTypes() throws {
		let genericType1 = System_String.typeOf
        let genericType2 = System_Array.typeOf
        
        let typesArrayRet = try Beyond_NET_Sample_GenericTests.returnGenericTypes(T1: genericType1,
                                                                                  T2: genericType2)
        
        let typesLength = try typesArrayRet.length
        XCTAssertEqual(2, .init(typesLength))
        
        for idx in 0..<typesLength {
            guard let type = try typesArrayRet.getValue(idx) else {
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
	
	func testReturnDefaultValueOfGenericType() throws {
		let genericType = System_String.typeOf
		
        let defaultValueRet = try Beyond_NET_Sample_GenericTests.returnDefaultValueOfGenericType(T: genericType)
        XCTAssertNil(defaultValueRet)
	}
	
	func testReturnArrayOfDefaultValuesOfGenericType() throws {
		let genericType = System_String.typeOf
		
		let numberOfElements: Int32 = 10
		
        let defaultValuesArrayRet = try Beyond_NET_Sample_GenericTests.returnArrayOfDefaultValuesOfGenericType(T: genericType,
                                                                                                               numberOfElements)
		
		let length = try defaultValuesArrayRet.length
		XCTAssertEqual(numberOfElements, length)
		
		for i in 0..<length {
            let defaultValue = try defaultValuesArrayRet.getValue(i)
            XCTAssertNil(defaultValue)
		}
	}
	
	func testReturnArrayOfRepeatedValues() throws {
		let numberOfElements: Int32 = 100

		let value = try System_Object()
		let systemObjectType = try value.getType()

		let genericTests = try Beyond_NET_Sample_GenericTests()

        let arrayRet = try genericTests.returnArrayOfRepeatedValues(T: systemObjectType,
                                                                    value,
                                                                    numberOfElements)

		let length = try arrayRet.length
		XCTAssertEqual(numberOfElements, length)

		for idx in 0..<length {
			guard let element = try arrayRet.getValue(idx) else {
				XCTFail("System.Array.GetValue should not throw and return an instance")

				return
			}

			let equal = value == element
			XCTAssertTrue(equal)
		}
	}
	
	func testReturnSimpleKeyValuePair() throws {
		let keyType = System_Type.typeOf
		let valueType = System_Text_StringBuilder.typeOf
		let key = System_Type.typeOf
		
		let value = try System_Text_StringBuilder()
		
		let expectedString = "Hello World"
		let expectedStringDN = expectedString.dotNETString()
        
        guard try value.append(expectedStringDN) == value else {
            XCTFail("Should return true")
            
            return
        }
        
        let keyValuePair = try Beyond_NET_Sample_GenericTests.returnSimpleKeyValuePair(TKey: keyType,
                                                                                       TValue: valueType,
                                                                                       key,
                                                                                       value)
		
		guard let keyRet = try keyValuePair.key else {
			XCTFail("SimpleKeyValuePair.Key getter should not throw and return an instance")
			
			return
		}
		
		let keyEqual = key == keyRet
		XCTAssertTrue(keyEqual)
		
		guard let valueRet = try keyValuePair.value else {
			XCTFail("SimpleKeyValuePair.Value getter should not throw and return an instance")
			
			return
		}
		
		let valueEqual = value == valueRet
		XCTAssertTrue(valueEqual)
		
		let stringRet = try valueRet.castTo(System_Text_StringBuilder.self).toString().string()
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testIncorrectParametersInGenericMethod() throws {
		let keyType = System_Type.typeOf
		let valueType = System_Text_StringBuilder.typeOf
		let key = System_Type.typeOf
		
		let value = try System_Object()
        
        do {
            _ = try Beyond_NET_Sample_GenericTests.returnSimpleKeyValuePair(TKey: keyType,
                                                                            TValue: valueType,
                                                                            key,
                                                                            value)
            
            XCTFail("ReturnSimpleKeyValuePair should throw an exception because the value passed in does not match the provided generic type")
        } catch {
			let exceptionMessage = error.localizedDescription
			
			XCTAssertEqual(exceptionMessage, "Object of type \'System.Object\' cannot be converted to type \'System.Text.StringBuilder\'.")
		}
	}
	
	func testReturnStringOfJoinedArray() throws {
		let numberOfElements: Int32 = 10
		let stringPrefix = "Hello_"
		
		let separator = ";"
		let separatorDN = separator.dotNETString()
		
		let stringType = System_String.typeOf

		let arrayOfStrings = try DNArray<System_String>(length: numberOfElements)

		var dnStrings = [System_String]()
		var strings = [String]()
		
		for idx in 0..<numberOfElements {
			let string = "\(stringPrefix)\(idx)"
			let stringDN = string.dotNETString()
			
			dnStrings.append(stringDN)
			strings.append(string)

			try arrayOfStrings.setValue(stringDN, idx)
		}
		
		let expectedString = strings.joined(separator: separator)

        let stringRet = try Beyond_NET_Sample_GenericTests.returnStringOfJoinedArray(T: stringType,
                                                                                     arrayOfStrings,
                                                                                     separatorDN).string()
		
		XCTAssertEqual(expectedString, stringRet)
	}
	
	func testConstructedGenericListOfStrings() throws {
		let genericTests = try Beyond_NET_Sample_GenericTests()
        
		let listOfStrings = try genericTests.listOfStrings
        
		let listType = System_Collections_Generic_List_A1.typeOf
		let systemStringType = System_String.typeOf
		
		let typeArguments = try DNArray<System_Type>(length: 1)
        
        typeArguments[0] = systemStringType
		
        let listOfStringType = try listType.makeGenericType(typeArguments)
		XCTAssertTrue(listOfStrings.is(listOfStringType))
		
        let arrayOfStrings = try listOfStrings.toArray(T: systemStringType)
		
		let length = try arrayOfStrings.length
		XCTAssertEqual(2, length)
		
		guard let firstObject = try arrayOfStrings.getValue(0 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
        
        let firstString = try firstObject.castTo(System_String.self).string()
		XCTAssertEqual("A", firstString)
		
		guard let secondObject = try arrayOfStrings.getValue(1 as Int32) else {
			XCTFail("System.Array.GetValue should not throw and return an instance")
			
			return
		}
		
        let secondString = try secondObject.castTo(System_String.self).string()
		XCTAssertEqual("B", secondString)
	}
}
