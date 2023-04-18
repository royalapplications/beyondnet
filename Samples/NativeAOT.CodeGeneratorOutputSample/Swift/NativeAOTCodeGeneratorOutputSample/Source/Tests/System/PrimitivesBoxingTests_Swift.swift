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
        let systemBooleanTypeName = "System.Boolean"
        
        let trueValue = true
        let trueObject = System_Object.fromBool(trueValue)
        let trueType = try? trueObject.getType()
        let trueTypeName = try? trueType?.fullName_get()?.string()
        
        XCTAssertEqual(systemBooleanTypeName, trueTypeName)
        
        let trueValueRet = try? trueObject.castToBool()
        
        XCTAssertEqual(trueValue, trueValueRet)
    }
    
    func testFloat() {
        let systemFloatTypeName = "System.Single"
        
        let floatValue: Float = 0.1234
        let floatObject = System_Object.fromFloat(floatValue)
        let floatType = try? floatObject.getType()
        let floatTypeName = try? floatType?.fullName_get()?.string()
        
        XCTAssertEqual(systemFloatTypeName, floatTypeName)
        
        let floatValueRet = try? floatObject.castToFloat()
        
        XCTAssertEqual(floatValue, floatValueRet)
    }

    // TODO
//    func testBool() {
//        boxAndUnbox(value: true,
//                    expectedTypeName: "System.Boolean",
//                    boxFunc: { DNObjectFromBool($0) },
//                    unboxFunc: { DNObjectCastToBool($0, &$1) })
//
//        boxAndUnbox(value: false,
//                    expectedTypeName: "System.Boolean",
//                    boxFunc: { DNObjectFromBool($0) },
//                    unboxFunc: { DNObjectCastToBool($0, &$1) })
//    }
//
//    func testFloat() {
//        boxAndUnbox(value: -123.123 as Float,
//                    expectedTypeName: "System.Single",
//                    boxFunc: { DNObjectFromFloat($0) },
//                    unboxFunc: { DNObjectCastToFloat($0, &$1) })
//    }
//
//    func testDouble() {
//        boxAndUnbox(value: -123456789.123456789 as Double,
//                    expectedTypeName: "System.Double",
//                    boxFunc: { DNObjectFromDouble($0) },
//                    unboxFunc: { DNObjectCastToDouble($0, &$1) })
//    }
//
//    func testInt8() {
//        boxAndUnbox(value: -123 as Int8,
//                    expectedTypeName: "System.SByte",
//                    boxFunc: { DNObjectFromInt8($0) },
//                    unboxFunc: { DNObjectCastToInt8($0, &$1) })
//    }
//
//    func testUInt8() {
//        boxAndUnbox(value: 123 as UInt8,
//                    expectedTypeName: "System.Byte",
//                    boxFunc: { DNObjectFromUInt8($0) },
//                    unboxFunc: { DNObjectCastToUInt8($0, &$1) })
//    }
//
//    func testInt16() {
//        boxAndUnbox(value: -1234 as Int16,
//                    expectedTypeName: "System.Int16",
//                    boxFunc: { DNObjectFromInt16($0) },
//                    unboxFunc: { DNObjectCastToInt16($0, &$1) })
//    }
//
//    func testUInt16() {
//        boxAndUnbox(value: 1234 as UInt16,
//                    expectedTypeName: "System.UInt16",
//                    boxFunc: { DNObjectFromUInt16($0) },
//                    unboxFunc: { DNObjectCastToUInt16($0, &$1) })
//    }
//
//    func testInt32() {
//        boxAndUnbox(value: -123456789 as Int32,
//                    expectedTypeName: "System.Int32",
//                    boxFunc: { DNObjectFromInt32($0) },
//                    unboxFunc: { DNObjectCastToInt32($0, &$1) })
//    }
//
//    func testUInt32() {
//        boxAndUnbox(value: 123456789 as UInt32,
//                    expectedTypeName: "System.UInt32",
//                    boxFunc: { DNObjectFromUInt32($0) },
//                    unboxFunc: { DNObjectCastToUInt32($0, &$1) })
//    }
//
//    func testInt64() {
//        boxAndUnbox(value: -123456789123456789 as Int64,
//                    expectedTypeName: "System.Int64",
//                    boxFunc: { DNObjectFromInt64($0) },
//                    unboxFunc: { DNObjectCastToInt64($0, &$1) })
//    }
//
//    func testUInt64() {
//        boxAndUnbox(value: 123456789123456789 as UInt64,
//                    expectedTypeName: "System.UInt64",
//                    boxFunc: { DNObjectFromUInt64($0) },
//                    unboxFunc: { DNObjectCastToUInt64($0, &$1) })
//    }
//}
//
//private extension PrimitivesBoxingTests {
//    func boxAndUnbox<T>(value: T,
//                        expectedTypeName: String,
//                        boxFunc: (_ input: T) -> System_Object?,
//                        unboxFunc: (_ input: System_Object) throws -> T?) where T: Equatable {
//        var exception: System_Exception_t?
//
//        let valueTypeName = expectedTypeName.cDotNETString()
//
//        defer { System_String_Destroy(valueTypeName) }
//
//        guard let valueType = System_Type_GetType_2(valueTypeName,
//                                                    &exception),
//              exception == nil else {
//            XCTFail("System.Type.GetType should not throw and return an instance")
//
//            return
//        }
//
//        guard let valueObject = boxFunc(value) else {
//            XCTFail("Should return an instance")
//
//            return
//        }
//
//        defer { System_Object_Destroy(valueObject) }
//
//        guard let valueObjectType = System_Object_GetType(valueObject,
//                                                           &exception),
//              exception == nil else {
//            XCTFail("System.Object.GetType should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Type_Destroy(valueObjectType) }
//
//        guard let numberObjectTypeName = String(cDotNETString: System_Type_FullName_Get(valueObjectType,
//                                                                                       &exception),
//                                                destroyDotNETString: true),
//              exception == nil else {
//            XCTFail("System.Type.FullName getter should not throw and return an instance")
//
//            return
//        }
//
//        XCTAssertEqual(expectedTypeName, numberObjectTypeName)
//
//        let valueRet = unboxFunc(valueObject,
//                                 &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertEqual(value, valueRet)
//
//        guard let systemObject = System_Object_Create(&exception),
//              exception == nil else {
//            XCTFail("System.Object ctor should return an instance")
//
//            return
//        }
//
//        defer { System_Object_Destroy(systemObject) }
//
//        _ = unboxFunc(systemObject,
//                      &exception)
//
//        XCTAssertNotNil(exception)
//        System_Exception_Destroy(exception)
//
//        let arrayLength: Int32 = 1
//
//        guard let array = System_Array_CreateInstance(valueType,
//                                                      arrayLength,
//                                                      &exception),
//              exception == nil else {
//            XCTFail("System.Array.CreateInstance should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Array_Destroy(array) }
//
//        System_Array_SetValue_4(array,
//                                valueObject,
//                                0,
//                                &exception)
//
//        XCTAssertNil(exception)
//
//        guard let valueObjectRetFromArray = System_Array_GetValue_1(array,
//                                                                    0,
//                                                                    &exception),
//              exception == nil else {
//            XCTFail("System.Array.GetValue should not throw and return an instance")
//
//            return
//        }
//
//        defer { System_Object_Destroy(valueObjectRetFromArray) }
//
//        let equal = System_Object_Equals(valueObject,
//                                         valueObjectRetFromArray,
//                                         &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertTrue(equal)
//
//        let valueRetFromArray = unboxFunc(valueObjectRetFromArray,
//                                          &exception)
//
//        XCTAssertNil(exception)
//        XCTAssertEqual(value, valueRetFromArray)
//    }
}
