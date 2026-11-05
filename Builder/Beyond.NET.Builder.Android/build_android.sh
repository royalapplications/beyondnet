#!/usr/bin/env bash

# Script to build .NET NativeAOT for Android
# Usage: build_android.sh <working_directory> <runtime_identifier> <configuration> <verbosity>

set -e

# Parse arguments
WORKING_DIR="$1"
RUNTIME_IDENTIFIER="${2:-linux-bionic-arm64}"
CONFIGURATION="${3:-Release}"
VERBOSITY="${4:-normal}"
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Script is located at: $SCRIPT_DIR"

if [ -z "$WORKING_DIR" ]; then
    echo "Error: Working directory is required"
    echo "Usage: $0 <working_directory> [runtime_identifier] [configuration] [verbosity]"
    exit 1
fi

if [ ! -d "$WORKING_DIR" ]; then
    echo "Error: Working directory does not exist: $WORKING_DIR"
    exit 1
fi

echo "Building .NET NativeAOT for Android"
echo "  Working Directory: $WORKING_DIR"
echo "  Runtime Identifier: $RUNTIME_IDENTIFIER"
echo "  Configuration: $CONFIGURATION"
echo "  Verbosity: $VERBOSITY"
echo ""

# Either set the "ANDROID_NDK_BIN_PATH" environment variable and point it to the following Android NDK installation directory's subpath "ndk_home/toolchains/llvm/prebuilt/platform-arch/bin"
# Or alternatively, make sure the "ANDROID_NDK_HOME" enviroment variable is set and pointing to your Android NDK installation. We'll then try to automatically detect where the bin path is.
if [ ! -d "${ANDROID_NDK_BIN_PATH}" ] ; then
  echo "Warning: ANDROID_NDK_BIN_PATH enviroment variable is not set or directory does not exist. Trying to fall back to ANDROID_NDK_HOME and automatically detecting the bin directory."

  if [ ! -d "${ANDROID_NDK_HOME}" ] ; then
    echo "Error: ANDROID_NDK_HOME enviroment variable is not set or directory does not exist."
    exit 1
  fi

  echo "Android NDK Home: ${ANDROID_NDK_HOME}"

  # ndk_bin_common.sh sets the "HOST_TAG" environment variable to the NDK's toolchain platform
  source "${ANDROID_NDK_HOME}/build/tools/ndk_bin_common.sh"
  echo "Android Host Tag: ${HOST_TAG}"

  ANDROID_NDK_BIN_PATH="${ANDROID_NDK_HOME}/toolchains/llvm/prebuilt/${HOST_TAG}/bin"

  if [ ! -d "${ANDROID_NDK_BIN_PATH}" ] ; then
    echo "Error: Android NDK bin Path not found at ${ANDROID_NDK_BIN_PATH}"
    exit 1
  fi
fi

echo "Android NDK bin Path: ${ANDROID_NDK_BIN_PATH}"

export PATH=${ANDROID_NDK_BIN_PATH}:$PATH

echo ""

# Change to working directory
cd "$WORKING_DIR"

# Run dotnet publish with Android-specific settings
echo "Running: dotnet publish -r $RUNTIME_IDENTIFIER -c $CONFIGURATION -v $VERBOSITY -p:PublishAotUsingRuntimePack=true"
echo ""

dotnet publish \
    -r "$RUNTIME_IDENTIFIER" \
    -c "$CONFIGURATION" \
    -v "$VERBOSITY" \
    -p:PublishAotUsingRuntimePack=true

EXIT_CODE=$?

if [ $EXIT_CODE -eq 0 ]; then
    echo ""
    echo "Android build completed successfully"
else
    echo ""
    echo "Android build failed with exit code $EXIT_CODE"
    exit $EXIT_CODE
fi

exit 0
