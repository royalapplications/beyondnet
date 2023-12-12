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
        
        let array = try tests.twoDimensionalArrayOfBool
        let rank = try array.rank
        
        XCTAssertEqual(rank, 2)
        
        // Check initial state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0))?.castToBool(), false)
        XCTAssertEqual(try array[[0, 0]].castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1))?.castToBool(), true)
        XCTAssertEqual(try array[[0, 1]].castToBool(), true)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0))?.castToBool(), true)
        XCTAssertEqual(try array[[1, 0]].castToBool(), true)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1))?.castToBool(), false)
        XCTAssertEqual(try array[[1, 1]].castToBool(), false)
        
        // Modify it
        array[[0, 0]] = true.dotNETObject()
        array[[0, 1]] = false.dotNETObject()
        array[[1, 0]] = false.dotNETObject()
        array[[1, 1]] = true.dotNETObject()
        
        // Check modified state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0))?.castToBool(), true)
        XCTAssertEqual(try array[[0, 0]].castToBool(), true)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1))?.castToBool(), false)
        XCTAssertEqual(try array[[0, 1]].castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0))?.castToBool(), false)
        XCTAssertEqual(try array[[1, 0]].castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1))?.castToBool(), true)
        XCTAssertEqual(try array[[1, 1]].castToBool(), true)
    }
    
    func testCreatingTwoDimensionalArrayOfBool() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let newArray = try DNMultidimensionalArray<System.Boolean>(lengths: [2, 2])
        let rank = try newArray.rank
        
        XCTAssertEqual(rank, 2)
        
        // Set values
        newArray[[0, 0]] = true.dotNETObject()
        newArray[[0, 1]] = false.dotNETObject()
        newArray[[1, 0]] = false.dotNETObject()
        newArray[[1, 1]] = true.dotNETObject()
        
        try tests.twoDimensionalArrayOfBool_set(newArray)
        
        let array = try tests.twoDimensionalArrayOfBool
        
        // Check state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0))?.castToBool(), true)
        XCTAssertEqual(try array[[0, 0]].castToBool(), true)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1))?.castToBool(), false)
        XCTAssertEqual(try array[[0, 1]].castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0))?.castToBool(), false)
        XCTAssertEqual(try array[[1, 0]].castToBool(), false)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1))?.castToBool(), true)
        XCTAssertEqual(try array[[1, 1]].castToBool(), true)
    }
    
    func testThreeDimensionalArrayOfInt32() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let array = try tests.threeDimensionalArrayOfInt32
        let rank = try array.rank
        
        XCTAssertEqual(rank, 3)
        
        // Check initial state
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(0))?.castToInt32(), 1)
        XCTAssertEqual(try array[[0, 0, 0]].castToInt32(), 1)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(1))?.castToInt32(), 2)
        XCTAssertEqual(try array[[0, 0, 1]].castToInt32(), 2)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(0), Int32(2))?.castToInt32(), 3)
        XCTAssertEqual(try array[[0, 0, 2]].castToInt32(), 3)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(0))?.castToInt32(), 4)
        XCTAssertEqual(try array[[0, 1, 0]].castToInt32(), 4)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(1))?.castToInt32(), 5)
        XCTAssertEqual(try array[[0, 1, 1]].castToInt32(), 5)
        
        XCTAssertEqual(try array.getValue(Int32(0), Int32(1), Int32(2))?.castToInt32(), 6)
        XCTAssertEqual(try array[[0, 1, 2]].castToInt32(), 6)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(0))?.castToInt32(), 7)
        XCTAssertEqual(try array[[1, 0, 0]].castToInt32(), 7)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(1))?.castToInt32(), 8)
        XCTAssertEqual(try array[[1, 0, 1]].castToInt32(), 8)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(0), Int32(2))?.castToInt32(), 9)
        XCTAssertEqual(try array[[1, 0, 2]].castToInt32(), 9)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(0))?.castToInt32(), 10)
        XCTAssertEqual(try array[[1, 1, 0]].castToInt32(), 10)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(1))?.castToInt32(), 11)
        XCTAssertEqual(try array[[1, 1, 1]].castToInt32(), 11)
        
        XCTAssertEqual(try array.getValue(Int32(1), Int32(1), Int32(2))?.castToInt32(), 12)
        XCTAssertEqual(try array[[1, 1, 2]].castToInt32(), 12)
        
        // Modify it
        array[[0, 0, 0]] = Int32(12).dotNETObject()
        array[[0, 0, 1]] = Int32(11).dotNETObject()
        array[[0, 0, 2]] = Int32(10).dotNETObject()
        
        array[[0, 1, 0]] = Int32(9).dotNETObject()
        array[[0, 1, 1]] = Int32(8).dotNETObject()
        array[[0, 1, 2]] = Int32(7).dotNETObject()
        
        array[[1, 0, 0]] = Int32(6).dotNETObject()
        array[[1, 0, 1]] = Int32(5).dotNETObject()
        array[[1, 0, 2]] = Int32(4).dotNETObject()
        
        array[[1, 1, 0]] = Int32(3).dotNETObject()
        array[[1, 1, 1]] = Int32(2).dotNETObject()
        array[[1, 1, 2]] = Int32(1).dotNETObject()
        
        // Check modified state
        XCTAssertEqual(try array[[0, 0, 0]].castToInt32(), 12)
        XCTAssertEqual(try array[[0, 0, 1]].castToInt32(), 11)
        XCTAssertEqual(try array[[0, 0, 2]].castToInt32(), 10)
        
        XCTAssertEqual(try array[[0, 1, 0]].castToInt32(), 9)
        XCTAssertEqual(try array[[0, 1, 1]].castToInt32(), 8)
        XCTAssertEqual(try array[[0, 1, 2]].castToInt32(), 7)
        
        XCTAssertEqual(try array[[1, 0, 0]].castToInt32(), 6)
        XCTAssertEqual(try array[[1, 0, 1]].castToInt32(), 5)
        XCTAssertEqual(try array[[1, 0, 2]].castToInt32(), 4)
        
        XCTAssertEqual(try array[[1, 1, 0]].castToInt32(), 3)
        XCTAssertEqual(try array[[1, 1, 1]].castToInt32(), 2)
        XCTAssertEqual(try array[[1, 1, 2]].castToInt32(), 1)
    }
    
    func testCreatingThreeDimensionalArrayOfInt32() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let newArray = try DNMultidimensionalArray<System.Int32>(lengths: [3, 3, 3])
        let rank = try newArray.rank
        
        XCTAssertEqual(rank, 3)
        
        // Set values
        newArray[[0, 0, 0]] = Int32(12).dotNETObject()
        newArray[[0, 0, 1]] = Int32(11).dotNETObject()
        newArray[[0, 0, 2]] = Int32(10).dotNETObject()
        
        newArray[[0, 1, 0]] = Int32(9).dotNETObject()
        newArray[[0, 1, 1]] = Int32(8).dotNETObject()
        newArray[[0, 1, 2]] = Int32(7).dotNETObject()
        
        newArray[[1, 0, 0]] = Int32(6).dotNETObject()
        newArray[[1, 0, 1]] = Int32(5).dotNETObject()
        newArray[[1, 0, 2]] = Int32(4).dotNETObject()
        
        newArray[[1, 1, 0]] = Int32(3).dotNETObject()
        newArray[[1, 1, 1]] = Int32(2).dotNETObject()
        newArray[[1, 1, 2]] = Int32(1).dotNETObject()
        
        try tests.threeDimensionalArrayOfInt32_set(newArray)
        
        let array = try tests.threeDimensionalArrayOfInt32
        
        // Check state
        XCTAssertEqual(try array[[0, 0, 0]].castToInt32(), 12)
        XCTAssertEqual(try array[[0, 0, 1]].castToInt32(), 11)
        XCTAssertEqual(try array[[0, 0, 2]].castToInt32(), 10)
        
        XCTAssertEqual(try array[[0, 1, 0]].castToInt32(), 9)
        XCTAssertEqual(try array[[0, 1, 1]].castToInt32(), 8)
        XCTAssertEqual(try array[[0, 1, 2]].castToInt32(), 7)
        
        XCTAssertEqual(try array[[1, 0, 0]].castToInt32(), 6)
        XCTAssertEqual(try array[[1, 0, 1]].castToInt32(), 5)
        XCTAssertEqual(try array[[1, 0, 2]].castToInt32(), 4)
        
        XCTAssertEqual(try array[[1, 1, 0]].castToInt32(), 3)
        XCTAssertEqual(try array[[1, 1, 1]].castToInt32(), 2)
        XCTAssertEqual(try array[[1, 1, 2]].castToInt32(), 1)
    }
    
    func testArrayOfNullableString() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let array = try tests.arrayOfNullableString
        let rank = try array.rank
        
        XCTAssertEqual(rank, 1)
        
        // Check initial state
        XCTAssertNil(array[0])
        XCTAssertEqual(array[1]?.string(), "a")
        XCTAssertEqual(array[2]?.string(), "b")
        XCTAssertEqual(array[3]?.string(), "c")
        
        // Modify it
        array[3] = nil
        
        try tests.arrayOfNullableString_set(array)
        let newArray = try tests.arrayOfNullableString
        
        // Check modified state
        XCTAssertNil(newArray[0])
        XCTAssertEqual(newArray[1]?.string(), "a")
        XCTAssertEqual(newArray[2]?.string(), "b")
        XCTAssertNil(newArray[3])
    }
    
    func testArrayOfGuids() throws {
        let tests = try Beyond.NET.Sample.ArrayTests()
        
        let array = try tests.arrayOfGuids
        let rank = try array.rank
        
        XCTAssertEqual(rank, 1)
        
        let emptyGuid = System.Guid.empty
        
        // Check initial state
        XCTAssertEqual(array[0], emptyGuid)
        XCTAssertNotEqual(array[1], emptyGuid)
        
        // Modify it
        array[0] = try System.Guid.newGuid()
        array[1] = emptyGuid
        
        try tests.arrayOfGuids_set(array)
        let newArray = try tests.arrayOfGuids
        
        // Check modified state
        XCTAssertNotEqual(newArray[0], emptyGuid)
        XCTAssertEqual(newArray[1], emptyGuid)
    }
}
