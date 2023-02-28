using System;

namespace NativeAOT.Generator;

internal readonly struct NativeClass
{
    public string Name { get; }

    public string NameWithUnderscores
    {
        get {
            return Name.Replace(".", "_");
        }
    }

    public NativeClass(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}