#include <stdio.h>
#include "Generated_C.h"

int main(int argc, char *argv[]) {
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