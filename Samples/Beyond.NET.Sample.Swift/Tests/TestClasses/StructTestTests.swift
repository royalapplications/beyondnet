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
	
	func testStructTest() {
		let nameOrig = "Joe"
		let nameNew = "John"
		
		guard let structTest = try? Beyond_NET_Sample_StructTest(nameOrig.dotNETString()) else {
			XCTFail("StructTest ctor should not throw and return an instance")
			
			return
		}
		
		guard let nameOrigRet = try? structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameOrig, nameOrigRet)
		
		XCTAssertNoThrow(try structTest.name_set(nameNew.dotNETString()))
		
		guard let nameNewRet = try? structTest.name?.string() else {
			XCTFail("StructTest.Name getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(nameNew, nameNewRet)
	}
    
    func testNullableValueTypes() {
        do {
            let nullRetVal = try Beyond_NET_Sample_StructTest.getNullableStructReturnValue(true)
            
            XCTAssertNil(nullRetVal)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValue should not throw")
        }
        
        do {
            let structRetVal = try Beyond_NET_Sample_StructTest.getNullableStructReturnValue(false)
            
            XCTAssertNotNil(structRetVal)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValue should not throw")
        }
        
        do {
            var nullOutRetVal: Beyond_NET_Sample_StructTest?
            
            try Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(true,
                                                                                        &nullOutRetVal)
            
            XCTAssertNil(nullOutRetVal)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValueInOutParameter should not throw")
        }
        
        do {
            var structRetVal: Beyond_NET_Sample_StructTest?
            
            try Beyond_NET_Sample_StructTest.getNullableStructReturnValueInOutParameter(false,
                                                                                        &structRetVal)
            
            XCTAssertNotNil(structRetVal)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValueInOutParameter should not throw")
        }
        
        do {
            var nullRef: Beyond_NET_Sample_StructTest?
            
            let ret = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(&nullRef)
            
            XCTAssertNil(ret)
            XCTAssertTrue(nullRef == ret)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValueOfRefParameter should not throw")
        }
        
        do {
            var structRef = try Beyond_NET_Sample_StructTest("test".dotNETString())
            
            let ret = try Beyond_NET_Sample_StructTest.getNullableStructReturnValueOfRefParameter(&structRef)
            
            XCTAssertNotNil(ret)
            XCTAssertTrue(structRef == ret)
        } catch {
            XCTFail("StructTest.GetNullableStructReturnValueOfRefParameter should not throw")
        }
    }
}
