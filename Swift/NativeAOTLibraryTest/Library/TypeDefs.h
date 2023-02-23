#ifndef NativeAOTLibraryTest_TypeDefs_h
#define NativeAOTLibraryTest_TypeDefs_h

#import <stdlib.h>
#import <CoreFoundation/CFAvailability.h>

typedef void* System_Object_t;
typedef void* System_Type_t;

typedef void* NativeAOTLibraryTest_Person_t;
typedef void* NativeAOTLibraryTest_Company_t;

typedef CF_ENUM(int32_t, CStatus) {
    success = 1,
    failure = -1
};

typedef CF_ENUM(int32_t, CBool) {
    yes = 1,
    no = 0
};

#endif /* NativeAOTLibraryTest_TypeDefs_h */
