import Foundation

public class Person: SystemObject {
	override class var type: SystemType {
		.init(handle: NativeAOTSample_Person_TypeOf())
	}
}

// MARK: - Public API
public extension Person {
	convenience init(firstName: String,
					 lastName: String,
					 age: Int32) {
		Debug.log("Will create \(Self.swiftTypeName)")
		
		let handle = firstName.withCString { firstNameC in
			lastName.withCString { lastNameC in
				NativeAOTSample_Person_Create(firstNameC,
											  lastNameC,
											  age)
			}
		}
		
		guard let handle else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}
		
		self.init(handle: handle)
		
		Debug.log("Did create \(Self.swiftTypeName)")
	}
	
	var age: Int32 {
		get {
			Debug.log("Will get age of \(swiftTypeName)")
			
			let value = NativeAOTSample_Person_Age_Get(handle)
			
			Debug.log("Did get age of \(swiftTypeName)")
			
			return value
		}
		set {
			Debug.log("Will set age of \(swiftTypeName)")
			
			NativeAOTSample_Person_Age_Set(handle, newValue)
			
			Debug.log("Did set age of \(swiftTypeName)")
		}
	}
	
	var firstName: String {
		get {
			Debug.log("Will get firstName of \(swiftTypeName)")
			
			guard let valueC = NativeAOTSample_Person_FirstName_Get(handle) else {
				fatalError("Failed to get firstName of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log("Did get firstName of \(swiftTypeName)")
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log("Will set firstName of \(swiftTypeName)")
			
			newValue.withCString {
				NativeAOTSample_Person_FirstName_Set(handle, $0)
			}
			
			Debug.log("Did set firstName of \(swiftTypeName)")
		}
	}
	
	var lastName: String {
		get {
			Debug.log("Will get lastName of \(swiftTypeName)")
			
			guard let valueC = NativeAOTSample_Person_LastName_Get(handle) else {
				fatalError("Failed to get lastName of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log("Did get lastName of \(swiftTypeName)")
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log("Will set lastName of \(swiftTypeName)")
			
			newValue.withCString {
				NativeAOTSample_Person_LastName_Set(handle, $0)
			}
			
			Debug.log("Did set lastName of \(swiftTypeName)")
		}
	}
	
	var fullName: String {
		Debug.log("Will get fullName of \(swiftTypeName)")
		
		guard let valueC = NativeAOTSample_Person_FullName_Get(handle) else {
			fatalError("Failed to get fullName of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get fullName of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	func reduceAge(byYears: Int32) throws {
		Debug.log("Will reduce age of \(swiftTypeName)")
		
		var exceptionHandle: System_Exception_t?
		
		let success = NativeAOTSample_Person_ReduceAge(handle,
													   byYears,
													   &exceptionHandle).boolValue
		
		if success {
			Debug.log("Did reduce age of \(swiftTypeName)")
		} else if let exceptionHandle {
			Debug.log("Reduce age of \(swiftTypeName) threw an exception")
			
			let exception = SystemException(handle: exceptionHandle)
			let error = exception.error
			
			throw error
		} else {
			fatalError("Reduce age of \(swiftTypeName) failed but didn't throw an exception. This is unexpected.")
		}
	}
	
	// Sample API for demonstrating non-escaping closures
	typealias ChangeAgeNewAgeProvider = () -> Int32
	
	// Sample API for demonstrating non-escaping closures
	func changeAge(_ newAgeProvider: ChangeAgeNewAgeProvider?) throws {
		Debug.log("Will change age of \(swiftTypeName)")
		
		var exceptionHandle: System_Exception_t?
		
		let closureBox: NativeBox<ChangeAgeNewAgeProvider>?
		let handler: NativeAOTSample_Person_ChangeAge_NewAgeProvider_t?
		
		if let newAgeProvider {
			closureBox = .init(value: newAgeProvider)
			
			handler = { innerContext in
				guard let innerContext else {
					fatalError("No context")
				}
				
				let closure = NativeBox<ChangeAgeNewAgeProvider>.fromPointerUnretained(innerContext).value
				let result = closure()
				
				return result
			}
		} else {
			closureBox = nil
			handler = nil
		}
		
		let context = closureBox?.unretainedPointer()
		
		let success = NativeAOTSample_Person_ChangeAge(handle,
													   context,
													   handler,
													   &exceptionHandle).boolValue
		
		if success {
			Debug.log("Did change age of \(swiftTypeName)")
		} else if let exceptionHandle {
			Debug.log("Change age of \(swiftTypeName) threw an exception")
			
			let exception = SystemException(handle: exceptionHandle)
			let error = exception.error
			
			throw error
		} else {
			fatalError("Change age of \(swiftTypeName) failed but didn't throw an exception. This is unexpected.")
		}
	}
}
