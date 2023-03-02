#ifndef System_UnhandledExceptionEventArgs_h
#define System_UnhandledExceptionEventArgs_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"

typedef void* System_UnhandledExceptionEventArgs_t;

System_Type_t System_UnhandledExceptionEventArgs_TypeOf(void);
System_Object_t System_UnhandledExceptionEventArgs_ExceptionObject_Get(System_UnhandledExceptionEventArgs_t instance);
CBool System_UnhandledExceptionEventArgs_IsTerminating_Get(System_UnhandledExceptionEventArgs_t instance);

#endif /* System_UnhandledExceptionEventArgs_h */
