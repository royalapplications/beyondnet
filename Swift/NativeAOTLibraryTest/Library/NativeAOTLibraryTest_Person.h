#ifndef NativeAOTLibraryTest_Person_h
#define NativeAOTLibraryTest_Person_h

#include <stdlib.h>
#import "TypeDefs.h"

NativeAOTLibraryTest_Person_t NativeAOTLibraryTest_Person_Create(const char* firstName,
                                                                 const char* lastName,
                                                                 const int32_t age);

int32_t NativeAOTLibraryTest_Person_Age_Get(NativeAOTLibraryTest_Person_t person);
void NativeAOTLibraryTest_Person_Age_Set(NativeAOTLibraryTest_Person_t person, const int32_t age);

char* NativeAOTLibraryTest_Person_FirstName_Get(NativeAOTLibraryTest_Person_t person);
void NativeAOTLibraryTest_Person_FirstName_Set(NativeAOTLibraryTest_Person_t person, const char* firstName);

char* NativeAOTLibraryTest_Person_LastName_Get(NativeAOTLibraryTest_Person_t person);
void NativeAOTLibraryTest_Person_LastName_Set(NativeAOTLibraryTest_Person_t person, const char* lastName);

char* NativeAOTLibraryTest_Person_FullName_Get(NativeAOTLibraryTest_Person_t person);

#endif /* NativeAOTLibraryTest_Person_h */
