import XCTest
import BeyondDotNETSampleKit

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
