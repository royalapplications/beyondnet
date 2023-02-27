#ifndef System_Object_h
#define System_Object_h

#import <stdlib.h>
#import "TypeDefs.h"

System_Type_t System_Object_GetType(System_Object_t instance);

CBool System_Object_Equals(System_Object_t firstObject, System_Object_t secondObject);

void System_Object_Destroy(System_Object_t instance);

#endif /* System_Object_h */
