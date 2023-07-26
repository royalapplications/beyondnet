#import <CoreFoundation/CoreFoundation.h>
#import <TargetConditionals.h>

#import <Generated_C.h>

// TODO: This should be in the generated XCframework

__attribute__((constructor))
static void __dn_library_init(void) {
#if TARGET_OS_IOS
    const CFStringEncoding stringEncoding = kCFStringEncodingUTF8;
    
    // iOS/iOS Simulator
    const char* bundleIdentifier = "com.todomycompany.beyonddotnetsamplenative";
    CFStringRef bundleIdentifierCF = CFStringCreateWithCString(kCFAllocatorDefault,
                                                               bundleIdentifier,
                                                               stringEncoding);
    
    if (!bundleIdentifierCF) {
        return;
    }
    
    CFBundleRef bundle = CFBundleGetBundleWithIdentifier(bundleIdentifierCF);
    
    CFRelease(bundleIdentifierCF); bundleIdentifierCF = NULL;
    
    if (!bundle) {
        return;
    }
    
    const char* resourceName = "icudt";
    CFStringRef resourceNameCF = CFStringCreateWithCString(kCFAllocatorDefault,
                                                           resourceName,
                                                           stringEncoding);
    
    if (!resourceNameCF) {
        return;
    }
    
    const char* resourceType = "dat";
    CFStringRef resourceTypeCF = CFStringCreateWithCString(kCFAllocatorDefault,
                                                           resourceType,
                                                           stringEncoding);
    
    if (!resourceTypeCF) {
        CFRelease(resourceName); resourceName = NULL;
        
        return;
    }
    
    CFURLRef resourceURL = CFBundleCopyResourceURL(bundle,
                                                   resourceNameCF,
                                                   resourceTypeCF,
                                                   NULL);
    
    CFRelease(resourceNameCF); resourceNameCF = NULL;
    CFRelease(resourceTypeCF); resourceTypeCF = NULL;
    
    if (!resourceURL) {
        return;
    }
    
    CFStringRef resourcePathCF = CFURLCopyFileSystemPath(resourceURL,
                                                         kCFURLPOSIXPathStyle);
    
    CFRelease(resourceURL); resourceURL = NULL;
    
    if (!resourcePathCF) {
        return;
    }
    
    CFIndex resourcePathLength = CFStringGetLength(resourcePathCF);
    CFIndex resourcePathMaxSize = CFStringGetMaximumSizeForEncoding(resourcePathLength, stringEncoding) + 1;
    char* resourcePath = (char *)malloc(resourcePathMaxSize);
    
    if (!resourcePath) {
        CFRelease(resourcePathCF); resourcePathCF = NULL;
        
        return;
    }
    
    if (!CFStringGetCString(resourcePathCF,
                            resourcePath,
                            resourcePathMaxSize,
                            stringEncoding)) {
        free(resourcePath); resourcePath = NULL;
        CFRelease(resourcePathCF); resourcePathCF = NULL;
        
        return;
    }
    
    CFRelease(resourcePathCF); resourcePathCF = NULL;
    
    System_String_t icuPathDN = DNStringFromC(resourcePath);
    
    System_Exception_t ex = NULL;
    const char* icuDatFilePathKey = "ICU_DAT_FILE_PATH";
    System_String_t name = DNStringFromC(icuDatFilePathKey);
    
    if (!name) {
        free(resourcePath); resourcePath = NULL;
        
        return;
    }
    
    printf("Setting AppContext key \"%s\" to \"%s\"\n",
           icuDatFilePathKey,
           resourcePath);
    
    free(resourcePath); resourcePath = NULL;
    
    System_AppContext_SetData(name,
                              icuPathDN,
                              &ex);
    
    System_String_Destroy(name);
    System_String_Destroy(icuPathDN);
    
    if (ex) {
        printf("Error: Setting icudt.dat path failed\n");
    }
#elif TARGET_OS_MAC && !TARGET_OS_IPHONE
    // macOS
#else
    // Other platform
#endif
}
