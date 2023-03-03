using System;
using NativeAOT.Core;

namespace NativeAOT.Bindings.System;

[NativeExport(
    ManagedType = typeof(object), 
    CName = "System_Object_t"
)]
public abstract class System_Object
{
    [NativeExport(
        CName = "System_Object_Create"
    )]
    public System_Object() { }
    
    [NativeExport(
        CName = "System_Object_ToString",
        NameOfManagedMember = nameof(object.ToString)
    )]
    public new abstract string? ToString();
}