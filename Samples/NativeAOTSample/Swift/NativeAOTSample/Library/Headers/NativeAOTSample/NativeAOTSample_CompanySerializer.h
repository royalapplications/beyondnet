#ifndef NativeAOTSample_CompanySerializer_h
#define NativeAOTSample_CompanySerializer_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Exception.h"
#import "System_Type.h"

#import "NativeAOTSample_Company.h"

typedef void* NativeAOTSample_CompanySerializer_t;

System_Type_t NativeAOTSample_CompanySerializer_TypeOf(void);

NativeAOTSample_CompanySerializer_t NativeAOTSample_CompanySerializer_Create(void);

const char* NativeAOTSample_CompanySerializer_SerializeToJson(NativeAOTSample_CompanySerializer_t instance,
															  NativeAOTSample_Company_t company);

NativeAOTSample_Company_t NativeAOTSample_CompanySerializer_DeserializeFromJson(NativeAOTSample_CompanySerializer_t instance,
																				const char* jsonString,
																				System_Exception_t* outException);

#endif /* NativeAOTSample_CompanySerializer_h */
