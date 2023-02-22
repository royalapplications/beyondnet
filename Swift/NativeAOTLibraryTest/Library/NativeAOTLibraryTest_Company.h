#ifndef NativeAOTLibraryTest_Company_h
#define NativeAOTLibraryTest_Company_h

#include <stdlib.h>

typedef void* nativeaotlibrarytest_company_t;

nativeaotlibrarytest_company_t nativeaotlibrarytest_company_create(const char* name);

void nativeaotlibrarytest_company_destroy(nativeaotlibrarytest_company_t company);

const char* nativeaotlibrarytest_company_name_get(nativeaotlibrarytest_company_t company);

int32_t nativeaotlibrarytest_company_numberofemployees_get(nativeaotlibrarytest_company_t company);

#endif /* NativeAOTLibraryTest_Company_h */
