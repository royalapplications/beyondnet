import Foundation

public class SystemGC: SystemObject { }

public extension SystemGC {
	static func collect() {
		System_GC_Collect()
	}
}
