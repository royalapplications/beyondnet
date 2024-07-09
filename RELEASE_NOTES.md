# Beyond.NET Release Notes

## Version 0.4 (Alpha)
- XML Documentation is now extracted and applied to generated Swift bindings
- Arrays are now exposed to Swift as `DNArray<T> where T: System_Object`
- Arrays conform to Swift's `MutableCollection` protocol now
- More type-safe way to do boxing/unboxing of primitives in Swift
- Support for generating interfaces as protocols in Swift
- Bugfixes related to nullability
- Bugfixes related to detecting overridden/shadowed members when generating Swift bindings

## Version 0.3 (Alpha)
- Support for Xcode 15
- SwiftUI sample project
- Fixed Swift equivalence operator (`==`, `!=`, `===`, `!==`) implementations for optionals
- typeOf implementations for .NET primitives are now generated
- Support for nullable structs
- Nullability annotations are now honored
- First class support for `ReadOnlySpan<byte>`

## Version 0.2 (Alpha)
- Support for automatically building for Apple platforms
- Many bugfixes in the generator

## Version 0.1 (Alpha)
- Initial alpha release
