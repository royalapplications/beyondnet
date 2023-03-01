#ifndef NativeAOTSample_TypeDefs_h
#define NativeAOTSample_TypeDefs_h

#import <stdlib.h>

typedef void* System_Object_t;
typedef void* System_Type_t;
typedef void* System_AppDomain_t;
typedef void* System_Exception_t;
typedef void* System_UnhandledExceptionEventArgs_t;

typedef void (*VoidDelegate_t)(void);
typedef void (*ContextDelegate_t)(void* context);

typedef void* NativeAOTSample_Person_t;
typedef void* NativeAOTSample_Company_t;

typedef enum __attribute__((enum_extensibility(open))): int32_t {
	CStatusSuccess = 1,
	CStatusFailure = -1
} CStatus;

typedef enum __attribute__((enum_extensibility(closed))): uint8_t {
	CBoolYes = 1,
	CBoolNo = 0
} CBool;

#endif /* NativeAOTSample_TypeDefs_h */
