import XCTest
import BeyondDotNETSampleKit

final class SystemStringTests: XCTestCase {
    private lazy var randomStrings: [String] = {
        let numberOfStrings = 1_000
        let letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        
        let strings: [String] = (0..<numberOfStrings).compactMap({ _ in
            guard let letter = letters.randomElement() else {
                return nil
            }
            
            return String(letter)
        })
        
        return strings
    }()
    
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testString() {
        let emptyStringDN = System_String.empty
        
        let emptyString = String(dotNETString: emptyStringDN)
        XCTAssertTrue(emptyString.isEmpty)
        
        let isNullOrEmpty = (try? System_String.isNullOrEmpty(emptyStringDN)) ?? true
        XCTAssertTrue(isNullOrEmpty)
        
        let isNullOrWhiteSpace = (try? System_String.isNullOrWhiteSpace(emptyStringDN)) ?? true
        XCTAssertTrue(isNullOrWhiteSpace)
        
        let nonEmptyString = "Hello World!"
        let nonEmptyStringDN = nonEmptyString.dotNETString()
        
        let isNonEmptyStringNullOrEmpty = (try? System_String.isNullOrEmpty(nonEmptyStringDN)) ?? true
        XCTAssertFalse(isNonEmptyStringNullOrEmpty)
        
        let stringForTrimmingDN = " \(nonEmptyString) ".dotNETString()
        
        guard let trimmedString = try? stringForTrimmingDN.trim().string() else {
            XCTFail("System.String.Trim should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(nonEmptyString, trimmedString)
        
        let worldDN = "World".dotNETString()
        let expectedIndexOfWorld: Int32 = 6
        
        let indexOfWorld = (try? nonEmptyStringDN.indexOf(worldDN)) ?? -1
        XCTAssertEqual(expectedIndexOfWorld, indexOfWorld)
        
        let splitOptions: System_StringSplitOptions = [ .removeEmptyEntries, .trimEntries ]
        
        let blankDN = " ".dotNETString()
        
        guard let split = try? nonEmptyStringDN.split(blankDN, splitOptions) else {
            XCTFail("System.String.Split should not throw and return an instance")
            
            return
        }
        
        guard (try? split.length) ?? 0 == 2 else {
            XCTFail("System.Array.Length getter should not throw and return 2")
            
            return
        }
    }
    
    func testStringReplace() {
        let hello = "Hello"
        let otherPart = " World 😀"
        let originalString = "\(hello)\(otherPart)"
        let emptyString = ""
        
        let expectedString = originalString.replacingOccurrences(of: hello,
                                                                 with: emptyString)
        
        let originalStringDN = originalString.dotNETString()
        let helloDN = hello.dotNETString()
        let emptyStringDN = emptyString.dotNETString()
        
        
        
        guard let replacedString = try? originalStringDN.replace(helloDN, emptyStringDN).string() else {
            XCTFail("System.String.Replace should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(expectedString, replacedString)
    }
    
    func testStringSubstring() {
        let string = "Hello World 😀"
        let needle = "World"
        
        let stringDN = string.dotNETString()
        let needleDN = needle.dotNETString()
        
        let expectedIndex: Int32 = 6
        
        let index = (try? stringDN.indexOf(needleDN)) ?? -1
        
        XCTAssertEqual(expectedIndex, index)
    }
    
    func testStringSplitAndJoin() {
        let components = [
            "1. First",
            " 2. Second",
            "",
            "3. Third "
        ]
        
        var cleanedComponents = [String]()
        
        for component in components {
            let trimmedComponent = component.trimmingCharacters(in: .whitespacesAndNewlines)
            
            guard !trimmedComponent.isEmpty else {
                continue
            }
            
            cleanedComponents.append(trimmedComponent)
        }
        
        let separator = ";"
        let separatorDN = separator.dotNETString()
        
        let joined = components.joined(separator: separator)
        let joinedDN = joined.dotNETString()
        
        let cleanedJoined = cleanedComponents.joined(separator: separator)
        
        let splitOptions: System_StringSplitOptions = [ .removeEmptyEntries, .trimEntries ]
        
        guard let split = try? joinedDN.split(separatorDN, splitOptions) else {
            XCTFail("System.String.Split should not throw and return an instance")
            
            return
        }
        
        let length = (try? split.length) ?? -1
        
        XCTAssertEqual(cleanedComponents.count, .init(length))
        
        for (idx, component) in cleanedComponents.enumerated() {
            guard let componentRetObj = try? split.getValue(Int32(idx)) else {
                XCTFail("System.Array.GetValue should not throw and return an instance")
                
                return
            }
            
            let componentRetDN: System_String
            
            do {
                componentRetDN = try componentRetObj.castTo()
            } catch {
                XCTFail("castTo should not throw")
                
                return
            }
            
            let componentRet = componentRetDN.string()
            
            XCTAssertEqual(component, componentRet)
        }
        
        guard let joinedRet = try? System_String.join(separatorDN,
                                                      split).string() else {
            XCTFail("System.String.Join should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(cleanedJoined, joinedRet)
    }
    
    func testStringComparison() {
        let hello = "Hello"
        let helloDN = hello.dotNETString()
        
        let hello2 = "Hello"
        let hello2DN = hello2.dotNETString()
        
        let world = "World"
        let worldDN = world.dotNETString()
        
        XCTAssertTrue(helloDN == hello2DN)
        XCTAssertFalse(helloDN === hello2DN)
        
        XCTAssertFalse(helloDN == worldDN)
        XCTAssertFalse(helloDN === worldDN)
    }
    
    func testSwiftStringToSystemStringConversionPerformance() {
        let strings = randomStrings
        
        measure {
            for string in strings {
                _ = string.dotNETString()
            }
        }
    }
    
    func testSystemStringToSwiftStringConversionPerformance() {
        let strings = randomStrings
        
        let dnStrings = strings.map({
            $0.dotNETString()
        })
        
        measure {
            for string in dnStrings {
                _ = string.string()
            }
        }
    }
}
