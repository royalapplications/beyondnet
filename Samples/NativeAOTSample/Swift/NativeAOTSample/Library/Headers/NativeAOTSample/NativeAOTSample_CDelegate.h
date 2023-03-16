#ifndef NativeAOTSample_CDelegate_h
#define NativeAOTSample_CDelegate_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"

typedef void* NativeAOT_Core_CDelegate_t;

typedef void (*NativeAOTSample_CDelegate_Destructor_t)(const void* context);

System_Type_t NativeAOT_Core_CDelegate_TypeOf(void);

NativeAOT_Core_CDelegate_t NativeAOT_Core_CDelegate_Create(const void* context,
                                                           const void* function,
                                                           const NativeAOTSample_CDelegate_Destructor_t destructorFunction);

void NativeAOT_Core_CDelegate_Destroy(NativeAOT_Core_CDelegate_t self);

#endif /* NativeAOTSample_CDelegate_h */
