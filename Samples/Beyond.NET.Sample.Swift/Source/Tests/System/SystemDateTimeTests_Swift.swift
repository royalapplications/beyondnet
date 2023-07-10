import XCTest
import BeyondNETSamplesSwift

final class SystemDateTimeTests_Swift: XCTestCase {
    @MainActor
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    @MainActor
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testSystemDateTime() {
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
        
        guard let nowDotNet = try? System_DateTime(.init(expectedYear),
                                                   .init(expectedMonth),
                                                   .init(expectedDay),
                                                   .init(expectedHour),
                                                   .init(expectedMinute),
                                                   .init(expectedSecond)) else {
            XCTFail("System.DateTime ctor should not throw and return an instance")
            
            return
        }
        
        let year = (try? nowDotNet.year) ?? -1
        let month = (try? nowDotNet.month) ?? -1
        let day = (try? nowDotNet.day) ?? -1
        let hour = (try? nowDotNet.hour) ?? -1
        let minute = (try? nowDotNet.minute) ?? -1
        
        XCTAssertEqual(expectedYear, .init(year))
        XCTAssertEqual(expectedMonth, .init(month))
        XCTAssertEqual(expectedDay, .init(day))
        
        XCTAssertEqual(expectedHour, .init(hour))
        XCTAssertEqual(expectedMinute, .init(minute))
    }
    
    func testSystemDateTimeParse() {
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
        
        guard let enUSCultureAsIFormatProvider = enUSCulture.castAs(System_IFormatProvider.self) else {
            XCTFail("System.Globalization.CultureInfo should be convertible to System.IFormatProvider")
            
            return
        }
        
        var nowDotNet: System_DateTime?
        
        let success = (try? System_DateTime.tryParse(dateStringDN,
                                                     enUSCultureAsIFormatProvider,
                                                     &nowDotNet)) ?? false
        
        guard success,
              let nowDotNet else {
            XCTFail("System.DateTime.TryParse should return true, an instance as out parameter and not throw")
            
            return
        }
        
        let year = (try? nowDotNet.year) ?? -1
        let month = (try? nowDotNet.month) ?? -1
        let day = (try? nowDotNet.day) ?? -1
        let hour = (try? nowDotNet.hour) ?? -1
        let minute = (try? nowDotNet.minute) ?? -1
        
        XCTAssertEqual(expectedYear, .init(year))
        XCTAssertEqual(expectedMonth, .init(month))
        XCTAssertEqual(expectedDay, .init(day))
        
        XCTAssertEqual(expectedHour, .init(hour))
        XCTAssertEqual(expectedMinute, .init(minute))
    }
    
    func testSystemDateTimeToSwiftDate() {
        let nanoSecsPerTicks: Int64 = 100
        
        let referenceSwiftDate = Date(timeIntervalSince1970: 0)
        
        guard let dateTime = try? System.DateTime(1970, 1, 1, 0, 0, 0, 0, 0, .utc) else {
            XCTFail("System.DateTime ctor should not throw and return an instance")
            
            return
        }
        
        guard let universalDateTime = try? dateTime.toUniversalTime() else {
            XCTFail("System.DateTime.ToUniversalTime should not throw and return an instance")
            
            return
        }
        
        guard let dateTimeKind = try? universalDateTime.kind else {
            XCTFail("System.DateTime.Kind should not throw")
            
            return
        }
        
        XCTAssertEqual(dateTimeKind, .utc)
        
        guard let ticks = try? universalDateTime.ticks else {
            XCTFail("System.DateTime.Ticks should not throw")
            
            return
        }
        
        let ticksPerSecond = System.TimeSpan.ticksPerSecond
        
        // Compute the sub-second fraction of nanoseconds.
        let subsecondTicks = ticks % ticksPerSecond
        let nanoseconds = subsecondTicks * nanoSecsPerTicks
        
        guard let day = try? universalDateTime.day else {
            XCTFail("System.DateTime.Day should not throw")
            
            return
        }
        
        guard let month = try? universalDateTime.month else {
            XCTFail("System.DateTime.Month should not throw")
            
            return
        }
        
        guard let year = try? universalDateTime.year else {
            XCTFail("System.DateTime.Year should not throw")
            
            return
        }
        
        guard let hour = try? universalDateTime.hour else {
            XCTFail("System.DateTime.Hour should not throw")
            
            return
        }
        
        guard let minute = try? universalDateTime.minute else {
            XCTFail("System.DateTime.Minute should not throw")
            
            return
        }
        
        guard let second = try? universalDateTime.second else {
            XCTFail("System.DateTime.Second should not throw")
            
            return
        }
        
        var dateComponents = DateComponents()
        dateComponents.day = Int(day)
        dateComponents.month = Int(month)
        dateComponents.year = Int(year)
        dateComponents.hour = Int(hour)
        dateComponents.minute = Int(minute)
        dateComponents.second = Int(second)
        dateComponents.nanosecond = Int(nanoseconds)
        
        var calendar = Calendar(identifier: .gregorian)
        calendar.timeZone = .gmt
        
        guard let retDate = calendar.date(from: dateComponents) else {
            XCTFail("Failed to get date from calendar")
            
            return
        }
        
        XCTAssertEqual(referenceSwiftDate, retDate)
    }
    
    func testSystemDateTimeToSwiftDateWithExtension() {
        let referenceSwiftDate = Date(timeIntervalSince1970: 0)
        
        guard let dateTime = try? System.DateTime(1970, 1, 1, 0, 0, 0, 0, 0, .utc) else {
            XCTFail("System.DateTime ctor should not throw and return an instance")
            
            return
        }
        
        guard let retDate = try? dateTime.swiftDate() else {
            XCTFail("System.DateTime.swiftDate should not throw and return an instance")
            
            return
        }
        
        XCTAssertEqual(referenceSwiftDate, retDate)
    }
    
    // TODO: Add extension and tests for converting from Swift Date to .NET DateTime
}
