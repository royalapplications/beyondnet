#ifndef NativeAOTCodeGeneratorOutputSample_Types_h
#define NativeAOTCodeGeneratorOutputSample_Types_h

typedef void* System_Exception_t;
typedef void* NativeAOT_CodeGeneratorInputSample_TestClass_t;

void* NativeAOT_CodeGeneratorInputSample_TestClass_Create(System_Exception_t* outException);
void* NativeAOT_CodeGeneratorInputSample_TestClass_Destroy(NativeAOT_CodeGeneratorInputSample_TestClass_t self);

void NativeAOT_CodeGeneratorInputSample_TestClass_SayHello(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                           System_Exception_t* outException);

void NativeAOT_CodeGeneratorInputSample_TestClass_SayHello1(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                            const char* name,
                                                            System_Exception_t* outException);

const char* NativeAOT_CodeGeneratorInputSample_TestClass_GetHello(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                                  System_Exception_t* outException);

int NativeAOT_CodeGeneratorInputSample_TestClass_Add(NativeAOT_CodeGeneratorInputSample_TestClass_t self,
                                                     int number1,
                                                     int number2,
                                                     System_Exception_t* outException);

#endif /* NativeAOTCodeGeneratorOutputSample_Types_h */
