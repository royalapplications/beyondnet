#ifndef System_Exception_h
#define System_Exception_h

#import <stdlib.h>
#import "TypeDefs.h"

char* System_Exception_Message_Get(System_Exception_t exception);
int32_t System_Exception_HResult_Get(System_Exception_t exception);
char* System_Exception_StackTrace_Get(System_Exception_t exception);

#endif /* System_Exception_h */
