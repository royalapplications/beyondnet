import XCTest
import BeyondDotNETSampleKit

public class DNArray<T>: /* System_Array, */ MutableCollection where T: System_Object {
    public typealias Element = T?
    public typealias Index = Int32
    
    public let systemArray: System_Array
    
    init(systemArray: System_Array) {
        self.systemArray = systemArray
    }
    
    public convenience init() throws {
        let elementType = T.typeOf
        let arr = try System_Array.createInstance(elementType, 0)
        
        self.init(systemArray: arr)
    }
    
    public convenience init(length: Index) throws {
        let elementType = T.typeOf
        
        let arr = try System_Array.createInstance(elementType,
                                                  length)
        
        self.init(systemArray: arr)
    }
    
    public var startIndex: Index {
        0
    }
    
    public var endIndex: Index {
        let length: Int32
        
        do {
            length = try systemArray.length
        } catch {
            fatalError("An exception was thrown while calling System.Array.Length: \(error.localizedDescription)")
        }
        
        guard length > 0 else {
            return 0
        }
        
        return length
    }
    
    public func index(after i: Index) -> Index {
        i + 1
    }
    
    public func index(before i: Index) -> Index {
        i - 1
    }
    
    public subscript(position: Index) -> Element {
        get {
            assert(position >= startIndex && position < endIndex, "Out of bounds")
            
            do {
                guard let element = try systemArray.getValue(position) else {
                    return nil
                }
                
                return try element.castTo()
            } catch {
                fatalError("An exception was thrown while calling System.Array.GetValue: \(error.localizedDescription)")
            }
        }
        set {
            assert(position >= startIndex && position < endIndex, "Out of bounds")

            do {
                try systemArray.setValue(newValue, position)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }
}

final class SystemArrayTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testGenericArray() throws {
        let now = try System_DateTime.now
        
        let arrayOfDateTime = try DNArray<System_DateTime>(length: 1)
        
        let index: Int32 = 0
        
        arrayOfDateTime[index] = now
        let retrievedNow = arrayOfDateTime[index]
        
        let equals = now == retrievedNow
        XCTAssertTrue(equals)
    }
    
    func testSystemArray() throws {
        let now = try System_DateTime.now
        let dateTimeType = try now.getType()
        
        let arrayLength: Int32 = 1
        
        let arrayOfDateTime = try System_Array.createInstance(dateTimeType,
                                                              arrayLength)
        
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
		
		let iList = try arrayOfDateTime.castTo(System_Collections_IList.self)
		
		let index: Int32 = 0
		
		try iList.item_set(index, now)
		
		let retrievedNow = try iList.item(index)
		
		let equals = now == retrievedNow
		XCTAssertTrue(equals)
	}
    
    func testEmptyArrayWithExtensionExplicitElementType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try System_String_Array(length: 0)
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
        
        let systemStringArray = try emptyArrayOfStrings.castTo(System_String_Array.self)
        XCTAssertEqual(systemStringArray, emptyArrayOfStrings)
    }
    
    func testEmptyArrayWithExtensionOnExplicitArrayType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try System_String_Array()
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
        
        let systemStringArray = try emptyArrayOfStrings.castTo(System_String_Array.self)
        XCTAssertEqual(systemStringArray, emptyArrayOfStrings)
    }
    
    func testEmptyArrayWithInitializerOnExplicitArrayType() throws {
        let systemStringType = System_String.typeOf
        
        let emptyArrayOfStrings = try System_String_Array()
        
        let length = try emptyArrayOfStrings.length
        XCTAssertEqual(0, .init(length))

        let arrayType = try emptyArrayOfStrings.getType()

        let isArray = try arrayType.isArray
        XCTAssertTrue(isArray)

        let arrayElementType = try arrayType.getElementType()

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
        
        let systemStringArray = try emptyArrayOfStrings.castTo(System_String_Array.self)
        XCTAssertEqual(systemStringArray, emptyArrayOfStrings)
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

        let arrayOfString = try System_String_Array(length: numberOfElements)

        let string = "Abc"
        let stringDN = string.dotNETString()
        
        try System_Array.fill(T: systemStringType,
                              arrayOfString,
                              stringDN)

        let length = try arrayOfString.length
        XCTAssertEqual(numberOfElements, length)

        for idx in 0..<length {
            let stringElement = try arrayOfString.getValue(idx)?.castAs(System_String.self)?.string()

            XCTAssertEqual(string, stringElement)
        }
    }
    
    func testReverseArrayWithGenerics() throws {
        let systemStringType = System_String.typeOf

        let strings = [
            "1",
            "2"
        ]

        let numberOfElements: Int32 = .init(strings.count)

        let arrayOfString = try System_String_Array(length: numberOfElements)

        for (idx, string) in strings.enumerated() {
            let stringDN = string.dotNETString()
            
            try arrayOfString.setValue(stringDN, Int32(idx))
        }
        
        try System_Array.reverse(T: systemStringType,
                                 arrayOfString)

        let reversedStrings = [String](strings.reversed())

        for idx in 0..<numberOfElements {
            let stringElement = arrayOfString[idx]?.string()

            let expectedString = reversedStrings[.init(idx)]

            XCTAssertEqual(expectedString, stringElement)
        }
    }
	
	func testSystemArrayIterator() throws {
		let length: Int32 = 10
		
        let arrayOfInt32 = try System_Int32_Array(length: length)
		
		var int32s = [Int32]()
		
		for idx in 0..<length {
			let randomInt32 = Int32.random(in: Int32.min..<Int32.max)
			let randomInt32Obj = randomInt32.dotNETObject()
			
			try arrayOfInt32.setValue(randomInt32Obj, idx)
			
			int32s.append(randomInt32)
		}
        
		for (idx, int32Obj) in arrayOfInt32.enumerated() {
			guard let int32Obj else {
				XCTFail("Failed to retrieve object from array")
				
				return
			}
			
			guard let int32 = try? int32Obj.castToInt32() else {
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
		
        let arrayOfString = try System_String_Array(length: .init(strings.count))
        
		for (idx, string) in strings.enumerated() {
			try arrayOfString.setValue(string.dotNETString(), Int32(idx))
		}
		
		guard let firstObject = arrayOfString[0],
			  let firstString = firstObject.castAs(System_String.self)?.string() else {
			XCTFail("Failed to retrieve first object or cast as string")
			
			return
		}
		
		XCTAssertEqual(strings[0], firstString)
		
		guard let secondObject = arrayOfString[1],
			  let secondString = secondObject.castAs(System_String.self)?.string() else {
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
        
        let arrayOfString = try System.String_Array(length: .init(strings.count))
        
        for (idx, string) in strings.enumerated() {
            arrayOfString[.init(idx)] = string.dotNETString()
        }
        
        for (idx, stringDN) in arrayOfString.enumerated() {
            guard let stringDN else {
                XCTFail("Failed to get .NET String from Array")
                
                return
            }
            
            let convertedString = stringDN.string()
            let originalString = strings[idx]
            
            XCTAssertEqual(convertedString, originalString)
        }
        
        let newStringAtIdxOne = "New World"
        
        arrayOfString[1] = newStringAtIdxOne.dotNETString()
        
        XCTAssertEqual(arrayOfString[1]?.castAs(System.String.self)?.string(), newStringAtIdxOne)
    }
    
    func testIteratingArrayPerformance() throws {
        let count: Int32 = 10_000
        
        var values = [System.Object]()
        
        let systemArray = try System.Object_Array(length: count)
        
        for idx in 0..<count {
            let obj = try System.Object()
            
            values.append(obj)
            try systemArray.setValue(obj, idx)
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
