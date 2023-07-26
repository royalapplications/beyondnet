#import <CoreFoundation/CoreFoundation.h>
#import <TargetConditionals.h>

#import <Generated_C.h>

// TODO: This should be in the generated XCframework

__attribute__((constructor))
static void __dn_library_init(void) {
#if TARGET_OS_IOS // iOS/iOS Simulator
    // TODO: These will need to be dynamically set by the Beyond.NET build system
    const char* bundleIdentifier = "com.todomycompany.beyonddotnetsamplenative";
    
    const char* icuFileName = "icudt";
    const char* icuFileType = "dat";
    
    const char* appContextIcuDatFilePathKey = "ICU_DAT_FILE_PATH";
    
    const CFStringEncoding stringEncoding = kCFStringEncodingUTF8;
    
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
    
    const char* resourceName = icuFileName;
    CFStringRef resourceNameCF = CFStringCreateWithCString(kCFAllocatorDefault,
                                                           resourceName,
                                                           stringEncoding);
    
    if (!resourceNameCF) {
        return;
    }
    
    const char* resourceType = icuFileType;
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
    System_String_t name = DNStringFromC(appContextIcuDatFilePathKey);
    
    if (!name) {
        free(resourcePath); resourcePath = NULL;
        
        return;
    }
    
    printf("Setting AppContext key \"%s\" to \"%s\"\n",
           appContextIcuDatFilePathKey,
           resourcePath);
    
    free(resourcePath); resourcePath = NULL;
    
    System_AppContext_SetData(name,
                              icuPathDN,
                              &ex);
    
    System_String_Destroy(name);
    System_String_Destroy(icuPathDN);
    
    if (ex) {
        printf("Error: Setting %s.%s path failed\n",
               icuFileName,
               icuFileType);
    }
#elif TARGET_OS_MAC && !TARGET_OS_IPHONE // macOS
#else // Other platform
#endif
}
