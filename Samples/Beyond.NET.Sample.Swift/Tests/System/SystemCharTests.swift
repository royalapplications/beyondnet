import XCTest
import BeyondDotNETSampleKit

final class SystemCharTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }

    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testToDNObjectAndBack() throws {
        let aCharacter = Character("a")
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        XCTAssertEqual("a", aCharacter)
        XCTAssertNotEqual("A", aCharacter)
        
        let dnObj = aDNChar.dotNETObject()

        // TODO: This currently fails with `caught error: "System.InvalidCastException: Specified cast is not valid. at NativeGeneratedCode.InteropUtils.DNObjectCastToChar(Void*, Void**) + 0x60"`
        let aDNCharBack = try XCTUnwrap(dnObj.castToChar())
        let aCharacterBack = aDNCharBack.character
        
        XCTAssertEqual("a", aCharacterBack)
    }
    
    func testGetLowercaseA() throws {
        let aDNChar = Beyond.NET.Sample.Source.CharTests.lowercaseA
        let aCharacter = try XCTUnwrap(aDNChar.character)
        
        XCTAssertEqual("a", aCharacter)
        XCTAssertNotEqual("A", aCharacter)
    }
    
    func testPassInLowercaseA() throws {
        let aCharacter: Character = "a"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUpperCharacter: Character = "A"
        let aUpperDNChar = try XCTUnwrap(DNChar(character: aUpperCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInLowercaseAOrThrow(aDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInLowercaseAOrThrow(aUpperDNChar))
    }
    
    func testGetUppercaseA() throws {
        let aUpperDNChar = Beyond.NET.Sample.Source.CharTests.uppercaseA
        let aUpperCharacter = try XCTUnwrap(aUpperDNChar.character)
        
        XCTAssertEqual("A", aUpperCharacter)
        XCTAssertNotEqual("a", aUpperCharacter)
    }
    
    func testPassInUppercaseA() throws {
        let aCharacter: Character = "a"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUpperCharacter: Character = "A"
        let aUpperDNChar = try XCTUnwrap(DNChar(character: aUpperCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInUppercaseAOrThrow(aUpperDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInUppercaseAOrThrow(aDNChar))
    }
    
    func testGetOne() throws {
        let oneDNChar = Beyond.NET.Sample.Source.CharTests.one
        let oneCharacter = try XCTUnwrap(oneDNChar.character)
        
        XCTAssertEqual("1", oneCharacter)
        XCTAssertNotEqual("2", oneCharacter)
    }
    
    func testPassInOne() throws {
        let oneCharacter: Character = "1"
        let oneDNChar = try XCTUnwrap(DNChar(character: oneCharacter))
        
        let twoCharacter: Character = "2"
        let twoDNChar = try XCTUnwrap(DNChar(character: twoCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInOneOrThrow(oneDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInOneOrThrow(twoDNChar))
    }
    
    func testGetLowercaseUmlautA() throws {
        let aUmlautDNChar = Beyond.NET.Sample.Source.CharTests.lowercaseUmlautA
        let aUmlautCharacter = try XCTUnwrap(aUmlautDNChar.character)
        
        XCTAssertEqual("ä", aUmlautCharacter)
        XCTAssertNotEqual("Ä", aUmlautCharacter)
    }
    
    func testPassInLowercaseUmlautA() throws {
        let aCharacter: Character = "ä"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUmlautCharacter: Character = "Ä"
        let aUmlautDNChar = try XCTUnwrap(DNChar(character: aUmlautCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInLowercaseUmlautAOrThrow(aDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInLowercaseUmlautAOrThrow(aUmlautDNChar))
    }
}
