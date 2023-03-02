#ifndef System_GC_h
#define System_GC_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"

System_Type_t System_GC_TypeOf(void);
void System_GC_Collect(void);

#endif /* System_GC_h */
