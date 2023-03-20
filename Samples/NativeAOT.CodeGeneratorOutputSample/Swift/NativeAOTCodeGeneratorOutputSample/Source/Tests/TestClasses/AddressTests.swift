import XCTest
import NativeAOTCodeGeneratorOutputSample

final class AddressTests: XCTestCase {
	func testAddress() {
		var exception: System_Exception_t?
		
		let street = "Grimmgasse"
		let city = "Vienna"
		
		var address: NativeAOT_CodeGeneratorInputSample_Address_t?
		
		street.withCString { streetC in
			city.withCString { cityC in
				address = NativeAOT_CodeGeneratorInputSample_Address_Create(streetC,
																			cityC,
																			&exception)
			}
		}
		
		defer { NativeAOT_CodeGeneratorInputSample_Address_Destroy(address) }
		
		XCTAssertNil(exception)
		
		guard let address else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		let retrievedStreetC = NativeAOT_CodeGeneratorInputSample_Address_Street_Get(address,
																					 &exception)
		
		XCTAssertNil(exception)
		
		defer { retrievedStreetC?.deallocate() }
		
		guard let retrievedStreetC else {
			XCTFail("Street is nil")
			
			return
		}
		
		let retrievedStreet = String(cString: retrievedStreetC)
		
		XCTAssertEqual(street, retrievedStreet)
		
		let addressType = System_Object_GetType(address,
												nil)
		
		defer { System_Type_Destroy(addressType) }
		
		let expectedAddressTypeFullName = "NativeAOT.CodeGeneratorInputSample.Address"
		
		let actualAddressFullTypeNameC = System_Type_FullName_Get(addressType,
																  nil)
		
		guard let actualAddressFullTypeNameC else {
			XCTFail("actualAddressFullTypeNameC should not be nil")
			
			return
		}
		
		let actualAddressFullTypeName = String(cString: actualAddressFullTypeNameC)
		
		XCTAssertEqual(expectedAddressTypeFullName, actualAddressFullTypeName)
	}
	
	func testAddressMover() {
		var exception: System_Exception_t?
		
		let originalStreet = "Grimmgasse"
		let newStreet = "Stephansplatz"
		
		let originalCity = "Vienna"
		let newCity = "Wien"
		
		var originalAddress: NativeAOT_CodeGeneratorInputSample_Address_t?
		
		originalStreet.withCString { streetC in
			originalCity.withCString { cityC in
				originalAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(streetC,
																					cityC,
																					&exception)
			}
		}
		
		XCTAssertNil(exception)
		
		guard let originalAddress else {
			XCTFail("Address is nil but should not")
			
			return
		}
		
		let moverFunc: NativeAOT_CodeGeneratorInputSample_MoveDelegate_CFunction_t = { context, newStreetC, newCityC in
			guard let context else {
				XCTFail("Context is nil but should not")
				
				return nil
			}
			
			guard let originalStreetC = NativeAOT_CodeGeneratorInputSample_Address_Street_Get(context, nil) else {
				XCTFail("Original Street C is nil but should not")
				
				return nil
			}
			
			let originalStreetInner = String(cString: originalStreetC)
			
			XCTAssertEqual("Grimmgasse", originalStreetInner)
			
			let newAddress = NativeAOT_CodeGeneratorInputSample_Address_Create(newStreetC,
																			   newCityC,
																			   nil)
			
			// TODO: Memory leak
			return newAddress
		}
		
		let moverDelegate = NativeAOT_CodeGeneratorInputSample_MoveDelegate_Create(originalAddress,
																				   moverFunc,
																				   nil)
		
		guard let moverDelegate else {
			XCTFail()
			
			return
		}
		
		var newAddress: NativeAOT_CodeGeneratorInputSample_Address_t?
		
		newStreet.withCString { newStreetC in
			newCity.withCString { newCityC in
				newAddress = NativeAOT_CodeGeneratorInputSample_Address_Move(originalAddress,
																			 moverDelegate,
																			 newStreet,
																			 newCity,
																			 nil)
			}
		}
		
		guard let newAddress else {
			XCTFail()
			
			return
		}
		
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
		
		let retrievedNewCity = String(cString: retrievedNewCityC)
		
		XCTAssertEqual(newCity, retrievedNewCity)
	}
}
