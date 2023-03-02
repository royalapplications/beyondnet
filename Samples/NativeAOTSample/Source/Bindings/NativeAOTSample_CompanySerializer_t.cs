using System.Runtime.InteropServices;
using NativeAOT.Core;

namespace NativeAOTSample;

internal static unsafe class NativeAOTSample_CompanySerializer_t
{
    #region Constants
    private const string NAMESPACE = nameof(NativeAOTSample);
    private const string TYPE_NAME = nameof(CompanySerializer);
    private const string FULL_TYPE_NAME = NAMESPACE + "_" + TYPE_NAME;
    private const string ENTRYPOINT_PREFIX = FULL_TYPE_NAME + "_";
    #endregion Constants

    #region Public API
    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "TypeOf")]
    internal static void* TypeOf()
    {
        return typeof(CompanySerializer).AllocateGCHandleAndGetAddress();
    }
    
    [UnmanagedCallersOnly(EntryPoint=ENTRYPOINT_PREFIX + "Create")]
    internal static void* Create()
    {
        CompanySerializer instance = new();

        void* handleAddress = instance.AllocateGCHandleAndGetAddress();

        return handleAddress;
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "SerializeToJson")]
    internal static byte* SerializeToJson(
        void* handleAddress,
        void* companyHandleAddress
    )
    {
        CompanySerializer? instance = InteropUtils.GetInstance<CompanySerializer>(handleAddress);

        if (instance is null) {
            return null;
        }

        Company? companyInstance = InteropUtils.GetInstance<Company>(companyHandleAddress);

        if (companyInstance is null) {
            return null;
        }

        string jsonString = instance.SerializeToJson(companyInstance);
        byte* jsonStringC = jsonString.CopyToCString();

        return jsonStringC;
    }

    [UnmanagedCallersOnly(EntryPoint = ENTRYPOINT_PREFIX + "DeserializeFromJson")]
    internal static void* DeserializeFromJson(
        void* handleAddress,
        byte* jsonString,
        void** outException
    )
    {
        CompanySerializer? instance = InteropUtils.GetInstance<CompanySerializer>(handleAddress);

        if (instance is null) {
            return null;
        }

        string? jsonStringDn = InteropUtils.ToDotNetString(jsonString);
        
        if (jsonStringDn == null) {
            return null;
        }

        try {
            Company company = instance.DeserializeFromJson(jsonStringDn);

            if (outException != null) {
                *outException = null;
            }
            
            return company.AllocateGCHandleAndGetAddress();
        } catch (Exception ex) {
            if (outException != null) {
                *outException = ex.AllocateGCHandleAndGetAddress();
            }

            return null;
        }
    }
    #endregion Public API
}