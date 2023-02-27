#ifndef System_AppDomain_h
#define System_AppDomain_h

#import <stdlib.h>
#import "TypeDefs.h"

System_AppDomain_t System_AppDomain_CurrentDomain_Get(void);
int32_t System_AppDomain_Id_Get(System_AppDomain_t instance);
CBool System_AppDomain_IsDefaultAppDomain(System_AppDomain_t instance);
const char* System_AppDomain_BaseDirectory_Get(System_AppDomain_t instance);

#endif /* System_AppDomain_h */
