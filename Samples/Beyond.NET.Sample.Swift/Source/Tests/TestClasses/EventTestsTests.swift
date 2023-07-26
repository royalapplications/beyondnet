import XCTest
import BeyondDotNETSampleNative

final class EventTestTests: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    /* func testEventsAsInDocs() {
        // Create an instance of Beyond.NET.Sample.EventTests
        let eventTest = try! Beyond.NET.Sample.EventTests()!
        
        // Create a variable that will hold the last value passed in to our event handler
        var lastValuePassedIntoEventHandler: Int32 = 0
        
        // Create an event handler
        let eventHandler = Beyond.NET.Sample.EventTests_ValueChangedDelegate { sender, newValue in
            // Remember the last value passed in here
            lastValuePassedIntoEventHandler = newValue
        }
        
        // Add the event handler
        eventTest.valueChanged_add(eventHandler)
        
        // Set a new value (our event handler will be called for this one)
        try! eventTest.value_set(5)
        
        // Remove the event handler
        eventTest.valueChanged_remove(eventHandler)
        
        // Set a another new value (our event handler will NOT be called for this one because we already removed the event handler)
        try! eventTest.value_set(10)
        
        // Prints "5"
        print(lastValuePassedIntoEventHandler)
    } */
    
    func testEventTest() {
        guard let eventTest = try? Beyond.NET.Sample.EventTests() else {
            XCTFail("Beyond.NET.Sample.EventTests ctor should not throw and return an instance")
            
            return
        }
        
        let expectedNewValue: Int32 = 5
        var newValuesPassedToValueChangedHandler = [Int32]()
        
        let eventHandler = Beyond.NET.Sample.EventTests_ValueChangedDelegate { sender, newValue in
            newValuesPassedToValueChangedHandler.append(newValue)
        }
        
        eventTest.valueChanged_add(eventHandler)
        
        do {
            try eventTest.value_set(expectedNewValue)
        } catch {
            XCTFail("Beyond.NET.Sample.EventTests.value setter should not throw")
            
            return
        }
        
        eventTest.valueChanged_remove(eventHandler)
        
        do {
            try eventTest.value_set(10)
        } catch {
            XCTFail("Beyond.NET.Sample.EventTests.value setter should not throw")
            
            return
        }
        
        XCTAssertEqual(1, newValuesPassedToValueChangedHandler.count)
        XCTAssertEqual(newValuesPassedToValueChangedHandler[0], expectedNewValue)
    }
}
