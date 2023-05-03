import XCTest
import BeyondNETSamplesSwift

final class SystemCollectionsGenericListTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTypeOf() {
        let systemTypeType = System_Type.typeOf
        let type = System_Collections_Generic_List_A1.typeOf
        
        let isGenericType = (try? type.isGenericType) ?? false
        XCTAssertTrue(isGenericType)
        
        let isConstructedGenericType = (try? type.isConstructedGenericType) ?? false
        XCTAssertFalse(isConstructedGenericType)
        
        guard let genericArguments = try? type.getGenericArguments() else {
            XCTFail("System.Type.GetGenericArguments should not throw and return an instance")
            
            return
        }
        
        let numberOfGenericArguments = (try? genericArguments.length) ?? -1
        XCTAssertEqual(1, numberOfGenericArguments)
        
        guard let genericArgument = try? genericArguments.getValue(0 as Int32) else {
            XCTFail("System.Array.GetValue should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(genericArgument.is(systemTypeType))
        
        guard let genericArgumentType = genericArgument.castAs(System_Type.self) else {
            XCTFail("Should be convertible to System.Type")
            
            return
        }
        
        guard let genericArgumentTypeName = try? genericArgumentType.name?.string() else {
            XCTFail("System.Reflection.MemberInfo.Name getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("T", genericArgumentTypeName)
    }
    
    func testCreate() {
        let systemStringType = System_String.typeOf
        
        guard let list = try? System_Collections_Generic_List_A1(systemStringType) else {
            XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
            
            return
        }
        
        guard let listType = try? list.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard let listTypeName = try? listType.fullName?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(listTypeName.contains("System.Collections.Generic.List`1[[System.String"))
    }
    
    func testUse() {
        let systemStringType = System_String.typeOf
        
        guard let list = try? System_Collections_Generic_List_A1(systemStringType) else {
            XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
            
            return
        }
        
        let strings = [
            "01. A",
            "02. B",
            "03. C"
        ]
        
        for string in strings {
            let stringDN = string.dotNETString()
            
            XCTAssertNoThrow(try list.add(systemStringType,
                                          stringDN))
        }
        
        let listCount = (try? list.count(systemStringType)) ?? -1
        XCTAssertEqual(strings.count, .init(listCount))
        
        for idx in 0..<listCount {
            guard let element = try? list.item(systemStringType,
                                               idx) else {
                XCTFail("System.Collections.Generic.List<System.String>[] getter not throw and return an instance")
                
                return
            }
            
            XCTAssertTrue(element.is(systemStringType))
            
            guard let elementString = element.castAs(System_String.self)?.string() else {
                XCTFail("Failed to get string")
                
                return
            }
            
            let expectedString = strings[.init(idx)]
            
            XCTAssertEqual(expectedString, elementString)
        }
		
		let idx1: Int32 = 1
		
		let newStringForIdx1 = "New String"
		let newStringForIdx1DN = newStringForIdx1.dotNETString()
		
		do {
			try list.item_set(systemStringType,
							  idx1,
							  newStringForIdx1DN)
		} catch {
			XCTFail("System.Collections.Generic.List<System.String>[] setter not throw and return an instance")
			
			return
		}
		
        guard let newElement1String = try? list.item(systemStringType,
                                                     idx1)?.castAs(System_String.self)?.string() else {
			XCTFail("System.Collections.Generic.List<System.String>[] getter not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStringForIdx1, newElement1String)
    }
}
