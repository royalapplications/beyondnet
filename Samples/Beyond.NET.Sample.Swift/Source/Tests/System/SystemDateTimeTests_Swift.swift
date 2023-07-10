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
    
    func testSwiftDateToSystemDateTime() {
        let nanoSecsPerTick = 100
        let nanoSecsPerMicrosec = 1000
        let nanoSecsPerMillisec = 1000000
        
        let referenceSwiftDate = Date(timeIntervalSince1970: 0)
        
        let components: Set<Calendar.Component> = [
            .era,
            .year,
            .month,
            .day,
            .hour,
            .minute,
            .second,
            .nanosecond,
            .calendar
        ]
        
        var calendar = Calendar(identifier: .gregorian)
        calendar.timeZone = .gmt
        
        let calComponents = calendar.dateComponents(components,
                                                    from: referenceSwiftDate)
        
        // DateTime doesn't support dates starting with year 10.000.
        // Except for the year 10.000 on the dot: we convert it to DateTime.MaxValue.
        //
        // DateTime.MaxValue is actually one tick before year 10.000. This
        // means that when we convert DateTime.MaxValue to NSDate, we
        // actually end up with a date in year 10.000 due to precision
        // differences. In order to be able to roundtrip a
        // DateTime.MaxValue value, we hardcode the corresponding
        // NSDate.SecondsSinceReferenceDate here.
        // TODO: Use this in extension
//        if (calComponents.Year >= 10000) {
//            if (d.SecondsSinceReferenceDate == 252423993600)
//                return DateTime.SpecifyKind (DateTime.MaxValue, DateTimeKind.Utc);
//            throw new ArgumentOutOfRangeException (nameof (d), d, $"The date is outside the range of DateTime: {d.SecondsSinceReferenceDate}");
//        }
        
        // DateTime doesn't support BC dates (AD dates have Era = 1)
        // TODO: Use this in extension
//        if (calComponents.Era != 1)
//            throw new ArgumentOutOfRangeException (nameof (d), d, "The date is outside the range of DateTime.");
        
        // NSCalendar gives us the number of nanoseconds corresponding
        // with the fractional second. DateTime's constructor wants
        // milliseconds and microseconds separately, where microseconds is
        // the fractional number of milliseconds. That doesn't count for
        // any remaining ticks, so add that at the end manually. This
        // means we need to do some math here, to split the sub-second
        // number of nanoseconds into milliseconds, microseconds and
        // ticks.
        guard var nanosecondsLeft = calComponents.nanosecond else {
            XCTFail("Failed to get nanoseconds from calendar components")
            
            return
        }
        
        let milliseconds = nanosecondsLeft / nanoSecsPerMillisec
        nanosecondsLeft -= milliseconds * nanoSecsPerMillisec
        let microseconds = nanosecondsLeft / nanoSecsPerMicrosec
        nanosecondsLeft -= microseconds * nanoSecsPerMicrosec
        let ticks = nanosecondsLeft / nanoSecsPerTick
        
        guard let year = calComponents.year else {
            XCTFail("Failed to get year from calendar components")
            
            return
        }
        
        guard let month = calComponents.month else {
            XCTFail("Failed to get month from calendar components")
            
            return
        }
        
        guard let day = calComponents.day else {
            XCTFail("Failed to get day from calendar components")
            
            return
        }
        
        guard let hour = calComponents.hour else {
            XCTFail("Failed to get hour from calendar components")
            
            return
        }
        
        guard let minute = calComponents.minute else {
            XCTFail("Failed to get minute from calendar components")
            
            return
        }
        
        guard let second = calComponents.second else {
            XCTFail("Failed to get second from calendar components")
            
            return
        }
        
        guard var retDate = try? System.DateTime(Int32(year),
                                                 Int32(month),
                                                 Int32(day),
                                                 Int32(hour),
                                                 Int32(minute),
                                                 Int32(second),
                                                 Int32(milliseconds),
                                                 Int32(microseconds),
                                                 .utc) else {
            XCTFail("System.DateTime ctor should not throw and return an instance")
            
            return
        }
        
        if ticks > 0 {
            guard let adjustedRetDate = try? retDate.addTicks(.init(ticks)) else {
                XCTFail("System.DateTime.AddTicks should not throw and return an instance")
                
                return
            }
            
            retDate = adjustedRetDate
        }
        
        // TODO: Compare
    }
}
