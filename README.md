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
So if you're, for instance targeting Swift the call tree looks like this: Swift -> C -> C# APIs marked with the `UnmanagedCallersOnly` attribute -> Original C# API.


## Memory Management

While .NET's memory management model is based on Garbage Collection (GC), C uses manual memory management. That means, everything that is allocated on the heap must be manually freed to not cause a memory leak.

For the generated C bindings we adopt exactly that model.

There's practically only one rule regarding memory management: Every object received from .NET, no matter how it is obtained must be manually freed when not needed anymore.

The only exception to this rule are primitive types like integers, booleans, etc and enums. Those don't need to be freed.

The generator creates destructor methods for every exposed .NET type. For instance, the signature of the destructor for the `System.Guid` type looks like this: `void System_Guid_Destroy(System_Guid_t self)`.

So if you, for instance obtain a reference to a System.Guid object by calling the generated binding for `System.Guid.Empty` you must at some point call the destructor, otherwise you're leaking memory.

Structs or other value types and delegates are no exception to this rule. Again, the only exception are primitive and enums. Also, it doesn't matter if you obtain an object by calling its constructor (`*_Create` functions in C) or through other means, you always have to destroy them at some point.

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