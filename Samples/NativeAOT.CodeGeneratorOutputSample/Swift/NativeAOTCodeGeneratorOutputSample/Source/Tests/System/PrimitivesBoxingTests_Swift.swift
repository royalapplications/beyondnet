import XCTest
import NativeAOTCodeGeneratorOutputSample

final class PrimitivesBoxingTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testBool() {
        boxAndUnbox(value: true,
                    expectedTypeName: "System.Boolean",
                    boxFunc: { System_Object.fromBool($0) },
                    unboxFunc: { try $0.castToBool() })
        
        boxAndUnbox(value: false,
                    expectedTypeName: "System.Boolean",
                    boxFunc: { System_Object.fromBool($0) },
                    unboxFunc: { try $0.castToBool() })
    }
    
    func testFloat() {
        boxAndUnbox(value: -123.123 as Float,
                    expectedTypeName: "System.Single",
                    boxFunc: { System_Object.fromFloat($0) },
                    unboxFunc: { try $0.castToFloat() })
    }

    func testDouble() {
        boxAndUnbox(value: -123456789.123456789 as Double,
                    expectedTypeName: "System.Double",
                    boxFunc: { System_Object.fromDouble($0) },
                    unboxFunc: { try $0.castToDouble() })
    }

    func testInt8() {
        boxAndUnbox(value: -123 as Int8,
                    expectedTypeName: "System.SByte",
                    boxFunc: { System_Object.fromInt8($0) },
                    unboxFunc: { try $0.castToInt8() })
    }

    func testUInt8() {
        boxAndUnbox(value: 123 as UInt8,
                    expectedTypeName: "System.Byte",
                    boxFunc: { System_Object.fromUInt8($0) },
                    unboxFunc: { try $0.castToUInt8() })
    }

    func testInt16() {
        boxAndUnbox(value: -1234 as Int16,
                    expectedTypeName: "System.Int16",
                    boxFunc: { System_Object.fromInt16($0) },
                    unboxFunc: { try $0.castToInt16() })
    }

    func testUInt16() {
        boxAndUnbox(value: 1234 as UInt16,
                    expectedTypeName: "System.UInt16",
                    boxFunc: { System_Object.fromUInt16($0) },
                    unboxFunc: { try $0.castToUInt16() })
    }

    func testInt32() {
        boxAndUnbox(value: -123456789 as Int32,
                    expectedTypeName: "System.Int32",
                    boxFunc: { System_Object.fromInt32($0) },
                    unboxFunc: { try $0.castToInt32() })
    }

    func testUInt32() {
        boxAndUnbox(value: 123456789 as UInt32,
                    expectedTypeName: "System.UInt32",
                    boxFunc: { System_Object.fromUInt32($0) },
                    unboxFunc: { try $0.castToUInt32() })
    }

    func testInt64() {
        boxAndUnbox(value: -123456789123456789 as Int64,
                    expectedTypeName: "System.Int64",
                    boxFunc: { System_Object.fromInt64($0) },
                    unboxFunc: { try $0.castToInt64() })
    }

    func testUInt64() {
        boxAndUnbox(value: 123456789123456789 as UInt64,
                    expectedTypeName: "System.UInt64",
                    boxFunc: { System_Object.fromUInt64($0) },
                    unboxFunc: { try $0.castToUInt64() })
    }
}

private extension PrimitivesBoxingTests_Swift {
    func boxAndUnbox<T>(value: T,
                        expectedTypeName: String,
                        boxFunc: (_ input: T) -> System_Object?,
                        unboxFunc: (_ input: System_Object) throws -> T?) where T: Equatable {
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
        
        guard let numberObjectTypeName = try? valueObjectType.fullName_get()?.string() else {
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
        
        guard let systemObject = try? System_Object() else {
            XCTFail("System.Object ctor should return an instance")
            
            return
        }
        
        do {
            _ = try unboxFunc(systemObject)
            
            XCTFail("Should throw")
            
            return
        } catch { }
        
        let arrayLength: Int32 = 1
        
        guard let array = try? System_Array.createInstance(valueType,
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