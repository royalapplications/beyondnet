import XCTest
import BeyondDotNETSampleKit

final class StructTestTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
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
        
        
        let origName = "test"
        var structRef: Beyond_NET_Sample_StructTest? = try Beyond_NET_Sample_StructTest(origName.dotNETString())
        
        let ret2 = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(&structRef)
        
        guard let ret2 else {
            XCTFail("StructTest.GetNullableStructReturnValueOfRefParameter should not be nil")
            
            return
        }
        
        XCTAssertTrue(structRef == ret2)
        
        XCTAssertEqual(try ret2.name?.string(), origName)
        
        let newName4 = "NotTest"
        try ret2.name_set(newName4.dotNETString())
        XCTAssertEqual(try ret2.name?.string(), newName4)
    }
}
