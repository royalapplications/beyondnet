import XCTest
import NativeAOTCodeGeneratorOutputSample

final class AddressTests: XCTestCase {
	override class func setUp() {
		Self.gcCollect()
	}
	
	override class func tearDown() {
		Self.gcCollect()
	}
	
	func testAddress() {
		var exception: System_Exception_t?
		
		let street = "Schwedenplatz"
		let streetDN = street.dotNETString()
		defer { System_String_Destroy(streetDN) }
		
		let city = "Vienna"
		let cityDN = city.dotNETString()
		defer { System_String_Destroy(cityDN) }
		
		guard let address = NativeAOT_CodeGeneratorInputSample_Address_Create(streetDN,
																			  cityDN,
																			  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Address_Destroy(address) }
		
		guard let retrievedStreet = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_Street_Get(address,
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
		
		let expectedAddressTypeFullName = "NativeAOT.CodeGeneratorInputSample.Address"
		
		guard let actualAddressFullTypeName = String(dotNETString: System_Type_FullName_Get(addressType,
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
		let originalStreetDN = originalStreet.dotNETString()
		defer { System_String_Destroy(originalStreetDN) }
		
		let newStreet = "Stephansplatz"
		let newStreetDN = newStreet.dotNETString()
		defer { System_String_Destroy(newStreetDN) }

		let originalCity = "Vienna"
		let originalCityDN = originalCity.dotNETString()
		defer { System_String_Destroy(originalCityDN) }
		
		let newCity = "Wien"
		let newCityDN = newCity.dotNETString()
		defer { System_String_Destroy(newCityDN) }

		guard let originalAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(originalStreetDN,
																					  originalCityDN,
																					  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")

			return
		}

		let moverFunc: NativeAOT_CodeGeneratorInputSample_MoveDelegate_CFunction_t = { context, newStreetInnerDN, newCityInnerDN in
			guard let context else {
				XCTFail("Context is nil but should not")

				return nil
			}

			var innerException: System_Exception_t?

			guard let originalStreetInner = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_Street_Get(context,
																													   &innerException),
												   destroyDotNETString: true),
				  innerException == nil else {
				XCTFail("Original Street is nil but should not")

				return nil
			}

			XCTAssertEqual("Schwedenplatz", originalStreetInner)

			guard let newAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(newStreetInnerDN,
																					 newCityInnerDN,
																					 &innerException),
				  innerException == nil else {
				XCTFail("Address ctor should not throw and return an instance")
				
				return nil
			}
			
			return newAddress
		}

		let moverDelegate = NativeAOT_CodeGeneratorInputSample_MoveDelegate_Create(originalAddress,
																				   moverFunc,
																				   nil)

		guard let moverDelegate else {
			XCTFail("Delegate should not be nil")

			return
		}

		defer { NativeAOT_CodeGeneratorInputSample_MoveDelegate_Destroy(moverDelegate) }

		guard let newAddress = NativeAOT_CodeGeneratorInputSample_Address_Move(originalAddress,
																			   moverDelegate,
																			   newStreetDN,
																			   newCityDN,
																			   &exception),
			  exception == nil else {
			XCTFail("Address.Move should not throw and return an instance")

			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Address_Destroy(newAddress) }

		guard let retrievedNewStreet = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_Street_Get(newAddress,
																												  &exception),
											  destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.Street getter should not throw and return an instance")
			
			return
		}
		
		XCTAssertEqual(newStreet, retrievedNewStreet)

		guard let retrievedNewCity = String(dotNETString: NativeAOT_CodeGeneratorInputSample_Address_City_Get(newAddress,
																											  &exception),
											destroyDotNETString: true),
			  exception == nil else {
			XCTFail("Address.City getter should not throw and return an instance")

			return
		}
		
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
