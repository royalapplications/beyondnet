namespace Beyond.NET.CodeGenerator.Generator.C;

internal static class CSharedCode
{
    internal const string HeaderCode = /*lang=C*/"""
#ifndef TypeDefinitions_h
#define TypeDefinitions_h

#import <stdlib.h>
#import <stdbool.h>
#import <stdint.h>

#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Wnullability-completeness"
#pragma clang diagnostic ignored "-Wflag-enum"
""";

    internal const string CommonTypesCode = /*lang=C*/"""
typedef const char* DNCString;

typedef struct DNReadOnlySpanOfByte {
    const void* dataPointer;
    int32_t dataLength;
} DNReadOnlySpanOfByte;
""";
    
    internal const string UtilsCode = /*lang=C*/"""
_Nonnull DNCString
DNStringToC(_Nonnull System_String_t systemString);

_Nonnull System_String_t
DNStringFromC(_Nonnull DNCString cString);

_Nullable System_Object_t
DNObjectCastTo(_Nonnull System_Object_t object, _Nonnull System_Type_t type, _Nullable System_Exception_t* outException);

_Nullable System_Object_t
DNObjectCastAs(_Nonnull System_Object_t object, _Nonnull System_Type_t type);

bool
DNObjectIs(_Nonnull System_Object_t object, _Nonnull System_Type_t type);

bool
DNObjectCastToBool(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Boolean_t
DNObjectFromBool(bool value);

wchar_t
DNObjectCastToChar(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Char_t
DNObjectFromChar(wchar_t value);

float
DNObjectCastToFloat(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Single_t
DNObjectFromFloat(float value);

double
DNObjectCastToDouble(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Double_t
DNObjectFromDouble(double value);

int8_t
DNObjectCastToInt8(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_SByte_t
DNObjectFromInt8(int8_t number);

uint8_t
DNObjectCastToUInt8(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Byte_t
DNObjectFromUInt8(uint8_t number);

int16_t
DNObjectCastToInt16(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Int16_t
DNObjectFromInt16(int16_t number);

uint16_t
DNObjectCastToUInt16(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_UInt16_t
DNObjectFromUInt16(uint16_t number);

int32_t
DNObjectCastToInt32(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Int32_t
DNObjectFromInt32(int32_t number);

uint32_t
DNObjectCastToUInt32(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_UInt32_t
DNObjectFromUInt32(uint32_t number);

int64_t
DNObjectCastToInt64(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_Int64_t
DNObjectFromInt64(int64_t number);

uint64_t
DNObjectCastToUInt64(_Nonnull System_Object_t object, _Nullable System_Exception_t* outException);

_Nonnull System_UInt64_t
DNObjectFromUInt64(uint64_t number);

void*
DNGetPinnedPointerToByteArray(_Nonnull System_Byte_Array_t byteArray, _Nullable System_Runtime_InteropServices_GCHandle_t* outGCHandle, _Nullable System_Exception_t* outException);
""";
    
        internal const string FooterCode = /*lang=C*/"""
#pragma clang diagnostic pop

#endif /* TypeDefinitions_h */
""";
}