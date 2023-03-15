#ifndef NativeAOTSample_NativeDelegate_h
#define NativeAOTSample_NativeDelegate_h

#import "CommonTypes.h"

#import "System_Object.h"
//#import "System_Type.h"

typedef void* NativeAOT_Core_NativeDelegate_t;

//System_Type_t NativeAOTSample_UnhandledExceptionTest_TypeOf(void);

NativeAOT_Core_NativeDelegate_t NativeAOT_Core_NativeDelegate_Create(void);
void NativeAOT_Core_NativeDelegate_Destroy(NativeAOT_Core_NativeDelegate_t self);

#endif /* NativeAOTSample_NativeDelegate_h */
