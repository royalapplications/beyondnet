#ifndef NativeAOTLibraryTest_TypeDefs_h
#define NativeAOTLibraryTest_TypeDefs_h

#import <stdlib.h>
#import <CoreFoundation/CFAvailability.h>

typedef void* nativeaotlibrarytest_person_t;
typedef void* nativeaotlibrarytest_company_t;

typedef CF_ENUM(int32_t, CStatus) {
    success = 1,
    failure = -1
};

typedef CF_ENUM(int32_t, CBool) {
    yes = 1,
    no = 0
};

#endif /* NativeAOTLibraryTest_TypeDefs_h */
