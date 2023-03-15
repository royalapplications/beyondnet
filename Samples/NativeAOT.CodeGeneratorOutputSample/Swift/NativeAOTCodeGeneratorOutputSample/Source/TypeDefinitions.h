// Number of generated types: 162
// Number of generated members: 1572

#pragma mark - BEGIN Header
#ifndef TypeDefinitions_h
#define TypeDefinitions_h

#import <stdlib.h>

#pragma mark - END Header
#pragma mark - BEGIN Shared Code
#pragma mark - BEGIN Common Types
typedef const char* CString;

typedef enum __attribute__((enum_extensibility(closed))): uint8_t {
    CBoolYes = 1,
    CBoolNo = 0
} CBool;
#pragma mark - END Common Types

#pragma mark - END Shared Code
#pragma mark - BEGIN Unsupported Types
// Omitted due to settings

#pragma mark - END Unsupported Types
#pragma mark - BEGIN Type Definitions
typedef enum __attribute__((enum_extensibility(closed))): uint32_t {
	NativeAOT_CodeGeneratorInputSample_NiceLevels_NotNice = 0,
	NativeAOT_CodeGeneratorInputSample_NiceLevels_LittleBitNice = 1,
	NativeAOT_CodeGeneratorInputSample_NiceLevels_Nice = 2,
	NativeAOT_CodeGeneratorInputSample_NiceLevels_VeryNice = 3
} NativeAOT_CodeGeneratorInputSample_NiceLevels;


typedef void* System_Enum_t;

typedef void* System_ValueType_t;

typedef void* System_Object_t;

typedef void* System_Type_t;

typedef void* System_Reflection_MemberInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_MemberTypes_Constructor = 1,
	System_Reflection_MemberTypes_Event = 2,
	System_Reflection_MemberTypes_Field = 4,
	System_Reflection_MemberTypes_Method = 8,
	System_Reflection_MemberTypes_Property = 16,
	System_Reflection_MemberTypes_TypeInfo = 32,
	System_Reflection_MemberTypes_Custom = 64,
	System_Reflection_MemberTypes_NestedType = 128,
	System_Reflection_MemberTypes_All = 191
} System_Reflection_MemberTypes;




typedef void* System_String_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_StringComparison_CurrentCulture = 0,
	System_StringComparison_CurrentCultureIgnoreCase = 1,
	System_StringComparison_InvariantCulture = 2,
	System_StringComparison_InvariantCultureIgnoreCase = 3,
	System_StringComparison_Ordinal = 4,
	System_StringComparison_OrdinalIgnoreCase = 5
} System_StringComparison;


typedef void* System_Globalization_CultureInfo_t;

typedef void* System_Void_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_CultureTypes_NeutralCultures = 1,
	System_Globalization_CultureTypes_SpecificCultures = 2,
	System_Globalization_CultureTypes_InstalledWin32Cultures = 4,
	System_Globalization_CultureTypes_AllCultures = 7,
	System_Globalization_CultureTypes_UserCustomCulture = 8,
	System_Globalization_CultureTypes_ReplacementCultures = 16,
	System_Globalization_CultureTypes_WindowsOnlyCultures = 32,
	System_Globalization_CultureTypes_FrameworkCultures = 64
} System_Globalization_CultureTypes;


typedef void* System_Globalization_CompareInfo_t;

typedef void* System_Reflection_Assembly_t;

typedef void* System_Reflection_AssemblyName_t;

typedef void* System_Version_t;


typedef void* System_IFormatProvider_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_NumberStyles_None = 0,
	System_Globalization_NumberStyles_AllowLeadingWhite = 1,
	System_Globalization_NumberStyles_AllowTrailingWhite = 2,
	System_Globalization_NumberStyles_AllowLeadingSign = 4,
	System_Globalization_NumberStyles_Integer = 7,
	System_Globalization_NumberStyles_AllowTrailingSign = 8,
	System_Globalization_NumberStyles_AllowParentheses = 16,
	System_Globalization_NumberStyles_AllowDecimalPoint = 32,
	System_Globalization_NumberStyles_AllowThousands = 64,
	System_Globalization_NumberStyles_Number = 111,
	System_Globalization_NumberStyles_AllowExponent = 128,
	System_Globalization_NumberStyles_Float = 167,
	System_Globalization_NumberStyles_AllowCurrencySymbol = 256,
	System_Globalization_NumberStyles_Currency = 383,
	System_Globalization_NumberStyles_Any = 511,
	System_Globalization_NumberStyles_AllowHexSpecifier = 512,
	System_Globalization_NumberStyles_HexNumber = 515
} System_Globalization_NumberStyles;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_TypeCode_Empty = 0,
	System_TypeCode_Object = 1,
	System_TypeCode_DBNull = 2,
	System_TypeCode_Boolean = 3,
	System_TypeCode_Char = 4,
	System_TypeCode_SByte = 5,
	System_TypeCode_Byte = 6,
	System_TypeCode_Int16 = 7,
	System_TypeCode_UInt16 = 8,
	System_TypeCode_Int32 = 9,
	System_TypeCode_UInt32 = 10,
	System_TypeCode_Int64 = 11,
	System_TypeCode_UInt64 = 12,
	System_TypeCode_Single = 13,
	System_TypeCode_Double = 14,
	System_TypeCode_Decimal = 15,
	System_TypeCode_DateTime = 16,
	System_TypeCode_String = 18
} System_TypeCode;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_ProcessorArchitecture_None = 0,
	System_Reflection_ProcessorArchitecture_MSIL = 1,
	System_Reflection_ProcessorArchitecture_X86 = 2,
	System_Reflection_ProcessorArchitecture_IA64 = 3,
	System_Reflection_ProcessorArchitecture_Amd64 = 4,
	System_Reflection_ProcessorArchitecture_Arm = 5
} System_Reflection_ProcessorArchitecture;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_AssemblyContentType_Default = 0,
	System_Reflection_AssemblyContentType_WindowsRuntime = 1
} System_Reflection_AssemblyContentType;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_AssemblyNameFlags_None = 0,
	System_Reflection_AssemblyNameFlags_PublicKey = 1,
	System_Reflection_AssemblyNameFlags_Retargetable = 256,
	System_Reflection_AssemblyNameFlags_EnableJITcompileOptimizer = 16384,
	System_Reflection_AssemblyNameFlags_EnableJITcompileTracking = 32768
} System_Reflection_AssemblyNameFlags;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Configuration_Assemblies_AssemblyHashAlgorithm_None = 0,
	System_Configuration_Assemblies_AssemblyHashAlgorithm_MD5 = 32771,
	System_Configuration_Assemblies_AssemblyHashAlgorithm_SHA1 = 32772,
	System_Configuration_Assemblies_AssemblyHashAlgorithm_SHA256 = 32780,
	System_Configuration_Assemblies_AssemblyHashAlgorithm_SHA384 = 32781,
	System_Configuration_Assemblies_AssemblyHashAlgorithm_SHA512 = 32782
} System_Configuration_Assemblies_AssemblyHashAlgorithm;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Configuration_Assemblies_AssemblyVersionCompatibility_SameMachine = 1,
	System_Configuration_Assemblies_AssemblyVersionCompatibility_SameProcess = 2,
	System_Configuration_Assemblies_AssemblyVersionCompatibility_SameDomain = 3
} System_Configuration_Assemblies_AssemblyVersionCompatibility;


typedef void* System_Reflection_StrongNameKeyPair_t;

typedef void* System_IO_FileStream_t;

typedef void* System_IO_Stream_t;

typedef void* System_MarshalByRefObject_t;


typedef void* System_Threading_Tasks_Task_t;

typedef void* System_Threading_Tasks_TaskScheduler_t;

typedef void* System_AggregateException_t;

typedef void* System_Exception_t;

typedef void* System_Reflection_MethodBase_t;

typedef void* System_RuntimeMethodHandle_t;

typedef void* System_Runtime_Serialization_SerializationInfo_t;

typedef void* System_Runtime_Serialization_SerializationInfoEnumerator_t;

typedef void* System_Runtime_Serialization_SerializationEntry_t;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_UnicodeCategory_UppercaseLetter = 0,
	System_Globalization_UnicodeCategory_LowercaseLetter = 1,
	System_Globalization_UnicodeCategory_TitlecaseLetter = 2,
	System_Globalization_UnicodeCategory_ModifierLetter = 3,
	System_Globalization_UnicodeCategory_OtherLetter = 4,
	System_Globalization_UnicodeCategory_NonSpacingMark = 5,
	System_Globalization_UnicodeCategory_SpacingCombiningMark = 6,
	System_Globalization_UnicodeCategory_EnclosingMark = 7,
	System_Globalization_UnicodeCategory_DecimalDigitNumber = 8,
	System_Globalization_UnicodeCategory_LetterNumber = 9,
	System_Globalization_UnicodeCategory_OtherNumber = 10,
	System_Globalization_UnicodeCategory_SpaceSeparator = 11,
	System_Globalization_UnicodeCategory_LineSeparator = 12,
	System_Globalization_UnicodeCategory_ParagraphSeparator = 13,
	System_Globalization_UnicodeCategory_Control = 14,
	System_Globalization_UnicodeCategory_Format = 15,
	System_Globalization_UnicodeCategory_Surrogate = 16,
	System_Globalization_UnicodeCategory_PrivateUse = 17,
	System_Globalization_UnicodeCategory_ConnectorPunctuation = 18,
	System_Globalization_UnicodeCategory_DashPunctuation = 19,
	System_Globalization_UnicodeCategory_OpenPunctuation = 20,
	System_Globalization_UnicodeCategory_ClosePunctuation = 21,
	System_Globalization_UnicodeCategory_InitialQuotePunctuation = 22,
	System_Globalization_UnicodeCategory_FinalQuotePunctuation = 23,
	System_Globalization_UnicodeCategory_OtherPunctuation = 24,
	System_Globalization_UnicodeCategory_MathSymbol = 25,
	System_Globalization_UnicodeCategory_CurrencySymbol = 26,
	System_Globalization_UnicodeCategory_ModifierSymbol = 27,
	System_Globalization_UnicodeCategory_OtherSymbol = 28,
	System_Globalization_UnicodeCategory_OtherNotAssigned = 29
} System_Globalization_UnicodeCategory;



typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_MidpointRounding_ToEven = 0,
	System_MidpointRounding_AwayFromZero = 1,
	System_MidpointRounding_ToZero = 2,
	System_MidpointRounding_ToNegativeInfinity = 3,
	System_MidpointRounding_ToPositiveInfinity = 4
} System_MidpointRounding;








typedef void* System_Decimal_t;

typedef void* System_DateTime_t;

typedef void* System_TimeSpan_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_TimeSpanStyles_None = 0,
	System_Globalization_TimeSpanStyles_AssumeNegative = 1
} System_Globalization_TimeSpanStyles;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_DateTimeKind_Unspecified = 0,
	System_DateTimeKind_Utc = 1,
	System_DateTimeKind_Local = 2
} System_DateTimeKind;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_DayOfWeek_Sunday = 0,
	System_DayOfWeek_Monday = 1,
	System_DayOfWeek_Tuesday = 2,
	System_DayOfWeek_Wednesday = 3,
	System_DayOfWeek_Thursday = 4,
	System_DayOfWeek_Friday = 5,
	System_DayOfWeek_Saturday = 6
} System_DayOfWeek;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_DateTimeStyles_None = 0,
	System_Globalization_DateTimeStyles_AllowLeadingWhite = 1,
	System_Globalization_DateTimeStyles_AllowTrailingWhite = 2,
	System_Globalization_DateTimeStyles_AllowInnerWhite = 4,
	System_Globalization_DateTimeStyles_AllowWhiteSpaces = 7,
	System_Globalization_DateTimeStyles_NoCurrentDateDefault = 8,
	System_Globalization_DateTimeStyles_AdjustToUniversal = 16,
	System_Globalization_DateTimeStyles_AssumeLocal = 32,
	System_Globalization_DateTimeStyles_AssumeUniversal = 64,
	System_Globalization_DateTimeStyles_RoundtripKind = 128
} System_Globalization_DateTimeStyles;


typedef void* System_DateOnly_t;

typedef void* System_TimeOnly_t;

typedef void* System_Globalization_Calendar_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_CalendarAlgorithmType_Unknown = 0,
	System_Globalization_CalendarAlgorithmType_SolarCalendar = 1,
	System_Globalization_CalendarAlgorithmType_LunarCalendar = 2,
	System_Globalization_CalendarAlgorithmType_LunisolarCalendar = 3
} System_Globalization_CalendarAlgorithmType;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_CalendarWeekRule_FirstDay = 0,
	System_Globalization_CalendarWeekRule_FirstFullWeek = 1,
	System_Globalization_CalendarWeekRule_FirstFourDayWeek = 2
} System_Globalization_CalendarWeekRule;


typedef void* System_Runtime_Serialization_IFormatterConverter_t;

typedef void* System_Runtime_Serialization_StreamingContext_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Runtime_Serialization_StreamingContextStates_CrossProcess = 1,
	System_Runtime_Serialization_StreamingContextStates_CrossMachine = 2,
	System_Runtime_Serialization_StreamingContextStates_File = 4,
	System_Runtime_Serialization_StreamingContextStates_Persistence = 8,
	System_Runtime_Serialization_StreamingContextStates_Remoting = 16,
	System_Runtime_Serialization_StreamingContextStates_Other = 32,
	System_Runtime_Serialization_StreamingContextStates_Clone = 64,
	System_Runtime_Serialization_StreamingContextStates_CrossAppDomain = 128,
	System_Runtime_Serialization_StreamingContextStates_All = 255
} System_Runtime_Serialization_StreamingContextStates;



typedef void* System_RuntimeTypeHandle_t;

typedef void* System_ModuleHandle_t;

typedef void* System_RuntimeFieldHandle_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_MethodAttributes_PrivateScope = 0,
	System_Reflection_MethodAttributes_ReuseSlot = 0,
	System_Reflection_MethodAttributes_Private = 1,
	System_Reflection_MethodAttributes_FamANDAssem = 2,
	System_Reflection_MethodAttributes_Assembly = 3,
	System_Reflection_MethodAttributes_Family = 4,
	System_Reflection_MethodAttributes_FamORAssem = 5,
	System_Reflection_MethodAttributes_Public = 6,
	System_Reflection_MethodAttributes_MemberAccessMask = 7,
	System_Reflection_MethodAttributes_UnmanagedExport = 8,
	System_Reflection_MethodAttributes_Static = 16,
	System_Reflection_MethodAttributes_Final = 32,
	System_Reflection_MethodAttributes_Virtual = 64,
	System_Reflection_MethodAttributes_HideBySig = 128,
	System_Reflection_MethodAttributes_NewSlot = 256,
	System_Reflection_MethodAttributes_VtableLayoutMask = 256,
	System_Reflection_MethodAttributes_CheckAccessOnOverride = 512,
	System_Reflection_MethodAttributes_Abstract = 1024,
	System_Reflection_MethodAttributes_SpecialName = 2048,
	System_Reflection_MethodAttributes_RTSpecialName = 4096,
	System_Reflection_MethodAttributes_PinvokeImpl = 8192,
	System_Reflection_MethodAttributes_HasSecurity = 16384,
	System_Reflection_MethodAttributes_RequireSecObject = 32768,
	System_Reflection_MethodAttributes_ReservedMask = 53248
} System_Reflection_MethodAttributes;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_MethodImplAttributes_IL = 0,
	System_Reflection_MethodImplAttributes_Managed = 0,
	System_Reflection_MethodImplAttributes_Native = 1,
	System_Reflection_MethodImplAttributes_OPTIL = 2,
	System_Reflection_MethodImplAttributes_CodeTypeMask = 3,
	System_Reflection_MethodImplAttributes_Runtime = 3,
	System_Reflection_MethodImplAttributes_ManagedMask = 4,
	System_Reflection_MethodImplAttributes_Unmanaged = 4,
	System_Reflection_MethodImplAttributes_NoInlining = 8,
	System_Reflection_MethodImplAttributes_ForwardRef = 16,
	System_Reflection_MethodImplAttributes_Synchronized = 32,
	System_Reflection_MethodImplAttributes_NoOptimization = 64,
	System_Reflection_MethodImplAttributes_PreserveSig = 128,
	System_Reflection_MethodImplAttributes_AggressiveInlining = 256,
	System_Reflection_MethodImplAttributes_AggressiveOptimization = 512,
	System_Reflection_MethodImplAttributes_InternalCall = 4096,
	System_Reflection_MethodImplAttributes_MaxMethodImplVal = 65535
} System_Reflection_MethodImplAttributes;


typedef void* System_Reflection_MethodBody_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_CallingConventions_Standard = 1,
	System_Reflection_CallingConventions_VarArgs = 2,
	System_Reflection_CallingConventions_Any = 3,
	System_Reflection_CallingConventions_HasThis = 32,
	System_Reflection_CallingConventions_ExplicitThis = 64
} System_Reflection_CallingConventions;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_BindingFlags_Default = 0,
	System_Reflection_BindingFlags_IgnoreCase = 1,
	System_Reflection_BindingFlags_DeclaredOnly = 2,
	System_Reflection_BindingFlags_Instance = 4,
	System_Reflection_BindingFlags_Static = 8,
	System_Reflection_BindingFlags_Public = 16,
	System_Reflection_BindingFlags_NonPublic = 32,
	System_Reflection_BindingFlags_FlattenHierarchy = 64,
	System_Reflection_BindingFlags_InvokeMethod = 256,
	System_Reflection_BindingFlags_CreateInstance = 512,
	System_Reflection_BindingFlags_GetField = 1024,
	System_Reflection_BindingFlags_SetField = 2048,
	System_Reflection_BindingFlags_GetProperty = 4096,
	System_Reflection_BindingFlags_SetProperty = 8192,
	System_Reflection_BindingFlags_PutDispProperty = 16384,
	System_Reflection_BindingFlags_PutRefDispProperty = 32768,
	System_Reflection_BindingFlags_ExactBinding = 65536,
	System_Reflection_BindingFlags_SuppressChangeType = 131072,
	System_Reflection_BindingFlags_OptionalParamBinding = 262144,
	System_Reflection_BindingFlags_IgnoreReturn = 16777216,
	System_Reflection_BindingFlags_DoNotWrapExceptions = 33554432
} System_Reflection_BindingFlags;


typedef void* System_Reflection_Binder_t;

typedef void* System_Reflection_FieldInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_FieldAttributes_PrivateScope = 0,
	System_Reflection_FieldAttributes_Private = 1,
	System_Reflection_FieldAttributes_FamANDAssem = 2,
	System_Reflection_FieldAttributes_Assembly = 3,
	System_Reflection_FieldAttributes_Family = 4,
	System_Reflection_FieldAttributes_FamORAssem = 5,
	System_Reflection_FieldAttributes_Public = 6,
	System_Reflection_FieldAttributes_FieldAccessMask = 7,
	System_Reflection_FieldAttributes_Static = 16,
	System_Reflection_FieldAttributes_InitOnly = 32,
	System_Reflection_FieldAttributes_Literal = 64,
	System_Reflection_FieldAttributes_NotSerialized = 128,
	System_Reflection_FieldAttributes_HasFieldRVA = 256,
	System_Reflection_FieldAttributes_SpecialName = 512,
	System_Reflection_FieldAttributes_RTSpecialName = 1024,
	System_Reflection_FieldAttributes_HasFieldMarshal = 4096,
	System_Reflection_FieldAttributes_PinvokeImpl = 8192,
	System_Reflection_FieldAttributes_HasDefault = 32768,
	System_Reflection_FieldAttributes_ReservedMask = 38144
} System_Reflection_FieldAttributes;


typedef void* System_Reflection_PropertyInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_PropertyAttributes_None = 0,
	System_Reflection_PropertyAttributes_SpecialName = 512,
	System_Reflection_PropertyAttributes_RTSpecialName = 1024,
	System_Reflection_PropertyAttributes_HasDefault = 4096,
	System_Reflection_PropertyAttributes_Reserved2 = 8192,
	System_Reflection_PropertyAttributes_Reserved3 = 16384,
	System_Reflection_PropertyAttributes_Reserved4 = 32768,
	System_Reflection_PropertyAttributes_ReservedMask = 62464
} System_Reflection_PropertyAttributes;


typedef void* System_Reflection_MethodInfo_t;

typedef void* System_Reflection_ParameterInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_ParameterAttributes_None = 0,
	System_Reflection_ParameterAttributes_In = 1,
	System_Reflection_ParameterAttributes_Out = 2,
	System_Reflection_ParameterAttributes_Lcid = 4,
	System_Reflection_ParameterAttributes_Retval = 8,
	System_Reflection_ParameterAttributes_Optional = 16,
	System_Reflection_ParameterAttributes_HasDefault = 4096,
	System_Reflection_ParameterAttributes_HasFieldMarshal = 8192,
	System_Reflection_ParameterAttributes_Reserved3 = 16384,
	System_Reflection_ParameterAttributes_Reserved4 = 32768,
	System_Reflection_ParameterAttributes_ReservedMask = 61440
} System_Reflection_ParameterAttributes;


