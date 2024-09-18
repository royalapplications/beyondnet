import XCTest
import BeyondDotNETSampleKit

final class StructTestTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testStructTest() throws {
		let nameOrig = "Joe"
		let nameNew = "John"
		
		let structTest = try Beyond_NET_Sample_StructTest(nameOrig.dotNETString())
		
		guard let nameOrigRet = try structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameOrig, nameOrigRet)
		
		try structTest.name_set(nameNew.dotNETString())
		
		guard let nameNewRet = try structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameNew, nameNewRet)
	}
    
    func testNullableValueTypes() throws {
        let nullRetVal = try Beyond_NET_Sample_StructTest.nullInstanceProperty
        XCTAssertNil(nullRetVal)
        
        let structRetVal = try Beyond_NET_Sample_StructTest.nonNullInstanceProperty
        XCTAssertEqual(try structRetVal?.name?.string(), "Test")
        
        let newName = "NotTest"
        try structRetVal?.name_set(newName.dotNETString())
        XCTAssertEqual(try structRetVal?.name?.string(), newName)
        
        let nullRetVal2 = try Beyond_NET_Sample_StructTest.getNullableStructReturnValue(true)
        XCTAssertNil(nullRetVal2)
        
        let structRetVal2 = try Beyond_NET_Sample_StructTest.getNullableStructReturnValue(false)
        XCTAssertEqual(try structRetVal2?.name?.string(), "Test")
        
        let newName2 = "NotTest"
        try structRetVal2?.name_set(newName2.dotNETString())
        XCTAssertEqual(try structRetVal2?.name?.string(), newName2)
        
        var nullOutRetVal: Beyond_NET_Sample_StructTest?
        
        try Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(true,
                                                                                    &nullOutRetVal)
        
        XCTAssertNil(nullOutRetVal)
        
        
        
        var structRetVal3: Beyond_NET_Sample_StructTest?
        
        try Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(false,
                                                                                    &structRetVal3)
        
        guard let structRetVal3 else {
            XCTFail("StructTest.GetNullableStructReturnValueInOutParameter should not be nil")
            
            return
        }
        
        XCTAssertEqual(try structRetVal3.name?.string(), "Test")
        
        let newName3 = "NotTest"
        try structRetVal3.name_set(newName3.dotNETString())
        XCTAssertEqual(try? structRetVal3.name?.string(), newName3)
        
        
        var nullRef: Beyond_NET_Sample_StructTest?
        
        let ret = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(&nullRef)
        
        XCTAssertNil(ret)
        XCTAssertTrue(nullRef == ret)
        
        
        let retNonRef = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfParameter(nullRef)
        
        XCTAssertNil(retNonRef)
        XCTAssertTrue(nullRef == retNonRef)
        
        
        let origName = "test"
        var structRef: Beyond_NET_Sample_StructTest? = try Beyond_NET_Sample_StructTest(origName.dotNETString())
        
        let ret2 = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(&structRef)
        
        guard let ret2 else {
            XCTFail("StructTest.GetNullableStructReturnValueOfRefParameter should not be nil")
            
            return
        }
        
        XCTAssertTrue(structRef == ret2)
        XCTAssertEqual(try ret2.name?.string(), origName)
        
        
        let retNonRef2 = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfParameter(structRef)
        
        guard let retNonRef2 else {
            XCTFail("StructTest.GetNullableStructReturnValueOfParameter should not be nil")
            
            return
        }
        
        XCTAssertTrue(structRef == retNonRef2)
        XCTAssertEqual(try ret2.name?.string(), origName)
        
        
        let newName4 = "NotTest"
        try ret2.name_set(newName4.dotNETString())
        XCTAssertEqual(try ret2.name?.string(), newName4)
    }
    
    func testNullableValueTypes_readOnlyNullInstanceField() throws {
        let testClass = try Beyond_NET_Sample_StructTestClass()
        let value = testClass.readOnlyNullInstanceField
        
        XCTAssertNil(value)
    }
    
    func testNullableValueTypes_nonNullInstanceField() throws {
        let origName = "Test"
        let newName = "New Test"
        
        let testClass = try Beyond_NET_Sample_StructTestClass()
        let origValue = testClass.nonNullInstanceField
        
        XCTAssertNotNil(origValue)
        XCTAssertEqual(try origValue?.name?.string(), origName)
        
        let newValue = try Beyond_NET_Sample_StructTest(newName.dotNETString())
        testClass.nonNullInstanceField_set(newValue)
    
        let newValueRet = testClass.nonNullInstanceField
        
        XCTAssertNotNil(newValueRet)
        XCTAssertEqual(try newValueRet?.name?.string(), newName)
        
        testClass.nonNullInstanceField_set(nil)
        
        let nilValueRet = testClass.nonNullInstanceField
        
        XCTAssertNil(nilValueRet)
    }
    
    func testNullableValueTypes_nullableStructPropertyWithGetterAndSetter() throws {
        let testClass = try Beyond_NET_Sample_StructTestClass()
        
        let origValue = try testClass.nullableStructPropertyWithGetterAndSetter
        XCTAssertNil(origValue)
        
        let newName = "Test"
        let newValue = try Beyond_NET_Sample_StructTest(newName.dotNETString())
        
        try testClass.nullableStructPropertyWithGetterAndSetter_set(newValue)
        
        let newValueRet = try testClass.nullableStructPropertyWithGetterAndSetter
        XCTAssertEqual(newValueRet, newValue)
        XCTAssertNotNil(newValueRet)
        XCTAssertEqual(try newValueRet?.name?.string(), newName)
        
        try testClass.nullableStructPropertyWithGetterAndSetter_set(nil)
        let nilValueRet = try testClass.nullableStructPropertyWithGetterAndSetter
        XCTAssertNil(nilValueRet)
    }
}
