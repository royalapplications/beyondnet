import XCTest
import BeyondNETSamplesSwift

final class AddressTests_Swift: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testAddress() {
		let street = "Schwedenplatz"
		let streetDN = street.dotNETString()
		
		let city = "Vienna"
		let cityDN = city.dotNETString()
		
		guard let address = try? Beyond_NET_Sample_Address(streetDN,
																			cityDN) else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		guard let retrievedStreet = try? address.street?.string() else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(street, retrievedStreet)
		
		guard let addressType = try? address.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		let expectedAddressTypeFullName = "Beyond.NET.Sample.Address"
		
		guard let actualAddressFullTypeName = try? addressType.fullName?.string() else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedAddressTypeFullName, actualAddressFullTypeName)
	}
	
	func testAddressMover() {
		let originalStreet = "Schwedenplatz"
		let originalStreetDN = originalStreet.dotNETString()
		
		let newStreet = "Stephansplatz"
		let newStreetDN = newStreet.dotNETString()

		let originalCity = "Vienna"
		let originalCityDN = originalCity.dotNETString()
		
		let newCity = "Wien"
		let newCityDN = newCity.dotNETString()

		guard let originalAddress = try? Beyond_NET_Sample_Address(originalStreetDN,
																					originalCityDN) else {
			XCTFail("Address is nil but should not")

			return
		}

		let moverFunc: Beyond_NET_Sample_MoveDelegate.ClosureType = { newStreetInnerDN, newCityInnerDN in
			guard let newAddress = try? Beyond_NET_Sample_Address(newStreetInnerDN,
																				   newCityInnerDN) else {
				XCTFail("Address ctor should not throw and return an instance")
				
				return nil
			}
			
			return newAddress
		}

		guard let moverDelegate = Beyond_NET_Sample_MoveDelegate(moverFunc) else {
			XCTFail("Delegate should not be nil")

			return
		}

		guard let newAddress = try? originalAddress.move(moverDelegate,
														 newStreetDN,
														 newCityDN) else {
			XCTFail("Address.Move should not throw and return an instance")

			return
		}
		
		guard let retrievedNewStreet = try? newAddress.street?.string() else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStreet, retrievedNewStreet)

		guard let retrievedNewCity = try? newAddress.city?.string() else {
			XCTFail("Address.City getter should not throw and return an instance")

			return
		}
		
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