typedef void* System_Reflection_ICustomAttributeProvider_t;

typedef void* System_Collections_IDictionary_t;

typedef void* System_Collections_ICollection_t;

typedef void* System_Array_t;

typedef void* System_Collections_IComparer_t;

typedef void* System_Collections_IEnumerator_t;

typedef void* System_Collections_IDictionaryEnumerator_t;

typedef void* System_Collections_DictionaryEntry_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Threading_Tasks_TaskStatus_Created = 0,
	System_Threading_Tasks_TaskStatus_WaitingForActivation = 1,
	System_Threading_Tasks_TaskStatus_WaitingToRun = 2,
	System_Threading_Tasks_TaskStatus_Running = 3,
	System_Threading_Tasks_TaskStatus_WaitingForChildrenToComplete = 4,
	System_Threading_Tasks_TaskStatus_RanToCompletion = 5,
	System_Threading_Tasks_TaskStatus_Canceled = 6,
	System_Threading_Tasks_TaskStatus_Faulted = 7
} System_Threading_Tasks_TaskStatus;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Threading_Tasks_TaskCreationOptions_None = 0,
	System_Threading_Tasks_TaskCreationOptions_PreferFairness = 1,
	System_Threading_Tasks_TaskCreationOptions_LongRunning = 2,
	System_Threading_Tasks_TaskCreationOptions_AttachedToParent = 4,
	System_Threading_Tasks_TaskCreationOptions_DenyChildAttach = 8,
	System_Threading_Tasks_TaskCreationOptions_HideScheduler = 16,
	System_Threading_Tasks_TaskCreationOptions_RunContinuationsAsynchronously = 64
} System_Threading_Tasks_TaskCreationOptions;


typedef void* System_Threading_Tasks_TaskFactory_t;

typedef void* System_Threading_CancellationToken_t;

typedef void* System_Threading_WaitHandle_t;

typedef void* Microsoft_Win32_SafeHandles_SafeWaitHandle_t;

typedef void* Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_t;

typedef void* System_Runtime_InteropServices_SafeHandle_t;

typedef void* System_Runtime_ConstrainedExecution_CriticalFinalizerObject_t;

typedef void* System_Threading_CancellationTokenRegistration_t;

typedef void* System_Threading_Tasks_ValueTask_t;

typedef void* System_Runtime_CompilerServices_ValueTaskAwaiter_t;

typedef void* System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t;

typedef void* System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t;

typedef void* System_Threading_Tasks_Sources_IValueTaskSource_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Threading_Tasks_Sources_ValueTaskSourceStatus_Pending = 0,
	System_Threading_Tasks_Sources_ValueTaskSourceStatus_Succeeded = 1,
	System_Threading_Tasks_Sources_ValueTaskSourceStatus_Faulted = 2,
	System_Threading_Tasks_Sources_ValueTaskSourceStatus_Canceled = 3
} System_Threading_Tasks_Sources_ValueTaskSourceStatus;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_None = 0,
	System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_UseSchedulingContext = 1,
	System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags_FlowExecutionContext = 2
} System_Threading_Tasks_Sources_ValueTaskSourceOnCompletedFlags;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Threading_Tasks_TaskContinuationOptions_None = 0,
	System_Threading_Tasks_TaskContinuationOptions_PreferFairness = 1,
	System_Threading_Tasks_TaskContinuationOptions_LongRunning = 2,
	System_Threading_Tasks_TaskContinuationOptions_AttachedToParent = 4,
	System_Threading_Tasks_TaskContinuationOptions_DenyChildAttach = 8,
	System_Threading_Tasks_TaskContinuationOptions_HideScheduler = 16,
	System_Threading_Tasks_TaskContinuationOptions_LazyCancellation = 32,
	System_Threading_Tasks_TaskContinuationOptions_RunContinuationsAsynchronously = 64,
	System_Threading_Tasks_TaskContinuationOptions_NotOnRanToCompletion = 65536,
	System_Threading_Tasks_TaskContinuationOptions_NotOnFaulted = 131072,
	System_Threading_Tasks_TaskContinuationOptions_OnlyOnCanceled = 196608,
	System_Threading_Tasks_TaskContinuationOptions_NotOnCanceled = 262144,
	System_Threading_Tasks_TaskContinuationOptions_OnlyOnFaulted = 327680,
	System_Threading_Tasks_TaskContinuationOptions_OnlyOnRanToCompletion = 393216,
	System_Threading_Tasks_TaskContinuationOptions_ExecuteSynchronously = 524288
} System_Threading_Tasks_TaskContinuationOptions;


typedef void* System_IAsyncResult_t;

typedef void* System_Runtime_CompilerServices_TaskAwaiter_t;

typedef void* System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t;

typedef void* System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t;

typedef void* System_Runtime_CompilerServices_YieldAwaitable_t;

typedef void* System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_IO_SeekOrigin_Begin = 0,
	System_IO_SeekOrigin_Current = 1,
	System_IO_SeekOrigin_End = 2
} System_IO_SeekOrigin;


typedef void* Microsoft_Win32_SafeHandles_SafeFileHandle_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_IO_FileAccess_Read = 1,
	System_IO_FileAccess_Write = 2,
	System_IO_FileAccess_ReadWrite = 3
} System_IO_FileAccess;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_IO_FileMode_CreateNew = 1,
	System_IO_FileMode_Create = 2,
	System_IO_FileMode_Open = 3,
	System_IO_FileMode_OpenOrCreate = 4,
	System_IO_FileMode_Truncate = 5,
	System_IO_FileMode_Append = 6
} System_IO_FileMode;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_IO_FileShare_None = 0,
	System_IO_FileShare_Read = 1,
	System_IO_FileShare_Write = 2,
	System_IO_FileShare_ReadWrite = 3,
	System_IO_FileShare_Delete = 4,
	System_IO_FileShare_Inheritable = 16
} System_IO_FileShare;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_IO_FileOptions_WriteThrough = -2147483648,
	System_IO_FileOptions_None = 0,
	System_IO_FileOptions_Encrypted = 16384,
	System_IO_FileOptions_DeleteOnClose = 67108864,
	System_IO_FileOptions_SequentialScan = 134217728,
	System_IO_FileOptions_RandomAccess = 268435456,
	System_IO_FileOptions_Asynchronous = 1073741824
} System_IO_FileOptions;


typedef void* System_IO_FileStreamOptions_t;

typedef void* System_Reflection_ManifestResourceInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_ResourceLocation_Embedded = 1,
	System_Reflection_ResourceLocation_ContainedInAnotherAssembly = 2,
	System_Reflection_ResourceLocation_ContainedInManifestFile = 4
} System_Reflection_ResourceLocation;


typedef void* System_Reflection_Module_t;

typedef void* System_Guid_t;

typedef enum __attribute__((enum_extensibility(closed))): uint8_t {
	System_Security_SecurityRuleSet_None = 0,
	System_Security_SecurityRuleSet_Level1 = 1,
	System_Security_SecurityRuleSet_Level2 = 2
} System_Security_SecurityRuleSet;


typedef void* System_Text_Rune_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Buffers_OperationStatus_Done = 0,
	System_Buffers_OperationStatus_DestinationTooSmall = 1,
	System_Buffers_OperationStatus_NeedMoreData = 2,
	System_Buffers_OperationStatus_InvalidData = 3
} System_Buffers_OperationStatus;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_CompareOptions_None = 0,
	System_Globalization_CompareOptions_IgnoreCase = 1,
	System_Globalization_CompareOptions_IgnoreNonSpace = 2,
	System_Globalization_CompareOptions_IgnoreSymbols = 4,
	System_Globalization_CompareOptions_IgnoreKanaType = 8,
	System_Globalization_CompareOptions_IgnoreWidth = 16,
	System_Globalization_CompareOptions_OrdinalIgnoreCase = 268435456,
	System_Globalization_CompareOptions_StringSort = 536870912,
	System_Globalization_CompareOptions_Ordinal = 1073741824
} System_Globalization_CompareOptions;


typedef void* System_Globalization_SortKey_t;

typedef void* System_Globalization_SortVersion_t;

typedef void* System_Globalization_TextInfo_t;

typedef void* System_Globalization_NumberFormatInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Globalization_DigitShapes_Context = 0,
	System_Globalization_DigitShapes_None = 1,
	System_Globalization_DigitShapes_NativeNational = 2
} System_Globalization_DigitShapes;


typedef void* System_Globalization_DateTimeFormatInfo_t;

typedef void* System_CharEnumerator_t;

typedef void* System_Text_StringRuneEnumerator_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Text_NormalizationForm_FormC = 1,
	System_Text_NormalizationForm_FormD = 2,
	System_Text_NormalizationForm_FormKC = 5,
	System_Text_NormalizationForm_FormKD = 6
} System_Text_NormalizationForm;


typedef void* System_Text_CompositeFormat_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_StringSplitOptions_None = 0,
	System_StringSplitOptions_RemoveEmptyEntries = 1,
	System_StringSplitOptions_TrimEntries = 2
} System_StringSplitOptions;


typedef void* System_Text_Encoding_t;

typedef void* System_Text_EncodingProvider_t;

typedef void* System_Text_EncoderFallback_t;

typedef void* System_Text_EncoderFallbackBuffer_t;

typedef void* System_Text_DecoderFallback_t;

typedef void* System_Text_DecoderFallbackBuffer_t;

typedef void* System_Text_Decoder_t;

typedef void* System_Text_Encoder_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_GenericParameterAttributes_None = 0,
	System_Reflection_GenericParameterAttributes_Covariant = 1,
	System_Reflection_GenericParameterAttributes_Contravariant = 2,
	System_Reflection_GenericParameterAttributes_VarianceMask = 3,
	System_Reflection_GenericParameterAttributes_ReferenceTypeConstraint = 4,
	System_Reflection_GenericParameterAttributes_NotNullableValueTypeConstraint = 8,
	System_Reflection_GenericParameterAttributes_DefaultConstructorConstraint = 16,
	System_Reflection_GenericParameterAttributes_SpecialConstraintMask = 28
} System_Reflection_GenericParameterAttributes;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_TypeAttributes_NotPublic = 0,
	System_Reflection_TypeAttributes_AutoLayout = 0,
	System_Reflection_TypeAttributes_AnsiClass = 0,
	System_Reflection_TypeAttributes_Class = 0,
	System_Reflection_TypeAttributes_Public = 1,
	System_Reflection_TypeAttributes_NestedPublic = 2,
	System_Reflection_TypeAttributes_NestedPrivate = 3,
	System_Reflection_TypeAttributes_NestedFamily = 4,
	System_Reflection_TypeAttributes_NestedAssembly = 5,
	System_Reflection_TypeAttributes_NestedFamANDAssem = 6,
	System_Reflection_TypeAttributes_VisibilityMask = 7,
	System_Reflection_TypeAttributes_NestedFamORAssem = 7,
	System_Reflection_TypeAttributes_SequentialLayout = 8,
	System_Reflection_TypeAttributes_ExplicitLayout = 16,
	System_Reflection_TypeAttributes_LayoutMask = 24,
	System_Reflection_TypeAttributes_Interface = 32,
	System_Reflection_TypeAttributes_ClassSemanticsMask = 32,
	System_Reflection_TypeAttributes_Abstract = 128,
	System_Reflection_TypeAttributes_Sealed = 256,
	System_Reflection_TypeAttributes_SpecialName = 1024,
	System_Reflection_TypeAttributes_RTSpecialName = 2048,
	System_Reflection_TypeAttributes_Import = 4096,
	System_Reflection_TypeAttributes_Serializable = 8192,
	System_Reflection_TypeAttributes_WindowsRuntime = 16384,
	System_Reflection_TypeAttributes_UnicodeClass = 65536,
	System_Reflection_TypeAttributes_AutoClass = 131072,
	System_Reflection_TypeAttributes_StringFormatMask = 196608,
	System_Reflection_TypeAttributes_CustomFormatClass = 196608,
	System_Reflection_TypeAttributes_HasSecurity = 262144,
	System_Reflection_TypeAttributes_ReservedMask = 264192,
	System_Reflection_TypeAttributes_BeforeFieldInit = 1048576,
	System_Reflection_TypeAttributes_CustomFormatMask = 12582912
} System_Reflection_TypeAttributes;


typedef void* System_Runtime_InteropServices_StructLayoutAttribute_t;

typedef void* System_Attribute_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Runtime_InteropServices_LayoutKind_Sequential = 0,
	System_Runtime_InteropServices_LayoutKind_Explicit = 2,
	System_Runtime_InteropServices_LayoutKind_Auto = 3
} System_Runtime_InteropServices_LayoutKind;


typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Runtime_InteropServices_CharSet_None = 1,
	System_Runtime_InteropServices_CharSet_Ansi = 2,
	System_Runtime_InteropServices_CharSet_Unicode = 3,
	System_Runtime_InteropServices_CharSet_Auto = 4
} System_Runtime_InteropServices_CharSet;


typedef void* System_Reflection_ConstructorInfo_t;

typedef void* System_Reflection_EventInfo_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	System_Reflection_EventAttributes_None = 0,
	System_Reflection_EventAttributes_SpecialName = 512,
	System_Reflection_EventAttributes_RTSpecialName = 1024,
	System_Reflection_EventAttributes_ReservedMask = 1024
} System_Reflection_EventAttributes;


typedef void* System_Reflection_InterfaceMapping_t;

typedef void* NativeAOT_CodeGeneratorInputSample_Person_t;

typedef enum __attribute__((enum_extensibility(closed))): int32_t {
	NativeAOT_CodeGeneratorInputSample_TestEnum_FirstCase = 0,
	NativeAOT_CodeGeneratorInputSample_TestEnum_SecondCase = 1
} NativeAOT_CodeGeneratorInputSample_TestEnum;


typedef void* NativeAOT_CodeGeneratorInputSample_TestClass_t;


#pragma mark - END Type Definitions
#pragma mark - BEGIN APIs
#pragma mark - BEGIN APIs of NativeAOT.CodeGeneratorInputSample.NiceLevels
#pragma mark - END APIs of NativeAOT.CodeGeneratorInputSample.NiceLevels

