#ifndef NativeAOTLibraryTest_Person_h
#define NativeAOTLibraryTest_Person_h

#include <stdlib.h>

typedef void* nativeaotlibrarytest_person_t;

nativeaotlibrarytest_person_t nativeaotlibrarytest_person_create(const int32_t age);
void nativeaotlibrarytest_person_destroy(nativeaotlibrarytest_person_t person);

int32_t nativeaotlibrarytest_person_age_get(nativeaotlibrarytest_person_t person);

#endif /* NativeAOTLibraryTest_Person_h */
