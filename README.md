# Beyond.NET

## What is it?

Beyond.NET is a toolset that makes it possible to call .NET code from other programming languages.
Conceptually, think of it like the reverse of the Xamarin tools.
Currently, C and Swift are the supported output languages. But any language that has C interoperability can use the generated bindings.



## How does it work?

Beyond.NET makes use of the fact that .NET methods can be decorated with the [`UnmanagedCallersOnly`](https://learn.microsoft.com/dotnet/api/system.runtime.interopservices.unmanagedcallersonlyattribute) attribute which makes the targeted API callable from native code.
Unfortunately, there are many [restrictions](https://learn.microsoft.com/dotnet/api/system.runtime.interopservices.unmanagedcallersonlyattribute#remarks) where this attribute can be applied which makes it very hard and error-prone to manually expose .NET APIs to native code.

This is where the code generator, that is at the core of Beyond.NET comes in.
The generator can target any compiled .NET assembly and generate native code bindings for all public types and APIs contained within it. It does this by loading the targeted assembly and reflecting over all of its types. Next, wrapper functions for all publicly available APIs are generated and decorated with the `UnmanagedCallersOnly` attribute which makes them callable from native code.
From there, bindings for other languages can be generated. Which language bindings are generated can be controlled by various settings but the C bindings form the basis for all other languages.
So if you're, for instance targeting Swift the call tree looks like this: Swift -> C -> .NET APIs marked with the `UnmanagedCallersOnly` attribute -> Original .NET API.
The generated C# code can then be compiled with .NET NativeAOT which allows the resulting library to be called using the generated language bindings.



## Quick Start Guide

### Prerequisites

- Make sure [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) is installed and on your path.
- On macOS, make sure [Xcode](https://developer.apple.com/xcode/), the macOS and iOS SDKs and the Command Line Tools (`xcode-select --install`) are installed.
- On Linux, make sure clang and zlib are installed


### Generator Executable

- Either clone the Beyond.NET repository or [download](https://github.com/royalapplications/beyondnet/releases/latest) one of the pre-built generator executables for your platform.
- If you do not have a pre-compiled executable of the generator, compile it by either running `dotnet publish` within its [directory](https://github.com/royalapplications/beyondnet/tree/main/Generator/Beyond.NET.CodeGenerator.CLI) or use one of our provided publish scripts like `publish_macos_universal` for compiling a universal macOS binary.
- Open a terminal, switch to the directory containing the built executable and execute the generator (`./beyondnetgen` or just `beyondnetgen` if you have it on your path).
- Since you've provided no arguments, the generator should show its usage screen.
- Optionally, symlink the generator executable to be somewhere on your path (ie. `ln -s ~/Path/To/beyondnetgen /usr/local/bin/beyondnetgen`) if you're not using a pre-built executable.


### Configuring the Generator

- Currently, the generator takes a single required argument: `PathToConfig.json`.
- Create a config file. See [Generator Configuration](#generator-configuration) for supported config values.
- Run the generator with the path to the config file as the first and only argument (`beyondnetgen /Path/To/Config.json`).
- If the generator was successful it will exit with 0 as exit code.
- If errors were encountered they'll appear in the terminal along other log output.


### Generator Modes

The generator always generates language bindings (C header file and optionally a Swift source code file) but it can also be configured to automatically compile a native version of the target assembly.
At the moment, automatic build support is only available on Apple platforms.

If enabled, an [XCFramework](https://developer.apple.com/documentation/xcode/creating-a-multi-platform-binary-framework-bundle) containing compiled binaries for macOS ARM64, macOS x64, iOS ARM64, iOS Simulator ARM64 and iOS Simulator x64 is built. The generated XCFramework is ready to use and can just be dropped into an Xcode project.

We recommend using the automatic build support if possible.
If you decide to [do things manually](README_MANUAL_BUILD.md), you will have to compile the generated C# file using NativeAOT, then link the resulting dynamic library into your native code and include the generated language bindings to call into it.


### Creating a native version of a .NET classlib for Apple platforms

Here's a short step by step guide on how to create a new .NET class library, generate Swift bindings and automatically compile an XCFramework for macOS and iOS.

- Open a terminal window.
- Ensure you have `beyondnetgen` on your path (you can check by running `which beyondnetgen`).
- Create a new .NET classlib project: `mkdir BeyondDemo && cd BeyondDemo && dotnet new classlib`.
- Rename the `Class1.cs` file that the `dotnet new` command automatically created to `Hello.cs` (ie. `mv Class1.cs Hello.cs`).
- Open `Hello.cs` in a text editor.
- Replace its contents with this:

```csharp
namespace BeyondDemo;

public class Hello
{
    public string Name { get; }

    public Hello(string name)
    {
        Name = name;
    }

    public string GetGreeting()
    {
        return $"Hello, {Name}!";
    }
}
```

- Compile the .NET class library: `dotnet publish`.
- Note the published dll's output path (should be something like this `/Path/To/BeyondDemo/bin/Release/net10.0/publish/BeyondDemo.dll`).
- Create a config file for Beyond.NET: `touch Config.json`.
- Open `Config.json` in a text editor.
- Replace its contents with this:

```json
{
    "AssemblyPath": "bin/Release/net9.0/publish/BeyondDemo.dll",

    "Build": {
        "Target": "apple-universal"
    }
}
```

- Ensure the `AssemblyPath` matches the path where your dll was built.
- Note that you can enter the path relative to the working directory like in the config example above.
- Run the generator: `beyondnetgen Config.json`.
- On a Mac Studio M2 Ultra, this takes a little more than a minute while on an 8-Core Intel Xeon iMac Pro, it takes around 3 minutes. So it might be worth getting some coffee depending on your hardware. (TODO: Outdated info, as now with parallel building support the times are way better)
- The individual code generation and builds steps are shown in the terminal.
- The last printed line should include the path where the build output has been written to (ie. `Build Output has been written to "/Path/To/BeyondDemo/bin/Release/net9.0/publish"`).
- Check the contents of the build output path: `ls bin/Release/net9.0/publish`
- It should include an XCFramework bundle called `BeyondDemoKit.xcframework`.
- Congratulations, you now have a fully functional native version of your .NET library that can be consumed by macOS and iOS Xcode projects.


### Using the generated XCFramework

Now that we have an XCFramework containing binaries for macOS and iOS, we can integrate it into an Xcode project.

- Open Xcode.
- Go to `File - New - Project...`.
- Select the `Multiplatform` tab.
- Select `App` and click `Next`.
- Enter `BeyondDemoApp` in the `Product Name` text field and click `Next`.
- Select the `BeyondDemo` folder that also contains the .NET project.
- Select the project (`BeyondDemoApp`) in Xcode's project navigator (sidebar).
- Ensure the `BeyondDemoApp` target is selected under `Targets` in the sidebar.
- Select the `General` tab.
- Under `Frameworks, Libraries and Embedded Content`, click the `+` button.
- Select `Add Other... - Add Files...`.
- Navigate one level up in the file picker, then go to `bin/Release/net9.0/publish` (depending on your output path).
- Select `BeyondDemoKit.xcframework`.
- The XCFramework should now show up and it should already be configured to `Embed & Sign`.
- Select `ContentView.swift` in the project navigator.
- Replace the whole contents of the file with this:

```swift
import SwiftUI
import BeyondDemoKit

struct ContentView: View {
    var body: some View {
        VStack {
            Text("\(greeting(for: "You"))")
        }
    }

    func greeting(for name: String) -> String {
        do {
            // Convert the Swift String into a .NET System.String
            let nameDN = name.dotNETString()

            // Create an instance of the .NET class "Hello"
            let hello = try BeyondDemo.Hello(nameDN)

            // Get a .NET System.String containing the greeting
            let theGreetingDN = try hello.getGreeting()

            // Convert the .NET System.String to a Swift String
            let theGreeting = theGreetingDN.string()

            // Return the greeting
            return theGreeting
        } catch {
            fatalError("An error occurred: \(error.localizedDescription)")
        }
    }
}

#Preview {
    ContentView()
}
```

- Now run the app.
- You should see `Hello, You!` on the screen.
- If you do, you successfully integrated a natively compiled .NET library into a multiplatform SwiftUI project!



## Generator Configuration

The generator currently uses a configuration file where all of its options are specified.

**Currently supported configuration values:**

```json
{
  "AssemblyPath": "/Path/To/Target/.NET/Assembly.dll",

  "Build": {
      "Target": "apple-universal",

      "ProductName": "AssemblyKit",
      "ProductBundleIdentifier": "com.mycompany.assemblykit",
      "ProductOutputPath": "/Path/To/ProductOutput",

      "MacOSDeploymentTarget": "13.0",
      "iOSDeploymentTarget": "16.0",

      "DisableParallelBuild": false,
      "DisableStripDotNETSymbols": false
  },

  "CSharpUnmanagedOutputPath": "/Path/To/Generated/CSharpUnmanaged/Output_CS.cs",
  "COutputPath": "/Path/To/Generated/C/Output_C.h",
  "SwiftOutputPath": "/Path/To/Generated/Swift/Output_Swift.swift",
  "KotlinOutputPath": "/Path/To/Generated/Kotlin/Output_Kotlin.kt",

  "KotlinPackageName": "com.mycompany.mypackagename",
  "KotlinNativeLibraryName": "NativeAssemblyName",

  "EmitUnsupported": false,
  "GenerateTypeCheckedDestroyMethods": false,
  "EnableGenericsSupport": false,
  "DoNotGenerateSwiftNestedTypeAliases": false,
  "DoNotGenerateDocumentation": false,
  "DoNotDeleteTemporaryDirectories": false,

  "IncludedTypeNames": [
      "IncludedTypeName",
      "AnotherIncludedTypeName"
  ],

  "ExcludedTypeNames": [
      "ExcludedTypeName",
      "AnotherExcludedTypeName"
  ],

  "ExcludedAssemblyNames": [
    "Assembly1, Version=4.2.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed",
    "Assembly2"
  ],

  "AssemblySearchPaths": [
      "/Path/To/Assemblies",
      "/Another/Path/To/Assemblies"
  ]
}
```

- **`AssemblyPath`**: Enter the path to the compiled .NET assembly you want to generate native bindings for. (Required)
- **`Build`**: Configuration options for automatic build support. (Optional; automatic build is disabled if not provided)
    - **`Target`**: The platform and architecture to build for. (Required; currently `apple-universal`, `macos-universal` and `ios-universal` are supported)
    - **`ProductName`**: The name of the resulting XCFramework and Swift/Clang module. This must be different than the target assembly name and any namespaces contained within it or its dependencies. (Optional; if not provided the assembly file name suffixed with `Kit` is used)
    - **`ProductBundleIdentifier`**: The bundle identifier of the resulting frameworks. (Optional; if not provided the bundle identifier is `com.mycompany.` suffixed with the `ProductName`)
    - **`ProductOutputPath`**: The output path for the resulting XCFramework. (Optional; if not provided, the directory of the `AssemblyPath` is used)
    - **`MacOSDeploymentTarget`**: The deployment target for the macOS portion of the XCFramework. (Optional; if not provided, `13.0` is used)
    - **`iOSDeploymentTarget`**: The deployment target for the iOS portion of the XCFramework. (Optional; if not provided, `16.0` is used)
    - **`DisableParallelBuild`**: Set to `true` to disable building in parallel (ie. for improved debugging). (Optional; if not provided, `false` is used)
    - **`DisableStripDotNETSymbols`**: Set to `true` to disable stripping .NET symbols (ie. for improved debugging). (Optional; if not provided, `false` is used)
- **`CSharpUnmanagedOutputPath`**: The generator will use this path to write the file containing the C# wrapper methods. (Required if `Build` is disabled; Optional if `Build` is enabled)
- **`COutputPath`**: The generator will use this path to write the generated C bindings header file. (Required if `Build` is disabled; Optional if `Build` is enabled)
- **`SwiftOutputPath`**: The generator will use this path to write the generated Swift bindings file. (Optional)
- **`KotlinOutputPath`**: The generator will use this path to write the generated Kotlin bindings file. (Optional)
- **`KotlinPackageName`**: When generating Kotlin code, this will be used as the package name for the generated code. (Optional, but highly recommended when targeting Kotlin. If not provided, a package name will be generated based on the assembly name)
- **`KotlinNativeLibraryName`**: When generating Kotlin code, this will be used to load the native library. (Optional, only when not targeting Kotlin. When targeting Kotlin it must be provided unless automatic builds are enabled. In this case we can infer the native library name. That's an unsupported scenario at the moment though, so right now it IS required.)
- **`EmitUnsupported`** (Boolean; `false` by default): If enabled (`true`), comments will be generated in the output files explaining why a binding for a certain type or API was not generated.
- **`GenerateTypeCheckedDestroyMethods`** (Boolean; `false` by default): If enabled (`true`), the generated `*_Destroy` methods will check the type of the passed in object. If the type does not match, an unhandled(!) exception will be thrown. Use this to detect memory management bugs in your code. Since it introduces overhead, it's disabled by default. Also, there's no need for manual memory management in higher level languages like Swift so this is unnecessary.
- **`EnableGenericsSupport`** (Boolean; `false` by default): Generics support is currently experimental and disabled by default. If you want to test the current state though or work on improving generics support, enable this by setting it to `true`.
- **`DoNotGenerateSwiftNestedTypeAliases`** (Boolean; `false` by default): If set to `true`, no typealiases matching the .NET namespaces of the generated types will be emitted. That means, for example that instead of `System.String.empty` you'd have to use `System_String.empty`.
- **`DoNotGenerateDocumentation`** (Boolean; `false` by default): If set to `true`, no documentation is extracted from .NET XML documentation files and no documentation is generated in the resulting bindings.
- **`DoNotDeleteTemporaryDirectories`** (Boolean; `false` by default): If set to `true`, any temporary directories created during the generation or build process are not deleted automatically.
- **`IncludedTypeNames`** (Array of Strings): Use this to provide a list of types that should be included even if they are not used by the target assembly.
- **`ExcludedTypeNames`** (Array of Strings): Use this to provide a list of types that should be excluded.
- **`ExcludedAssemblyNames`** (Array of Strings): Use this to provide a list of [assembly names](https://learn.microsoft.com/dotnet/api/system.reflection.assemblyname#remarks) whose types should be excluded.
  Simple names (e.g. `MyAssembly`) loosely match any assembly with that name, regardless of its version, culture or signing keys. Fully-qualified names (e.g. `MyAssembly, Version=1.2.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed`) match strictly that exact assembly identity.
- **`AssemblySearchPaths`** (Array of Strings): Use this to provide a list of file system paths that are included when searching for assembly references.

Note that all paths can either be absolute or relative to the working directory.


## Opaque types

Every .NET type that is not a primitive or an enum gets exposed as an "opaque type" in C. That means that a typealias for `void*` is generated for .NET classes and structs.

By itself, those opaque types are pretty useless. To actually access instance properties, call methods or do anything useful with them, you need to call one of the generated methods and pass the instance as the first (`self`) parameter.

In the Swift bindings, these opaque types are also used under the hood but not exposed to the consumer. So you can treat them as an implementation detail and use the generated APIs like regular Swift types.



## Exception Handling

While .NET has exceptions, C does not. And so, since C is the basis for all other language bindings we have to get creative to support catching exceptions in C.

Because pretty much everything in .NET can throw and there are no guarantees about what can and what can't throw this is actually a pretty big deal.

The way we solved it is to have an "out" parameter (double pointer in C) appended to almost every .NET API that we generate bindings for. Then, in the implementation, we wrap the actual method call in a try/catch block and set the exception parameter to the caught exception or null if the method did not throw.

Currently, the only exceptions (pun intended) are field getters and setters. As far as I'm aware, these can never throw.

So here's an example of how that looks in practice:

**C#:**
```csharp
static void WriteLine(string text)
```

**C:**
```c
void WriteLine(System_String_t text, System_Exception_t* exception)
```

When calling the `WriteLine` method from C, you should provide a reference to a `System_Exception_t` object which, after the method call will either be null or contain a value which indicates the method did throw.

The code generator for Swift produces APIs annotated with the `throws` keyword so you can use Swift's native error handling when calling into .NET.

**Swift:**
```swift
func writeLine(_ text: System_String) throws
```



## Memory Management

While .NET's memory management model is based on Garbage Collection (GC), C uses manual memory management. That means, everything that is allocated on the heap must be manually freed to not cause a memory leak.

For the generated C bindings we adopt exactly that model.

There's practically only one rule regarding memory management: Every object received from .NET, no matter how it is obtained must be manually freed when not needed anymore.

The only exception to this rule are primitive types like integers, booleans, etc and enums. Those don't need to be freed.

The generator creates destructor methods for every exposed .NET type. For instance, the signature of the destructor for the `System.Guid` type looks like this: `void System_Guid_Destroy(System_Guid_t self)`.

So if you, for instance obtain a reference to a `System.Guid` object by calling the generated binding for `System.Guid.Empty` you must at some point call the destructor, otherwise you're leaking memory.

Structs or other value types and delegates are no exception to this rule. Again, the only exceptions are primitive and enums. Also, it doesn't matter if you obtain an object by calling its constructor (`*_Create` functions in C) or through other means, you always have to destroy them at some point.

When using the generated bindings for Swift, there's no need to deal with any of that. Instead we handle allocation and deallocation transparently and the standard Swift memory management rules apply. That means you can just treat .NET objects like regular Swift objects. That includes .NET delegates which are mapped to Swift closures.



## Equality

When using the C bindings, don't ever compare two pointers to .NET objects! Because of the way `GCHandle` works, you might hold two different pointers even if they actually point to the very same object.

Instead, use the bindings for `System.Object.Equals` or `System.Object.ReferenceEquals` depending on the use case.

In Swift, the `==` and `===` operators are overridden for .NET objects and call those functions respectively. So feel free to compare .NET objects in Swift like regular Swift objects.



## Properties

Because C doesn't have properties, we expose them as regular methods suffixed with `_Get` and/or `_Set`.
Here's an example in C#:

```csharp
public class PropertyTests {
    public int FavoriteNumber { get; set; }
}
```

The generated C accessors for this property look like this:

```c
int32_t PropertyTests_FavoriteNumber_Get(PropertyTests_t self, System_Exception_t* outException);
void PropertyTests_FavoriteNumber_Set(PropertyTests_t self, int32_t value, System_Exception_t*  outException);
```

In Swift, we can generate a "proper" property for the getter but since setters can't currently be marked as throwing the setter is exposed as a function suffixed with `_set`:

```swift
var favoriteNumber: Int32 { get throws }
func favoriteNumber_set(_ value: Int32) throws
```



## Type checking/casting

You can check if an instance of an object is of a certain type in C# by using the `is` keyword (ie. `if (myObj is string) ...`). Since this is implemented at the language level there's no easy way to wrap this in the generated code. Instead, we use `System.Type.IsAssignableTo` to implement a wrapper for the `is` keyword.

In C, this is exposed as the `DNObjectIs` method. As the first argument, you pass it the object you want to check and as the second argument you provide a `System.Type` object you want to compare against. The function then returns `true` or `false` depending on the result of the type check.

The same concept applies to casting using the C# `as` keyword and direct casts (ie. `var aString = (string)someObject`). In C the `as` keyword is exposed through the `DNObjectCastAs` method. Again, you call it by providing an object you want to safely cast and as the second argument you provide the type you want to cast to. If the cast succeeds, a `System.Object` of the specified type is returned or null if the cast did not succeed.

Direct casts are exposed through the `DNObjectCastTo` method. It works the same as `DNObjectCastAs` but has a third argument which might hold a `System.Exception` object if the cast failed.

In the Swift bindings, we have extension methods on `DNObject` (the base type for all generated class and struct bindings) which makes type checking/casting much easier:

```swift
let string = System.String.empty

if string.is(System.String.typeOf) {
    print("Hooray, it's a System.String.")
}

if !string.is(System.Guid.typeOf) {
    print("Yes, it's certainly not a Sytem.Guid.")
}

if let object: System.Object = string.castAs() {
    print("Hooray, a System.String is also a System.Object so the cast succeeded.")
}

if let guid: System.Guid = string.castAs() {
    print("Oh no, a System.String should not be convertible to a System.Guid. This is an error!")
}
```

There are also extensions for direct casts called `castTo`. These work the same as `castAs` but throw an error if the cast fails.



## Method overloads, Member overrides, shadowed members

Since C doesn't have the concept of inheritance, overridden and shadowed members are just redeclared for subclasses.
In Swift, overridden or shadowed members are actually generated using the `override` keyword.

Also, C doesn't support method overloading but in this case, the "fix" is not that easy.
Take the following C# type for instance:

```csharp
public static class OverloadTests
{
    public static void Print(int value) { }
    public static void Print(DateTime value) { }
    public static void Print(string value) { }
}
```

We have three methods with the same name, the same number of arguments and even the same argument name. The only difference between them is the argument type.

In C, we basically add a counter as a suffix to every overloaded method so the resulting C interfaces look like this:

```c
void OverloadTests_Print(int32_t value, System_Exception_t* outException);
void OverloadTests_Print_1(System_DateTime_t value, System_Exception_t* outException);
void OverloadTests_Print_2(System_String_t value, System_Exception_t* outException);
```

In Swift, we fortunately can do overloads just like in C# and so the Swift signatures for those functions look like this:

```swift
class func print(_ value: Int32) throws
class func print(_ value: System_DateTime) throws
class func print(_ value: System_String) throws
```

The same rules apply to shadowed members.



## .NET Object boxing

Sometimes you need to box primitives in .NET to use them in a more "generic" context. For instance, if you have an array of `System.Object`'s (`object[]`) and want to store integers (`int`, `long`, etc.) in it you need to box them. In C# this is handled transparently or explicitly if you cast for example, an `int` to an `object` (`var intAsObj = (object)5`).

In the generated bindings, you always have to do this explicitly and we provide helper functions in C and Swift for exactly that task.
For instance, to convert a C `int32_t` to a `System_Object_t` and back again you can do this:

```c
int32_t number = 5;
System_Object_t numberObj = DNObjectFromInt32(number);
int32_t numberRet = DNObjectCastToInt32(numberObj, NULL); // TODO: Error handling
```

In Swift we provide extension methods to convert back and forth between primitives and .NET objects. The same task can be achieved like this in Swift:

```swift
let number: Int32 = 5
let numberObj = number.dotNETObject()
let numberRet = try numberObj.value // Or: try numberObj.castToInt32()
```



## Delegates and Events

.NET Delegates and Events are mapped to C function pointers and Swift closures with some infrastructure around them to allow for proper memory management.


### Delegates

Here's a C# class that declares a delegate which takes and returns a string. The delegate handler can do some transformation, like uppercasing a string and return the uppercased variant.

```csharp
public static class Transformer {
  public delegate string StringTransformerDelegate(string inputString);

  public static string TransformString(
      string inputString,
      StringTransformerDelegate stringTransformer
  )
  {
      string outputString = stringTransformer(inputString);

      return outputString;
  }
}
```

The full C# type declaration is [available in the repository](Samples/Beyond.NET.Sample.Managed/Source/Transformer.cs).

Because calling this from C is quite involved, instead of listing the required code here, [here's a link](Samples/Beyond.NET.Sample.C/transform.c) to a full (commented) C program that makes use of this API.

The Swift bindings for this allow for much simpler usage:

```swift
// Create an input string and convert it to a .NET System.String
let inputString = "Hello World".dotNETString()

// Call Beyond.NET.Sample.Transformer.transformString by:
// - Providing the input string as the first argument
// - Initializing an instance of Beyond_NET_Sample_Transformer_StringTransformerDelegate by passing it a closure that matches the .NET delegate as its sole parameter
let outputString = try! Beyond.NET.Sample.Transformer.transformString(inputString, .init({ stringToTransform in
    // Take the string that should be transformed, call System.String.ToUpper on it and return it
    return try! stringToTransform.toUpper()
})).string() // Convert the returned System.String to a Swift String

// Prints "HELLO WORLD!"
print(outputString)
```

Yes, we omitted any kind of error handling and just force unwrap optionals in this example for brevity. The point here is that it's quite easy to call .NET APIs that use delegates and the whole memory management story is being taken care of by the generated bindings.

There are still some things worth noting here:
- There's a wrapper object involved for any exposed .NET delegate.
- You can keep a reference of this wrapper object around and assign or use it somewhere else. Also, when using event handlers you need it to be able to remove an event handler at a later point in time and that's when you need the delegate wrapper object.
- That's why we call `.init` as the second parameter of `transformString` instead of just passing in the closure.
- Memory management is handled transparently because behind the scenes we know when the .NET GC collects the delegate and can in turn deallocate our C and Swift wrappers when that time comes.

We won't go into the details of how that whole process works in the C bindings because we think the sample in the repository is well enough documented. In case you still find there to not be enough information on the subject, please feel free to file a Github issue.


### Events

Events work pretty much the same as delegates, except that additional APIs are generated to add and remove event handlers.

Here's a C# example using events:

```csharp
public class EventTests
{
    public delegate void ValueChangedDelegate(object sender, int newValue);

    public event ValueChangedDelegate? ValueChanged;

    private int m_value;
    public int Value
    {
        get => m_value;
        set {
            m_value = value;
            ValueChanged?.Invoke(this, value);
        }
    }
}
```

So we have a `ValueChanged` event which fires every time the `Value` property setter is called. The new (`int`) value is passed in to the event handler.

In Swift, this is how we can consume that event:

```swift
// Create an instance of Beyond.NET.Sample.EventTests
let eventTest = try! Beyond.NET.Sample.EventTests()

// Create a variable that will hold the last value passed in to our event handler
var lastValuePassedIntoEventHandler: Int32 = 0

// Create an event handler
let eventHandler = Beyond.NET.Sample.EventTests_ValueChangedDelegate { sender, newValue in
    // Remember the last value passed in here
    lastValuePassedIntoEventHandler = newValue
}

// Add the event handler
eventTest.valueChanged_add(eventHandler)

// Set a new value (our event handler will be called for this one)
try! eventTest.value_set(5)

// Remove the event handler
eventTest.valueChanged_remove(eventHandler)

// Set a another new value (our event handler will NOT be called for this one because we already removed the event handler)
try! eventTest.value_set(10)

// Prints "5"
print(lastValuePassedIntoEventHandler)
```

I guess this is pretty self explanatory. Again, for brevity we omitted error handling here. Regular Swift memory management rules apply. Most of the time you'll likely want to create an event handler, store it as a variable outside of the function's scope and unsubscribe from the event in your class's deinitializer.



## Converting between .NET and Swift types

For very common types we provide convenience extensions to convert between the two worlds.
That includes strings, dates, byte arrays (Swift `Data` objects), etc.

Here's an example that converts a `System.String` to a Swift `String` and back again:

```swift
let systemString = System.String.empty
let swiftString = systemString.string()
let systemStringRet = swiftString.dotNETString()
```



## .NET Interfaces in Swift

In Swift, .NET interfaces are exposed as protocols and .NET types that implement interfaces are generated as protocol conforming types.
Since Swift doesn't allow extending protocol metatypes, if you want to get the .NET type of a particular interface, you'll have to use `IInterfaceName_DNInterface.typeOf` instead of just `IInterfaceName.typeOf`. Apart from that, the Swift bindings for .NET interfaces should act and feel very much like native Swift protocols.



## C# `out` parameters in Swift

C# methods that include parameters marked with the `out` keyword are converted to Swift functions with parameters marked with the `inout` keyword.

This C# code...
```csharp
void ReturnIntAsOut(out int returnValue);
```

... is imported like this into Swift:
```swift
func returnIntAsOut(_ returnValue: inout Int32) throws
```

And to use it in Swift you'd do something like this:

```swift
var returnValue: Int32 = 0
try target.returnIntAsOut(&returnValue)
// returnValue now contains the value returned by .NET
```

The same concept applies to functions that return classes, structs, enums or any other type via C# `out` parameters.

Unfortunately, there's a major difference between .NET's `out` and Swift's `inout` keywords. The difference is that, as the name implies a value goes in and another value might(!) come out of a function which includes Swift's `inout` parameters. In C# however, no value enters a function with `out` parameters. This in turn means that for non-optional values, a default value has to be provided in Swift to satisfy the compiler. In many cases this is not a big deal (ie. no harm in specifying an unused default value for primitives) but there are cases where it's undesirable or flat out impossible to provide a default value. Think about a function that returns an .NET interface via an `out` parameter. You could only provide a default value if you did have access to an implementation of that interface which might not be the case. Even if you had access to such an implementation, you might not want to create an instance because it's costly.

Fortunately we can work around the limitation by letting you create "placeholder" objects explicitly for passing a temporary default value from Swift to .NET.

Consider the following C# method:

```csharp
void ReturnIEnumerableAsOut(out IEnumerable returnValue) {
    returnValue = "Abc";
}
```

It's imported like this into Swift:

```swift
func returnIEnumerableAsOut(_ returnValue: inout System.Collections.IEnumerable) throws
```

To use it you would have to specify a default value that conforms to the `System.Collections.IEnumerable` protocol/interface like so:

```swift
// System.String implements System.Collections.IEnumerable
var returnValue: System.Collections.IEnumerable = System.String.empty
try target.returnIEnumerableAsOut(&returnValue)
// returnValue now contains a .NET string with the following content: "Abc"
```

With out parameter placeholders however you can rewrite the code like this:

```swift
var returnValue = System.Collections.IEnumerable_DNInterface.outParameterPlaceholder
try target.returnIEnumerableAsOut(&returnValue)
```

Please note that the **only** valid use case for out parameter placeholders is to pass them to .NET functions with `out` parameters.
- **Do not(!)** call any APIs on the placeholder object as it will crash the program!
- **Do not(!)** use out parameter placeholders to pass a default value to .NET APIs that receive an optional(!) value as `out` parameter! In this case, just use a regular Swift optional.



## A word (or two) about generics

.NET generics are a wonderful feature. If you're not trying to expose it to other languages, that is.

Let's get the good news out before we dive into the limitations and the reasons behind those limitations: We DO have limited(!) and experimental support for .NET generics.

Generics are without a doubt the hardest .NET construct to expose to other languages. So any support in this project that involves generics should be taken with a big grain of salt.

There are basically two kinds of generics in .NET:

- Generic Methods
- Generic Types

Let's start with generic methods.
Here's a simple example of a generic method in a non-generic class:

```csharp
class GenericTests
{
  T ReturnDefaultValue<T>()
  {
    return default(T);
  }
}
```

It takes a generic parameter named `T` and returns the default value for the type of `T`.

At compile time and when generating bindings for other languages, it's impossible to know which types might be used to specialize this method. The number might be basically infinite if `T` is not constrained.

So we can't generate a binding for every single specialization. Especially when you consider that methods can have multiple generic parameters.

Instead, we need to use a more dynamic approach to support generating bindings for .NET generics in other languages, including the most restricted language, C which acts as the basis for all other language bindings.

The only viable way I found was to use reflection and, unfortunately this has many downsides.

**TODO**: Expand on generics support.



## Stable ABI/Breaking changes

We're far from the point where we can ensure that the generated code will be binary compatible from one version of the generator to the next. At least during the alpha phase, things will certainly break when upgrading from one version to the next. At a later stage of development we might introduce ABI compatibility guarantees.

Right now, expect things to break!



## Debugging with LLDB

While debugging code with LLDB you might run into situations where the [.NET code raises signals](https://github.com/dotnet/runtime/blob/main/docs/workflow/debugging/coreclr/debugging-runtime.md?plain=1#L148) which would cause the debugger to halt program execution although it's perfectly fine to continue.
To handle that, you can add a symbolic breakpoint in Xcode and configure it like this:
* Name: `Ignore_SIGUSR1`
* Symbol: `NSApplicationMain` (macOS) or `UIApplicationMain` (iOS) or `main` (C programs or XCTest bundles)
* Action: Debugger Command: `process handle SIGUSR1 -n true -p true -s false`
* Enable: `Automatically continue after evaluating actions`
Also see this [StackOverflow post](https://stackoverflow.com/questions/10431579/permanently-configuring-lldb-in-xcode-4-3-2-not-to-stop-on-signals).



## Unit Tests

We've got quite an extensive suite of unit tests. All of them are written in Swift.

To run them:
- Open [BeyondNETSamplesSwift.xcworkspace](Samples/Beyond.NET.Sample.Swift/BeyondNETSamplesSwift.xcworkspace) in Xcode.
- In the scheme selector, choose `Build .NET Stuff` and go to `Product - Build`.
- Wait until the build finishes.
- Then open the scheme selector again and this time choose `BeyondNETSampleSwift`.
- Go to `Product - Test`.



## License

The project is licensed under the [MIT license](LICENSE).



## Contributions

Needless to say, any kind of contribution to this project is very welcome!
