import Foundation

public class Person: SystemObject { }

// MARK: - Public API
public extension Person {
	convenience init(firstName: String,
					 lastName: String,
					 age: Int32) {
		let firstNameC = firstName.cString(using: .utf8)
		let lastNameC = lastName.cString(using: .utf8)
		
		Debug.log("Will create \(Self.swiftTypeName)")
		
		guard let handle = NativeAOTLibraryTest_Person_Create(firstNameC,
															  lastNameC,
															  age) else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}
		
		self.init(handle: handle)
		
		Debug.log("Did create \(Self.swiftTypeName)")
	}
	
	var age: Int32 {
		get {
			Debug.log("Will get age of \(swiftTypeName)")
			
			let value = NativeAOTLibraryTest_Person_Age_Get(handle)
			
			Debug.log("Did get age of \(swiftTypeName)")
			
			return value
		}
		set {
			Debug.log("Will set age of \(swiftTypeName)")
			
			NativeAOTLibraryTest_Person_Age_Set(handle, newValue)
			
			Debug.log("Did set age of \(swiftTypeName)")
		}
	}
	
	var firstName: String {
		get {
			Debug.log("Will get firstName of \(swiftTypeName)")
			
			guard let valueC = NativeAOTLibraryTest_Person_FirstName_Get(handle) else {
				fatalError("Failed to get firstName of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log("Did get firstName of \(swiftTypeName)")
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log("Will set firstName of \(swiftTypeName)")
			
			let newValueC = newValue.cString(using: .utf8)
			NativeAOTLibraryTest_Person_FirstName_Set(handle, newValueC)
			
			Debug.log("Did set firstName of \(swiftTypeName)")
		}
	}
	
	var lastName: String {
		get {
			Debug.log("Will get lastName of \(swiftTypeName)")
			
			guard let valueC = NativeAOTLibraryTest_Person_LastName_Get(handle) else {
				fatalError("Failed to get lastName of \(swiftTypeName)")
			}
			
			defer { valueC.deallocate() }
			
			Debug.log("Did get lastName of \(swiftTypeName)")
			
			let value = String(cString: valueC)
			
			return value
		}
		set {
			Debug.log("Will set lastName of \(swiftTypeName)")
			
			let newValueC = newValue.cString(using: .utf8)
			NativeAOTLibraryTest_Person_LastName_Set(handle, newValueC)
			
			Debug.log("Did set lastName of \(swiftTypeName)")
		}
	}
	
	var fullName: String {
		Debug.log("Will get fullName of \(swiftTypeName)")
		
		guard let valueC = NativeAOTLibraryTest_Person_FullName_Get(handle) else {
			fatalError("Failed to get fullName of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did get fullName of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
}
