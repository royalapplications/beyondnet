using NativeAOT.CodeGenerator.SourceCode;
using NativeAOT.CodeGenerator.Syntax.CSharpUnmanaged;

namespace NativeAOT.CodeGenerator.Generator.CSharpUnmanaged;

public class CSharpUnmanagedCodeGenerator: ICodeGenerator
{
    public Settings Settings { get; }
    
    public CSharpUnmanagedCodeGenerator(Settings settings)
    {
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }
    
    public Result Generate(
        IEnumerable<Type> types,
        Dictionary<Type, string> unsupportedTypes,
        SourceCodeWriter writer
    )
    {
        SourceCodeSection headerSection = writer.AddSection("Header");
        SourceCodeSection sharedCodeSection = writer.AddSection("Shared Code");
        SourceCodeSection unsupportedTypesSection = writer.AddSection("Unsupported Types");
        SourceCodeSection apisSection = writer.AddSection("APIs");

        string header = GetHeaderCode();
        headerSection.Code.AppendLine(header);

        string sharedCode = GetSharedCode();
        sharedCodeSection.Code.AppendLine(sharedCode);

        if (Settings.EmitUnsupported) {
            foreach (var kvp in unsupportedTypes) {
                Type type = kvp.Key;
                string reason = kvp.Value;
    
                string typeName = type.FullName ?? type.Name;
    
                unsupportedTypesSection.Code.AppendLine($"// Unsupported Type \"{typeName}\": {reason}");
            }
        } else {
            unsupportedTypesSection.Code.AppendLine("// Omitted due to settings");
        }

        CSharpUnmanagedTypeSyntaxWriter typeSyntaxWriter = new(Settings);

        Result result = new();
        
        foreach (Type type in types) {
            Syntax.State state = new() {
                Settings = Settings
            };
            
            string typeCode = typeSyntaxWriter.Write(type, state);
            
            apisSection.Code.AppendLine(typeCode);
            
            result.AddGeneratedType(
                type,
                state.GeneratedMembers
            );
        }

        return result;
    }

    private string GetHeaderCode()
    {
        return $"""
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace {Settings.NamespaceForGeneratedCode};

""";
    }

    private string GetSharedCode()
    {
        return """
#if NETSTANDARD2_0 ||  NETCOREAPP2_0 ||  NETCOREAPP2_1 ||  NETCOREAPP2_2 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
public static class ConditionalWeakTable_Extensions
{
    public static void AddOrUpdate<TKey, TValue>(
        this ConditionalWeakTable<TKey, TValue> conditionalWeakTable,
        TKey key,
        TValue value
    ) 
        where TKey: class
        where TValue: class
    {
        conditionalWeakTable.Remove(key);
        conditionalWeakTable.Add(key, value);
    }
}
#endif

internal unsafe class NativeDelegateBox<TDelegateType, TFunctionPointerType>
    where TDelegateType: Delegate
    where TFunctionPointerType: unmanaged
{
    internal TDelegateType Trampoline { get; }
    internal void* Context { get; }
    internal TFunctionPointerType FunctionPointer { get; }

    internal NativeDelegateBox(
        TDelegateType trampoline,
        void* context,
        TFunctionPointerType? functionPointer
    )
    {
        Trampoline = trampoline ?? throw new ArgumentNullException(nameof(trampoline));
        Context = context is not null ? context : throw new ArgumentNullException(nameof(context));
        FunctionPointer = functionPointer ?? throw new ArgumentNullException(nameof(functionPointer));
    }
}

internal static unsafe class InteropUtils
{
    #region Allocation
    internal static GCHandle AllocateGCHandle(this object instance, GCHandleType handleType)
    {
        GCHandle handle = GCHandle.Alloc(instance, handleType);

        return handle;
    }
    
    internal static void* AllocateGCHandleAndGetAddress(this object? instance)
    {
        if (instance is null) {
            return null;
        }
        
        GCHandle handle = instance.AllocateGCHandle(GCHandleType.Normal);
        void* handleAddress = handle.ToHandleAddress();

        return handleAddress;
    }
    #endregion Allocation

    #region Free
    internal static void FreeIfAllocated(void* handleAddress)
    {
        if (handleAddress is null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);
        
        handle?.FreeIfAllocated();
    }

    internal static void CheckedFreeIfAllocated<T>(void* handleAddress)
    {
        if (handleAddress is null) {
            return;
        }

        GCHandle? handle = GetGCHandle(handleAddress);

        if (handle is null) {
            return;
        }

        object? target = handle?.Target;

        if (target is not null &&
            !(target is T)) {
            throw new Exception("Type of handle is unexpected");
        }
        
        handle?.FreeIfAllocated();
    }
    
    internal static void FreeIfAllocated(this GCHandle handle)
    {
        if (!handle.IsAllocated) {
            return;
        }

        handle.Free();
    }
    #endregion Free

    #region Handle Address/GCHandle <-> Object Conversion
    internal static void* ToHandleAddress(this GCHandle handle)
    {
        void* handleAddress = (void*)GCHandle.ToIntPtr(handle);

        return handleAddress;
    }

    internal static GCHandle? GetGCHandle(void* handleAddress)
    {
        if (handleAddress is null) {
            return null;
        }
        
        GCHandle handle = GCHandle.FromIntPtr((nint)handleAddress);

        return handle;
    }

    internal static T? GetInstance<T>(void* handleAddress)
    {
        GCHandle? handle = GetGCHandle(handleAddress);

        object? target = handle?.Target;

        if (target is null) {
            return default;
        }

        T instance = (T)target;

        return instance;
    }
    #endregion Handle Address/GCHandle <-> Object Conversion

    #region Strings
    /// <summary>
    /// This allocates a native char* and copies the contents of the managed string into it.
    /// The allocated native string must be freed when not needed anymore!
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = "DNStringToC")]
    internal static byte* DNStringToC(void* /* System.String? */ systemString)
    {
        if (systemString is null) {
            return null;
        }

        System.String? systemStringConverted = InteropUtils.GetInstance<System.String>(systemString);

        if (systemStringConverted is null) {
            return null;
        }

        byte* cString = (byte*)Marshal.StringToHGlobalAuto(systemStringConverted);
        
        return cString;
    }

    /// <summary>
    /// This allocates a managed string and copies the contents of the native char* into it.
    /// </summary>
    [UnmanagedCallersOnly(EntryPoint = "DNStringFromC")]
    internal static void* /* System.String? */ DNStringFromC(byte* cString)
    {
        if (cString is null) {
            return null;
        }
        
        System.String? systemString = Marshal.PtrToStringAuto((nint)cString);

        if (systemString is null) {
            return null;
        }
        
        void* systemStringNative = systemString.AllocateGCHandleAndGetAddress();

        return systemStringNative;
    }
    #endregion Strings

    #region Bools
    internal static byte ToCBool(this bool @bool)
    {
        if (@bool) {
            return 1;
        } else {
            return 0;
        }
    }

    public static bool ToBool(this byte cBool)
    {
        return cBool == 1;
    }
    #endregion Bools
}
""";
    }
}