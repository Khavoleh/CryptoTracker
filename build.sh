#!/bin/bash

# Path to the project file
PROJECT_PATH="CryptoTracker/CryptoTracker.csproj"
TEST_PROJECT_PATH="CryptoTrackerTests/CryptoTrackerTests.csproj"
OUTPUT_DIRECTORY="Publish"

# Project build
dotnet build "$PROJECT_PATH"
if [ $? -ne 0 ]; then
    echo "Build failed!"
    exit 1
fi

# Run tests
echo "Running tests..."
dotnet test "$TEST_PROJECT_PATH"
if [ $? -ne 0 ]; then
    echo "Tests failed!"
    exit 1
fi

# Project publish
dotnet publish "$PROJECT_PATH" -c Release -o "$OUTPUT_DIRECTORY"
if [ $? -ne 0 ]; then
    echo "Publish failed!"
    exit 1
fi

echo "Build, test, and publish completed successfully!"
