#ifndef TypeDefinitions_h
#define TypeDefinitions_h

#import <stdlib.h>

#pragma mark - Common Enums
typedef enum __attribute__((enum_extensibility(closed))): uint8_t {
    CBoolYes = 1,
    CBoolNo = 0
} CBool;

typedef void* System_Object_t;
typedef void* System_Type_t;
typedef void* System_Guid_t;
typedef void* System_Exception_t;

typedef void* NativeAOT_CodeGeneratorInputSample_TestClass_t;

void
System_Object_Destroy(System_Object_t self);

System_Type_t
System_Type_GetType(const char* typeName,
                    CBool throwOnError,
                    CBool ignoreCase,
                    System_Exception_t* outException);

CBool
System_Type_IsAssignableTo(System_Type_t self,
                           System_Type_t targetType,
                           System_Exception_t* outException);

const char*
System_Type_ToString(System_Type_t self,
                     System_Exception_t* outException);

System_Type_t
System_Object_GetType(System_Object_t self,
                      System_Exception_t* outException);

const char*
System_Exception_ToString(System_Exception_t self,
                          System_Exception_t* outException);

System_Guid_t
System_Guid_NewGuid(System_Exception_t* outException);

System_Guid_t
System_Guid_Create2(const char* g,
                    System_Exception_t* outException);

const char*
System_Guid_ToString(System_Guid_t self,
                     System_Exception_t* outException);


NativeAOT_CodeGeneratorInputSample_TestClass_t
NativeAOT_CodeGeneratorInputSample_TestClass_Create(System_Exception_t* outException);

void
NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(NativeAOT_CodeGeneratorInputSample_TestClass_t self);

void
NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                      System_Exception_t* outException);

void
NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                       const char* name,
                                                       System_Exception_t* outException);

const char*
NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                      System_Exception_t* outException);

int
NativeAOT_CodeGeneratorInputSample_TestClass_Add(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                 int number1,
                                                 int number2,
                                                 System_Exception_t* outException);

int
NativeAOT_CodeGeneratorInputSample_TestClass_Divide(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                    int number1,
                                                    int number2,
                                                    System_Exception_t* outException);

#endif /* TypeDefinitions_h */
