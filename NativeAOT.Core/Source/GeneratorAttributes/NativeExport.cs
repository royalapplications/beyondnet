using System;

namespace NativeAOT.Core;

[AttributeUsage(
    AttributeTargets.Enum |
    AttributeTargets.Class |
    AttributeTargets.Constructor | 
    AttributeTargets.Event |
    AttributeTargets.Method |
    AttributeTargets.Property |
    AttributeTargets.Struct |
    AttributeTargets.Interface)]
public class NativeExport: Attribute
{
    public Type ManagedType { get; set; }
    public string NameOfManagedMember { get; set; }
    public bool Throwing { get; set; }

    public string CName { get; set; }

    // public string Name { get; set; }

    // public CExport(string name)
    // {
    //     Name = name ?? throw new ArgumentNullException(nameof(name));
    // }
}