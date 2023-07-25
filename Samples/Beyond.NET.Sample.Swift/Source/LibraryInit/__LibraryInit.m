#import <Foundation/Foundation.h>
#import <TargetConditionals.h>

#import <Generated_C.h>

// TODO: This should be in the generated XCframework

@interface __DNLibraryInit : NSObject
@end

@implementation __DNLibraryInit

+ (void)load {
#if TARGET_OS_IOS
    // iOS/iOS Simulator
    NSBundle* currentBundle = [NSBundle bundleForClass:__DNLibraryInit.class];
    
    if (!currentBundle) {
        return;
    }
    
    NSString* frameworksPath = currentBundle.privateFrameworksPath;
    NSString* beyondDotNETSampleNativeFrameworkPath = [frameworksPath stringByAppendingPathComponent:@"BeyondDotNETSampleNative.framework"];
    
    NSBundle* bundle = [NSBundle bundleWithPath:beyondDotNETSampleNativeFrameworkPath];
    
    if (!bundle) {
        return;
    }
    
    System_Exception_t ex = NULL;
    System_String_t name = DNStringFromC("ICU_DAT_FILE_PATH");
    
    if (!name) {
        return;
    }
    
    NSString* icuPath = [bundle pathForResource:@"icudt" ofType:@"dat"];
    
    if (!icuPath) {
        return;
    }
    
    System_String_t icuPathDN = DNStringFromC(icuPath.UTF8String);
    
    System_AppContext_SetData(name,
                              icuPathDN,
                              &ex);
    
    System_String_Destroy(name);
    System_String_Destroy(icuPathDN);
    
    if (ex) {
        NSLog(@"Error: Setting icudt.dat path failed.");
    }
#elif TARGET_OS_MAC && !TARGET_OS_IPHONE
    // macOS
#else
    // Other platform
#endif
}

@end
