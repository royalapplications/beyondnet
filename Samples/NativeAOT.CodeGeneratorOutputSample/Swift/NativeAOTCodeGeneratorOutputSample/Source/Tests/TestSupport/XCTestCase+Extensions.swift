import Foundation
import XCTest
import NativeAOTCodeGeneratorOutputSample

fileprivate class UnhandledExceptionHandlerStorage {
	static var unhandledExceptionHandler: System_UnhandledExceptionEventHandler_t?
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
		var exception: System_Exception_t?
		
		System_GC_Collect_1(&exception)
		
		XCTAssertNil(exception)
	}
	
	@MainActor
	class func addUnhandledExceptionHandler() -> System_UnhandledExceptionEventHandler_t? {
		var exception: System_Exception_t?
		
		guard let appDomain = System_AppDomain_CurrentDomain_Get(&exception),
			  exception == nil else {
			XCTFail("System.AppDomain.CurrentDomain getter should not throw and return an instance")
			
			return nil
		}
		
		defer { System_AppDomain_Destroy(appDomain) }
		
		let handlerFunc: System_UnhandledExceptionEventHandler_CFunction_t = { _, _, eventArgs  in
			defer { System_UnhandledExceptionEventArgs_Destroy(eventArgs) }
			
			let exceptionAsString: String?
			
			var innerException: System_Exception_t?
			
			if let exceptionObject = System_UnhandledExceptionEventArgs_ExceptionObject_Get(eventArgs,
																							&innerException),
				  innerException == nil {
				defer { System_Object_Destroy(exceptionObject) }
				
				exceptionAsString = String(dotNETString: System_Object_ToString(exceptionObject,
																				&innerException),
										   destroyDotNETString: true)
			} else {
				exceptionAsString = nil
			}
			
			XCTFail("An unhandled exception was thrown:\n\(exceptionAsString ?? "N/A")")
		}
		
		let handler = System_UnhandledExceptionEventHandler_Create(nil,
																   handlerFunc,
																   nil)
		
		System_AppDomain_UnhandledException_Add(appDomain,
												handler)
		
		return handler
	}
	
	@MainActor
	class func removeUnhandledExceptionHandler(_ handler: System_UnhandledExceptionEventHandler_t?) {
		guard let handler else { return }

		var exception: System_Exception_t?
		
		defer { System_UnhandledExceptionEventHandler_Destroy(handler) }
		
		guard let appDomain = System_AppDomain_CurrentDomain_Get(&exception),
			  exception == nil else {
			return
		}
		
		defer { System_AppDomain_Destroy(appDomain) }
		
		System_AppDomain_UnhandledException_Remove(appDomain,
												   handler)
	}
}
