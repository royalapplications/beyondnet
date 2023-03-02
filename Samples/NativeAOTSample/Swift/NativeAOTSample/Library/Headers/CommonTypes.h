#ifndef NativeAOTSample_TypeDefs_h
#define NativeAOTSample_TypeDefs_h

#import <stdlib.h>

#pragma mark - Common Enums
typedef enum __attribute__((enum_extensibility(open))): int32_t {
	CStatusSuccess = 1,
	CStatusFailure = -1
} CStatus;

typedef enum __attribute__((enum_extensibility(closed))): uint8_t {
	CBoolYes = 1,
	CBoolNo = 0
} CBool;

#pragma mark - Common Function Pointer Signatures
typedef void (*ContextDelegate_t)(void* context);

#endif /* NativeAOTSample_TypeDefs_h */
