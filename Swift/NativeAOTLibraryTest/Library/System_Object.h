#ifndef System_Object_h
#define System_Object_h

#import <stdlib.h>
#import "NativeAOTLibraryTest_TypeDefs.h"

CBool System_Object_Equals(System_Object_t firstObject, System_Object_t secondObject);

void System_Object_Destroy(System_Object_t object);

#endif /* System_Object_h */
