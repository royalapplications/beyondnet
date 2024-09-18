import XCTest
import BeyondDotNETSampleKit

final class AddressTests: XCTestCase {
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testAddress() throws {
		let street = "Schwedenplatz"
		let streetDN = street.dotNETString()
		
		let city = "Vienna"
		let cityDN = city.dotNETString()
		
        let address = try Beyond_NET_Sample_Address(streetDN,
                                                    cityDN)
		
		let retrievedStreet = try address.street.string()
		XCTAssertEqual(street, retrievedStreet)
		
		let addressType = try address.getType()
		let expectedAddressTypeFullName = "Beyond.NET.Sample.Address"
		let actualAddressFullTypeName = try addressType.fullName?.string()
		XCTAssertEqual(expectedAddressTypeFullName, actualAddressFullTypeName)
	}
	
	func testAddressMover() throws {
		let originalStreet = "Schwedenplatz"
		let originalStreetDN = originalStreet.dotNETString()
		
		let newStreet = "Stephansplatz"
		let newStreetDN = newStreet.dotNETString()

		let originalCity = "Vienna"
		let originalCityDN = originalCity.dotNETString()
		
		let newCity = "Wien"
		let newCityDN = newCity.dotNETString()

        let originalAddress = try Beyond_NET_Sample_Address(originalStreetDN,
                                                            originalCityDN)

		let moverFunc: Beyond_NET_Sample_MoveDelegate.ClosureType = { newStreetInnerDN, newCityInnerDN in
			guard let newAddress = try? Beyond_NET_Sample_Address(newStreetInnerDN,
																				   newCityInnerDN) else {
				XCTFail("Address ctor should not throw and return an instance")
				
				return nil
			}
			
			return newAddress
		}

        let newAddress = try originalAddress.move(.init(moverFunc),
                                                  newStreetDN,
                                                  newCityDN)
		
		let retrievedNewStreet = try newAddress.street.string()
		XCTAssertEqual(newStreet, retrievedNewStreet)

		let retrievedNewCity = try newAddress.city.string()
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
