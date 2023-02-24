#ifndef NativeAOTLibraryTest_Company_h
#define NativeAOTLibraryTest_Company_h

#import <stdlib.h>
#import "TypeDefs.h"

NativeAOTLibraryTest_Company_t NativeAOTLibraryTest_Company_Create(const char* name);

char* NativeAOTLibraryTest_Company_Name_Get(NativeAOTLibraryTest_Company_t company);
void NativeAOTLibraryTest_Company_Name_Set(NativeAOTLibraryTest_Company_t company, const char* name);

int32_t NativeAOTLibraryTest_Company_NumberOfEmployees_Get(NativeAOTLibraryTest_Company_t company);

CStatus NativeAOTLibraryTest_Company_AddEmployee(NativeAOTLibraryTest_Company_t company,
                                                 NativeAOTLibraryTest_Person_t employee);

CStatus NativeAOTLibraryTest_Company_RemoveEmployee(NativeAOTLibraryTest_Company_t company,
                                                    NativeAOTLibraryTest_Person_t employee);

CBool NativeAOTLibraryTest_Company_ContainsEmployee(NativeAOTLibraryTest_Company_t company,
                                                    NativeAOTLibraryTest_Person_t employee);

NativeAOTLibraryTest_Company_t NativeAOTLibraryTest_Company_GetEmployeeAtIndex(NativeAOTLibraryTest_Company_t company,
                                                                               const int32_t index);

#endif /* NativeAOTLibraryTest_Company_h */
