import XCTest
import BeyondNETSampleSwift

final class SystemArrayTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemArray() {
        guard let now = try? System_DateTime.now else {
            XCTFail("System.DateTime.Now should not throw and return an instance")
            
            return
        }
        
        guard let dateTimeType = try? now.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        let arrayLength: Int32 = 1
        
        guard let arrayOfDateTime = try? System_Array.createInstance(dateTimeType,
                                                                     arrayLength) else {
            XCTFail("System.Array.CreateInstance should not fail and return an instance")
            
            return
        }
        
        let index: Int32 = 0
        
        XCTAssertNoThrow(try arrayOfDateTime.setValue(now,
                                                      index))
        
        guard let retrievedNow = try? arrayOfDateTime.getValue(index) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        let equals = now == retrievedNow
        XCTAssertTrue(equals)
    }
	
	func testSystemArrayConvertedToIList() {
		guard let now = try? System_DateTime.now else {
			XCTFail("System.DateTime.Now should not throw and return an instance")
			
			return
		}
		
		guard let dateTimeType = try? now.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		let arrayLength: Int32 = 1
		
		guard let arrayOfDateTime = try? System_Array.createInstance(dateTimeType,
																	 arrayLength) else {
			XCTFail("System.Array.CreateInstance should not fail and return an instance")
			
			return
		}
		
		guard let iList = try? arrayOfDateTime.castTo(System_Collections_IList.self) else {
			XCTFail("Failed to cast System.Array to System.Collections.IList")
			
			return
		}
		
		let index: Int32 = 0
		
		XCTAssertNoThrow(try iList.item_set(index,
											now))
		
		guard let retrievedNow = try? iList.item(index) else {
			XCTFail("System.Collections.IList[] should not throw and return an instance")
			
			return
		}
		
		let equals = now == retrievedNow
		XCTAssertTrue(equals)
	}
    
    func testEmptyArrayWithGenerics() {
        let systemStringType = System_String.typeOf

        guard let emptyArrayOfString = try? System_Array.empty(systemStringType) else {
            XCTFail("System.Array<System.String>.Empty should not throw and return an instance")

            return
        }

        let length = (try? emptyArrayOfString.length) ?? -1
        XCTAssertEqual(0, .init(length))

        guard let arrayType = try? emptyArrayOfString.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")

            return
        }

        let isArray = (try? arrayType.isArray) ?? false
        XCTAssertTrue(isArray)

        guard let arrayElementType = try? arrayType.getElementType() else {
            XCTFail("System.Type.GetElementType should not throw and return an instance")

            return
        }

        let arrayElementTypeIsSystemString = arrayElementType == systemStringType
        XCTAssertTrue(arrayElementTypeIsSystemString)
    }
    
    func testFillArrayWithGenerics() {
        let systemStringType = System_String.typeOf

        let numberOfElements: Int32 = 5

        guard let arrayOfString = try? System_Array.createInstance(systemStringType,
                                                                   numberOfElements) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")

            return
        }

        let string = "Abc"
        let stringDN = string.dotNETString()
        
        XCTAssertNoThrow(try System_Array.fill(systemStringType,
                                               arrayOfString,
                                               stringDN))

        let length = (try? arrayOfString.length) ?? -1
        XCTAssertEqual(numberOfElements, length)

        for idx in 0..<length {
            guard let stringElement = try? arrayOfString.getValue(idx)?.castAs(System_String.self)?.string() else {
                XCTFail("System.Array.GetValue should not throw and return an instance")

                return
            }

            XCTAssertEqual(string, stringElement)
        }
    }
    
    func testReverseArrayWithGenerics() {
        let systemStringType = System_String.typeOf

        let strings = [
            "1",
            "2"
        ]

        let numberOfElements: Int32 = .init(strings.count)

        guard let arrayOfString = try? System_Array.createInstance(systemStringType,
                                                                   numberOfElements) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")

            return
        }

        for (idx, string) in strings.enumerated() {
            let stringDN = string.dotNETString()
            
            XCTAssertNoThrow(try arrayOfString.setValue(stringDN,
                                                        Int32(idx)))
        }
        
        XCTAssertNoThrow(try System_Array.reverse(systemStringType,
                                                  arrayOfString))

        let reversedStrings = [String](strings.reversed())

        for idx in 0..<numberOfElements {
            guard let stringElement = try? arrayOfString.getValue(idx)?.castAs(System_String.self)?.string() else {
                XCTFail("System.Array.GetValue should not throw and return an instance")

                return
            }

            let expectedString = reversedStrings[.init(idx)]

            XCTAssertEqual(expectedString, stringElement)
        }
    }
	
	func testSystemArrayIterator() {
		let length: Int32 = 10
		
		guard let int32Type = try? System_Type.getType("System.Int32".dotNETString()) else {
			XCTFail("System.Type.GetType should not throw and return an instance")
			
			return
		}
		
		guard let arrayOfInt32 = try? System_Array.createInstance(int32Type,
																  length) else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		var int32s = [Int32]()
		
		for idx in 0..<length {
			let randomInt32 = Int32.random(in: Int32.min..<Int32.max)
			let randomInt32Obj = randomInt32.dotNETObject()
			
			XCTAssertNoThrow(try arrayOfInt32.setValue(randomInt32Obj, idx))
			
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
	
	func testSystemArraySubscript() {
		let systemStringType = System_String.typeOf
		
		let strings = [
			"Hello",
			"World"
		]
		
		guard let arrayOfString = try? System_Array.createInstance(systemStringType,
																   Int32(strings.count)) else {
			XCTFail("System.Array.CreateInstance should not throw and return an instance")
			
			return
		}
		
		for (idx, string) in strings.enumerated() {
			XCTAssertNoThrow(try arrayOfString.setValue(string.dotNETString(), Int32(idx)))
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
}
