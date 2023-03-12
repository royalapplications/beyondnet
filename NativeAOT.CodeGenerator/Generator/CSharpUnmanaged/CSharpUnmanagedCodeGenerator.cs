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
    
    public void Generate(
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
        
        foreach (var type in types) {
            string typeCode = typeSyntaxWriter.Write(type);
            
            apisSection.Code.AppendLine(typeCode);
        }
    }

    private string GetHeaderCode()
    {
        return $"""
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace {Settings.NamespaceForGeneratedCode};

""";
    }

    private string GetSharedCode()
    {
        return """
internal enum CBool: byte
{
    True = 1,
    False = 0
}

internal enum CStatus: int
{
    Success = 1,
    Failure = -1
}

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
    
    internal static void* AllocateGCHandleAndGetAddress(this object instance)
    {
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

    internal static T? GetInstance<T>(void* handleAddress) where T : class
    {
        GCHandle? handle = GetGCHandle(handleAddress);

        T? instance = handle?.Target as T;

        return instance;
    }
    #endregion Handle Address/GCHandle <-> Object Conversion

    #region Strings
    /// <summary>
    /// This allocates a native char* and copies the contents of the managed string into it.
    /// The allocated native string must be freed when not needed anymore!
    /// </summary>
    internal static byte* CopyToCString(this string? @string)
    {
        if (@string is null) {
            return null;
        }

        byte* cString = (byte*)Marshal.StringToHGlobalAuto(@string);
        
        return cString;
    }

    /// <summary>
    /// This allocates a managed string and copies the contents of the native char* into it.
    /// </summary>
    internal static string? ToDotNetString(byte* cString)
    {
        if (cString is null) {
            return null;
        }
        
        string? @string = Marshal.PtrToStringAuto((nint)cString);

        return @string;
    }
    #endregion Strings

    #region Bools
    internal static CBool ToCBool(this bool @bool)
    {
        if (@bool) {
            return CBool.True;
        } else {
            return CBool.False;
        }
    }

    public static bool ToBool(this CBool cBool)
    {
        return cBool == CBool.True;
    }
    #endregion Bools
}
""";
    }
}