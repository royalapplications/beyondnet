#ifndef NativeAOTSample_Person_NewAgeProviderDelegate_h
#define NativeAOTSample_Person_NewAgeProviderDelegate_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"

typedef void* NativeAOTSample_Person_NewAgeProviderDelegate_t;

typedef int32_t (*NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t)(const void* context);
typedef void (*NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t)(const void* context);

const System_Type_t NativeAOTSample_Person_NewAgeProviderDelegate_TypeOf(void);

const NativeAOTSample_Person_NewAgeProviderDelegate_t NativeAOTSample_Person_NewAgeProviderDelegate_Create(const void* context,
																										   const NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t function,
																										   const NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t destructorFunction);

const void* NativeAOTSample_Person_NewAgeProviderDelegate_Context_Get(const NativeAOTSample_Person_NewAgeProviderDelegate_t self);
const NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_t NativeAOTSample_Person_NewAgeProviderDelegate_CFunction_Get(const NativeAOTSample_Person_NewAgeProviderDelegate_t self);
const NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_t NativeAOTSample_Person_NewAgeProviderDelegate_CDestructorFunction_Get(const NativeAOTSample_Person_NewAgeProviderDelegate_t self);

#endif /* NativeAOTSample_Person_NewAgeProviderDelegate_h */
