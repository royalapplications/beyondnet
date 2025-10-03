import XCTest
import BeyondDotNETSampleKit

final class PrimitivesTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }

    override class func tearDown() {
        Self.sharedTearDown()
    }

    func testBoolBoxing() {
        boxAndUnbox(value: true,
                    expectedTypeName: System.Boolean.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToBool() })

        boxAndUnbox(value: false,
                    expectedTypeName: System.Boolean.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToBool() })
    }

    func testFloatBoxing() {
        boxAndUnbox(value: -123.123 as Float,
                    expectedTypeName: System.Single.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToFloat() })
    }

    func testDoubleBoxing() {
        boxAndUnbox(value: -123456789.123456789 as Double,
                    expectedTypeName: System.Double.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToDouble() })
    }

    func testInt8Boxing() {
        boxAndUnbox(value: -123 as Int8,
                    expectedTypeName: System.SByte.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt8() })
    }

    func testUInt8Boxing() {
        boxAndUnbox(value: 123 as UInt8,
                    expectedTypeName: System.Byte.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt8() })
    }

    func testInt16Boxing() {
        boxAndUnbox(value: -1234 as Int16,
                    expectedTypeName: System.Int16.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt16() })
    }

    func testUInt16Boxing() {
        boxAndUnbox(value: 1234 as UInt16,
                    expectedTypeName: System.UInt16.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt16() })
    }

    func testInt32Boxing() {
        boxAndUnbox(value: -123456789 as Int32,
                    expectedTypeName: System.Int32.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt32() })
    }

    func testUInt32Boxing() {
        boxAndUnbox(value: 123456789 as UInt32,
                    expectedTypeName: System.UInt32.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt32() })
    }

    func testInt64Boxing() {
        boxAndUnbox(value: -123456789123456789 as Int64,
                    expectedTypeName: System.Int64.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToInt64() })
    }

    func testUInt64Boxing() {
        boxAndUnbox(value: 123456789123456789 as UInt64,
                    expectedTypeName: System.UInt64.fullTypeName,
					boxFunc: { $0.dotNETObject() },
                    unboxFunc: { try $0.castToUInt64() })
    }
    
    func testDoubleExtensions() throws {
        func check(input: Double, expected: Int32) throws {
            let result = try input.roundToInt()
            XCTAssertEqual(expected, result)
        }
        
        try check(input: 1.1, expected: 1)
        try check(input: 1.6, expected: 2)
        try check(input: 999.4, expected: 999)
        try check(input: 9999.9, expected: 10_000)
    }
    
    func testFloatExtensions() throws {
        func check(input: Float, expected: Int32) throws {
            let result = try input.roundToInt()
            XCTAssertEqual(expected, result)
        }
        
        try check(input: 1.1, expected: 1)
        try check(input: 1.6, expected: 2)
        try check(input: 999.4, expected: 999)
        try check(input: 9999.9, expected: 10_000)
    }
}

private extension PrimitivesTests {
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
