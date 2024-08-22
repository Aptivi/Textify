#!/bin/bash

# This script builds. Use when you have dotnet installed.
releaseconf=$1
if [ -z $releaseconf ]; then
	releaseconf=Release
fi

# Check for dependencies
dotnetpath=`which dotnet`
if [ ! $? == 0 ]; then
	echo dotnet is not found.
	exit 1
fi

# Download packages
echo Downloading packages...
"$dotnetpath" restore "../Textify.sln" --configuration $releaseconf
if [ ! $? == 0 ]; then
	echo Download failed.
	exit 1
fi

# Build Textify
echo Building Textify...
"$dotnetpath" build "../Textify.sln" --configuration $releaseconf
if [ ! $? == 0 ]; then
	echo Build failed.
	exit 1
fi

# Inform success
echo Build successful.
exit 0
