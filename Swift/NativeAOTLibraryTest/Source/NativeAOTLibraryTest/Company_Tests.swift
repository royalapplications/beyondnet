import XCTest

@testable import NativeAOTLibraryTest

final class CompanyTests: XCTestCase {
    func testCompany() {
        let companyName = "Fancy Company GmbH"
        
        let company = Company(name: companyName)
		
		XCTAssertEqual("Company", company.type.name)
		XCTAssertEqual("NativeAOTLibraryTest.Company", company.type.fullName)
        
        XCTAssertEqual(companyName, company.name)
        XCTAssertEqual(0, company.numberOfEmployees)
        XCTAssertNil(company.employee(at: 0))
        XCTAssertNil(company.employee(at: -1))
        XCTAssertNil(company.employee(at: 1))
        
        let employee0 = Person(firstName: "First",
                               lastName: "Employee",
                               age: 1)
        
        XCTAssertTrue(company.addEmployee(employee0))
        
        XCTAssertEqual(1, company.numberOfEmployees)
        
        let employeeAtIndex0 = company.employee(at: 0)
        
        guard let employeeAtIndex0 else {
            XCTFail("Employee at index 0 is nil")
            
            return
        }
        
        XCTAssertTrue(company.containsEmployee(employee0))
        XCTAssertTrue(company.containsEmployee(employeeAtIndex0))
        
        XCTAssertEqual(employee0, employeeAtIndex0)
        
        XCTAssertEqual(employee0.firstName, employeeAtIndex0.firstName)
        XCTAssertEqual(employee0.lastName, employeeAtIndex0.lastName)
        XCTAssertEqual(employee0.age, employeeAtIndex0.age)
        
        let employee1 = Person(firstName: "Second",
                               lastName: "Employee",
                               age: 18)
        
        XCTAssertTrue(company.addEmployee(employee1))
        XCTAssertEqual(2, company.numberOfEmployees)
        XCTAssertTrue(company.containsEmployee(employee1))
        
        let employeeAtIndex1 = company.employee(at: 1)
        
        guard let employeeAtIndex1 else {
            XCTFail("Employee at index 1 is nil")
            
            return
        }
        
        XCTAssertEqual(employee1, employeeAtIndex1)
        
        XCTAssertEqual(employee1.firstName, employeeAtIndex1.firstName)
        XCTAssertEqual(employee1.lastName, employeeAtIndex1.lastName)
        XCTAssertEqual(employee1.age, employeeAtIndex1.age)
        
        XCTAssertTrue(company.removeEmployee(employeeAtIndex1))
        XCTAssertFalse(company.containsEmployee(employee1))
        XCTAssertTrue(company.removeEmployee(employee1))
        XCTAssertEqual(1, company.numberOfEmployees)
        
        company.removeEmployee(employeeAtIndex0)
        XCTAssertEqual(0, company.numberOfEmployees)
    }
	
	func testCompanyCreationAndNameAccessPerformance() {
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = true }
		
		let iterations = 100_000
		let companyName = "Fancy Company"
		
		measure {
			for _ in 0..<iterations {
				_ = Company(name: companyName)
			}
		}
	}
	
	func testCompanyNameAccessPerformance() {
		Debug.isLoggingEnabled = false
		defer { Debug.isLoggingEnabled = true }
		
		let iterations = 100_000
		let companyName = "Fancy Company"
		
		var companies = [Company]()
		
		for _ in 0..<iterations {
			companies.append(.init(name: companyName))
		}
		
		measure {
			for company in companies {
				_ = company.name
			}
		}
	}
}
