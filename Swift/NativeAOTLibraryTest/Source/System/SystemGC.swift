import Foundation

public class SystemGC: SystemObject {
	override class var type: SystemType {
		.init(handle: System_GC_TypeOf())
	}
}

public extension SystemGC {
	static func collect() {
		Debug.log("Will call collect of \(swiftTypeName)")
		
		System_GC_Collect()
		
		Debug.log("Did call collect of \(swiftTypeName)")
	}
}
