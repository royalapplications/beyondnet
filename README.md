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

Since new C# code is generated as part of the language bindings, it's required to either include the single generated C# source code file in the existing .NET project you're targeting or create a new project soley for the purpose of compiling a native version of the assembly. We recommend the latter as you currently need to compile your project using NativeAOT to actually take advantage of the generated bindings and that way, the original assembly stays unmodified.

It's important to note that while Beyond.NET generates code for you, it doesn't compile it. You'll have to do that yourself.



## Quick Start Guide

### Prerequisites
- Make sure [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) is installed.
- On macOS, make sure [Xcode](https://developer.apple.com/xcode/) is installed.
- On Linux, make sure clang and zlib are installed


### Generator Executable
- Either clone the Beyond.NET repository or download one of the pre-built generator executables for your platform.
- If you do not have a pre-compiled executable of the generator, compile it by either running `dotnet publish` within its directory or use one of our provided publish scripts like `publish_macos_universal` for compiling a universal macOS binary.
- Open a terminal, switch to the directory containing the built executable and execute the generator (`./beyondnetgen`).
- Since you've provided no arguments, the generator should show its usage screen.


### Configuration
- Currently, the generator takes a single required argument: `PathToConfig.json`.
- Create a config file. See [Generator Config](#generator-config) for an example and the supported config values.
- Run the generator with the path to the config file as the first and only argument (`./beyondnetgen /Path/To/Config.json`).
- If the generator was successful it will exit with 0 as exit code and not print anything to stdout or stderr.
- If errors were encountered they'll appear in the terminal.


### Build a native version of the target .NET assembly
- Let's assume the assembly you're creating native bindings for is called `MyLib` (`MyLib.dll`).
- Create a new .NET class lib project (ie. `mkdir MyLibNative && cd MyLibNative && dotnet new classlib`).
- Either copy the generated .cs file containing the unmanaged C# bindings to the new project's directory or adjust the path in the generator config file to point to your new project's path.
- Open `MyLibNative.csproj` in a text editor.
- Make sure `TargetFramework` is set to `net8.0`.
- Set `AllowUnsafeBlocks` to `true` as the generated bindings use unsafe code (ie. `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>`).
- Add a project reference to the original .NET assembly (ie. `<ProjectReference Include="..\MyLib.csproj" />`).
- Set `PublishAot` to `true` (ie. `<PublishAot>true</PublishAot>`).
- Set `RuntimeIdentifier` or `RuntimeIdentifiers` to the platforms you're targeting (ie. `<RuntimeIdentifiers>osx-x64;osx-arm64</RuntimeIdentifiers>`).
- Note that .NET does not support multi-architecture builds out of the box. If you want to create a universal macOS dylib, you will need to do two separate builds, then merge them using the `lipo` CLI tool.
- Also, the install name of dylibs created by .NET's NativeAOT compiler needs to adjusted from `/usr/lib/MyLibNative.dylib` to `@rpath/MyLibNative.dylib`.
- There's a sample publish script which does all of this in the repository called `publish_macos_universal`. You can use this as the basis for your build. Just make sure to adjust the `OUTPUT_PRODUCT_NAME` variable to match the assembly name of your .NET library (`MyLib`).
- Run the publish script (ie. `./publish_macos_universal`).
- On macOS this will produce a universal `MyNativeLib.dylib` in the bin directory under `bin/Release/net8.0/osx-universal/publish`.


### Use generated bindings from Swift
- Create a macOS App Xcode project.
- Add the generated C bindings header file (ie. `Output_C.h`) and the generated Swift bindings file (ie. `Output_Swift.swift`) to the project.
- Create an Objective-C bridging header.
  - You can either just add a temporary Objective-C class (you can delete it later) to trigger the creation of the bridging header or create an empty header file and adjust the "Objective-C Bridging Header" build setting of the Xcode project to point to that header file.
- In the briding header, import the generated C bindings header file (ie. `#import "Output_C.h"`).
- You're now ready to call any of the APIs that bindings were generated for.
- Please note that since C and Swift do not have support for namespaces, all generated types will have their namespace prefixed. `System.Guid.NewGuid` for instance gets generated as `System_Guid.newGuid` in Swift and `System_Guid_NewGuid` in C. However, by default, the Swift code generator will also produces typealiases in nested types that basically restore the .NET namespaces by emulating them using nested types in Swift. So you can in fact use `System.Guid.newGuid` in Swift. Hooray!



## Generator Configuration

The generator currently uses a configuration file where all of its options are specified.

**Minimal Example:**

```json
{
  "AssemblyPath": "/Path/To/Target/.NET/Assembly.dll",

  "CSharpUnmanagedOutputPath": "/Path/To/Generated/CSharpUnmanaged/Output_CS.cs",
  "COutputPath": "/Path/To/Generated/C/Output_C.h",
  "SwiftOutputPath": "/Path/To/Generated/Swift/Output_Swift.swift"
}
```

- `AssemblyPath`: Enter the path to the compiled .NET assembly you want to generate native bindings for. (Required)
- `CSharpUnmanagedOutputPath`: The generator will use this path to write the generated file containing the C# wrapper methods. (Required)
- `COutputPath`: The generator will use this path to write the generated C bindings header file. (Required)
- `SwiftOutputPath`: The generator will use this path to write the generated Swift bindings file. (Optional)
- All paths can either be absolute or relative to the config file.

There are several other optional options that control the behavior of the generator:

- `EmitUnsupported` (Boolean; `false` by default): If enabled (`true`), comments will be generated in the output files explaining why a binding for a certain type or API was not generated.
- `GenerateTypeCheckedDestroyMethods` (Boolean; `false` by default): If enabled (`true`), the generated `*_Destroy` methods will check the type of the passed in object. If the type does not match, an unhandled(!) exception will be thrown. Use this to detect memory management bugs in your code. Since it introduces overhead, it's disabled by default. Also, there's no need for manual memory management in higher level languages like Swift so this is unnecessary.
- `DoNotGenerateSwiftNestedTypeAliases` (Boolean; `false` by default): If set to `true`, no typealiases matching the .NET namespaces of the generated types will be emitted. That means, for example that instead of `System.String.empty` you'd have to use `System_String.empty`.
- `EnableGenericsSupport` (Boolean; `false` by default): Generics support is currently experimental and disabled by default. If you want to test the current state though or work on improving generics support, enable this by setting it to `true`.
- `IncludedTypeNames` (Array of Strings): Use this to provide a list of types that should be included even if they are not used by the target assembly.
- `ExcludedTypeNames` (Array of Strings): Use this to provide a list of types that should be excluded.
- `AssemblySearchPaths` (Array of Strings): Use this to provide a list of file system paths that are included when searching for assembly references.



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
void Namespace_WriteLine(System_String_t text, System_Exception_t* exception)
```

When calling the `WriteLine` method from C, you should provide a reference to a `System_Exception_t` object which, after the method call will either be null or contain a value which indicates the method did throw.

The code generator for Swift produces APIs annotated with the `throws` keyword so you can use Swift's native error handling when calling into .NET.



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

When using the C bindings, don't ever compare two pointers to .NET objects. Because of the way `GCHandle` works, you might hold two different pointers even if they actually point to the very same object.

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

In C, this is exposed as the `DNObjectIs` method. As the first argument, you pass it the object you want to check and as the second argument you provide a `System.Type` object you want to compare against. The function then returns true or false depending on the result of the type check.

The same concept applies to casting using the C# `as` keyword and direct casts (ie. `var aString = (string)someObject`). In C the `as` keyword is exposed through the `DNObjectCastAs` method. Again, you call it by providing an object you want to safely cast and as the second argument you provide the type you want to cast to. If the cast succeeds, a `System.Object` of the specified type is returned or null if the cast did not succeed.

Direct casts are exposed through the `DNObjectCastTo` method. It works the same as `DNObjectCastAs` but has a third argument which might hold a `System.Exception` object if the cast failed.

In the Swift bindings, we have extension methods on `DNObject` (the base type for all generated class and struct bindings) which makes type checking/casting much easier.



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
class func print(_ value: System_DateTime?) throws
class func print(_ value: System_String?) throws
```

The same rules apply to shadowed members.



## .NET Object boxing

Sometimes you need to box primitives in .NET to use them in a more "generic" context. For instance, if you have an array of `System.Object`'s (`object[]`) and want to store integers (`int`, `long`, etc.) in it you need to box them. In C# this is handled transparently or explicitly if you cast for example, an `int` to an `object` (`var intAsObj = (object)5`).

In the generated bindings, you always have to do this explicitly and we provide helper functions in C and Swift for that task.
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
let numberRet = try numberObj.castToInt32()
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
    return try! stringToTransform!.toUpper()
}))!.string() // Convert the returned System.String to a Swift String

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
let eventTest = try! Beyond.NET.Sample.EventTests()!

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

