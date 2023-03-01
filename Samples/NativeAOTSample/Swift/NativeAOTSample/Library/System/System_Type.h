#ifndef System_Type_h
#define System_Type_h

#import <stdlib.h>
#import "TypeDefs.h"

System_Type_t System_Type_TypeOf(void);
System_Type_t System_Type_GetType(const char* typeName);
char* System_Type_Name_Get(System_Type_t instance);
char* System_Type_FullName_Get(System_Type_t instance);

CBool System_Type_IsAssignableFrom(System_Type_t instance,
								   System_Type_t targetType);

CBool System_Type_IsAssignableTo(System_Type_t instance,
								 System_Type_t targetType);

#endif /* System_Type_h */
