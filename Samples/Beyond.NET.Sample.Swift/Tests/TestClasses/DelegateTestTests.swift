import XCTest
import BeyondDotNETSampleKit

final class DelegateTestTests: XCTestCase {
    override class func setUp() {
        Self.sharedSetUp()
    }
    
    override class func tearDown() {
        Self.sharedTearDown()
    }
    
    func testTransformInt() throws {
        let original: Int32 = 0
        let valueToAdd: Int32 = 1
        let expectedResult: Int32 = original + valueToAdd
        
        let result = try Beyond.NET.Sample.DelegatesTest.transformInt(original, .init({ i in
            let innerResult = i + valueToAdd
            
            return innerResult
        }))

        XCTAssertEqual(result, expectedResult)
    }
    
    // TODO: Delegates with ref parameters
//    func testTransformIntWithRef() {
//        let original: Int32 = 0
//        var originalVar = original
//        let valueToAdd: Int32 = 1
//        let expectedResult: Int32 = original + valueToAdd
//
//        let result = try? Beyond.NET.Sample.DelegatesTest.transformIntWithRef(&originalVar, .init({ iRef in
//            let innerResult = iRef + valueToAdd
//
//            iRef = innerResult
//
//            return innerResult
//        }))
//
//        XCTAssertEqual(result, expectedResult)
//        XCTAssertEqual(originalVar, expectedResult)
//        XCTAssertEqual(original, 0)
//    }
    
    func testTransformPoint() throws {
        let originalX: Double = 0
        let originalY: Double = 0
        
        let valueToAddToX: Double = 0.1
        let valueToAddToY: Double = 0.2
        
        let original = try Beyond.NET.Sample.Point(originalX, originalY)
        let expectedResult = try Beyond.NET.Sample.Point(originalX + valueToAddToX, originalY + valueToAddToY)
        
        let result = try Beyond.NET.Sample.DelegatesTest.transformPoint(original, .init({ p in
            guard let pX = try? p.x else {
                XCTFail("Beyond.NET.Sample.Point.X should not throw")
                
                guard let emptyPoint = try? Beyond.NET.Sample.Point() else {
                    fatalError("Beyond.NET.Sample.Point ctor should not throw and return an instance")
                }
                
                return emptyPoint
            }
            
            guard let pY = try? p.y else {
                XCTFail("Beyond.NET.Sample.Point.Y should not throw")
                
                guard let emptyPoint = try? Beyond.NET.Sample.Point() else {
                    fatalError("Beyond.NET.Sample.Point ctor should not throw and return an instance")
                }
                
                return emptyPoint
            }
            
            let newX = pX + valueToAddToX
            let newY = pY + valueToAddToY
            
            guard let innerResult = try? Beyond.NET.Sample.Point(newX, newY) else {
                XCTFail("Beyond.NET.Sample.Point ctor should not throw and return an instance")

                guard let emptyPoint = try? Beyond.NET.Sample.Point() else {
                    fatalError("Beyond.NET.Sample.Point ctor should not throw and return an instance")
                }
                
                return emptyPoint
            }
            
            return innerResult
        }))
        
        let resultX = try result.x
        let resultY = try result.y
        let expectedX = try expectedResult.x
        let expectedY = try expectedResult.y
        
        XCTAssertEqual(resultX, expectedX)
        XCTAssertEqual(resultY, expectedY)
    }
    
    // TODO: Delegates with ref parameters
//    func testTransformPointWithRef() {
//        let originalX: Double = 0
//        let originalY: Double = 0
//
//        let valueToAddToX: Double = 0.1
//        let valueToAddToY: Double = 0.2
//
//        guard let original = try? Beyond.NET.Sample.Point(originalX, originalY) else {
//            XCTFail("Beyond.NET.Sample.Point ctor should not throw and return an instance")
//
//            return
//        }
//
//        var originalVar: Beyond.NET.Sample.Point? = original
//
//        guard let expectedResult = try? Beyond.NET.Sample.Point(originalX + valueToAddToX, originalY + valueToAddToY) else {
//            XCTFail("Beyond.NET.Sample.Point ctor should not throw and return an instance")
//
//            return
//        }
//
//        let result = try? Beyond.NET.Sample.DelegatesTest.transformRefPoint(&originalVar, .init({ pRef in
//            guard let pX = try? pRef?.x else {
//                XCTFail("Beyond.NET.Sample.Point.X should not throw")
//
//                return nil
//            }
//
//            guard let pY = try? pRef?.y else {
//                XCTFail("Beyond.NET.Sample.Point.Y should not throw")
//
//                return nil
//            }
//
//            let newX = pX + valueToAddToX
//            let newY = pY + valueToAddToY
//
//            guard let innerResult = try? Beyond.NET.Sample.Point(newX, newY) else {
//                XCTFail("Beyond.NET.Sample.Point ctor should not throw and return an instance")
//
//                return nil
//            }
//
//            // Setting this should also replace the value pointed to by originalVar
//            pRef = innerResult
//
//            return innerResult
//        }))
//
//        guard let result,
//              let resultX = try? result.x,
//              let resultY = try? result.y,
//              let expectedX = try? expectedResult.x,
//              let expectedY = try? expectedResult.y,
//              let originalVar,
//              let originalVarX = try? originalVar.x,
//              let originalVarY = try? originalVar.y else {
//            XCTFail("No result")
//
//            return
//        }
//
//        XCTAssertEqual(resultX, expectedX)
//        XCTAssertEqual(resultY, expectedY)
//
//        XCTAssertEqual(originalVarX, expectedX)
//        XCTAssertEqual(originalVarY, expectedY)
//    }
}
