#ifndef NativeAOTSample_Person_h
#define NativeAOTSample_Person_h

#include <stdlib.h>
#import "TypeDefs.h"

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

// Sample API for demonstrating non-escaping closures
CStatus NativeAOTSample_Person_ChangeAge(NativeAOTSample_Person_t instance,
										 const void* context,
										 NativeAOTSample_Person_ChangeAge_NewAgeProvider_t newAgeProvider,
										 System_Exception_t* exception);

#endif /* NativeAOTSample_Person_h */
