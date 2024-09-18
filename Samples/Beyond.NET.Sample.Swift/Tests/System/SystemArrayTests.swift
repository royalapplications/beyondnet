import XCTest
import BeyondDotNETSampleKit

final class SystemArrayTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testGenericArray() throws {
        let now = try System_DateTime.now
        
        let arrayOfDateTime = try DNArray<System_DateTime>(length: 1)
        
        let expectedType = DNArray<System_DateTime>.typeOf
        let type = try arrayOfDateTime.getType()
        XCTAssertEqual(type, expectedType)
        
        let typeName = DNArray<System_DateTime>.typeName
        XCTAssertEqual(typeName, "DateTime[]")
        
        let fullTypeName = DNArray<System_DateTime>.fullTypeName
        XCTAssertEqual(fullTypeName, "System.DateTime[]")
        
        let rank = try arrayOfDateTime.rank
        XCTAssertEqual(rank, 1)
        
        let index: Int32 = 0
        
        arrayOfDateTime[index] = now
        let retrievedNow = arrayOfDateTime[index]
        
        let equals = now == retrievedNow
        XCTAssertTrue(equals)
        
        XCTAssertThrowsError(try arrayOfDateTime.setValue("A System.String is not a System.DateTime".dotNETString(), index))
    }
    
    func testSystemArray() throws {
        let now = try System_DateTime.now
        let dateTimeType = try now.getType()
        
        let arrayLength: Int32 = 1
        
        let arrayOfDateTime = try System_Array.createInstance(dateTimeType,
                                                              arrayLength)
        
        let rank = try arrayOfDateTime.rank
        XCTAssertEqual(rank, 1)
        
        let index: Int32 = 0
        
        try arrayOfDateTime.setValue(now, index)
        
        let retrievedNow = try arrayOfDateTime.getValue(index)
        
        let equals = now == retrievedNow
        XCTAssertTrue(equals)
    }
	
	func testSystemArrayConvertedToIList() throws {
		let now = try System_DateTime.now
		let dateTimeType = try now.getType()
		
		let arrayLength: Int32 = 1
		
        let arrayOfDateTime = try System_Array.createInstance(dateTimeType,
                                                              arrayLength)
		
		let index: Int32 = 0
		
		try arrayOfDateTime.item_set(index, now)
		
		let retrievedNow = try arrayOfDateTime.item(index)
		
		let equals = now == retrievedNow
		XCTAssertTrue(equals)
	}
    
    func testEmptyArrayWithExtensionExplicitElementType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try DNArray<System_String>.empty
        
        let expectedType = DNArray<System_String>.typeOf
        let type = try emptyArrayOfStrings.getType()
        XCTAssertEqual(type, expectedType)
        
        let typeName = DNArray<System_String>.typeName
        XCTAssertEqual(typeName, "String[]")
        
        let fullTypeName = DNArray<System_String>.fullTypeName
        XCTAssertEqual(fullTypeName, "System.String[]")
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
        
        let rank = try emptyArrayOfStrings.rank
        XCTAssertEqual(rank, 1)
    }
    
    func testEmptyArrayWithExtensionOnExplicitArrayType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try DNArray<System_String>.empty
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
    }
    
    func testEmptyArrayWithInitializerOnExplicitArrayType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try DNArray<System_String>.empty
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
    }
    
    func testEmptyArrayWithGenerics() throws {
        let systemStringType = System_String.typeOf

        let emptyArrayOfString = try System_Array.empty(T: systemStringType)

        let length = try emptyArrayOfString.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfString.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
    }
    
    func testFillArrayWithGenerics() throws {
        let systemStringType = System_String.typeOf

        let numberOfElements: Int32 = 5

        let arrayOfString = try DNArray<System_String>(length: numberOfElements)

        let string = "Abc"
        let stringDN = string.dotNETString()
        
        try System_Array.fill(T: systemStringType,
                              arrayOfString,
                              stringDN)

        let length = try arrayOfString.length
        XCTAssertEqual(numberOfElements, length)

        for stringElement in arrayOfString {
            XCTAssertEqual(string, stringElement.string())
        }
    }
    
    func testReverseArrayWithGenerics() throws {
        let systemStringType = System_String.typeOf

        let strings = [
            "1",
            "2"
        ]

        let numberOfElements: Int32 = .init(strings.count)

        let arrayOfString = try DNArray<System_String>(length: numberOfElements)

        for (idx, string) in strings.enumerated() {
            let stringDN = string.dotNETString()
            
            arrayOfString[Int32(idx)] = stringDN
        }
        
        try System_Array.reverse(T: systemStringType,
                                 arrayOfString)

        let reversedStrings = [String](strings.reversed())

        for idx in 0..<numberOfElements {
            let stringElement = arrayOfString[idx].string()

            let expectedString = reversedStrings[.init(idx)]

            XCTAssertEqual(expectedString, stringElement)
        }
    }
	
	func testSystemArrayIterator() throws {
		let length: Int32 = 10
		
        let arrayOfInt32 = try DNArray<System_Int32>(length: length)
		
		var int32s = [Int32]()
		
		for idx in 0..<length {
			let randomInt32 = Int32.random(in: Int32.min..<Int32.max)
            let randomInt32Obj = randomInt32.dotNETObject()
			
            arrayOfInt32[idx] = randomInt32Obj
			
			int32s.append(randomInt32)
		}
        
		for (idx, int32Obj) in arrayOfInt32.enumerated() {
			guard let int32 = try? int32Obj.value else {
				XCTFail("Object in array is not a Int32")
				
				return
			}
			
			XCTAssertEqual(int32, int32s[idx])
		}
	}
	
	func testSystemArraySubscript() throws {
		let strings = [
			"Hello",
			"World"
		]
		
        let arrayOfString = try DNArray<System_String>(length: .init(strings.count))
        
		for (idx, string) in strings.enumerated() {
			try arrayOfString.setValue(string.dotNETString(), Int32(idx))
		}
        
        let firstObject = arrayOfString[0]
		
		guard let firstString = firstObject.castAs(System_String.self)?.string() else {
			XCTFail("Failed to retrieve first object or cast as string")
			
			return
		}
		
		XCTAssertEqual(strings[0], firstString)
		
        let secondObject = arrayOfString[1]
        
		guard let secondString = secondObject.castAs(System_String.self)?.string() else {
			XCTFail("Failed to retrieve second object or cast as string")
			
			return
		}
		
		XCTAssertEqual(strings[1], secondString)
	}
    
    func testMutatingSystemArray() throws {
        let strings = [
            "Hello",
            "World"
        ]
        
        let arrayOfString = try DNArray<System.String>(length: .init(strings.count))
        
        for (idx, string) in strings.enumerated() {
            arrayOfString[.init(idx)] = string.dotNETString()
        }
        
        for (idx, stringDN) in arrayOfString.enumerated() {
            let convertedString = stringDN.string()
            let originalString = strings[idx]
            
            XCTAssertEqual(convertedString, originalString)
        }
        
        let newStringAtIdxOne = "New World"
        
        arrayOfString[1] = newStringAtIdxOne.dotNETString()
        
        XCTAssertEqual(arrayOfString[1].castAs(System.String.self)?.string(), newStringAtIdxOne)
    }
    
    func testIteratingArrayPerformance() throws {
        let count: Int32 = 10_000
        
        var values = [System.Object]()
        
        let systemArray = try DNArray<System.Object>(length: count)
        
        for idx in 0..<count {
            let obj = try System.Object()
            
            values.append(obj)
            systemArray[idx] = obj
        }
        
        XCTAssertEqual(systemArray.count, values.count)
        
        for (idx, obj) in systemArray.enumerated() {
            let expectedObj = values[idx]
            
            XCTAssertTrue(obj === expectedObj)
        }
        
        measure {
            for obj in systemArray {
                // Aka PleaseDontOptimizeThisOut
                let _ = obj
            }
        }
    }
}
