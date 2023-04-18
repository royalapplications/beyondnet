import XCTest
import NativeAOTCodeGeneratorOutputSample

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
        let systemTypeType = System_Type.typeOf()
        let type = System_Collections_Generic_List_A1.typeOf()
        
        let isGenericType = (try? type.isGenericType_get()) ?? false
        XCTAssertTrue(isGenericType)
        
        let isConstructedGenericType = (try? type.isConstructedGenericType_get()) ?? false
        XCTAssertFalse(isConstructedGenericType)
        
        guard let genericArguments = try? type.getGenericArguments() else {
            XCTFail("System.Type.GetGenericArguments should not throw and return an instance")
            
            return
        }
        
        let numberOfGenericArguments = (try? genericArguments.length_get()) ?? -1
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
        
        guard let genericArgumentTypeName = try? genericArgumentType.name_get()?.string() else {
            XCTFail("System.Reflection.MemberInfo.Name getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual("T", genericArgumentTypeName)
    }
    
    func testCreate() {
        let systemStringType = System_String.typeOf()
        
        guard let list = try? System_Collections_Generic_List_A1(systemStringType) else {
            XCTFail("System.Collections.Generic.List<System.String> ctor should not throw and return an instance")
            
            return
        }
        
        guard let listType = try? list.getType() else {
            XCTFail("System.Object.GetType should not throw and return an instance")
            
            return
        }
        
        guard let listTypeName = try? listType.fullName_get()?.string() else {
            XCTFail("System.Type.FullName getter should not throw and return an instance")
            
            return
        }
        
        XCTAssertTrue(listTypeName.contains("System.Collections.Generic.List`1[[System.String"))
    }
    
    func testUse() {
        let systemStringType = System_String.typeOf()
        
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
        
        let listCount = (try? list.count_get(systemStringType)) ?? -1
        XCTAssertEqual(strings.count, .init(listCount))
        
        guard let array = try? list.toArray(systemStringType) else {
            XCTFail("System.Collections.Generic.List<System.String>.ToArray should not throw and return an instance")
            
            return
        }
        
        for idx in 0..<listCount {
            guard let element = try? array.getValue(idx) else {
                XCTFail("System.Array.GetValue should not throw and return an instance")
                
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
    }
}
