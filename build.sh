#!/bin/bash

# Path to the project file
PROJECT_PATH="CryptoTracker/CryptoTracker.csproj"
OUTPUT_DIRECTORY="Publish"

# Project build
dotnet build "$PROJECT_PATH"
if [ $? -ne 0 ]; then
    echo "Build failed!"
    exit 1
fi

# Project publish
dotnet publish "$PROJECT_PATH" -c Release -o "$OUTPUT_DIRECTORY"
if [ $? -ne 0 ]; then
    echo "Publish failed!"
    exit 1
fi

echo "Build, test, and publish completed successfully!"
