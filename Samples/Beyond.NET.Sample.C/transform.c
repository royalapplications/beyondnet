#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#include "Generated_C.h"

// We use this as the "context" for our .NET delegate
struct Transformer {
	int uppercase;
};

// Declare a typedef for our Transformer struct
typedef struct Transformer* Transformer_t;

// Creates an instance of a Transformer and configures it
Transformer_t transformer_create(
	int uppercase // Pass 0 to lowercase or 1 to uppercase
) {
	// Allocate memory for the transformer context
	Transformer_t transformer = (Transformer_t)malloc(sizeof(Transformer_t));

	// Configure it
	transformer->uppercase = uppercase;

	return transformer;
}

// Destroys an instance of a Transformer
void transformer_destroy(
	void* context // The context (Transformer_t)
) {
	if (!context) {
		// Nothing to do
		return;
	}

	// Cast the context to our Transformer_t
	Transformer_t transformer = (Transformer_t)context;

	printf("Destroying transformer\n");

	// Free the memory we previously allocated in transformer_create
	free(transformer);
}

// Does the actual transformation; Note that the signature must match the one of our .NET StringTransformerDelegate + a context as the first parameter
System_String_t transformer_transform(
	void* context, // The context (Transformer_t)
	System_String_t string // The string to transform
) {
	if (!context ||
		!string) {
		// Nothing to do
		return NULL;
	}

	// Cast the context to our Transformer_t
	Transformer_t transformer = (Transformer_t)context;

	// Should we upper- or lowercase?
	int shouldUppercase = transformer->uppercase;

	System_String_t transformedSystemString;

	if (shouldUppercase) { // Uppercase
		// Call the .NET System.String.ToUpper method
		transformedSystemString = System_String_ToUpper(
			string,
			NULL // TODO: Error handling
		);
	} else { // Lowercase
		// Call the .NET System.String.ToLower method
		transformedSystemString = System_String_ToLower(
			string,
			NULL // TODO: Error handling
		);
	}

	// Need to destroy all parameters passed to the delegate handler
	System_String_Destroy(string);

	// The return value will be destroyed by the .NET bindings so you don't need to worry about memory management of return values
	return transformedSystemString;
}

// Entry point of the program
int main(int argc, char *argv[]) {
	if (argc < 2) {
		printf("Error: No input specified. Please provide a string to transform as the first and only argument.\n");

		return 1;
	}

	// Get the original string from CLI parameters
	const char* originalCString = argv[1];

	// Convert the C String to a .NET System.String
	System_String_t originalSystemString = DNStringFromC(originalCString);

	// By setting this to 0 instead of 1 we'd get a lowercased string
	int shouldUppercase = 1;

	// Allocate memory for our Transformer_t and configure it
	// The memory is freed later in the delegate destructor callback
	Transformer_t transformer = transformer_create(shouldUppercase);

	// Create a delegate object
	Beyond_NET_Sample_Transformer_StringTransformerDelegate_t stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate_Create(
		transformer,
		transformer_transform, // function pointer
		transformer_destroy // destructor function pointer
	);

	// Pass the original string and the delegate object
	System_String_t transformedSystemString = Beyond_NET_Sample_Transformer_TransformString(
		originalSystemString,
		stringTransformerDelegate,
		NULL // TODO: Error handling
	);

	// Convert the returned .NET System.String to a C String
	const char* transformedCString = DNStringToC(transformedSystemString);

	// Print the original and the transformed strings
	printf("Original: %s\nTransformed: %s\n", originalCString, transformedCString);

	// Clean up
	System_String_Destroy(originalSystemString);
	System_String_Destroy(transformedSystemString);
	free((void*)transformedCString);

	// While we don't need the delegate object here anymore and thus can destroy it, .NET might still hold a reference to it
	Beyond_NET_Sample_Transformer_StringTransformerDelegate_Destroy(stringTransformerDelegate);

	// Force the GC to collect now which will cause our delegate destructor function (transformer_destroy) to be called
	System_GC_Collect_1(NULL);
	System_GC_WaitForPendingFinalizers(NULL);

	return 0;
}