#ifndef NativeAOTLibraryTest_Company_h
#define NativeAOTLibraryTest_Company_h

#import <stdlib.h>
#import "NativeAOTLibraryTest_TypeDefs.h"

nativeaotlibrarytest_company_t nativeaotlibrarytest_company_create(const char* name);

void nativeaotlibrarytest_company_destroy(nativeaotlibrarytest_company_t company);

const char* nativeaotlibrarytest_company_name_get(nativeaotlibrarytest_company_t company);

int32_t nativeaotlibrarytest_company_numberofemployees_get(nativeaotlibrarytest_company_t company);

void nativeaotlibrarytest_company_addemployee(nativeaotlibrarytest_company_t company, nativeaotlibrarytest_person_t employee);

#endif /* NativeAOTLibraryTest_Company_h */
