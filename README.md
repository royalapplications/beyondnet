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

Since new C# code is generated as part of the language bindings, it's required to either include the single generated C# source code file in the existing .NET project you're targeting or create a new project soley for the purpose of compiling a native version of the assembly. We recommend the latter as you will need to compile your project using NativeAOT to actually take advantage of the generated bindings and that way, the original assembly stays unmodified.

It's important to note that while Beyond.NET generates code for you, it doesn't compile it. You'll have to do that yourself.


## Quick Start Guide

### Prerequisites
- Make sure [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) is installed.
- On macOS, make sure [Xcode](https://developer.apple.com/xcode/) is installed.
- On Linux, make sure clang and zlib are installed

### Generator Executable
- Either clone the Beyond.NET repository or download one of the pre-built generator executables for your platform.
- If you do not have a pre-compiled executable of the generator, compile it by either running `dotnet publish` within its directory or use one of our provided publish scripts like `publish_macos_universal` for compiling a universal macOS binary.
- Open a terminal and execute the generator (`./beyondnetgen`).
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
- On macOS this will produce a `MyNativeLib.dylib` in the bin directory under `bin/Release/net8.0/osx-universal/publish`.

### Use generated bindings from Swift
- Create a macOS App Xcode project.
- Add the generated C bindings header file (ie. `Output_C.h`) and the generated Swift bindings file (ie. `Output_Swift.swift`) to the project.
- Create an Objective-C bridging header.
  - You can either just add a temporary ObjC class (you can delete it later) to trigger the creation of the bridging header or create an empty header file and adjust the "Objective-C Bridging Header" build setting of the Xcode project to point to that header file.
- In the briding header import the generated C bindings header file (ie. `#import "Output_C.h"`).
- You're now ready to call any of the APIs that bindings were generated for.
- Please note that since Swift does not have support for namespaces, all generated types will have their namespace prefixed. `System.Guid.NewGuid` for instance gets generated as `System_Guid.newGuid` in Swift and `System_Guid_NewGuid` in C.


## Generator Configuration

The generator currently uses a configuration file where all of its options are specified.

**Minimal example:**

```
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

- EmitUnsupported (Boolean; false by default): If enabled (true), comments will be generated in the output files explaining why a binding for a certain type or API was not generated.
- GenerateTypeCheckedDestroyMethods (Boolean; false by default): If enabled (true), the generated `*_Destroy` methods will check the type of the passed in object. If the type does not match, an unhandled(!) exception will be thrown. Use this to detect memory management bugs in your code. Since it introduces overhead, it's disabled by default. Also, there's no need for manual memory management in higher level languages like Swift so this is unnecessary.
- EnableGenericsSupport (Boolean; false by default): Generics support is currently experimental and disabled by default. If you want to test the current state though or work on improving generics support, enable this by setting it to `true`.
- IncludedTypeNames (Array of Strings): Use this to provide a list of types that should be included even if they are not used by the target assembly.


# Exception Handling

While .NET has exceptions, C does not. And so, since C is the basis for all other language bindings we have to get creative to support catching exceptions in C.

Because pretty much everything in .NET can throw and there are no guarantees about what can and what can't throw this is actually a pretty big deal.

The way we solved it is to have an "out" parameter (double pointer in C) appended to almost every .NET API that we generate bindings for. Then, in the implementation, we wrap the actual method call in a try/catch block and set the exception parameter to the caught exception or null if the method did not throw.

Currently, the only exceptions (pun intended) are field getters and setters. As far as I'm aware, these can never throw.

So here's an example of how that looks in practice:

**C#:**
```
static void WriteLine(string text)
```

**C:**
```
void Namespace_WriteLine(System_String_t text, System_Exception_t* exception)
```

When calling the `WriteLine` method from C, you should provide a reference to a `System_Exception_t` object which, after the method call will either be null or contain a value which indicates the method did throw.

The code generator for Swift produces APIs annotated with the throws keyword so you can use Swift's native error handling when calling into .NET.


## Memory Management

While .NET's memory management model is based on Garbage Collection (GC), C uses manual memory management. That means, everything that is allocated on the heap must be manually freed to not cause a memory leak.

For the generated C bindings we adopt exactly that model.

There's practically only one rule regarding memory management: Every object received from .NET, no matter how it is obtained must be manually freed when not needed anymore.

The only exception to this rule are primitive types like integers, booleans, etc and enums. Those don't need to be freed.

The generator creates destructor methods for every exposed .NET type. For instance, the signature of the destructor for the `System.Guid` type looks like this: `void System_Guid_Destroy(System_Guid_t self)`.

So if you, for instance obtain a reference to a System.Guid object by calling the generated binding for `System.Guid.Empty` you must at some point call the destructor, otherwise you're leaking memory.

Structs or other value types and delegates are no exception to this rule. Again, the only exceptions are primitive and enums. Also, it doesn't matter if you obtain an object by calling its constructor (`*_Create` functions in C) or through other means, you always have to destroy them at some point.

When using the generated bindings for Swift, there's no need to deal with any of that. Instead we handle allocation and deallocation transparently and the standard Swift memory management rules apply. That means you can just treat .NET objects like regular Swift objects. That includes .NET delegates which are mapped to Swift closures.


## Equality

When using the C bindings, don't ever compare two pointers to .NET objects. Because of the way `GCHandle` works, you might hold two different pointers even if they actually point to the very same object.

Instead, use the bindings for `System.Object.Equals` or `System.Object.ReferenceEquals` depending on the use case.

In Swift, the `==` and `===` operators are overridden for .NET objects and call those functions respectively. So feel free to compare .NET objects in Swift like regular Swift objects.


## A word (or two) about generics

.NET generics are a wonderful feature. If you're not trying to expose it to other languages, that is.

Let's get the good news out before we dive into the limitations and the reasons behind those limitations: We DO have limited(!) and experimental support for .NET generics.

Generics are without a doubt the hardest C# language construct to expose to other languages. So any support in this project that involves generics should be taken with a big grain of salt.

There are basically two kinds of generics in .NET:

- Generic Methods
- Generic Types

Let's start with generic methods.
Here's a simple example of a generic method in a non-generic class:

```
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

**To-do**: Expand on generics...

## Debugging with LLDB

While debugging code with LLDB you might run into situations where the .NET code raises signals which would cause the debugger to halt program execution although it's perfectly fine to continue.
To handle that, you can add a symbolic breakpoint in Xcode and configure it like this:
* Name: `Ignore_SIGUSR1`
* Symbol: `NSApplicationMain`
* Action: Debugger Command: `process handle SIGUSR1 -n true -p true -s false`
* Enable: `Automatically continue after evaluating actions`
Also see this [StackOverflow post](https://stackoverflow.com/questions/10431579/permanently-configuring-lldb-in-xcode-4-3-2-not-to-stop-on-signals).