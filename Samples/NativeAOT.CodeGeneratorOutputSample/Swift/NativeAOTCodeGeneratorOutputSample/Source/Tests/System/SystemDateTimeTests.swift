import XCTest
import NativeAOTCodeGeneratorOutputSample

final class SystemDateTimeTests: XCTestCase {
    func testSystemDateTime() {
        var exception: System_Exception_t?
        
        let nowSwift = Date()
        let calendar = Calendar.current
        
        let components = calendar.dateComponents([ .year, .month, .day, .hour, .minute, .second ],
                                                 from: nowSwift)
        
        guard let expectedYear = components.year,
              let expectedMonth = components.month,
              let expectedDay = components.day,
              let expectedHour = components.hour,
              let expectedMinute = components.minute,
              let expectedSecond = components.second else {
            XCTFail("Failed to get year/month/day hour/minute/second")
            
            return
        }
        
        
        guard let nowDotNet = System_DateTime_Create7(.init(expectedYear),
                                                      .init(expectedMonth),
                                                      .init(expectedDay),
                                                      .init(expectedHour),
                                                      .init(expectedMinute),
                                                      .init(expectedSecond),
                                                      &exception),
              exception == nil else {
            XCTFail("System.DateTime ctor should not throw and return an instance")
            
            return
        }
        
        defer { System_DateTime_Destroy(nowDotNet) }
        
        let year = System_DateTime_Year_Get(nowDotNet,
                                            &exception)
        
        XCTAssertNil(exception)
        
        let month = System_DateTime_Month_Get(nowDotNet,
                                              &exception)
        
        XCTAssertNil(exception)
        
        let day = System_DateTime_Day_Get(nowDotNet,
                                          &exception)
        
        XCTAssertNil(exception)
        
        let hour = System_DateTime_Hour_Get(nowDotNet,
                                            &exception)
        
        XCTAssertNil(exception)
        
        let minute = System_DateTime_Minute_Get(nowDotNet,
                                                &exception)
        
        XCTAssertNil(exception)
        
        XCTAssertEqual(expectedYear, .init(year))
        XCTAssertEqual(expectedMonth, .init(month))
        XCTAssertEqual(expectedDay, .init(day))
        
        XCTAssertEqual(expectedHour, .init(hour))
        XCTAssertEqual(expectedMinute, .init(minute))
    }
}
