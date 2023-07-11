#include <stdlib.h>
#include <stdio.h>
#include "Generated_C.h"

int newguid(int argc, char *argv[]) {
	int numGuids = 1;

	if (argc == 2) {
		numGuids = atoi(argv[1]);
	}

	if (numGuids <= 0) {
		numGuids = 1;
	}

	while (numGuids > 0) {
		System_Exception_t exception;
		System_Guid_t guid = System_Guid_NewGuid(&exception);

		if (guid == NULL ||
			exception != NULL) {
			printf("An error occurred while creating a new System.Guid.\n");

			return 1;
		}

		System_String_t guidStringDN = System_Guid_ToString(guid, &exception);

		if (guidStringDN == NULL ||
			exception != NULL) {
			printf("An error occurred while converting a System.Guid to a System.String.\n");

			return 1;
		}

		const char* guidString = DNStringToC(guidStringDN);

		if (guidString == NULL) {
			printf("An error occurred while converting a System.String to a char*.\n");

			return 1;
		}

		printf("%s\n", guidString);

		free((void*)guidString);
		System_String_Destroy(guidStringDN);
		System_Guid_Destroy(guid);
		
		numGuids--;
	}

	return 0;
}

struct TransformerContext {
	int uppercase;
};
typedef struct TransformerContext* TransformerContext_t;

TransformerContext_t transformercontext_create(void) {
	TransformerContext_t context = (TransformerContext_t)malloc(sizeof(TransformerContext_t));
	context->uppercase = 0;

	return context;
}

void transformercontext_destroy(TransformerContext_t context) {
	free(context);
}

System_String_t transform(
	void* context, // The context (TransformerContext_t)
	System_String_t string // The string to transform
) {
	// Cast the context to our TransformerContext_t
	TransformerContext_t transformerContext = (TransformerContext_t)context;

	// Should we upper- or lowercase?
	int shouldUppercase = transformerContext->uppercase;

	System_String_t transformedSystemString;

	if (shouldUppercase) { // Uppercase
		transformedSystemString = System_String_ToUpper(
			string,
			NULL // TODO: Error handling
		);
	} else { // Lowercase
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

void destroy_transformer(
	void* context // The context (TransformerContext_t) to destroy
) {
	if (context == NULL) {
		return;
	}

	TransformerContext_t transformerContext = (TransformerContext_t)context;

	printf("Destroying string transformer delegate context\n");
	transformercontext_destroy(transformerContext);
}

void transformertest(void) {
	const char* originalCString = "Hello";

	// Convert the C String to a .NET System.String
	System_String_t originalSystemString = DNStringFromC(originalCString);

	// Allocate memory for our TransformerContext_t
	// The memory is freed later in the delegate destructor callback
	TransformerContext_t transformerContext = transformercontext_create();

	// We want to transform the string to uppercase
	transformerContext->uppercase = 1;

	// Create a delegate object
	Beyond_NET_Sample_Transformer_StringTransformerDelegate_t stringTransformerDelegate = Beyond_NET_Sample_Transformer_StringTransformerDelegate_Create(
		transformerContext,
		transform, // function pointer
		destroy_transformer // destructor function pointer
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
	printf("Original: %s\n", originalCString);
	printf("Transformed: %s\n", transformedCString);

	// Clean up
	System_String_Destroy(originalSystemString);
	System_String_Destroy(transformedSystemString);
	Beyond_NET_Sample_Transformer_StringTransformerDelegate_Destroy(stringTransformerDelegate);

	// Force the GC to collect and call our delegate destructor function
	System_GC_Collect_1(NULL);
	System_GC_WaitForPendingFinalizers(NULL);
}

int main(int argc, char *argv[]) {
	transformertest();

	return newguid(argc, argv);
}