#pragma mark - BEGIN APIs of System.Enum
CString /* System.String */
System_Enum_GetName(
	System_Type_t /* System.Type */ enumType,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Enum_GetUnderlyingType(
	System_Type_t /* System.Type */ enumType,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Enum_GetValues(
	System_Type_t /* System.Type */ enumType,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Enum_GetValuesAsUnderlyingType(
	System_Type_t /* System.Type */ enumType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Enum_HasFlag(
	System_Enum_t /* System.Enum */ self,
	System_Enum_t /* System.Enum */ flag,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Enum_IsDefined(
	System_Type_t /* System.Type */ enumType,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_Parse(
	System_Type_t /* System.Type */ enumType,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_Parse1(
	System_Type_t /* System.Type */ enumType,
	CString /* System.String */ value,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Enum_Equals(
	System_Enum_t /* System.Enum */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Enum_GetHashCode(
	System_Enum_t /* System.Enum */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Enum_CompareTo(
	System_Enum_t /* System.Enum */ self,
	System_Object_t /* System.Object */ target,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Enum_ToString(
	System_Enum_t /* System.Enum */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Enum_ToString1(
	System_Enum_t /* System.Enum */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Enum_ToString2(
	System_Enum_t /* System.Enum */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Enum_ToString3(
	System_Enum_t /* System.Enum */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Enum_Format(
	System_Type_t /* System.Type */ enumType,
	System_Object_t /* System.Object */ value,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

System_TypeCode /* System.TypeCode */
System_Enum_GetTypeCode(
	System_Enum_t /* System.Enum */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject(
	System_Type_t /* System.Type */ enumType,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject1(
	System_Type_t /* System.Type */ enumType,
	int8_t /* System.SByte */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject2(
	System_Type_t /* System.Type */ enumType,
	int16_t /* System.Int16 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject3(
	System_Type_t /* System.Type */ enumType,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject4(
	System_Type_t /* System.Type */ enumType,
	uint8_t /* System.Byte */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject5(
	System_Type_t /* System.Type */ enumType,
	uint16_t /* System.UInt16 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject6(
	System_Type_t /* System.Type */ enumType,
	uint32_t /* System.UInt32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject7(
	System_Type_t /* System.Type */ enumType,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Enum_ToObject8(
	System_Type_t /* System.Type */ enumType,
	uint64_t /* System.UInt64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Enum_Destroy(
	System_Enum_t /* System.Enum */ self
);

#pragma mark - END APIs of System.Enum

#pragma mark - BEGIN APIs of System.ValueType
CBool /* System.Boolean */
System_ValueType_Equals(
	System_ValueType_t /* System.ValueType */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_ValueType_GetHashCode(
	System_ValueType_t /* System.ValueType */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_ValueType_ToString(
	System_ValueType_t /* System.ValueType */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_ValueType_Destroy(
	System_ValueType_t /* System.ValueType */ self
);

#pragma mark - END APIs of System.ValueType

#pragma mark - BEGIN APIs of System.Object
System_Type_t /* System.Type */
System_Object_GetType(
	System_Object_t /* System.Object */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Object_ToString(
	System_Object_t /* System.Object */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Object_Equals(
	System_Object_t /* System.Object */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Object_Equals1(
	System_Object_t /* System.Object */ objA,
	System_Object_t /* System.Object */ objB,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Object_ReferenceEquals(
	System_Object_t /* System.Object */ objA,
	System_Object_t /* System.Object */ objB,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Object_GetHashCode(
	System_Object_t /* System.Object */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Object_Create(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Object_Destroy(
	System_Object_t /* System.Object */ self
);

#pragma mark - END APIs of System.Object

#pragma mark - BEGIN APIs of System.Type
System_Type_t /* System.Type */
System_Type_GetType(
	CString /* System.String */ typeName,
	CBool /* System.Boolean */ throwOnError,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetType1(
	CString /* System.String */ typeName,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetType2(
	CString /* System.String */ typeName,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromHandle(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetType3(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetElementType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Type_GetArrayRank(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetGenericTypeDefinition(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsAssignableTo(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ targetType,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_EventInfo_t /* System.Reflection.EventInfo */
System_Type_GetEvent(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_EventInfo_t /* System.Reflection.EventInfo */
System_Type_GetEvent1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Type_GetField(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Type_GetField1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */
System_Type_GetMemberWithSameMetadataDefinitionAs(
	System_Type_t /* System.Type */ self,
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ member,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Type_GetMethod(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Type_GetMethod1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetNestedType(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetNestedType1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */
System_Type_GetProperty(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */
System_Type_GetProperty1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */
System_Type_GetProperty2(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Type_t /* System.Type */ returnType,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */
System_Type_GetTypeHandle(
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

System_TypeCode /* System.TypeCode */
System_Type_GetTypeCode(
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromCLSID(
	System_Guid_t /* System.Guid */ clsid,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromCLSID1(
	System_Guid_t /* System.Guid */ clsid,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromCLSID2(
	System_Guid_t /* System.Guid */ clsid,
	CString /* System.String */ server,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromCLSID3(
	System_Guid_t /* System.Guid */ clsid,
	CString /* System.String */ server,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromProgID(
	CString /* System.String */ progID,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromProgID1(
	CString /* System.String */ progID,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromProgID2(
	CString /* System.String */ progID,
	CString /* System.String */ server,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetTypeFromProgID3(
	CString /* System.String */ progID,
	CString /* System.String */ server,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetInterface(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetInterface1(
	System_Type_t /* System.Type */ self,
	CString /* System.String */ name,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */
System_Type_GetInterfaceMap(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ interfaceType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsInstanceOfType(
	System_Type_t /* System.Type */ self,
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsEquivalentTo(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ other,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_GetEnumUnderlyingType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Type_GetEnumValues(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Type_GetEnumValuesAsUnderlyingType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_MakeArrayType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_MakeArrayType1(
	System_Type_t /* System.Type */ self,
	int32_t /* System.Int32 */ rank,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_MakeByRefType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_MakePointerType(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_MakeGenericMethodParameter(
	int32_t /* System.Int32 */ position,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Type_ToString(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_Equals(
	System_Type_t /* System.Type */ self,
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Type_GetHashCode(
	System_Type_t /* System.Type */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_Equals1(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ o,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Type_ReflectionOnlyGetType(
	CString /* System.String */ typeName,
	CBool /* System.Boolean */ throwIfNotFound,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsEnumDefined(
	System_Type_t /* System.Type */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Type_GetEnumName(
	System_Type_t /* System.Type */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsSubclassOf(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ c,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Type_IsAssignableFrom(
	System_Type_t /* System.Type */ self,
	System_Type_t /* System.Type */ c,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Type_Delimiter_Get(
	
);

System_Object_t /* System.Object */
System_Type_Missing_Get(
	
);

void /* System.Void */
System_Type_Destroy(
	System_Type_t /* System.Type */ self
);

#pragma mark - END APIs of System.Type

#pragma mark - BEGIN APIs of System.Reflection.MemberInfo
CBool /* System.Boolean */
System_Reflection_MemberInfo_HasSameMetadataDefinitionAs(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ self,
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ other,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_MemberInfo_IsDefined(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ self,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_MemberInfo_Equals(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_MemberInfo_GetHashCode(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_MemberInfo_Destroy(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ self
);

#pragma mark - END APIs of System.Reflection.MemberInfo

#pragma mark - BEGIN APIs of System.Reflection.MemberTypes
#pragma mark - END APIs of System.Reflection.MemberTypes



#pragma mark - BEGIN APIs of System.String
CString /* System.String */
System_String_Intern(
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_IsInterned(
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare1(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare2(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare3(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare4(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	CBool /* System.Boolean */ ignoreCase,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare5(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare6(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare7(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	CBool /* System.Boolean */ ignoreCase,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare8(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Compare9(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_CompareOrdinal(
	CString /* System.String */ strA,
	CString /* System.String */ strB,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_CompareOrdinal1(
	CString /* System.String */ strA,
	int32_t /* System.Int32 */ indexA,
	CString /* System.String */ strB,
	int32_t /* System.Int32 */ indexB,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_CompareTo(
	CString /* System.String */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_CompareTo1(
	CString /* System.String */ self,
	CString /* System.String */ strB,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_EndsWith(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_EndsWith1(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_EndsWith2(
	CString /* System.String */ self,
	CString /* System.String */ value,
	CBool /* System.Boolean */ ignoreCase,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_EndsWith3(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Equals(
	CString /* System.String */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Equals1(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Equals2(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Equals3(
	CString /* System.String */ a,
	CString /* System.String */ b,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Equals4(
	CString /* System.String */ a,
	CString /* System.String */ b,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_GetHashCode(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_GetHashCode1(
	CString /* System.String */ self,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_StartsWith(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_StartsWith1(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_StartsWith2(
	CString /* System.String */ self,
	CString /* System.String */ value,
	CBool /* System.Boolean */ ignoreCase,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_StartsWith3(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_String_Clone(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Copy(
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_IsNullOrEmpty(
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_IsNullOrWhiteSpace(
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToString(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToString1(
	CString /* System.String */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_CharEnumerator_t /* System.CharEnumerator */
System_String_GetEnumerator(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */
System_String_EnumerateRunes(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TypeCode /* System.TypeCode */
System_String_GetTypeCode(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_IsNormalized(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_IsNormalized1(
	CString /* System.String */ self,
	System_Text_NormalizationForm /* System.Text.NormalizationForm */ normalizationForm,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Normalize(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Normalize1(
	CString /* System.String */ self,
	System_Text_NormalizationForm /* System.Text.NormalizationForm */ normalizationForm,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat(
	System_Object_t /* System.Object */ arg0,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat1(
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat2(
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Object_t /* System.Object */ arg2,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat3(
	CString /* System.String */ str0,
	CString /* System.String */ str1,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat4(
	CString /* System.String */ str0,
	CString /* System.String */ str1,
	CString /* System.String */ str2,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Concat5(
	CString /* System.String */ str0,
	CString /* System.String */ str1,
	CString /* System.String */ str2,
	CString /* System.String */ str3,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format(
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format1(
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format2(
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Object_t /* System.Object */ arg2,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format3(
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format4(
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Format5(
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	CString /* System.String */ format,
	System_Object_t /* System.Object */ arg0,
	System_Object_t /* System.Object */ arg1,
	System_Object_t /* System.Object */ arg2,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Insert(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ startIndex,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_PadLeft(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ totalWidth,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_PadLeft1(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ totalWidth,
	uint8_t /* System.Char */ paddingChar,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_PadRight(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ totalWidth,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_PadRight1(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ totalWidth,
	uint8_t /* System.Char */ paddingChar,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Remove(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Remove1(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Replace(
	CString /* System.String */ self,
	CString /* System.String */ oldValue,
	CString /* System.String */ newValue,
	CBool /* System.Boolean */ ignoreCase,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Replace1(
	CString /* System.String */ self,
	CString /* System.String */ oldValue,
	CString /* System.String */ newValue,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Replace2(
	CString /* System.String */ self,
	uint8_t /* System.Char */ oldChar,
	uint8_t /* System.Char */ newChar,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Replace3(
	CString /* System.String */ self,
	CString /* System.String */ oldValue,
	CString /* System.String */ newValue,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ReplaceLineEndings(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ReplaceLineEndings1(
	CString /* System.String */ self,
	CString /* System.String */ replacementText,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Substring(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Substring1(
	CString /* System.String */ self,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToLower(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToLower1(
	CString /* System.String */ self,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToLowerInvariant(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToUpper(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToUpper1(
	CString /* System.String */ self,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_ToUpperInvariant(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Trim(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Trim1(
	CString /* System.String */ self,
	uint8_t /* System.Char */ trimChar,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_TrimStart(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_TrimStart1(
	CString /* System.String */ self,
	uint8_t /* System.Char */ trimChar,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_TrimEnd(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_TrimEnd1(
	CString /* System.String */ self,
	uint8_t /* System.Char */ trimChar,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Contains(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Contains1(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Contains2(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_String_Contains3(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf1(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf2(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf3(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf4(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf5(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf6(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf7(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf8(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_IndexOf9(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf1(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf2(
	CString /* System.String */ self,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf3(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf4(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf5(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf6(
	CString /* System.String */ self,
	CString /* System.String */ value,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf7(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_LastIndexOf8(
	CString /* System.String */ self,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_StringComparison /* System.StringComparison */ comparisonType,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Create(
	uint8_t /* System.Char */ c,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_String_Length_Get(
	CString /* System.String */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_String_Empty_Get(
	
);

void /* System.Void */
System_String_Destroy(
	CString /* System.String */ self
);

#pragma mark - END APIs of System.String

#pragma mark - BEGIN APIs of System.StringComparison
#pragma mark - END APIs of System.StringComparison

#pragma mark - BEGIN APIs of System.Globalization.CultureInfo
System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_CreateSpecificCulture(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CultureInfo_Equals(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CultureInfo_GetHashCode(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_ToString(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_CultureInfo_GetFormat(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Type_t /* System.Type */ formatType,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_ClearCachedData(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetConsoleFallbackUICulture(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_CultureInfo_Clone(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_ReadOnly(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ ci,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetCultureInfo(
	int32_t /* System.Int32 */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetCultureInfo1(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetCultureInfo2(
	CString /* System.String */ name,
	CString /* System.String */ altName,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetCultureInfo3(
	CString /* System.String */ name,
	CBool /* System.Boolean */ predefinedOnly,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_GetCultureInfoByIetfLanguageTag(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_Create1(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_Create2(
	CString /* System.String */ name,
	CBool /* System.Boolean */ useUserOverride,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_Create3(
	int32_t /* System.Int32 */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_Create4(
	int32_t /* System.Int32 */ culture,
	CBool /* System.Boolean */ useUserOverride,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_CurrentCulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_CurrentCulture_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_CurrentUICulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_CurrentUICulture_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_InstalledUICulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_DefaultThreadCurrentCulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_DefaultThreadCurrentCulture_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_DefaultThreadCurrentUICulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_DefaultThreadCurrentUICulture_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_InvariantCulture_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Globalization_CultureInfo_Parent_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CultureInfo_LCID_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CultureInfo_KeyboardLayoutId_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_Name_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_IetfLanguageTag_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_DisplayName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_NativeName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_EnglishName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_TwoLetterISOLanguageName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_ThreeLetterISOLanguageName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CultureInfo_ThreeLetterWindowsLanguageName_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */
System_Globalization_CultureInfo_CompareInfo_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_TextInfo_t /* System.Globalization.TextInfo */
System_Globalization_CultureInfo_TextInfo_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CultureInfo_IsNeutralCulture_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureTypes /* System.Globalization.CultureTypes */
System_Globalization_CultureInfo_CultureTypes_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_CultureInfo_NumberFormat_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_NumberFormat_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_CultureInfo_DateTimeFormat_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_DateTimeFormat_Set(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_Calendar_t /* System.Globalization.Calendar */
System_Globalization_CultureInfo_Calendar_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CultureInfo_UseUserOverride_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CultureInfo_IsReadOnly_Get(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CultureInfo_Destroy(
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ self
);

#pragma mark - END APIs of System.Globalization.CultureInfo

#pragma mark - BEGIN APIs of System.Void
#pragma mark - END APIs of System.Void

#pragma mark - BEGIN APIs of System.Globalization.CultureTypes
#pragma mark - END APIs of System.Globalization.CultureTypes

#pragma mark - BEGIN APIs of System.Globalization.CompareInfo
System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */
System_Globalization_CompareInfo_GetCompareInfo(
	int32_t /* System.Int32 */ culture,
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ assembly,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */
System_Globalization_CompareInfo_GetCompareInfo1(
	CString /* System.String */ name,
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ assembly,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */
System_Globalization_CompareInfo_GetCompareInfo2(
	int32_t /* System.Int32 */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */
System_Globalization_CompareInfo_GetCompareInfo3(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsSortable(
	uint8_t /* System.Char */ ch,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsSortable1(
	CString /* System.String */ text,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsSortable2(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	CString /* System.String */ string2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	CString /* System.String */ string2,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare2(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	int32_t /* System.Int32 */ offset1,
	int32_t /* System.Int32 */ length1,
	CString /* System.String */ string2,
	int32_t /* System.Int32 */ offset2,
	int32_t /* System.Int32 */ length2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare3(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	int32_t /* System.Int32 */ offset1,
	CString /* System.String */ string2,
	int32_t /* System.Int32 */ offset2,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare4(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	int32_t /* System.Int32 */ offset1,
	CString /* System.String */ string2,
	int32_t /* System.Int32 */ offset2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_Compare5(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ string1,
	int32_t /* System.Int32 */ offset1,
	int32_t /* System.Int32 */ length1,
	CString /* System.String */ string2,
	int32_t /* System.Int32 */ offset2,
	int32_t /* System.Int32 */ length2,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsPrefix(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ prefix,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsPrefix1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ prefix,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsSuffix(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ suffix,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_IsSuffix1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ suffix,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf2(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf3(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf4(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf5(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf6(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf7(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf8(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf9(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf10(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_IndexOf11(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf2(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf3(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf4(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf5(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf6(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf7(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf8(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf9(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf10(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	uint8_t /* System.Char */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LastIndexOf11(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	CString /* System.String */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_SortKey_t /* System.Globalization.SortKey */
System_Globalization_CompareInfo_GetSortKey(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_SortKey_t /* System.Globalization.SortKey */
System_Globalization_CompareInfo_GetSortKey1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_CompareInfo_Equals(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_GetHashCode(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_GetHashCode1(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	CString /* System.String */ source,
	System_Globalization_CompareOptions /* System.Globalization.CompareOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CompareInfo_ToString(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_CompareInfo_Name_Get(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_SortVersion_t /* System.Globalization.SortVersion */
System_Globalization_CompareInfo_Version_Get(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_CompareInfo_LCID_Get(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_CompareInfo_Destroy(
	System_Globalization_CompareInfo_t /* System.Globalization.CompareInfo */ self
);

#pragma mark - END APIs of System.Globalization.CompareInfo

#pragma mark - BEGIN APIs of System.Reflection.Assembly
System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_Load(
	CString /* System.String */ assemblyString,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_LoadWithPartialName(
	CString /* System.String */ partialName,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_Load1(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ assemblyRef,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetExecutingAssembly(
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetCallingAssembly(
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */
System_Reflection_Assembly_GetManifestResourceInfo(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ resourceName,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_Stream_t /* System.IO.Stream */
System_Reflection_Assembly_GetManifestResourceStream(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_Stream_t /* System.IO.Stream */
System_Reflection_Assembly_GetManifestResourceStream1(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Type_t /* System.Type */ type,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */
System_Reflection_Assembly_GetName(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */
System_Reflection_Assembly_GetName1(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CBool /* System.Boolean */ copiedName,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Assembly_GetType(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Assembly_GetType1(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	CBool /* System.Boolean */ throwOnError,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Assembly_GetType2(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	CBool /* System.Boolean */ throwOnError,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_Assembly_IsDefined(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_Assembly_CreateInstance(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ typeName,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_Assembly_CreateInstance1(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ typeName,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Module_t /* System.Reflection.Module */
System_Reflection_Assembly_GetModule(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetSatelliteAssembly(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetSatelliteAssembly1(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Version_t /* System.Version */ version,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_Reflection_Assembly_GetFile(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_Assembly_GetObjectData(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_Assembly_ToString(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_Assembly_Equals(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_Assembly_GetHashCode(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_Assembly_CreateQualifiedName(
	CString /* System.String */ assemblyName,
	CString /* System.String */ typeName,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetAssembly(
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_GetEntryAssembly(
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_LoadFile(
	CString /* System.String */ path,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_LoadFrom(
	CString /* System.String */ assemblyFile,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_UnsafeLoadFrom(
	CString /* System.String */ assemblyFile,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_ReflectionOnlyLoad(
	CString /* System.String */ assemblyString,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_Assembly_ReflectionOnlyLoadFrom(
	CString /* System.String */ assemblyFile,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_Assembly_Destroy(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ self
);

#pragma mark - END APIs of System.Reflection.Assembly

#pragma mark - BEGIN APIs of System.Reflection.AssemblyName
System_Object_t /* System.Object */
System_Reflection_AssemblyName_Clone(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */
System_Reflection_AssemblyName_GetAssemblyName(
	CString /* System.String */ assemblyFile,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_ToString(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_GetObjectData(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_OnDeserialization(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Object_t /* System.Object */ sender,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_AssemblyName_ReferenceMatchesDefinition(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ reference,
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ definition,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */
System_Reflection_AssemblyName_Create(
	CString /* System.String */ assemblyName,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */
System_Reflection_AssemblyName_Create1(
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_Name_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_Name_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Reflection_AssemblyName_Version_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_Version_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Version_t /* System.Version */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */
System_Reflection_AssemblyName_CultureInfo_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_CultureInfo_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_CultureName_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_CultureName_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_CodeBase_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_CodeBase_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_EscapedCodeBase_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_ProcessorArchitecture /* System.Reflection.ProcessorArchitecture */
System_Reflection_AssemblyName_ProcessorArchitecture_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_ProcessorArchitecture_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Reflection_ProcessorArchitecture /* System.Reflection.ProcessorArchitecture */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyContentType /* System.Reflection.AssemblyContentType */
System_Reflection_AssemblyName_ContentType_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_ContentType_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Reflection_AssemblyContentType /* System.Reflection.AssemblyContentType */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_AssemblyNameFlags /* System.Reflection.AssemblyNameFlags */
System_Reflection_AssemblyName_Flags_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_Flags_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Reflection_AssemblyNameFlags /* System.Reflection.AssemblyNameFlags */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */
System_Reflection_AssemblyName_HashAlgorithm_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_HashAlgorithm_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Configuration_Assemblies_AssemblyHashAlgorithm /* System.Configuration.Assemblies.AssemblyHashAlgorithm */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Configuration_Assemblies_AssemblyVersionCompatibility /* System.Configuration.Assemblies.AssemblyVersionCompatibility */
System_Reflection_AssemblyName_VersionCompatibility_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_VersionCompatibility_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Configuration_Assemblies_AssemblyVersionCompatibility /* System.Configuration.Assemblies.AssemblyVersionCompatibility */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_StrongNameKeyPair_t /* System.Reflection.StrongNameKeyPair */
System_Reflection_AssemblyName_KeyPair_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_KeyPair_Set(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Reflection_StrongNameKeyPair_t /* System.Reflection.StrongNameKeyPair */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_AssemblyName_FullName_Get(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_AssemblyName_Destroy(
	System_Reflection_AssemblyName_t /* System.Reflection.AssemblyName */ self
);

#pragma mark - END APIs of System.Reflection.AssemblyName

#pragma mark - BEGIN APIs of System.Version
System_Object_t /* System.Object */
System_Version_Clone(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_CompareTo(
	System_Version_t /* System.Version */ self,
	System_Object_t /* System.Object */ version,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_CompareTo1(
	System_Version_t /* System.Version */ self,
	System_Version_t /* System.Version */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Version_Equals(
	System_Version_t /* System.Version */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Version_Equals1(
	System_Version_t /* System.Version */ self,
	System_Version_t /* System.Version */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_GetHashCode(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Version_ToString(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Version_ToString1(
	System_Version_t /* System.Version */ self,
	int32_t /* System.Int32 */ fieldCount,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Parse(
	CString /* System.String */ input,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Create(
	int32_t /* System.Int32 */ major,
	int32_t /* System.Int32 */ minor,
	int32_t /* System.Int32 */ build,
	int32_t /* System.Int32 */ revision,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Create1(
	int32_t /* System.Int32 */ major,
	int32_t /* System.Int32 */ minor,
	int32_t /* System.Int32 */ build,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Create2(
	int32_t /* System.Int32 */ major,
	int32_t /* System.Int32 */ minor,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Create3(
	CString /* System.String */ version,
	System_Exception_t* /* System.Exception */ outException
);

System_Version_t /* System.Version */
System_Version_Create4(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_Major_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_Minor_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_Build_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Version_Revision_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int16_t /* System.Int16 */
System_Version_MajorRevision_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

int16_t /* System.Int16 */
System_Version_MinorRevision_Get(
	System_Version_t /* System.Version */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Version_Destroy(
	System_Version_t /* System.Version */ self
);

#pragma mark - END APIs of System.Version


#pragma mark - BEGIN APIs of System.IFormatProvider
System_Object_t /* System.Object */
System_IFormatProvider_GetFormat(
	System_IFormatProvider_t /* System.IFormatProvider */ self,
	System_Type_t /* System.Type */ formatType,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IFormatProvider_Destroy(
	System_IFormatProvider_t /* System.IFormatProvider */ self
);

#pragma mark - END APIs of System.IFormatProvider

#pragma mark - BEGIN APIs of System.Globalization.NumberStyles
#pragma mark - END APIs of System.Globalization.NumberStyles

#pragma mark - BEGIN APIs of System.TypeCode
#pragma mark - END APIs of System.TypeCode

#pragma mark - BEGIN APIs of System.Reflection.ProcessorArchitecture
#pragma mark - END APIs of System.Reflection.ProcessorArchitecture

#pragma mark - BEGIN APIs of System.Reflection.AssemblyContentType
#pragma mark - END APIs of System.Reflection.AssemblyContentType

#pragma mark - BEGIN APIs of System.Reflection.AssemblyNameFlags
#pragma mark - END APIs of System.Reflection.AssemblyNameFlags

#pragma mark - BEGIN APIs of System.Configuration.Assemblies.AssemblyHashAlgorithm
#pragma mark - END APIs of System.Configuration.Assemblies.AssemblyHashAlgorithm

#pragma mark - BEGIN APIs of System.Configuration.Assemblies.AssemblyVersionCompatibility
#pragma mark - END APIs of System.Configuration.Assemblies.AssemblyVersionCompatibility

#pragma mark - BEGIN APIs of System.Reflection.StrongNameKeyPair
System_Reflection_StrongNameKeyPair_t /* System.Reflection.StrongNameKeyPair */
System_Reflection_StrongNameKeyPair_Create(
	System_IO_FileStream_t /* System.IO.FileStream */ keyPairFile,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_StrongNameKeyPair_t /* System.Reflection.StrongNameKeyPair */
System_Reflection_StrongNameKeyPair_Create1(
	CString /* System.String */ keyPairContainer,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_StrongNameKeyPair_Destroy(
	System_Reflection_StrongNameKeyPair_t /* System.Reflection.StrongNameKeyPair */ self
);

#pragma mark - END APIs of System.Reflection.StrongNameKeyPair

#pragma mark - BEGIN APIs of System.IO.FileStream
void /* System.Void */
System_IO_FileStream_Lock(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	int64_t /* System.Int64 */ position,
	int64_t /* System.Int64 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_Unlock(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	int64_t /* System.Int64 */ position,
	int64_t /* System.Int64 */ length,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_FileStream_FlushAsync(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_Flush1(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_Flush2(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	CBool /* System.Boolean */ flushToDisk,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_SetLength(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_IO_FileStream_ReadByte(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_WriteByte(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	uint8_t /* System.Byte */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_IO_FileStream_DisposeAsync(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_CopyTo(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_FileStream_CopyToAsync(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	int32_t /* System.Int32 */ bufferSize,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_IO_FileStream_EndRead(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_IAsyncResult_t /* System.IAsyncResult */ asyncResult,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_EndWrite(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_IAsyncResult_t /* System.IAsyncResult */ asyncResult,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_IO_FileStream_Seek(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	int64_t /* System.Int64 */ offset,
	System_IO_SeekOrigin /* System.IO.SeekOrigin */ origin,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create(
	int /* System.IntPtr */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create1(
	int /* System.IntPtr */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	CBool /* System.Boolean */ ownsHandle,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create2(
	int /* System.IntPtr */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	CBool /* System.Boolean */ ownsHandle,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create3(
	int /* System.IntPtr */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	CBool /* System.Boolean */ ownsHandle,
	int32_t /* System.Int32 */ bufferSize,
	CBool /* System.Boolean */ isAsync,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create4(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create5(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create6(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ handle,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	int32_t /* System.Int32 */ bufferSize,
	CBool /* System.Boolean */ isAsync,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create7(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create8(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create9(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_IO_FileShare /* System.IO.FileShare */ share,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create10(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_IO_FileShare /* System.IO.FileShare */ share,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create11(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_IO_FileShare /* System.IO.FileShare */ share,
	int32_t /* System.Int32 */ bufferSize,
	CBool /* System.Boolean */ useAsync,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create12(
	CString /* System.String */ path,
	System_IO_FileMode /* System.IO.FileMode */ mode,
	System_IO_FileAccess /* System.IO.FileAccess */ access,
	System_IO_FileShare /* System.IO.FileShare */ share,
	int32_t /* System.Int32 */ bufferSize,
	System_IO_FileOptions /* System.IO.FileOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileStream_t /* System.IO.FileStream */
System_IO_FileStream_Create13(
	CString /* System.String */ path,
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ options,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_IO_FileStream_Handle_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_IO_FileStream_CanRead_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_IO_FileStream_CanWrite_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */
System_IO_FileStream_SafeFileHandle_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_IO_FileStream_Name_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_IO_FileStream_IsAsync_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_IO_FileStream_Length_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_IO_FileStream_Position_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_Position_Set(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_IO_FileStream_CanSeek_Get(
	System_IO_FileStream_t /* System.IO.FileStream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStream_Destroy(
	System_IO_FileStream_t /* System.IO.FileStream */ self
);

#pragma mark - END APIs of System.IO.FileStream

#pragma mark - BEGIN APIs of System.IO.Stream
void /* System.Void */
System_IO_Stream_CopyTo(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_CopyTo1(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_CopyToAsync(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_CopyToAsync1(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	int32_t /* System.Int32 */ bufferSize,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_CopyToAsync2(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_CopyToAsync3(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IO_Stream_t /* System.IO.Stream */ destination,
	int32_t /* System.Int32 */ bufferSize,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_Dispose(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_Close(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_IO_Stream_DisposeAsync(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_Flush(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_FlushAsync(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_IO_Stream_FlushAsync1(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_IO_Stream_EndRead(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IAsyncResult_t /* System.IAsyncResult */ asyncResult,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_EndWrite(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_IAsyncResult_t /* System.IAsyncResult */ asyncResult,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_IO_Stream_Seek(
	System_IO_Stream_t /* System.IO.Stream */ self,
	int64_t /* System.Int64 */ offset,
	System_IO_SeekOrigin /* System.IO.SeekOrigin */ origin,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_SetLength(
	System_IO_Stream_t /* System.IO.Stream */ self,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_IO_Stream_ReadByte(
	System_IO_Stream_t /* System.IO.Stream */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_Stream_WriteByte(
	System_IO_Stream_t /* System.IO.Stream */ self,
	uint8_t /* System.Byte */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_Stream_t /* System.IO.Stream */
System_IO_Stream_Synchronized(
	System_IO_Stream_t /* System.IO.Stream */ stream,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_Stream_t /* System.IO.Stream */
System_IO_Stream_Null_Get(
	
);

void /* System.Void */
System_IO_Stream_Destroy(
	System_IO_Stream_t /* System.IO.Stream */ self
);

#pragma mark - END APIs of System.IO.Stream

#pragma mark - BEGIN APIs of System.MarshalByRefObject
System_Object_t /* System.Object */
System_MarshalByRefObject_GetLifetimeService(
	System_MarshalByRefObject_t /* System.MarshalByRefObject */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_MarshalByRefObject_InitializeLifetimeService(
	System_MarshalByRefObject_t /* System.MarshalByRefObject */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_MarshalByRefObject_Destroy(
	System_MarshalByRefObject_t /* System.MarshalByRefObject */ self
);

#pragma mark - END APIs of System.MarshalByRefObject


#pragma mark - BEGIN APIs of System.Threading.Tasks.Task
void /* System.Void */
System_Threading_Tasks_Task_Start(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_Start1(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */ scheduler,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_RunSynchronously(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_RunSynchronously1(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */ scheduler,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_Dispose(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_CompilerServices_TaskAwaiter_t /* System.Runtime.CompilerServices.TaskAwaiter */
System_Threading_Tasks_Task_GetAwaiter(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable */
System_Threading_Tasks_Task_ConfigureAwait(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	CBool /* System.Boolean */ continueOnCapturedContext,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_CompilerServices_YieldAwaitable_t /* System.Runtime.CompilerServices.YieldAwaitable */
System_Threading_Tasks_Task_Yield(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_Wait(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_Wait1(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_Wait2(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_Wait3(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_Wait4(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	int32_t /* System.Int32 */ millisecondsTimeout,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_Wait5(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	int32_t /* System.Int32 */ millisecondsTimeout,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_WaitAsync(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_WaitAsync1(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_WaitAsync2(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_FromException(
	System_Exception_t /* System.Exception */ exception,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_FromCanceled(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_Delay(
	System_TimeSpan_t /* System.TimeSpan */ delay,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_Delay1(
	System_TimeSpan_t /* System.TimeSpan */ delay,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_Delay2(
	int32_t /* System.Int32 */ millisecondsDelay,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_Delay3(
	int32_t /* System.Int32 */ millisecondsDelay,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Threading_Tasks_Task_Id_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_AggregateException_t /* System.AggregateException */
System_Threading_Tasks_Task_Exception_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskStatus /* System.Threading.Tasks.TaskStatus */
System_Threading_Tasks_Task_Status_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_IsCanceled_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_IsCompleted_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_IsCompletedSuccessfully_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */
System_Threading_Tasks_Task_CreationOptions_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Threading_Tasks_Task_AsyncState_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_Task_Factory_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_Task_CompletedTask_Get(
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_Task_IsFaulted_Get(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Task_Destroy(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ self
);

#pragma mark - END APIs of System.Threading.Tasks.Task

#pragma mark - BEGIN APIs of System.Threading.Tasks.TaskScheduler
System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */
System_Threading_Tasks_TaskScheduler_FromCurrentSynchronizationContext(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_TaskScheduler_Destroy(
	System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */ self
);

#pragma mark - END APIs of System.Threading.Tasks.TaskScheduler

#pragma mark - BEGIN APIs of System.AggregateException
void /* System.Void */
System_AggregateException_GetObjectData(
	System_AggregateException_t /* System.AggregateException */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

System_Exception_t /* System.Exception */
System_AggregateException_GetBaseException(
	System_AggregateException_t /* System.AggregateException */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_AggregateException_t /* System.AggregateException */
System_AggregateException_Flatten(
	System_AggregateException_t /* System.AggregateException */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_AggregateException_ToString(
	System_AggregateException_t /* System.AggregateException */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_AggregateException_t /* System.AggregateException */
System_AggregateException_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_AggregateException_t /* System.AggregateException */
System_AggregateException_Create1(
	CString /* System.String */ message,
	System_Exception_t* /* System.Exception */ outException
);

System_AggregateException_t /* System.AggregateException */
System_AggregateException_Create2(
	CString /* System.String */ message,
	System_Exception_t /* System.Exception */ innerException,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_AggregateException_Message_Get(
	System_AggregateException_t /* System.AggregateException */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_AggregateException_Destroy(
	System_AggregateException_t /* System.AggregateException */ self
);

#pragma mark - END APIs of System.AggregateException

#pragma mark - BEGIN APIs of System.Exception
System_Exception_t /* System.Exception */
System_Exception_GetBaseException(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Exception_GetObjectData(
	System_Exception_t /* System.Exception */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Exception_ToString(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Exception_GetType(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Exception_t /* System.Exception */
System_Exception_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_Exception_t /* System.Exception */
System_Exception_Create1(
	CString /* System.String */ message,
	System_Exception_t* /* System.Exception */ outException
);

System_Exception_t /* System.Exception */
System_Exception_Create2(
	CString /* System.String */ message,
	System_Exception_t /* System.Exception */ innerException,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodBase_t /* System.Reflection.MethodBase */
System_Exception_TargetSite_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Exception_Message_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Collections_IDictionary_t /* System.Collections.IDictionary */
System_Exception_Data_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Exception_t /* System.Exception */
System_Exception_InnerException_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Exception_HelpLink_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Exception_HelpLink_Set(
	System_Exception_t /* System.Exception */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Exception_Source_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Exception_Source_Set(
	System_Exception_t /* System.Exception */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Exception_HResult_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Exception_HResult_Set(
	System_Exception_t /* System.Exception */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Exception_StackTrace_Get(
	System_Exception_t /* System.Exception */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Exception_Destroy(
	System_Exception_t /* System.Exception */ self
);

#pragma mark - END APIs of System.Exception

#pragma mark - BEGIN APIs of System.Reflection.MethodBase
System_Reflection_MethodBase_t /* System.Reflection.MethodBase */
System_Reflection_MethodBase_GetMethodFromHandle(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodBase_t /* System.Reflection.MethodBase */
System_Reflection_MethodBase_GetMethodFromHandle1(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ handle,
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ declaringType,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodBase_t /* System.Reflection.MethodBase */
System_Reflection_MethodBase_GetCurrentMethod(
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodImplAttributes /* System.Reflection.MethodImplAttributes */
System_Reflection_MethodBase_GetMethodImplementationFlags(
	System_Reflection_MethodBase_t /* System.Reflection.MethodBase */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodBody_t /* System.Reflection.MethodBody */
System_Reflection_MethodBase_GetMethodBody(
	System_Reflection_MethodBase_t /* System.Reflection.MethodBase */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_MethodBase_Equals(
	System_Reflection_MethodBase_t /* System.Reflection.MethodBase */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_MethodBase_GetHashCode(
	System_Reflection_MethodBase_t /* System.Reflection.MethodBase */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_MethodBase_Destroy(
	System_Reflection_MethodBase_t /* System.Reflection.MethodBase */ self
);

#pragma mark - END APIs of System.Reflection.MethodBase

#pragma mark - BEGIN APIs of System.RuntimeMethodHandle
void /* System.Void */
System_RuntimeMethodHandle_GetObjectData(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_RuntimeMethodHandle_GetHashCode(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeMethodHandle_Equals(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */
System_RuntimeMethodHandle_FromIntPtr(
	int /* System.IntPtr */ value,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeMethodHandle_ToIntPtr(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeMethodHandle_Equals1(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeMethodHandle_GetFunctionPointer(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeMethodHandle_Value_Get(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_RuntimeMethodHandle_Destroy(
	System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */ self
);

#pragma mark - END APIs of System.RuntimeMethodHandle

#pragma mark - BEGIN APIs of System.Runtime.Serialization.SerializationInfo
void /* System.Void */
System_Runtime_Serialization_SerializationInfo_SetType(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */
System_Runtime_Serialization_SerializationInfo_GetEnumerator(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Object_t /* System.Object */ value,
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue1(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue2(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	CBool /* System.Boolean */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue3(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	uint8_t /* System.Char */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue4(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	int8_t /* System.SByte */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue5(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	uint8_t /* System.Byte */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue6(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	int16_t /* System.Int16 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue7(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	uint16_t /* System.UInt16 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue8(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue9(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	uint32_t /* System.UInt32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue10(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue11(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	uint64_t /* System.UInt64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue12(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	float /* System.Single */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue13(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue14(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AddValue15(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_DateTime_t /* System.DateTime */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Runtime_Serialization_SerializationInfo_GetValue(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_Serialization_SerializationInfo_GetBoolean(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Runtime_Serialization_SerializationInfo_GetChar(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

int8_t /* System.SByte */
System_Runtime_Serialization_SerializationInfo_GetSByte(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Byte */
System_Runtime_Serialization_SerializationInfo_GetByte(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

int16_t /* System.Int16 */
System_Runtime_Serialization_SerializationInfo_GetInt16(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

uint16_t /* System.UInt16 */
System_Runtime_Serialization_SerializationInfo_GetUInt16(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Runtime_Serialization_SerializationInfo_GetInt32(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

uint32_t /* System.UInt32 */
System_Runtime_Serialization_SerializationInfo_GetUInt32(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_Runtime_Serialization_SerializationInfo_GetInt64(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

uint64_t /* System.UInt64 */
System_Runtime_Serialization_SerializationInfo_GetUInt64(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

float /* System.Single */
System_Runtime_Serialization_SerializationInfo_GetSingle(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_Runtime_Serialization_SerializationInfo_GetDouble(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Runtime_Serialization_SerializationInfo_GetDecimal(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Runtime_Serialization_SerializationInfo_GetDateTime(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_SerializationInfo_GetString(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */
System_Runtime_Serialization_SerializationInfo_Create(
	System_Type_t /* System.Type */ type,
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ converter,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */
System_Runtime_Serialization_SerializationInfo_Create1(
	System_Type_t /* System.Type */ type,
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ converter,
	CBool /* System.Boolean */ requireSameTokenInPartialTrust,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_SerializationInfo_FullTypeName_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_FullTypeName_Set(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_SerializationInfo_AssemblyName_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_AssemblyName_Set(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_Serialization_SerializationInfo_IsFullTypeNameSetExplicit_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_Serialization_SerializationInfo_IsAssemblyNameSetExplicit_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Runtime_Serialization_SerializationInfo_MemberCount_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Runtime_Serialization_SerializationInfo_ObjectType_Get(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfo_Destroy(
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ self
);

#pragma mark - END APIs of System.Runtime.Serialization.SerializationInfo

#pragma mark - BEGIN APIs of System.Runtime.Serialization.SerializationInfoEnumerator
CBool /* System.Boolean */
System_Runtime_Serialization_SerializationInfoEnumerator_MoveNext(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfoEnumerator_Reset(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_SerializationEntry_t /* System.Runtime.Serialization.SerializationEntry */
System_Runtime_Serialization_SerializationInfoEnumerator_Current_Get(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_SerializationInfoEnumerator_Name_Get(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Runtime_Serialization_SerializationInfoEnumerator_Value_Get(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Runtime_Serialization_SerializationInfoEnumerator_ObjectType_Get(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationInfoEnumerator_Destroy(
	System_Runtime_Serialization_SerializationInfoEnumerator_t /* System.Runtime.Serialization.SerializationInfoEnumerator */ self
);

#pragma mark - END APIs of System.Runtime.Serialization.SerializationInfoEnumerator

#pragma mark - BEGIN APIs of System.Runtime.Serialization.SerializationEntry
System_Object_t /* System.Object */
System_Runtime_Serialization_SerializationEntry_Value_Get(
	System_Runtime_Serialization_SerializationEntry_t /* System.Runtime.Serialization.SerializationEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_SerializationEntry_Name_Get(
	System_Runtime_Serialization_SerializationEntry_t /* System.Runtime.Serialization.SerializationEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Runtime_Serialization_SerializationEntry_ObjectType_Get(
	System_Runtime_Serialization_SerializationEntry_t /* System.Runtime.Serialization.SerializationEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_SerializationEntry_Destroy(
	System_Runtime_Serialization_SerializationEntry_t /* System.Runtime.Serialization.SerializationEntry */ self
);

#pragma mark - END APIs of System.Runtime.Serialization.SerializationEntry


#pragma mark - BEGIN APIs of System.Globalization.UnicodeCategory
#pragma mark - END APIs of System.Globalization.UnicodeCategory


#pragma mark - BEGIN APIs of System.MidpointRounding
#pragma mark - END APIs of System.MidpointRounding







#pragma mark - BEGIN APIs of System.Decimal
System_Decimal_t /* System.Decimal */
System_Decimal_FromOACurrency(
	int64_t /* System.Int64 */ cy,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_Decimal_ToOACurrency(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Add(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Ceiling(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_Compare(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_CompareTo(
	System_Decimal_t /* System.Decimal */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_CompareTo1(
	System_Decimal_t /* System.Decimal */ self,
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Divide(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_Equals(
	System_Decimal_t /* System.Decimal */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_Equals1(
	System_Decimal_t /* System.Decimal */ self,
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_GetHashCode(
	System_Decimal_t /* System.Decimal */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_Equals2(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Floor(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Decimal_ToString(
	System_Decimal_t /* System.Decimal */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Decimal_ToString1(
	System_Decimal_t /* System.Decimal */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Decimal_ToString2(
	System_Decimal_t /* System.Decimal */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Decimal_ToString3(
	System_Decimal_t /* System.Decimal */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Parse(
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Parse1(
	CString /* System.String */ s,
	System_Globalization_NumberStyles /* System.Globalization.NumberStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Parse2(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Parse3(
	CString /* System.String */ s,
	System_Globalization_NumberStyles /* System.Globalization.NumberStyles */ style,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Remainder(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Multiply(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Negate(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Round(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Round1(
	System_Decimal_t /* System.Decimal */ d,
	int32_t /* System.Int32 */ decimals,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Round2(
	System_Decimal_t /* System.Decimal */ d,
	System_MidpointRounding /* System.MidpointRounding */ mode,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Round3(
	System_Decimal_t /* System.Decimal */ d,
	int32_t /* System.Int32 */ decimals,
	System_MidpointRounding /* System.MidpointRounding */ mode,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Subtract(
	System_Decimal_t /* System.Decimal */ d1,
	System_Decimal_t /* System.Decimal */ d2,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Byte */
System_Decimal_ToByte(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

int8_t /* System.SByte */
System_Decimal_ToSByte(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

int16_t /* System.Int16 */
System_Decimal_ToInt16(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_Decimal_ToDouble(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_ToInt32(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_Decimal_ToInt64(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

uint16_t /* System.UInt16 */
System_Decimal_ToUInt16(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint32_t /* System.UInt32 */
System_Decimal_ToUInt32(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

uint64_t /* System.UInt64 */
System_Decimal_ToUInt64(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

float /* System.Single */
System_Decimal_ToSingle(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Truncate(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

System_TypeCode /* System.TypeCode */
System_Decimal_GetTypeCode(
	System_Decimal_t /* System.Decimal */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Clamp(
	System_Decimal_t /* System.Decimal */ value,
	System_Decimal_t /* System.Decimal */ min,
	System_Decimal_t /* System.Decimal */ max,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_CopySign(
	System_Decimal_t /* System.Decimal */ value,
	System_Decimal_t /* System.Decimal */ sign,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Max(
	System_Decimal_t /* System.Decimal */ x,
	System_Decimal_t /* System.Decimal */ y,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Min(
	System_Decimal_t /* System.Decimal */ x,
	System_Decimal_t /* System.Decimal */ y,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Decimal_Sign(
	System_Decimal_t /* System.Decimal */ d,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Abs(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsCanonical(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsEvenInteger(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsInteger(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsNegative(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsOddInteger(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Decimal_IsPositive(
	System_Decimal_t /* System.Decimal */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_MaxMagnitude(
	System_Decimal_t /* System.Decimal */ x,
	System_Decimal_t /* System.Decimal */ y,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_MinMagnitude(
	System_Decimal_t /* System.Decimal */ x,
	System_Decimal_t /* System.Decimal */ y,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create(
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create1(
	uint32_t /* System.UInt32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create2(
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create3(
	uint64_t /* System.UInt64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create4(
	float /* System.Single */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create5(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Create6(
	int32_t /* System.Int32 */ lo,
	int32_t /* System.Int32 */ mid,
	int32_t /* System.Int32 */ hi,
	CBool /* System.Boolean */ isNegative,
	uint8_t /* System.Byte */ scale,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Byte */
System_Decimal_Scale_Get(
	System_Decimal_t /* System.Decimal */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Decimal_Zero_Get(
	
);

System_Decimal_t /* System.Decimal */
System_Decimal_One_Get(
	
);

System_Decimal_t /* System.Decimal */
System_Decimal_MinusOne_Get(
	
);

System_Decimal_t /* System.Decimal */
System_Decimal_MaxValue_Get(
	
);

System_Decimal_t /* System.Decimal */
System_Decimal_MinValue_Get(
	
);

void /* System.Void */
System_Decimal_Destroy(
	System_Decimal_t /* System.Decimal */ self
);

#pragma mark - END APIs of System.Decimal

#pragma mark - BEGIN APIs of System.DateTime
System_DateTime_t /* System.DateTime */
System_DateTime_Add(
	System_DateTime_t /* System.DateTime */ self,
	System_TimeSpan_t /* System.TimeSpan */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddDays(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddHours(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddMilliseconds(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddMicroseconds(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddMinutes(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddMonths(
	System_DateTime_t /* System.DateTime */ self,
	int32_t /* System.Int32 */ months,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddSeconds(
	System_DateTime_t /* System.DateTime */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddTicks(
	System_DateTime_t /* System.DateTime */ self,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_AddYears(
	System_DateTime_t /* System.DateTime */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Compare(
	System_DateTime_t /* System.DateTime */ t1,
	System_DateTime_t /* System.DateTime */ t2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_CompareTo(
	System_DateTime_t /* System.DateTime */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_CompareTo1(
	System_DateTime_t /* System.DateTime */ self,
	System_DateTime_t /* System.DateTime */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_DaysInMonth(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateTime_Equals(
	System_DateTime_t /* System.DateTime */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateTime_Equals1(
	System_DateTime_t /* System.DateTime */ self,
	System_DateTime_t /* System.DateTime */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateTime_Equals2(
	System_DateTime_t /* System.DateTime */ t1,
	System_DateTime_t /* System.DateTime */ t2,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_FromBinary(
	int64_t /* System.Int64 */ dateData,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_FromFileTime(
	int64_t /* System.Int64 */ fileTime,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_FromFileTimeUtc(
	int64_t /* System.Int64 */ fileTime,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_FromOADate(
	double /* System.Double */ d,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateTime_IsDaylightSavingTime(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_SpecifyKind(
	System_DateTime_t /* System.DateTime */ value,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_DateTime_ToBinary(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_GetHashCode(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateTime_IsLeapYear(
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Parse(
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Parse1(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Parse2(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ styles,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_ParseExact(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_ParseExact1(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_DateTime_Subtract(
	System_DateTime_t /* System.DateTime */ self,
	System_DateTime_t /* System.DateTime */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Subtract1(
	System_DateTime_t /* System.DateTime */ self,
	System_TimeSpan_t /* System.TimeSpan */ value,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_DateTime_ToOADate(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_DateTime_ToFileTime(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_DateTime_ToFileTimeUtc(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_ToLocalTime(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToLongDateString(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToLongTimeString(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToShortDateString(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToShortTimeString(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToString(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToString1(
	System_DateTime_t /* System.DateTime */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToString2(
	System_DateTime_t /* System.DateTime */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateTime_ToString3(
	System_DateTime_t /* System.DateTime */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_ToUniversalTime(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TypeCode /* System.TypeCode */
System_DateTime_GetTypeCode(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create(
	int64_t /* System.Int64 */ ticks,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create1(
	int64_t /* System.Int64 */ ticks,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create2(
	System_DateOnly_t /* System.DateOnly */ date,
	System_TimeOnly_t /* System.TimeOnly */ time,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create3(
	System_DateOnly_t /* System.DateOnly */ date,
	System_TimeOnly_t /* System.TimeOnly */ time,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create4(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create5(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create6(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create7(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create8(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create9(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create10(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create11(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create12(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create13(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ microsecond,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create14(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ microsecond,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create15(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ microsecond,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Create16(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ microsecond,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Date_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Day_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DayOfWeek /* System.DayOfWeek */
System_DateTime_DayOfWeek_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_DayOfYear_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Hour_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTimeKind /* System.DateTimeKind */
System_DateTime_Kind_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Millisecond_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Microsecond_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Nanosecond_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Minute_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Month_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Now_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Second_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_DateTime_Ticks_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_DateTime_TimeOfDay_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_Today_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateTime_Year_Get(
	System_DateTime_t /* System.DateTime */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_UtcNow_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateTime_MinValue_Get(
	
);

System_DateTime_t /* System.DateTime */
System_DateTime_MaxValue_Get(
	
);

System_DateTime_t /* System.DateTime */
System_DateTime_UnixEpoch_Get(
	
);

void /* System.Void */
System_DateTime_Destroy(
	System_DateTime_t /* System.DateTime */ self
);

#pragma mark - END APIs of System.DateTime

#pragma mark - BEGIN APIs of System.TimeSpan
System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Add(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_TimeSpan_t /* System.TimeSpan */ ts,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Compare(
	System_TimeSpan_t /* System.TimeSpan */ t1,
	System_TimeSpan_t /* System.TimeSpan */ t2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_CompareTo(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_CompareTo1(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_TimeSpan_t /* System.TimeSpan */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromDays(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Duration(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeSpan_Equals(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeSpan_Equals1(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_TimeSpan_t /* System.TimeSpan */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeSpan_Equals2(
	System_TimeSpan_t /* System.TimeSpan */ t1,
	System_TimeSpan_t /* System.TimeSpan */ t2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_GetHashCode(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromHours(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromMilliseconds(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromMicroseconds(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromMinutes(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Negate(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromSeconds(
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Subtract(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_TimeSpan_t /* System.TimeSpan */ ts,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Multiply(
	System_TimeSpan_t /* System.TimeSpan */ self,
	double /* System.Double */ factor,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Divide(
	System_TimeSpan_t /* System.TimeSpan */ self,
	double /* System.Double */ divisor,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_Divide1(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_TimeSpan_t /* System.TimeSpan */ ts,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_FromTicks(
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Parse(
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Parse1(
	CString /* System.String */ input,
	System_IFormatProvider_t /* System.IFormatProvider */ formatProvider,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_ParseExact(
	CString /* System.String */ input,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ formatProvider,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_ParseExact1(
	CString /* System.String */ input,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ formatProvider,
	System_Globalization_TimeSpanStyles /* System.Globalization.TimeSpanStyles */ styles,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeSpan_ToString(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeSpan_ToString1(
	System_TimeSpan_t /* System.TimeSpan */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeSpan_ToString2(
	System_TimeSpan_t /* System.TimeSpan */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ formatProvider,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Create(
	int64_t /* System.Int64 */ ticks,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Create1(
	int32_t /* System.Int32 */ hours,
	int32_t /* System.Int32 */ minutes,
	int32_t /* System.Int32 */ seconds,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Create2(
	int32_t /* System.Int32 */ days,
	int32_t /* System.Int32 */ hours,
	int32_t /* System.Int32 */ minutes,
	int32_t /* System.Int32 */ seconds,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Create3(
	int32_t /* System.Int32 */ days,
	int32_t /* System.Int32 */ hours,
	int32_t /* System.Int32 */ minutes,
	int32_t /* System.Int32 */ seconds,
	int32_t /* System.Int32 */ milliseconds,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Create4(
	int32_t /* System.Int32 */ days,
	int32_t /* System.Int32 */ hours,
	int32_t /* System.Int32 */ minutes,
	int32_t /* System.Int32 */ seconds,
	int32_t /* System.Int32 */ milliseconds,
	int32_t /* System.Int32 */ microseconds,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_TimeSpan_Ticks_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Days_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Hours_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Milliseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Microseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Nanoseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Minutes_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeSpan_Seconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalDays_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalHours_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalMilliseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalMicroseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalNanoseconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalMinutes_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_TimeSpan_TotalSeconds_Get(
	System_TimeSpan_t /* System.TimeSpan */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_Zero_Get(
	
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_MaxValue_Get(
	
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeSpan_MinValue_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_NanosecondsPerTick_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerMicrosecond_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerMillisecond_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerSecond_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerMinute_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerHour_Get(
	
);

int64_t /* System.Int64 */
System_TimeSpan_TicksPerDay_Get(
	
);

void /* System.Void */
System_TimeSpan_Destroy(
	System_TimeSpan_t /* System.TimeSpan */ self
);

#pragma mark - END APIs of System.TimeSpan

#pragma mark - BEGIN APIs of System.Globalization.TimeSpanStyles
#pragma mark - END APIs of System.Globalization.TimeSpanStyles

#pragma mark - BEGIN APIs of System.DateTimeKind
#pragma mark - END APIs of System.DateTimeKind

#pragma mark - BEGIN APIs of System.DayOfWeek
#pragma mark - END APIs of System.DayOfWeek

#pragma mark - BEGIN APIs of System.Globalization.DateTimeStyles
#pragma mark - END APIs of System.Globalization.DateTimeStyles

#pragma mark - BEGIN APIs of System.DateOnly
System_DateOnly_t /* System.DateOnly */
System_DateOnly_FromDayNumber(
	int32_t /* System.Int32 */ dayNumber,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_AddDays(
	System_DateOnly_t /* System.DateOnly */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_AddMonths(
	System_DateOnly_t /* System.DateOnly */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_AddYears(
	System_DateOnly_t /* System.DateOnly */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateOnly_ToDateTime(
	System_DateOnly_t /* System.DateOnly */ self,
	System_TimeOnly_t /* System.TimeOnly */ time,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_DateOnly_ToDateTime1(
	System_DateOnly_t /* System.DateOnly */ self,
	System_TimeOnly_t /* System.TimeOnly */ time,
	System_DateTimeKind /* System.DateTimeKind */ kind,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_FromDateTime(
	System_DateTime_t /* System.DateTime */ dateTime,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_CompareTo(
	System_DateOnly_t /* System.DateOnly */ self,
	System_DateOnly_t /* System.DateOnly */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_CompareTo1(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateOnly_Equals(
	System_DateOnly_t /* System.DateOnly */ self,
	System_DateOnly_t /* System.DateOnly */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_DateOnly_Equals1(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_GetHashCode(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_Parse(
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_Parse1(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_ParseExact(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_ParseExact1(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToLongDateString(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToShortDateString(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToString(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToString1(
	System_DateOnly_t /* System.DateOnly */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToString2(
	System_DateOnly_t /* System.DateOnly */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_DateOnly_ToString3(
	System_DateOnly_t /* System.DateOnly */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_Parse2(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_Create(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_Create1(
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_MinValue_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_DateOnly_t /* System.DateOnly */
System_DateOnly_MaxValue_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_Year_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_Month_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_Day_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DayOfWeek /* System.DayOfWeek */
System_DateOnly_DayOfWeek_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_DayOfYear_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_DateOnly_DayNumber_Get(
	System_DateOnly_t /* System.DateOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_DateOnly_Destroy(
	System_DateOnly_t /* System.DateOnly */ self
);

#pragma mark - END APIs of System.DateOnly

#pragma mark - BEGIN APIs of System.TimeOnly
System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Add(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_TimeSpan_t /* System.TimeSpan */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_AddHours(
	System_TimeOnly_t /* System.TimeOnly */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_AddMinutes(
	System_TimeOnly_t /* System.TimeOnly */ self,
	double /* System.Double */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeOnly_IsBetween(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_TimeOnly_t /* System.TimeOnly */ start,
	System_TimeOnly_t /* System.TimeOnly */ end,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_FromTimeSpan(
	System_TimeSpan_t /* System.TimeSpan */ timeSpan,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_FromDateTime(
	System_DateTime_t /* System.DateTime */ dateTime,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeSpan_t /* System.TimeSpan */
System_TimeOnly_ToTimeSpan(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_CompareTo(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_TimeOnly_t /* System.TimeOnly */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_CompareTo1(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeOnly_Equals(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_TimeOnly_t /* System.TimeOnly */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_TimeOnly_Equals1(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_GetHashCode(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Parse(
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Parse1(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_ParseExact(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_ParseExact1(
	CString /* System.String */ s,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Globalization_DateTimeStyles /* System.Globalization.DateTimeStyles */ style,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToLongTimeString(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToShortTimeString(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToString(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToString1(
	System_TimeOnly_t /* System.TimeOnly */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToString2(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_TimeOnly_ToString3(
	System_TimeOnly_t /* System.TimeOnly */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Parse2(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Create(
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Create1(
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Create2(
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Create3(
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ microsecond,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_Create4(
	int64_t /* System.Int64 */ ticks,
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_MinValue_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_TimeOnly_t /* System.TimeOnly */
System_TimeOnly_MaxValue_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Hour_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Minute_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Second_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Millisecond_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Microsecond_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_TimeOnly_Nanosecond_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_TimeOnly_Ticks_Get(
	System_TimeOnly_t /* System.TimeOnly */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_TimeOnly_Destroy(
	System_TimeOnly_t /* System.TimeOnly */ self
);

#pragma mark - END APIs of System.TimeOnly

#pragma mark - BEGIN APIs of System.Globalization.Calendar
System_Object_t /* System.Object */
System_Globalization_Calendar_Clone(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_Calendar_t /* System.Globalization.Calendar */
System_Globalization_Calendar_ReadOnly(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ calendar,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddMilliseconds(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	double /* System.Double */ milliseconds,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddDays(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ days,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddHours(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ hours,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddMinutes(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ minutes,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddMonths(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ months,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddSeconds(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ seconds,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddWeeks(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ weeks,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_AddYears(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	int32_t /* System.Int32 */ years,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDayOfMonth(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

System_DayOfWeek /* System.DayOfWeek */
System_Globalization_Calendar_GetDayOfWeek(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDayOfYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDaysInMonth(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDaysInMonth1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDaysInYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetDaysInYear1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetEra(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetHour(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_Globalization_Calendar_GetMilliseconds(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetMinute(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetMonth(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetMonthsInYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetMonthsInYear1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetSecond(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetWeekOfYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */ rule,
	System_DayOfWeek /* System.DayOfWeek */ firstDayOfWeek,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	System_DateTime_t /* System.DateTime */ time,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapDay(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapDay1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapMonth(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapMonth1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetLeapMonth(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_GetLeapMonth1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_Calendar_IsLeapYear1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_ToDateTime(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Globalization_Calendar_ToDateTime1(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	int32_t /* System.Int32 */ month,
	int32_t /* System.Int32 */ day,
	int32_t /* System.Int32 */ hour,
	int32_t /* System.Int32 */ minute,
	int32_t /* System.Int32 */ second,
	int32_t /* System.Int32 */ millisecond,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_ToFourDigitYear(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self,
	int32_t /* System.Int32 */ year,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_Calendar_CurrentEra_Get(
	
);

void /* System.Void */
System_Globalization_Calendar_Destroy(
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ self
);

#pragma mark - END APIs of System.Globalization.Calendar

#pragma mark - BEGIN APIs of System.Globalization.CalendarAlgorithmType
#pragma mark - END APIs of System.Globalization.CalendarAlgorithmType

#pragma mark - BEGIN APIs of System.Globalization.CalendarWeekRule
#pragma mark - END APIs of System.Globalization.CalendarWeekRule

#pragma mark - BEGIN APIs of System.Runtime.Serialization.IFormatterConverter
System_Object_t /* System.Object */
System_Runtime_Serialization_IFormatterConverter_Convert(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Type_t /* System.Type */ type,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Runtime_Serialization_IFormatterConverter_Convert1(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_TypeCode /* System.TypeCode */ typeCode,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_Serialization_IFormatterConverter_ToBoolean(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Runtime_Serialization_IFormatterConverter_ToChar(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int8_t /* System.SByte */
System_Runtime_Serialization_IFormatterConverter_ToSByte(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Byte */
System_Runtime_Serialization_IFormatterConverter_ToByte(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int16_t /* System.Int16 */
System_Runtime_Serialization_IFormatterConverter_ToInt16(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint16_t /* System.UInt16 */
System_Runtime_Serialization_IFormatterConverter_ToUInt16(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Runtime_Serialization_IFormatterConverter_ToInt32(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint32_t /* System.UInt32 */
System_Runtime_Serialization_IFormatterConverter_ToUInt32(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_Runtime_Serialization_IFormatterConverter_ToInt64(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

uint64_t /* System.UInt64 */
System_Runtime_Serialization_IFormatterConverter_ToUInt64(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

float /* System.Single */
System_Runtime_Serialization_IFormatterConverter_ToSingle(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_Runtime_Serialization_IFormatterConverter_ToDouble(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Decimal_t /* System.Decimal */
System_Runtime_Serialization_IFormatterConverter_ToDecimal(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
System_Runtime_Serialization_IFormatterConverter_ToDateTime(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Runtime_Serialization_IFormatterConverter_ToString(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_IFormatterConverter_Destroy(
	System_Runtime_Serialization_IFormatterConverter_t /* System.Runtime.Serialization.IFormatterConverter */ self
);

#pragma mark - END APIs of System.Runtime.Serialization.IFormatterConverter

#pragma mark - BEGIN APIs of System.Runtime.Serialization.StreamingContext
CBool /* System.Boolean */
System_Runtime_Serialization_StreamingContext_Equals(
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Runtime_Serialization_StreamingContext_GetHashCode(
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */
System_Runtime_Serialization_StreamingContext_Create(
	System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */ state,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */
System_Runtime_Serialization_StreamingContext_Create1(
	System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */ state,
	System_Object_t /* System.Object */ additional,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_Serialization_StreamingContextStates /* System.Runtime.Serialization.StreamingContextStates */
System_Runtime_Serialization_StreamingContext_State_Get(
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Runtime_Serialization_StreamingContext_Context_Get(
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_Serialization_StreamingContext_Destroy(
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ self
);

#pragma mark - END APIs of System.Runtime.Serialization.StreamingContext

#pragma mark - BEGIN APIs of System.Runtime.Serialization.StreamingContextStates
#pragma mark - END APIs of System.Runtime.Serialization.StreamingContextStates


#pragma mark - BEGIN APIs of System.RuntimeTypeHandle
System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */
System_RuntimeTypeHandle_FromIntPtr(
	int /* System.IntPtr */ value,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeTypeHandle_ToIntPtr(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_RuntimeTypeHandle_GetHashCode(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeTypeHandle_Equals(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeTypeHandle_Equals1(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_ModuleHandle_t /* System.ModuleHandle */
System_RuntimeTypeHandle_GetModuleHandle(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_RuntimeTypeHandle_GetObjectData(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeTypeHandle_Value_Get(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_RuntimeTypeHandle_Destroy(
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ self
);

#pragma mark - END APIs of System.RuntimeTypeHandle

#pragma mark - BEGIN APIs of System.ModuleHandle
int32_t /* System.Int32 */
System_ModuleHandle_GetHashCode(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_ModuleHandle_Equals(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_ModuleHandle_Equals1(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	System_ModuleHandle_t /* System.ModuleHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */
System_ModuleHandle_GetRuntimeTypeHandleFromMetadataToken(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ typeToken,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */
System_ModuleHandle_ResolveTypeHandle(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ typeToken,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */
System_ModuleHandle_GetRuntimeMethodHandleFromMetadataToken(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ methodToken,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeMethodHandle_t /* System.RuntimeMethodHandle */
System_ModuleHandle_ResolveMethodHandle(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ methodToken,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */
System_ModuleHandle_GetRuntimeFieldHandleFromMetadataToken(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ fieldToken,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */
System_ModuleHandle_ResolveFieldHandle(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	int32_t /* System.Int32 */ fieldToken,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_ModuleHandle_MDStreamVersion_Get(
	System_ModuleHandle_t /* System.ModuleHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_ModuleHandle_t /* System.ModuleHandle */
System_ModuleHandle_EmptyHandle_Get(
	
);

void /* System.Void */
System_ModuleHandle_Destroy(
	System_ModuleHandle_t /* System.ModuleHandle */ self
);

#pragma mark - END APIs of System.ModuleHandle

#pragma mark - BEGIN APIs of System.RuntimeFieldHandle
int32_t /* System.Int32 */
System_RuntimeFieldHandle_GetHashCode(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeFieldHandle_Equals(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_RuntimeFieldHandle_Equals1(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self,
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */
System_RuntimeFieldHandle_FromIntPtr(
	int /* System.IntPtr */ value,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeFieldHandle_ToIntPtr(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_RuntimeFieldHandle_GetObjectData(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

int /* System.IntPtr */
System_RuntimeFieldHandle_Value_Get(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_RuntimeFieldHandle_Destroy(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ self
);

#pragma mark - END APIs of System.RuntimeFieldHandle

#pragma mark - BEGIN APIs of System.Reflection.MethodAttributes
#pragma mark - END APIs of System.Reflection.MethodAttributes

#pragma mark - BEGIN APIs of System.Reflection.MethodImplAttributes
#pragma mark - END APIs of System.Reflection.MethodImplAttributes

#pragma mark - BEGIN APIs of System.Reflection.MethodBody
int32_t /* System.Int32 */
System_Reflection_MethodBody_LocalSignatureMetadataToken_Get(
	System_Reflection_MethodBody_t /* System.Reflection.MethodBody */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_MethodBody_MaxStackSize_Get(
	System_Reflection_MethodBody_t /* System.Reflection.MethodBody */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_MethodBody_InitLocals_Get(
	System_Reflection_MethodBody_t /* System.Reflection.MethodBody */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_MethodBody_Destroy(
	System_Reflection_MethodBody_t /* System.Reflection.MethodBody */ self
);

#pragma mark - END APIs of System.Reflection.MethodBody

#pragma mark - BEGIN APIs of System.Reflection.CallingConventions
#pragma mark - END APIs of System.Reflection.CallingConventions

#pragma mark - BEGIN APIs of System.Reflection.BindingFlags
#pragma mark - END APIs of System.Reflection.BindingFlags

#pragma mark - BEGIN APIs of System.Reflection.Binder
System_Object_t /* System.Object */
System_Reflection_Binder_ChangeType(
	System_Reflection_Binder_t /* System.Reflection.Binder */ self,
	System_Object_t /* System.Object */ value,
	System_Type_t /* System.Type */ type,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_Binder_Destroy(
	System_Reflection_Binder_t /* System.Reflection.Binder */ self
);

#pragma mark - END APIs of System.Reflection.Binder

#pragma mark - BEGIN APIs of System.Reflection.FieldInfo
System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Reflection_FieldInfo_GetFieldFromHandle(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ handle,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Reflection_FieldInfo_GetFieldFromHandle1(
	System_RuntimeFieldHandle_t /* System.RuntimeFieldHandle */ handle,
	System_RuntimeTypeHandle_t /* System.RuntimeTypeHandle */ declaringType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_FieldInfo_Equals(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_FieldInfo_GetHashCode(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_FieldInfo_GetValue(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_FieldInfo_SetValue(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_FieldInfo_SetValue1(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Object_t /* System.Object */ value,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ invokeAttr,
	System_Reflection_Binder_t /* System.Reflection.Binder */ binder,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_FieldInfo_GetRawConstantValue(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_FieldInfo_Destroy(
	System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */ self
);

#pragma mark - END APIs of System.Reflection.FieldInfo

#pragma mark - BEGIN APIs of System.Reflection.FieldAttributes
#pragma mark - END APIs of System.Reflection.FieldAttributes

#pragma mark - BEGIN APIs of System.Reflection.PropertyInfo
System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_PropertyInfo_GetGetMethod(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_PropertyInfo_GetGetMethod1(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	CBool /* System.Boolean */ nonPublic,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_PropertyInfo_GetSetMethod(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_PropertyInfo_GetSetMethod1(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	CBool /* System.Boolean */ nonPublic,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_PropertyInfo_GetValue(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_PropertyInfo_GetConstantValue(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_PropertyInfo_GetRawConstantValue(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_PropertyInfo_SetValue(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_PropertyInfo_Equals(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_PropertyInfo_GetHashCode(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_PropertyInfo_Destroy(
	System_Reflection_PropertyInfo_t /* System.Reflection.PropertyInfo */ self
);

#pragma mark - END APIs of System.Reflection.PropertyInfo

#pragma mark - BEGIN APIs of System.Reflection.PropertyAttributes
#pragma mark - END APIs of System.Reflection.PropertyAttributes

#pragma mark - BEGIN APIs of System.Reflection.MethodInfo
System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_MethodInfo_GetGenericMethodDefinition(
	System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_MethodInfo_GetBaseDefinition(
	System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_MethodInfo_Equals(
	System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_MethodInfo_GetHashCode(
	System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_MethodInfo_Destroy(
	System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */ self
);

#pragma mark - END APIs of System.Reflection.MethodInfo

#pragma mark - BEGIN APIs of System.Reflection.ParameterInfo
CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsDefined(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_ParameterInfo_GetRealObject(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_ParameterInfo_ToString(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_ParameterAttributes /* System.Reflection.ParameterAttributes */
System_Reflection_ParameterInfo_Attributes_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */
System_Reflection_ParameterInfo_Member_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_ParameterInfo_Name_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_ParameterInfo_ParameterType_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_ParameterInfo_Position_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsIn_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsLcid_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsOptional_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsOut_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_IsRetval_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_ParameterInfo_DefaultValue_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Reflection_ParameterInfo_RawDefaultValue_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_ParameterInfo_HasDefaultValue_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_ParameterInfo_MetadataToken_Get(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_ParameterInfo_Destroy(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ self
);

#pragma mark - END APIs of System.Reflection.ParameterInfo

#pragma mark - BEGIN APIs of System.Reflection.ParameterAttributes
#pragma mark - END APIs of System.Reflection.ParameterAttributes

#pragma mark - BEGIN APIs of System.Reflection.ICustomAttributeProvider
CBool /* System.Boolean */
System_Reflection_ICustomAttributeProvider_IsDefined(
	System_Reflection_ICustomAttributeProvider_t /* System.Reflection.ICustomAttributeProvider */ self,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_ICustomAttributeProvider_Destroy(
	System_Reflection_ICustomAttributeProvider_t /* System.Reflection.ICustomAttributeProvider */ self
);

#pragma mark - END APIs of System.Reflection.ICustomAttributeProvider

#pragma mark - BEGIN APIs of System.Collections.IDictionary
CBool /* System.Boolean */
System_Collections_IDictionary_Contains(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self,
	System_Object_t /* System.Object */ key,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IDictionary_Add(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self,
	System_Object_t /* System.Object */ key,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IDictionary_Clear(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Collections_IDictionaryEnumerator_t /* System.Collections.IDictionaryEnumerator */
System_Collections_IDictionary_GetEnumerator(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IDictionary_Remove(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self,
	System_Object_t /* System.Object */ key,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IDictionary_Destroy(
	System_Collections_IDictionary_t /* System.Collections.IDictionary */ self
);

#pragma mark - END APIs of System.Collections.IDictionary

#pragma mark - BEGIN APIs of System.Collections.ICollection
void /* System.Void */
System_Collections_ICollection_CopyTo(
	System_Collections_ICollection_t /* System.Collections.ICollection */ self,
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_ICollection_Destroy(
	System_Collections_ICollection_t /* System.Collections.ICollection */ self
);

#pragma mark - END APIs of System.Collections.ICollection

#pragma mark - BEGIN APIs of System.Array
void /* System.Void */
System_Array_Copy(
	System_Array_t /* System.Array */ sourceArray,
	System_Array_t /* System.Array */ destinationArray,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Copy1(
	System_Array_t /* System.Array */ sourceArray,
	int32_t /* System.Int32 */ sourceIndex,
	System_Array_t /* System.Array */ destinationArray,
	int32_t /* System.Int32 */ destinationIndex,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_ConstrainedCopy(
	System_Array_t /* System.Array */ sourceArray,
	int32_t /* System.Int32 */ sourceIndex,
	System_Array_t /* System.Array */ destinationArray,
	int32_t /* System.Int32 */ destinationIndex,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Clear(
	System_Array_t /* System.Array */ array,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Clear1(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_GetLength(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ dimension,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_GetUpperBound(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ dimension,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_GetLowerBound(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ dimension,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Initialize(
	System_Array_t /* System.Array */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Array_CreateInstance(
	System_Type_t /* System.Type */ elementType,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Array_CreateInstance1(
	System_Type_t /* System.Type */ elementType,
	int32_t /* System.Int32 */ length1,
	int32_t /* System.Int32 */ length2,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
System_Array_CreateInstance2(
	System_Type_t /* System.Type */ elementType,
	int32_t /* System.Int32 */ length1,
	int32_t /* System.Int32 */ length2,
	int32_t /* System.Int32 */ length3,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Copy2(
	System_Array_t /* System.Array */ sourceArray,
	System_Array_t /* System.Array */ destinationArray,
	int64_t /* System.Int64 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Copy3(
	System_Array_t /* System.Array */ sourceArray,
	int64_t /* System.Int64 */ sourceIndex,
	System_Array_t /* System.Array */ destinationArray,
	int64_t /* System.Int64 */ destinationIndex,
	int64_t /* System.Int64 */ length,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue1(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ index1,
	int32_t /* System.Int32 */ index2,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue2(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ index1,
	int32_t /* System.Int32 */ index2,
	int32_t /* System.Int32 */ index3,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue1(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ index1,
	int32_t /* System.Int32 */ index2,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue2(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ index1,
	int32_t /* System.Int32 */ index2,
	int32_t /* System.Int32 */ index3,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue3(
	System_Array_t /* System.Array */ self,
	int64_t /* System.Int64 */ index,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue4(
	System_Array_t /* System.Array */ self,
	int64_t /* System.Int64 */ index1,
	int64_t /* System.Int64 */ index2,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_GetValue5(
	System_Array_t /* System.Array */ self,
	int64_t /* System.Int64 */ index1,
	int64_t /* System.Int64 */ index2,
	int64_t /* System.Int64 */ index3,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue3(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int64_t /* System.Int64 */ index,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue4(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int64_t /* System.Int64 */ index1,
	int64_t /* System.Int64 */ index2,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_SetValue5(
	System_Array_t /* System.Array */ self,
	System_Object_t /* System.Object */ value,
	int64_t /* System.Int64 */ index1,
	int64_t /* System.Int64 */ index2,
	int64_t /* System.Int64 */ index3,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_Array_GetLongLength(
	System_Array_t /* System.Array */ self,
	int32_t /* System.Int32 */ dimension,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Array_Clone(
	System_Array_t /* System.Array */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_BinarySearch(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_BinarySearch1(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_BinarySearch2(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_BinarySearch3(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Object_t /* System.Object */ value,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_CopyTo(
	System_Array_t /* System.Array */ self,
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_CopyTo1(
	System_Array_t /* System.Array */ self,
	System_Array_t /* System.Array */ array,
	int64_t /* System.Int64 */ index,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_IndexOf(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_IndexOf1(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_IndexOf2(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_LastIndexOf(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_LastIndexOf1(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ startIndex,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Array_LastIndexOf2(
	System_Array_t /* System.Array */ array,
	System_Object_t /* System.Object */ value,
	int32_t /* System.Int32 */ startIndex,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Reverse(
	System_Array_t /* System.Array */ array,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Reverse1(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort(
	System_Array_t /* System.Array */ array,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort1(
	System_Array_t /* System.Array */ keys,
	System_Array_t /* System.Array */ items,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort2(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort3(
	System_Array_t /* System.Array */ keys,
	System_Array_t /* System.Array */ items,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort4(
	System_Array_t /* System.Array */ array,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort5(
	System_Array_t /* System.Array */ keys,
	System_Array_t /* System.Array */ items,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort6(
	System_Array_t /* System.Array */ array,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Sort7(
	System_Array_t /* System.Array */ keys,
	System_Array_t /* System.Array */ items,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ length,
	System_Collections_IComparer_t /* System.Collections.IComparer */ comparer,
	System_Exception_t* /* System.Exception */ outException
);

System_Collections_IEnumerator_t /* System.Collections.IEnumerator */
System_Array_GetEnumerator(
	System_Array_t /* System.Array */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Array_Destroy(
	System_Array_t /* System.Array */ self
);

#pragma mark - END APIs of System.Array

#pragma mark - BEGIN APIs of System.Collections.IComparer
int32_t /* System.Int32 */
System_Collections_IComparer_Compare(
	System_Collections_IComparer_t /* System.Collections.IComparer */ self,
	System_Object_t /* System.Object */ x,
	System_Object_t /* System.Object */ y,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IComparer_Destroy(
	System_Collections_IComparer_t /* System.Collections.IComparer */ self
);

#pragma mark - END APIs of System.Collections.IComparer

#pragma mark - BEGIN APIs of System.Collections.IEnumerator
CBool /* System.Boolean */
System_Collections_IEnumerator_MoveNext(
	System_Collections_IEnumerator_t /* System.Collections.IEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IEnumerator_Reset(
	System_Collections_IEnumerator_t /* System.Collections.IEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_IEnumerator_Destroy(
	System_Collections_IEnumerator_t /* System.Collections.IEnumerator */ self
);

#pragma mark - END APIs of System.Collections.IEnumerator

#pragma mark - BEGIN APIs of System.Collections.IDictionaryEnumerator
void /* System.Void */
System_Collections_IDictionaryEnumerator_Destroy(
	System_Collections_IDictionaryEnumerator_t /* System.Collections.IDictionaryEnumerator */ self
);

#pragma mark - END APIs of System.Collections.IDictionaryEnumerator

#pragma mark - BEGIN APIs of System.Collections.DictionaryEntry
CString /* System.String */
System_Collections_DictionaryEntry_ToString(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */
System_Collections_DictionaryEntry_Create(
	System_Object_t /* System.Object */ key,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Collections_DictionaryEntry_Key_Get(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_DictionaryEntry_Key_Set(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Collections_DictionaryEntry_Value_Get(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_DictionaryEntry_Value_Set(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Collections_DictionaryEntry_Destroy(
	System_Collections_DictionaryEntry_t /* System.Collections.DictionaryEntry */ self
);

#pragma mark - END APIs of System.Collections.DictionaryEntry

#pragma mark - BEGIN APIs of System.Threading.Tasks.TaskStatus
#pragma mark - END APIs of System.Threading.Tasks.TaskStatus

#pragma mark - BEGIN APIs of System.Threading.Tasks.TaskCreationOptions
#pragma mark - END APIs of System.Threading.Tasks.TaskCreationOptions

#pragma mark - BEGIN APIs of System.Threading.Tasks.TaskFactory
System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_TaskFactory_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_TaskFactory_Create1(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_TaskFactory_Create2(
	System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */ scheduler,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_TaskFactory_Create3(
	System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions,
	System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ continuationOptions,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */
System_Threading_Tasks_TaskFactory_Create4(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */ creationOptions,
	System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */ continuationOptions,
	System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */ scheduler,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_CancellationToken_t /* System.Threading.CancellationToken */
System_Threading_Tasks_TaskFactory_CancellationToken_Get(
	System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskScheduler_t /* System.Threading.Tasks.TaskScheduler */
System_Threading_Tasks_TaskFactory_Scheduler_Get(
	System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskCreationOptions /* System.Threading.Tasks.TaskCreationOptions */
System_Threading_Tasks_TaskFactory_CreationOptions_Get(
	System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_TaskContinuationOptions /* System.Threading.Tasks.TaskContinuationOptions */
System_Threading_Tasks_TaskFactory_ContinuationOptions_Get(
	System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_TaskFactory_Destroy(
	System_Threading_Tasks_TaskFactory_t /* System.Threading.Tasks.TaskFactory */ self
);

#pragma mark - END APIs of System.Threading.Tasks.TaskFactory

#pragma mark - BEGIN APIs of System.Threading.CancellationToken
CBool /* System.Boolean */
System_Threading_CancellationToken_Equals(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ other,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationToken_Equals1(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Object_t /* System.Object */ other,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Threading_CancellationToken_GetHashCode(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_CancellationToken_ThrowIfCancellationRequested(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_CancellationToken_t /* System.Threading.CancellationToken */
System_Threading_CancellationToken_Create(
	CBool /* System.Boolean */ canceled,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_CancellationToken_t /* System.Threading.CancellationToken */
System_Threading_CancellationToken_None_Get(
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationToken_IsCancellationRequested_Get(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationToken_CanBeCanceled_Get(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_WaitHandle_t /* System.Threading.WaitHandle */
System_Threading_CancellationToken_WaitHandle_Get(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_CancellationToken_Destroy(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ self
);

#pragma mark - END APIs of System.Threading.CancellationToken

#pragma mark - BEGIN APIs of System.Threading.WaitHandle
void /* System.Void */
System_Threading_WaitHandle_Close(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_WaitHandle_Dispose(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_WaitOne(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	int32_t /* System.Int32 */ millisecondsTimeout,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_WaitOne1(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_WaitOne2(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_WaitOne3(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	int32_t /* System.Int32 */ millisecondsTimeout,
	CBool /* System.Boolean */ exitContext,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_WaitOne4(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	CBool /* System.Boolean */ exitContext,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_SignalAndWait(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toSignal,
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toWaitOn,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_SignalAndWait1(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toSignal,
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toWaitOn,
	System_TimeSpan_t /* System.TimeSpan */ timeout,
	CBool /* System.Boolean */ exitContext,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_WaitHandle_SignalAndWait2(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toSignal,
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ toWaitOn,
	int32_t /* System.Int32 */ millisecondsTimeout,
	CBool /* System.Boolean */ exitContext,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Threading_WaitHandle_WaitTimeout_Get(
	
);

void /* System.Void */
System_Threading_WaitHandle_Destroy(
	System_Threading_WaitHandle_t /* System.Threading.WaitHandle */ self
);

#pragma mark - END APIs of System.Threading.WaitHandle

#pragma mark - BEGIN APIs of Microsoft.Win32.SafeHandles.SafeWaitHandle
Microsoft_Win32_SafeHandles_SafeWaitHandle_t /* Microsoft.Win32.SafeHandles.SafeWaitHandle */
Microsoft_Win32_SafeHandles_SafeWaitHandle_Create(
	System_Exception_t* /* System.Exception */ outException
);

Microsoft_Win32_SafeHandles_SafeWaitHandle_t /* Microsoft.Win32.SafeHandles.SafeWaitHandle */
Microsoft_Win32_SafeHandles_SafeWaitHandle_Create1(
	int /* System.IntPtr */ existingHandle,
	CBool /* System.Boolean */ ownsHandle,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
Microsoft_Win32_SafeHandles_SafeWaitHandle_Destroy(
	Microsoft_Win32_SafeHandles_SafeWaitHandle_t /* Microsoft.Win32.SafeHandles.SafeWaitHandle */ self
);

#pragma mark - END APIs of Microsoft.Win32.SafeHandles.SafeWaitHandle

#pragma mark - BEGIN APIs of Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
void /* System.Void */
Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_Destroy(
	Microsoft_Win32_SafeHandles_SafeHandleZeroOrMinusOneIsInvalid_t /* Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid */ self
);

#pragma mark - END APIs of Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid

#pragma mark - BEGIN APIs of System.Runtime.InteropServices.SafeHandle
int /* System.IntPtr */
System_Runtime_InteropServices_SafeHandle_DangerousGetHandle(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_InteropServices_SafeHandle_Close(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_InteropServices_SafeHandle_Dispose(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_InteropServices_SafeHandle_SetHandleAsInvalid(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_InteropServices_SafeHandle_DangerousRelease(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_InteropServices_SafeHandle_Destroy(
	System_Runtime_InteropServices_SafeHandle_t /* System.Runtime.InteropServices.SafeHandle */ self
);

#pragma mark - END APIs of System.Runtime.InteropServices.SafeHandle

#pragma mark - BEGIN APIs of System.Runtime.ConstrainedExecution.CriticalFinalizerObject
void /* System.Void */
System_Runtime_ConstrainedExecution_CriticalFinalizerObject_Destroy(
	System_Runtime_ConstrainedExecution_CriticalFinalizerObject_t /* System.Runtime.ConstrainedExecution.CriticalFinalizerObject */ self
);

#pragma mark - END APIs of System.Runtime.ConstrainedExecution.CriticalFinalizerObject

#pragma mark - BEGIN APIs of System.Threading.CancellationTokenRegistration
void /* System.Void */
System_Threading_CancellationTokenRegistration_Dispose(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_CancellationTokenRegistration_DisposeAsync(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationTokenRegistration_Unregister(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationTokenRegistration_Equals(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_CancellationTokenRegistration_Equals1(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ other,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Threading_CancellationTokenRegistration_GetHashCode(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_CancellationToken_t /* System.Threading.CancellationToken */
System_Threading_CancellationTokenRegistration_Token_Get(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_CancellationTokenRegistration_Destroy(
	System_Threading_CancellationTokenRegistration_t /* System.Threading.CancellationTokenRegistration */ self
);

#pragma mark - END APIs of System.Threading.CancellationTokenRegistration

#pragma mark - BEGIN APIs of System.Threading.Tasks.ValueTask
System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_FromCanceled(
	System_Threading_CancellationToken_t /* System.Threading.CancellationToken */ cancellationToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_FromException(
	System_Exception_t /* System.Exception */ exception,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Threading_Tasks_ValueTask_GetHashCode(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_Equals(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_Equals1(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ other,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */
System_Threading_Tasks_ValueTask_AsTask(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_Preserve(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_CompilerServices_ValueTaskAwaiter_t /* System.Runtime.CompilerServices.ValueTaskAwaiter */
System_Threading_Tasks_ValueTask_GetAwaiter(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable */
System_Threading_Tasks_ValueTask_ConfigureAwait(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	CBool /* System.Boolean */ continueOnCapturedContext,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_Create(
	System_Threading_Tasks_Task_t /* System.Threading.Tasks.Task */ task,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_Create1(
	System_Threading_Tasks_Sources_IValueTaskSource_t /* System.Threading.Tasks.Sources.IValueTaskSource */ source,
	int16_t /* System.Int16 */ token,
	System_Exception_t* /* System.Exception */ outException
);

System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */
System_Threading_Tasks_ValueTask_CompletedTask_Get(
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_IsCompleted_Get(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_IsCompletedSuccessfully_Get(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_IsFaulted_Get(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Threading_Tasks_ValueTask_IsCanceled_Get(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_ValueTask_Destroy(
	System_Threading_Tasks_ValueTask_t /* System.Threading.Tasks.ValueTask */ self
);

#pragma mark - END APIs of System.Threading.Tasks.ValueTask

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.ValueTaskAwaiter
void /* System.Void */
System_Runtime_CompilerServices_ValueTaskAwaiter_GetResult(
	System_Runtime_CompilerServices_ValueTaskAwaiter_t /* System.Runtime.CompilerServices.ValueTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_CompilerServices_ValueTaskAwaiter_IsCompleted_Get(
	System_Runtime_CompilerServices_ValueTaskAwaiter_t /* System.Runtime.CompilerServices.ValueTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_ValueTaskAwaiter_Destroy(
	System_Runtime_CompilerServices_ValueTaskAwaiter_t /* System.Runtime.CompilerServices.ValueTaskAwaiter */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.ValueTaskAwaiter

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_GetAwaiter(
	System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_Destroy(
	System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter
void /* System.Void */
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_GetResult(
	System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_IsCompleted_Get(
	System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_Destroy(
	System_Runtime_CompilerServices_ConfiguredValueTaskAwaitable_ConfiguredValueTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter

#pragma mark - BEGIN APIs of System.Threading.Tasks.Sources.IValueTaskSource
System_Threading_Tasks_Sources_ValueTaskSourceStatus /* System.Threading.Tasks.Sources.ValueTaskSourceStatus */
System_Threading_Tasks_Sources_IValueTaskSource_GetStatus(
	System_Threading_Tasks_Sources_IValueTaskSource_t /* System.Threading.Tasks.Sources.IValueTaskSource */ self,
	int16_t /* System.Int16 */ token,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Sources_IValueTaskSource_GetResult(
	System_Threading_Tasks_Sources_IValueTaskSource_t /* System.Threading.Tasks.Sources.IValueTaskSource */ self,
	int16_t /* System.Int16 */ token,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Threading_Tasks_Sources_IValueTaskSource_Destroy(
	System_Threading_Tasks_Sources_IValueTaskSource_t /* System.Threading.Tasks.Sources.IValueTaskSource */ self
);

#pragma mark - END APIs of System.Threading.Tasks.Sources.IValueTaskSource

#pragma mark - BEGIN APIs of System.Threading.Tasks.Sources.ValueTaskSourceStatus
#pragma mark - END APIs of System.Threading.Tasks.Sources.ValueTaskSourceStatus

#pragma mark - BEGIN APIs of System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags
#pragma mark - END APIs of System.Threading.Tasks.Sources.ValueTaskSourceOnCompletedFlags

#pragma mark - BEGIN APIs of System.Threading.Tasks.TaskContinuationOptions
#pragma mark - END APIs of System.Threading.Tasks.TaskContinuationOptions

#pragma mark - BEGIN APIs of System.IAsyncResult
void /* System.Void */
System_IAsyncResult_Destroy(
	System_IAsyncResult_t /* System.IAsyncResult */ self
);

#pragma mark - END APIs of System.IAsyncResult

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.TaskAwaiter
void /* System.Void */
System_Runtime_CompilerServices_TaskAwaiter_GetResult(
	System_Runtime_CompilerServices_TaskAwaiter_t /* System.Runtime.CompilerServices.TaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_CompilerServices_TaskAwaiter_IsCompleted_Get(
	System_Runtime_CompilerServices_TaskAwaiter_t /* System.Runtime.CompilerServices.TaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_TaskAwaiter_Destroy(
	System_Runtime_CompilerServices_TaskAwaiter_t /* System.Runtime.CompilerServices.TaskAwaiter */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.TaskAwaiter

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.ConfiguredTaskAwaitable
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_GetAwaiter(
	System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_Destroy(
	System_Runtime_CompilerServices_ConfiguredTaskAwaitable_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.ConfiguredTaskAwaitable

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter
void /* System.Void */
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_GetResult(
	System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_IsCompleted_Get(
	System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_Destroy(
	System_Runtime_CompilerServices_ConfiguredTaskAwaitable_ConfiguredTaskAwaiter_t /* System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.YieldAwaitable
System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */
System_Runtime_CompilerServices_YieldAwaitable_GetAwaiter(
	System_Runtime_CompilerServices_YieldAwaitable_t /* System.Runtime.CompilerServices.YieldAwaitable */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_YieldAwaitable_Destroy(
	System_Runtime_CompilerServices_YieldAwaitable_t /* System.Runtime.CompilerServices.YieldAwaitable */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.YieldAwaitable

#pragma mark - BEGIN APIs of System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter
void /* System.Void */
System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_GetResult(
	System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_IsCompleted_Get(
	System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_Destroy(
	System_Runtime_CompilerServices_YieldAwaitable_YieldAwaiter_t /* System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter */ self
);

#pragma mark - END APIs of System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter

#pragma mark - BEGIN APIs of System.IO.SeekOrigin
#pragma mark - END APIs of System.IO.SeekOrigin

#pragma mark - BEGIN APIs of Microsoft.Win32.SafeHandles.SafeFileHandle
Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */
Microsoft_Win32_SafeHandles_SafeFileHandle_Create(
	int /* System.IntPtr */ preexistingHandle,
	CBool /* System.Boolean */ ownsHandle,
	System_Exception_t* /* System.Exception */ outException
);

Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */
Microsoft_Win32_SafeHandles_SafeFileHandle_Create1(
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
Microsoft_Win32_SafeHandles_SafeFileHandle_IsAsync_Get(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
Microsoft_Win32_SafeHandles_SafeFileHandle_IsInvalid_Get(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
Microsoft_Win32_SafeHandles_SafeFileHandle_Destroy(
	Microsoft_Win32_SafeHandles_SafeFileHandle_t /* Microsoft.Win32.SafeHandles.SafeFileHandle */ self
);

#pragma mark - END APIs of Microsoft.Win32.SafeHandles.SafeFileHandle

#pragma mark - BEGIN APIs of System.IO.FileAccess
#pragma mark - END APIs of System.IO.FileAccess

#pragma mark - BEGIN APIs of System.IO.FileMode
#pragma mark - END APIs of System.IO.FileMode

#pragma mark - BEGIN APIs of System.IO.FileShare
#pragma mark - END APIs of System.IO.FileShare

#pragma mark - BEGIN APIs of System.IO.FileOptions
#pragma mark - END APIs of System.IO.FileOptions

#pragma mark - BEGIN APIs of System.IO.FileStreamOptions
System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */
System_IO_FileStreamOptions_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileMode /* System.IO.FileMode */
System_IO_FileStreamOptions_Mode_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_Mode_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_IO_FileMode /* System.IO.FileMode */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileAccess /* System.IO.FileAccess */
System_IO_FileStreamOptions_Access_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_Access_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_IO_FileAccess /* System.IO.FileAccess */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileShare /* System.IO.FileShare */
System_IO_FileStreamOptions_Share_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_Share_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_IO_FileShare /* System.IO.FileShare */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_FileOptions /* System.IO.FileOptions */
System_IO_FileStreamOptions_Options_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_Options_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_IO_FileOptions /* System.IO.FileOptions */ value,
	System_Exception_t* /* System.Exception */ outException
);

int64_t /* System.Int64 */
System_IO_FileStreamOptions_PreallocationSize_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_PreallocationSize_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	int64_t /* System.Int64 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_IO_FileStreamOptions_BufferSize_Get(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_BufferSize_Set(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_IO_FileStreamOptions_Destroy(
	System_IO_FileStreamOptions_t /* System.IO.FileStreamOptions */ self
);

#pragma mark - END APIs of System.IO.FileStreamOptions

#pragma mark - BEGIN APIs of System.Reflection.ManifestResourceInfo
System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */
System_Reflection_ManifestResourceInfo_Create(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ containingAssembly,
	CString /* System.String */ containingFileName,
	System_Reflection_ResourceLocation /* System.Reflection.ResourceLocation */ resourceLocation,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_Assembly_t /* System.Reflection.Assembly */
System_Reflection_ManifestResourceInfo_ReferencedAssembly_Get(
	System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_ManifestResourceInfo_FileName_Get(
	System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_ResourceLocation /* System.Reflection.ResourceLocation */
System_Reflection_ManifestResourceInfo_ResourceLocation_Get(
	System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_ManifestResourceInfo_Destroy(
	System_Reflection_ManifestResourceInfo_t /* System.Reflection.ManifestResourceInfo */ self
);

#pragma mark - END APIs of System.Reflection.ManifestResourceInfo

#pragma mark - BEGIN APIs of System.Reflection.ResourceLocation
#pragma mark - END APIs of System.Reflection.ResourceLocation

#pragma mark - BEGIN APIs of System.Reflection.Module
CBool /* System.Boolean */
System_Reflection_Module_IsResource(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_Module_IsDefined(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_Module_GetMethod(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Reflection_Module_GetField(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Reflection_Module_GetField1(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ name,
	System_Reflection_BindingFlags /* System.Reflection.BindingFlags */ bindingAttr,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Module_GetType(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ className,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Module_GetType1(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ className,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Module_GetType2(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	CString /* System.String */ className,
	CBool /* System.Boolean */ throwOnError,
	CBool /* System.Boolean */ ignoreCase,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_FieldInfo_t /* System.Reflection.FieldInfo */
System_Reflection_Module_ResolveField(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	int32_t /* System.Int32 */ metadataToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */
System_Reflection_Module_ResolveMember(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	int32_t /* System.Int32 */ metadataToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodBase_t /* System.Reflection.MethodBase */
System_Reflection_Module_ResolveMethod(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	int32_t /* System.Int32 */ metadataToken,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_Module_ResolveString(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	int32_t /* System.Int32 */ metadataToken,
	System_Exception_t* /* System.Exception */ outException
);

System_Type_t /* System.Type */
System_Reflection_Module_ResolveType(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	int32_t /* System.Int32 */ metadataToken,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_Module_GetObjectData(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Runtime_Serialization_SerializationInfo_t /* System.Runtime.Serialization.SerializationInfo */ info,
	System_Runtime_Serialization_StreamingContext_t /* System.Runtime.Serialization.StreamingContext */ context,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_Module_Equals(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_Module_GetHashCode(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_Module_ToString(
	System_Reflection_Module_t /* System.Reflection.Module */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_Module_Destroy(
	System_Reflection_Module_t /* System.Reflection.Module */ self
);

#pragma mark - END APIs of System.Reflection.Module

#pragma mark - BEGIN APIs of System.Guid
System_Guid_t /* System.Guid */
System_Guid_Parse(
	CString /* System.String */ input,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_ParseExact(
	CString /* System.String */ input,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Guid_ToString(
	System_Guid_t /* System.Guid */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Guid_GetHashCode(
	System_Guid_t /* System.Guid */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Guid_Equals(
	System_Guid_t /* System.Guid */ self,
	System_Object_t /* System.Object */ o,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Guid_Equals1(
	System_Guid_t /* System.Guid */ self,
	System_Guid_t /* System.Guid */ g,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Guid_CompareTo(
	System_Guid_t /* System.Guid */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Guid_CompareTo1(
	System_Guid_t /* System.Guid */ self,
	System_Guid_t /* System.Guid */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Guid_ToString1(
	System_Guid_t /* System.Guid */ self,
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Guid_ToString2(
	System_Guid_t /* System.Guid */ self,
	CString /* System.String */ format,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_Parse1(
	CString /* System.String */ s,
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_NewGuid(
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_Create(
	uint32_t /* System.UInt32 */ a,
	uint16_t /* System.UInt16 */ b,
	uint16_t /* System.UInt16 */ c,
	uint8_t /* System.Byte */ d,
	uint8_t /* System.Byte */ e,
	uint8_t /* System.Byte */ f,
	uint8_t /* System.Byte */ g,
	uint8_t /* System.Byte */ h,
	uint8_t /* System.Byte */ i,
	uint8_t /* System.Byte */ j,
	uint8_t /* System.Byte */ k,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_Create1(
	int32_t /* System.Int32 */ a,
	int16_t /* System.Int16 */ b,
	int16_t /* System.Int16 */ c,
	uint8_t /* System.Byte */ d,
	uint8_t /* System.Byte */ e,
	uint8_t /* System.Byte */ f,
	uint8_t /* System.Byte */ g,
	uint8_t /* System.Byte */ h,
	uint8_t /* System.Byte */ i,
	uint8_t /* System.Byte */ j,
	uint8_t /* System.Byte */ k,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_Create2(
	CString /* System.String */ g,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Guid_Empty_Get(
	
);

void /* System.Void */
System_Guid_Destroy(
	System_Guid_t /* System.Guid */ self
);

#pragma mark - END APIs of System.Guid

#pragma mark - BEGIN APIs of System.Security.SecurityRuleSet
#pragma mark - END APIs of System.Security.SecurityRuleSet

#pragma mark - BEGIN APIs of System.Text.Rune
int32_t /* System.Int32 */
System_Text_Rune_CompareTo(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Text_Rune_t /* System.Text.Rune */ other,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_Equals(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_Equals1(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Text_Rune_t /* System.Text.Rune */ other,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Rune_GetHashCode(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_GetRuneAt(
	CString /* System.String */ input,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsValid(
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsValid1(
	uint32_t /* System.UInt32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Text_Rune_ToString(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

double /* System.Double */
System_Text_Rune_GetNumericValue(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_UnicodeCategory /* System.Globalization.UnicodeCategory */
System_Text_Rune_GetUnicodeCategory(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsControl(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsDigit(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsLetter(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsLetterOrDigit(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsLower(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsNumber(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsPunctuation(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsSeparator(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsSymbol(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsUpper(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsWhiteSpace(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_ToLower(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_ToLowerInvariant(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_ToUpper(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Globalization_CultureInfo_t /* System.Globalization.CultureInfo */ culture,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_ToUpperInvariant(
	System_Text_Rune_t /* System.Text.Rune */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_Create(
	uint8_t /* System.Char */ ch,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_Create1(
	uint8_t /* System.Char */ highSurrogate,
	uint8_t /* System.Char */ lowSurrogate,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_Create2(
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_Create3(
	uint32_t /* System.UInt32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsAscii_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Rune_IsBmp_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Rune_Plane_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_Rune_ReplacementChar_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Rune_Utf16SequenceLength_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Rune_Utf8SequenceLength_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Rune_Value_Get(
	System_Text_Rune_t /* System.Text.Rune */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_Rune_Destroy(
	System_Text_Rune_t /* System.Text.Rune */ self
);

#pragma mark - END APIs of System.Text.Rune

#pragma mark - BEGIN APIs of System.Buffers.OperationStatus
#pragma mark - END APIs of System.Buffers.OperationStatus

#pragma mark - BEGIN APIs of System.Globalization.CompareOptions
#pragma mark - END APIs of System.Globalization.CompareOptions

#pragma mark - BEGIN APIs of System.Globalization.SortKey
int32_t /* System.Int32 */
System_Globalization_SortKey_Compare(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ sortkey1,
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ sortkey2,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_SortKey_Equals(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_SortKey_GetHashCode(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_SortKey_ToString(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_SortKey_OriginalString_Get(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_SortKey_Destroy(
	System_Globalization_SortKey_t /* System.Globalization.SortKey */ self
);

#pragma mark - END APIs of System.Globalization.SortKey

#pragma mark - BEGIN APIs of System.Globalization.SortVersion
CBool /* System.Boolean */
System_Globalization_SortVersion_Equals(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_SortVersion_Equals1(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self,
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ other,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_SortVersion_GetHashCode(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_SortVersion_t /* System.Globalization.SortVersion */
System_Globalization_SortVersion_Create(
	int32_t /* System.Int32 */ fullVersion,
	System_Guid_t /* System.Guid */ sortId,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_SortVersion_FullVersion_Get(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Guid_t /* System.Guid */
System_Globalization_SortVersion_SortId_Get(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_SortVersion_Destroy(
	System_Globalization_SortVersion_t /* System.Globalization.SortVersion */ self
);

#pragma mark - END APIs of System.Globalization.SortVersion

#pragma mark - BEGIN APIs of System.Globalization.TextInfo
System_Object_t /* System.Object */
System_Globalization_TextInfo_Clone(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_TextInfo_t /* System.Globalization.TextInfo */
System_Globalization_TextInfo_ReadOnly(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ textInfo,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Globalization_TextInfo_ToLower(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	uint8_t /* System.Char */ c,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_ToLower1(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Globalization_TextInfo_ToUpper(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	uint8_t /* System.Char */ c,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_ToUpper1(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_TextInfo_Equals(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_GetHashCode(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_ToString(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_ToTitleCase(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	CString /* System.String */ str,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_ANSICodePage_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_OEMCodePage_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_MacCodePage_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_EBCDICCodePage_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_TextInfo_LCID_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_CultureName_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_TextInfo_IsReadOnly_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_TextInfo_ListSeparator_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_TextInfo_ListSeparator_Set(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_TextInfo_IsRightToLeft_Get(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_TextInfo_Destroy(
	System_Globalization_TextInfo_t /* System.Globalization.TextInfo */ self
);

#pragma mark - END APIs of System.Globalization.TextInfo

#pragma mark - BEGIN APIs of System.Globalization.NumberFormatInfo
System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_NumberFormatInfo_GetInstance(
	System_IFormatProvider_t /* System.IFormatProvider */ formatProvider,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_NumberFormatInfo_Clone(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_NumberFormatInfo_GetFormat(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Type_t /* System.Type */ formatType,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_NumberFormatInfo_ReadOnly(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ nfi,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_NumberFormatInfo_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_NumberFormatInfo_InvariantInfo_Get(
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_CurrencyDecimalDigits_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencyDecimalDigits_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_CurrencyDecimalSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencyDecimalSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_NumberFormatInfo_IsReadOnly_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_CurrencyGroupSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencyGroupSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_CurrencySymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencySymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */
System_Globalization_NumberFormatInfo_CurrentInfo_Get(
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_NaNSymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NaNSymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_CurrencyNegativePattern_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencyNegativePattern_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_NumberNegativePattern_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NumberNegativePattern_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_PercentPositivePattern_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentPositivePattern_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_PercentNegativePattern_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentNegativePattern_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_NegativeInfinitySymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NegativeInfinitySymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_NegativeSign_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NegativeSign_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_NumberDecimalDigits_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NumberDecimalDigits_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_NumberDecimalSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NumberDecimalSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_NumberGroupSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_NumberGroupSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_CurrencyPositivePattern_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_CurrencyPositivePattern_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PositiveInfinitySymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PositiveInfinitySymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PositiveSign_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PositiveSign_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_NumberFormatInfo_PercentDecimalDigits_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentDecimalDigits_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PercentDecimalSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentDecimalSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PercentGroupSeparator_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentGroupSeparator_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PercentSymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PercentSymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_NumberFormatInfo_PerMilleSymbol_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_PerMilleSymbol_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DigitShapes /* System.Globalization.DigitShapes */
System_Globalization_NumberFormatInfo_DigitSubstitution_Get(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_DigitSubstitution_Set(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self,
	System_Globalization_DigitShapes /* System.Globalization.DigitShapes */ value,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_NumberFormatInfo_Destroy(
	System_Globalization_NumberFormatInfo_t /* System.Globalization.NumberFormatInfo */ self
);

#pragma mark - END APIs of System.Globalization.NumberFormatInfo

#pragma mark - BEGIN APIs of System.Globalization.DigitShapes
#pragma mark - END APIs of System.Globalization.DigitShapes

#pragma mark - BEGIN APIs of System.Globalization.DateTimeFormatInfo
System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_DateTimeFormatInfo_GetInstance(
	System_IFormatProvider_t /* System.IFormatProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_DateTimeFormatInfo_GetFormat(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Type_t /* System.Type */ formatType,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Globalization_DateTimeFormatInfo_Clone(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Globalization_DateTimeFormatInfo_GetEra(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ eraName,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetEraName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetAbbreviatedEraName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	int32_t /* System.Int32 */ era,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetAbbreviatedDayName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_DayOfWeek /* System.DayOfWeek */ dayofweek,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetShortestDayName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_DayOfWeek /* System.DayOfWeek */ dayOfWeek,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetDayName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_DayOfWeek /* System.DayOfWeek */ dayofweek,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetAbbreviatedMonthName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	int32_t /* System.Int32 */ month,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_GetMonthName(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	int32_t /* System.Int32 */ month,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_DateTimeFormatInfo_ReadOnly(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ dtfi,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_DateTimeFormatInfo_Create(
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_DateTimeFormatInfo_InvariantInfo_Get(
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */
System_Globalization_DateTimeFormatInfo_CurrentInfo_Get(
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_AMDesignator_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_AMDesignator_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_Calendar_t /* System.Globalization.Calendar */
System_Globalization_DateTimeFormatInfo_Calendar_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_Calendar_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Globalization_Calendar_t /* System.Globalization.Calendar */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_DateSeparator_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_DateSeparator_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_DayOfWeek /* System.DayOfWeek */
System_Globalization_DateTimeFormatInfo_FirstDayOfWeek_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_FirstDayOfWeek_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_DayOfWeek /* System.DayOfWeek */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */
System_Globalization_DateTimeFormatInfo_CalendarWeekRule_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_CalendarWeekRule_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Globalization_CalendarWeekRule /* System.Globalization.CalendarWeekRule */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_FullDateTimePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_FullDateTimePattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_LongDatePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_LongDatePattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_LongTimePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_LongTimePattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_MonthDayPattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_MonthDayPattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_PMDesignator_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_PMDesignator_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_RFC1123Pattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_ShortDatePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_ShortDatePattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_ShortTimePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_ShortTimePattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_SortableDateTimePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_TimeSeparator_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_TimeSeparator_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_UniversalSortableDateTimePattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_YearMonthPattern_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_YearMonthPattern_Set(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Globalization_DateTimeFormatInfo_IsReadOnly_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Globalization_DateTimeFormatInfo_NativeCalendarName_Get(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Globalization_DateTimeFormatInfo_Destroy(
	System_Globalization_DateTimeFormatInfo_t /* System.Globalization.DateTimeFormatInfo */ self
);

#pragma mark - END APIs of System.Globalization.DateTimeFormatInfo

#pragma mark - BEGIN APIs of System.CharEnumerator
System_Object_t /* System.Object */
System_CharEnumerator_Clone(
	System_CharEnumerator_t /* System.CharEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_CharEnumerator_MoveNext(
	System_CharEnumerator_t /* System.CharEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_CharEnumerator_Dispose(
	System_CharEnumerator_t /* System.CharEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_CharEnumerator_Reset(
	System_CharEnumerator_t /* System.CharEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_CharEnumerator_Current_Get(
	System_CharEnumerator_t /* System.CharEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_CharEnumerator_Destroy(
	System_CharEnumerator_t /* System.CharEnumerator */ self
);

#pragma mark - END APIs of System.CharEnumerator

#pragma mark - BEGIN APIs of System.Text.StringRuneEnumerator
System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */
System_Text_StringRuneEnumerator_GetEnumerator(
	System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_StringRuneEnumerator_MoveNext(
	System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Rune_t /* System.Text.Rune */
System_Text_StringRuneEnumerator_Current_Get(
	System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_StringRuneEnumerator_Destroy(
	System_Text_StringRuneEnumerator_t /* System.Text.StringRuneEnumerator */ self
);

#pragma mark - END APIs of System.Text.StringRuneEnumerator

#pragma mark - BEGIN APIs of System.Text.NormalizationForm
#pragma mark - END APIs of System.Text.NormalizationForm

#pragma mark - BEGIN APIs of System.Text.CompositeFormat
System_Text_CompositeFormat_t /* System.Text.CompositeFormat */
System_Text_CompositeFormat_Parse(
	CString /* System.String */ format,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Text_CompositeFormat_Format_Get(
	System_Text_CompositeFormat_t /* System.Text.CompositeFormat */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_CompositeFormat_Destroy(
	System_Text_CompositeFormat_t /* System.Text.CompositeFormat */ self
);

#pragma mark - END APIs of System.Text.CompositeFormat

#pragma mark - BEGIN APIs of System.StringSplitOptions
#pragma mark - END APIs of System.StringSplitOptions

#pragma mark - BEGIN APIs of System.Text.Encoding
void /* System.Void */
System_Text_Encoding_RegisterProvider(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ provider,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_Encoding_GetEncoding(
	int32_t /* System.Int32 */ codepage,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_Encoding_GetEncoding1(
	int32_t /* System.Int32 */ codepage,
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ encoderFallback,
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ decoderFallback,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_Encoding_GetEncoding2(
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_Encoding_GetEncoding3(
	CString /* System.String */ name,
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ encoderFallback,
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ decoderFallback,
	System_Exception_t* /* System.Exception */ outException
);

System_Object_t /* System.Object */
System_Text_Encoding_Clone(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Encoding_GetByteCount(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	CString /* System.String */ s,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Encoding_GetByteCount1(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	CString /* System.String */ s,
	int32_t /* System.Int32 */ index,
	int32_t /* System.Int32 */ count,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Encoding_IsAlwaysNormalized(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Encoding_IsAlwaysNormalized1(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Text_NormalizationForm /* System.Text.NormalizationForm */ form,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Decoder_t /* System.Text.Decoder */
System_Text_Encoding_GetDecoder(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoder_t /* System.Text.Encoder */
System_Text_Encoding_GetEncoder(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Encoding_GetMaxByteCount(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	int32_t /* System.Int32 */ charCount,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Encoding_GetMaxCharCount(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	int32_t /* System.Int32 */ byteCount,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_Encoding_Equals(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Object_t /* System.Object */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Text_Encoding_GetHashCode(
	System_Text_Encoding_t /* System.Text.Encoding */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_IO_Stream_t /* System.IO.Stream */
System_Text_Encoding_CreateTranscodingStream(
	System_IO_Stream_t /* System.IO.Stream */ innerStream,
	System_Text_Encoding_t /* System.Text.Encoding */ innerStreamEncoding,
	System_Text_Encoding_t /* System.Text.Encoding */ outerStreamEncoding,
	CBool /* System.Boolean */ leaveOpen,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_Encoding_Destroy(
	System_Text_Encoding_t /* System.Text.Encoding */ self
);

#pragma mark - END APIs of System.Text.Encoding

#pragma mark - BEGIN APIs of System.Text.EncodingProvider
System_Text_Encoding_t /* System.Text.Encoding */
System_Text_EncodingProvider_GetEncoding(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_EncodingProvider_GetEncoding1(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ self,
	int32_t /* System.Int32 */ codepage,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_EncodingProvider_GetEncoding2(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ self,
	CString /* System.String */ name,
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ encoderFallback,
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ decoderFallback,
	System_Exception_t* /* System.Exception */ outException
);

System_Text_Encoding_t /* System.Text.Encoding */
System_Text_EncodingProvider_GetEncoding3(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ self,
	int32_t /* System.Int32 */ codepage,
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ encoderFallback,
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ decoderFallback,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_EncodingProvider_Destroy(
	System_Text_EncodingProvider_t /* System.Text.EncodingProvider */ self
);

#pragma mark - END APIs of System.Text.EncodingProvider

#pragma mark - BEGIN APIs of System.Text.EncoderFallback
System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */
System_Text_EncoderFallback_CreateFallbackBuffer(
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_EncoderFallback_Destroy(
	System_Text_EncoderFallback_t /* System.Text.EncoderFallback */ self
);

#pragma mark - END APIs of System.Text.EncoderFallback

#pragma mark - BEGIN APIs of System.Text.EncoderFallbackBuffer
CBool /* System.Boolean */
System_Text_EncoderFallbackBuffer_Fallback(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self,
	uint8_t /* System.Char */ charUnknown,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_EncoderFallbackBuffer_Fallback1(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self,
	uint8_t /* System.Char */ charUnknownHigh,
	uint8_t /* System.Char */ charUnknownLow,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

uint8_t /* System.Char */
System_Text_EncoderFallbackBuffer_GetNextChar(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_EncoderFallbackBuffer_MovePrevious(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_EncoderFallbackBuffer_Reset(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_EncoderFallbackBuffer_Destroy(
	System_Text_EncoderFallbackBuffer_t /* System.Text.EncoderFallbackBuffer */ self
);

#pragma mark - END APIs of System.Text.EncoderFallbackBuffer

#pragma mark - BEGIN APIs of System.Text.DecoderFallback
System_Text_DecoderFallbackBuffer_t /* System.Text.DecoderFallbackBuffer */
System_Text_DecoderFallback_CreateFallbackBuffer(
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_DecoderFallback_Destroy(
	System_Text_DecoderFallback_t /* System.Text.DecoderFallback */ self
);

#pragma mark - END APIs of System.Text.DecoderFallback

#pragma mark - BEGIN APIs of System.Text.DecoderFallbackBuffer
uint8_t /* System.Char */
System_Text_DecoderFallbackBuffer_GetNextChar(
	System_Text_DecoderFallbackBuffer_t /* System.Text.DecoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Text_DecoderFallbackBuffer_MovePrevious(
	System_Text_DecoderFallbackBuffer_t /* System.Text.DecoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_DecoderFallbackBuffer_Reset(
	System_Text_DecoderFallbackBuffer_t /* System.Text.DecoderFallbackBuffer */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_DecoderFallbackBuffer_Destroy(
	System_Text_DecoderFallbackBuffer_t /* System.Text.DecoderFallbackBuffer */ self
);

#pragma mark - END APIs of System.Text.DecoderFallbackBuffer

#pragma mark - BEGIN APIs of System.Text.Decoder
void /* System.Void */
System_Text_Decoder_Reset(
	System_Text_Decoder_t /* System.Text.Decoder */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_Decoder_Destroy(
	System_Text_Decoder_t /* System.Text.Decoder */ self
);

#pragma mark - END APIs of System.Text.Decoder

#pragma mark - BEGIN APIs of System.Text.Encoder
void /* System.Void */
System_Text_Encoder_Reset(
	System_Text_Encoder_t /* System.Text.Encoder */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Text_Encoder_Destroy(
	System_Text_Encoder_t /* System.Text.Encoder */ self
);

#pragma mark - END APIs of System.Text.Encoder

#pragma mark - BEGIN APIs of System.Reflection.GenericParameterAttributes
#pragma mark - END APIs of System.Reflection.GenericParameterAttributes

#pragma mark - BEGIN APIs of System.Reflection.TypeAttributes
#pragma mark - END APIs of System.Reflection.TypeAttributes

#pragma mark - BEGIN APIs of System.Runtime.InteropServices.StructLayoutAttribute
System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */
System_Runtime_InteropServices_StructLayoutAttribute_Create(
	System_Runtime_InteropServices_LayoutKind /* System.Runtime.InteropServices.LayoutKind */ layoutKind,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */
System_Runtime_InteropServices_StructLayoutAttribute_Create1(
	int16_t /* System.Int16 */ layoutKind,
	System_Exception_t* /* System.Exception */ outException
);

System_Runtime_InteropServices_LayoutKind /* System.Runtime.InteropServices.LayoutKind */
System_Runtime_InteropServices_StructLayoutAttribute_Value_Get(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Runtime_InteropServices_StructLayoutAttribute_Pack_Get(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self
);

void /* System.Void */
System_Runtime_InteropServices_StructLayoutAttribute_Pack_Set(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self,
	int32_t /* System.Int32 */ value
);

int32_t /* System.Int32 */
System_Runtime_InteropServices_StructLayoutAttribute_Size_Get(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self
);

void /* System.Void */
System_Runtime_InteropServices_StructLayoutAttribute_Size_Set(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self,
	int32_t /* System.Int32 */ value
);

System_Runtime_InteropServices_CharSet /* System.Runtime.InteropServices.CharSet */
System_Runtime_InteropServices_StructLayoutAttribute_CharSet_Get(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self
);

void /* System.Void */
System_Runtime_InteropServices_StructLayoutAttribute_CharSet_Set(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self,
	System_Runtime_InteropServices_CharSet /* System.Runtime.InteropServices.CharSet */ value
);

void /* System.Void */
System_Runtime_InteropServices_StructLayoutAttribute_Destroy(
	System_Runtime_InteropServices_StructLayoutAttribute_t /* System.Runtime.InteropServices.StructLayoutAttribute */ self
);

#pragma mark - END APIs of System.Runtime.InteropServices.StructLayoutAttribute

#pragma mark - BEGIN APIs of System.Attribute
CBool /* System.Boolean */
System_Attribute_IsDefined(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined1(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute1(
	System_Reflection_MemberInfo_t /* System.Reflection.MemberInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined2(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined3(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute2(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute3(
	System_Reflection_ParameterInfo_t /* System.Reflection.ParameterInfo */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined4(
	System_Reflection_Module_t /* System.Reflection.Module */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined5(
	System_Reflection_Module_t /* System.Reflection.Module */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute4(
	System_Reflection_Module_t /* System.Reflection.Module */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute5(
	System_Reflection_Module_t /* System.Reflection.Module */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined6(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefined7(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute6(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ element,
	System_Type_t /* System.Type */ attributeType,
	System_Exception_t* /* System.Exception */ outException
);

System_Attribute_t /* System.Attribute */
System_Attribute_GetCustomAttribute7(
	System_Reflection_Assembly_t /* System.Reflection.Assembly */ element,
	System_Type_t /* System.Type */ attributeType,
	CBool /* System.Boolean */ inherit,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_Equals(
	System_Attribute_t /* System.Attribute */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Attribute_GetHashCode(
	System_Attribute_t /* System.Attribute */ self,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_Match(
	System_Attribute_t /* System.Attribute */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Attribute_IsDefaultAttribute(
	System_Attribute_t /* System.Attribute */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Attribute_Destroy(
	System_Attribute_t /* System.Attribute */ self
);

#pragma mark - END APIs of System.Attribute

#pragma mark - BEGIN APIs of System.Runtime.InteropServices.LayoutKind
#pragma mark - END APIs of System.Runtime.InteropServices.LayoutKind

#pragma mark - BEGIN APIs of System.Runtime.InteropServices.CharSet
#pragma mark - END APIs of System.Runtime.InteropServices.CharSet

#pragma mark - BEGIN APIs of System.Reflection.ConstructorInfo
CBool /* System.Boolean */
System_Reflection_ConstructorInfo_Equals(
	System_Reflection_ConstructorInfo_t /* System.Reflection.ConstructorInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_ConstructorInfo_GetHashCode(
	System_Reflection_ConstructorInfo_t /* System.Reflection.ConstructorInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
System_Reflection_ConstructorInfo_ConstructorName_Get(
	
);

CString /* System.String */
System_Reflection_ConstructorInfo_TypeConstructorName_Get(
	
);

void /* System.Void */
System_Reflection_ConstructorInfo_Destroy(
	System_Reflection_ConstructorInfo_t /* System.Reflection.ConstructorInfo */ self
);

#pragma mark - END APIs of System.Reflection.ConstructorInfo

#pragma mark - BEGIN APIs of System.Reflection.EventInfo
System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetAddMethod(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetRemoveMethod(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetRaiseMethod(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetAddMethod1(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	CBool /* System.Boolean */ nonPublic,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetRemoveMethod1(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	CBool /* System.Boolean */ nonPublic,
	System_Exception_t* /* System.Exception */ outException
);

System_Reflection_MethodInfo_t /* System.Reflection.MethodInfo */
System_Reflection_EventInfo_GetRaiseMethod1(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	CBool /* System.Boolean */ nonPublic,
	System_Exception_t* /* System.Exception */ outException
);

CBool /* System.Boolean */
System_Reflection_EventInfo_Equals(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	System_Object_t /* System.Object */ obj,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
System_Reflection_EventInfo_GetHashCode(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
System_Reflection_EventInfo_Destroy(
	System_Reflection_EventInfo_t /* System.Reflection.EventInfo */ self
);

#pragma mark - END APIs of System.Reflection.EventInfo

#pragma mark - BEGIN APIs of System.Reflection.EventAttributes
#pragma mark - END APIs of System.Reflection.EventAttributes

#pragma mark - BEGIN APIs of System.Reflection.InterfaceMapping
System_Type_t /* System.Type */
System_Reflection_InterfaceMapping_TargetType_Get(
	System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */ self
);

void /* System.Void */
System_Reflection_InterfaceMapping_TargetType_Set(
	System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */ self,
	System_Type_t /* System.Type */ value
);

System_Type_t /* System.Type */
System_Reflection_InterfaceMapping_InterfaceType_Get(
	System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */ self
);

void /* System.Void */
System_Reflection_InterfaceMapping_InterfaceType_Set(
	System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */ self,
	System_Type_t /* System.Type */ value
);

void /* System.Void */
System_Reflection_InterfaceMapping_Destroy(
	System_Reflection_InterfaceMapping_t /* System.Reflection.InterfaceMapping */ self
);

#pragma mark - END APIs of System.Reflection.InterfaceMapping

#pragma mark - BEGIN APIs of NativeAOT.CodeGeneratorInputSample.Person
CString /* System.String */
NativeAOT_CodeGeneratorInputSample_Person_GetNiceLevelString(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_Person_GetWelcomeMessage(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_AddChild(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ child,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_RemoveChild(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ child,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_RemoveChildAt(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */
NativeAOT_CodeGeneratorInputSample_Person_ChildAt(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	int32_t /* System.Int32 */ index,
	System_Exception_t* /* System.Exception */ outException
);

NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */
NativeAOT_CodeGeneratorInputSample_Person_Create(
	CString /* System.String */ firstName,
	CString /* System.String */ lastName,
	int32_t /* System.Int32 */ age,
	System_Exception_t* /* System.Exception */ outException
);

NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */
NativeAOT_CodeGeneratorInputSample_Person_Create1(
	CString /* System.String */ firstName,
	CString /* System.String */ lastName,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_Person_FirstName_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_FirstName_Set(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_Person_LastName_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_LastName_Set(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	CString /* System.String */ value,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_Person_Age_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_Age_Set(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	int32_t /* System.Int32 */ value,
	System_Exception_t* /* System.Exception */ outException
);

System_Array_t /* System.Array */
NativeAOT_CodeGeneratorInputSample_Person_ChildrenAsArray_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

NativeAOT_CodeGeneratorInputSample_NiceLevels /* NativeAOT.CodeGeneratorInputSample.NiceLevels */
NativeAOT_CodeGeneratorInputSample_Person_NiceLevel_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_NiceLevel_Set(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	NativeAOT_CodeGeneratorInputSample_NiceLevels /* NativeAOT.CodeGeneratorInputSample.NiceLevels */ value,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_Person_FullName_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_Person_NumberOfChildren_Get(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_Person_DEFAULT_AGE_Get(
	
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_DEFAULT_AGE_Set(
	int32_t /* System.Int32 */ value
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_Person_AGE_WHEN_BORN_Get(
	
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_Person_Destroy(
	NativeAOT_CodeGeneratorInputSample_Person_t /* NativeAOT.CodeGeneratorInputSample.Person */ self
);

#pragma mark - END APIs of NativeAOT.CodeGeneratorInputSample.Person

#pragma mark - BEGIN APIs of NativeAOT.CodeGeneratorInputSample.TestEnum
#pragma mark - END APIs of NativeAOT.CodeGeneratorInputSample.TestEnum

#pragma mark - BEGIN APIs of NativeAOT.CodeGeneratorInputSample.TestClass
void /* System.Void */
NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	CString /* System.String */ name,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	System_Exception_t* /* System.Exception */ outException
);

System_DateTime_t /* System.DateTime */
NativeAOT_CodeGeneratorInputSample_TestClass_GetDate(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_TestClass_Add(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	int32_t /* System.Int32 */ number1,
	int32_t /* System.Int32 */ number2,
	System_Exception_t* /* System.Exception */ outException
);

int32_t /* System.Int32 */
NativeAOT_CodeGeneratorInputSample_TestClass_Divide(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self,
	int32_t /* System.Int32 */ number1,
	int32_t /* System.Int32 */ number2,
	System_Exception_t* /* System.Exception */ outException
);

CString /* System.String */
NativeAOT_CodeGeneratorInputSample_TestClass_GetTestEnumName(
	NativeAOT_CodeGeneratorInputSample_TestEnum /* NativeAOT.CodeGeneratorInputSample.TestEnum */ testEnum,
	System_Exception_t* /* System.Exception */ outException
);

NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */
NativeAOT_CodeGeneratorInputSample_TestClass_Create(
	System_Exception_t* /* System.Exception */ outException
);

void /* System.Void */
NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(
	NativeAOT_CodeGeneratorInputSample_TestClass_t /* NativeAOT.CodeGeneratorInputSample.TestClass */ self
);

#pragma mark - END APIs of NativeAOT.CodeGeneratorInputSample.TestClass


#pragma mark - END APIs
#pragma mark - BEGIN Footer
#endif /* TypeDefinitions_h */

#pragma mark - END Footer
