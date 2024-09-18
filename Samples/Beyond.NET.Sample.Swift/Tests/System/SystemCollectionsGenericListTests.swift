import XCTest
import BeyondDotNETSampleKit

final class SystemCollectionsGenericListTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTypeOf() throws {
        let systemTypeType = System_Type.typeOf
        let type = System_Collections_Generic_List_A1.typeOf
        
        let isGenericType = try type.isGenericType
        XCTAssertTrue(isGenericType)
        
        let isConstructedGenericType = try type.isConstructedGenericType
        XCTAssertFalse(isConstructedGenericType)
        
        let genericArguments = try type.getGenericArguments()
        let numberOfGenericArguments = try genericArguments.length
        XCTAssertEqual(1, numberOfGenericArguments)
        
        let genericArgument = genericArguments[0]
        
        XCTAssertTrue(genericArgument.is(systemTypeType))
        
        guard let genericArgumentType = genericArgument.castAs(System_Type.self) else {
            XCTFail("Should be convertible to System.Type")
            
            return
        }
        
        let genericArgumentTypeName = try genericArgumentType.name.string()
        XCTAssertEqual("T", genericArgumentTypeName)
    }
    
    func testCreate() throws {
        let systemStringType = System_String.typeOf
        
        let list = try System_Collections_Generic_List_A1(T: systemStringType)
        let listType = try list.getType()
        
        guard let listTypeName = try listType.fullName?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(listTypeName.contains("System.Collections.Generic.List`1[[System.String"))
    }
    
    func testUse() throws {
        let systemStringType = System_String.typeOf
        
        let list = try System_Collections_Generic_List_A1(T: systemStringType)
        
        let strings = [
            "01. A",
            "02. B",
            "03. C"
        ]
        
        for string in strings {
            let stringDN = string.dotNETString()
            
            try list.add(T: systemStringType,
                         stringDN)
        }
        
        let listCount = try list.count(T: systemStringType)
        XCTAssertEqual(strings.count, .init(listCount))
        
        for idx in 0..<listCount {
            guard let element = try? list.item(T: systemStringType,
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
		
        try list.item_set(T: systemStringType,
                          idx1,
                          newStringForIdx1DN)
		
        guard let newElement1String = try? list.item(T: systemStringType,
                                                     idx1)?.castAs(System_String.self)?.string() else {
			XCTFail("System.Collections.Generic.List<System.String>[] getter not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStringForIdx1, newElement1String)
    }
}
