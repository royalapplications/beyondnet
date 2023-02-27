#ifndef NativeAOTLibraryTest_Company_h
#define NativeAOTLibraryTest_Company_h

#import <stdlib.h>
#import "TypeDefs.h"

System_Type_t NativeAOTLibraryTest_Company_TypeOf(void);

NativeAOTLibraryTest_Company_t NativeAOTLibraryTest_Company_Create(const char* name);

char* NativeAOTLibraryTest_Company_Name_Get(NativeAOTLibraryTest_Company_t instance);
void NativeAOTLibraryTest_Company_Name_Set(NativeAOTLibraryTest_Company_t instance, const char* name);

int32_t NativeAOTLibraryTest_Company_NumberOfEmployees_Get(NativeAOTLibraryTest_Company_t instance);

CStatus NativeAOTLibraryTest_Company_AddEmployee(NativeAOTLibraryTest_Company_t instance,
                                                 NativeAOTLibraryTest_Person_t employee);

CStatus NativeAOTLibraryTest_Company_RemoveEmployee(NativeAOTLibraryTest_Company_t instance,
                                                    NativeAOTLibraryTest_Person_t employee);

CBool NativeAOTLibraryTest_Company_ContainsEmployee(NativeAOTLibraryTest_Company_t instance,
                                                    NativeAOTLibraryTest_Person_t employee);

NativeAOTLibraryTest_Company_t NativeAOTLibraryTest_Company_GetEmployeeAtIndex(NativeAOTLibraryTest_Company_t instance,
                                                                               const int32_t index);

CStatus NativeAOTLibraryTest_Company_NumberOfEmployeesChanged_Get(NativeAOTLibraryTest_Company_t instance,
																  const void** outContext,
																  ContextDelegate_t* outDelegate);

void NativeAOTLibraryTest_Company_NumberOfEmployeesChanged_Set(NativeAOTLibraryTest_Company_t instance,
                                                               const void* context,
                                                               ContextDelegate_t delegate);

#endif /* NativeAOTLibraryTest_Company_h */
