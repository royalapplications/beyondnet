import XCTest
import BeyondNETSamplesSwift

final class AddressTests: XCTestCase {
	@MainActor
	override class func setUp() {
		Self.sharedSetUp()
	}
	
	@MainActor
	override class func tearDown() {
		Self.sharedTearDown()
	}
	
	func testAddress() {
		var exception: System_Exception_t?
		
		let street = "Schwedenplatz"
		let streetDN = street.cDotNETString()
		defer { System_String_Destroy(streetDN) }
		
		let city = "Vienna"
		let cityDN = city.cDotNETString()
		defer { System_String_Destroy(cityDN) }
		
		guard let address = Beyond_NET_Sample_Address_Create(streetDN,
																			  cityDN,
																			  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		defer { Beyond_NET_Sample_Address_Destroy(address) }
		
		guard let retrievedStreet = String(cDotNETString: Beyond_NET_Sample_Address_Street_Get(address,
																											   &exception),
										   destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(street, retrievedStreet)
		
		let addressType = System_Object_GetType(address,
												&exception)
		
		guard let addressType,
			  exception == nil else {
			XCTFail("System.Object.GetType should not throw and return an instance")
			
			return
		}
		
		defer { System_Type_Destroy(addressType) }
		
		let expectedAddressTypeFullName = "Beyond.NET.Sample.Address"
		
		guard let actualAddressFullTypeName = String(cDotNETString: System_Type_FullName_Get(addressType,
																							&exception),
													 destroyDotNETString: true),
			  exception == nil else {
			XCTFail("System.Type.FullName getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(expectedAddressTypeFullName, actualAddressFullTypeName)
	}
	
	func testAddressMover() {
		var exception: System_Exception_t?
		
		let originalStreet = "Schwedenplatz"
		let originalStreetDN = originalStreet.cDotNETString()
		defer { System_String_Destroy(originalStreetDN) }
		
		let newStreet = "Stephansplatz"
		let newStreetDN = newStreet.cDotNETString()
		defer { System_String_Destroy(newStreetDN) }

		let originalCity = "Vienna"
		let originalCityDN = originalCity.cDotNETString()
		defer { System_String_Destroy(originalCityDN) }
		
		let newCity = "Wien"
		let newCityDN = newCity.cDotNETString()
		defer { System_String_Destroy(newCityDN) }

		guard let originalAddress = Beyond_NET_Sample_Address_Create(originalStreetDN,
																					  originalCityDN,
																					  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")

			return
		}

		let moverFunc: Beyond_NET_Sample_MoveDelegate_CFunction_t = { context, newStreetInnerDN, newCityInnerDN in
			guard let context else {
				XCTFail("Context is nil but should not")

				return nil
			}

			var innerException: System_Exception_t?

			guard let originalStreetInner = String(cDotNETString: Beyond_NET_Sample_Address_Street_Get(context,
																													   &innerException),
												   destroyDotNETString: true),
				  innerException == nil else {
				XCTFail("Original Street is nil but should not")

				return nil
			}

			XCTAssertEqual("Schwedenplatz", originalStreetInner)

			guard let newAddress = Beyond_NET_Sample_Address_Create(newStreetInnerDN,
																					 newCityInnerDN,
																					 &innerException),
				  innerException == nil else {
				XCTFail("Address ctor should not throw and return an instance")
				
				return nil
			}
			
			return newAddress
		}

		let moverDelegate = Beyond_NET_Sample_MoveDelegate_Create(originalAddress,
																				   moverFunc,
																				   nil)

		guard let moverDelegate else {
			XCTFail("Delegate should not be nil")

			return
		}

		defer { Beyond_NET_Sample_MoveDelegate_Destroy(moverDelegate) }

		guard let newAddress = Beyond_NET_Sample_Address_Move(originalAddress,
																			   moverDelegate,
																			   newStreetDN,
																			   newCityDN,
																			   &exception),
			  exception == nil else {
			XCTFail("Address.Move should not throw and return an instance")

			return
		}
		
		defer { Beyond_NET_Sample_Address_Destroy(newAddress) }

		guard let retrievedNewStreet = String(cDotNETString: Beyond_NET_Sample_Address_Street_Get(newAddress,
																												  &exception),
											  destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStreet, retrievedNewStreet)

		guard let retrievedNewCity = String(cDotNETString: Beyond_NET_Sample_Address_City_Get(newAddress,
																											  &exception),
											destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.City getter should not throw and return an instance")

			return
		}
		
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}