import XCTest
import BeyondDotNETSampleKit

final class PrimitivesBoxingTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testBool() {
        boxAndUnbox(value: true,
                    expectedTypeName: System.Boolean.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToBool() })
        
        boxAndUnbox(value: false,
                    expectedTypeName: System.Boolean.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToBool() })
    }
    
    func testFloat() {
        boxAndUnbox(value: -123.123 as Float,
                    expectedTypeName: System.Single.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToFloat() })
    }

    func testDouble() {
        boxAndUnbox(value: -123456789.123456789 as Double,
                    expectedTypeName: System.Double.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToDouble() })
    }

    func testInt8() {
        boxAndUnbox(value: -123 as Int8,
                    expectedTypeName: System.SByte.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt8() })
    }

    func testUInt8() {
        boxAndUnbox(value: 123 as UInt8,
                    expectedTypeName: System.Byte.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt8() })
    }

    func testInt16() {
        boxAndUnbox(value: -1234 as Int16,
                    expectedTypeName: System.Int16.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt16() })
    }

    func testUInt16() {
        boxAndUnbox(value: 1234 as UInt16,
                    expectedTypeName: System.UInt16.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt16() })
    }

    func testInt32() {
        boxAndUnbox(value: -123456789 as Int32,
                    expectedTypeName: System.Int32.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt32() })
    }

    func testUInt32() {
        boxAndUnbox(value: 123456789 as UInt32,
                    expectedTypeName: System.UInt32.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt32() })
    }

    func testInt64() {
        boxAndUnbox(value: -123456789123456789 as Int64,
                    expectedTypeName: System.Int64.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt64() })
    }

    func testUInt64() {
        boxAndUnbox(value: 123456789123456789 as UInt64,
                    expectedTypeName: System.UInt64.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt64() })
    }
}

private extension PrimitivesBoxingTests {
    func boxAndUnbox<T>(value: T,
                        expectedTypeName: String,
                        boxFunc: (_ input: T) -> System.Object?,
                        unboxFunc: (_ input: System.Object) throws -> T?) where T: Equatable {
        let valueTypeName = expectedTypeName.dotNETString()
        
        guard let valueType = try? System_Type.getType(valueTypeName) else {
            XCTFail("System.Type.GetType should not throw and return an instance")
            
            return
        }
        
        guard let valueObject = boxFunc(value) else {
            XCTFail("Should return an instance")
            
            return
        }
        
        guard let valueObjectType = try? valueObject.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard let numberObjectTypeName = try? valueObjectType.fullName?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedTypeName, numberObjectTypeName)
        
        let valueRet: T?
        
        do {
            valueRet = try unboxFunc(valueObject)
        } catch {
            XCTFail("Should not throw")
            
            return
        }
        
        XCTAssertEqual(value, valueRet)
        
        guard let systemObject = try? System.Object() else {
            XCTFail("System.Object ctor should return an instance")
            
            return
        }
        
        do {
            _ = try unboxFunc(systemObject)
            
            XCTFail("Should throw")
            
            return
        } catch { }
        
        let arrayLength: Int32 = 1
        
        guard let array = try? System.Array.createInstance(valueType,
                                                           arrayLength) else {
            XCTFail("System.Array.CreateInstance should not throw and return an instance")
            
            return
        }
        
        XCTAssertNoThrow(try array.setValue(valueObject,
                                            0 as Int32))
        
        guard let valueObjectRetFromArray = try? array.getValue(0 as Int32) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        let equal = valueObject == valueObjectRetFromArray
        
        XCTAssertTrue(equal)
        
        do {
            let valueRetFromArray = try unboxFunc(valueObjectRetFromArray)
            
            XCTAssertEqual(value, valueRetFromArray)
        } catch {
            XCTFail("Should not throw")
            
            return
        }
    }
}
