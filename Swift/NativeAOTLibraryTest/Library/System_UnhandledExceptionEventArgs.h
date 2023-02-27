#ifndef System_UnhandledExceptionEventArgs_h
#define System_UnhandledExceptionEventArgs_h

#import <stdlib.h>
#import "TypeDefs.h"

System_Object_t System_UnhandledExceptionEventArgs_ExceptionObject_Get(System_UnhandledExceptionEventArgs_t instance);
CBool System_UnhandledExceptionEventArgs_IsTerminating_Get(System_UnhandledExceptionEventArgs_t instance);

#endif /* System_UnhandledExceptionEventArgs_h */
