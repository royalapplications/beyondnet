import XCTest
import NativeAOTCodeGeneratorOutputSample

final class AddressTests: XCTestCase {
	func testAddress() {
		var exception: System_Exception_t?
		
		let street = "Schwedenplatz"
		let city = "Vienna"
		
		guard let address = NativeAOT_CodeGeneratorInputSample_Address_Create(street,
																			  city,
																			  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Address_Destroy(address) }
		
		let retrievedStreetC = NativeAOT_CodeGeneratorInputSample_Address_Street_Get(address,
																					 &exception)
		
		XCTAssertNil(exception)
		
		guard let retrievedStreetC else {
			XCTFail("Street is nil")
			
			return
		}
		
		defer { retrievedStreetC.deallocate() }
		
		let retrievedStreet = String(cString: retrievedStreetC)
		XCTAssertEqual(street, retrievedStreet)
		
		let addressType = System_Object_GetType(address,
												&exception)
		
		XCTAssertNil(exception)
		
		guard let addressType else {
			XCTFail("Address is nil")
			
			return
		}
		
		defer { System_Type_Destroy(addressType) }
		
		let expectedAddressTypeFullName = "NativeAOT.CodeGeneratorInputSample.Address"
		
		let actualAddressFullTypeNameC = System_Type_FullName_Get(addressType,
																  &exception)
		
		XCTAssertNil(exception)
		
		guard let actualAddressFullTypeNameC else {
			XCTFail("actualAddressFullTypeNameC should not be nil")
			
			return
		}
		
		let actualAddressFullTypeName = String(cString: actualAddressFullTypeNameC)
		XCTAssertEqual(expectedAddressTypeFullName, actualAddressFullTypeName)
	}
	
	func testAddressMover() {
		var exception: System_Exception_t?
		
		let originalStreet = "Schwedenplatz"
		let newStreet = "Stephansplatz"

		let originalCity = "Vienna"
		let newCity = "Wien"

		guard let originalAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(originalStreet,
																					  originalCity,
																					  &exception),
			  exception == nil else {
			XCTFail("Address is nil but should not")

			return
		}

		let moverFunc: NativeAOT_CodeGeneratorInputSample_MoveDelegate_CFunction_t = { context, newStreetC, newCityC in
			guard let context else {
				XCTFail("Context is nil but should not")

				return nil
			}

			var innerException: System_Exception_t?

			guard let originalStreetC = NativeAOT_CodeGeneratorInputSample_Address_Street_Get(context,
																							  &innerException) else {
				XCTFail("Original Street C is nil but should not")

				return nil
			}

			XCTAssertNil(innerException)

			defer { originalStreetC.deallocate() }

			let originalStreetInner = String(cString: originalStreetC)

			XCTAssertEqual("Schwedenplatz", originalStreetInner)

			let newAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(newStreetC,
																			   newCityC,
																			   &innerException)
			
			XCTAssertNil(innerException)
			
			guard let newAddress else {
				XCTFail("Address should not be nil")

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
																			   newStreet,
																			   newCity,
																			   &exception),
			  exception == nil else {
			XCTFail("Address.Move should not throw and return an instance")

			return
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Address_Destroy(newAddress) }

		let retrievedNewStreetC = NativeAOT_CodeGeneratorInputSample_Address_Street_Get(newAddress, nil)

		guard let retrievedNewStreetC else {
			XCTFail()

			return
		}

		let retrievedNewStreet = String(cString: retrievedNewStreetC)
		XCTAssertEqual(newStreet, retrievedNewStreet)

		let retrievedNewCityC = NativeAOT_CodeGeneratorInputSample_Address_City_Get(newAddress, nil)

		guard let retrievedNewCityC else {
			XCTFail()

			return
		}

		defer { retrievedNewStreetC.deallocate() }

		let retrievedNewCity = String(cString: retrievedNewCityC)
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
