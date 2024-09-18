import XCTest
import BeyondDotNETSampleKit

final class SystemDateTimeTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemDateTime() throws {
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
        
        let nowDotNet = try System_DateTime(.init(expectedYear),
                                            .init(expectedMonth),
                                            .init(expectedDay),
                                            .init(expectedHour),
                                            .init(expectedMinute),
                                            .init(expectedSecond))
        
        let year = try nowDotNet.year
        let month = try nowDotNet.month
        let day = try nowDotNet.day
        let hour = try nowDotNet.hour
        let minute = try nowDotNet.minute
        
        XCTAssertEqual(expectedYear, .init(year))
        XCTAssertEqual(expectedMonth, .init(month))
        XCTAssertEqual(expectedDay, .init(day))
        
        XCTAssertEqual(expectedHour, .init(hour))
        XCTAssertEqual(expectedMinute, .init(minute))
    }
    
    func testSystemDateTimeParse() throws {
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
        
        let cultureNameDN = "en-US".dotNETString()
        
        guard let enUSCulture = try? System_Globalization_CultureInfo(cultureNameDN) else {
            XCTFail("System.CultureInfo ctor should not throw and return an instance")
            
            return
        }
        
        let dateString = "\(expectedMonth)/\(expectedDay)/\(expectedYear) \(expectedHour):\(expectedMinute):\(expectedSecond)"
        let dateStringDN = dateString.dotNETString()
        
        var nowDotNet = System_DateTime.minValue
        
        guard try System_DateTime.tryParse(dateStringDN,
                                           enUSCulture,
                                           &nowDotNet) else {
            XCTFail("System.DateTime.TryParse should return true, an instance as out parameter and not throw")
            
            return
        }
        
        let year = try nowDotNet.year
        let month = try nowDotNet.month
        let day = try nowDotNet.day
        let hour = try nowDotNet.hour
        let minute = try nowDotNet.minute
        
        XCTAssertEqual(expectedYear, .init(year))
        XCTAssertEqual(expectedMonth, .init(month))
        XCTAssertEqual(expectedDay, .init(day))
        
        XCTAssertEqual(expectedHour, .init(hour))
        XCTAssertEqual(expectedMinute, .init(minute))
    }
    
    func testDateConversion() throws {
        let referenceSwiftDate = Date(timeIntervalSince1970: 0)
        
        let dateTime = try System.DateTime(1970, 1, 1, 0, 0, 0, 0, 0, .utc)
        
        let dateTimeMinValue = System.DateTime.minValue
        
        let retDate = try dateTime.swiftDate()
        
        XCTAssertEqual(retDate, referenceSwiftDate)
        XCTAssertNotEqual(retDate, .init())
        
        let retDateTime = try retDate.dotNETDateTime()
        
        XCTAssertTrue(retDateTime == dateTime)
        XCTAssertFalse(retDateTime == dateTimeMinValue)
    }
}
