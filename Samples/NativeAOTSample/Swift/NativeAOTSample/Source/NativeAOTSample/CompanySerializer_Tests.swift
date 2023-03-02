import XCTest

@testable import NativeAOTSample

final class CompanySerializerTests: XCTestCase {
	func testCompanySerializer() {
		let company = Company(name: "Fancy Company")
		
		let firstEmployee = Person(firstName: "First",
								   lastName: "Employee",
								   age: 1)
		
		let secondEmployee = Person(firstName: "Second",
									lastName: "Employee",
									age: 2)
		
		company.addEmployee(firstEmployee)
		company.addEmployee(secondEmployee)
		
		let serializer = CompanySerializer()
		
		let json = serializer.serializeToJSON(company: company)
		XCTAssertFalse(json.isEmpty)
		
		// For debugging
		print(json)
		
		let deserializedCompany: Company
		
		do {
			deserializedCompany = try serializer.deserializeFromJSON(json)
		} catch {
			XCTFail("Serializer threw an exception althought it shouldn't: \(error.localizedDescription)")
			
			return
		}
		
		XCTAssertEqual(company.name, deserializedCompany.name)
		XCTAssertEqual(company.numberOfEmployees, deserializedCompany.numberOfEmployees)
		
		guard let firstDeserializedEmployee = deserializedCompany.employee(at: 0) else {
			XCTFail("First employee is nil but should not")
			
			return
		}
		
		XCTAssertTrue(firstEmployee == firstDeserializedEmployee)
		
		guard let secondDeserializedEmployee = deserializedCompany.employee(at: 1) else {
			XCTFail("Second employee is nil but should not")
			
			return
		}
		
		XCTAssertTrue(secondEmployee == secondDeserializedEmployee)
	}
}
