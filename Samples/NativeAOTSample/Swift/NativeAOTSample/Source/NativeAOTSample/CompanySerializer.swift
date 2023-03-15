import Foundation

public class CompanySerializer: System.Object {
    override class var type: System._Type {
		.init(handle: NativeAOTSample_CompanySerializer_TypeOf())
	}
	
	convenience init() {
		Debug.log("Will create \(Self.swiftTypeName)")
		
		guard let handle = NativeAOTSample_CompanySerializer_Create() else {
			fatalError("Failed to create \(Self.swiftTypeName)")
		}
		
		self.init(handle: handle)
		
		Debug.log("Did create \(Self.swiftTypeName)")
	}
}

// MARK: - Public API
public extension CompanySerializer {
	func serializeToJSON(company: Company) -> String {
		Debug.log("Will call serializeToJson of \(swiftTypeName)")
		
		guard let valueC = NativeAOTSample_CompanySerializer_SerializeToJson(handle,
																			 company.handle) else {
			fatalError("Failed to get value from serializeToJson of \(swiftTypeName)")
		}
		
		defer { valueC.deallocate() }
		
		Debug.log("Did call serializeToJson of \(swiftTypeName)")
		
		let value = String(cString: valueC)
		
		return value
	}
	
	func deserializeFromJSON(_ jsonString: String) throws -> Company {
		Debug.log("Will call deserializeFromJson of \(swiftTypeName)")
		
		var exceptionHandle: System_Exception_t?
		
		let companyHandle = jsonString.withCString { jsonStringC in
			NativeAOTSample_CompanySerializer_DeserializeFromJson(handle,
																  jsonStringC,
																  &exceptionHandle)
		}
		
		if let companyHandle {
			Debug.log("Did call deserializeFromJson of \(swiftTypeName)")
			
			return .init(handle: companyHandle)
		} else if let exceptionHandle {
			Debug.log("deserializeFromJson of \(swiftTypeName) threw an exception")
			
            let exception = System.Exception(handle: exceptionHandle)
			let error = exception.error
			
			throw error
		} else {
			fatalError("deserializeFromJson of \(swiftTypeName) failed but didn't throw an exception. This is unexpected.")
		}
	}
}
