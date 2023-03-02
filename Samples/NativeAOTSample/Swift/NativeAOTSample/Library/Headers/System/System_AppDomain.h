#ifndef System_AppDomain_h
#define System_AppDomain_h

#import "CommonTypes.h"

#import "System_Object.h"
#import "System_Type.h"
#import "System_UnhandledExceptionEventArgs.h"

typedef void* System_AppDomain_t;

typedef void (*UnhandledExceptionEventHandler_t)(void* context,
												 System_Object_t sender,
												 System_UnhandledExceptionEventArgs_t eventArgs);

System_Type_t System_AppDomain_TypeOf(void);
System_AppDomain_t System_AppDomain_CurrentDomain_Get(void);
int32_t System_AppDomain_Id_Get(System_AppDomain_t instance);
CBool System_AppDomain_IsDefaultAppDomain(System_AppDomain_t instance);
const char* System_AppDomain_BaseDirectory_Get(System_AppDomain_t instance);

void System_AppDomain_UnhandledException_Add(System_AppDomain_t instance,
											 const void* context,
											 UnhandledExceptionEventHandler_t eventHandler);

CStatus System_AppDomain_UnhandledException_Remove(System_AppDomain_t instance,
												   const void* context,
												   UnhandledExceptionEventHandler_t eventHandler);

#endif /* System_AppDomain_h */
