import Foundation

public extension System {
    class GC: System.Object {
        override class var type: System._Type {
            .init(handle: System_GC_TypeOf())
        }
    }
}

public extension System.GC {
	static func collect() {
		Debug.log("Will call collect of \(swiftTypeName)")
		
		System_GC_Collect()
		
		Debug.log("Did call collect of \(swiftTypeName)")
	}
}
