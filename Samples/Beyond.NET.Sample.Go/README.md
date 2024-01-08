# Beyond.NET with golang

This sample currently only works on macOS because it uses parts of the resulting xcframework when compiling with `Beyond.NET.Sample_BuildConfig.json`.
Obviously we can do the .NET/Native build as part of the `build` script in this directory but haven't done so yet.
Also, the sample currently uses manual bindings for the C generated stuff as the generator is not yet able to generate golang code.
