### Build a native version of the target .NET assembly

- Let's assume the assembly you're creating native bindings for is called `MyLib` (`MyLib.dll`).
- Create a new .NET class lib project (ie. `mkdir MyLibNative && cd MyLibNative && dotnet new classlib`).
- Either copy the generated .cs file containing the unmanaged C# bindings to the new project's directory or adjust the path in the generator config file to point to your new project's path.
- Open `MyLibNative.csproj` in a text editor.
- Make sure `TargetFramework` is set to `net9.0`.
- Set `AllowUnsafeBlocks` to `true` as the generated bindings use unsafe code (ie. `<AllowUnsafeBlocks>true</AllowUnsafeBlocks>`).
- Add a project reference to the original .NET assembly (ie. `<ProjectReference Include="..\MyLib.csproj" />`).
- Set `PublishAot` to `true` (ie. `<PublishAot>true</PublishAot>`).
- Set `RuntimeIdentifier` or `RuntimeIdentifiers` to the platforms you're targeting (ie. `<RuntimeIdentifiers>osx-x64;osx-arm64</RuntimeIdentifiers>`).
- Note that .NET does not support multi-architecture builds out of the box. If you want to create a universal macOS dylib, you will need to do two separate builds, then merge them using the `lipo` CLI tool.
- Also, the install name of dylibs created by .NET's NativeAOT compiler needs to adjusted from `/usr/lib/MyLibNative.dylib` to `@rpath/MyLibNative.dylib`.
- There's a sample publish script which does all of this in the repository called `publish_macos_universal`. You can use this as the basis for your build. Just make sure to adjust the `OUTPUT_PRODUCT_NAME` variable to match the assembly name of your .NET library (`MyLib`).
- Run the publish script (ie. `./publish_macos_universal`).
- On macOS this will produce a universal `MyNativeLib.dylib` in the bin directory under `bin/Release/net9.0/osx-universal/publish`.


### Use generated bindings from Swift

- Create a macOS App Xcode project.
- Add the generated C bindings header file (ie. `Output_C.h`) and the generated Swift bindings file (ie. `Output_Swift.swift`) to the project.
- Create an Objective-C bridging header.
  - You can either just add a temporary Objective-C class (you can delete it later) to trigger the creation of the bridging header or create an empty header file and adjust the "Objective-C Bridging Header" build setting of the Xcode project to point to that header file.
- In the briding header, import the generated C bindings header file (ie. `#import "Output_C.h"`).
- Open the project settings and in the "General" tab, under "Frameworks, Libraries and Embedded Content" click the "+" button
    - Select "Add other... - Add files..." and select the native dylib (ie. `MyNativeLib.dylib`)
- Try to build the project. If it fails, you might need to adjust either your header or library search paths in the project settings.
    - If Xcode fails to link the native dylib (the error looks something like this: `Library not found for -lMyNativeLib`) you need to adjust "Build Settings - Library Search Paths" in you project settings to point to the path where the library is located.
        - For example, if you the native library is located one level below your Xcode project in a folder called "MyLibNative/bin/Release/net9.0/osx-universal/publish" use this: `$(PROJECT_DIR)/../MyLibNative/bin/Release/net9.0/osx-universal/publish`)
    - If Xcode complains about being unable to find the generated C header, adjust "Build Settings - Header Search Paths" to point to the path where the generated header is located.
        - For example, if your header is one level below your Xcode project in a folder called "Generated" use this: `$(PROJECT_DIR)/../Generated`
- You're now ready to call any of the APIs that bindings were generated for.
- As a simple test, open `AppDelegate.swift` and in `applicationDidFinishLaunching` add this code: `print((try? System.DateTime.now?.toString()?.string()) ?? "Error")`
    - When you run the app, you should see the current date and time printed to the console.
- Please note that since C and Swift do not have support for namespaces, all generated types will have their namespace prefixed. `System.Guid.NewGuid` for instance gets generated as `System_Guid.newGuid` in Swift and `System_Guid_NewGuid` in C. However, by default, the Swift code generator will also produces typealiases in nested types that basically restore the .NET namespaces by emulating them using nested types in Swift. So you can in fact use `System.Guid.newGuid` in Swift. Hooray!