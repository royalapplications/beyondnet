import XCTest
import BeyondDotNETSampleKit

final class SystemCharTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }

    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testLowercaseA() throws {
        let aCharacter: Character = "a"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUpperCharacter: Character = "A"
        let aUpperDNChar = try XCTUnwrap(DNChar(character: aUpperCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInLowercaseAOrThrow(aDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInLowercaseAOrThrow(aUpperDNChar))
    }
    
    func testUppercaseA() throws {
        let aCharacter: Character = "a"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUpperCharacter: Character = "A"
        let aUpperDNChar = try XCTUnwrap(DNChar(character: aUpperCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInUppercaseAOrThrow(aUpperDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInUppercaseAOrThrow(aDNChar))
    }
    
    func testOne() throws {
        let oneCharacter: Character = "1"
        let oneDNChar = try XCTUnwrap(DNChar(character: oneCharacter))
        
        let twoCharacter: Character = "2"
        let twoDNChar = try XCTUnwrap(DNChar(character: twoCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInOneOrThrow(oneDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInOneOrThrow(twoDNChar))
    }
    
    func testLowercaseUmlautA() throws {
        let aCharacter: Character = "ä"
        let aDNChar = try XCTUnwrap(DNChar(character: aCharacter))
        
        let aUpperCharacter: Character = "Ä"
        let aUpperDNChar = try XCTUnwrap(DNChar(character: aUpperCharacter))
        
        try XCTAssertNoThrow(Beyond.NET.Sample.Source.CharTests.passInLowercaseUmlautAOrThrow(aDNChar))
        try XCTAssertThrowsError(Beyond.NET.Sample.Source.CharTests.passInLowercaseUmlautAOrThrow(aUpperDNChar))
    }
}
