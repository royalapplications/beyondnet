import Foundation

struct System_Array_Iterator: IteratorProtocol {
	let array: System_Array
	
	private let length: Int32
	private var currentIndex: Int32 = -1
	
	init(_ array: System_Array) {
		self.length = (try? array.length) ?? 0
		self.array = array
	}
	
	mutating func next() -> System_Object? {
		let nextIndex = currentIndex + 1
		
		guard nextIndex < length else {
			return nil
		}
		
		let nextValue = try? self.array.getValue(nextIndex)
		
		currentIndex = nextIndex
		
		return nextValue
	}
}

extension System_Array: Sequence {
	public func makeIterator() -> some IteratorProtocol {
		return System_Array_Iterator(self)
	}
}

public class System_Collections_Generic_List<T>: System_Collections_Generic_List_A1 where T: System_Object {
	internal var __typeOfT: System_Type {
		return T.typeOf
	}
	
	public convenience init?() throws {
		try self.init(T.typeOf)
	}
	
	public func add(_ item: T? /* System.Object */) throws {
		try super.add(__typeOfT, item?.castTo())
	}
	
	public func clear() throws {
		try super.clear(__typeOfT)
	}
	
	public func contains(_ item: T? /* System.Object */) throws -> Bool /* System.Boolean */ {
		try super.contains(__typeOfT, item?.castTo())
	}
	
	public func indexOf(_ item: T? /* System.Object */) throws -> Int32 /* System.Int32 */ {
		try super.indexOf(__typeOfT, item?.castTo())
	}
	
	public func remove(_ item: T? /* System.Object */) throws -> Bool /* System.Boolean */ {
		try super.remove(__typeOfT, item?.castTo())
	}
	
	public func count() throws -> Int32 /* System.Int32 */ {
		try super.count(__typeOfT)
	}
	
	public func item(_ index: Int32 /* System.Int32 */) throws -> T? /* System.Object */ {
		try super.item(__typeOfT, index)?.castTo()
	}
	
	public func item_set(_ index: Int32 /* System.Int32 */, _ value: T? /* System.Object */) throws {
		try super.item_set(__typeOfT, index, value?.castTo())
	}
}