I guess this is pretty self explanatory. Again, for brevity we omitted error handling here. Regular Swift memory management rules apply here. Most of the time you'll likely want to create an event handler, store it as a variable outside of the function's scope and unsubscribe from the event in your class's deinitializer.



## Converting between .NET and Swift types

For very common types we provide convenience extensions to convert between the two worlds.
That includes strings, dates, byte arrays (Swift `Data` objects), etc.

Here's an example that converts a `System.String` to a Swift `String` and back again:

```swift
let systemString = System.String.empty!
let swiftString = systemString.string()
let systemStringRet = swiftString.dotNETString()
```



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

While debugging code with LLDB you might run into situations where the .NET code raises signals which would cause the debugger to halt program execution although it's perfectly fine to continue.
To handle that, you can add a symbolic breakpoint in Xcode and configure it like this:
* Name: `Ignore_SIGUSR1`
* Symbol: `NSApplicationMain`
* Action: Debugger Command: `process handle SIGUSR1 -n true -p true -s false`
* Enable: `Automatically continue after evaluating actions`
Also see this [StackOverflow post](https://stackoverflow.com/questions/10431579/permanently-configuring-lldb-in-xcode-4-3-2-not-to-stop-on-signals).



## License

The project is licensed under the [MIT license](LICENSE).



## Contributions

Needless to say, any kind of contribution to this project is very welcome!