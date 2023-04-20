import XCTest
import NativeAOTCodeGeneratorOutputSample

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
		
		guard let address = try? NativeAOT_CodeGeneratorInputSample_Address(streetDN,
																			cityDN) else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		guard let retrievedStreet = try? address.street_get()?.string() else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(street, retrievedStreet)
		
		guard let addressType = try? address.getType() else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		let expectedAddressTypeFullName = "NativeAOT.CodeGeneratorInputSample.Address"
		
		guard let actualAddressFullTypeName = try? addressType.fullName_get()?.string() else {
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

		guard let originalAddress = try? NativeAOT_CodeGeneratorInputSample_Address(originalStreetDN,
																					originalCityDN) else {
			XCTFail("Address is nil but should not")

			return
		}

		let moverFunc: NativeAOT_CodeGeneratorInputSample_MoveDelegate.ClosureType = { newStreetInnerDN, newCityInnerDN in
			guard let newAddress = try? NativeAOT_CodeGeneratorInputSample_Address(newStreetInnerDN,
																				   newCityInnerDN) else {
				XCTFail("Address ctor should not throw and return an instance")
				
				return nil
			}
			
			return newAddress
		}

		guard let moverDelegate = NativeAOT_CodeGeneratorInputSample_MoveDelegate(moverFunc) else {
			XCTFail("Delegate should not be nil")

			return
		}

		guard let newAddress = try? originalAddress.move(moverDelegate,
														 newStreetDN,
														 newCityDN) else {
			XCTFail("Address.Move should not throw and return an instance")

			return
		}
		
		guard let retrievedNewStreet = try? newAddress.street_get()?.string() else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStreet, retrievedNewStreet)

		guard let retrievedNewCity = try? newAddress.city_get()?.string() else {
			XCTFail("Address.City getter should not throw and return an instance")

			return
		}
		
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
