# Beyond.NET Release Notes

## Version 0.5 (Alpha)
- .NET: Updated to 9.0.202
- General: Bugfixes related to nullability
- General: Increase likelyness of detecting init-only properties
- Configuration: Added `ExcludedAssemblyNames` as a configuration option
- Swift: Improved enum support in .NET delegates
- Kotlin: Compatibility with Kotlin 2.0
- Kotlin: Many improvements and bugfixes

## Version 0.4 (Alpha)
- Kotlin: Rudimentary Kotlin/JNA support (see [Building the sample for Android on Linux](https://github.com/royalapplications/beyondnet/issues/80) and [Kotlin To-Do's](https://github.com/royalapplications/beyondnet/issues/81))
- Swift: XML Documentation is now extracted and applied to generated bindings
- Swift: Support for generating interfaces as protocols
- Swift: Arrays are now exposed to Swift as `DNArray<T> where T: System_Object`
- Swift: Arrays conform to Swift's `MutableCollection` protocol now
- Swift: More type-safe way to do boxing/unboxing of primitives
- Swift: Support for `out` parameter placeholders (see [C# out parameters in Swift](https://github.com/royalapplications/beyondnet?tab=readme-ov-file#c-out-parameters-in-swift))
- Swift: Performance improvements when converting to/from `System.Guid` and Swift `UUID`
- Swift: Bugfixes related to detecting overridden/shadowed members when generating Swift bindings
- Swift: Additional tests
- Bugfixes related to nullability

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
