import XCTest
import BeyondDotNETSampleKit

/*
/// This is a generic base class for all .NET array types.
/// The element type of the array is specified as the first and only generic type argument.
public class DNArray<T>: System_Array, MutableCollection where T: System_Object {
    public typealias Element = T?
    public typealias Index = Int32
    
    public override class var typeOf: System_Type /* System.Type */ {
        do {
            let elementType = T.typeOf
            let arrayType = try elementType.makeArrayType()
            
            return arrayType
        } catch {
            return super.typeOf
        }
    }
    
    public override class var typeName: String {
        "\(T.typeName)[]"
    }
    
    public override class var fullTypeName: String {
        "\(T.fullTypeName)[]"
    }
    
    /// - Returns: An empty .NET array of the specified type.
    public static var empty: DNArray<T> { get throws {
        try DNArray<T>.init()
    }}
    
    /// Creates and initializes an empty .NET array of the specified type.
    public convenience init() throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, 0, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Creates and initializes a .NET array of the specified type and length.
    public convenience init(length: Index) throws {
        let elementType = T.typeOf
        let elementTypeC = elementType.__handle
        
        var __exceptionC: System_Exception_t?
        
        let newArrayC = System_Array_CreateInstance(elementTypeC, length, &__exceptionC)
        
        if let __exceptionC {
            let __exception = System_Exception(handle: __exceptionC)
            let __error = __exception.error
            
            throw __error
        }
        
        self.init(handle: newArrayC)
    }
    
    /// Get or set and element of this .NET array at the specified position/index.
    /// If an exception is raised on the .NET side while indexing into the array, a fatalError will be raised on the Swift side of things.
    public subscript(position: Index) -> Element {
        get {
            assert(position >= startIndex && position < endIndex, "Out of bounds")
            
            do {
                guard let element = try getValue(position) else {
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
                try setValue(newValue, position)
            } catch {
                fatalError("An exception was thrown while calling System.Array.SetValue: \(error.localizedDescription)")
            }
        }
    }
}
*/

/*
class DNMultidimensionalArray<T> where T: System_Object {
    public override class var typeOf: System_Type /* System.Type */ {
        do {
            let elementType = T.typeOf
            let arrayRank: Int32 = 1
            let arrayType = try elementType.makeArrayType(arrayRank)
            
            return arrayType
        } catch {
             return super.typeOf 
        }
    }
    
    public override class var typeName: String {
        let type = typeOf
        
        if let name = try? type.name.string() {
            return name
        }
        
        return "\(T.typeName)[]"
    }
    
    public override class var fullTypeName: String {
        let type = typeOf
        
        if let name = try? type.fullName?.string() {
            return name
        }
        
        return "\(T.fullTypeName)[]"
    }
    
    /// - Returns: An empty .NET array of the specified type.
//    public static var empty: DNArray<T> { get throws {
//        try DNArray<T>.init()
//    }}
}
 */

final class ArrayTestsTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTwoDimensionalArrayOfBool() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let array = tests.twoDimensionalArrayOfBool
        let rank = try array.rank
        
        XCTAssertEqual(rank, 2)
        
        // Check initial state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0))?.castToBool(), false)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1))?.castToBool(), true)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0))?.castToBool(), true)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1))?.castToBool(), false)
        
        // Modify it
        try array.setValue(true.dotNETObject(), Int32(0), Int32(0))
        try array.setValue(false.dotNETObject(), Int32(0), Int32(1))
        
        try array.setValue(false.dotNETObject(), Int32(1), Int32(0))
        try array.setValue(true.dotNETObject(), Int32(1), Int32(1))
        
        // Check modified state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0))?.castToBool(), true)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1))?.castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0))?.castToBool(), false)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1))?.castToBool(), true)
        
        // TODO: DNArray is not equiped to handle multi-dimensional arrays
//        let firstValue = array[0]
//
//        for value in array {
//            print(value)
//        }
    }
    
    func testThreeDimensionalArrayOfInt32() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let array = tests.threeDimensionalArrayOfInt32
        let rank = try array.rank
        
        XCTAssertEqual(rank, 3)
        
        // Check initial state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(0))?.castToInt32(), 1)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(1))?.castToInt32(), 2)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(2))?.castToInt32(), 3)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(0))?.castToInt32(), 4)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(1))?.castToInt32(), 5)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(2))?.castToInt32(), 6)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(0))?.castToInt32(), 7)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(1))?.castToInt32(), 8)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(2))?.castToInt32(), 9)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(0))?.castToInt32(), 10)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(1))?.castToInt32(), 11)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(2))?.castToInt32(), 12)
        
        // Modify it
        try array.setValue(Int32(12).dotNETObject(), Int32(0), Int32(0), Int32(0))
        try array.setValue(Int32(11).dotNETObject(), Int32(0), Int32(0), Int32(1))
        try array.setValue(Int32(10).dotNETObject(), Int32(0), Int32(0), Int32(2))
        
        try array.setValue(Int32(9).dotNETObject(), Int32(0), Int32(1), Int32(0))
        try array.setValue(Int32(8).dotNETObject(), Int32(0), Int32(1), Int32(1))
        try array.setValue(Int32(7).dotNETObject(), Int32(0), Int32(1), Int32(2))
        
        try array.setValue(Int32(6).dotNETObject(), Int32(1), Int32(0), Int32(0))
        try array.setValue(Int32(5).dotNETObject(), Int32(1), Int32(0), Int32(1))
        try array.setValue(Int32(4).dotNETObject(), Int32(1), Int32(0), Int32(2))
        
        try array.setValue(Int32(3).dotNETObject(), Int32(1), Int32(1), Int32(0))
        try array.setValue(Int32(2).dotNETObject(), Int32(1), Int32(1), Int32(1))
        try array.setValue(Int32(1).dotNETObject(), Int32(1), Int32(1), Int32(2))
        
        // Check modified state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(0))?.castToInt32(), 12)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(1))?.castToInt32(), 11)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(2))?.castToInt32(), 10)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(0))?.castToInt32(), 9)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(1))?.castToInt32(), 8)
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(2))?.castToInt32(), 7)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(0))?.castToInt32(), 6)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(1))?.castToInt32(), 5)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(2))?.castToInt32(), 4)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(0))?.castToInt32(), 3)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(1))?.castToInt32(), 2)
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(2))?.castToInt32(), 1)
        
        // TODO: DNArray is not equiped to handle multi-dimensional arrays
//        let firstValue = array[0]
//
//        for value in array {
//            print(value)
//        }
    }
}
