#ifndef NativeAOTSample_Person_h
#define NativeAOTSample_Person_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"
#import "NativeAOTSample_CDelegate.h"

typedef void* NativeAOTSample_Person_t;

System_Type_t NativeAOTSample_Person_TypeOf(void);

NativeAOTSample_Person_t NativeAOTSample_Person_Create(const char* firstName,
													   const char* lastName,
													   const int32_t age);

int32_t NativeAOTSample_Person_Age_Get(NativeAOTSample_Person_t instance);
void NativeAOTSample_Person_Age_Set(NativeAOTSample_Person_t instance, const int32_t age);

char* NativeAOTSample_Person_FirstName_Get(NativeAOTSample_Person_t instance);
void NativeAOTSample_Person_FirstName_Set(NativeAOTSample_Person_t instance, const char* firstName);

char* NativeAOTSample_Person_LastName_Get(NativeAOTSample_Person_t instance);
void NativeAOTSample_Person_LastName_Set(NativeAOTSample_Person_t instance, const char* lastName);

char* NativeAOTSample_Person_FullName_Get(NativeAOTSample_Person_t instance);

CStatus NativeAOTSample_Person_ReduceAge(NativeAOTSample_Person_t instance,
										 int32_t byYears,
										 System_Exception_t* exception);


// Sample API for demonstrating non-escaping closures
typedef int32_t (*NativeAOTSample_Person_ChangeAge_NewAgeProvider_t)(const void* context);

const NativeAOT_Core_CDelegate_t NativeAOTSample_Person_NewAgeProvider_Create(const void* context,
																			  const NativeAOTSample_Person_ChangeAge_NewAgeProvider_t newAgeProvider,
																			  const NativeAOTSample_CDelegate_Destructor_t destructorFunction);

CStatus NativeAOTSample_Person_ChangeAge(NativeAOTSample_Person_t instance,
										 const NativeAOT_Core_CDelegate_t newAgeProvider,
										 System_Exception_t* exception);

#endif /* NativeAOTSample_Person_h */
