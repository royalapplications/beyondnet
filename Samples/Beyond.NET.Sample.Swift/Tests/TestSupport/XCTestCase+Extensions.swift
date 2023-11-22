import XCTest
import BeyondDotNETSampleKit

fileprivate class UnhandledExceptionHandlerStorage {
	static var unhandledExceptionHandler: System.UnhandledExceptionEventHandler?
}

extension XCTestCase {
	@MainActor
	class func sharedSetUp() {
		if UnhandledExceptionHandlerStorage.unhandledExceptionHandler == nil {
			UnhandledExceptionHandlerStorage.unhandledExceptionHandler = addUnhandledExceptionHandler()
		}
		
		gcCollect()
	}
	
	@MainActor
	class func sharedTearDown() {
		gcCollect()
	}
}

private extension XCTestCase {
	@MainActor
	class func gcCollect() {
        XCTAssertNoThrow(try System.GC.collect())
	}
	
	@MainActor
	class func addUnhandledExceptionHandler() -> System.UnhandledExceptionEventHandler? {
        guard let appDomain = try? System.AppDomain.currentDomain else {
			XCTFail("System.AppDomain.CurrentDomain getter should not throw and return an instance")
			
			return nil
		}
        
        let handler: System.UnhandledExceptionEventHandler? = .init { sender, e in
            let exceptionAsString: String?
            
            if let exceptionObject = try? e.exceptionObject {
                exceptionAsString = try? exceptionObject.toString()?.string()
            } else {
                exceptionAsString = nil
            }
            
            XCTFail("An unhandled exception was thrown:\n\(exceptionAsString ?? "N/A")")
        }
        
        guard let handler else {
            XCTFail("System.UnhandledExceptionEventHandler ctor should not throw and return an instance")
            
            return nil
        }
        
        appDomain.unhandledException_add(handler)
		
		return handler
	}
	
	@MainActor
	class func removeUnhandledExceptionHandler(_ handler: System.UnhandledExceptionEventHandler?) {
		guard let handler else { return }
        
        guard let appDomain = try? System.AppDomain.currentDomain else {
            return
        }
        
        appDomain.unhandledException_remove(handler)
	}
}
