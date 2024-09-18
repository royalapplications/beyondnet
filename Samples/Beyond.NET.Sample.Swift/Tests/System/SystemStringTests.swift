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
    
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testString() throws {
        let emptyStringDN = System_String.empty
        
        let emptyString = String(dotNETString: emptyStringDN)
        XCTAssertTrue(emptyString.isEmpty)
        
        let isNullOrEmpty = try System_String.isNullOrEmpty(emptyStringDN)
        XCTAssertTrue(isNullOrEmpty)
        
        let isNullOrWhiteSpace = try System_String.isNullOrWhiteSpace(emptyStringDN)
        XCTAssertTrue(isNullOrWhiteSpace)
        
        let nonEmptyString = "Hello World!"
        let nonEmptyStringDN = nonEmptyString.dotNETString()
        
        let isNonEmptyStringNullOrEmpty = try System_String.isNullOrEmpty(nonEmptyStringDN)
        XCTAssertFalse(isNonEmptyStringNullOrEmpty)
        
        let stringForTrimmingDN = " \(nonEmptyString) ".dotNETString()
        
        let trimmedString = try stringForTrimmingDN.trim().string()
        XCTAssertEqual(nonEmptyString, trimmedString)
        
        let worldDN = "World".dotNETString()
        let expectedIndexOfWorld: Int32 = 6
        
        let indexOfWorld = try nonEmptyStringDN.indexOf(worldDN)
        XCTAssertEqual(expectedIndexOfWorld, indexOfWorld)
        
        let splitOptions: System_StringSplitOptions = [ .removeEmptyEntries, .trimEntries ]
        
        let blankDN = " ".dotNETString()
        
        let split = try nonEmptyStringDN.split(blankDN, splitOptions)
        
        guard try split.length == 2 else {
            XCTFail("System.Array.Length getter should not throw and return 2")
            
            return
        }
    }
    
    func testStringReplace() throws {
        let hello = "Hello"
        let otherPart = " World 😀"
        let originalString = "\(hello)\(otherPart)"
        let emptyString = ""
        
        let expectedString = originalString.replacingOccurrences(of: hello,
                                                                 with: emptyString)
        
        let originalStringDN = originalString.dotNETString()
        let helloDN = hello.dotNETString()
        let emptyStringDN = emptyString.dotNETString()
        
        let replacedString = try originalStringDN.replace(helloDN, emptyStringDN).string()
        XCTAssertEqual(expectedString, replacedString)
    }
    
    func testStringSubstring() throws {
        let string = "Hello World 😀"
        let needle = "World"
        
        let stringDN = string.dotNETString()
        let needleDN = needle.dotNETString()
        
        let expectedIndex: Int32 = 6
        
        let index = try stringDN.indexOf(needleDN)
        XCTAssertEqual(expectedIndex, index)
    }
    
    func testStringSplitAndJoin() throws {
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
        
        let split = try joinedDN.split(separatorDN, splitOptions)
        
        let length = try split.length
        
        XCTAssertEqual(cleanedComponents.count, .init(length))
        
        for (idx, component) in cleanedComponents.enumerated() {
            let componentRetObj = split[Int32(idx)]
            
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
        
        let joinedRet = try? System_String.join(separatorDN,
                                                split.nullable()).string()
        
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
