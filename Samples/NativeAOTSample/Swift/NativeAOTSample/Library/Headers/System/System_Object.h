#ifndef System_Object_h
#define System_Object_h

#import <stdlib.h>
#import "TypeDefs.h"

System_Type_t System_Object_TypeOf(void);

System_Object_t System_Object_Create(void);
void System_Object_Destroy(System_Object_t instance);

System_Type_t System_Object_GetType(System_Object_t instance);

CBool System_Object_Equals(System_Object_t firstObject, System_Object_t secondObject);
CBool System_Object_ReferenceEquals(System_Object_t firstObject, System_Object_t secondObject);

const char* System_Object_ToString(System_Object_t instance);

System_Object_t System_Object_CastAs(System_Object_t instance,
									 System_Type_t targetType);

CBool System_Object_Is(System_Object_t instance,
					   System_Type_t targetType);

#endif /* System_Object_h */
